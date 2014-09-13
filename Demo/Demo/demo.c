#include "allheaders.h"
#include "track_sort.h"
/************************************************************************/
/*��ʾ����*/
IplImage* GetBackgroud(char *vpath,int nFrame);
int  project_demo_1(char *vediopath,char **modelspath,char*kmeanspath);//����֡�
int  project_demo_2(char *vediopath,char **modelspath,char*kmeanspath);//����֡�
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
	pCapture = cvCreateFileCapture(vpath);//��ȡ��Ƶ�ļ�

	while(pFrame = cvQueryFrame( pCapture ))
	{
		nFrmNum++;
		if(nFrmNum == 1)
		{
			pBkImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			pBkMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrmMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
			cvConvert(pBkImg, pBkMat);//ת��Ϊ�������飬������
		}
		if(nFrmNum == nFrame)
			break;
		cvCvtColor(pFrame, pBkImg, CV_BGR2GRAY);
		cvConvert(pBkImg, pFrmMat);//ת��Ϊ�������飬������
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
	/*����Ϊ���ݱ�����ʼ��*/
	struct feature* features;//���ڴ����������Ϣ
	int n = 0;               //���ڴ�����������    
	IplImage* pFrame = NULL; //��Ƶ֡
	IplImage* pFImg = NULL;  //�����ͼ��
	IplImage* FFrame = NULL; //��һ֡
	IplImage* SFrame = NULL; //�ڶ�֡
	IplImage* FImage = NULL; //��һ֡�Ҷ�ͼ
	IplImage* SImage = NULL;//�ڶ�֡�Ҷ�ͼ
	CvMat* BWimage = NULL;//��ֺ��ֵͼ
	CvMat* DBWimage = NULL;//��̬ѧ������ͼ��

	CvMat* pFrameMat = NULL;//֡����
	CvMat* FFrameMat = NULL;//��һ֡����
	CvMat* SFrameMat = NULL;//�ڶ�֡����
	CvMat* FDiffMat = NULL;//�ڶ�����һ֡��־���
	//CvMat* TempMat = NULL; //��ʱ����
	CvCapture* pCapture = NULL;
	int nFrmNum = 0;
	// int FrmNum=0;
	int i=0;
	int j=0;//ѭ�����Ʊ���

	////���ڱ���������Ƶд��
	//CvVideoWriter *writer = 0;
	//int isColor = 1;
	//int fps     = 4;  // FPS
	//int frameW  = 0; // �߶�
	//int frameH  = 0; // ���
	//int nRes = 0;
	//��������
	cvNamedWindow("video", CV_WINDOW_AUTOSIZE );//��Ƶ����
	cvNamedWindow("change",CV_WINDOW_AUTOSIZE );//��������
	//cvNamedWindow("song",CV_WINDOW_AUTOSIZE);//���Դ���

	//ʹ������������

	cvMoveWindow("video", 380, 50);
	cvMoveWindow("change", 740, 50);
	InitBlocks();//��ʼ�������ſ�
	load_svm_model(modelspath,VTYPE);//���ط�����
	load_kmeans_data(kmeanspath);//���ؾ�������
	/*����Ϊ��Ƶ��������ʾ*/

	pCapture = cvCreateFileCapture(vediopath);//��ȡ��Ƶ�ļ�

	//frameH  = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FRAME_HEIGHT);
	//frameW  = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FRAME_WIDTH);
	////fps     = (int) cvGetCaptureProperty(pCapture, CV_CAP_PROP_FPS);
	//writer=cvCreateVideoWriter("out4.avi",CV_FOURCC('X','V','I','D'),
	//                         fps,cvSize(frameW,frameH),isColor);

	//printf("fps:%d frameH:%d frameW:%d\n",fps,frameH,frameW);
	//��֡��ȡ��Ƶ
	while(pFrame = cvQueryFrame( pCapture ))//��Ƶ�����ڴ���ɶ�ȡ��ָ����һ֡�������ص�ǰ��ȡ��֡
	{
		nFrmNum++;
		//����ǵ�һ֡����Ҫ�����ڴ棬����ʼ��
		if(nFrmNum == 2)
		{
			//InitBlocks();//��ʼ�������ſ�

			FFrame = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);//��ʼ��֡������ǰͼ��֡
			pFImg = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			FImage = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			SFrame = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);//��ʼ���ڶ�֡
			SImage = cvCreateImage(cvSize(pFrame->width, pFrame->height),  IPL_DEPTH_8U,1);
			FFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			BWimage = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			DBWimage = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			pFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			FFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			SFrameMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			FDiffMat = cvCreateMat(pFrame->height, pFrame->width, CV_32FC1);
			//ת���ɵ�ͨ��ͼ���ٴ���
			//ת��Ϊ��ֵͼ��
			cvCvtColor(pFrame, FImage, CV_BGR2GRAY);
			cvConvert(FImage, FFrameMat);//ת��Ϊ�������飬������
			//�˴�����ͼ����ǿ����............
			//cvSmooth(SFrameMat,SFrameMat, CV_MEDIAN,3,0,0,0);//��ֵ�˲�
			//FFrameMat = cvCloneMat(TempMat);//���ƾ���
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


			cvCvtColor(pFrame, SImage, CV_BGR2GRAY);//ת��Ϊ�Ҷ�ͼ
			cvConvert(SImage, SFrameMat);//ת��Ϊ����
			//�˴��������ͼ����ǿ����
			cvSmooth(SFrameMat,SFrameMat, CV_MEDIAN,3,0,0,0);//��ֵ�˲�
			//���
			cvAbsDiff(SFrameMat,FFrameMat,FDiffMat);

			//��ֵ�����ͼ
			cvThreshold(FDiffMat,BWimage,60,255,CV_THRESH_BINARY);
			//���²��֡

			//FFrameMat = cvCloneMat(SFrameMat);
			cvCopy(SFrameMat,FFrameMat,NULL);
			//�Զ�ֵͼ���������
			cvDilate(BWimage,DBWimage,NULL,5);//����6��3*3�����ε���̬ѧ����

			for(i=0;i<DBWimage->rows;i++)
				for(j=0;j<DBWimage->cols;j++)
				{
					if(cvmGet(DBWimage,i,j)==0)
						cvmSet(SFrameMat,i,j,0);
				}
				//ת��Ϊͼ��
				//cvConvert(SFrameMat, pFImg);

				cvConvert(DBWimage, pFImg);

				/*���ٳ���������*/
				trackandsort(modelspath,VTYPE,kmeanspath,pFImg,pFrame,nFrmNum);

				/*�����Ǵ���Ч��չʾ����*/
				//����SIFT������

				//draw_features( pFrame, features, n );

				//д������Ƶ
				//nRes = cvWriteFrame(writer,pFrame); 
				//��ʾͼ��
				cvShowImage("video", pFrame);
				cvShowImage("change", pFImg);
				//	cvShowImage("song",pSongImg);

				//�ȴ�ʱ����Ը���CPU�ٶȵ���
				if( cvWaitKey(2) >= 0 )
					break;


		}

	}


	free_all_para();
	//���ٴ���
	cvDestroyWindow("video");
	cvDestroyWindow("change");
	//IplConvKernel* StrElem = NULL;//��̬ѧ����
	//�ͷ�ͼ��;���

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
	//�ͷŴ洢�ռ�


	//�ͷ�¼��ָ��
	//cvReleaseVideoWriter(&writer); 

	return 0;
}
int  project_demo_2(char *vediopath,char **modelspath,char*kmeanspath)
{
	/*����Ϊ���ݱ�����ʼ��*/
	//struct feature* features;//���ڴ����������Ϣ
	int n = 0;               //���ڴ�����������    
	IplImage* pFrame = NULL;
	IplImage* pFrImg = NULL;
	IplImage* pBkImg = NULL;


	CvMat* pFrameMat = NULL;
	CvMat* pFrMat = NULL;
	CvMat* pBkMat = NULL;

	CvCapture* pCapture = NULL;
	int nFrmNum = 0;

	//��̬ѧ����ʱ�ں˵Ĵ�С
	IplConvKernel* Element = cvCreateStructuringElementEx(13,13,1,1,CV_SHAPE_RECT,NULL);
	//��������
	cvNamedWindow("video", CV_WINDOW_AUTOSIZE );//��Ƶ����
	cvNamedWindow("change",CV_WINDOW_AUTOSIZE );//��������
	cvNamedWindow("song",CV_WINDOW_AUTOSIZE);//���Դ���

	//ʹ������������

	cvMoveWindow("video", 380, 50);
	cvMoveWindow("change", 740, 50);
	cvMoveWindow("song",380,300);

	InitBlocks();//��ʼ�������ſ�
	load_svm_model(modelspath,VTYPE);
	load_kmeans_data(kmeanspath);

	/*����Ϊ��Ƶ��������ʾ*/
	pCapture = cvCreateFileCapture(vediopath);//��ȡ��Ƶ�ļ�

	//��֡��ȡ��Ƶ
	while(pFrame = cvQueryFrame( pCapture ))//��Ƶ�����ڴ���ɶ�ȡ��ָ����һ֡�������ص�ǰ��ȡ��֡
	{
		nFrmNum++;
		//����ǵ�һ֡����Ҫ�����ڴ棬����ʼ��
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
			//��˹�˲��ȣ���ƽ��ͼ��
			cvSmooth(pFrameMat, pFrameMat, CV_GAUSSIAN,3,0,0,0);


			//��ǰ֡������ͼ���,cvAbsDiff�������������ľ���ֵ
			cvAbsDiff(pFrameMat, pBkMat, pFrMat);

			//��ֵ��ǰ��ͼ
			cvThreshold(pFrMat, pFrImg, 28, 255, CV_THRESH_BINARY);

			//ͨ�����ұ߽��ҳ�ROI���������ڵ��˶�������������ȫĿ�굵��
			//cvCanny(pFrImg, pBkImg, 50, 150, 3);
			cvDilate(pFrImg,pFrImg,NULL,1);//����
			/*���ٳ���������*/
			trackandsort(modelspath,VTYPE,kmeanspath,pFrImg,pFrame,nFrmNum);
			
			/*���±���*/
			cvRunningAvg(pFrameMat, pBkMat, 0.005, 0);
			//������ת��Ϊͼ���ʽ��������ʾ
			cvConvert(pBkMat, pBkImg);
			/*�����Ǵ���Ч��չʾ����*/
			//����SIFT������

			//draw_features( pFrame, features, n );
			cvShowImage("video", pFrame);
			cvShowImage("change", pFrImg);
			cvShowImage("song",pBkImg);

			//�ȴ�ʱ����Ը���CPU�ٶȵ���
			if( cvWaitKey(2) >= 0 )
				break;


		}

	}
	free_all_para();
	//���ٴ���
	cvDestroyWindow("video");
	cvDestroyWindow("change");
	cvDestroyWindow("song");

	//ɾ���ṹԪ��
	cvReleaseStructuringElement(&Element);


	//�ͷ�ͼ��;���
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
//											"train23"};//����������·��
//
//	char *kmeanspath = "kmeans.txt";//��������·��
//
//	char *vediopath = "test9.xvid";//�����Ƶ·��
//
//	project_demo_1(vediopath,modelspath,kmeanspath);
//
//	return 0;
//}