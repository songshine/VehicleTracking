#ifndef _TRACK_DEMO_H_
#define _TRACK_DEMO_H_

#include <cxcore.h>
#include "histogram.h"
#include "VehicleQueue.h"

#define CONTOUR_MIN_AERA 10000  //���ٵ���С����
#define MAXTRACK 32          //���ͬʱ��������
//ö�ٳ�������

typedef enum vehicle_type{bus,coach,sedan,truck,motor}vehicle;

//�����̵߳Ĳ����ṹ��
typedef struct SORTPROCPARA
{
	CvRect rect;	//˵Ҫ��ȡ������
	IplImage *pImage;	//ͼ��
	int nBlock;	//�ſ����
}SORTPARA;


typedef struct VehicleTrackBlock{
	int Direction;      //1��ʾ��������0��ʾ�뿪����
	int FramesTracked;  //�Ѿ����ٵ�������֡�������ڼ��㳵���ĳ��١�
	CvRect rect;   //���������ٳ�������ʱ,���ٳ���ȷ���ĸ��������λ��
	//CvRect NowArea;      //���������ٳ�������ʱ,���ٳ�����ǰ�ĸ�������������
	double avgX;           //����x��������
	double avgY;           //����y��������
	//int AvSpeedX;      //���ٳ���Ŀǰ��ͼ��ˮƽλ�ü���ֱ�ڳ��������ϵ�ƽ���ٶ�(��λ:����/��)��
	//int AvSpeedY;      //���ٳ���Ŀǰ��ͼ��ֱλ�ü�ƽ���ڳ��������ϵ�ƽ���ٶ�(��λ:����/��
	vehicle vType;     //��������,Ϊ�ձ�ʾ��û�з���
	SORTPARA sp;		//���ſ��Ӧ���̲߳���
	HANDLE hThread;		//���ſ��Ӧ���߳̾��
	bool bIsSorting;    //�ж��Ƿ���ſ����ڴ��ڷ����߳���
}AvTrackBlock;





/*isShowPro�Ƿ���ʾ���ٹ���
pBegin����Ȥ�������Ͻ�����
pEnd����Ȥ�������½�����*/
extern void tracking(IplImage *pBkImg, IplImage *pFrame,int nFrame,
					 BOOL isShowPro,CvPoint* pBegin,CvPoint* pEnd);//only tracking
extern void sorting();//�Խ������Ȥ����ĳ������з���
extern void show_class_on_image(IplImage *pFrame);//��ͼ������ʾ������
extern void init_envir(
					   char** modelpaths,
					   char* kmeanspath,
					   int nkm,
					   int roadId,
					   VehicleQueue* pVehicleQueue);//��ʼ������
extern char* out_result_to_file(char* pOutPath,char* pBegTime=NULL);//������
extern void clear_record();//��ռ�¼
extern void free_all_para();//�ͷ���Դ

#endif