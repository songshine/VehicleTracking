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
            // TODO: 这行代码将数据加载到表“projectDataSet14.VehicleType”中。您可以根据需要移动或移除它。
            this.vehicleTypeTableAdapter1.Fill(this.projectDataSet14.VehicleType);
            // TODO: 这行代码将数据加载到表“projectDataSet13.Region”中。您可以根据需要移动或移除它。
            this.regionTableAdapter1.Fill(this.projectDataSet13.Region);
         
            LoadRoadList();
         

        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (dateTimePickerBeginTime.Value >= dateTimePickerEndTime.Value)
            {
                MessageBox.Show("时间设置不合法!");
                //DialogResult = DialogResult.Cancel;
                return;
            }
            if (!checkBoxRegion.Checked && comboBoxRegion.SelectedIndex == -1)
            {
                MessageBox.Show("请选择区或县");
                return;

            }
            if (!checkBoxRoad.Checked && comboBoxRoad.SelectedIndex == -1)
            {
                MessageBox.Show("请选择道口");
                return;

            }
            if (!checkBoxVehicle.Checked && comboBoxVehicleType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择车型");
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
                        MessageBox.Show("连接数据库失败!");
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
                    MessageBox.Show("出现错误: " + ex.Message + "请修改搜索选项再重试!");

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
        {//返回查询字符串
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