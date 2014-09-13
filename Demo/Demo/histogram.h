#ifndef _HISTOGRAM_H
#define _HISTOGRAM_H


struct histogram_parameter
{
	int K;
	int Vectordim;
	int datasize;
};
extern void h_out_data(double *hgm,int len,char *out_file);
extern double ** h_load_data(char *pkmeansname,const struct histogram_parameter para1);
extern void histogram(double **psiftdata,  double **kmeansdata,
					  double *ptraindata,const struct histogram_parameter *para1);
#endif
