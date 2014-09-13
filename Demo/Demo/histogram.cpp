#include "stdafx.h"
#include "histogram.h"

//double **Kmeans_data = NULL;
//struct histogram_parameter para;

/************************************************************************/
/* 直方图中局部函数                                                       */
double** array(int m, int n);
int Get_minid(double **sift_data,double **kmeansdata,int id,
			  const struct histogram_parameter para1);
void do_histogram(double **sift_data,double ** kmeansdata,double* hgm,
				  const struct histogram_parameter para1);
int his = 0;
/************************************************************************/



double** array(int m, int n)
{
    double **p = NULL;
    int i;
    p=(double**)malloc(m*sizeof(double*));
    for(i=0;i<m;i++)
		p[i] = (double*)malloc(n*sizeof(double));
    return p;
}





// f1 is sift data                   data_size*Vectordim
// f2 is class data   : class id     data_size*2
// f3 is kmeans data                 K*Vectordim

double ** h_load_data(char *pkmeansname,const struct histogram_parameter para1)
{
    int i,j;
    FILE *f1;
	double **kmeansdata;
	double temp;
    if((f1 = fopen(pkmeansname,"r")) == NULL)
    {
        AfxMessageBox("can't open kmeans file");
		exit(0);
    }
   
    if(feof(f1))
    {
        AfxMessageBox("%s is a empty file!");
        fclose(f1);
        exit(0);
    }
   
    //just scanf! !
    
    
    kmeansdata = array(para1.K,para1.Vectordim);
   
    for(i=0; i<para1.K; i++)
    {
        for(j=0; j<para1.Vectordim;j++)
        {
            if(j==(para1.Vectordim-1))
                fscanf(f1,"%lf\n",&temp);
            else
                fscanf(f1,"%lf ",&temp);
			kmeansdata[i][j] = temp;
        }

    }
	fclose(f1);
	return kmeansdata;
    
}


// the id is row of sift data
int Get_minid(double **sift_data,double **kmeansdata,int id,
			  const struct histogram_parameter para1)
{
    int i=0;
	int j=0;
	double sum = 0,min = 0;
    int min_id = 0;
    for(j=0;j<para1.K;j++)
    {
        sum = 0.0;
        for(i=0;i<para1.Vectordim;i++)
            sum += pow(sift_data[id][i]-kmeansdata[j][i],2);
        if(j == 0)
        {
            min = sum;
            min_id = j;
        }
        else
            if(min>sum)
            {
                min = sum;
                min_id = j;
            }
    }
    return min_id;


}
//id is the number of train data,
// beg is row of sift data
void do_histogram(double ** sift_data,double ** kmeansdata,double *hgm,
				  const struct histogram_parameter para1)
{

    int min_id,all = 0;

	int i;
	for(i=0;i<para1.datasize;i++)
	{
		min_id = Get_minid(sift_data,kmeansdata,i,para1);
		hgm[min_id]++;
		all++;
	} 
    for(i=0;i<para1.K;i++)
        hgm[i] = hgm[i]/all;

}
void h_out_data(double *hgm,int len,char *out_file)
{
	int i;
	FILE *pFile;
	pFile=fopen(out_file,"wb");
	for(i=0;i<len;i++)
		if(i == len-1)
			fprintf(pFile,"%d:%f\n",i+1,hgm[i]);
		else
			fprintf(pFile,"%d:%f ",i+1,hgm[i]);
	fclose(pFile);
}


void histogram(double **psiftdata,  double **kmeansdata,
			   double *ptraindata,const struct histogram_parameter *para1)
{
	if(kmeansdata != NULL )
		do_histogram(psiftdata,kmeansdata,ptraindata,*para1);
	else
		return;
}









