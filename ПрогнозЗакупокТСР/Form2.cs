using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ТСР
{
    public partial class Form2 : Form
    {
         
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.Istoch ;
            textBox2.Text = Properties.Settings.Default.ServPort ;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Istoch = textBox1.Text.ToString();
            Properties.Settings.Default.ServPort = textBox2.Text.ToString();
            Properties.Settings.Default.Save();    
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
