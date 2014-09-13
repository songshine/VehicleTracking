using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MIS_1
{
    class ManagerSet:BaseSet
    {
        public ManagerSet(string str):base(str)
        {
           
        }
        ///////////////////////基本操作
        public bool DeleteManager(string strManagerId)
        {//删除管理员
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string cmdString = "delete from Manager where ManagerId=" + strManagerId;
            if (ExeNoQuerySqlString(null, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("删除失败");
                return false;
            }

        }
        public bool AddManager(string strManagerName,string strManagePw,string JoinTime)
        {//添加管理员
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
              string cmdString = "insert into Manager(ManagerName,LevelId,Password,JoinTime) values('"
                + strManagerName + "'," + 1 + ",'" + strManagePw + "','" + JoinTime + "')";
              if (ExeNoQuerySqlString(null, cmdString))
              {
                  return true;
              }
              else
              {
                  MessageBox.Show("添加管理员失败");
                  return false;
              }

        }
        public bool AlterManager(string strMangerId, string strManagerName, string strManagePw, string JoinTime)
        {//修改管理员信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string cmdString = "Update Manager set ManagerName='"
            + strManagerName + "',Password='" + strManagePw + "',JoinTime='" + JoinTime + "' where ManagerId=" + strMangerId;
            if (ExeNoQuerySqlString(null, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("修改管理员信息失败");
                return false;
            }
         
        }
    }
}
