using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MIS_1
{
    public partial class BaseRoadInfoForm : Form
    {
        public BaseRoadInfoForm()
        {
            InitializeComponent();
        }
        public void DoThingBeforeLoad(string str)
        {
            btnOk.Text = str;
            this.Text = str;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (textBoxRoad.Text == null)
            {
                MessageBox.Show("������������Ϊ��!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void BaseRoadInfoForm_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���projectDataSet3.City���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.cityTableAdapter.Fill(this.projectDataSet3.City);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet5.Region���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.regionTableAdapter.Fill(this.projectDataSet5.Region);

        }
        public ArrayList ReturnInfo()
        {
            DataRowView drvRegion = (DataRowView)this.comboBoxRegion.SelectedItem;
            DataRowView drvCity = (DataRowView)this.comboBoxCity.SelectedItem;
            string strRegion = drvRegion["RegionName"].ToString();
            string strCity = drvCity["CityName"].ToString();
            string strRoad = this.textBoxRoad.Text;
            ArrayList al = new ArrayList();
            al.Add(strRoad);
            al.Add(strRegion);
            al.Add(strCity);

            return al;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}