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
        ///////////////////////��������
        public bool DeleteManager(string strManagerId)
        {//ɾ������Ա
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "delete from Manager where ManagerId=" + strManagerId;
            if (ExeNoQuerySqlString(null, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("ɾ��ʧ��");
                return false;
            }

        }
        public bool AddManager(string strManagerName,string strManagePw,string JoinTime)
        {//��ӹ���Ա
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
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
                  MessageBox.Show("��ӹ���Աʧ��");
                  return false;
              }

        }
        public bool AlterManager(string strMangerId, string strManagerName, string strManagePw, string JoinTime)
        {//�޸Ĺ���Ա��Ϣ
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
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
                MessageBox.Show("�޸Ĺ���Ա��Ϣʧ��");
                return false;
            }
         
        }
    }
}
