// stdafx.cpp : ֻ������׼�����ļ���Դ�ļ�
// Demo.pch ����ΪԤ����ͷ
// stdafx.obj ������Ԥ����������Ϣ

#include "stdafx.h"

CString GetCurPath()
{//��ȡ��������·��
	char  exepath[MAX_PATH];
	CString  strdir,tmpdir; 
	memset(exepath,0,MAX_PATH); 
	GetModuleFileName(NULL,exepath,MAX_PATH); 
	tmpdir=exepath; 
	strdir=tmpdir.Left(tmpdir.ReverseFind('\\'));
	return strdir; 
}
