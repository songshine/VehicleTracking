using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class BeforeStatisticForm : Form
    {//该类用于统计之前的显示
        public int nWay;
        public BeforeStatisticForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//设置统计条件
            nWay = 1;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {//使用默认数据
            nWay = 2;
            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {//取消统计
            DialogResult = DialogResult.Cancel;
        }

        private void BeforeStatisticForm_Load(object sender, EventArgs e)
        {
            label1.Text = "对数据库中的数据进行统计作图，可以选择\r\n默认数据，" +
                          "即对当前列表中的数据进行统计，\r\n或者设置统计条件" +
                          "对符合条件的数据\r\n进行统计作图分析!";
        }
    }
}