// Vehicle.h : CVehicle ���ʵ��



// CVehicle ʵ��

// ���������� 2011��9��20��, 16:28

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
// �������ַ����п��ܰ������������/��������Ҫ
// ��Ϣ�����ڲ鿴��������ַ������ҵ������밲ȫ
// �йص�������Ƴ� #error��������Ҫ���������
// ��Ϊ������ʽ��ʹ���������û������֤��
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
// RFX_Text() �� RFX_Int() �������������
// ��Ա���������ͣ����������ݿ��ֶε����͡�
// ODBC �����Զ�����ֵת��Ϊ�����������
	RFX_Long(pFX, _T("[Id]"), m_Id);
	RFX_Long(pFX, _T("[TypeId]"), m_TypeId);
	RFX_Long(pFX, _T("[RoadId]"), m_RoadId);
	RFX_LongBinary(pFX, _T("[Picture]"), m_Picture);
	RFX_Date(pFX, _T("[RecordTime]"), m_RecordTime);

}
/////////////////////////////////////////////////////////////////////////////
// CVehicle ���

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


