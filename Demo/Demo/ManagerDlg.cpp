// ManagerDlg.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "Demo.h"
#include "ManagerDlg.h"
#include "Vehicle.h"
#include "ConditionDlg.h"

// CManagerDlg �Ի���

IMPLEMENT_DYNAMIC(CManagerDlg, CDialog)

CManagerDlg::CManagerDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CManagerDlg::IDD, pParent)
	, m_strSearch(_T(""))
{
	m_strSearch = "���복�����ͻ��ߵ�������";
}

CManagerDlg::~CManagerDlg()
{
}

void CManagerDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Text(pDX, IDC_EDIT_SEARCH, m_strSearch);
	DDX_Control(pDX, IDC_LIST_RESULT, m_ctrList);
}


BEGIN_MESSAGE_MAP(CManagerDlg, CDialog)
	ON_BN_CLICKED(IDC_BTN_SEARCH, &CManagerDlg::OnkedBtnSearch)
	ON_STN_CLICKED(IDC_STATIC_CONDITION, &CManagerDlg::OnStaticCondition)
	ON_NOTIFY(NM_DBLCLK, IDC_LIST_RESULT, &CManagerDlg::OnNMDblclkListResult)
	ON_WM_CTLCOLOR()
	ON_BN_CLICKED(IDC_BTN_CRYSTAL, &CManagerDlg::OnBtnCrystal)
END_MESSAGE_MAP()


// CManagerDlg ��Ϣ�������



BOOL CManagerDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  �ڴ���Ӷ���ĳ�ʼ��
	InitList();
	//InitDataBase();
	return TRUE;  // return TRUE unless you set the focus to a control
	// �쳣: OCX ����ҳӦ���� FALSE
}
void CManagerDlg::InitList()
{
	DWORD NewStyle = m_ctrList.GetExtendedStyle();
	NewStyle |= LVS_EX_GRIDLINES;
	m_ctrList.SetExtendedStyle(NewStyle);

	m_ctrList.SetTextColor(RGB(0,0,255));
	m_ctrList.SetBkColor(RGB(204,232,207));
	m_ctrList.SetTextBkColor(RGB(204,232,207));
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset vehSet(&db);
	CString strSql;
	strSql.Format("select Vehicle.Id,VehicleType.Name,RoadCrossing.Name,RecordTime\
				  from Vehicle,VehicleType,RoadCrossing\
				  where Vehicle.TypeId=VehicleType.Id and Vehicle.RoadId=RoadCrossing.Id");
	vehSet.Open(CRecordset::forwardOnly,strSql);

	CString strField[4]={"  ���","  ����","  ����","  ʱ��"};
	m_ctrList.InsertColumn(0,strField[0],LVCFMT_LEFT,100);
	m_ctrList.InsertColumn(1,strField[1],LVCFMT_LEFT,100);
	m_ctrList.InsertColumn(2,strField[2],LVCFMT_LEFT,120);
	m_ctrList.InsertColumn(3,strField[3],LVCFMT_LEFT,200);
	
	InsertAllSetData(vehSet);
	db.Close();
}
void CManagerDlg::OnkedBtnSearch()
{/*��Ӧ���Ұ�ť*/
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	UpdateData();
	SearchItCan(m_strSearch);
}

void CManagerDlg::OnStaticCondition()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	if(IDOK == m_condDlg.DoModal())
	{
		SearchWithCondition(m_condDlg);
	}
}
//void CManagerDlg::InitDataBase()
//{
//	CDatabase* m_pDB;
//	m_pDB = new CDatabase();
//	m_pDB->Open("VehicleRecord");
//	m_pVeSet = new CVehicle(m_pDB);
//	CString strSql = "select * from Vehicle";
//	m_pVeSet->Open(CRecordset::snapshot,strSql);
//}
BOOL CManagerDlg::InsertAItem(CRecordset& set,BOOL bIsWithCons)
{/*����һ����¼*/
	short i=0;
	CDBVariant varValue;
	int nItem=m_ctrList.GetItemCount();
	nItem=m_ctrList.GetItemCount();
	set.GetFieldValue(i,varValue);
	CString str;
	str.Format("    %s",*varValue.m_pstring);
	m_ctrList.InsertItem(nItem,str);

	for(i=1;i<3;i++)
	{
		set.GetFieldValue(i,varValue);
		str.Format("   %s",*varValue.m_pstring);
		str.TrimRight();
		m_ctrList.SetItemText(nItem,i,str);
	}		
	set.GetFieldValue(i,varValue);

	//te = varValue.m_pdate;
	str.Format("  %04d/%02d/%02d %02d:%02d:%02d",varValue.m_pdate->year,
		varValue.m_pdate->month,varValue.m_pdate->day,
		varValue.m_pdate->hour,varValue.m_pdate->minute,
		varValue.m_pdate->second);

	CTime begTime(m_condDlg.m_nBY,m_condDlg.m_nBMo,m_condDlg.m_nBD,m_condDlg.m_nBH,m_condDlg.m_nBMi,0);
	CTime endTime(m_condDlg.m_nEY,m_condDlg.m_nEMo,m_condDlg.m_nED,m_condDlg.m_nEH,m_condDlg.m_nEMi,0);
	CTime nowTime(varValue.m_pdate->year,varValue.m_pdate->month,
		varValue.m_pdate->day,varValue.m_pdate->hour,varValue.m_pdate->minute,0);
	if(bIsWithCons)
	{
		if(nowTime >= begTime && nowTime <= endTime)
		{
			m_ctrList.SetItemText(nItem,i,str);
			return TRUE;
		}
		else
		{
			m_ctrList.DeleteItem(nItem);
			return FALSE;
		}
	}
	else
	{
		m_ctrList.SetItemText(nItem,i,str);
		return TRUE;
	}

}
void CManagerDlg::InsertAllSetData(CRecordset& set)
{/*�����ݿ��е����м�¼������*/
	int nItem = 0;
	int nAll = 0;
	while(!set.IsEOF())
	{
		InsertAItem(set);
		nAll++;
		set.MoveNext();		
	}
	ShowStaticInfo(nAll);
}



void CManagerDlg::OnNMDblclkListResult(NMHDR *pNMHDR, LRESULT *pResult)
{/*���б����˫��*/
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset vehSet(&db);
	CString strSql,strId,strTemp;
	strSql.Format("select * from Vehicle");
	vehSet.Open(CRecordset::forwardOnly,strSql);
	short i = 0;
	int nIdex=m_ctrList.GetSelectionMark();
	CDBVariant varValue;
	while(!vehSet.IsEOF())
	{
		vehSet.GetFieldValue(i,varValue);
		strTemp.Format("%s",*varValue.m_pstring);
		strId = m_ctrList.GetItemText(nIdex,0); 
		strId.TrimLeft();
		strId.TrimRight();
		if(strId == strTemp)
		{
			i = 3;
			vehSet.GetFieldValue(i,varValue);
			ShowImageFromDB(varValue);
			return;
		}
		vehSet.MoveNext();
	}
	
	*pResult = 0;
}
BOOL CManagerDlg::ShowImageFromDB(CDBVariant& varValue)
{/*�����ݿ��е�ͼƬ��Ӧ��ʾ*/
	CFile file;
	CFileStatus fileStaus;
	varValue.m_pbinary;
	long nSize = varValue.m_pbinary->m_dwDataLength;
	file.Open("zxy.bmp",CFile::modeCreate | CFile::modeWrite);
	void* buffer = GlobalLock(varValue.m_pbinary->m_hData);
	file.Write(buffer,varValue.m_pbinary->m_dwDataLength);
	GlobalUnlock(varValue.m_pbinary->m_hData);
	file.Close();
	
	ShowBmpImage("zxy.bmp");
	return TRUE;
}
void CManagerDlg::ShowBmpImage(CString strPath)
{/*����·����ʾbmp��ʽ��ͼƬ*/
	IplImage* img;
	img = cvLoadImage(strPath);
	cvNamedWindow("song",CV_WINDOW_AUTOSIZE);
	cvShowImage("song",img);
}
void CManagerDlg::ShowStaticInfo(CString strInfo)
{
	GetDlgItem(IDC_STATIC_INFO)->SetWindowText(strInfo);
}

void CManagerDlg::ShowStaticInfo(int nInfo)
{
	CString strInfo;
	strInfo.Format("��ǰ��¼����Ϊ%d .˫����Ų鿴��ӦͼƬ.",nInfo);
	ShowStaticInfo(strInfo);
}


void CManagerDlg::SearchItCan(CString strIt)
{/*���ݸ�����Ϣ��ѯ*/
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset vehSet(&db);
	CString strSql;
	strSql.Format("select Vehicle.Id,VehicleType.Name,RoadCrossing.Name,RecordTime\
				  from Vehicle,VehicleType,RoadCrossing\
				  where Vehicle.TypeId=VehicleType.Id and\
				  Vehicle.RoadId=RoadCrossing.Id and (VehicleType.Name='%s' or RoadCrossing.Name='%s')", strIt,strIt);
	vehSet.Open(CRecordset::forwardOnly,strSql);
	m_ctrList.DeleteAllItems();
	short i = 1;
	int nAll = 0;
	CDBVariant varValue;
	while(!vehSet.IsEOF())
	{

		InsertAItem(vehSet);
		nAll++;
		vehSet.MoveNext();
	}
	ShowStaticInfo(nAll);
}

void CManagerDlg::SearchWithCondition(CConditionDlg& dlg)
{
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset vehSet(&db);
	CString strSql;
	strSql.Format("select Vehicle.Id,VehicleType.Name,RoadCrossing.Name,RecordTime\
				  from Vehicle,VehicleType,RoadCrossing\
				  where Vehicle.TypeId=VehicleType.Id and\
				  Vehicle.RoadId=RoadCrossing.Id and VehicleType.Name='%s' and RoadCrossing.Name='%s'", dlg.m_strType,dlg.m_strRoad);
	vehSet.Open(CRecordset::forwardOnly,strSql);
	m_ctrList.DeleteAllItems();
	short i = 1;
	int nAll = 0;
	while(!vehSet.IsEOF())
	{
		//setTemp = vehSet;
		if(InsertAItem(vehSet,TRUE))
			nAll++;
		vehSet.MoveNext();
	}
	ShowStaticInfo(nAll);
	vehSet.Close();
	db.Close();
}

BOOL CManagerDlg::IsBetweenTwoTime(CConditionDlg& dlg,CRecordset& set)
{
	short i = 3;
	CDBVariant varValue;
	CTime begTime(dlg.m_nBY,dlg.m_nBMo,dlg.m_nBD,dlg.m_nBH,dlg.m_nBMi,0);
	CTime endTime(dlg.m_nEY,dlg.m_nEMo,dlg.m_nED,dlg.m_nEH,dlg.m_nEMi,0);

	set.GetFieldValue(i,varValue);
	CTime nowTime(varValue.m_pdate->year,varValue.m_pdate->month,
		varValue.m_pdate->day,varValue.m_pdate->hour,varValue.m_pdate->minute,0);

	if(nowTime >= begTime && nowTime <= endTime)
		return TRUE;
	else
		return FALSE;
	return TRUE;
}
HBRUSH CManagerDlg::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	HBRUSH hbr = CDialog::OnCtlColor(pDC, pWnd, nCtlColor);

	// TODO:  �ڴ˸��� DC ���κ�����
	UINT id = pWnd->GetDlgCtrlID();
	if( id== IDC_STATIC_CONDITION)
	{
		pDC->SetTextColor(RGB(0,0,255));
	}
	// TODO:  ���Ĭ�ϵĲ������軭�ʣ��򷵻���һ������
	return hbr;
}

void CManagerDlg::OnBtnCrystal()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	PROCESS_INFORMATION pi;
	STARTUPINFO si = {sizeof(si)};
	//TCHAR cCommandLine[_MAX_PATH];
	if(CreateProcess(NULL,"song_2.exe",NULL,NULL,FALSE,0,NULL,NULL,&si,&pi))
	{
		WaitForSingleObject(pi.hProcess,INFINITE);
		CloseHandle(pi.hThread);
		CloseHandle(pi.hProcess);
	}
}
