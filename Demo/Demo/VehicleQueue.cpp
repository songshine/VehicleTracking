#include "StdAfx.h"
#include "VehicleQueue.h"
#include "DataAccuess.h"

#pragma region Construct and Deconstruct

VehicleQueue::VehicleQueue(DataAccess* pDataAccess)
{
	m_pDataAccess = pDataAccess;
}

VehicleQueue::~VehicleQueue(void)
{
	m_bIsStop = TRUE;
}

#pragma endregion

#pragma region Initializes Static variables

int VehicleQueue::m_headIndex = 0;
int VehicleQueue::m_tailIndex = 0;
int VehicleQueue::m_count = 0;
BOOL VehicleQueue::m_bIsStop = FALSE;
DataAccess* VehicleQueue::m_pDataAccess = NULL;
CMutex VehicleQueue::m_mutex;
VehicleDetect* VehicleQueue::m_vehicleArray[MAX_ITEMS];

#pragma endregion


#pragma region Public Methods

void VehicleQueue::Push(IplImage* img, int typeId, int roadId)
{
	VehicleDetect* pVehicleDetect = new VehicleDetect(img,typeId,roadId);
	Push(pVehicleDetect);
}
void VehicleQueue::Push(VehicleDetect* pVehicleDetect)
{
	m_mutex.Lock();
	m_vehicleArray[m_tailIndex] = pVehicleDetect;
	m_tailIndex = (m_tailIndex+1) % MAX_ITEMS;
	m_count++;
	m_mutex.Unlock();
}

void VehicleQueue::Start()
{
	HANDLE  hInsertThread = CreateThread(NULL,0,(LPTHREAD_START_ROUTINE)InsertVehicleThread,
		NULL,0,NULL);
	CloseHandle(hInsertThread);
	m_bIsStop = FALSE;
}
void VehicleQueue::Stop()
{
	m_bIsStop = TRUE;
}
#pragma endregion


#pragma region Static Methods


DWORD VehicleQueue::InsertVehicleThread( LPVOID lpParameter)
{
	while(!m_bIsStop)
	{
		if(m_headIndex != m_tailIndex)
		{
			m_mutex.Lock();
			VehicleDetect* pVehicleDetect = m_vehicleArray[m_headIndex];
			m_headIndex = (m_headIndex+1) % MAX_ITEMS;
			m_count--;
			m_mutex.Unlock();

			m_pDataAccess->InsertVehicle(
				pVehicleDetect->m_img,
				pVehicleDetect->m_typeId,
				pVehicleDetect->m_roadId
				);

			cvReleaseImage(&pVehicleDetect->m_img);
			delete pVehicleDetect;
		}
	}
	return 0;
}
#pragma endregion
