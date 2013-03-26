using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YK.Common;

namespace YkTools
{
    public partial class DESEncryptForm : Form
    {
        public DESEncryptForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = DESEncrypt.Encrypt(this.textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = DESEncrypt.Decrypt(this.textBox2.Text);
        }
    }
}
