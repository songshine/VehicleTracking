using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
namespace MIS_1
{
    public partial class BaseInfoManagerForm : Form
    {


        RoadSet rs = new RoadSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        VehicleSet vs = new VehicleSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public BaseInfoManagerForm()
        {
            InitializeComponent();
        }

        private void BaseInfoManagerForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“projectDataSet2.City”中。您可以根据需要移动或移除它。
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
            // TODO: 这行代码将数据加载到表“projectDataSet3.City”中。您可以根据需要移动或移除它。
            this.cityTableAdapter.Fill(this.projectDataSet3.City);
            // TODO: 这行代码将数据加载到表“projectDataSet5.Region”中。您可以根据需要移动或移除它。
            this.regionTableAdapter.Fill(this.projectDataSet5.Region);
            // TODO:加载车型到列表中
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
            InitPageDialog();//初始化
            
             
        }
        public void SelectPageToOpen(int index)
        {
            this.tabControl.SelectedIndex = index;
        }
        private void  InitPageDialog()
        {
            ShowTheSelectedItemInTheText();
            ShowCityRemindInfo();
            ShowRegionRemindInfo();
            ShowRoadRemindInfo();
            ShowVTypeRemindInfo();
            
            LoadRoadInfo();//加载道口信息
            LoadRegionInfo();
            ShowpanelRegion(false);
        }
       
        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        /////////////////////////////////////////////第一页////////////////////////

        private void ShowVTypeRemindInfo()
        {
            this.textBoxVTypeRemind.Text = "  删除时，直接选中后点删除按钮;\r\n  修改时，选中需要修改的项，然后写入修改后的车型名，点击修改按钮;\r\n  添加时，直接写入新的车型名，然后点击添加按钮!";

        }
        private void ShowRegionRemindInfo()
        {
            this.textBoxRoadRemindInfo.Text = "添加新的道口到数据库中，\r\n或者修改删除现有的数据库中与道口有关的信息!\r\n操作完成后，点击菜单中的刷新按钮即可在树形图中查看";
        }
        private void ShowCityRemindInfo()
        {
            this.textBoxRegionRemindInfo.Text = "添加新的区县到数据库中，\r\n或者修改数据库中现有的区县信息.";
        }
        private void ShowRoadRemindInfo()
        {
            this.textBoxCityRemindInfo.Text = "添加市到数据库中，或者修改现有的市信息.";
        }
        private void listBoxVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {//当列表中的选项发送变化时执行
            ShowTheSelectedItemInTheText();
        }
        private void ShowTheSelectedItemInTheText()
        {//将选中的车型名称在原车型文本框中显示出来
            string str = GetSelectedString();
            this.textBoxVehicleTypeOld.Text = str;
        }
        private string GetSelectedString()
        {//从车型列表中获取选择的字符串
            DataRowView drv = (DataRowView)this.listBoxVehicleType.SelectedItem;
            if (drv != null)
            {
                string str = drv.Row["Name"].ToString();
                str.Trim();
                return str;
            }
            return null;
        }
        private void btnVTypeDelete_Click(object sender, EventArgs e)
        {//单击删除车型按钮
            
            string str = GetSelectedString();
            if (str != null)
            {
                if (DialogResult.Cancel == MessageBox.Show("确实能够要删除选中的车型吗", "删除", MessageBoxButtons.OKCancel))
                {
                    return;
                }
                if (vs.DeleteVehicleType(str))
                {
                    MessageBox.Show("删除车型成功!");
                    textBoxVehicleTypeNew.Text = null;
                }
                else
                {
                    MessageBox.Show("删除车型失败!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先选中要删除的项!");
                return;
            }
            //从新加载列表框中的数据
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }

        private void btnVTypeAlter_Click(object sender, EventArgs e)
        {//单击修改车型
            string strOld = GetSelectedString();
            string strNew = textBoxVehicleTypeNew.Text;
            if (strOld == null)
            {
                MessageBox.Show("请先选中要删除的项!");
                return;
            }
            if (strNew == "")
            {
                MessageBox.Show("请输入修改后的车型名称");
                return;
            }
            if (strNew == "在此输入修改后或需要添加的新车型")
            {
                MessageBox.Show("请输入修改后的车型名!");
                return;
            }
            string strInfo = "确定要将车型" + strOld + "修改成" + strNew + "吗?";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            if (DialogResult.Yes != MessageBox.Show(strInfo, "提示", button))
                return;
            if (vs.AlterVehicleType(strOld, strNew))
            {
                MessageBox.Show("修改车型成功");
                textBoxVehicleTypeNew.Text = "在此输入修改后或需要添加的新车型";
            }
            else
            {
                MessageBox.Show("修改车型失败");
                textBoxVehicleTypeNew.Text = "在此输入修改后或需要添加的新车型";
                return;
            }
            //从新加载列表框中的数据
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }

        private void btnVTypeAdd_Click(object sender, EventArgs e)
        {//单击添加车型
            string strNew = textBoxVehicleTypeNew.Text;
            if (strNew == "")
            {
                MessageBox.Show("车型名称不允许为空!");
                return;
            }
            if (strNew == "在此输入修改后或需要添加的新车型")
            {
                MessageBox.Show("请输入要添加的车型名!");
                return;
            }
            if (vs.AddVheicleType(strNew))
            {
                MessageBox.Show("添加车型成功");
                textBoxVehicleTypeNew.Text = "在此输入修改后或需要添加的新车型";
            }
            else
            {
                MessageBox.Show("添加车型失败");
                textBoxVehicleTypeNew.Text = "在此输入修改后或需要添加的新车型";
                return;
            }

            //从新加载列表框中的数据
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }
        
        private void BaseInfoManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void btnRoadAdd_Click(object sender, EventArgs e)
        {//点击添加道口后
            BaseRoadInfoForm brif = new BaseRoadInfoForm();
            brif.DoThingBeforeLoad("添加");
            if (brif.ShowDialog() == DialogResult.OK)
            {
                ArrayList al = brif.ReturnInfo();

                if (rs.AddRoad(al[0].ToString(), al[1].ToString(), al[2].ToString()))
                {
                    LoadRoadInfo();//重新加载道口信息
                    MessageBox.Show("添加道口成功!");
                    
                }
            }
        }

        private void btnRoadAlter_Click(object sender, EventArgs e)
        {//点击修改道口后
            BaseRoadInfoForm brif = new BaseRoadInfoForm();
            brif.DoThingBeforeLoad("修改");
            string str1 = null, str2 = null, str3 = null;
            if (listView1.SelectedItems.Count != 0)
            {
                str1 = listView1.SelectedItems[0].SubItems[1].Text;
                str2 = listView1.SelectedItems[0].SubItems[2].Text;
                str3 = listView1.SelectedItems[0].SubItems[3].Text;
            }
            else
            {
                MessageBox.Show("请先选中要修改的项!");
                return;
            }
            brif.textBoxRoad.Text = str1;
            brif.comboBoxRegion.Text = str2;
            brif.comboBoxCity.Text = str3;
            string strId = rs.GetRoadId(str1, str2, str3);
            if (brif.ShowDialog() == DialogResult.OK)
            {
                ArrayList al = brif.ReturnInfo();

                if (rs.AlterRoad(strId, al[0].ToString(), al[1].ToString(), al[2].ToString()))
                {
                    LoadRoadInfo();//重新加载道口信息
                    MessageBox.Show("修改道口成功!");
                }
            }
        }
        private void btnRoadDelete_Click(object sender, EventArgs e)
        {//点击删除道口后
            string str1 = null, str2 = null, str3 = null;
            if (listView1.SelectedItems.Count != 0)
            {
                str1 = listView1.SelectedItems[0].SubItems[1].Text;
                str2 = listView1.SelectedItems[0].SubItems[2].Text;
                str3 = listView1.SelectedItems[0].SubItems[3].Text;
            }
            else
            {
                MessageBox.Show("请先选中要删除的项!");
                return;
            }


            if (rs.DeleteRoad(str1, str2, str3))
            {
                LoadRoadInfo();//重新加载道口信息
                MessageBox.Show("删除道口成功!");
            }

        }
        private void LoadRoadInfo()
        {//加载道口信息
            SqlCommand cmd = new SqlCommand();
            string strSql = "Select Id,Name,RegionName,CityName from RoadInfo";
            SqlDataReader sdr = rs.ExeQuerySlqString(null, strSql);
            if (sdr != null)
            {
                listView1.Items.Clear();
                while (sdr.Read())
                {
                    ListViewItem lvi = new ListViewItem(new string[] { sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString(), sdr[3].ToString() });
                    this.listView1.Items.Add(lvi);
                }
                sdr.Close();
            }
            else
            {
                MessageBox.Show("从数据库中读取道口信息出错!");
            }

        }
        /////////////////////////第三页/////////////////////////

        private void LoadRegionInfo()
        {//加载区县信息
            string strSql = "select RegionId,RegionName,CityName from Region,City where Region.CityId=City.CityId";
            SqlDataReader sdr =rs.ExeQuerySlqString(null, strSql);
            if (sdr != null)
            {
                listViewRegion.Items.Clear();
                while (sdr.Read())
                {
                    ListViewItem lvi = new ListViewItem(new string[] { sdr[0].ToString(), sdr[1].ToString(), sdr[2].ToString() });
                    this.listViewRegion.Items.Add(lvi);
                }
                sdr.Close();
            }
            else
            {
                MessageBox.Show("从数据库中读取道口信息出错!");
            }
        }      
        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            btnHideOkButton.Text = "添加";
            btnAddRegion.Enabled = false;
            ShowpanelRegion(true);
            
        }
        private void btnDeleteRegion_Click(object sender, EventArgs e)
        {//删除区县
            string str1 = null;
            if (this.listViewRegion.SelectedItems.Count != 0)
            {
                str1 = this.listViewRegion.SelectedItems[0].SubItems[0].Text;
                
            }
            else
            {
                MessageBox.Show("请先选中要删除的项!");
                return;
            }
            if(rs.DeleteRegion(str1))
            {
                MessageBox.Show("删除成功!");
                LoadRegionInfo();
            }
        }
        private void btnAlter_Click(object sender, EventArgs e)
        {
            btnHideOkButton.Text = "修改";
            btnAlter.Enabled = false;
            ShowpanelRegion(true);
            
        }
        private void btnHideOkButton_Click(object sender, EventArgs e)
        {
             DataRowView drv = (DataRowView)this.comboBoxCity.SelectedItem;
             string strCity = drv.Row["CityName"].ToString();
             strCity.Trim();
            if(textBoxRegion.Text == "")
            {
                MessageBox.Show("区县名不允许为空!");
                return;
            }
            if (btnHideOkButton.Text == "修改")
            {
                string str1 = null;
                if (this.listViewRegion.SelectedItems.Count != 0)
                {
                    str1 = this.listViewRegion.SelectedItems[0].SubItems[0].Text;

                }
                else
                {
                    MessageBox.Show("请先选中要修改的项!");
                    return;
                }
               

                if (rs.AlterRegion(str1, textBoxRegion.Text, strCity))
                {
                    MessageBox.Show("修改成功");
                    LoadRegionInfo();
                    return;
                }
                btnAlter.Enabled = true;
            }
            else
            {
                if (rs.AddRegion(textBoxRegion.Text, strCity))
                {
                    MessageBox.Show("添加成功!");
                    LoadRegionInfo();
                    return;
                }
                btnAddRegion.Enabled = false;
            }
        }

        private void ShowpanelRegion(bool bIsReally)
        {
            if (bIsReally)
                this.panelRegion.Visible = true;
            else
                this.panelRegion.Visible = false;
        }
      
       ////////////////////////////// 第四页/////////////////////

        private void btnDeleteCity_Click(object sender, EventArgs e)
        {//单击删除市
            string strCityName = GetSelectedCityString();
            if (strCityName != null)
            {
                if (rs.DeleteCity(strCityName))
                    MessageBox.Show("删除市成功!");              
            }
            else
            {
                MessageBox.Show("请先选中要删除的项!");
                return;
            }
            // TODO: 这行代码将数据加载到表“projectDataSet2.City”中。您可以根据需要移动或移除它。
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
        }

        private void btnAddCity_Click(object sender, EventArgs e)
        {
            string strNew = textBoxCityNew.Text;
            if (strNew == "")
            {
                MessageBox.Show("车型市不允许为空!");
                return;
            }
            if (strNew == "在此输入修改后或需要添加的新车型")
            {
                MessageBox.Show("请输入修改后的市名!");
                return;
            }
            if (rs.AddCity(strNew))
            {
                MessageBox.Show("添加市成功");
                textBoxCityNew.Text = "在此输入修改后或需要添加的新车型";
            }
            
            // TODO: 这行代码将数据加载到表“projectDataSet2.City”中。您可以根据需要移动或移除它。
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
        }

        private void btnAlterCity_Click(object sender, EventArgs e)
        {
            string strOld = GetSelectedCityString();
            string strNew = textBoxCityNew.Text;
            if (strOld == null)
            {
                MessageBox.Show("请先选中要修改的项!");
                return;
            }
            if (strNew == "")
            {
                MessageBox.Show("请输入修改后的市名称");
                return;
            }
            if (strNew == "在此输入修改后或需要添加的新车型")
            {
                MessageBox.Show("请输入要添加的市名!");
                return;
            }
            if (rs.AlterCity(strOld, strNew))
            {
                MessageBox.Show("修改车型成功");
                textBoxCityNew.Text = "在此输入修改后或需要添加的新车型";
            }
          

            // TODO: 这行代码将数据加载到表“projectDataSet2.City”中。您可以根据需要移动或移除它。
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView drv = (DataRowView)this.listBox1.SelectedItem;
            if (drv != null)
            {
                string str = drv.Row["CityName"].ToString();
                str.Trim();
                this.textBoxCityOld.Text = str;
            }
        }
      
        private string GetSelectedCityString()
        {
            DataRowView drv = (DataRowView)this.listBox1.SelectedItem;
            string str = drv.Row["CityName"].ToString();
            str.Trim();
            return str;
        }

        private void textBoxVehicleTypeNew_Click(object sender, EventArgs e)
        {//单击车型编辑框时
            textBoxVehicleTypeNew.Text = "";
           
        }

        private void textBoxCityNew_Click(object sender, EventArgs e)
        {//单击市编辑框时
            textBoxCityNew.Text = "";
        }

       
       
    }
}