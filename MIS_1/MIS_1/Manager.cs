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
        {//��ӹ���Ա
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "��ӹ���Ա";
            omf.AlterButtonOk("���");
            if (omf.ShowDialog() == DialogResult.OK)
            {
                LoadManagerList();
            }
        }
        public void AlterManager()
        {//�޸Ĺ���Ա��Ϣ
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("����Ҫ��ѡ��Ҫ�޸ĵ���!");
                return;
            }
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "�޸Ĺ���Ա��Ϣ";
            omf.AlterButtonOk("�޸�");
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
        {//ɾ������Ա
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("����Ҫ��ѡ��Ҫɾ������!");
                return;
            }
            OperateManagerForm omf = new OperateManagerForm();
            omf.Text = "ɾ������Ա";
            omf.AlterButtonOk("ɾ��");
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
            // TODO: ���д��뽫���ݼ��ص���projectDataSet.Manager���С������Ը�����Ҫ�ƶ����Ƴ�����
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
        {//��������
           
            string strSql = "select ManagerId,ManagerName,Password,JoinTime from Manager where  ManagerName='" +this.toolStripTextBoxFind.Text + "'";

            DataSet ds = ms.ExeQuerySqlString(null, strSql, "Manager");
            dataGridView1.AutoGenerateColumns = true;
            managerBindingSource.DataSource = ds;
            managerBindingSource.DataMember = "Manager";          
            
        }
    }
}