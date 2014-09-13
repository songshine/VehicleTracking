using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        private void LogForm_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���projectDataSet8.Manager���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.managerTableAdapter2.Fill(this.projectDataSet8.Manager);
           

        }

        private void btnLogLog_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            if (this.textBoxLogPassword.Text == "")
            {
                MessageBox.Show("����������!");
                return;
            }
            string str = ((DataRowView)comboBoxLogName.SelectedItem).Row["Password"].ToString();
            if (str == this.textBoxLogPassword.Text)
            {
                this.Close();                
                this.DialogResult = DialogResult.OK;
                return;
            }
            this.textBoxLogPassword.Text = "";
            MessageBox.Show("��¼ʧ��!");
        }

        private void btnLogCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}