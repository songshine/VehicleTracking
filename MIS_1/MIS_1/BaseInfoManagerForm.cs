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
            // TODO: ���д��뽫���ݼ��ص���projectDataSet2.City���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet3.City���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.cityTableAdapter.Fill(this.projectDataSet3.City);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet5.Region���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.regionTableAdapter.Fill(this.projectDataSet5.Region);
            // TODO:���س��͵��б���
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
            InitPageDialog();//��ʼ��
            
             
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
            
            LoadRoadInfo();//���ص�����Ϣ
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
        /////////////////////////////////////////////��һҳ////////////////////////

        private void ShowVTypeRemindInfo()
        {
            this.textBoxVTypeRemind.Text = "  ɾ��ʱ��ֱ��ѡ�к��ɾ����ť;\r\n  �޸�ʱ��ѡ����Ҫ�޸ĵ��Ȼ��д���޸ĺ�ĳ�����������޸İ�ť;\r\n  ���ʱ��ֱ��д���µĳ�������Ȼ������Ӱ�ť!";

        }
        private void ShowRegionRemindInfo()
        {
            this.textBoxRoadRemindInfo.Text = "����µĵ��ڵ����ݿ��У�\r\n�����޸�ɾ�����е����ݿ���������йص���Ϣ!\r\n������ɺ󣬵���˵��е�ˢ�°�ť����������ͼ�в鿴";
        }
        private void ShowCityRemindInfo()
        {
            this.textBoxRegionRemindInfo.Text = "����µ����ص����ݿ��У�\r\n�����޸����ݿ������е�������Ϣ.";
        }
        private void ShowRoadRemindInfo()
        {
            this.textBoxCityRemindInfo.Text = "����е����ݿ��У������޸����е�����Ϣ.";
        }
        private void listBoxVehicleType_SelectedIndexChanged(object sender, EventArgs e)
        {//���б��е�ѡ��ͱ仯ʱִ��
            ShowTheSelectedItemInTheText();
        }
        private void ShowTheSelectedItemInTheText()
        {//��ѡ�еĳ���������ԭ�����ı�������ʾ����
            string str = GetSelectedString();
            this.textBoxVehicleTypeOld.Text = str;
        }
        private string GetSelectedString()
        {//�ӳ����б��л�ȡѡ����ַ���
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
        {//����ɾ�����Ͱ�ť
            
            string str = GetSelectedString();
            if (str != null)
            {
                if (DialogResult.Cancel == MessageBox.Show("ȷʵ�ܹ�Ҫɾ��ѡ�еĳ�����", "ɾ��", MessageBoxButtons.OKCancel))
                {
                    return;
                }
                if (vs.DeleteVehicleType(str))
                {
                    MessageBox.Show("ɾ�����ͳɹ�!");
                    textBoxVehicleTypeNew.Text = null;
                }
                else
                {
                    MessageBox.Show("ɾ������ʧ��!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("����ѡ��Ҫɾ������!");
                return;
            }
            //���¼����б���е�����
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }

        private void btnVTypeAlter_Click(object sender, EventArgs e)
        {//�����޸ĳ���
            string strOld = GetSelectedString();
            string strNew = textBoxVehicleTypeNew.Text;
            if (strOld == null)
            {
                MessageBox.Show("����ѡ��Ҫɾ������!");
                return;
            }
            if (strNew == "")
            {
                MessageBox.Show("�������޸ĺ�ĳ�������");
                return;
            }
            if (strNew == "�ڴ������޸ĺ����Ҫ��ӵ��³���")
            {
                MessageBox.Show("�������޸ĺ�ĳ�����!");
                return;
            }
            string strInfo = "ȷ��Ҫ������" + strOld + "�޸ĳ�" + strNew + "��?";
            MessageBoxButtons button = MessageBoxButtons.YesNo;
            if (DialogResult.Yes != MessageBox.Show(strInfo, "��ʾ", button))
                return;
            if (vs.AlterVehicleType(strOld, strNew))
            {
                MessageBox.Show("�޸ĳ��ͳɹ�");
                textBoxVehicleTypeNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
            }
            else
            {
                MessageBox.Show("�޸ĳ���ʧ��");
                textBoxVehicleTypeNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
                return;
            }
            //���¼����б���е�����
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }

        private void btnVTypeAdd_Click(object sender, EventArgs e)
        {//������ӳ���
            string strNew = textBoxVehicleTypeNew.Text;
            if (strNew == "")
            {
                MessageBox.Show("�������Ʋ�����Ϊ��!");
                return;
            }
            if (strNew == "�ڴ������޸ĺ����Ҫ��ӵ��³���")
            {
                MessageBox.Show("������Ҫ��ӵĳ�����!");
                return;
            }
            if (vs.AddVheicleType(strNew))
            {
                MessageBox.Show("��ӳ��ͳɹ�");
                textBoxVehicleTypeNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
            }
            else
            {
                MessageBox.Show("��ӳ���ʧ��");
                textBoxVehicleTypeNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
                return;
            }

            //���¼����б���е�����
            this.vehicleTypeTableAdapter.Fill(this.projectDataSet17.VehicleType);
        }
        
        private void BaseInfoManagerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
        private void btnRoadAdd_Click(object sender, EventArgs e)
        {//�����ӵ��ں�
            BaseRoadInfoForm brif = new BaseRoadInfoForm();
            brif.DoThingBeforeLoad("���");
            if (brif.ShowDialog() == DialogResult.OK)
            {
                ArrayList al = brif.ReturnInfo();

                if (rs.AddRoad(al[0].ToString(), al[1].ToString(), al[2].ToString()))
                {
                    LoadRoadInfo();//���¼��ص�����Ϣ
                    MessageBox.Show("��ӵ��ڳɹ�!");
                    
                }
            }
        }

        private void btnRoadAlter_Click(object sender, EventArgs e)
        {//����޸ĵ��ں�
            BaseRoadInfoForm brif = new BaseRoadInfoForm();
            brif.DoThingBeforeLoad("�޸�");
            string str1 = null, str2 = null, str3 = null;
            if (listView1.SelectedItems.Count != 0)
            {
                str1 = listView1.SelectedItems[0].SubItems[1].Text;
                str2 = listView1.SelectedItems[0].SubItems[2].Text;
                str3 = listView1.SelectedItems[0].SubItems[3].Text;
            }
            else
            {
                MessageBox.Show("����ѡ��Ҫ�޸ĵ���!");
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
                    LoadRoadInfo();//���¼��ص�����Ϣ
                    MessageBox.Show("�޸ĵ��ڳɹ�!");
                }
            }
        }
        private void btnRoadDelete_Click(object sender, EventArgs e)
        {//���ɾ�����ں�
            string str1 = null, str2 = null, str3 = null;
            if (listView1.SelectedItems.Count != 0)
            {
                str1 = listView1.SelectedItems[0].SubItems[1].Text;
                str2 = listView1.SelectedItems[0].SubItems[2].Text;
                str3 = listView1.SelectedItems[0].SubItems[3].Text;
            }
            else
            {
                MessageBox.Show("����ѡ��Ҫɾ������!");
                return;
            }


            if (rs.DeleteRoad(str1, str2, str3))
            {
                LoadRoadInfo();//���¼��ص�����Ϣ
                MessageBox.Show("ɾ�����ڳɹ�!");
            }

        }
        private void LoadRoadInfo()
        {//���ص�����Ϣ
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
                MessageBox.Show("�����ݿ��ж�ȡ������Ϣ����!");
            }

        }
        /////////////////////////����ҳ/////////////////////////

        private void LoadRegionInfo()
        {//����������Ϣ
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
                MessageBox.Show("�����ݿ��ж�ȡ������Ϣ����!");
            }
        }      
        private void btnAddRegion_Click(object sender, EventArgs e)
        {
            btnHideOkButton.Text = "���";
            btnAddRegion.Enabled = false;
            ShowpanelRegion(true);
            
        }
        private void btnDeleteRegion_Click(object sender, EventArgs e)
        {//ɾ������
            string str1 = null;
            if (this.listViewRegion.SelectedItems.Count != 0)
            {
                str1 = this.listViewRegion.SelectedItems[0].SubItems[0].Text;
                
            }
            else
            {
                MessageBox.Show("����ѡ��Ҫɾ������!");
                return;
            }
            if(rs.DeleteRegion(str1))
            {
                MessageBox.Show("ɾ���ɹ�!");
                LoadRegionInfo();
            }
        }
        private void btnAlter_Click(object sender, EventArgs e)
        {
            btnHideOkButton.Text = "�޸�";
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
                MessageBox.Show("������������Ϊ��!");
                return;
            }
            if (btnHideOkButton.Text == "�޸�")
            {
                string str1 = null;
                if (this.listViewRegion.SelectedItems.Count != 0)
                {
                    str1 = this.listViewRegion.SelectedItems[0].SubItems[0].Text;

                }
                else
                {
                    MessageBox.Show("����ѡ��Ҫ�޸ĵ���!");
                    return;
                }
               

                if (rs.AlterRegion(str1, textBoxRegion.Text, strCity))
                {
                    MessageBox.Show("�޸ĳɹ�");
                    LoadRegionInfo();
                    return;
                }
                btnAlter.Enabled = true;
            }
            else
            {
                if (rs.AddRegion(textBoxRegion.Text, strCity))
                {
                    MessageBox.Show("��ӳɹ�!");
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
      
       ////////////////////////////// ����ҳ/////////////////////

        private void btnDeleteCity_Click(object sender, EventArgs e)
        {//����ɾ����
            string strCityName = GetSelectedCityString();
            if (strCityName != null)
            {
                if (rs.DeleteCity(strCityName))
                    MessageBox.Show("ɾ���гɹ�!");              
            }
            else
            {
                MessageBox.Show("����ѡ��Ҫɾ������!");
                return;
            }
            // TODO: ���д��뽫���ݼ��ص���projectDataSet2.City���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
        }

        private void btnAddCity_Click(object sender, EventArgs e)
        {
            string strNew = textBoxCityNew.Text;
            if (strNew == "")
            {
                MessageBox.Show("�����в�����Ϊ��!");
                return;
            }
            if (strNew == "�ڴ������޸ĺ����Ҫ��ӵ��³���")
            {
                MessageBox.Show("�������޸ĺ������!");
                return;
            }
            if (rs.AddCity(strNew))
            {
                MessageBox.Show("����гɹ�");
                textBoxCityNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
            }
            
            // TODO: ���д��뽫���ݼ��ص���projectDataSet2.City���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.cityTableAdapter1.Fill(this.projectDataSet2.City);
        }

        private void btnAlterCity_Click(object sender, EventArgs e)
        {
            string strOld = GetSelectedCityString();
            string strNew = textBoxCityNew.Text;
            if (strOld == null)
            {
                MessageBox.Show("����ѡ��Ҫ�޸ĵ���!");
                return;
            }
            if (strNew == "")
            {
                MessageBox.Show("�������޸ĺ��������");
                return;
            }
            if (strNew == "�ڴ������޸ĺ����Ҫ��ӵ��³���")
            {
                MessageBox.Show("������Ҫ��ӵ�����!");
                return;
            }
            if (rs.AlterCity(strOld, strNew))
            {
                MessageBox.Show("�޸ĳ��ͳɹ�");
                textBoxCityNew.Text = "�ڴ������޸ĺ����Ҫ��ӵ��³���";
            }
          

            // TODO: ���д��뽫���ݼ��ص���projectDataSet2.City���С������Ը�����Ҫ�ƶ����Ƴ�����
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
        {//�������ͱ༭��ʱ
            textBoxVehicleTypeNew.Text = "";
           
        }

        private void textBoxCityNew_Click(object sender, EventArgs e)
        {//�����б༭��ʱ
            textBoxCityNew.Text = "";
        }

       
       
    }
}