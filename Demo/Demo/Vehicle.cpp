// Vehicle.h : CVehicle 类的实现



// CVehicle 实现

// 代码生成在 2011年9月20日, 16:28

#include "stdafx.h"
#include "Vehicle.h"
IMPLEMENT_DYNAMIC(CVehicle, CRecordset)

CVehicle::CVehicle(CDatabase* pdb)
	: CRecordset(pdb)
{
	m_Id = 0;
	m_TypeId = 0;
	m_RoadId = 0;
	m_Picture;
	m_RecordTime;
	m_nFields = 5;
	m_nDefaultType = dynaset;
}
//#error Security Issue: The connection string may contain a password
// 此连接字符串中可能包含明文密码和/或其他重要
// 信息。请在查看完此连接字符串并找到所有与安全
// 有关的问题后移除 #error。可能需要将此密码存
// 储为其他格式或使用其他的用户身份验证。
CString CVehicle::GetDefaultConnect()
{
	return _T("DSN=Project;Trusted_Connection=Yes;APP=Microsoft\x00ae Visual Studio\x00ae 2005;WSID=SONG-PC;DATABASE=Project");
}

CString CVehicle::GetDefaultSQL()
{
	return _T("[dbo].[Vehicle]");
}

void CVehicle::DoFieldExchange(CFieldExchange* pFX)
{
	pFX->SetFieldType(CFieldExchange::outputColumn);
// RFX_Text() 和 RFX_Int() 这类宏依赖的是
// 成员变量的类型，而不是数据库字段的类型。
// ODBC 尝试自动将列值转换为所请求的类型
	RFX_Long(pFX, _T("[Id]"), m_Id);
	RFX_Long(pFX, _T("[TypeId]"), m_TypeId);
	RFX_Long(pFX, _T("[RoadId]"), m_RoadId);
	RFX_LongBinary(pFX, _T("[Picture]"), m_Picture);
	RFX_Date(pFX, _T("[RecordTime]"), m_RecordTime);

}
/////////////////////////////////////////////////////////////////////////////
// CVehicle 诊断

#ifdef _DEBUG
void CVehicle::AssertValid() const
{
	CRecordset::AssertValid();
}

void CVehicle::Dump(CDumpContext& dc) const
{
	CRecordset::Dump(dc);
}
#endif //_DEBUG


