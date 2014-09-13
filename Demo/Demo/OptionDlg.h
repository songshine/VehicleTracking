#pragma once
#include "afxwin.h"
#include "DataAccuess.h"

// COptionDlg 对话框

class CMainOptionDlg : public CDialog
{
	DECLARE_DYNAMIC(CMainOptionDlg)

public:
	CMainOptionDlg(DataAccess* dataAccess,CWnd* pParent = NULL);

	virtual ~CMainOptionDlg();

// 对话框数据
	enum { IDD = IDD_DLG_OPTION };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBtnClassifier();
public:
	afx_msg void OnBtnKmeans();

public:
	CString m_strClassPath;
public:
	CString m_strKmeansPath;
	int m_roadId;
public:
	int m_nKmeans;

public:
	afx_msg void OnBnClickedOk();
public:
	CComboBox m_cbCtrlCity;
	CComboBox m_cbCtrlRegion;
	CComboBox m_cbCtrlRoad;
private:
	DataAccess* m_pDataAccess;
	CObArray* m_pCityList;
	CObArray* m_pRegionList;
	CObArray* m_pRoadList;

	void UpdateList(CComboBox& comboBox, CObArray* pArrayList);
public:
	virtual BOOL OnInitDialog();
private:
	void InitList();
public:

	afx_msg void OnCbnSelchangeComboCity();
	afx_msg void OnCbnSelchangeComboRegion();
	CButton m_checkCtrl;
	afx_msg void OnBnClickedBtnRecordVideo();
public:
	int GetRoadId(){return m_roadId;}
	CString m_strRecordVideo;
	BOOL m_bIsRecordClass;
	
	afx_msg void OnBnClickedCheckShowClass();
};
