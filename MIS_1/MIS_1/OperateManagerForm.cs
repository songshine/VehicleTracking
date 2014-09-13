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
                case "ɾ��":
                    DeleteManager();
                    break;
                case "���":
                    if (this.textBoxManagerName.Text == "" || this.textBoxPassword.Text == "")
                    {
                        MessageBox.Show("���ܳ��ֿհ�!");
                        return;
                    }
                    AddManager();
                    break;
                case "�޸�":
                    if (this.textBoxManagerName.Text == "" || this.textBoxPassword.Text == "")
                    {
                        MessageBox.Show("���ܳ��ֿհ�!");
                        return;
                    }
                    AlterManager();
                    break;
            }
         
           
        }
     
        private void DeleteManager()
        {//ɾ��
            if(ms.DeleteManager(textBoxManagerId.Text))
            {
                 MessageBox.Show("ɾ���ɹ�!");
                DialogResult = DialogResult.OK;
            }
            
            
        }
        private void AddManager()
        {//���
            if(ms.AddManager(textBoxManagerName.Text,textBoxPassword.Text,dateTimePicker.Text))
            {
                MessageBox.Show("��ӹ���Ա�ɹ�!");
                DialogResult = DialogResult.OK;
            }
          
            
        }
        private void AlterManager()
        {//�޸�
            if(ms.AlterManager(textBoxManagerId.Text,textBoxManagerName.Text,textBoxPassword.Text,dateTimePicker.Text))
            {
                MessageBox.Show("�޸ĳɹ�!");
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