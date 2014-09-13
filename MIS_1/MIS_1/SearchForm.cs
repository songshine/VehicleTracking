using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
namespace MIS_1
{
    public partial class VehicleSearchForm : Form
    {
        SqlConnection conn = new SqlConnection();//���ݿ�����
        private int nRecorders = 0;//��ǰͳ�Ƶļ�¼����
        DataSet myDS = new DataSet();
        VehicleSet vs = new VehicleSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public VehicleSearchForm()
        {
            InitializeComponent();
        }

        private void VehicleSearchForm_Load(object sender, EventArgs e)
        {
            // TODO: ���д��뽫���ݼ��ص���projectDataSet12.VehicleInfo���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.vehicleInfoTableAdapter.Fill(this.projectDataSet12.VehicleInfo);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet12.VehicleInfo���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.vehicleInfoTableAdapter.Fill(this.projectDataSet12.VehicleInfo);
           
            InitVehicleInfoList();
            LinkDataBase();
            LoadTreeView();
          
        }
        public void ReloadDialog()
        {
            InitVehicleInfoList();
            LoadTreeView();
        }
        private void linkLabelVehicle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {//��Ӧ�������������������¼�
            VehicleQueryOptionForm vqo = new VehicleQueryOptionForm();
            if (vqo.ShowDialog() == DialogResult.OK)
            {
                string strSql = vqo.ReturnQuerySqlString();
                SearchWithCondition(strSql);
            }
        }

        private void btnVehicleSearch_Click(object sender, EventArgs e)
        {//����������ť

            SearchAboutStr(textVehicleSearch.Text);
        }
        public void SearchAboutStr(string str)
        {//���ݸ����ؼ�������

            if (str == "*")
            {
                this.projectDataSet12.VehicleInfo.DefaultView.RowFilter = null;
                InitVehicleInfoList();
                //SearchWithCondition("select * from VehicleInfo");
                return;
            }
            if (str == "")
            {
                MessageBox.Show("������Ҫ�����Ĺؼ���!");
                return;
            }
            DataView dv = this.projectDataSet12.VehicleInfo.DefaultView;
            if (dv != null)
            {
                dv.RowFilter = "Name='" + str + "' or Expr1='" +
                    str + "' or RegionName='" + str +"'";

            }
            dataGridView1.DataSource = dv;
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);
            }
            myDS.Tables.Add(dv.ToTable());
            nRecorders = dv.Count;
            ShowStatisticInfo();
        }
        public void DeleteAllSelected()
        {//ɾ������ѡ�е���
            string str;
            int index = 0;//��ͷҪ���ڵ�������
            int rowCount = dataGridView1.SelectedRows.Count;
            if (rowCount == 0)
            {
                return;
            }
            
            str = "ȷ��ɾ����ǰѡ�е�" + rowCount + "����?";
            DialogResult result = MessageBox.Show(str, "ɾ��", MessageBoxButtons.OKCancel);
            index = dataGridView1.SelectedRows[rowCount - 1].Index - 1;
            if (index < 0)
                index = 0;          
  
            if (result == DialogResult.OK)
            {

                for (int i = rowCount-1; i >= 0; i--)
                {
                    try
                    {
                        str = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                    }
                    catch (NullReferenceException)
                    {
                        
                            continue;
                    }
                    if (str != "")
                    {
                        DeleteARow(str);
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                    }

                }               

                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
                nRecorders -= dataGridView1.SelectedRows.Count;
                ShowStatisticInfo();


            }
        }
        private void DeleteARow(string id)
        {//�����ݿ���ɾ��id��Ӧһ����¼
            vs.DeleteVehicleInfo(id);
        }
        private void LinkDataBase()
        {//�������ݿ�
            try
            {
                conn.ConnectionString = "Data Source=(local);Initial Catalog=Project;Integrated Security=True";
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("�������ݿ�ʧ��!");
                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("���ִ���:{0}" + ex.Message);
                Close();
            }

        }
        public void SearchWithCondition(string strSql)
        {//��������������
            DataView dv = this.projectDataSet12.VehicleInfo.DefaultView;
            if (dv != null)
            {
                dv.RowFilter = strSql;

            }
            dataGridView1.DataSource = dv;
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);               
            }
            myDS.Tables.Add(dv.ToTable());
            nRecorders = dv.Count;
            ShowStatisticInfo();
        }
        public void ChangeStatisticDS(string strSql)
        {//����SQL���ı�����ͳ�Ƶ�����
            DataView dv = this.projectDataSet12.VehicleInfo.DefaultView; 
            if (dv != null)
            {
                dv.RowFilter = strSql;

            }
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);
            }
            myDS.Tables.Add(dv.ToTable());
        }
        private void InitVehicleInfoList()
        {//�������ݿ������ݵ��б���
            // TODO: ���д��뽫���ݼ��ص���projectDataSet12.VehicleInfo���С������Ը�����Ҫ�ƶ����Ƴ�����
            //this.vehicleInfoTableAdapter11.Fill(this.projectDataSet12.VehicleInfo);
            // TODO: ���д��뽫���ݼ��ص���projectDataSet15.VehicleInfo���С������Ը�����Ҫ�ƶ����Ƴ�����
            this.vehicleInfoTableAdapter.Fill(this.projectDataSet12.VehicleInfo);
            DataTable dt = new DataTable();
            //���Ʊ��е�����
            dt = this.projectDataSet12.VehicleInfo.Copy();
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);
            }
            myDS.Tables.Add(dt);

            nRecorders = this.projectDataSet12.VehicleInfo.Count;
            ShowStatisticInfo();
            //MessageBox.Show("Hello!");

        }

        public void StatisticAndDraw()
        {//ͳ����ͼ
            StatisticForm sf = new StatisticForm();  
            sf.LoadCrystalData(myDS);
            sf.Show();
        }

       
        private void ShowStatisticInfo()
        {//��ʾ��ʾ��Ϣ
            string info = "                                         ��ǰ��¼����Ϊ�� " + nRecorders
                        +"\r\n                                      ����*����������ʾȫ��!";
            info += "˫����Ӧ����ʾͼƬ!";
            label1.Text = info;
        }

       
        public void GoToFirstRecord()
        {
            //ָ���һ����¼
            int index = 0;//��ͷҪ���ڵ�������
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            this.dataGridView1.Rows[index].Cells[0].Selected = false;
        }
        public void GotoLastRecord()
        {
            //ָ�����һ����¼
            int index = this.dataGridView1.Rows.Count - 2;//��ͷҪ���ڵ�������
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            this.dataGridView1.Rows[index].Cells[0].Selected = false;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {//˫�������ʾͼƬ
            string strId = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            strId.Trim();
            string strSql = "select Picture from Vehicle where Id=" + strId;
            SqlCommand cmd = new SqlCommand(strSql, conn);
            cmd.Connection = conn;

            byte[] b = (byte[])cmd.ExecuteScalar();
            if (b.Length > 0)
            {
                MemoryStream stream = new MemoryStream(b, true);
                //stream.Write(b, 0, b.Length);
                Image ie = new Bitmap(stream);
               
                
               
                //��ʾͼƬ
               
                PicShowForm psf = new PicShowForm();
                psf.SetImage(ie);
                psf.Show();
                stream.Close();
               
            }
            //conn.Close();
            
        }
        private void LoadTreeView()
        {//���������б�
            treeView1.Nodes.Clear();
            InsertACityNode(treeView1);
            InsertARegionNode(treeView1);
            InsertARoadNode(treeView1);

        }
     
        private void InsertACityNode(TreeView treeView)
        {//����һ����
            TreeNodeCollection nodes = treeView.Nodes;
       
            string strSql = "select CityName from City";
            SqlDataReader sdr = vs.ExeQuerySlqString(null, strSql);
            if (sdr == null)
            {
                sdr.Close();
                return;
            }
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    TreeNode tn = new TreeNode(sdr[0].ToString());
                    treeView.Nodes.Add(tn);
                }
            }
            sdr.Close();
                
        }
        private void InsertARegionNode(TreeView treeView)
        {//����һ��������
            TreeNodeCollection nodes = treeView.Nodes;
            string strSql = "select RegionName,CityName from Region,City where Region.CityId=City.CityId";
            SqlDataReader sdr = vs.ExeQuerySlqString(null,strSql);
            if (sdr == null)
            {
                sdr.Close();
                return;
            }
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    
                    foreach (TreeNode n in nodes)
                    {
                        if(n.Text == sdr[1].ToString())
                        {
                            TreeNode tn = new TreeNode(sdr[0].ToString());
                            n.Nodes.Add(tn);
                            break;
                        }
                    }
                    
                }
            }
            sdr.Close();

        }
        private void InsertARoadNode(TreeView treeView)
        {//����һ������
            TreeNodeCollection nodes = treeView.Nodes;
            string strSql = "select RoadCrossing.Name,RegionName,CityName from RoadCrossing,Region,City where RoadCrossing.RegionId=Region.RegionId and RoadCrossing.CityId=City.CityId ";
            SqlDataReader sdr = vs.ExeQuerySlqString(null, strSql);
            bool isInsert = false;
            if (sdr == null)
            {
                sdr.Close();
                return;
            }
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    foreach (TreeNode n in nodes)
                    {
                        if (n.Text == sdr[2].ToString())
                        {
                            foreach (TreeNode n1 in n.Nodes)
                            {
                                if (n1.Text == sdr[1].ToString())
                                {
                                    TreeNode tn = new TreeNode(sdr[0].ToString());
                                    n1.Nodes.Add(tn);
                                    isInsert = true;
                                    break;
                                }
                            }
                            if (isInsert)
                            {
                                isInsert = false;
                                break;
                            }
                        }
                    }
                }
            }
            sdr.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
                this.SearchAboutStr(e.Node.Text);
        }

        private void VehicleSearchForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
            string strId = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            string strVehicleType = "" + dataGridView1.Rows[e.RowIndex].Cells[1].Value; 
            strId.Trim();
            strVehicleType.Trim();
            if (strVehicleType == "")
                return;
            string strSql1 = "select Id from vehicletype where name='" + strVehicleType+"'";
            SqlCommand cmd1 = new SqlCommand(strSql1, conn);
            cmd1.Connection = conn;
            SqlDataReader sdr = cmd1.ExecuteReader();
            
            if (!sdr.HasRows)
            {
                MessageBox.Show("�������Ʋ��Ϸ�!");
                return;
            }
            sdr.Read();
            string strSql2 = "update vehicle set TypeId ="+sdr[0].ToString()+" where Id=" + strId;
            SqlCommand cmd2 = new SqlCommand(strSql2, conn);
            cmd2.Connection = conn;
            sdr.Close();
            cmd2.ExecuteNonQuery();
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
                return;

        }

       

        
      

     
    }
}