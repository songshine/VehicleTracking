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
    public partial class Manager : Form
    {
        ManagerSet ms = new ManagerSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public Manager()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {          
            LoadManagerList();
           
        }
        public void AddManager()
        {//添加管理员
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "添加管理员";
            omf.AlterButtonOk("添加");
            if (omf.ShowDialog() == DialogResult.OK)
            {
                LoadManagerList();
            }
        }
        public void AlterManager()
        {//修改管理员信息
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("您需要先选中要修改的项!");
                return;
            }
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "修改管理员信息";
            omf.AlterButtonOk("修改");
            omf.InitAllText(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                dataGridView1.CurrentRow.Cells[3].Value.ToString());
            if (omf.ShowDialog() == DialogResult.OK)
            {
                LoadManagerList();
            }
        }

        public void DeleteManager()
        {//删除管理员
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("您需要先选中要删除的项!");
                return;
            }
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "删除管理员";
            omf.AlterButtonOk("删除");
            omf.InitAllText(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                dataGridView1.CurrentRow.Cells[3].Value.ToString());
            if(omf.ShowDialog() == DialogResult.OK)
            {
                LoadManagerList();
            }
        }

        private void LoadManagerList()
        {
            // TODO: 这行代码将数据加载到表“projectDataSet.Manager”中。您可以根据需要移动或移除它。
            //this.managerTableAdapter.Fill(this.projectDataSet.Manager);
            try
            {
                this.managerTableAdapter.FillBy2(this.projectDataSet.Manager);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }



      

        private void toolStripBtnAddMan_Click(object sender, EventArgs e)
        {
            AddManager();
        }

        private void toolStripBtnDeleteMan_Click(object sender, EventArgs e)
        {
            DeleteManager();
        }

        private void toolStripBtnAlterMan_Click(object sender, EventArgs e)
        {
            AlterManager();
        }

        private void toolStripBtnFind_Click(object sender, EventArgs e)
        {//单击搜索
           
            string strSql = "select ManagerId,ManagerName,Password,JoinTime from Manager where  ManagerName='" +this.toolStripTextBoxFind.Text + "'";

            DataSet ds = ms.ExeQuerySqlString(null, strSql, "Manager");
            dataGridView1.AutoGenerateColumns = true;
            managerBindingSource.DataSource = ds;
            managerBindingSource.DataMember = "Manager";          
            
        }
    }
}