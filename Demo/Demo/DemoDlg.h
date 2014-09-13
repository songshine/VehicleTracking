// DemoDlg.h : 头文件
//

#pragma once
#include "Vehicle.h"
#include "afxcmn.h"
#include "DataAccuess.h"
#include "VehicleQueue.h"

// CDemoDlg 对话框
class CAdminUIDlg : public CDialog
{
// 构造
public:
	CAdminUIDlg(CWnd* pParent = NULL);	// 标准构造函数
	virtual ~CAdminUIDlg();
// 对话框数据
	enum { IDD = IDD_DEMO_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV 支持


// 实现
protected:
	HICON m_hIcon;

	// 生成的消息映射函数
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	void DisplayImage(IplImage* img);//将Iplimage图片显示在picture控件上
	void OnInitThreadParam();//初始化跟踪分类视频参数
	char* GetBeginTime();//获取跟踪开始时间
	char* StrToChar(CString str);
	//void BeginTest(CString vediopath);
public:
	afx_msg void OnBtnOpenFile();//响应打开文件的的按钮
public:
	afx_msg void OnBtnPause();//响应暂停的按钮

public:
	afx_msg void OnBtnExit();//响应退出按钮
public:
	afx_msg void OnDestroy();
private:
	CString m_strVideoPath; //视频文件所在路径
	CWinThread  * m_pThread;//跟踪分类视频线程
	CvvImage m_cimg;//用于在picture控件上显示图片
	CDC* m_cdc;//用于显示图片的用户设备接口
	CRect m_rect;//picture控件的矩形区域
	BOOL m_bIsPauseThread;//判断是否暂停视频
	char* m_cModelPaths[VTYPE*(VTYPE-1)/2]; //分类器路径，默认当前路径
	char* m_cKmeansPath;  //聚类中心文件所在路径，默认路径为当前路径
	char* m_cResultPath; //分类结果存放路径
	int m_nKmeans;//聚类中心数，默认为100
	int m_roadId;

public:
	
	afx_msg void OnBtnOption();//响应选项按钮
public:
	afx_msg void OnBtnTrack();//响应显示跟踪过程的按钮
public:
	afx_msg void OnBtnShowResult();//响应显示分类结果的按钮
public:
	int GetModelPaths(CString strPath);//从文件夹中获取分类器路径
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
{//跟踪分类线程参数
	HWND hwnd;
	CAdminUIDlg* pDemoDlg;
	//CvCapture* pCapture;
	CString vediopath;
}THREADPARAM;
