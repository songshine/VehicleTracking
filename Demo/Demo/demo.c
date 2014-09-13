#include "allheaders.h"
#include "track_sort.h"
/************************************************************************/
/*演示样本*/
IplImage* GetBackgroud(char *vpath,int nFrame);
int  project_demo_1(char *vediopath,char **modelspath,char*kmeanspath);//相邻帧差法
int  project_demo_2(char *vediopath,char **modelspath,char*kmeanspath);//背景帧差法
/************************************************************************/

IplImage* GetBackgroud(char *vpath,int nFrame)
{
	IplImage* pFrame = NULL;
	IplImage* pBkImg = NULL;
	CvMat* pBkMat = NULL;
	CvMat* pFrmMat = NULL;
	CvCapture* pCapture = NULL;
	int nFrmNum = 0;
	double beta = (double)1.0/nFrame;
	pCapture = cvCreateFileCapture(vpath);//读取视频文件

	while(pFrame = cvQueryFrame( pCapture ))
	{
		nFrmNum++;
		if(nFrmNum == 1)
		{
			pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrmMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);//转化为线性数组，即矩阵
		}
		if(nFrmNum == nFrame)
			break;
		cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
		cvConvert(pBkImg, pFrmMat);//转化为线性数组，即矩阵
		cvAddWeighted(pBkMat,1,pFrmMat,beta,0,pBkMat);
	}
	cvConvert(pBkMat, pBkImg);
	cvReleaseMat(&pBkMat);
	cvReleaseMat(&pFrmMat);
	cvReleaseCapture(&pCapture);
	return pBkImg;


}
int  project_demo_1(char *vediopath,char **modelspath,char*kmeanspath)
{
	/*以下为数据变量初始化*/
	struct feature* features;//用于存放特征点信息
	int n = 0;               //用于存放特征点个数    
	IplImage* pFrame = NULL; //视频帧
	IplImage* pFImg = NULL;  //处理后图像
	IplImage* FFrame = NULL; //第一帧
	IplImage* SFrame = NULL; //第二帧
	IplImage* FImage = NULL; //第一帧灰度图
	IplImage* SImage = NULL;//第二帧灰度图
	CvMat* BWimage = NULL;//差分后二值图
	CvMat* DBWimage = NULL;//形态学操作后图像

	CvMat* pFrameMat = NULL;//帧矩阵
	CvMat* FFrameMat = NULL;//第一帧矩阵
	CvMat* SFrameMat = NULL;//第二帧矩阵
	CvMat* FDiffMat = NULL;//第二，第一帧差分矩阵
	//CvMat* TempMat = NULL; //临时矩阵
	CvCapture* pCapture = NULL;
	int nFrmNum = 0;
	// int FrmNum=0;
	int i=0;
	int j=0;//循环控制变量

	////用于保存结果的视频写入
	//CvVideoWriter *writer = 0;
	//int isColor = 1;
	//int fps     = 4;  // FPS
	//int frameW  = 0; // 高度
	//int frameH  = 0; // 宽度
	//int nRes = 0;
	//创建窗口
	cvNamedWindow("video", CV_WINDOW_AUTOSIZE );//视频窗口
	cvNamedWindow("change",CV_WINDOW_AUTOSIZE );//背景窗口
	//cvNamedWindow("song",CV_WINDOW_AUTOSIZE);//测试窗口

	//使窗口有序排列

	cvMoveWindow("video", 380, 50);
	cvMoveWindow("change", 740, 50);
	InitBlocks();//初始化所有团块
	load_svm_model(modelspath,VTYPE);//加载分类器
	load_kmeans_data(kmeanspath);//加载聚类数据
	/*以下为视频处理与显示*/

	pCapture = cvCreateFileCapture(vediopath);//读取视频文件

	//frameH  = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FRAME_HEIGHT);
	//frameW  = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FRAME_WIDTH);
	////fps     = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FPS);
	//writer=cvCreateVideoWriter("out4.avi",CV_FOURCC('X','V','I','D'),
	//                         fps,cvSize(frameW,frameH),isColor);

	//printf("fps:%d frameH:%d frameW:%d\n",fps,frameH,frameW);
	//逐帧读取视频
	while(pFrame = cvQueryFrame( pCapture ))//视频流，第次完成读取后指向下一帧，并返回当前读取的帧
	{
		nFrmNum++;
		//如果是第一帧，需要申请内存，并初始化
		if(nFrmNum == 2)
		{
			//InitBlocks();//初始化所有团块

			FFrame = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);//初始化帧矩阵以前图像帧
			pFImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			FImage = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			SFrame = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);//初始化第二帧
			SImage = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			FFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			BWimage = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			DBWimage = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			FFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			SFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			FDiffMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			//转化成单通道图像再处理
			//转化为灰值图像
			cvCvtColor(pFrame, FImage, CV_BGR2GRAY);
			cvConvert(FImage, FFrameMat);//转化为线性数组，即矩阵
			//此处进行图像增强操作............
			//cvSmooth(SFrameMat,SFrameMat, CV_MEDIAN,3,0,0,0);//中值滤波
			//FFrameMat = cvCloneMat(TempMat);//复制矩阵
			//cvWriteFrame(writer,pFrame);
		}
		else if(nFrmNum > 2)
		{
			if(nFrmNum%2 == 0 )
		 {
			 cvShowImage("video", pFrame);
			 cvShowImage("change", pFImg);
			 continue;

			}		 


			cvCvtColor(pFrame, SImage, CV_BGR2GRAY);//转化为灰度图
			cvConvert(SImage, SFrameMat);//转化为矩阵
			//此处加入相关图像增强操作
			cvSmooth(SFrameMat,SFrameMat, CV_MEDIAN,3,0,0,0);//中值滤波
			//差分
			cvAbsDiff(SFrameMat,FFrameMat,FDiffMat);

			//二值化差分图
			cvThreshold(FDiffMat,BWimage,60,255,CV_THRESH_BINARY);
			//更新差分帧

			//FFrameMat = cvCloneMat(SFrameMat);
			cvCopy(SFrameMat,FFrameMat,NULL);
			//对二值图像进行膨胀
			cvDilate(BWimage,DBWimage,NULL,5);//进行6次3*3正方形的形态学膨胀

			for(i=0;i<DBWimage->rows;i++)
				for(j=0;j<DBWimage->cols;j++)
				{
					if(cvmGet(DBWimage,i,j)==0)
						cvmSet(SFrameMat,i,j,0);
				}
				//转换为图像
				//cvConvert(SFrameMat, pFImg);

				cvConvert(DBWimage, pFImg);

				/*跟踪车辆并分类*/
				trackandsort(modelspath,VTYPE,kmeanspath,pFImg,pFrame,nFrmNum);

				/*以下是处理效果展示部分*/
				//绘制SIFT特征点

				//draw_features( pFrame, features, n );

				//写入结果视频
				//nRes = cvWriteFrame(writer,pFrame); 
				//显示图像
				cvShowImage("video", pFrame);
				cvShowImage("change", pFImg);
				//	cvShowImage("song",pSongImg);

				//等待时间可以根据CPU速度调整
				if( cvWaitKey(2) >= 0 )
					break;


		}

	}


	free_all_para();
	//销毁窗口
	cvDestroyWindow("video");
	cvDestroyWindow("change");
	//IplConvKernel* StrElem = NULL;//形态学算子
	//释放图像和矩阵

	cvReleaseImage(&pFImg);
	cvReleaseImage(&FFrame);
	cvReleaseImage(&SFrame);
	cvReleaseImage(&FImage);
	cvReleaseImage(&SImage);
	// cvReleaseImage(&pFrame);

	cvReleaseMat(&pFrameMat);
	cvReleaseMat(&BWimage);
	cvReleaseMat(&DBWimage);
	cvReleaseMat(&FFrameMat);
	cvReleaseMat(&SFrameMat);
	cvReleaseMat(&FDiffMat);
	cvReleaseCapture(&pCapture);
	//释放存储空间


	//释放录制指针
	//cvReleaseVideoWriter(&writer); 

	return 0;
}
int  project_demo_2(char *vediopath,char **modelspath,char*kmeanspath)
{
	/*以下为数据变量初始化*/
	//struct feature* features;//用于存放特征点信息
	int n = 0;               //用于存放特征点个数    
	IplImage* pFrame = NULL;
	IplImage* pFrImg = NULL;
	IplImage* pBkImg = NULL;


	CvMat* pFrameMat = NULL;
	CvMat* pFrMat = NULL;
	CvMat* pBkMat = NULL;

	CvCapture* pCapture = NULL;
	int nFrmNum = 0;

	//形态学处理时内核的大小
	IplConvKernel* Element = cvCreateStructuringElementEx(13,13,1,1,CV_SHAPE_RECT,NULL);
	//创建窗口
	cvNamedWindow("video", CV_WINDOW_AUTOSIZE );//视频窗口
	cvNamedWindow("change",CV_WINDOW_AUTOSIZE );//背景窗口
	cvNamedWindow("song",CV_WINDOW_AUTOSIZE);//测试窗口

	//使窗口有序排列

	cvMoveWindow("video", 380, 50);
	cvMoveWindow("change", 740, 50);
	cvMoveWindow("song",380,300);

	InitBlocks();//初始化所有团块
	load_svm_model(modelspath,VTYPE);
	load_kmeans_data(kmeanspath);

	/*以下为视频处理与显示*/
	pCapture = cvCreateFileCapture(vediopath);//读取视频文件

	//逐帧读取视频
	while(pFrame = cvQueryFrame( pCapture ))//视频流，第次完成读取后指向下一帧，并返回当前读取的帧
	{
		nFrmNum++;
		//如果是第一帧，需要申请内存，并初始化
		if(nFrmNum == 2)
		{
			//InitBlocks();
			pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			pFrImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);

			pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
			cvCvtColor(pFrame, pFrImg, CV_BGR2GRAY);
			cvConvert(pFrImg, pFrameMat);
			cvConvert(pFrImg, pFrMat);
			cvConvert(pBkImg, pBkMat);
		}
		else if(nFrmNum > 2)
		{
			if(nFrmNum%2 == 0 )
		 {
			 cvShowImage("video", pFrame);
			 cvShowImage("song",pBkImg);
			 continue;

			}


			cvCvtColor(pFrame, pFrImg, CV_BGR2GRAY);
			cvConvert(pFrImg, pFrameMat);
			//高斯滤波先，以平滑图像
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN,3,0,0,0);


			//当前帧跟背景图相减,cvAbsDiff计算两个数组差的绝对值
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);

			//二值化前景图
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			//通过查找边界找出ROI矩形区域内的运动车辆，建立完全目标档案
			//cvCanny(pFrImg, pBkImg, 50, 150, 3);
			cvDilate(pFrImg,pFrImg,NULL,1);//膨胀
			/*跟踪车辆并分类*/
			trackandsort(modelspath,VTYPE,kmeanspath,pFrImg,pFrame,nFrmNum);
			
			/*更新背景*/
			cvRunningAvg(pFrameMat, pBkMat, 0.005, 0);
			//将背景转化为图像格式，用以显示
			cvConvert(pBkMat, pBkImg);
			/*以下是处理效果展示部分*/
			//绘制SIFT特征点

			//draw_features( pFrame, features, n );
			cvShowImage("video", pFrame);
			cvShowImage("change", pFrImg);
			cvShowImage("song",pBkImg);

			//等待时间可以根据CPU速度调整
			if( cvWaitKey(2) >= 0 )
				break;


		}

	}
	free_all_para();
	//销毁窗口
	cvDestroyWindow("video");
	cvDestroyWindow("change");
	cvDestroyWindow("song");

	//删除结构元素
	cvReleaseStructuringElement(&Element);


	//释放图像和矩阵
	cvReleaseImage(&pFrImg);
	cvReleaseImage(&pBkImg);

	cvReleaseMat(&pFrameMat);
	cvReleaseMat(&pFrMat);
	cvReleaseMat(&pBkMat);
	cvReleaseCapture(&pCapture);
	return 0;
}
//int main()
//{
//	char *modelspath[VTYPE*(VTYPE-1)/2] = {	"train01",
//											"train02",
//											"train03",
//											"train12",
//											"train13",
//											"train23"};//分类器所在路径
//
//	char *kmeanspath = "kmeans.txt";//聚类中心路径
//
//	char *vediopath = "test9.xvid";//监控视频路径
//
//	project_demo_1(vediopath,modelspath,kmeanspath);
//
//	return 0;
//}