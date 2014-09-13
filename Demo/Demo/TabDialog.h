#pragma once
#include "afxcmn.h"

#ifdef _WIN32_WCE
#error "Windows CE ��֧�� CDHtmlDialog��"
#endif 

// CTabDialog �Ի���

class CTabDialog : public CDHtmlDialog
{
	DECLARE_DYNCREATE(CTabDialog)

public:
	CTabDialog(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CTabDialog();
// ��д
	HRESULT OnButtonOK(IHTMLElement *pElement);
	HRESULT OnButtonCancel(IHTMLElement *pElement);

// �Ի�������
	enum { IDD = IDD_DIALOG_TAB, IDH = IDR_HTML_TABDIALOG };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��
	virtual BOOL OnInitDialog();

	DECLARE_MESSAGE_MAP()
	DECLARE_DHTML_EVENT_MAP()
public:
	afx_msg void OnBnClickedOk();
	CTabCtrl m_tabCtrl;

	//CMainOptionDlg m_mainOptionDlg;
	//CRecordVideoDlg m_recordVideoDlg;
};
