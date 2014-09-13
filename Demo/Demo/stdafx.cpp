// stdafx.cpp : 只包括标准包含文件的源文件
// Demo.pch 将作为预编译头
// stdafx.obj 将包含预编译类型信息

#include "stdafx.h"

CString GetCurPath()
{//获取程序所在路径
	char  exepath[MAX_PATH];
	CString  strdir,tmpdir; 
	memset(exepath,0,MAX_PATH); 
	GetModuleFileName(NULL,exepath,MAX_PATH); 
	tmpdir=exepath; 
	strdir=tmpdir.Left(tmpdir.ReverseFind('\\'));
	return strdir; 
}
