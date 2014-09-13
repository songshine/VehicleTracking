// RoadInfo.cpp : ʵ���ļ�
//

#include "stdafx.h"
#include "Demo.h"
#include "RoadInfo.h"


// CRoadInfo �Ի���

IMPLEMENT_DYNAMIC(CRoadInfo, CDialog)

CRoadInfo::CRoadInfo(CWnd* pParent /*=NULL*/)
	: CDialog(CRoadInfo::IDD, pParent)
{
	m_nRoadId = -1;
}

CRoadInfo::~CRoadInfo()
{

}

void CRoadInfo::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COMBO2, m_comCity);
	DDX_Control(pDX, IDC_COMBO3, m_comRegion);
	DDX_Control(pDX, IDC_COMBO4, m_comRoad);
}


BEGIN_MESSAGE_MAP(CRoadInfo, CDialog)
	ON_CBN_SELCHANGE(IDC_COMBO2, &CRoadInfo::OnCbnSelchangeCombo2)
	ON_CBN_SELCHANGE(IDC_COMBO3, &CRoadInfo::OnCbnSelchangeCombo3)
	ON_BN_CLICKED(IDOK, &CRoadInfo::OnBnClickedOk)
END_MESSAGE_MAP()


// CRoadInfo ��Ϣ�������
void CRoadInfo::InitCombox()
{
	InitCityCombox();
	InitRegionCombox();
	InitRoadCombox();
}

void CRoadInfo::InitCityCombox()
{//��ʼ���������б�
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql = "select * from City";
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 1;
	CDBVariant varValue;
	while(!Rs.IsEOF())
	{
		Rs.GetFieldValue(i,varValue);
		m_comCity.AddString(*varValue.m_pstring);
		Rs.MoveNext();
	}
	Rs.Close();
	db.Close();
}
void CRoadInfo::InitRegionCombox()
{//��ʼ�������б�
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql = "select * from Region";
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 1;
	CDBVariant varValue;
	while(!Rs.IsEOF())
	{
		Rs.GetFieldValue(i,varValue);
		m_comRegion.AddString(*varValue.m_pstring);
		Rs.MoveNext();
	}
	Rs.Close();
	db.Close();
}
void CRoadInfo::InitRoadCombox()
{//��ʼ�������б�
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql = "select * from RoadCrossing";
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 1;
	CDBVariant varValue;
	while(!Rs.IsEOF())
	{
		Rs.GetFieldValue(i,varValue);
		m_comRoad.AddString(*varValue.m_pstring);
		Rs.MoveNext();
	}
	Rs.Close();
	db.Close();
}
BOOL CRoadInfo::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  �ڴ���Ӷ���ĳ�ʼ��
	InitCombox();
	return TRUE;  // return TRUE unless you set the focus to a control
	// �쳣: OCX ����ҳӦ���� FALSE
}

void CRoadInfo::OnCbnSelchangeCombo2()
{//�����б仯ʱ��Ӧ
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	int index = m_comCity.GetCurSel();
	CString cityname;
	m_comCity.GetLBText(index,cityname);
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql ;
	strSql.Format("select RegionName from City,Region where City.CityId=Region.CityId and City.CityName = '%s'",cityname);
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 0;
	CDBVariant varValue;
	int count = m_comRegion.GetCount();
	for (int i = count - 1; i >= 0; i--)
	{
		m_comRegion.DeleteString(i);
	}
	while(!Rs.IsEOF())
	{
		Rs.GetFieldValue(i,varValue);
		m_comRegion.AddString(*varValue.m_pstring);
		Rs.MoveNext();
	}
	Rs.Close();
	db.Close();

}

void CRoadInfo::OnCbnSelchangeCombo3()
{//�������仯ʱ��Ӧ
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	int indexCity = m_comCity.GetCurSel();
	int indexRegion = m_comRegion.GetCurSel();
	if(indexCity == -1)
	{
		m_comCity.SetCurSel(-1);
		MessageBox("����ѡ��������!");
		return;
	}
	CString cityname,regionname;
	m_comCity.GetLBText(indexCity,cityname);
	m_comRegion.GetLBText(indexRegion,regionname);
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql ;
	strSql.Format("select Name from City,Region,RoadCrossing where City.CityId=RoadCrossing.CityId  and Region.RegionId = RoadCrossing.RegionId and City.CityName = '%s' and Region.RegionName='%s'",cityname,regionname);
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 0;
	CDBVariant varValue;
	int count = m_comRoad.GetCount();
	for (int i = count - 1; i >= 0; i--)
	{
		m_comRoad.DeleteString(i);
	}
	while(!Rs.IsEOF())
	{
		Rs.GetFieldValue(i,varValue);
		m_comRoad.AddString(*varValue.m_pstring);
		Rs.MoveNext();
	}
	Rs.Close();
	db.Close();
}

void CRoadInfo::OnBnClickedOk()
{
	// TODO: �ڴ���ӿؼ�֪ͨ����������
	CDatabase db;
	db.Open("VehicleRecord");
	CRecordset Rs(&db);
	CString strSql ;
	int indexCity = m_comCity.GetCurSel();
	int indexRegion = m_comRegion.GetCurSel();
	int indexRoad = m_comRoad.GetCurSel();
	CString cityname,regionname,roadname;
	m_comCity.GetLBText(indexCity,cityname);
	m_comRegion.GetLBText(indexRegion,regionname);
	m_comRoad.GetLBText(indexRoad,roadname);
	strSql.Format("select Id from City,Region,RoadCrossing where City.CityId=RoadCrossing.CityId  and Region.RegionId = RoadCrossing.RegionId and City.CityName = '%s' and Region.RegionName='%s' and RoadCrossing.Name='%s'",cityname,regionname,roadname);
	Rs.Open(CRecordset::forwardOnly,strSql);

	short i = 0;
	CDBVariant varValue;
	Rs.GetFieldValue(i,varValue);
	m_nRoadId = varValue.m_iVal;
	OnOK();
}
