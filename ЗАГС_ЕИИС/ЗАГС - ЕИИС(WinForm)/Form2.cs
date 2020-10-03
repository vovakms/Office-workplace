using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
 


namespace ОтчетыHyTech
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.IPServer = textBox1.Text;
            Properties.Settings.Default.LOGIN    = textBox2.Text;
            Properties.Settings.Default.PASSWORD = textBox3.Text;
            Properties.Settings.Default.ConODBC  = textBox4.Text;
            Properties.Settings.Default.IPServer2 = textBox6.Text;
            Properties.Settings.Default.LOGIN2 = textBox7.Text;
            Properties.Settings.Default.PASSWORD2 = textBox8.Text;
            Properties.Settings.Default.ConODBC2 =  textBox5.Text;
             
            Properties.Settings.Default.Save();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form2 form = new Form2();

            textBox1.Text = Properties.Settings.Default.IPServer;
            textBox2.Text = Properties.Settings.Default.LOGIN;
            textBox3.Text = Properties.Settings.Default.PASSWORD;
            textBox4.Text = Properties.Settings.Default.ConODBC;
            textBox5.Text = Properties.Settings.Default.ConODBC2;
            textBox6.Text = Properties.Settings.Default.IPServer2;
            textBox7.Text = Properties.Settings.Default.LOGIN2;
            textBox8.Text = Properties.Settings.Default.PASSWORD2; 
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
