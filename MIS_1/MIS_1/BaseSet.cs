using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace MIS_1
{
    class BaseSet
    {
        protected SqlConnection conn;
        public string strLink;

        public BaseSet(string str)
        {
            strLink = str;
            conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
            }
        }
       // ~BaseSet()
       // {

       //     if (conn.State == ConnectionState.Open)
       //     {
       //         conn.Close();
       //     }
       //}
       
        ///////////////////////��������
        protected SqlConnection LinkDataBase()
        {//�������ݿ�
              
            SqlConnection conne = new SqlConnection(); ;
            try
            {
                conne.ConnectionString = strLink;
                conne.Open();
                if (conne.State != ConnectionState.Open)
                {
                    MessageBox.Show("�������ݿ�ʧ��!");
                    return null;
                }
                return conne;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("���ִ���:{0}" + ex.Message);
                return null;

            }
        }
      
        public bool ExeNoQuerySqlString(SqlConnection conne, string strSql)
        {//ִ�зǲ�ѯ���
            if(conne == null)
                conne=conn;
            if (conne.State == ConnectionState.Open)
            {
                try
                {

                    SqlCommand cmd = new SqlCommand(strSql);
                    cmd.Connection = conne;
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (SqlException e)
                {
                    MessageBox.Show("�����쳣:{0}", e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public SqlDataReader ExeQuerySlqString(SqlConnection conne, string strSql)
        {//ִ�в�ѯ���,����һ��SqlDataReader
            SqlDataReader sdr;
            if(conne == null)
                conne=conn;
            if (conne.State == ConnectionState.Open)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(strSql);
                    cmd.Connection = conne;
                    sdr = cmd.ExecuteReader();
                    return sdr;

                }
                catch (SqlException e)
                {
                    MessageBox.Show("�����쳣:{0}", e.Message);
                    return null;
                }
            }
            return null;

        }

        public DataSet ExeQuerySqlString(SqlConnection conne, string strSql, string strTableName)
        {//ִ�в�ѯ���,����һ��DataSet
            if(conne == null)
                conne=conn;
                try
                 {      
                    DataSet ds = new DataSet();
                    SqlDataAdapter daManager = new SqlDataAdapter(strSql, conn);
                    daManager.Fill(ds, strTableName);
                    return ds;

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("���ִ���: " + ex.Message);
                    return null;

                }
              
            }
        
    }
}
