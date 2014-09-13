// DemoDlg.h : ͷ�ļ�
//

#pragma once
#include "Vehicle.h"
#include "afxcmn.h"
#include "DataAccuess.h"
#include "VehicleQueue.h"

// CDemoDlg �Ի���
class CAdminUIDlg : public CDialog
{
// ����
public:
	CAdminUIDlg(CWnd* pParent = NULL);	// ��׼���캯��
	virtual ~CAdminUIDlg();
// �Ի�������
	enum { IDD = IDD_DEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	void DisplayImage(IplImage* img);//��IplimageͼƬ��ʾ��picture�ؼ���
	void OnInitThreadParam();//��ʼ�����ٷ�����Ƶ����
	char* GetBeginTime();//��ȡ���ٿ�ʼʱ��
	char* StrToChar(CString str);
	//void BeginTest(CString vediopath);
public:
	afx_msg void OnBtnOpenFile();//��Ӧ���ļ��ĵİ�ť
public:
	afx_msg void OnBtnPause();//��Ӧ��ͣ�İ�ť

public:
	afx_msg void OnBtnExit();//��Ӧ�˳���ť
public:
	afx_msg void OnDestroy();
private:
	CString m_strVideoPath; //��Ƶ�ļ�����·��
	CWinThread  * m_pThread;//���ٷ�����Ƶ�߳�
	CvvImage m_cimg;//������picture�ؼ�����ʾͼƬ
	CDC* m_cdc;//������ʾͼƬ���û��豸�ӿ�
	CRect m_rect;//picture�ؼ��ľ�������
	BOOL m_bIsPauseThread;//�ж��Ƿ���ͣ��Ƶ
	char* m_cModelPaths[VTYPE*(VTYPE-1)/2]; //������·����Ĭ�ϵ�ǰ·��
	char* m_cKmeansPath;  //���������ļ�����·����Ĭ��·��Ϊ��ǰ·��
	char* m_cResultPath; //���������·��
	int m_nKmeans;//������������Ĭ��Ϊ100
	int m_roadId;

public:
	
	afx_msg void OnBtnOption();//��Ӧѡ�ť
public:
	afx_msg void OnBtnTrack();//��Ӧ��ʾ���ٹ��̵İ�ť
public:
	afx_msg void OnBtnShowResult();//��Ӧ��ʾ�������İ�ť
public:
	int GetModelPaths(CString strPath);//���ļ����л�ȡ������·��
public:
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnBtnSave();
public:
	afx_msg void OnBtnStop();
public:
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
public:
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
public:
	afx_msg void OnSetRoi();
public:
	afx_msg void OnAnalyse();
public:
	virtual BOOL PreTranslateMessage(MSG* pMsg);
public:
	CListCtrl m_IconList;
	CImageList m_ImageList;
	void SetIconList();
	void OnRecordVideo();
	BOOL SetRoadInfo();
public:
	afx_msg void OnNMClickList1(NMHDR *pNMHDR, LRESULT *pResult);

private:
	DataAccess m_dataAccess;
	VehicleQueue m_vehicleQueue;

};
typedef struct WATERTHREADPARAM 
{//���ٷ����̲߳���
	HWND hwnd;
	CAdminUIDlg* pDemoDlg;
	//CvCapture* pCapture;
	CString vediopath;
}THREADPARAM;
