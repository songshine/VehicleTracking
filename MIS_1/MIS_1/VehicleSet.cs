using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace MIS_1
{
    class VehicleSet:BaseSet
    {
        //private SqlConnection conn;
        public VehicleSet(string str):base(str)
        {
            
        }
       

        ////////////////////////////////���ڳ������͵Ĺ���///////////////
        public bool DeleteVehicleType(string str)
        {//ɾ������
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "delete from VehicleType where Name='" + str + "'";
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AlterVehicleType(string strOld, string strNew)
        {//�޸ĳ���
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "Update VehicleType set Name='" + strNew + "' where Name='" + strOld + "'";
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AddVheicleType(string str)
        {//��ӳ���
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "insert into VehicleType(Name) values('" + str + "')";
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteVehicleInfo(string strVehicleInfoId)
        {//ɾ��һ�г�����Ϣ
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
          
            string cmdString = "delete from Vehicle where Id=" + strVehicleInfoId;
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }
              
        }
        public bool GetTheVehicleImage(string strId,string strPathImage)
        {//��ȡͼƬ����������
            return true;
        }
    }
    
}
