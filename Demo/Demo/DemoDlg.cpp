// DemoDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Demo.h"
#include "DemoDlg.h"
#include "track_sort.h"
#include "OptionDlg.h"
#include <io.h>
#include "Vehicle.h"
#include "ManagerDlg.h"
#include "RoadInfo.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

#pragma region Global Variables

THREADPARAM g_threadParam;		//线程参数
BOOL s_bFirstOn = TRUE;       //是否是第一次跟踪视频
IplImage* g_pFrame = NULL;    //需要处理的当前的帧
BOOL g_bIsOverThread = FALSE;//是否需要结束当前线程
BOOL g_bIsaThreadOn = FALSE;//判断当前是否有线程正在运行
BOOL g_bIsTracking = FALSE; //判断是否显示跟踪状态
BOOL g_bIsSorting = FALSE;  //判断是否在图片上显示分类结果
BOOL g_bHaveGotPosition = FALSE;//判断是否已经得到跟踪位置
BOOL g_bIsChangePara = FALSE;//判断是否修改了参数
CvPoint g_pointDown;		//右键鼠标按下的点
CvPoint g_pointUp;			//右键鼠标抬起的点
char* g_cBegTime = NULL;	//开始检测时间
CMutex g_mutexImage;   //图像使用互斥量
BOOL g_bIsRecordVideo = FALSE;
CString g_strRecordVideoPath="record.avi";  //录制视频存放路径
BOOL g_bIsRecordClass = TRUE;      //是否录制车型
#pragma endregion

#pragma region Global Methods

/*获取背景图片*/
IplImage* g_GetBackgroud(CString strVPath,int nFrame)//获取背景图片
{
	IplImage* pFrame = NULL;
	IplImage* pBkImg = NULL;
	CvMat* pBkMat = NULL;
	CvMat* pFrmMat = NULL;
	CvCapture* pCapture = NULL;
	long nFrmNum = 0;
	double beta = (double)1.0/nFrame;
	if (!(pCapture = cvCreateFileCapture(strVPath)))
	{
		AfxMessageBox("视频打开失败");
		return NULL;
	}//读取视频文件

	while(pFrame = cvQueryFrame( pCapture ))
	{
		nFrmNum++;
		if(nFrmNum == 1)
		{
			pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrmMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);//转化为线性数组，即矩阵
		}
		if(nFrmNum == nFrame)
			break;
		cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
		cvConvert(pBkImg, pFrmMat);//转化为线性数组，即矩阵
		cvAddWeighted(pBkMat,1,pFrmMat,beta,0,pBkMat);
	}
	cvConvert(pBkMat, pBkImg);
	cvReleaseMat(&pBkMat);
	cvReleaseMat(&pFrmMat);
	cvReleaseCapture(&pCapture);
	return pBkImg;

}



/*视频车辆跟踪的线程Verson 1*/
UINT VideoProcThread_1(LPVOID pParam)
{
	THREADPARAM *m_pParam = (WATERTHREADPARAM*)pParam;
	int nFrmNum = 0;               
	IplImage* pFrImg = NULL;
	IplImage* pBkImg = NULL;
	CvMat* pFrameMat = NULL;
	CvMat* pFrMat = NULL;
	CvMat* pBkMat = NULL;
	CvCapture* pCapture = NULL;
	CvVideoWriter* pVideoWrite = NULL;
	CString sFilePath = m_pParam->vediopath;
	IplConvKernel* Element = cvCreateStructuringElementEx(13,13,1,1,CV_SHAPE_RECT,NULL);
	if (!(pCapture = cvCreateFileCapture(sFilePath)))
	{
		AfxMessageBox("视频打开失败");
		return 0;
	}
	g_cBegTime = m_pParam->pDemoDlg->GetBeginTime();
	g_mutexImage.Lock();
	while((g_pFrame = cvQueryFrame(pCapture)) && !g_bIsOverThread)//视频流，第次完成读取后指向下一帧，并返回当前读取的帧
	{
		g_mutexImage.Unlock();
		nFrmNum++;
		//如果是第一帧，需要申请内存，并初始化
		if(nFrmNum == 2)
		{
			pFrImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);

			pBkImg = g_GetBackgroud(sFilePath,50);//获取背景图片
			cvConvert(pBkImg, pBkMat);
			//高斯滤波先，以平滑图像
			cvSmooth(pBkMat, pBkMat, CV_GAUSSIAN,3,0,0,0);
		}
		else if(nFrmNum > 2)
		{

			cvCvtColor(g_pFrame, pFrImg, CV_BGR2GRAY);
			cvConvert(pFrImg, pFrameMat);

			//高斯滤波先，以平滑图像
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN,3,0,0,0);


			//当前帧跟背景图相减,cvAbsDiff计算两个数组差的绝对值
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);

			//二值化前景图 
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			//通过查找边界找出ROI矩形区域内的运动车辆，建立完全目标档案
			//cvCanny(pFrImg, pBkImg, 50, 150, 3);

			cvDilate(pFrImg,pFrImg,NULL,1);//膨胀
			/*跟踪车辆并分类*/
			if(g_bHaveGotPosition)
			{
				g_mutexImage.Lock();
				tracking(pFrImg,g_pFrame,nFrmNum,g_bIsTracking,&g_pointDown,&g_pointUp);

				if(g_bIsRecordVideo && pVideoWrite == NULL)
				{
					pVideoWrite = cvCreateVideoWriter(
						g_strRecordVideoPath,
						CV_FOURCC('X','V','I','D'),
						12,
						cvGetSize(g_pFrame)
						);					
				}
				else if(!g_bIsRecordVideo && pVideoWrite != NULL)
				{
					cvReleaseVideoWriter(&pVideoWrite);
				}
				if(g_bIsRecordVideo && pVideoWrite == NULL && !g_bIsRecordClass)
					cvWriteFrame(pVideoWrite,g_pFrame);
				sorting();
				if(g_bIsSorting)			
				{				
					show_class_on_image(g_pFrame);

					if(g_bIsRecordVideo && pVideoWrite == NULL && g_bIsRecordClass)
						cvWriteFrame(pVideoWrite,g_pFrame);
				}
				g_mutexImage.Unlock();
			}

			/*更新背景*/
			if(nFrmNum%8 == 0)
				cvRunningAvg(pFrameMat, pBkMat, 0.005, 0);
			m_pParam->pDemoDlg->DisplayImage(g_pFrame);

		}
		g_mutexImage.Lock();
	}

	g_mutexImage.Unlock();
	//删除结构元素
	cvReleaseStructuringElement(&Element);

	if(nFrmNum != 0)
	{
		//释放图像和矩阵
		cvReleaseImage(&pFrImg);
		cvReleaseImage(&pBkImg);
		cvReleaseMat(&pFrameMat);
		cvReleaseMat(&pFrMat);
		cvReleaseMat(&pBkMat);
		cvReleaseCapture(&pCapture);
		pCapture = NULL;

		if(pVideoWrite != NULL)
			cvReleaseVideoWriter(&pVideoWrite);
	}

	m_pParam->pDemoDlg->Invalidate(TRUE);
	g_bIsaThreadOn = FALSE;
	m_pParam->pDemoDlg->OnBtnSave();
	m_pParam->pDemoDlg->GetDlgItem(IDC_INFO)->SetWindowText("通过右键==>选项修改参数");
	return 0;
}




/*视频车辆跟踪的线程Verson 1*/
UINT VideoProcThread_2(LPVOID pParam)
{
	THREADPARAM *m_pParam = (WATERTHREADPARAM*)pParam;
	int nFrmNum = 0;               //用于存放特征点个数    
	IplImage* pFrImg = NULL;
	IplImage* pBkImg = NULL;
	CvMat* pFrameMat = NULL;
	CvMat* pFrMat = NULL;
	CvMat* pBkMat = NULL;
	CvCapture* pCapture = NULL;
	CvVideoWriter* pVideoWrite = NULL;
	CString sFilePath = m_pParam->vediopath;
	//IplConvKernel* Element = cvCreateStructuringElementEx(13,13,1,1,CV_SHAPE_RECT,NULL);
	if (!(pCapture = cvCreateFileCapture(sFilePath)))
	{
		AfxMessageBox("视频打开失败");
		return 0;
	}
	g_bIsaThreadOn = TRUE;
	//g_cBegTime = m_pParam->pDemoDlg->GetBeginTime();

	while(!g_bIsOverThread && (g_pFrame = cvQueryFrame(pCapture)))//视频流，第次完成读取后指向下一帧，并返回当前读取的帧
	{
		nFrmNum++;
		//如果是第一帧，需要申请内存，并初始化
		if(nFrmNum == 2)
		{
			pFrImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),IPL_DEPTH_8U,1);
			pBkImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);

			cvCvtColor(g_pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);
			//高斯滤波先，以平滑图像
			cvSmooth(pBkMat, pBkMat, CV_GAUSSIAN,3,0,0,0);
		}
		else if(nFrmNum > 2)
		{
			//cvNamedWindow("song",CV_WINDOW_AUTOSIZE);
			if(nFrmNum%2 == 0)
				continue;

			cvCvtColor(g_pFrame, pFrImg, CV_BGR2GRAY);

			cvConvert(pFrImg, pFrameMat);

			//高斯滤波先，以平滑图像
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN);

			//当前帧跟背景图相减,cvAbsDiff计算两个数组差的绝对值
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);
			cvCopy(pFrameMat,pBkMat,NULL);

			//二值化前景图 
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			cvDilate(pFrImg,pFrImg,NULL,3);//膨胀

			/*跟踪车辆并分类*/
			if(g_bHaveGotPosition)
			{
				g_mutexImage.Lock();

				if(g_bIsRecordVideo && pVideoWrite == NULL)
				{
					pVideoWrite = cvCreateVideoWriter(
						g_strRecordVideoPath,
						CV_FOURCC('X','V','I','D'),
						12,
						cvGetSize(g_pFrame)
						);					
				}
				else if(!g_bIsRecordVideo && pVideoWrite != NULL)
				{
					cvReleaseVideoWriter(&pVideoWrite);
				}

				if(g_bIsRecordVideo && pVideoWrite == NULL && !g_bIsRecordClass)
				{
					cvWriteFrame(pVideoWrite,g_pFrame);
				}
				tracking(
					pFrImg,g_pFrame,
					nFrmNum,
					g_bIsTracking,
					&g_pointDown,
					&g_pointUp
					);
				sorting();
				if(g_bIsSorting)			
				{				
					show_class_on_image(g_pFrame);

					if(g_bIsRecordVideo && pVideoWrite == NULL && g_bIsRecordClass)
						cvWriteFrame(pVideoWrite,g_pFrame);
				}
				g_mutexImage.Unlock();
			}

			m_pParam->pDemoDlg->DisplayImage(g_pFrame);

		}
	}
	//删除结构元素
	//cvReleaseStructuringElement(&Element);
	if(nFrmNum != 0)
	{
		//释放图像和矩阵
		cvReleaseImage(&pFrImg);
		cvReleaseImage(&pBkImg);
		cvReleaseMat(&pFrameMat);
		cvReleaseMat(&pFrMat);
		cvReleaseMat(&pBkMat);
		cvReleaseCapture(&pCapture);
		pCapture = NULL;


		if(pVideoWrite != NULL)
			cvReleaseVideoWriter(&pVideoWrite);
	}


	m_pParam->pDemoDlg->Invalidate(TRUE);	
	g_bIsaThreadOn = FALSE;
	return 0;
}

#pragma endregion

#pragma region About Dialog

/* 用于应用程序“关于”菜单项的 CAboutDlg 对话框*/
/*About Dialog*/
class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

	// 对话框数据
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	// 实现
protected:
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
END_MESSAGE_MAP()


#pragma endregion

#pragma region CDemoDlg Dialog

// CDemoDlg 对话框
#pragma region Basic Methods

CAdminUIDlg::CAdminUIDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CAdminUIDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
		//使窗口有序排列
	CString strDir = GetCurPath();/*获取绝对路径*/
	m_cKmeansPath = StrToChar(strDir+"\\kmeans.txt");/*聚类中心文件所在路径*/
	m_cResultPath = StrToChar(strDir+"\\result.txt");/*分类结果存放路径*/
	

	GetModelPaths(strDir);/*获取分类器路径*/
	m_pThread = NULL;
	m_cdc = NULL;
	m_bIsPauseThread = FALSE;
	m_nKmeans = 100;

	m_roadId = 1;
	m_vehicleQueue.SetDataAccess(&m_dataAccess);
	
}

CAdminUIDlg::~CAdminUIDlg()
{
	//free_all_para();
	/*if(m_cdc != NULL)
		ReleaseDC(m_cdc); 
	free(m_cKmeansPath);
	free(m_cResultPath);*/
	g_bIsOverThread = TRUE;

}

void CAdminUIDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LIST1, m_IconList);
}

BEGIN_MESSAGE_MAP(CAdminUIDlg, CDialog)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BTN_OPEN_FILE, &CAdminUIDlg::OnBtnOpenFile)
	ON_BN_CLICKED(IDC_BTN_PAUSE, &CAdminUIDlg::OnBtnPause)
	ON_BN_CLICKED(IDC_BTN_EXIT, &CAdminUIDlg::OnBtnExit)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_BTN_OPTION, &CAdminUIDlg::OnBtnOption)
	ON_BN_CLICKED(IDC_BTN_TRACK, &CAdminUIDlg::OnBtnTrack)
	ON_BN_CLICKED(IDC_BTN_SHOW_RESULT, &CAdminUIDlg::OnBtnShowResult)
	ON_WM_RBUTTONDOWN()
	ON_BN_CLICKED(IDC_BTN_SAVE, &CAdminUIDlg::OnBtnSave)
	ON_COMMAND(IDC_BTN_STOP, &CAdminUIDlg::OnBtnStop)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
//	ON_WM_MOUSEMOVE()
ON_COMMAND(ID_SET_ROI, &CAdminUIDlg::OnSetRoi)
ON_COMMAND(ID_ANALYSE, &CAdminUIDlg::OnAnalyse)
ON_NOTIFY(NM_CLICK, IDC_LIST1, &CAdminUIDlg::OnNMClickList1)
END_MESSAGE_MAP()


// CDemoDlg 消息处理程序

BOOL CAdminUIDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// 将“关于...”菜单项添加到系统菜单中。

	// IDM_ABOUTBOX 必须在系统命令范围内。
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// 设置此对话框的图标。当应用程序主窗口不是对话框时，框架将自动
	//  执行此操作
	SetIcon(m_hIcon, TRUE);			// 设置大图标
	SetIcon(m_hIcon, FALSE);		// 设置小图标

	// TODO: 在此添加额外的初始化代码
	m_cdc = GetDlgItem(IDC_PICTURE)->GetDC(); 
	GetDlgItem(IDC_PICTURE)->GetClientRect(&m_rect); //获取picture控件所在矩形区域
	SetIconList();//设置图标
	//GetDlgItem(IDC_INFO)->SetWindowText("通过右键==>选项修改参数");
	return TRUE;  // 除非将焦点设置到控件，否则返回 TRUE
}

void CAdminUIDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// 如果向对话框添加最小化按钮，则需要下面的代码
//  来绘制该图标。对于使用文档/视图模型的 MFC 应用程序，
//  这将由框架自动完成。

void CAdminUIDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // 用于绘制的设备上下文

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// 使图标在工作矩形中居中
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// 绘制图标
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//当用户拖动最小化窗口时系统调用此函数取得光标显示。
//
HCURSOR CAdminUIDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}
void CAdminUIDlg::OnInitThreadParam()
{
	g_threadParam.hwnd=m_hWnd;
	g_threadParam.pDemoDlg=this;
	g_threadParam.vediopath = m_strVideoPath;
}

#pragma endregion

#pragma region Tool Methods

/*将图像img显示对话框上的picture控件上*/
void CAdminUIDlg::DisplayImage(IplImage* img)
{//在控件列表中显示图片

	m_cimg.CopyOf(img); 
	m_cimg.DrawToHDC(m_cdc->GetSafeHdc(),&m_rect); 

}
char* CAdminUIDlg::StrToChar(CString str)
{//从CString转换成char*型
	char* pCh;
	if(str.IsEmpty())
		return NULL;
	int len = str.GetLength();
	pCh = (char*)malloc((len+1)*sizeof(char));
	strcpy(pCh,str.GetBuffer(len));

	return pCh;
}
char* CAdminUIDlg::GetBeginTime()
{//获取系统当前时间
	time_t rawtime;
	struct tm * timeinfo;

	time ( &rawtime );
	timeinfo = localtime ( &rawtime );
	return asctime (timeinfo);
}
BOOL CAdminUIDlg::SetRoadInfo()
{
	CRoadInfo* pRi = new CRoadInfo();
	if(IDOK != pRi->DoModal())
	{
		return FALSE;
	}
	return TRUE;
}


int CAdminUIDlg::GetModelPaths(CString strPath)
{
	CString pathWild =strPath+"\\*.model" ;
	int index = 0;
	struct _finddata_t c_file;
	long hFile;

	if( (hFile = _findfirst( LPCTSTR(pathWild), &c_file )) == -1L )
	{
		MessageBox("No Modle files in current directory!\n" ) ;
		return 0;
	}
	else
	{
		do {
			CString strTemp = strPath+"\\"+c_file.name;
			m_cModelPaths[index++] = StrToChar(strTemp);
		} while (_findnext(hFile, &c_file) == 0);
		if(index != VTYPE*(VTYPE-1)/2)
		{
			MessageBox("缺少足够的分类器！");
			return 0;
		}
	}
	_findclose(hFile);
	return 1;
}
#pragma endregion


void CAdminUIDlg::OnBtnOpenFile()
{
	// TODO: 在此添加控件通知处理程序代码
	// 打开对话框	
	CFileDialog fileDlg(TRUE,NULL,_T(""),
		OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		_T("Vedio Files(*.avi;*.xvid)|*.avi;*.xvid|"));
	fileDlg.m_ofn.lpstrTitle = "打开视频文件对话框";
	if(IDOK == fileDlg.DoModal())
	{
		/*if(!SetRoadInfo())
			return;*/
		if(s_bFirstOn)
		{
			m_vehicleQueue.Start();
			init_envir(
				m_cModelPaths,
				m_cKmeansPath,
				m_nKmeans,
				m_roadId,
				&m_vehicleQueue);
			s_bFirstOn = FALSE;
		}
		if(g_bIsChangePara)
		{
			free_all_para();
			init_envir(
				m_cModelPaths,
				m_cKmeansPath,
				m_nKmeans,
				m_roadId,
				&m_vehicleQueue);
			g_bIsChangePara = FALSE;
		}

		m_strVideoPath = fileDlg.GetPathName();
		OnInitThreadParam();//更改跟踪分类线程参数

		g_bHaveGotPosition = FALSE;//重新获取跟踪区域
		g_bIsOverThread = TRUE;//中断当前正在运行的线程
		Sleep(100);//等待当前运行的线程停止

		GetDlgItem(IDC_INFO)->SetWindowText("请用鼠标选取要跟踪的区域(按左键不放拖动选择)...");
		m_pThread = AfxBeginThread(VideoProcThread_2, &g_threadParam, THREAD_PRIORITY_NORMAL, 0, 0, NULL);
		g_bIsOverThread = FALSE;

	}
}

void CAdminUIDlg::OnBtnPause()
{
	// TODO: 在此添加控件通知处理程序代码
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;

	if (m_bIsPauseThread)
	{
		GetDlgItem(IDC_BTN_PAUSE)->SetWindowText("暂停");
		m_bIsPauseThread = FALSE;
		m_pThread->ResumeThread();
	} 
	else
	{
		GetDlgItem(IDC_BTN_PAUSE)->SetWindowText("启动");
		m_bIsPauseThread = TRUE;
		m_pThread->SuspendThread();
	}
	UpdateData(FALSE);
}


void CAdminUIDlg::OnBtnExit()
{
	// TODO: 在此添加控件通知处理程序代码
	if(IDYES == MessageBox("你确定要退出吗？",NULL,MB_YESNO))
		CDialog::OnCancel();
}

void CAdminUIDlg::OnDestroy()
{
	CDialog::OnDestroy();

	// TODO: 在此处添加消息处理程序代码
	
}
void CAdminUIDlg::OnBtnOption()
{
	// TODO: 在此添加控件通知处理程序代码
	CMainOptionDlg dlg(&m_dataAccess);
	/*if (g_bIsaThreadOn)
	{
		MessageBox("请等待当前视频分类停止！");
		return;
	}*/

	if(IDOK == dlg.DoModal())
	{
		m_nKmeans = dlg.m_nKmeans;
		GetModelPaths(dlg.m_strClassPath);

		if(m_cKmeansPath != NULL)
			free(m_cKmeansPath);
		m_cKmeansPath = StrToChar(dlg.m_strKmeansPath);

		if(m_cResultPath != NULL)
			free(m_cResultPath);

		if(g_bIsaThreadOn)
			MessageBox("输入参数修改后将在下一次视频打开时生效！");
		g_bIsChangePara = TRUE;
		if(g_bIsRecordVideo)
			MessageBox("正在录制视频，无法更改参数");
		else
		{
			g_strRecordVideoPath = dlg.m_strRecordVideo;
			g_bIsRecordClass = dlg.m_bIsRecordClass;
		}
		m_roadId = dlg.GetRoadId();
	}
}

void CAdminUIDlg::OnBtnTrack()
{
	// TODO: 在此添加控件通知处理程序代码
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(!g_bHaveGotPosition)
	{
		MessageBox("请用鼠标选取要跟踪的区域(按左键不放拖动选择)...");
		return;

	}
	if (g_bIsTracking)
	{
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("显示跟踪过程");
		g_bIsTracking = FALSE;
	}
	else
	{
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("隐藏跟踪过程");
		g_bIsTracking = TRUE;
	}
	UpdateData(FALSE);
}

void CAdminUIDlg::OnBtnShowResult()
{
	// TODO: 在此添加控件通知处理程序代码
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(!g_bHaveGotPosition)
	{
		MessageBox("请用鼠标选取要跟踪的区域(按左键不放拖动选择)...");
		return;

	}
	if (g_bIsSorting)
	{
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("显示分类结果");
		g_bIsSorting = FALSE;
	}
	else
	{
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("隐藏分类结果");
		g_bIsSorting = TRUE;
	}
	UpdateData(FALSE);
}

void CAdminUIDlg::OnRButtonDown(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	if(m_rect.PtInRect(point))		
	{
		CMenu *pMenu=new CMenu;
		pMenu->LoadMenu(IDR_MENU1);
		CMenu *pM=pMenu->GetSubMenu(0);
		ClientToScreen(&point);
		pM->TrackPopupMenu(TPM_RIGHTBUTTON, point.x, 
			point.y, this);
	}
	CDialog::OnRButtonDown(nFlags, point);

}

void CAdminUIDlg::OnBtnSave()
{
	// TODO: 在此添加控件通知处理程序代码
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	char* info = out_result_to_file(m_cResultPath,g_cBegTime);
	MessageBox(info);
	g_cBegTime = GetBeginTime();
	//MessageBox("保存成功！");
}

void CAdminUIDlg::OnBtnStop()
{
	// TODO: 在此添加命令处理程序代码
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	g_bIsOverThread = TRUE;
}

void CAdminUIDlg::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	
	UpdateData(TRUE);	
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(g_bHaveGotPosition)
	{
		OnBtnPause();
		return;
	}
	if(m_rect.PtInRect(point) && g_pFrame != NULL)
	{
		float k_h = (float)g_pFrame->height/m_rect.Height();
		float k_w = (float)g_pFrame->width/m_rect.Width();
		g_pointDown.x = (int)(point.x*k_h);
		g_pointDown.y = (int)(point.y*k_w);
	}
	CDialog::OnLButtonDown(nFlags, point);
}

void CAdminUIDlg::OnLButtonUp(UINT nFlags, CPoint point)
{
	// TODO: 在此添加消息处理程序代码和/或调用默认值
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(g_bHaveGotPosition)
		return;
	if(m_rect.PtInRect(point) && g_pFrame != NULL)
	{
		float k_h = (float)g_pFrame->height/m_rect.Height();
		float k_w = (float)g_pFrame->width/m_rect.Width();
		g_pointUp.x = (int)(point.x*k_h);
		g_pointUp.y = (int)(point.y*k_w);
		if(abs(g_pointUp.x-g_pointDown.x) < 80 || //防止选择的跟踪区域过小
			abs(g_pointUp.y-g_pointDown.y) < 80)
		{
			GetDlgItem(IDC_INFO)->SetWindowText("请用鼠标重新选取要跟踪的区域(按左键不放拖动选择)...");
			return;
		}
		g_bHaveGotPosition = TRUE;
		g_bIsSorting = TRUE;
		g_bIsTracking = TRUE;
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("隐藏跟踪过程");
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("隐藏分类结果");
		GetDlgItem(IDC_INFO)->SetWindowText("正在跟踪分类中...");
	}
	
	CDialog::OnLButtonUp(nFlags, point);
}

void CAdminUIDlg::OnSetRoi()
{//选取感兴趣区域
	// TODO: 在此添加命令处理程序代码
	g_bHaveGotPosition = FALSE;
	GetDlgItem(IDC_INFO)->SetWindowText("请用鼠标选取要跟踪的区域(按左键不放拖动选择)...");
}


void CAdminUIDlg::OnAnalyse()
{
	PROCESS_INFORMATION pi;
	STARTUPINFO si = {sizeof(si)};
	if(CreateProcess(NULL,"MIS_1.exe",NULL,NULL,FALSE,0,NULL,NULL,&si,&pi))
	{
		//WaitForSingleObject(pi.hProcess,INFINITE);
		CloseHandle(pi.hThread);
		CloseHandle(pi.hProcess);
	}
}



BOOL CAdminUIDlg::PreTranslateMessage(MSG* pMsg)
{
	// TODO: 在此添加专用代码和/或调用基类
	if(pMsg-> message == WM_LBUTTONDBLCLK)     
	{ 
		CPoint   ptCursor; 
		GetCursorPos(&ptCursor);        
		if(m_rect.PtInRect(ptCursor)) 
		{
			OnBtnOpenFile();
		}
	}
	return CDialog::PreTranslateMessage(pMsg);
}
void CAdminUIDlg::SetIconList()
{
	m_ImageList.Create(32, 32, ILC_COLOR16|ILC_MASK,1, 4);

	HICON hIcon = ::LoadIcon (AfxGetResourceHandle(), MAKEINTRESOURCE(IDI_OPEN));
	m_ImageList.Add(hIcon);
	hIcon = ::LoadIcon (AfxGetResourceHandle(), MAKEINTRESOURCE(IDI_RECORD));
	m_ImageList.Add(hIcon);
	hIcon = ::LoadIcon (AfxGetResourceHandle(), MAKEINTRESOURCE(IDI_OPTION));
	m_ImageList.Add(hIcon);
	hIcon = ::LoadIcon (AfxGetResourceHandle(), MAKEINTRESOURCE(IDI_STATISTIC));
	m_ImageList.Add(hIcon);	
	hIcon = ::LoadIcon (AfxGetResourceHandle(), MAKEINTRESOURCE(IDI_EXIT));
	m_ImageList.Add(hIcon);
	m_IconList.SetImageList(&m_ImageList, LVSIL_NORMAL);

	CRect rc;
	m_IconList.GetClientRect(rc);

	// set new icon spacing
	m_IconList.SetIconSpacing(rc.Width(), 110);

	// change colors
	m_IconList.SetTextColor(RGB(0,0,0));
	m_IconList.SetTextBkColor(RGB(180,180,200));
	m_IconList.SetBkColor(RGB(180,180,200));	

	//insert items
	m_IconList.InsertColumn(0, "OutlookBar");

	m_IconList.InsertItem(0, "打开视频文件", 0);
	m_IconList.InsertItem(1,"录制视频",1);
	
	m_IconList.InsertItem(2, "设置选项", 2);

	m_IconList.InsertItem(3, "数据管理", 3);

	m_IconList.InsertItem(4, "退出", 4);
	//m_IconList.InsertItem(4, "Security", 4);
}
void CAdminUIDlg::OnRecordVideo()
{
	if(g_bIsaThreadOn && g_bHaveGotPosition)
	{
		if(g_bIsRecordVideo)
		{
			m_IconList.SetItemText(1,0,"录制视频");
			g_bIsRecordVideo = FALSE;

			GetDlgItem(IDC_INFO)->SetWindowText("正在跟踪分类中...");
		}
		else
		{
			m_IconList.SetItemText(1,0,"停止录制");
			g_bIsRecordVideo = TRUE;
			CString strVideoPath;
			strVideoPath.Format("正在录制视频,视频存放路径:%s",g_bIsRecordVideo);
			GetDlgItem(IDC_INFO)->SetWindowText(strVideoPath);
		}
	}
}
void CAdminUIDlg::OnNMClickList1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: 在此添加控件通知处理程序代码

	int nIndex = m_IconList.GetNextItem(-1, LVNI_ALL | LVNI_SELECTED); 

	if(nIndex == -1)
		return;
	switch(nIndex)
	{
		case 0:
			OnBtnOpenFile();
			break;	
		case 1:
			OnRecordVideo();
			break;
		case 2:
			OnBtnOption();
			break;
		case 3:
			OnAnalyse();
			break;
		case 4:
			OnBtnExit();
			break;
	}

	*pResult = 0;
}

#pragma endregion
