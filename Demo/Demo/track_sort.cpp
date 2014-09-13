#include "stdafx.h"
#include "track_sort.h"
#include <time.h>
#include "Vehicle.h"
#include "VehicleQueue.h"

/*局部变量*/
int nNumofTrack = 0;    //当前跟踪车的数量
static int IndexofBlock = 0;   //在团块中的索引
AvTrackBlock TrackBlock[MAXTRACK];
struct svm_model* models[VTYPE*(VTYPE-1)/2];// 分类器
double** kmeansdata = NULL;//聚类中心数据
int nKmeans = 0;//聚类中心数
long int nResult[VTYPE]; //分类结果保存

VehicleQueue* g_pVehicleQueue;

struct svm_model* v_models[2];//判断是否为机动车辆的分类器
double** v_kmeansdata = NULL; //判断是否为机动车辆的聚类中心文件

int nRoadId;
void SetRoadId(int id);
/************************************************************************/
/*							局部函数声明部分						*/
/*用于分类的进程*/
DWORD SortProc( LPVOID lpParameter );

/*将发现的新车辆插入到团块中*/
void InsertNewBlock(int FramesTracked,int avgX,int avgY,CvRect);

/*车辆超出边界从团块中删除*/
void DeleteOldBlock(int id);

/*更新团块中的数据，将已经出跟踪区的团块清除*/
void UpdateBlocks(int nFrame);
/*将枚举类型vehicle转化成字符串类型*/
char *VehicleToString(vehicle vh);

/*将特征点输出到文件中*/
void out_sift_data(struct feature* features,int v,char *sift_file);

/*对于某一辆车所对应的直方图数据利用一个分类器进行预测*/
double track_predict(double ** sift_data,double *hgm,struct svm_model* model,
					 const struct histogram_parameter *para1);

/*对于一辆车利用多个分类器进行分类，最终得到分类结果*/
vehicle sort(double ** siftdescr,struct svm_model **pmodels,int numoftype,
			 double **kmeans_data,const struct histogram_parameter *para1); 

/*从图片中的某个矩形区域得到sift特征点以及特征点数量*/
double **GetSiftArray(IplImage *pImage,int *nNum);

/*判断是否为机动车辆*/
int IsAVehicle(IplImage *pImage,CvRect rect);
/*判断两个矩形是否相交，最终返回相交的面积，不相交返回值为0*/
double CrossRect(CvRect rt1,CvRect rt2);

/* 求矩形rt的面积*/
int AreaofRect(CvRect rt);
/*求两个矩形中心的距离*/
double DistanceOfTwoRects(CvRect rt1,CvRect rt2);
/*判断某个点是否落在了矩形区域中*/
int  IsinRect(CvPoint pt,CvRect rt);

/*求数组arr中最大的下标*/
int MaxIndexofArray(int *arr,int n);
int MaxofArray(int *arr,int n);

/*对于index对应的团块进行分类*/
void sorting(int index);

/*获取车辆类型*/
int GetVehicleType(int* vType,int* type,int numoftype);


void InitBlocks();	//初始化团块
void load_svm_model(char** modlepath,int ntype);//加载所有分类器
void load_kmeans_data(char* kmeans_file);//加载聚类文件
void v_load_svm_model(char* modelpath[2]);//加载用于判断是否为机动车辆的分类器
void v_load_kmeans_data(char* kmeans_file);
/*释放二维数组p*/
void free_array(double **p,int v);
/**********************************************************************************/

/************************************************************************/
/*							局部函数定义部分						*/

int max(int a,int b)
{
	return (a>b?a:b);
}
int min(int a,int b)
{
	return (a>b?b:a);
}
void free_array(double **p,int v)
{
	int i;
	for(i=0;i<v;i++)
		free(p[i]);
	free(p);
	p = NULL;
}
int AreaofRect(CvRect rt)
{
	return (rt.width*rt.height);
}
/*求两个矩形中心的距离*/
double DistanceOfTwoRects(CvRect rt1,CvRect rt2)
{
	int x1,x2,y1,y2;
	x1 = (rt1.x + rt1.x + rt1.width) / 2; 
	y1 = (rt1.y + rt1.y + rt1.height) / 2;

	x2 = (rt2.x + rt2.x + rt2.width) / 2; 
	y2 = (rt2.y + rt2.y + rt2.height) / 2;

	return sqrt((double)((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2)));


}
/*在帧上显示分类结果*/
void show_class_on_image(IplImage *pFrame)
{
	int i;
	CvPoint pt;
	CvFont font;
	cvInitFont(&font, CV_FONT_HERSHEY_SIMPLEX, 0.5, 0.5, 0, 1, 8);
	for(i=0;i<MAXTRACK;i++)
	{		
		if(TrackBlock[i].Direction == 1 && TrackBlock[i].vType != -1)
		{
			pt.x = TrackBlock[i].rect.x + TrackBlock[i].rect.width;
			pt.y = TrackBlock[i].rect.y + TrackBlock[i].rect.height;
			char info[32];
			/*printf(info,"%s(%g,%g)",VehicleToString(TrackBlock[i].vType),
				TrackBlock[i].avgX,TrackBlock[i].avgY);*/
				sprintf(info,"%s (%g,%g)",VehicleToString(TrackBlock[i].vType),
				TrackBlock[i].avgX,TrackBlock[i].avgY);
			pt.x=TrackBlock[i].rect.x;//写字的左下角点
			pt.y=TrackBlock[i].avgY;
			cvPutText(pFrame,info, pt, &font, cvScalar(0,255,0,0));
		}

	}
}

/*初始化分类环境，在进行跟踪分类前对此函数的调用是必须的*/
void init_envir(
				char** modelpaths,
				char* kmeanspath,
				int nkm,
				int roadId,
				VehicleQueue* pVehicleQueue)
{

	int i;
	nKmeans = nkm;
	nRoadId = roadId;
	for(i=0;i<VTYPE;i++)
		nResult[i]=0;
	InitBlocks();//初始化所有团块
	load_svm_model(modelpaths,VTYPE);//加载分类器
	load_kmeans_data(kmeanspath);//加载聚类数据
	g_pVehicleQueue = pVehicleQueue;
}
void SetRoadId(int id)
{//设置道口编号
	nRoadId = id;
}
/*/加载分类器*/
void load_svm_model(char **modlepath,int ntype)
{
	int i=0;
	int nNumofModel = ntype*(ntype-1)/2;
	for(i=0;i<nNumofModel;i++)
	{
		if((models[i]=svm_load_model(modlepath[i]))==0)
		{
			AfxMessageBox("can't open model file");
			exit(0);
		}
	}
}
/*加载聚类中心文件*/
void load_kmeans_data(char *kmeans_file)
{
	struct histogram_parameter hp;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	kmeansdata = h_load_data(kmeans_file,hp);
}
void v_load_svm_model(char* modelpath[2])//加载用于判断是否为机动车辆的分类器
{
	int i;
	for(i=0;i<2;i++)
		if((v_models[i]=svm_load_model(modelpath[i]))==0)
		{
			AfxMessageBox("can't open model file");
			exit(0);
		}

}
void v_load_kmeans_data(char* kmeans_file)
{
	struct histogram_parameter hp;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	v_kmeansdata = h_load_data(kmeans_file,hp);
}
void free_all_para()
{
	int i;
	int nNumofModels = VTYPE*(VTYPE-1)/2;
	free_array(kmeansdata,nKmeans);
	kmeansdata = NULL;
	for(i=0;i<nNumofModels;i++)
		svm_free_and_destroy_model(&models[i]);
	

}
/*判断矩形rt1和rt2是否相交，返回相交的面积，面积为零时表示不相交*/
double CrossRect(CvRect rt1,CvRect rt2)
{
	double crossarea = 0;
	double minx,miny;
	double maxx,maxy;
	minx = max(rt1.x,rt2.x);  
    miny = max(rt1.y,rt2.y);  
    maxx = min(rt1.x+rt1.width,rt2.x+rt2.width);  
    maxy = min(rt1.y+rt1.height,rt2.y+rt2.height); 

	if ( minx>maxx ) return 0;
	if ( miny>maxy ) return 0;

	return (maxx-minx)*(maxy-miny);

}
/*根据多个分类器的分类结果，判断其类型*/
int GetVehicleType(int* vType,int* type,int numoftype)
{
	int t,v;
	int ma = MaxofArray(vType,numoftype);
	int maId = MaxIndexofArray(vType,numoftype);
	if(ma == numoftype-1)
		return maId;
	for(t=numoftype-1;t>maId;t--)
	{
		if(vType[t] == ma && maId != t)
		{
			v = (2*numoftype-3-maId)*maId/2+t-1;
			return type[v];
		}
	}
	return maId;

}

//初始化团块数据
void InitBlocks()
{
	int i;
	for(i=0;i<MAXTRACK;i++)
	{
		TrackBlock[i].avgX = 0;
		TrackBlock[i].avgY = 0;
		TrackBlock[i].Direction = 0;
		TrackBlock[i].FramesTracked = 0;
		TrackBlock[i].vType = (vehicle)-1;
		TrackBlock[i].hThread = NULL;
		TrackBlock[i].rect.x = 0;
		TrackBlock[i].rect.y = 0;
		TrackBlock[i].rect.height = 0;
		TrackBlock[i].rect.width = 0;
		TrackBlock[i].bIsSorting = false;
	}
}
//插入新的需要跟踪的团块
void InsertNewBlock(int FramesTracked,int avgX,int avgY,CvRect rect)
{
	TrackBlock[IndexofBlock].avgX = avgX;
	TrackBlock[IndexofBlock].avgY = avgY;
	TrackBlock[IndexofBlock].Direction = 1;
	TrackBlock[IndexofBlock].FramesTracked = FramesTracked;
	TrackBlock[IndexofBlock].vType = (vehicle)-1;
	TrackBlock[IndexofBlock].rect = rect;
	nNumofTrack++;
}
/*删除团块*/
void DeleteOldBlock(int id)
{
	TrackBlock[id].Direction = 0;
	TrackBlock[id].avgX = 0;
	TrackBlock[id].avgY = 0;
	TrackBlock[id].FramesTracked = 0;	
	TrackBlock[id].vType = (vehicle)-1;
	nNumofTrack--;

}
/*更新团块中的数据，将已经出跟踪区的团块清除*/
void UpdateBlocks(int nFrame)
{
	int j;
	for(j=0;j<MAXTRACK;j++)
	{
		if(TrackBlock[j].FramesTracked != nFrame && TrackBlock[j].vType != -1)
		{
			int temp = (int)TrackBlock[j].vType;
			TrackBlock[j].Direction = 0;
			TrackBlock[j].FramesTracked = 0;
			TrackBlock[j].avgX = 0;
			TrackBlock[j].avgY = 0;
			
			nNumofTrack--;
			nResult[temp]++;

			if((int)TrackBlock[j].vType != -1)
				g_pVehicleQueue->Push(TrackBlock[j].sp.pImage,(int)TrackBlock[j].vType,nRoadId);

			TrackBlock[j].vType = (vehicle)-1;
			cvReleaseImage(&(TrackBlock[j].sp.pImage));
		}
	}
}

//从图片pImage上帝rect区域中提取sift特征点，最终返回特征点的nNum个128维的数据
double **GetSiftArray(IplImage *pImage,int *nNum)
{
	double **siftdescr = NULL;
	IplImage *pGrayImg;
	int nNumofSift = 0;
	struct feature *features;
	try	{
		pGrayImg = cvCreateImage(cvSize(pImage->width,pImage->height),IPL_DEPTH_8U,1);
		cvCvtColor(pImage, pGrayImg, CV_BGR2GRAY);//转化为灰度图
		
		nNumofSift = sift_features(pGrayImg,&features);

		/*cvShowImage("test",pGrayImg);
		cvWaitKey(0);*/
	}
	catch (CException* e)
	{
		AfxMessageBox("出现异常");
		exit(1);
	}	
	
	if(nNumofSift > 1)
	{
		int i,j;
		siftdescr = (double **)malloc(nNumofSift*sizeof(double *));
		for(i=0;i<nNumofSift;i++)
			siftdescr[i] = (double *)malloc(VECT*sizeof(double));
		for(i=0;i<nNumofSift;i++)
			for(j=0;j<VECT;j++)
				siftdescr[i][j] = features[i].descr[j];
		*nNum = nNumofSift;
	}
	
	free(features);
	//cvReleaseImage(&pSubImg);
	cvReleaseImage(&pGrayImg);
	return siftdescr;

	
}
//分类用的线程
DWORD SortProc( LPVOID lpParameter )
{	
	int nNum;
	double **siftdescr = NULL;
	struct histogram_parameter hp;
	SORTPARA *pSortPara = (SORTPARA *)lpParameter;
	int nBlock = pSortPara->nBlock;
	TrackBlock[nBlock].bIsSorting = true;
	/*if(1==IsAVehicle(pSortPara->pImage,pSortPara->rect))
	{
		TrackBlock[nBlock].vType = (vehicle)-1;
		TrackBlock[nBlock].bIsSorting = false;
		return 0;
	}*/
	siftdescr = GetSiftArray(pSortPara->pImage,&nNum);

	hp.datasize = nNum;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	if(NULL != siftdescr)
	{
		TrackBlock[nBlock].vType = sort(siftdescr,models,VTYPE,	kmeansdata,&hp);
		free_array(siftdescr,nNum);
	}
	TrackBlock[nBlock].bIsSorting = false;
	return 0;
}

/*输出sift特征点*/
void out_sift_data(struct feature* features,int v,char *sift_file)
{
	int i,j;
	FILE *pFile;
	pFile=fopen(sift_file,"wb");
	for(i=0;i<v;i++)
		for(j=0;j<VECT;j++)
		{
			if(j==VECT-1)
				fprintf(pFile,"%f\n",(features+i)->descr[j]);
			else
				fprintf(pFile,"%f ",(features+i)->descr[j]);
		}
	fclose(pFile);
}

/**
将枚举类型vehicle转换成字符串类型
**/
char *VehicleToString(vehicle vh)
{
	switch(vh)
	{
	case bus:return "bus";break;
	case coach:return "coach";break;
	case sedan:return "sedan";break;
	case truck:return "truck";break;
	case motor:return "motor";break;
	default:return NULL;
	}

}
/**
求数组arr中最大的下标
**/
int MaxIndexofArray(int *arr,int n)
{
	int i;
	int max=0;
	for(i=1;i<n;i++)
		if(arr[i] > arr[max])
			max=i;
	return max;
}
int MaxofArray(int *arr,int n)
{
	int i;
	int max=arr[0];
	for(i=1;i<n;i++)
		if(arr[i] > max)
			max=arr[i];
	return max;
}
/**
判断点pt是否落在矩形区域rt中，如果在，这返回1；否则，返回0.
**/
int  IsinRect(CvPoint pt,CvRect rt)
{
	if ((pt.x >= rt.x)&&(pt.x <= (rt.x+rt.width))&&
		(pt.y >= rt.y)&&(pt.y <= (rt.y+rt.height))) 
		return 1; 
	else 
		return 0; 

}


/**
利用一个分类器去预测一些sift特征点
@sift_data：sift特征点数据
@hgm：直方图分布数据
@model_file：分类器所在路径
@para1：构造直方图时所需要的参数
@return：返回最终的预测类型
**/
double track_predict(double ** sift_data,double *hgm,struct svm_model* model,
					 const struct histogram_parameter *para1)
{
	int i;
	struct svm_node *x;
	double predict_label;//预测类型
	x=(struct svm_node *)malloc((para1->K+1)*sizeof(struct svm_node));

	for(i=0;i<para1->K;i++)
	{
		x[i].index=i+1;
		x[i].value=hgm[i];
	}
	x[i].index=-1;
	predict_label=svm_predict(model,x);
	free(x);
	return predict_label; 
}

/**
根据siftdescr中的特征点数据，将其归类
@siftdescr：存放sift特征点数据
@modelpath：所有分类器的路径
@numoftype：车辆类型的个数
@kmeanpath：聚类文件所在路径
@para1：构造直方图时需要的参数
@return：返回最后sift特征点数据所对应的车辆类型
**/

vehicle sort(double ** siftdescr,struct svm_model **pmodels,int numoftype,
			 double **kmeans_data,const struct histogram_parameter *para1)
{
	int *vType;
	int *type;
	int i;
	int v;
	vehicle ve;
	double *hgm;

	int numofmodels=numoftype*(numoftype-1)/2;

	vType=(int*)malloc(numoftype*sizeof(int));

	for(i=0;i<numoftype;i++)
		vType[i]=0;
	type = (int*)malloc(numofmodels*sizeof(int));

	hgm=(double *)malloc(para1->K*sizeof(double));

	for(i=0;i<para1->K;i++)
		hgm[i]=0;

	histogram(siftdescr,kmeans_data,hgm,para1);
	//h_out_data(hgm,para1->K,"hgm19.txt");

	for(i=0;i<numofmodels;i++)
	{
		v=(int)track_predict(siftdescr,hgm,pmodels[i],para1);
		type[i] = v;
		vType[v]++;
	}
	for(i=0;i<numoftype;i++)
		printf("%d ",vType[i]);
	ve = (vehicle)GetVehicleType(vType,type,numoftype);
	//ve = (vehicle)MaxIndexofArray(vType,numoftype);
	free(hgm);
	free(vType);
	return ve;
}




/**
跟踪车辆并标出车辆类型

@modelspath：所有用来分类的分类器的路径
@numoftype：分类器的个数
@kmeanspath：聚类文件路径
@pBkImg：差分后的图片，方便用来寻找所有运动物体的轮廓
@pFrame：处理之前的图片，将寻找到的边框画在此图片上
@nFrame：当前帧号
@return：
**/



/*判断是否为机动车辆*/
int IsAVehicle(IplImage *pImage,CvRect rect)
{
	int nNum;
	double **siftdescr = NULL;
	struct histogram_parameter hp;
	int vType;
	siftdescr = GetSiftArray(pImage,&nNum);

	hp.datasize = nNum;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	if(NULL != siftdescr)
	{
		vType = (int)sort(siftdescr,v_models,2,v_kmeansdata,&hp);
		free_array(siftdescr,nNum);
	}
	return vType;
}

void tracking(IplImage *pBkImg, IplImage *pFrame,int nFrame,
			  BOOL isShowPro,CvPoint* pBegin,CvPoint* pEnd)             
{
	IplImage *cloneImg1 = 0;//为了防止对改变前景图，将
	CvSeq * contour = 0;//轮廓边缘提取时的参数
	CvPoint pt3,pt4;
	int mode = CV_RETR_EXTERNAL;//轮廓边缘提取时的参数
	int i,j;
	int FindCar = 0;
	int avgX = 0;//移动物体中心的X方向坐标中心
	int avgY = 0;//移动物体中心的Y方向坐标中心
	CvRect bndRect = cvRect(0,0,0,0);//用cvBoundingRect画出外接矩形时需要的矩形
	CvMemStorage * storage = cvCreateMemStorage(0);//轮廓边缘提取时的参数

	if(nFrame >=2)
	{
		cloneImg1 = cvCreateImage(cvSize(pBkImg->width,pBkImg->height),
			pBkImg->depth,pBkImg->nChannels);
		cvCopy(pBkImg,cloneImg1,NULL);
	/*	
		cvNamedWindow("song",CV_WINDOW_AUTOSIZE);
		cvShowImage("song",cloneImg1);
		cvWaitKey(0);*/

		cvFindContours(cloneImg1, storage, &contour, sizeof(CvContour),
			mode, CV_CHAIN_APPROX_SIMPLE,cvPoint(0,0));
		for(;contour!=0;contour=contour->h_next)
		{
			//得到一个可以将移动轮廓包围的矩形区域
			bndRect = cvBoundingRect(contour, 0);

			//获取移动轮廓中心的坐标
			avgX = (bndRect.x + bndRect.x + bndRect.width) / 2; 
			avgY = (bndRect.y + bndRect.y + bndRect.height) / 2;


			//只处理出现在感兴趣区域中的车辆
			if(avgY > pBegin->y && avgY < pEnd->y && avgX < pEnd->x && avgX>pBegin->x)
			{
				double ration;//长和宽之比				
				ration = (double)(bndRect.width)/bndRect.height;
				double area = bndRect.width*bndRect.height;
				pt3.x = bndRect.x;
				pt3.y = bndRect.y;
				pt4.x = bndRect.x + bndRect.width;
				pt4.y = bndRect.y + bndRect.height;

				if(bndRect.height > 100 && bndRect.width > 100  && AreaofRect(bndRect) > CONTOUR_MIN_AERA
					&& ration > 0.6)
				{
					if(isShowPro)
						cvRectangle(pFrame,pt3,pt4,CV_RGB(255,0,0),1, 8, 0 );
					//在跟踪数组中寻找看是否有匹配的车辆，没有则表示是新车辆
					for(i=0;i<MAXTRACK;i++)
					{
						//在此处进行跟踪处理
						double crossRect = CrossRect(TrackBlock[i].rect,bndRect);
						if(TrackBlock[i].Direction == 1 &&  crossRect> 120
							&& TrackBlock[i].FramesTracked < nFrame /*&& abs(avgX-TrackBlock[i].avgX) < 40/*&& abs(AreaofRect(TrackBlock[i].rect)-AreaofRect(bndRect))<100*/)
						{

							TrackBlock[i].FramesTracked=nFrame;
							TrackBlock[i].avgX = avgX;
							TrackBlock[i].avgY = avgY;
							//if(AreaofRect(TrackBlock[i].rect)-AreaofRect(bndRect) <= 100)
							TrackBlock[i].rect = bndRect;
							i = MAXTRACK;//使跳出for循环
							FindCar = 1;
						}
					}

					if(FindCar != 1)//表示找到新车辆
					{					
						//修改线程参数	
						TrackBlock[IndexofBlock].sp.pImage = cvCreateImage(cvSize(bndRect.width,bndRect.height),
							pFrame->depth,pFrame->nChannels);
						cvSetImageROI(pFrame,bndRect);
						cvCopy(pFrame,TrackBlock[IndexofBlock].sp.pImage,NULL);
						cvResetImageROI(pFrame);

						
						TrackBlock[IndexofBlock].sp.nBlock = IndexofBlock;		
						TrackBlock[IndexofBlock].sp.rect = bndRect;

						//将车辆放到团块中以方便跟踪
						InsertNewBlock(nFrame,avgX,avgY,bndRect);

						//sorting(IndexofBlock);

						if(IndexofBlock == MAXTRACK-1)
							IndexofBlock = 0;
						else
							IndexofBlock++;
					}	
					//赋值为0为下一次寻找车辆做准备
					FindCar=0;
				}
			}
		}
	}

		//对于没有匹配的车辆，表示已经出了边界，清空对应的团块		
	UpdateBlocks(nFrame);

	//在视频中设置并画出感兴趣的区域
	cvRectangle(pFrame,*pBegin,*pEnd,CV_RGB(255,0,0),2, 8, 0 );
	/*释放资源*/
	cvReleaseMemStorage(&storage);
	cvReleaseImage(&cloneImg1);
}

/*只是分类*/
void sorting()
{
	int i;
	for(i=0;i<MAXTRACK;i++)
		if(TrackBlock[i].Direction == 1 && TrackBlock[i].bIsSorting == false 
			&& TrackBlock[i].vType == -1)
		{
			TrackBlock[i].hThread = CreateThread(NULL,0,(LPTHREAD_START_ROUTINE)SortProc,
											(LPVOID)&(TrackBlock[i].sp),0,NULL);
			CloseHandle(TrackBlock[i].hThread);
		}
}
/*对于第index个团块进行分类*/
void sorting(int index)
{
	if(TrackBlock[index].Direction == 1 && TrackBlock[index].bIsSorting == false 
		&& TrackBlock[index].vType == -1)
	{
		TrackBlock[index].hThread = CreateThread(NULL,0,(LPTHREAD_START_ROUTINE)SortProc,
			(LPVOID)&(TrackBlock[index].sp),0,NULL);
		CloseHandle(TrackBlock[index].hThread);
	}
}
/*输出结果*/
char* out_result_to_file(char* pOutPath,char* pBegTime)
{
	FILE* file;
	int i;
	char* pRes = (char*)malloc(1024*sizeof(char));
	time_t rawtime;
	struct tm * timeinfo;

	time ( &rawtime );
	timeinfo = localtime ( &rawtime );
	sprintf(pRes,"starting time: %s%s: %ld\n%s: %ld\n%s: %ld\n%s: %ld\nfinish time: %s\n%s",
		pBegTime,VehicleToString((vehicle)0),nResult[0],VehicleToString((vehicle)1),nResult[1],
		VehicleToString((vehicle)2),nResult[2],VehicleToString((vehicle)3),nResult[3],
		asctime (timeinfo),"保存成功!");
	file = fopen(pOutPath,"ab");
	fprintf(file,"starting time: %s",pBegTime);
	for(i=0;i<VTYPE;i++)
	{
		fprintf(file,"%s: %ld\n",VehicleToString((vehicle)i),nResult[i]);
	}
	fprintf(file,"finish time: %s\n", asctime (timeinfo));
	fclose(file);
	return pRes;
}
/*清空记录*/
void clear_record()
{
	int i;
	for(i=0;i<VTYPE;i++)
		nResult[i] = 0;
}

/*************************************************************************/
/***************************分类测试主要函数******************************/
double **T_GetSiftArray(IplImage *pImage,int *nNum);
vehicle T_Sort(IplImage* pImage,struct svm_model **pmodels,
			   int numoftype,double **kmeans_data);
IplImage* T_GetImage(char* imgPath);
void T_ToShowImage(IplImage* pImg,vehicle ve);
/****************************************************************************/
void T_ToShowImage(IplImage* pImg,vehicle ve)
{
	CvFont font;
	CvPoint point;
	point.x = pImg->width/2;
	point.y = pImg->height/2;
	cvNamedWindow("song",CV_WINDOW_AUTOSIZE);

	cvInitFont(&font, CV_FONT_HERSHEY_SIMPLEX, 0.5, 0.5, 0, 1, 8);
	cvPutText(pImg,VehicleToString(ve), point, &font, cvScalar(0,255,0,0));

	cvShowImage("song",pImg);
	cvWaitKey(0);
	cvDestroyWindow("song");
	
}
IplImage* T_GetImage(char* imgPath)
{
	IplImage* pImg;
	if((pImg = cvLoadImage(imgPath,1)) == 0){
		fprintf(stderr,"Load imgae error!");
		exit(1);
	}
	return pImg;
}
double **T_GetSiftArray(IplImage *pImage,int *nNum)
{
	double **siftdescr = NULL;
	IplImage *pGrayImg;
	int nNumofSift = 0;
	struct feature *features;

	pGrayImg = cvCreateImage(cvSize(pImage->width,pImage->height),IPL_DEPTH_8U,1);
	cvCvtColor(pImage, pGrayImg, CV_BGR2GRAY);//转化为灰度图

	

	nNumofSift = sift_features(pGrayImg,&features);
	printf("%d",nNumofSift);
	out_sift_data(features,nNumofSift,"sift19.txt");

	if(nNumofSift > 1)
	{
		int i,j;
		siftdescr = (double **)malloc(nNumofSift*sizeof(double *));
		for(i=0;i<nNumofSift;i++)
			siftdescr[i] = (double *)malloc(VECT*sizeof(double));
		for(i=0;i<nNumofSift;i++)
			for(j=0;j<VECT;j++)
				siftdescr[i][j] = features[i].descr[j];
		*nNum = nNumofSift;
	}

	free(features);
	cvReleaseImage(&pGrayImg);
	return siftdescr;

}
vehicle T_Sort(IplImage* pImage,struct svm_model **pmodels,
					int numoftype,double **kmeans_data)
{

	int nNum;
	vehicle vType = (vehicle)-1;
	double **siftdescr = NULL;
	struct histogram_parameter hp;
	
	siftdescr = T_GetSiftArray(pImage,&nNum);

	hp.datasize = nNum;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	if(NULL != siftdescr)
	{
		vType = sort(siftdescr,pmodels,VTYPE,kmeansdata,&hp);
		free_array(siftdescr,nNum);
	}
	return vType;
}
//int main(/*int argc,char** argv*/)
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
//	IplImage* pImage;//需要分类的图片
//	vehicle vType;//类型
//
//	/*if(argc<2)
//	{
//		printf("Error!");
//		exit(1);
//	}*/
//	/*加载分类器和聚类文件*/
//	load_svm_model(modelspath,VTYPE);
//	load_kmeans_data(kmeanspath);
//
//	/*加载图片后，进行分类，然后显示*/
//	pImage = T_GetImage("7.jpg");
//	vType = T_Sort(pImage,models,VTYPE,kmeansdata);
//	T_ToShowImage(pImage,vType);
//
//	/*释放资源*/
//	cvReleaseImage(&pImage);
//	free_all_para();
//
//	return 0;
//}
