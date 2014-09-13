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
    public partial class OperateManagerForm : Form
    {
        //SqlConnection conn = new SqlConnection();
        ManagerSet ms = new ManagerSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public OperateManagerForm()
        {
            InitializeComponent();
        }
       
        public void AlterButtonOk(string str)
        {
            btnOk.Text = str;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (btnOk.Text)
            {
                case "删除":
                    DeleteManager();
                    break;
                case "添加":
                    if (this.textBoxManagerName.Text == "" || this.textBoxPassword.Text == "")
                    {
                        MessageBox.Show("不能出现空白!");
                        return;
                    }
                    AddManager();
                    break;
                case "修改":
                    if (this.textBoxManagerName.Text == "" || this.textBoxPassword.Text == "")
                    {
                        MessageBox.Show("不能出现空白!");
                        return;
                    }
                    AlterManager();
                    break;
            }
         
           
        }
     
        private void DeleteManager()
        {//删除
            if(ms.DeleteManager(textBoxManagerId.Text))
            {
                 MessageBox.Show("删除成功!");
                DialogResult = DialogResult.OK;
            }
            
            
        }
        private void AddManager()
        {//添加
            if(ms.AddManager(textBoxManagerName.Text,textBoxPassword.Text,dateTimePicker.Text))
            {
                MessageBox.Show("添加管理员成功!");
                DialogResult = DialogResult.OK;
            }
          
            
        }
        private void AlterManager()
        {//修改
            if(ms.AlterManager(textBoxManagerId.Text,textBoxManagerName.Text,textBoxPassword.Text,dateTimePicker.Text))
            {
                MessageBox.Show("修改成功!");
                DialogResult = DialogResult.OK;
            }
            
        }
       public void InitAllText(string id,string name,string pw,string time)
       {
          
           textBoxManagerId.Text = id;
           textBoxManagerName.Text = name;
           textBoxPassword.Text = pw;
           dateTimePicker.Text = time;

       }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OperateManagerForm_Load(object sender, EventArgs e)
        {

        }
    }
    
}