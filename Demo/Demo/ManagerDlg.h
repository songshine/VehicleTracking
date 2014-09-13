#pragma once
#include "afxcmn.h"
#include "Vehicle.h"
#include "ConditionDlg.h"
// CManagerDlg 对话框

class CManagerDlg : public CDialog
{
	DECLARE_DYNAMIC(CManagerDlg)

public:
	CManagerDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CManagerDlg();

// 对话框数据
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CString m_strSearch;
public:
	afx_msg void OnkedBtnSearch();
public:
	afx_msg void OnStaticCondition();
public:
	void InitList();
	//void DeleteAllListItem();
	BOOL InsertAItem(CRecordset& set,BOOL bIsWithCons = FALSE);
	void InsertAllSetData(CRecordset& set);
	/*以下两个函数实现不同条件下的查询*/
	void SearchItCan(CString strIt);
	void SearchWithCondition(CConditionDlg& dlg);
public:
	virtual BOOL OnInitDialog();
public:
	CListCtrl m_ctrList;
public:
	afx_msg void OnNMDblclkListResult(NMHDR *pNMHDR, LRESULT *pResult);

public://显示数据库中的车辆图片
	BOOL ShowImageFromDB(CDBVariant& varValue);

private://在静态文本框中显示信息
	void ShowBmpImage(CString strPath);
	void ShowStaticInfo(CString strInfo);
	void ShowStaticInfo(int nInfo);
	/*判断记录中的时间是否在通过条件对话框获取的起始时间之间*/
	BOOL IsBetweenTwoTime(CConditionDlg& dlg,CRecordset& set);
	
private:
	CConditionDlg m_condDlg;
	afx_msg HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
	afx_msg void OnBtnCrystal();
};
