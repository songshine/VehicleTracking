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
    {//��������ͳ��֮ǰ����ʾ
        public int nWay;
        public BeforeStatisticForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//����ͳ������
            nWay = 1;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {//ʹ��Ĭ������
            nWay = 2;
            DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {//ȡ��ͳ��
            DialogResult = DialogResult.Cancel;
        }

        private void BeforeStatisticForm_Load(object sender, EventArgs e)
        {
            label1.Text = "�����ݿ��е����ݽ���ͳ����ͼ������ѡ��\r\nĬ�����ݣ�" +
                          "���Ե�ǰ�б��е����ݽ���ͳ�ƣ�\r\n��������ͳ������" +
                          "�Է�������������\r\n����ͳ����ͼ����!";
        }
    }
}