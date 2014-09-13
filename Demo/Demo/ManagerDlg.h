#pragma once
#include "afxcmn.h"
#include "Vehicle.h"
#include "ConditionDlg.h"
// CManagerDlg �Ի���

class CManagerDlg : public CDialog
{
	DECLARE_DYNAMIC(CManagerDlg)

public:
	CManagerDlg(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CManagerDlg();

// �Ի�������
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

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
	/*������������ʵ�ֲ�ͬ�����µĲ�ѯ*/
	void SearchItCan(CString strIt);
	void SearchWithCondition(CConditionDlg& dlg);
public:
	virtual BOOL OnInitDialog();
public:
	CListCtrl m_ctrList;
public:
	afx_msg void OnNMDblclkListResult(NMHDR *pNMHDR, LRESULT *pResult);

public://��ʾ���ݿ��еĳ���ͼƬ
	BOOL ShowImageFromDB(CDBVariant& varValue);

private://�ھ�̬�ı�������ʾ��Ϣ
	void ShowBmpImage(CString strPath);
	void ShowStaticInfo(CString strInfo);
	void ShowStaticInfo(int nInfo);
	/*�жϼ�¼�е�ʱ���Ƿ���ͨ�������Ի����ȡ����ʼʱ��֮��*/
	BOOL IsBetweenTwoTime(CConditionDlg& dlg,CRecordset& set);
	
private:
	CConditionDlg m_condDlg;
	afx_msg HBRUSH OnCtlColor(CDC* pDC, CWnd* pWnd, UINT nCtlColor);
	afx_msg void OnBtnCrystal();
};
