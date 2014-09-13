using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MIS_1
{
    public partial class PicShowForm : Form
    {
        public PicShowForm()
        {
            InitializeComponent();
        }
        public void SetImage(Image ie)
        {
            pictureBox1.Image = ie;
            Size sz = new Size(ie.Width, ie.Height + 24);
            this.Size = sz;
        }
    }
}