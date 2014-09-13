using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MIS_1
{
    public partial class VehicleQueryOptionForm : Form
    {
        public VehicleQueryOptionForm()
        {
            InitializeComponent();
        }

        private void VehicleQueryOptionForm_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���projectDataSet14.VehicleType���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.vehicleTypeTableAdapter1.Fill(this.projectDataSet14.VehicleType);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet13.Region���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.regionTableAdapter1.Fill(this.projectDataSet13.Region);
         
            LoadRoadList();
         

        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (dateTimePickerBeginTime.Value >= dateTimePickerEndTime.Value)
            {
                MessageBox.Show("ʱ�����ò��Ϸ�!");
                //DialogResult = DialogResult.Cancel;
                return;
            }
            if (!checkBoxRegion.Checked && comboBoxRegion.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ��������");
                return;

            }
            if (!checkBoxRoad.Checked && comboBoxRoad.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ�����");
                return;

            }
            if (!checkBoxVehicle.Checked && comboBoxVehicleType.SelectedIndex == -1)
            {
                MessageBox.Show("��ѡ����");
                return;

            }
            DialogResult = DialogResult.OK;

        }      
        private void LoadRoadList()
        {
            string strId = ((DataRowView)comboBoxRegion.SelectedItem).Row["RegionId"].ToString();
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    conn.ConnectionString = "Data Source=(local);Initial Catalog=Project;Integrated Security=True";
                    conn.Open();
                    if (conn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("�������ݿ�ʧ��!");
                        return;
                    }
                    DataSet ds = new DataSet();
                    string strSql = "select Name from RoadCrossing where RegionId=" + strId;
                    SqlDataAdapter daRoad = new SqlDataAdapter(strSql, conn);
                    daRoad.Fill(ds, "RoadCrossing");
                    comboBoxRoad.DataSource = ds.Tables[0];
      
                    comboBoxRoad.DisplayMember = "Name";
                    comboBoxRoad.ValueMember = "Name";

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("���ִ���: " + ex.Message + "���޸�����ѡ��������!");

                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }

        private void comboBoxRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadRoadList();
        }

        private void checkBoxRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRegion.Checked)
            {
                comboBoxRegion.Enabled = false;   
            }
            else
            {
                comboBoxRegion.Enabled = true;
            }
          
        }

        private void checkBoxRoad_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRoad.Checked)
            {
                comboBoxRoad.Enabled = false;
            }
            else
            {
                comboBoxRoad.Enabled = true;
            }     
            
        }

        private void checkBoxVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVehicle.Checked)
            {
                comboBoxVehicleType.Enabled = false;
            }
            else
            {
                comboBoxVehicleType.Enabled = true;
            }
            
        }
        public string ReturnQuerySqlString()
        {//���ز�ѯ�ַ���
            string strSql;
            strSql = "RecordTime>='" + dateTimePickerBeginTime.Text
            + "' and RecordTime<='" + dateTimePickerEndTime.Text + "' ";
            if (!checkBoxRegion.Checked)
            {
                string strName = ((DataRowView)comboBoxRegion.SelectedItem).Row["RegionName"].ToString();
                strSql += "and RegionName='" + strName+"' ";
            }
            if (!checkBoxRoad.Checked)
            {
                string strName = ((DataRowView)comboBoxRoad.SelectedItem).Row["Name"].ToString();
                strSql += "and Expr1='" + strName+"' ";
            }
            if (!checkBoxVehicle.Checked)
            {
                string strType = ((DataRowView)comboBoxVehicleType.SelectedItem).Row["Name"].ToString();
                strSql += "and Name='" + strType+"'";
            }
            return strSql;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}