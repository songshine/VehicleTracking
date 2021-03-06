// OptionDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Demo.h"
#include "OptionDlg.h"
#include "DataAccuess.h"

// COptionDlg 对话框

IMPLEMENT_DYNAMIC(CMainOptionDlg, CDialog)

CMainOptionDlg::CMainOptionDlg(DataAccess* dataAccess,CWnd* pParent)
	: CDialog(CMainOptionDlg::IDD, pParent)
	, m_strClassPath(_T(""))
	, m_strKmeansPath(_T(""))
	, m_nKmeans(0)
	, m_strRecordVideo(_T(""))
	
{
	CString strdit = GetCurPath();
	m_strClassPath = strdit;
	m_strKmeansPath = strdit+"\\kmeans.txt";
	m_strRecordVideo = strdit+"record.avi";
	m_nKmeans = 100;	

	m_pCityList = NULL;
	m_pRegionList = NULL;
	m_pRoadList = NULL;
	m_bIsRecordClass = FALSE;
	m_pDataAccess = dataAccess;

}

CMainOptionDlg::~CMainOptionDlg()
{
	
}

void CMainOptionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_CLASSIFIER_PATH, m_strClassPath);
	DDX_Text(pDX, IDC_EDIT_KMEANS_PATH, m_strKmeansPath);
	DDX_Text(pDX, IDC_EDIT_KMEANS_NUM, m_nKmeans);
	DDX_Control(pDX, IDC_COMBO_CITY, m_cbCtrlCity);
	DDX_Control(pDX, IDC_COMBO_REGION, m_cbCtrlRegion);
	DDX_Text(pDX,IDC_EDIT_RECORD_VIDEO,m_strRecordVideo);
	DDX_Control(pDX, IDC_COMBO_ROAD, m_cbCtrlRoad);
	DDX_Control(pDX, IDC_CHECK_SHOW_CLASS, m_checkCtrl);
	DDX_Check(pDX, IDC_CHECK_SHOW_CLASS, m_bIsRecordClass);
}


BEGIN_MESSAGE_MAP(CMainOptionDlg, CDialog)
	ON_BN_CLICKED(IDC_BTN_CLASSIFIER, &CMainOptionDlg::OnBtnClassifier)
	ON_BN_CLICKED(IDC_BTN_KMEANS, &CMainOptionDlg::OnBtnKmeans)
	ON_BN_CLICKED(IDOK, &CMainOptionDlg::OnBnClickedOk)
	ON_CBN_SELCHANGE(IDC_COMBO_CITY, &CMainOptionDlg::OnCbnSelchangeComboCity)
	ON_CBN_SELCHANGE(IDC_COMBO_REGION, &CMainOptionDlg::OnCbnSelchangeComboRegion)
	ON_BN_CLICKED(IDC_BTN_RECORD_VIDEO, &CMainOptionDlg::OnBnClickedBtnRecordVideo)
	ON_BN_CLICKED(IDC_CHECK_SHOW_CLASS, &CMainOptionDlg::OnBnClickedCheckShowClass)
END_MESSAGE_MAP()


// COptionDlg 消息处理程序
BOOL CMainOptionDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	InitList();
	return TRUE;  // return TRUE unless you set the focus to a control
}
void CMainOptionDlg::OnBtnClassifier()
{
	
	UpdateData();
	BROWSEINFO bInfo;
	ZeroMemory(&bInfo, sizeof(bInfo));
	bInfo.hwndOwner =GetSafeHwnd();
	bInfo.lpszTitle = _T("请选分类器文件夹的路径: ");
	bInfo.ulFlags = BIF_RETURNONLYFSDIRS; 

	LPITEMIDLIST lpDlist; //用来保存返回信息的IDList
	lpDlist = SHBrowseForFolder(&bInfo) ; //显示选择对话框
	if(lpDlist != NULL) //用户按了确定按钮
	{
		TCHAR chPath[MAX_PATH]; //用来存储路径的字符串
		SHGetPathFromIDList(lpDlist, chPath);//把项目标识列表转化成字符串
		m_strClassPath = chPath; //将TCHAR类型的字符串转换为CString类型的字符串
	}
	UpdateData(FALSE);
}

void CMainOptionDlg::OnBtnKmeans()
{
	
	UpdateData();
	CFileDialog fileDlg(TRUE,NULL,_T(""),
		OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		_T("Text Files(*.txt)|*.txt|"));
	fileDlg.m_ofn.lpstrTitle = "打开聚类文件对话框";
	if(IDOK == fileDlg.DoModal())
	{
		m_strKmeansPath = fileDlg.GetPathName();
	}
	UpdateData(FALSE);
}



void CMainOptionDlg::OnBnClickedOk()
{
	
	UpdateData();
	if(	m_strClassPath.IsEmpty() 
		|| m_strKmeansPath.IsEmpty() 
		|| m_nKmeans == 0
		|| CB_ERR == m_cbCtrlRoad.GetCurSel())
	{
		MessageBox("请将信息填写完整");
		return;
	}

	m_roadId = (int)((CIdName*)m_pRoadList->GetAt(m_cbCtrlRoad.GetCurSel()))->m_id;
	OnOK();
}
void CMainOptionDlg::UpdateList(CComboBox& comboBox, CObArray* pArrayList)
{
	int count = pArrayList->GetSize();

	comboBox.ResetContent();
	for(int i=0;i<count;i++)
	{
		CIdName* idName = (CIdName*)pArrayList->GetAt(i);
		comboBox.AddString(idName->m_name);
	}
	if(count > 0)
		comboBox.SetCurSel(0);
}

//Inits City、Region and Road list.
void CMainOptionDlg::InitList()
{
	m_pCityList = m_pDataAccess->GetCityList();
	UpdateList(m_cbCtrlCity,m_pCityList);

	if(m_pCityList != NULL 
		&& (m_pCityList->GetSize() > 0))
	{
		m_pRegionList = m_pDataAccess->GetRegionListByCityId(
			((CIdName*)m_pCityList->GetAt(0))->m_id
			);
		UpdateList(m_cbCtrlRegion,m_pRegionList);
	}

	if( m_pCityList != NULL 
		&& m_pCityList->GetSize() > 0 
		&& m_pRegionList != NULL
		&& m_pRegionList->GetSize() > 0)
	{
		m_pRoadList = m_pDataAccess->GetRoadListByCityAndRegionId(
			((CIdName*)m_pCityList->GetAt(0))->m_id,
			((CIdName*)m_pRegionList->GetAt(0))->m_id);
		UpdateList(m_cbCtrlRoad,m_pRoadList);
	}

}

//Changes City
void CMainOptionDlg::OnCbnSelchangeComboCity()
{
	long cityId = 0, regionId = 0; 
	int curSelIndex = m_cbCtrlCity.GetCurSel();
	if(CB_ERR != curSelIndex)
		cityId = ((CIdName*)m_pCityList->GetAt(curSelIndex))->m_id;

	//Updates Region List
	m_pRegionList->RemoveAll();
	delete m_pRegionList;
	m_pRegionList = m_pDataAccess->GetRegionListByCityId(cityId);
	UpdateList(m_cbCtrlRegion,m_pRegionList);

	//Updates Road List
	m_pRoadList->RemoveAll();
	delete m_pRoadList;
	if(m_pRegionList->GetSize() > 0)
	{			
		regionId = ((CIdName*)m_pRegionList->GetAt(0))->m_id;		
	}	
	m_pRoadList = m_pDataAccess->GetRoadListByCityAndRegionId(cityId,regionId);	
	UpdateList(m_cbCtrlRoad,m_pRoadList);

}

//Changes Region
void CMainOptionDlg::OnCbnSelchangeComboRegion()
{
	long cityId = 0, regionId = 0;
	if(CB_ERR != m_cbCtrlCity.GetCurSel())
		cityId = ((CIdName*)m_pCityList->GetAt(m_cbCtrlCity.GetCurSel()))->m_id;
	if(CB_ERR != m_cbCtrlRegion.GetCurSel())
		regionId = ((CIdName*)m_pRegionList->GetAt(m_cbCtrlRegion.GetCurSel()))->m_id;

	//Updates Road List
	m_pRoadList->RemoveAll();
	delete m_pRoadList;
	m_pRoadList = m_pDataAccess->GetRoadListByCityAndRegionId(cityId,regionId);
	UpdateList(m_cbCtrlRoad,m_pRoadList);
}

void CMainOptionDlg::OnBnClickedBtnRecordVideo()
{

	UpdateData();
	CFileDialog fileDlg(FALSE,NULL,_T(""),
		OFN_HIDEREADONLY | OFN_OVERWRITEPROMPT,
		_T("AVI Files(*.avu)|*.avi|"));
	fileDlg.m_ofn.lpstrTitle = "保存视频文件";
	if(IDOK == fileDlg.DoModal())
	{
		m_strRecordVideo = fileDlg.GetPathName();
	}
	UpdateData(FALSE);
}

void CMainOptionDlg::OnBnClickedCheckShowClass()
{
	//m_bIsRecordClass = !m_bIsRecordClass;
	UpdateData(FALSE);
}
