#pragma once
#include "afxwin.h"


// CConditionDlg 对话框

class CConditionDlg : public CDialog
{
	DECLARE_DYNAMIC(CConditionDlg)

public:
	CConditionDlg(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CConditionDlg();

// 对话框数据
	enum { IDD = IDD_DIALOG2 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CComboBox m_comRoad;
public:
	CComboBox m_comType;
private:
	void InitTwoList();
	virtual BOOL OnInitDialog();
	afx_msg void OnBnClickedOk();
public:
	int m_nBY;
	int m_nBMo;
	int m_nBD;
	int m_nBH;
	int m_nBMi;
	int m_nEY;
	int m_nEMo;
	int m_nED;
	int m_nEH;
	int m_nEMi;
	CString m_strRoad;
	CString m_strType;
public:
	afx_msg HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
};
