#pragma once
#include "afxwin.h"


// CRoadInfo �Ի���

class CRoadInfo : public CDialog
{
	DECLARE_DYNAMIC(CRoadInfo)

public:
	CRoadInfo(CWnd* pParent = NULL);   // ��׼���캯��
	virtual ~CRoadInfo();

// �Ի�������
	enum { IDD = IDD_DIALOG3 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV ֧��

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
	int m_nRoadId;//���ڱ��
};
