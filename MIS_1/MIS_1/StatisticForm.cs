using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class StatisticForm : Form
    {
        DataSet myData = new DataSet();
        public StatisticForm()
        {
            InitializeComponent();
        }

        private void StatisticForm_Load(object sender, EventArgs e)
        {
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDataSource(myData);
            crystalReportViewer1.ReportSource = cr;
        }
        public void LoadCrystalData(DataSet ds)
        {
            myData = ds;
        }
    }
}