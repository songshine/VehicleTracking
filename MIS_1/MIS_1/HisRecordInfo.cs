using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

using Microsoft.ReportingServices.ReportRendering;

namespace MIS_1
{
    class HisRecordInfo : BaseSet
    {
        public HisRecordInfo(string str)
            : base(str)
        {

        }


        ////////////////////////////////关于车辆类型的管理///////////////

        public bool DeleteVehicleInfo(string strVehicleInfoId)
        {//删除一行车辆信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }

            string cmdString = "delete from History_Vehicle where Id=" + strVehicleInfoId;
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public byte[] GetImageInHistory(string strId)
        {
            string strSql = "select Picture from History_Vehicle where Id=" + strId;
            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Connection = conn;

            byte[] b = (byte[])cmd.ExecuteScalar();
            return b;

        }
    }
}
