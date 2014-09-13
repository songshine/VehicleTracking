#pragma once
#include "afxwin.h"


// CRoadInfo 对话框

class CRoadInfo : public CDialog
{
	DECLARE_DYNAMIC(CRoadInfo)

public:
	CRoadInfo(CWnd* pParent = NULL);   // 标准构造函数
	virtual ~CRoadInfo();

// 对话框数据
	enum { IDD = IDD_DIALOG3 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV 支持

	DECLARE_MESSAGE_MAP()
public:
	CComboBox m_comCity;
public:
	CComboBox m_comRegion;
public:
	CComboBox m_comRoad;
public:
	void InitCombox();
	void InitCityCombox();
	void InitRegionCombox();
	void InitRoadCombox();
public:
	virtual BOOL OnInitDialog();
public:
	afx_msg void OnCbnSelchangeCombo2();
public:
	afx_msg void OnCbnSelchangeCombo3();
public:
	afx_msg void OnBnClickedOk();
public:
	int m_nRoadId;//道口编号
};
