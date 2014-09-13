#include "StdAfx.h"
#include "DataAccuess.h"
#include "Vehicle.h"

#pragma region Construct and deconstruct

DataAccess::DataAccess(void)
{
	m_db.Open(DSNName);
	m_pVehicleSet = NULL;
}

DataAccess::~DataAccess(void)
{
	if(m_pVehicleSet != NULL
		&& m_pVehicleSet->IsOpen())
	{
		m_pVehicleSet->Close();
		delete m_pVehicleSet;
	}
	if(m_db.IsOpen())
	{
		m_db.Close();
	}
}
#pragma endregion

#pragma region Inserts Vehicle

BOOL DataAccess::InsertVehicle(IplImage* img,int typeId,int roadId)
{

	BOOL retVal = TRUE;
	if(m_pVehicleSet == NULL)
	{
		m_pVehicleSet = new CVehicle(&m_db);
		m_pVehicleSet->Open(CRecordset::snapshot); 
	}
	
	try 
	{
		CFile file;
		CFileStatus fileStaus;
		cvSaveImage("song.bmp",img,0);

		file.Open("song.bmp",CFile::modeRead);
		file.GetStatus(fileStaus);
		m_pVehicleSet->AddNew();
		m_pVehicleSet->m_TypeId = typeId;
		m_pVehicleSet->m_RoadId = roadId;
		CTime timeNow = CTime::GetCurrentTime();
		m_pVehicleSet->m_RecordTime = timeNow;
		m_pVehicleSet->m_Picture.m_dwDataLength = fileStaus.m_size;
		//int nSize = file.GetLength();
		void* lpBuffer = GlobalAlloc(GPTR,fileStaus.m_size);
		m_pVehicleSet->m_Picture.m_hData = GlobalLock(lpBuffer);
		file.Read(m_pVehicleSet->m_Picture.m_hData,fileStaus.m_size);

		m_pVehicleSet->SetFieldDirty(&(m_pVehicleSet->m_Picture));
		m_pVehicleSet->SetFieldNull(&(m_pVehicleSet->m_Picture),FALSE);

		m_pVehicleSet->Update();
		GlobalUnlock(lpBuffer);
	}
	catch(CException* e)
	{
		retVal = FALSE;
	}
	return retVal;
}
#pragma endregion

#pragma region Private Methods

CObArray* DataAccess::GetIdAndNameList(CRecordset& set)
{
	CObArray* pArrayList = new CObArray();

	int id;
	CString name;
	CDBVariant varValue;
	
	while(!set.IsEOF())
	{		
		set.GetFieldValue((short)0,varValue);
		id = varValue.m_lVal;
		set.GetFieldValue((short)1,varValue);
		name = *varValue.m_pstring;
		CIdName* idName = new CIdName(id,name);
		pArrayList->Add(idName);

		set.MoveNext();
	}
	return pArrayList;
}

#pragma endregion

#pragma region City List

CObArray* DataAccess::GetCityList()
{
	CObArray* pCityList;
	CRecordset vehSet(&m_db);
	CString strSql = "select CityId, CityName from City";
	vehSet.Open(CRecordset::forwardOnly,strSql);	
	pCityList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pCityList;
}

#pragma endregion

#pragma region Region List

CObArray* DataAccess::GetRegionList()
{
	CObArray* pRegionList;
	CRecordset vehSet(&m_db);
	CString strSql = "select RegionId, RegionName from Region";
	vehSet.Open(CRecordset::forwardOnly,strSql);

	pRegionList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pRegionList;
}
CObArray* DataAccess::GetRegionListByCityId(long cityId)
{
	CObArray* pRegionList;

	CRecordset vehSet(&m_db);
	CString strSql;
	strSql.Format("select RegionId, RegionName from Region where CityId=%ld",cityId);
	vehSet.Open(CRecordset::forwardOnly,strSql);

	pRegionList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pRegionList;
}
#pragma endregion

#pragma region Road List

CObArray* DataAccess::GetRoadList()
{
	CObArray* pRoadList;

	CRecordset vehSet(&m_db);
	CString strSql = "select Id, Name from RoadCrossing";
	vehSet.Open(CRecordset::forwardOnly,strSql);

	pRoadList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pRoadList;
}
CObArray* DataAccess::GetRoadListByCityAndRegionId(long cityId, long regionId)
{
	CObArray* pRoadList;
	CRecordset vehSet(&m_db);
	CString strSql;
	strSql.Format("select Id, Name from RoadCrossing where CityId=%ld and RegionId=%ld",cityId,regionId);
	vehSet.Open(CRecordset::forwardOnly,strSql);
	pRoadList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pRoadList;
}


#pragma endregion

#pragma region Vehilce Type List

CObArray* DataAccess::GetVehicleTypeList()
{
	CObArray* pTypeList;

	CRecordset vehSet(&m_db);
	CString strSql = "select Id, Name from VehicleType";
	vehSet.Open(CRecordset::forwardOnly,strSql);

	pTypeList = GetIdAndNameList(vehSet);

	vehSet.Close();

	return pTypeList;
}

#pragma endregion
