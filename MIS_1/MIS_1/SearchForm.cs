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
        SqlConnection conn = new SqlConnection();//数据库连接
        private int nRecorders = 0;//当前统计的记录数量
        DataSet myDS = new DataSet();
        VehicleSet vs = new VehicleSet("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public VehicleSearchForm()
        {
            InitializeComponent();
        }

        private void VehicleSearchForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“projectDataSet12.VehicleInfo”中。您可以根据需要移动或移除它。
            this.vehicleInfoTableAdapter.Fill(this.projectDataSet12.VehicleInfo);
            // TODO: 这行代码将数据加载到表“projectDataSet12.VehicleInfo”中。您可以根据需要移动或移除它。
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
        {//响应单击设置条件搜索的事件
            VehicleQueryOptionForm vqo = new VehicleQueryOptionForm();
            if (vqo.ShowDialog() == DialogResult.OK)
            {
                string strSql = vqo.ReturnQuerySqlString();
                SearchWithCondition(strSql);
            }
        }

        private void btnVehicleSearch_Click(object sender, EventArgs e)
        {//单击搜索按钮

            SearchAboutStr(textVehicleSearch.Text);
        }
        public void SearchAboutStr(string str)
        {//根据给定关键字搜索

            if (str == "*")
            {
                this.projectDataSet12.VehicleInfo.DefaultView.RowFilter = null;
                InitVehicleInfoList();
                //SearchWithCondition("select * from VehicleInfo");
                return;
            }
            if (str == "")
            {
                MessageBox.Show("请输入要搜索的关键字!");
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
        {//删除所有选中的项
            string str;
            int index = 0;//箭头要落在的行索引
            int rowCount = dataGridView1.SelectedRows.Count;
            if (rowCount == 0)
            {
                return;
            }
            
            str = "确定删除当前选中的" + rowCount + "项吗?";
            DialogResult result = MessageBox.Show(str, "删除", MessageBoxButtons.OKCancel);
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
        {//从数据库中删除id对应一条记录
            vs.DeleteVehicleInfo(id);
        }
        private void LinkDataBase()
        {//连接数据库
            try
            {
                conn.ConnectionString = "Data Source=(local);Initial Catalog=Project;Integrated Security=True";
                conn.Open();
                if (conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("连接数据库失败!");
                    Close();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("出现错误:{0}" + ex.Message);
                Close();
            }

        }
        public void SearchWithCondition(string strSql)
        {//设置条件后搜索
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
        {//根据SQL语句改变用来统计的数据
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
        {//加载数据库中数据到列表中
            // TODO: 这行代码将数据加载到表“projectDataSet12.VehicleInfo”中。您可以根据需要移动或移除它。
            //this.vehicleInfoTableAdapter11.Fill(this.projectDataSet12.VehicleInfo);
            // TODO: 这行代码将数据加载到表“projectDataSet15.VehicleInfo”中。您可以根据需要移动或移除它。
            this.vehicleInfoTableAdapter.Fill(this.projectDataSet12.VehicleInfo);
            DataTable dt = new DataTable();
            //复制表中的数据
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
        {//统计作图
            StatisticForm sf = new StatisticForm();  
            sf.LoadCrystalData(myDS);
            sf.Show();
        }

       
        private void ShowStatisticInfo()
        {//显示提示信息
            string info = "                                         当前记录数量为： " + nRecorders
                        +"\r\n                                      输入*后点击搜索显示全部!";
            info += "双击对应行显示图片!";
            label1.Text = info;
        }

       
        public void GoToFirstRecord()
        {
            //指向第一个记录
            int index = 0;//箭头要落在的行索引
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            this.dataGridView1.Rows[index].Cells[0].Selected = false;
        }
        public void GotoLastRecord()
        {
            //指向最后一个记录
            int index = this.dataGridView1.Rows.Count - 2;//箭头要落在的行索引
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            this.dataGridView1.Rows[index].Cells[0].Selected = false;
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {//双击鼠标显示图片
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
               
                
               
                //显示图片
               
                PicShowForm psf = new PicShowForm();
                psf.SetImage(ie);
                psf.Show();
                stream.Close();
               
            }
            //conn.Close();
            
        }
        private void LoadTreeView()
        {//加载树形列表
            treeView1.Nodes.Clear();
            InsertACityNode(treeView1);
            InsertARegionNode(treeView1);
            InsertARoadNode(treeView1);

        }
     
        private void InsertACityNode(TreeView treeView)
        {//插入一个市
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
        {//插入一个区或县
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
        {//插入一个道口
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
                MessageBox.Show("车型名称不合法!");
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