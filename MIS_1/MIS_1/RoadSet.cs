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
        {//��ȡ���ڱ��
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
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
        {//��ȡ���ر��
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return null;
            }
            string cityId = GetCityId(strCity);
            if (cityId == null)
            {
                MessageBox.Show("���в�����");
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
        {//��ȡ�б��
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
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
       


        ////////////���еĲ���//////////////////////////
        public bool DeleteCity(string strCityName)
        {//ɾ��ָ��Id����
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "delete from CIty where CityName='" + strCityName + "'";
            if (ExeNoQuerySqlString(conn, cmdString))
            {

                return true;
            }
            else
            {
                MessageBox.Show("ɾ��ʧ��");
                return false;
            }
        }

        public bool AddCity(string strCityName)
        {//���һ����
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string strId = GetCityId(strCityName);
            if (strId != null)
            {
                MessageBox.Show("�������Ѿ����ڣ�");
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
        {//�޸�һ���е���Ϣ
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                Console.WriteLine("�޷����ӵ����ݿ�!");
                return false;
            }
            string strId = GetCityId(strCItyNew);
            if (strId != null)
            {
                MessageBox.Show("�������Ѿ����ڣ�");
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


        ///////�����Ĳ���////////////////
        public bool AddRegion(string strRegionName, string strCityName)
        {//���һ����
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string regionId = GetRegionId(strRegionName, strCityName);
            if (regionId != null)
            {
                Console.WriteLine("���������Ѿ�����!");
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
                    Console.WriteLine("��ѯʧ��!");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool AlterRegion(string strRegionId, string strRegionName, string strCityName)
        {//�޸�һ��������Ϣ
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string regionId = GetRegionId(strRegionName, strCityName);
            string cityId = GetCityId(strCityName);
            if (regionId != null)
            {
                MessageBox.Show("�������Ѿ�����");
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
                    MessageBox.Show("�޸���������Ϣʧ��!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("���в�����!");
                return false;
            }
        }
        public bool DeleteRegion(string strRegionId)
        {//ɾ��һ����
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string cmdString = "delete from Region where RegionId=" + strRegionId;

            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("ɾ��������ʧ��!");
                return false;

            }

        }
        //////////////////////////�Ե��ڵĲ���//////////////////
        public bool AddRoad(string strRoad, string strRegion, string strCity)
        {//��ӵ��� 
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string regionId = GetRegionId(strRegion, strCity);
            string cityId = GetCityId(strCity);
            if (cityId != null && regionId != null)
            {
                string cmdString = "insert into RoadCrossing(Name,RegionId,CityId) values('" + strRoad + "'," + regionId + "," + cityId + ")";
                if (GetRoadId(strRoad, strRegion, strCity) != null)
                {
                    MessageBox.Show("�õ����Ѿ�����!");
                    return false;
                }
                if (ExeNoQuerySqlString(conn, cmdString))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("���ػ��в�����!");
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        public  bool DeleteRoad(string strRoad, string strRegion, string strCity)
        {//ɾ������
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
                return false;
            }
            string strId = GetRoadId(strRoad, strRegion, strCity);
            string cmdString = "delete from RoadCrossing where Id=" + strId;
            if (GetRoadId(strRoad, strRegion, strCity) == null)
            {
                MessageBox.Show("Ҫɾ���ĵ��ڲ�����!");
                return false;
            }
            if (ExeNoQuerySqlString(conn, cmdString))
            {
                return true;
            }
            else
            {
                MessageBox.Show("ɾ������ʧ��!");
                return false;

            }

        }
        public bool AlterRoad(string strId, string strRoad, string strRegion, string strCity)
        {//�޸ĵ�����Ϣ
            if (conn.State != ConnectionState.Open)
                conn = LinkDataBase();
            if (conn == null)
            {
                MessageBox.Show("�޷����ӵ����ݿ�!");
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
                    MessageBox.Show("�޸ĵ�����Ϣʧ��!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("���ػ����в�����!");
                return false;
            }
        }
    }
}
