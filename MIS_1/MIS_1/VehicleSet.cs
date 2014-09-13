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
       

        ////////////////////////////////关于车辆类型的管理///////////////
        public bool DeleteVehicleType(string str)
        {//删除车型
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
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
        {//修改车型
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
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
        {//添加车型
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
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
        {//删除一行车辆信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
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
        {//读取图片并保存起来
            return true;
        }
    }
    
}
