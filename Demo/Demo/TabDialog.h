#pragma once
#include "afxcmn.h"

#ifdef _WIN32_WCE
#error "Windows CE 不支持 CDHtmlDialog。"
#endif 

// CTabDialog 对话框

class CTabDialog : public CDHtmlDialog
{
	DECLARE_DYNCREATE(CTabDialog)

public:
	CTabDialog(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CTabDialog();
// 重写
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// 对话框数据
	enum { IDD = IDD_DIALOG_TAB, IDH = IDR_HTML_TABDIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持
	virtual BOOL OnInitDialog();

	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	afx_msg void OnBnClickedOk();
	CTabCtrl m_tabCtrl;

	//CMainOptionDlg m_mainOptionDlg;
	//CRecordVideoDlg m_recordVideoDlg;
};
