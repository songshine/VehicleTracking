// DemoDlg.cpp : ʵ���ļ�
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

THREADPARAM g_threadParam;		//�̲߳���
BOOL s_bFirstOn = TRUE;       //�Ƿ��ǵ�һ�θ�����Ƶ
IplImage* g_pFrame = NULL;    //��Ҫ����ĵ�ǰ��֡
BOOL g_bIsOverThread = FALSE;//�Ƿ���Ҫ������ǰ�߳�
BOOL g_bIsaThreadOn = FALSE;//�жϵ�ǰ�Ƿ����߳���������
BOOL g_bIsTracking = FALSE; //�ж��Ƿ���ʾ����״̬
BOOL g_bIsSorting = FALSE;  //�ж��Ƿ���ͼƬ����ʾ������
BOOL g_bHaveGotPosition = FALSE;//�ж��Ƿ��Ѿ��õ�����λ��
BOOL g_bIsChangePara = FALSE;//�ж��Ƿ��޸��˲���
CvPoint g_pointDown;		//�Ҽ���갴�µĵ�
CvPoint g_pointUp;			//�Ҽ����̧��ĵ�
char* g_cBegTime = NULL;	//��ʼ���ʱ��
CMutex g_mutexImage;   //ͼ��ʹ�û�����
BOOL g_bIsRecordVideo = FALSE;
CString g_strRecordVideoPath="record.avi";  //¼����Ƶ���·��
BOOL g_bIsRecordClass = TRUE;      //�Ƿ�¼�Ƴ���
#pragma endregion

#pragma region Global Methods

/*��ȡ����ͼƬ*/
IplImage* g_GetBackgroud(CString strVPath,int nFrame)//��ȡ����ͼƬ
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
		AfxMessageBox("��Ƶ��ʧ��");
		return NULL;
	}//��ȡ��Ƶ�ļ�

	while(pFrame = cvQueryFrame( pCapture ))
	{
		nFrmNum++;
		if(nFrmNum == 1)
		{
			pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrmMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);//ת��Ϊ�������飬������
		}
		if(nFrmNum == nFrame)
			break;
		cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
		cvConvert(pBkImg, pFrmMat);//ת��Ϊ�������飬������
		cvAddWeighted(pBkMat,1,pFrmMat,beta,0,pBkMat);
	}
	cvConvert(pBkMat, pBkImg);
	cvReleaseMat(&pBkMat);
	cvReleaseMat(&pFrmMat);
	cvReleaseCapture(&pCapture);
	return pBkImg;

}



/*��Ƶ�������ٵ��߳�Verson 1*/
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
		AfxMessageBox("��Ƶ��ʧ��");
		return 0;
	}
	g_cBegTime = m_pParam->pDemoDlg->GetBeginTime();
	g_mutexImage.Lock();
	while((g_pFrame = cvQueryFrame(pCapture)) && !g_bIsOverThread)//��Ƶ�����ڴ���ɶ�ȡ��ָ����һ֡�������ص�ǰ��ȡ��֡
	{
		g_mutexImage.Unlock();
		nFrmNum++;
		//����ǵ�һ֡����Ҫ�����ڴ棬����ʼ��
		if(nFrmNum == 2)
		{
			pFrImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);

			pBkImg = g_GetBackgroud(sFilePath,50);//��ȡ����ͼƬ
			cvConvert(pBkImg, pBkMat);
			//��˹�˲��ȣ���ƽ��ͼ��
			cvSmooth(pBkMat, pBkMat, CV_GAUSSIAN,3,0,0,0);
		}
		else if(nFrmNum > 2)
		{

			cvCvtColor(g_pFrame, pFrImg, CV_BGR2GRAY);
			cvConvert(pFrImg, pFrameMat);

			//��˹�˲��ȣ���ƽ��ͼ��
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN,3,0,0,0);


			//��ǰ֡������ͼ���,cvAbsDiff�������������ľ���ֵ
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);

			//��ֵ��ǰ��ͼ 
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			//ͨ�����ұ߽��ҳ�ROI���������ڵ��˶�������������ȫĿ�굵��
			//cvCanny(pFrImg, pBkImg, 50, 150, 3);

			cvDilate(pFrImg,pFrImg,NULL,1);//����
			/*���ٳ���������*/
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

			/*���±���*/
			if(nFrmNum%8 == 0)
				cvRunningAvg(pFrameMat, pBkMat, 0.005, 0);
			m_pParam->pDemoDlg->DisplayImage(g_pFrame);

		}
		g_mutexImage.Lock();
	}

	g_mutexImage.Unlock();
	//ɾ���ṹԪ��
	cvReleaseStructuringElement(&Element);

	if(nFrmNum != 0)
	{
		//�ͷ�ͼ��;���
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
	m_pParam->pDemoDlg->GetDlgItem(IDC_INFO)->SetWindowText("ͨ���Ҽ�==>ѡ���޸Ĳ���");
	return 0;
}




/*��Ƶ�������ٵ��߳�Verson 1*/
UINT VideoProcThread_2(LPVOID pParam)
{
	THREADPARAM *m_pParam = (WATERTHREADPARAM*)pParam;
	int nFrmNum = 0;               //���ڴ�����������    
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
		AfxMessageBox("��Ƶ��ʧ��");
		return 0;
	}
	g_bIsaThreadOn = TRUE;
	//g_cBegTime = m_pParam->pDemoDlg->GetBeginTime();

	while(!g_bIsOverThread && (g_pFrame = cvQueryFrame(pCapture)))//��Ƶ�����ڴ���ɶ�ȡ��ָ����һ֡�������ص�ǰ��ȡ��֡
	{
		nFrmNum++;
		//����ǵ�һ֡����Ҫ�����ڴ棬����ʼ��
		if(nFrmNum == 2)
		{
			pFrImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),IPL_DEPTH_8U,1);
			pBkImg = cvCreateImage(cvSize(g_pFrame->width, g_pFrame->height),IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(g_pFrame->height, g_pFrame->width, CV_32FC1);

			cvCvtColor(g_pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);
			//��˹�˲��ȣ���ƽ��ͼ��
			cvSmooth(pBkMat, pBkMat, CV_GAUSSIAN,3,0,0,0);
		}
		else if(nFrmNum > 2)
		{
			//cvNamedWindow("song",CV_WINDOW_AUTOSIZE);
			if(nFrmNum%2 == 0)
				continue;

			cvCvtColor(g_pFrame, pFrImg, CV_BGR2GRAY);

			cvConvert(pFrImg, pFrameMat);

			//��˹�˲��ȣ���ƽ��ͼ��
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN);

			//��ǰ֡������ͼ���,cvAbsDiff�������������ľ���ֵ
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);
			cvCopy(pFrameMat,pBkMat,NULL);

			//��ֵ��ǰ��ͼ 
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			cvDilate(pFrImg,pFrImg,NULL,3);//����

			/*���ٳ���������*/
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
	//ɾ���ṹԪ��
	//cvReleaseStructuringElement(&Element);
	if(nFrmNum != 0)
	{
		//�ͷ�ͼ��;���
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

/* ����Ӧ�ó��򡰹��ڡ��˵���� CAboutDlg �Ի���*/
/*About Dialog*/
class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

	// �Ի�������
	enum { IDD = IDD_ABOUTBOX };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

	// ʵ��
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

// CDemoDlg �Ի���
#pragma region Basic Methods

CAdminUIDlg::CAdminUIDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CAdminUIDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
		//ʹ������������
	CString strDir = GetCurPath();/*��ȡ����·��*/
	m_cKmeansPath = StrToChar(strDir+"\\kmeans.txt");/*���������ļ�����·��*/
	m_cResultPath = StrToChar(strDir+"\\result.txt");/*���������·��*/
	

	GetModelPaths(strDir);/*��ȡ������·��*/
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


// CDemoDlg ��Ϣ�������

BOOL CAdminUIDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// ��������...���˵�����ӵ�ϵͳ�˵��С�

	// IDM_ABOUTBOX ������ϵͳ���Χ�ڡ�
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

	// ���ô˶Ի����ͼ�ꡣ��Ӧ�ó��������ڲ��ǶԻ���ʱ����ܽ��Զ�
	//  ִ�д˲���
	SetIcon(m_hIcon, TRUE);			// ���ô�ͼ��
	SetIcon(m_hIcon, FALSE);		// ����Сͼ��

	// TODO: �ڴ���Ӷ���ĳ�ʼ������
	m_cdc = GetDlgItem(IDC_PICTURE)->GetDC(); 
	GetDlgItem(IDC_PICTURE)->GetClientRect(&m_rect); //��ȡpicture�ؼ����ھ�������
	SetIconList();//����ͼ��
	//GetDlgItem(IDC_INFO)->SetWindowText("ͨ���Ҽ�==>ѡ���޸Ĳ���");
	return TRUE;  // ���ǽ��������õ��ؼ������򷵻� TRUE
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

// �����Ի��������С����ť������Ҫ����Ĵ���
//  �����Ƹ�ͼ�ꡣ����ʹ���ĵ�/��ͼģ�͵� MFC Ӧ�ó���
//  �⽫�ɿ���Զ���ɡ�

void CAdminUIDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // ���ڻ��Ƶ��豸������

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// ʹͼ���ڹ��������о���
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// ����ͼ��
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

//���û��϶���С������ʱϵͳ���ô˺���ȡ�ù����ʾ��
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

/*��ͼ��img��ʾ�Ի����ϵ�picture�ؼ���*/
void CAdminUIDlg::DisplayImage(IplImage* img)
{//�ڿؼ��б�����ʾͼƬ

	m_cimg.CopyOf(img); 
	m_cimg.DrawToHDC(m_cdc->GetSafeHdc(),&m_rect); 

}
char* CAdminUIDlg::StrToChar(CString str)
{//��CStringת����char*��
	char* pCh;
	if(str.IsEmpty())
		return NULL;
	int len = str.GetLength();
	pCh = (char*)malloc((len+1)*sizeof(char));
	strcpy(pCh,str.GetBuffer(len));

	return pCh;
}
char* CAdminUIDlg::GetBeginTime()
{//��ȡϵͳ��ǰʱ��
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
			MessageBox("ȱ���㹻�ķ�������");
			return 0;
		}
	}
	_findclose(hFile);
	return 1;
}
#pragma endregion


void CAdminUIDlg::OnBtnOpenFile()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	// �򿪶Ի���	
	CFileDialog fileDlg(TRUE,NULL,_T(""),
		OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		_T("Vedio Files(*.avi;*.xvid)|*.avi;*.xvid|"));
	fileDlg.m_ofn.lpstrTitle = "����Ƶ�ļ��Ի���";
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
		OnInitThreadParam();//���ĸ��ٷ����̲߳���

		g_bHaveGotPosition = FALSE;//���»�ȡ��������
		g_bIsOverThread = TRUE;//�жϵ�ǰ�������е��߳�
		Sleep(100);//�ȴ���ǰ���е��߳�ֹͣ

		GetDlgItem(IDC_INFO)->SetWindowText("�������ѡȡҪ���ٵ�����(����������϶�ѡ��)...");
		m_pThread = AfxBeginThread(VideoProcThread_2, &g_threadParam, THREAD_PRIORITY_NORMAL, 0, 0, NULL);
		g_bIsOverThread = FALSE;

	}
}

void CAdminUIDlg::OnBtnPause()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;

	if (m_bIsPauseThread)
	{
		GetDlgItem(IDC_BTN_PAUSE)->SetWindowText("��ͣ");
		m_bIsPauseThread = FALSE;
		m_pThread->ResumeThread();
	} 
	else
	{
		GetDlgItem(IDC_BTN_PAUSE)->SetWindowText("����");
		m_bIsPauseThread = TRUE;
		m_pThread->SuspendThread();
	}
	UpdateData(FALSE);
}


void CAdminUIDlg::OnBtnExit()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if(IDYES == MessageBox("��ȷ��Ҫ�˳���",NULL,MB_YESNO))
		CDialog::OnCancel();
}

void CAdminUIDlg::OnDestroy()
{
	CDialog::OnDestroy();

	// TODO: �ڴ˴������Ϣ����������
	
}
void CAdminUIDlg::OnBtnOption()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	CMainOptionDlg dlg(&m_dataAccess);
	/*if (g_bIsaThreadOn)
	{
		MessageBox("��ȴ���ǰ��Ƶ����ֹͣ��");
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
			MessageBox("��������޸ĺ�����һ����Ƶ��ʱ��Ч��");
		g_bIsChangePara = TRUE;
		if(g_bIsRecordVideo)
			MessageBox("����¼����Ƶ���޷����Ĳ���");
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
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(!g_bHaveGotPosition)
	{
		MessageBox("�������ѡȡҪ���ٵ�����(����������϶�ѡ��)...");
		return;

	}
	if (g_bIsTracking)
	{
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("��ʾ���ٹ���");
		g_bIsTracking = FALSE;
	}
	else
	{
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("���ظ��ٹ���");
		g_bIsTracking = TRUE;
	}
	UpdateData(FALSE);
}

void CAdminUIDlg::OnBtnShowResult()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	if(!g_bHaveGotPosition)
	{
		MessageBox("�������ѡȡҪ���ٵ�����(����������϶�ѡ��)...");
		return;

	}
	if (g_bIsSorting)
	{
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("��ʾ������");
		g_bIsSorting = FALSE;
	}
	else
	{
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("���ط�����");
		g_bIsSorting = TRUE;
	}
	UpdateData(FALSE);
}

void CAdminUIDlg::OnRButtonDown(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
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
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	char* info = out_result_to_file(m_cResultPath,g_cBegTime);
	MessageBox(info);
	g_cBegTime = GetBeginTime();
	//MessageBox("����ɹ���");
}

void CAdminUIDlg::OnBtnStop()
{
	// TODO: �ڴ���������������
	if (!g_bIsaThreadOn)
		return;
	if (m_pThread == NULL)
		return;
	g_bIsOverThread = TRUE;
}

void CAdminUIDlg::OnLButtonDown(UINT nFlags, CPoint point)
{
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
	
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
	// TODO: �ڴ������Ϣ�����������/�����Ĭ��ֵ
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
		if(abs(g_pointUp.x-g_pointDown.x) < 80 || //��ֹѡ��ĸ��������С
			abs(g_pointUp.y-g_pointDown.y) < 80)
		{
			GetDlgItem(IDC_INFO)->SetWindowText("�����������ѡȡҪ���ٵ�����(����������϶�ѡ��)...");
			return;
		}
		g_bHaveGotPosition = TRUE;
		g_bIsSorting = TRUE;
		g_bIsTracking = TRUE;
		GetDlgItem(IDC_BTN_TRACK)->SetWindowText("���ظ��ٹ���");
		GetDlgItem(IDC_BTN_SHOW_RESULT)->SetWindowText("���ط�����");
		GetDlgItem(IDC_INFO)->SetWindowText("���ڸ��ٷ�����...");
	}
	
	CDialog::OnLButtonUp(nFlags, point);
}

void CAdminUIDlg::OnSetRoi()
{//ѡȡ����Ȥ����
	// TODO: �ڴ���������������
	g_bHaveGotPosition = FALSE;
	GetDlgItem(IDC_INFO)->SetWindowText("�������ѡȡҪ���ٵ�����(����������϶�ѡ��)...");
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
	// TODO: �ڴ����ר�ô����/����û���
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

	m_IconList.InsertItem(0, "����Ƶ�ļ�", 0);
	m_IconList.InsertItem(1,"¼����Ƶ",1);
	
	m_IconList.InsertItem(2, "����ѡ��", 2);

	m_IconList.InsertItem(3, "���ݹ���", 3);

	m_IconList.InsertItem(4, "�˳�", 4);
	//m_IconList.InsertItem(4, "Security", 4);
}
void CAdminUIDlg::OnRecordVideo()
{
	if(g_bIsaThreadOn && g_bHaveGotPosition)
	{
		if(g_bIsRecordVideo)
		{
			m_IconList.SetItemText(1,0,"¼����Ƶ");
			g_bIsRecordVideo = FALSE;

			GetDlgItem(IDC_INFO)->SetWindowText("���ڸ��ٷ�����...");
		}
		else
		{
			m_IconList.SetItemText(1,0,"ֹͣ¼��");
			g_bIsRecordVideo = TRUE;
			CString strVideoPath;
			strVideoPath.Format("����¼����Ƶ,��Ƶ���·��:%s",g_bIsRecordVideo);
			GetDlgItem(IDC_INFO)->SetWindowText(strVideoPath);
		}
	}
}
void CAdminUIDlg::OnNMClickList1(NMHDR *pNMHDR, LRESULT *pResult)
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������

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
