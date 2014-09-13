// ConditionDlg.cpp : 实现文件
//

#include "stdafx.h"
#include "Demo.h"
#include "ConditionDlg.h"


// CConditionDlg 对话框

IMPLEMENT_DYNAMIC(CConditionDlg, CDialog)

CConditionDlg::CConditionDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CConditionDlg::IDD, pParent)
	, m_nBY(0)
	, m_nBMo(0)
	, m_nBD(0)
	, m_nBH(0)
	, m_nBMi(0)
	, m_nEY(0)
	, m_nEMo(0)
	, m_nED(0)
	, m_nEH(0)
	, m_nEMi(0)
{
	m_nBY = 2010;
	m_nBMo = 1;
	m_nBD = 1;

	m_nEY = 2010;
	m_nEMo = 1;
	m_nED = 1;
}

CConditionDlg::~CConditionDlg()
{
}

void CConditionDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_COM_ROAD, m_comRoad);
	DDX_Control(pDX, IDC_COM_TYPE, m_comType);
	DDX_Text(pDX, IDC_B_Y, m_nBY);
	DDV_MinMaxInt(pDX, m_nBY, 2010, 3000);
	DDX_Text(pDX, IDC_B_MO, m_nBMo);
	DDV_MinMaxInt(pDX, m_nBMo, 1, 12);
	DDX_Text(pDX, IDC_B_D, m_nBD);
	DDV_MinMaxInt(pDX, m_nBD, 1, 31);
	DDX_Text(pDX, IDC_B_H, m_nBH);
	DDV_MinMaxInt(pDX, m_nBH, 0, 23);
	DDX_Text(pDX, IDC_B_MI, m_nBMi);
	DDV_MinMaxInt(pDX, m_nBMi, 0, 59);
	DDX_Text(pDX, IDC_E_Y, m_nEY);
	DDV_MinMaxInt(pDX, m_nEY, 2010, 3000);
	DDX_Text(pDX, IDC_E_MO, m_nEMo);
	DDV_MinMaxInt(pDX, m_nEMo, 1, 12);
	DDX_Text(pDX, IDC_E_D, m_nED);
	DDV_MinMaxInt(pDX, m_nED, 1, 31);
	DDX_Text(pDX, IDC_E_H, m_nEH);
	DDV_MinMaxInt(pDX, m_nEH, 0, 23);
	DDX_Text(pDX, IDC_E_MI, m_nEMi);
	DDV_MinMaxInt(pDX, m_nEMi, 0, 59);
}


BEGIN_MESSAGE_MAP(CConditionDlg, CDialog)
	ON_BN_CLICKED(IDOK, &CConditionDlg::OnBnClickedOk)
	ON_WM_CTLCOLOR()
END_MESSAGE_MAP()

BOOL CConditionDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// TODO:  在此添加额外的初始化
	InitTwoList();
	return TRUE;  // return TRUE unless you set the focus to a control
	// 异常: OCX 属性页应返回 FALSE
}

// CConditionDlg 消息处理程序
void CConditionDlg::InitTwoList()
{
	CDatabase db1;
	db1.Open("VehicleRecord");
	CRecordset vehSet1(&db1);
	CString strSql;
	strSql.Format("select * from VehicleType");	
	vehSet1.Open(CRecordset::forwardOnly,strSql);
	CDBVariant varValue;
	short i = 1;
	while(!vehSet1.IsEOF())
	{
		vehSet1.GetFieldValue(i,varValue);
		m_comType.AddString(*varValue.m_pstring);
		vehSet1.MoveNext();
	}
	db1.Close();

	CDatabase db2;
	db2.Open("VehicleRecord");
	CRecordset vehSet2(&db2);
	strSql.Format("select * from RoadCrossing");	
	vehSet2.Open(CRecordset::forwardOnly,strSql);
	while(!vehSet2.IsEOF())
	{
		vehSet2.GetFieldValue(i,varValue);
		m_comRoad.AddString(*varValue.m_pstring);
		vehSet2.MoveNext();
	}

	db2.Close();
}

void CConditionDlg::OnBnClickedOk()
{
	// TODO: 在此添加控件通知处理程序代码
	UpdateData();
	CTime t1(m_nBY,m_nBMo,m_nBD,m_nBH,m_nBMi,0);
	CTime t2(m_nEY,m_nEMo,m_nED,m_nEH,m_nEMi,0);
	if(t1 >= t2)
	{
		MessageBox("开始和结束时间时间不合法！");
		return;
	}
	if(m_comRoad.GetCurSel() == -1)
	{
		MessageBox("请选择道口!");
		return;
	}
	if(m_comType.GetCurSel() == -1)
	{
		MessageBox("请选择车辆类型！");
		return;
	}
	int id1,id2;
	id1 = m_comRoad.GetCurSel();
	m_comRoad.GetLBText(id1,m_strRoad);

	id2 = m_comType.GetCurSel();
	m_comType.GetLBText(id2,m_strType);
	OnOK();
}

HBRUSH CConditionDlg::OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor)
{
	HBRUSH hbr = CDialog::OnCtlColor(pDC, pWnd, nCtlColor);

	// TODO:  在此更改 DC 的任何属性
	if(pWnd->GetDlgCtrlID() == IDC_STATIC_CONDITION)
	{
		pDC->SetTextColor(RGB(0,0,255));
	}

	// TODO:  如果默认的不是所需画笔，则返回另一个画笔
	return hbr;
}
