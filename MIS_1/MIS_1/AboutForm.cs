using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            label1.Text = "ʵʱ��Ƶ�����������ݹ���\r\n"+"���մ�ѧ  ��Ȩ����";
        }
    }
}