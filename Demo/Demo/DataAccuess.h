#pragma once

#include "Vehicle.h"

#define DbName "Project"
#define DSNName "Project"

class CIdName:public CObject
{
public: 
	long m_id;
	CString m_name;
	CIdName(long id, CString name)
	{
		m_id = id;
		m_name = name;
	}
	CIdName(){}
};
class DataAccess
{
public:
	DataAccess(void);
	~DataAccess(void);

	//插入Vehicle记录
	BOOL InsertVehicle(IplImage* img,int typeId,int roadId);

	//获取city、region或者Road列表
	//city
	CObArray* GetCityList();
	//Region
	CObArray* GetRegionList();
	CObArray* GetRegionListByCityId(long cityId);
	//Road
	CObArray* GetRoadList();
	CObArray* GetRoadListByCityAndRegionId(long cityId, long regionId);

	//获取车辆类别列表
	CObArray* GetVehicleTypeList();

private:
	CObArray* GetIdAndNameList(CRecordset& set);
	/*int CStringToInt(const CString& strInt);*/
private:
	CDatabase m_db;
	CVehicle* m_pVehicleSet;
};
