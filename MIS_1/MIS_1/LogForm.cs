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
            // TODO: 这行代码将数据加载到表“projectDataSet8.Manager”中。您可以根据需要移动或移除它。
            this.managerTableAdapter2.Fill(this.projectDataSet8.Manager);
           

        }

        private void btnLogLog_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            if (this.textBoxLogPassword.Text == "")
            {
                MessageBox.Show("请输入密码!");
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
            MessageBox.Show("登录失败!");
        }

        private void btnLogCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}