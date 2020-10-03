using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
 
namespace СистемаОтчетов
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
 
          //  if (!File.Exists("SQL\\" + listBox1.SelectedItem.ToString() + ".sql"))
          //      File.Create("SQL\\" + listBox1.SelectedItem.ToString() + ".sql").Close();
            
                

           // richTextBox1.AppendText("SQL\\"+listBox1.SelectedItem.ToString() + ".txt");
           // richTextBox1.LoadFile("C:\\АСВ_Спр001.txt"); // "SQL\\"+   + listBox1.SelectedItem.ToString() + ".txt"
          
           // listBox1.DataSource = File.ReadAllLines("C:\\АСВ_Спр001.txt");
            textBox6.Text = File.ReadAllText("SQL\\"+listBox1.SelectedItem.ToString() + ".sql");
            
        }
         
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!File.Exists("SQL\\" + listBox1.SelectedItem.ToString() + ".sql"))
                File.Create("SQL\\" + listBox1.SelectedItem.ToString() + ".sql").Close();
            File.WriteAllText("SQL\\" + listBox1.SelectedItem.ToString() + ".sql", textBox6.Text);

            Properties.Settings.Default.Istoch = textBox5.Text.ToString();
            Properties.Settings.Default.ServPort = textBox1.Text.ToString();
            Properties.Settings.Default.Save();    

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
               File.WriteAllText("SQL\\" + listBox1.SelectedItem.ToString() + ".sql", textBox6.Text);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox5.Text = Properties.Settings.Default.Istoch;
            textBox1.Text = Properties.Settings.Default.ServPort;
        }
    }
}
