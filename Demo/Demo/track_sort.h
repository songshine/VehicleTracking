#ifndef _TRACK_DEMO_H_
#define _TRACK_DEMO_H_

#include <cxcore.h>
#include "histogram.h"
#include "VehicleQueue.h"

#define CONTOUR_MIN_AERA 10000  //跟踪的最小区域
#define MAXTRACK 32          //最多同时跟踪数量
//枚举车辆类型

typedef enum vehicle_type{bus,coach,sedan,truck,motor}vehicle;

//分类线程的参数结构体
typedef struct SORTPROCPARA
{
	CvRect rect;	//说要获取的区域
	IplImage *pImage;	//图像
	int nBlock;	//团块序号
}SORTPARA;


typedef struct VehicleTrackBlock{
	int Direction;      //1表示进入区域，0表示离开区域
	int FramesTracked;  //已经跟踪到车辆的帧数，用于计算车辆的车速。
	CvRect rect;   //即建立跟踪车辆对象时,跟踪车辆确定的跟踪区域的位置
	//CvRect NowArea;      //即建立跟踪车辆对象时,跟踪车辆当前的跟踪区域的面积。
	double avgX;           //车辆x方向中心
	double avgY;           //车辆y方向中心
	//int AvSpeedX;      //跟踪车辆目前在图像水平位置即垂直于车道方向上的平均速度(单位:像素/秒)。
	//int AvSpeedY;      //跟踪车辆目前在图像垂直位置即平行于车道方向上的平均速度(单位:像素/秒
	vehicle vType;     //车辆类型,为空表示还没有分类
	SORTPARA sp;		//该团块对应的线程参数
	HANDLE hThread;		//该团块对应的线程句柄
	bool bIsSorting;    //判断是否该团块正在处于分类线程中
}AvTrackBlock;





/*isShowPro是否显示跟踪过程
pBegin感兴趣区域左上角坐标
pEnd感兴趣区域右下角坐标*/
extern void tracking(IplImage *pBkImg, IplImage *pFrame,int nFrame,
					 BOOL isShowPro,CvPoint* pBegin,CvPoint* pEnd);//only tracking
extern void sorting();//对进入感兴趣区域的车辆进行分类
extern void show_class_on_image(IplImage *pFrame);//在图像上显示分类结果
extern void init_envir(
					   char** modelpaths,
					   char* kmeanspath,
					   int nkm,
					   int roadId,
					   VehicleQueue* pVehicleQueue);//初始化环境
extern char* out_result_to_file(char* pOutPath,char* pBegTime=NULL);//输出结果
extern void clear_record();//清空记录
extern void free_all_para();//释放资源

#endif