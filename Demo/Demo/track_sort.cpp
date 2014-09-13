#include "stdafx.h"
#include "track_sort.h"
#include <time.h>
#include "Vehicle.h"
#include "VehicleQueue.h"

/*�ֲ�����*/
int nNumofTrack = 0;    //��ǰ���ٳ�������
static int IndexofBlock = 0;   //���ſ��е�����
AvTrackBlock TrackBlock[MAXTRACK];
struct svm_model* models[VTYPE*(VTYPE-1)/2];// ������
double** kmeansdata = NULL;//������������
int nKmeans = 0;//����������
long int nResult[VTYPE]; //����������

VehicleQueue* g_pVehicleQueue;

struct svm_model* v_models[2];//�ж��Ƿ�Ϊ���������ķ�����
double** v_kmeansdata = NULL; //�ж��Ƿ�Ϊ���������ľ��������ļ�

int nRoadId;
void SetRoadId(int id);
/************************************************************************/
/*							�ֲ�������������						*/
/*���ڷ���Ľ���*/
DWORD SortProc( LPVOID lpParameter );

/*�����ֵ��³������뵽�ſ���*/
void InsertNewBlock(int FramesTracked,int avgX,int avgY,CvRect);

/*���������߽���ſ���ɾ��*/
void DeleteOldBlock(int id);

/*�����ſ��е����ݣ����Ѿ������������ſ����*/
void UpdateBlocks(int nFrame);
/*��ö������vehicleת�����ַ�������*/
char *VehicleToString(vehicle vh);

/*��������������ļ���*/
void out_sift_data(struct feature* features,int v,char *sift_file);

/*����ĳһ��������Ӧ��ֱ��ͼ��������һ������������Ԥ��*/
double track_predict(double ** sift_data,double *hgm,struct svm_model* model,
					 const struct histogram_parameter *para1);

/*����һ�������ö�����������з��࣬���յõ�������*/
vehicle sort(double ** siftdescr,struct svm_model **pmodels,int numoftype,
			 double **kmeans_data,const struct histogram_parameter *para1); 

/*��ͼƬ�е�ĳ����������õ�sift�������Լ�����������*/
double **GetSiftArray(IplImage *pImage,int *nNum);

/*�ж��Ƿ�Ϊ��������*/
int IsAVehicle(IplImage *pImage,CvRect rect);
/*�ж����������Ƿ��ཻ�����շ����ཻ����������ཻ����ֵΪ0*/
double CrossRect(CvRect rt1,CvRect rt2);

/* �����rt�����*/
int AreaofRect(CvRect rt);
/*�������������ĵľ���*/
double DistanceOfTwoRects(CvRect rt1,CvRect rt2);
/*�ж�ĳ�����Ƿ������˾���������*/
int  IsinRect(CvPoint pt,CvRect rt);

/*������arr�������±�*/
int MaxIndexofArray(int *arr,int n);
int MaxofArray(int *arr,int n);

/*����index��Ӧ���ſ���з���*/
void sorting(int index);

/*��ȡ��������*/
int GetVehicleType(int* vType,int* type,int numoftype);


void InitBlocks();	//��ʼ���ſ�
void load_svm_model(char** modlepath,int ntype);//�������з�����
void load_kmeans_data(char* kmeans_file);//���ؾ����ļ�
void v_load_svm_model(char* modelpath[2]);//���������ж��Ƿ�Ϊ���������ķ�����
void v_load_kmeans_data(char* kmeans_file);
/*�ͷŶ�ά����p*/
void free_array(double **p,int v);
/**********************************************************************************/

/************************************************************************/
/*							�ֲ��������岿��						*/

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
/*�������������ĵľ���*/
double DistanceOfTwoRects(CvRect rt1,CvRect rt2)
{
	int x1,x2,y1,y2;
	x1 = (rt1.x + rt1.x + rt1.width) / 2; 
	y1 = (rt1.y + rt1.y + rt1.height) / 2;

	x2 = (rt2.x + rt2.x + rt2.width) / 2; 
	y2 = (rt2.y + rt2.y + rt2.height) / 2;

	return sqrt((double)((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2)));


}
/*��֡����ʾ������*/
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
			pt.x=TrackBlock[i].rect.x;//д�ֵ����½ǵ�
			pt.y=TrackBlock[i].avgY;
			cvPutText(pFrame,info, pt, &font, cvScalar(0,255,0,0));
		}

	}
}

/*��ʼ�����໷�����ڽ��и��ٷ���ǰ�Դ˺����ĵ����Ǳ����*/
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
	InitBlocks();//��ʼ�������ſ�
	load_svm_model(modelpaths,VTYPE);//���ط�����
	load_kmeans_data(kmeanspath);//���ؾ�������
	g_pVehicleQueue = pVehicleQueue;
}
void SetRoadId(int id)
{//���õ��ڱ��
	nRoadId = id;
}
/*/���ط�����*/
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
/*���ؾ��������ļ�*/
void load_kmeans_data(char *kmeans_file)
{
	struct histogram_parameter hp;
	hp.K = nKmeans;
	hp.Vectordim = VECT;
	kmeansdata = h_load_data(kmeans_file,hp);
}
void v_load_svm_model(char* modelpath[2])//���������ж��Ƿ�Ϊ���������ķ�����
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
/*�жϾ���rt1��rt2�Ƿ��ཻ�������ཻ����������Ϊ��ʱ��ʾ���ཻ*/
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
/*���ݶ���������ķ��������ж�������*/
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

//��ʼ���ſ�����
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
//�����µ���Ҫ���ٵ��ſ�
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
/*ɾ���ſ�*/
void DeleteOldBlock(int id)
{
	TrackBlock[id].Direction = 0;
	TrackBlock[id].avgX = 0;
	TrackBlock[id].avgY = 0;
	TrackBlock[id].FramesTracked = 0;	
	TrackBlock[id].vType = (vehicle)-1;
	nNumofTrack--;

}
/*�����ſ��е����ݣ����Ѿ������������ſ����*/
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

//��ͼƬpImage�ϵ�rect��������ȡsift�����㣬���շ����������nNum��128ά������
double **GetSiftArray(IplImage *pImage,int *nNum)
{
	double **siftdescr = NULL;
	IplImage *pGrayImg;
	int nNumofSift = 0;
	struct feature *features;
	try	{
		pGrayImg = cvCreateImage(cvSize(pImage->width,pImage->height),IPL_DEPTH_8U,1);
		cvCvtColor(pImage, pGrayImg, CV_BGR2GRAY);//ת��Ϊ�Ҷ�ͼ
		
		nNumofSift = sift_features(pGrayImg,&features);

		/*cvShowImage("test",pGrayImg);
		cvWaitKey(0);*/
	}
	catch (CException* e)
	{
		AfxMessageBox("�����쳣");
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
//�����õ��߳�
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

/*���sift������*/
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
��ö������vehicleת�����ַ�������
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
������arr�������±�
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
�жϵ�pt�Ƿ����ھ�������rt�У�����ڣ��ⷵ��1�����򣬷���0.
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
����һ��������ȥԤ��һЩsift������
@sift_data��sift����������
@hgm��ֱ��ͼ�ֲ�����
@model_file������������·��
@para1������ֱ��ͼʱ����Ҫ�Ĳ���
@return���������յ�Ԥ������
**/
double track_predict(double ** sift_data,double *hgm,struct svm_model* model,
					 const struct histogram_parameter *para1)
{
	int i;
	struct svm_node *x;
	double predict_label;//Ԥ������
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
����siftdescr�е����������ݣ��������
@siftdescr�����sift����������
@modelpath�����з�������·��
@numoftype���������͵ĸ���
@kmeanpath�������ļ�����·��
@para1������ֱ��ͼʱ��Ҫ�Ĳ���
@return���������sift��������������Ӧ�ĳ�������
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
���ٳ����������������

@modelspath��������������ķ�������·��
@numoftype���������ĸ���
@kmeanspath�������ļ�·��
@pBkImg����ֺ��ͼƬ����������Ѱ�������˶����������
@pFrame������֮ǰ��ͼƬ����Ѱ�ҵ��ı߿��ڴ�ͼƬ��
@nFrame����ǰ֡��
@return��
**/



/*�ж��Ƿ�Ϊ��������*/
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
	IplImage *cloneImg1 = 0;//Ϊ�˷�ֹ�Ըı�ǰ��ͼ����
	CvSeq * contour = 0;//������Ե��ȡʱ�Ĳ���
	CvPoint pt3,pt4;
	int mode = CV_RETR_EXTERNAL;//������Ե��ȡʱ�Ĳ���
	int i,j;
	int FindCar = 0;
	int avgX = 0;//�ƶ��������ĵ�X������������
	int avgY = 0;//�ƶ��������ĵ�Y������������
	CvRect bndRect = cvRect(0,0,0,0);//��cvBoundingRect������Ӿ���ʱ��Ҫ�ľ���
	CvMemStorage * storage = cvCreateMemStorage(0);//������Ե��ȡʱ�Ĳ���

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
			//�õ�һ�����Խ��ƶ�������Χ�ľ�������
			bndRect = cvBoundingRect(contour, 0);

			//��ȡ�ƶ��������ĵ�����
			avgX = (bndRect.x + bndRect.x + bndRect.width) / 2; 
			avgY = (bndRect.y + bndRect.y + bndRect.height) / 2;


			//ֻ��������ڸ���Ȥ�����еĳ���
			if(avgY > pBegin->y && avgY < pEnd->y && avgX < pEnd->x && avgX>pBegin->x)
			{
				double ration;//���Ϳ�֮��				
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
					//�ڸ���������Ѱ�ҿ��Ƿ���ƥ��ĳ�����û�����ʾ���³���
					for(i=0;i<MAXTRACK;i++)
					{
						//�ڴ˴����и��ٴ���
						double crossRect = CrossRect(TrackBlock[i].rect,bndRect);
						if(TrackBlock[i].Direction == 1 &&  crossRect> 120
							&& TrackBlock[i].FramesTracked < nFrame /*&& abs(avgX-TrackBlock[i].avgX) < 40/*&& abs(AreaofRect(TrackBlock[i].rect)-AreaofRect(bndRect))<100*/)
						{

							TrackBlock[i].FramesTracked=nFrame;
							TrackBlock[i].avgX = avgX;
							TrackBlock[i].avgY = avgY;
							//if(AreaofRect(TrackBlock[i].rect)-AreaofRect(bndRect) <= 100)
							TrackBlock[i].rect = bndRect;
							i = MAXTRACK;//ʹ����forѭ��
							FindCar = 1;
						}
					}

					if(FindCar != 1)//��ʾ�ҵ��³���
					{					
						//�޸��̲߳���	
						TrackBlock[IndexofBlock].sp.pImage = cvCreateImage(cvSize(bndRect.width,bndRect.height),
							pFrame->depth,pFrame->nChannels);
						cvSetImageROI(pFrame,bndRect);
						cvCopy(pFrame,TrackBlock[IndexofBlock].sp.pImage,NULL);
						cvResetImageROI(pFrame);

						
						TrackBlock[IndexofBlock].sp.nBlock = IndexofBlock;		
						TrackBlock[IndexofBlock].sp.rect = bndRect;

						//�������ŵ��ſ����Է������
						InsertNewBlock(nFrame,avgX,avgY,bndRect);

						//sorting(IndexofBlock);

						if(IndexofBlock == MAXTRACK-1)
							IndexofBlock = 0;
						else
							IndexofBlock++;
					}	
					//��ֵΪ0Ϊ��һ��Ѱ�ҳ�����׼��
					FindCar=0;
				}
			}
		}
	}

		//����û��ƥ��ĳ�������ʾ�Ѿ����˱߽磬��ն�Ӧ���ſ�		
	UpdateBlocks(nFrame);

	//����Ƶ�����ò���������Ȥ������
	cvRectangle(pFrame,*pBegin,*pEnd,CV_RGB(255,0,0),2, 8, 0 );
	/*�ͷ���Դ*/
	cvReleaseMemStorage(&storage);
	cvReleaseImage(&cloneImg1);
}

/*ֻ�Ƿ���*/
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
/*���ڵ�index���ſ���з���*/
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
/*������*/
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
		asctime (timeinfo),"����ɹ�!");
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
/*��ռ�¼*/
void clear_record()
{
	int i;
	for(i=0;i<VTYPE;i++)
		nResult[i] = 0;
}

/*************************************************************************/
/***************************���������Ҫ����******************************/
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
	cvCvtColor(pImage, pGrayImg, CV_BGR2GRAY);//ת��Ϊ�Ҷ�ͼ

	

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
//											"train23"};//����������·��
//
//	char *kmeanspath = "kmeans.txt";//��������·��
//
//	IplImage* pImage;//��Ҫ�����ͼƬ
//	vehicle vType;//����
//
//	/*if(argc<2)
//	{
//		printf("Error!");
//		exit(1);
//	}*/
//	/*���ط������;����ļ�*/
//	load_svm_model(modelspath,VTYPE);
//	load_kmeans_data(kmeanspath);
//
//	/*����ͼƬ�󣬽��з��࣬Ȼ����ʾ*/
//	pImage = T_GetImage("7.jpg");
//	vType = T_Sort(pImage,models,VTYPE,kmeansdata);
//	T_ToShowImage(pImage,vType);
//
//	/*�ͷ���Դ*/
//	cvReleaseImage(&pImage);
//	free_all_para();
//
//	return 0;
//}
