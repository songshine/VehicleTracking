using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
namespace MIS_1
{
    class RoadSet:BaseSet
    {
        public RoadSet(string str):base(str)
        {
            
        }

        
        public string GetRoadId(string strRoad, string strRegion, string strCity)
        {//获取道口编号
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return null;
            }
            string cmdString = "select Id from RoadInfo where Name='" + strRoad + "' and RegionName='" + strRegion + "' and CityName='" + strCity + "'";
            SqlDataReader sdr = ExeQuerySlqString(conn, cmdString);
            if (!sdr.HasRows)
            {
                sdr.Close();
                return null;
            }
            sdr.Read();
            string str = sdr[0].ToString();
            sdr.Close();
            return str;
        }
        public string GetRegionId(string strRegion, string strCity)
        {//获取区县编号
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return null;
            }
            string cityId = GetCityId(strCity);
            if (cityId == null)
            {
                MessageBox.Show("该市不存在");
                return null;
            }
            string cmdString = "select RegionId from Region where RegionName='" + strRegion + "' and CityId=" + cityId;
            SqlDataReader sdr = ExeQuerySlqString(conn, cmdString);
            if (!sdr.HasRows)
            {
                sdr.Close();
                return null;
            }
            sdr.Read();
            string str = sdr[0].ToString();
            sdr.Close();
            return str;
        }
        public string GetCityId(string strCity)
        {//获取市编号
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return null;
            }
            string cmdString = "select CityId from City where CityName='" + strCity + "'";
            SqlDataReader sdr = ExeQuerySlqString(conn, cmdString);
            if (!sdr.HasRows)
            {
                sdr.Close();
                return null;
            }
            sdr.Read();
            string str = sdr[0].ToString();
            sdr.Close();
            return str;
        }
       


        ////////////对市的操作//////////////////////////
        public bool DeleteCity(string strCityName)
        {//删除指定Id的市
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string cmdString = "delete from CIty where CityName='" + strCityName + "'";
            if (ExeNoQuerySqlString(conn, cmdString))
            {

                return true;
            }
            else
            {
                MessageBox.Show("删除失败");
                return false;
            }
        }

        public bool AddCity(string strCityName)
        {//添加一个市
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string strId = GetCityId(strCityName);
            if (strId != null)
            {
                MessageBox.Show("该市名已经存在！");
                return false;
            }
            string cmdString = "insert into City(CityName) values('" + strCityName + "')";
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AlterCity(string strCityOld, string strCItyNew)
        {//修改一个市的信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                Console.WriteLine("无法连接到数据库!");
                return false;
            }
            string strId = GetCityId(strCItyNew);
            if (strId != null)
            {
                MessageBox.Show("该市名已经存在！");
                return false;
            }
            string cmdString = "Update City set CityName='" + strCItyNew + "' where CityName='" + strCityOld + "'";
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        ///////对区的操作////////////////
        public bool AddRegion(string strRegionName, string strCityName)
        {//添加一个区
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string regionId = GetRegionId(strRegionName, strCityName);
            if (regionId != null)
            {
                Console.WriteLine("该区或县已经存在!");
                return false;
            }
            string cityId = GetCityId(strCityName);
            if (cityId != null)
            {
                string cmdString = "insert into Region(RegionName,CityId) values('" + strRegionName + "'," + cityId + ")";
                if (ExeNoQuerySqlString(conn, cmdString))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("查询失败!");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool AlterRegion(string strRegionId, string strRegionName, string strCityName)
        {//修改一个区的信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string regionId = GetRegionId(strRegionName, strCityName);
            string cityId = GetCityId(strCityName);
            if (regionId != null)
            {
                MessageBox.Show("该区县已经存在");
                return false;
            }
            if (cityId != null)
            {
                string cmdString = "update Region set  RegionName='" + strRegionName + "',CityId=" + cityId + "  where RegionId=" + strRegionId;
                if (ExeNoQuerySqlString(conn, cmdString))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("修改区或县信息失败!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("该市不存在!");
                return false;
            }
        }
        public bool DeleteRegion(string strRegionId)
        {//删除一个区
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string cmdString = "delete from Region where RegionId=" + strRegionId;

            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("删除区或县失败!");
                return false;

            }

        }
        //////////////////////////对道口的操作//////////////////
        public bool AddRoad(string strRoad, string strRegion, string strCity)
        {//添加道口 
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string regionId = GetRegionId(strRegion, strCity);
            string cityId = GetCityId(strCity);
            if (cityId != null && regionId != null)
            {
                string cmdString = "insert into RoadCrossing(Name,RegionId,CityId) values('" + strRoad + "'," + regionId + "," + cityId + ")";
                if (GetRoadId(strRoad, strRegion, strCity) != null)
                {
                    MessageBox.Show("该道口已经存在!");
                    return false;
                }
                if (ExeNoQuerySqlString(conn, cmdString))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("区县或市不存在!");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public  bool DeleteRoad(string strRoad, string strRegion, string strCity)
        {//删除道口
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string strId = GetRoadId(strRoad, strRegion, strCity);
            string cmdString = "delete from RoadCrossing where Id=" + strId;
            if (GetRoadId(strRoad, strRegion, strCity) == null)
            {
                MessageBox.Show("要删除的道口不存在!");
                return false;
            }
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("删除道口失败!");
                return false;

            }

        }
        public bool AlterRoad(string strId, string strRoad, string strRegion, string strCity)
        {//修改道口信息
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("无法连接到数据库!");
                return false;
            }
            string regionId = GetRegionId(strRegion, strCity);
            string cityId = GetCityId(strCity);
            if (regionId != null && cityId != null)
            {
                string cmdString = "update RoadCrossing set  Name='" + strRoad + "',RegionId=" + regionId + ",CityId=" + cityId + "  where Id=" + strId;
                if (ExeNoQuerySqlString(conn, cmdString))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("修改道口信息失败!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("区县或者市不存在!");
                return false;
            }
        }
    }
}
