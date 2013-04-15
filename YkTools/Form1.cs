using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YkTools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = "124.203.31.162";
            int index=ip.IndexOf(".", ip.IndexOf(".")+1);
            string str = ip.Substring(0,index);
            MessageBox.Show(str);
        }
    }
}
