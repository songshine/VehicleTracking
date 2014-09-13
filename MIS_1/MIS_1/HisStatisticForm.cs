using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class HisStatisticForm : Form
    {
        DataSet myData = new DataSet();
        public HisStatisticForm()
        {
            InitializeComponent();
        }

        private void HisStatisticForm_Load(object sender, EventArgs e)
        {

            CrystalReport2 cr = new CrystalReport2();
            cr.SetDataSource(myData);
            crystalReportViewer1.ReportSource = cr;
        }
        public void LoadCrystalData(DataSet ds)
        {
            myData = ds;
        }
    }
}