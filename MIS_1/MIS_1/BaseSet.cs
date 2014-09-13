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
                MessageBox.Show("无法连接到数据库!");
            }
        }
       // ~BaseSet()
       // {

       //     if (conn.State == ConnectionState.Open)
       //     {
       //         conn.Close();
       //     }
       //}
       
        ///////////////////////基本操作
        protected SqlConnection LinkDataBase()
        {//连接数据库
              
            SqlConnection conne = new SqlConnection(); ;
            try
            {
                conne.ConnectionString = strLink;
                conne.Open();
                if (conne.State != ConnectionState.Open)
                {
                    MessageBox.Show("连接数据库失败!");
                    return null;
                }
                return conne;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("出现错误:{0}" + ex.Message);
                return null;

            }
        }
      
        public bool ExeNoQuerySqlString(SqlConnection conne, string strSql)
        {//执行非查询语句
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
                    MessageBox.Show("出现异常:{0}", e.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public SqlDataReader ExeQuerySlqString(SqlConnection conne, string strSql)
        {//执行查询语句,返回一个SqlDataReader
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
                    MessageBox.Show("出现异常:{0}", e.Message);
                    return null;
                }
            }
            return null;

        }

        public DataSet ExeQuerySqlString(SqlConnection conne, string strSql, string strTableName)
        {//执行查询语句,返回一个DataSet
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
                    MessageBox.Show("出现错误: " + ex.Message);
                    return null;

                }
              
            }
        
    }
}
