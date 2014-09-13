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
    public partial class MainFram : Form
    {
        private string strManagerName = "";
        private int nManagerId;
        private bool bIsLog = false;
        DataRowView  drv;
        SqlConnection conn = new SqlConnection();
        public MainFram()
        {
            InitializeComponent();
        }

        private void toolStripManager_Click(object sender, EventArgs e)
        {
            //为了方便调试而加的注释
            if (bIsLog)
            {
                int levelId = (int)drv.Row["LevelId"];
                if (levelId == 0)
                {
                    Manager mr = new Manager();
                    mr.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("你无权此操作!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("请先登录!");
                return;
            }
            //Manager mrForm = new Manager();
            //mrForm.Show();
            //return;
        }

        private void 车辆管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //为了方便调试而加的注释
            if (bIsLog)
            {
                VehicleSearchForm vsf = new VehicleSearchForm();
                vsf.MdiParent = this;
                vsf.Show();
            }
            else
            {
                MessageBox.Show("请先登录!");
                return;
            }
            //VehicleSearchForm vsf = new VehicleSearchForm();
            //vsf.MdiParent = this;
            //vsf.Show();
        }

        private void LoginIn_Click(object sender, EventArgs e)
        {
            //通过窗体名称查询该窗体是否已经存在，如存在则显示，否则就新创建一个
            
            LogForm lf = new LogForm();
            bool bIsS = LoginMIS(lf);
            if (!bIsLog) return;
            int levelId = (int)drv.Row["LevelId"];
            if (this.ActiveMdiChild == null)
            {
                VehicleSearchForm vsf = new VehicleSearchForm();
                vsf.MdiParent = this;
                this.Text = strManagerName;
                vsf.Show();
            }
      
        }

        private void LoginOff_Click(object sender, EventArgs e)
        {//退出
            this.Close();
        }

        private void 注销ToolStripLogOff_Click(object sender, EventArgs e)
        {//注销
            if (DialogResult.Cancel == MessageBox.Show("              确定要注销吗？", "注销", MessageBoxButtons.OKCancel))
            {
                return;
            }
            if (!bIsLog)
                return;
            this.Text = "未登录";
            strManagerName = "";
            drv.Delete();
            bIsLog = false;
            foreach (Form _form in this.MdiChildren)
            {
                _form.Close();
            }
            HideAllFunction(true);
            MessageBox.Show("注销成功");
            

        }
        private void HideAllFunction(bool ifReally)
        {
            if (ifReally)
            {
                this.toolStrip1.Visible = false;
                this.操作ToolStripMenuItem.Visible = false;
                this.窗口ToolStripMenuItem.Visible = false;
            }
            else
            {
                this.toolStrip1.Visible = true;
                this.操作ToolStripMenuItem.Visible = true;
                this.窗口ToolStripMenuItem.Visible = true;
            }
        }
        public bool LoginMIS(LogForm lf)
        {
            if (bIsLog)
            {
                MessageBox.Show("已经登录，请注销后再登录");
                return false;
              
            }
            if (lf.ShowDialog() == DialogResult.OK)
            {
                drv = ((DataRowView)lf.comboBoxLogName.SelectedItem);

                //this.Text +="—"+drv.Row["ManagerName"].ToString();
                strManagerName = drv.Row["ManagerName"].ToString();
                nManagerId = (int)drv.Row["ManagerId"];
                bIsLog = true;
                int levelId = (int)drv.Row["LevelId"];
                if (levelId == 1)
                {
                    toolStripManager.Visible = false;
                    toolStripBtnManager.Visible = false;
                }
                else
                {
                    toolStripManager.Visible = true;
                    toolStripBtnManager.Visible = true;
                }
                HideAllFunction(false);
                return true;
            }
            return false;
       }
       
        private void MainFram_Load(object sender, EventArgs e)
        {//加载主窗口

            VehicleSearchForm vsForm = new VehicleSearchForm();
            vsForm.MdiParent = this;
            vsForm.Show();
         }

       
       
        private void toolStripBtnFirst_Click(object sender, EventArgs e)
        {//到最后一条记录
            VehicleSearchForm vsf = new VehicleSearchForm();
            vsf = (VehicleSearchForm)this.ActiveMdiChild;
            if (vsf != null)
                vsf.GoToFirstRecord();
        }

        private void toolStripBtnLast_Click(object sender, EventArgs e)
        {//到第一条记录
            VehicleSearchForm vsf = new VehicleSearchForm();
            vsf = (VehicleSearchForm)this.ActiveMdiChild;
            if (vsf != null)
                vsf.GotoLastRecord();
        }

        private void toolStripBtnDelete_Click(object sender, EventArgs e)
        {//删除选中的项
            VehicleSearchForm vsf = new VehicleSearchForm();
            vsf = (VehicleSearchForm)this.ActiveMdiChild;
            if(vsf != null)
                vsf.DeleteAllSelected();
        }

        private void toolStripBtnStatistic_Click(object sender, EventArgs e)
        {//统计作图
            BeforeStatisticForm bsf = new BeforeStatisticForm();
            if (DialogResult.OK == bsf.ShowDialog())
            {
                VehicleSearchForm vsf = new VehicleSearchForm();
                vsf = (VehicleSearchForm)this.ActiveMdiChild;
                if (vsf == null)
                    return;
                if (bsf.nWay == 1)
                {//设置条件后统计
                    VehicleQueryOptionForm vqo = new VehicleQueryOptionForm();

                    vqo.Text = "设置统计数据条件";
                    if (vqo.ShowDialog() == DialogResult.OK)
                    {
                        string strSql = vqo.ReturnQuerySqlString();
                        vsf.ChangeStatisticDS(strSql);
                        vsf.StatisticAndDraw();
                    }

                }
                else if (bsf.nWay == 2)
                {//统计列表中所有的数据
                    vsf.StatisticAndDraw();
                }
            }
        }
        
        private void ToolStripBaseInfo_Click(object sender, EventArgs e)
        {//基本信息管理
            BaseInfoManagerForm bimf = new BaseInfoManagerForm();
            if (bimf.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void MainFram_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void toolStripBtnManager_Click(object sender, EventArgs e)
        {
            Manager mr = new Manager();
            mr.Show();
        }

        private void 添加车型ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BaseInfoManagerForm bimf = new BaseInfoManagerForm();
            bimf.SelectPageToOpen(0);
            bimf.Show();
        }

        private void 添加道口ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BaseInfoManagerForm bimf = new BaseInfoManagerForm();
            bimf.SelectPageToOpen(1);
            bimf.Show();
        }

        private void 添加区县ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BaseInfoManagerForm bimf = new BaseInfoManagerForm();
            bimf.SelectPageToOpen(2);
            bimf.Show();
        }

        private void 添加市ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BaseInfoManagerForm bimf = new BaseInfoManagerForm();
            bimf.SelectPageToOpen(3);
            bimf.Show();
        }

        private void 车辆管理窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {//打开窗口

            VehicleSearchForm vsf = new VehicleSearchForm();
            vsf.MdiParent = this;
            VehicleSearchForm vsf1 = (VehicleSearchForm)this.ActiveMdiChild;
            if(vsf1 != null)
            {
                if (vsf.WindowState == FormWindowState.Maximized)
                    vsf.WindowState = FormWindowState.Maximized;
                else
                    vsf.WindowState = FormWindowState.Normal;
            }
            
            vsf.Show();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehicleSearchForm vsf = (VehicleSearchForm)this.ActiveMdiChild;
            vsf.Close();
        }

        private void toolStripButtonReLoad_Click(object sender, EventArgs e)
        {
            VehicleSearchForm vsf = (VehicleSearchForm)this.ActiveMdiChild;
            if (vsf != null)
            {
                vsf.ReloadDialog();
            }
        }

        private void toolStripBtnHisRecord_Click(object sender, EventArgs e)
        {//查询历史记录
            HisRecordForm hrf = new HisRecordForm();
            hrf.Show();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void 版本更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("恭喜您，已经是最新版本了!");
            
        }

        private void ToolStripHisRecQuery_Click(object sender, EventArgs e)
        {
            HisRecordForm hrf = new HisRecordForm();
            hrf.Show();
        }

 
    }
}