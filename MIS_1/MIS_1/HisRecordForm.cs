using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace MIS_1
{

    public partial class HisRecordForm : Form
    {
        int nRecorders;
        DataSet myDS = new DataSet();
        HisRecordInfo hri = new HisRecordInfo("Data Source=(local);Initial Catalog=Project;Integrated Security=True");
        public HisRecordForm()
        {
            InitializeComponent();
        }

        private void HisRecordForm_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“projectDataSet18.HisRecordInfo”中。您可以根据需要移动或移除它。
            this.hisRecordInfoTableAdapter.Fill(this.projectDataSet18.HisRecordInfo);
            nRecorders = dataGridView1.Rows.Count;
            label1.Text = "记录数为:" + nRecorders;
            label2.Text = "选中右键删除";
            DataView dv = this.projectDataSet18.HisRecordInfo.DefaultView;
            dataGridView1.DataSource = dv;
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);
            }
            myDS.Tables.Add(dv.ToTable());


        }
        private void DeleteAHisRecord(string id)
        {
            hri.DeleteVehicleInfo(id);
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
            else
            {
                return;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str;
            int index = 0;//箭头要落在的行索引
            int rowCount = dataGridView1.SelectedRows.Count;
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            str = "确定删除当前选中的" + dataGridView1.SelectedRows.Count + "项吗?";
            DialogResult result = MessageBox.Show(str, "删除", MessageBoxButtons.OKCancel);
            nRecorders -= dataGridView1.SelectedRows.Count;
            label1.Text = "记录数为:" + nRecorders;
            index = dataGridView1.SelectedRows[dataGridView1.SelectedRows.Count - 1].Index - 1;
            if (index < 0)
                index = 0;

            if (result == DialogResult.OK)
            {

                for (int i = rowCount - 1; i >= 0; i--)
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
                        DeleteAHisRecord(str);
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[i].Index);
                    }

                }

                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {//查询功能

            SearchWithStr(textBox1.Text);

        }

        public void SearchWithStr(string str)
        {
            int nRecorders;
            if (str == "*")
            {
                this.projectDataSet18.HisRecordInfo.DefaultView.RowFilter = null;
                this.hisRecordInfoTableAdapter.Fill(this.projectDataSet18.HisRecordInfo);
                nRecorders = dataGridView1.Rows.Count;
                label1.Text = "记录数为:" + nRecorders;
                return;
            }
            if (str == "")
            {
                MessageBox.Show("请输入要搜索的关键字!");
                return;
            }
            DataView dv = this.projectDataSet18.HisRecordInfo.DefaultView;
            if (dv != null)
            {
                dv.RowFilter = "Name='" + str + "' or Expr1='" +
                    str + "' or RegionName='" + str + "'";

            }
            dataGridView1.DataSource = dv;
            if (myDS.Tables.Count != 0)
            {
                myDS.Tables.RemoveAt(0);
            }
            myDS.Tables.Add(dv.ToTable());
            nRecorders = dv.Count;
            label1.Text = "记录数为:" + nRecorders;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strId = "" + dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            strId.Trim();

    
            byte[] b = hri.GetImageInHistory(strId);

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
        }
        public void SearchWithCondition(string strSql)
        {//设置条件后搜索
            DataView dv = this.projectDataSet18.HisRecordInfo.DefaultView;
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

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HisRecQuery hrq = new HisRecQuery();
            if(DialogResult.OK == hrq.ShowDialog())
            {
                string strSql = hrq.ReturnQuerySqlString();
                SearchWithCondition(strSql);
            }
        }
     

        public void ChangeStatisticDS(string strSql)
        {//根据SQL语句改变用来统计的数据
            DataView dv = this.projectDataSet18.HisRecordInfo.DefaultView;
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
        public void StatisticAndDraw()
        {//统计作图
            HisStatisticForm sf = new HisStatisticForm();
            sf.LoadCrystalData(myDS);
            sf.Show();
        }

        private void linkStatistic_Click(object sender, EventArgs e)
        {
            BeforeStatisticForm bsf = new BeforeStatisticForm();
            if (DialogResult.OK == bsf.ShowDialog())
            {

                if (bsf.nWay == 1)
                {//设置条件后统计
                    HisRecQuery vqo = new HisRecQuery();

                    vqo.Text = "设置统计数据条件";
                    if (vqo.ShowDialog() == DialogResult.OK)
                    {
                        string strSql = vqo.ReturnQuerySqlString();
                        this.ChangeStatisticDS(strSql);
                        this.StatisticAndDraw();
                    }

                }
                else if (bsf.nWay == 2)
                {//统计列表中所有的数据
                    this.StatisticAndDraw();
                }
            }
        }
    }
}