#pragma once
#define MAX_ITEMS 128
#include "DataAccuess.h"
class VehicleDetect
{
public:
	IplImage* m_img;
	int m_typeId;
	int m_roadId;
public:
	VehicleDetect(const IplImage* img,const int typeId,const int roadId)
	{
		m_img = cvCloneImage(img);
		m_typeId = typeId;
		m_roadId = roadId;
	}
};

/*将车辆数据压栈..然后不断插入数据库*/
class VehicleQueue
{
public:
	VehicleQueue(DataAccess* pDataAccess);
	VehicleQueue(){}
	~VehicleQueue(void);
private:
	static DataAccess* m_pDataAccess;
	static int m_headIndex;
	static int m_tailIndex;
	static int m_count;
	static BOOL m_bIsStop; 
	static CMutex m_mutex;
	static VehicleDetect* m_vehicleArray[MAX_ITEMS];
	static DWORD InsertVehicleThread( LPVOID lpParameter);
	void Push(VehicleDetect* pVehicleDetect);
	
public:
	void SetDataAccess(DataAccess* pDataAccess)
	{
		m_pDataAccess = pDataAccess;
	}
	
	void Push(IplImage* img, int typeId, int roadId);
	void Start();
	void Stop();
};
