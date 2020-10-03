using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Data.OleDb;
using System.Net;
using System.Net.Mail;

using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SQLite;
using System.IO;
using System.Data.Common;

namespace Почтовичок
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Zapros("0");  //  table;
            
            dataGridView1.Columns[0].Visible = false;

            richTextBox2.AppendText("Кол-во адресов - " + (dataGridView1.RowCount - 1).ToString() + "\n");

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                richTextBox2.AppendText(dataGridView1.Rows[i].Cells[2].Value.ToString() + "\n");
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }

            tabPage2.Parent = null;
            tabPage3.Parent = null;
            tabPage4.Parent = null;

            textBox3.Text = Properties.Settings.Default.СерверSMPT;
            textBox4.Text = Properties.Settings.Default.УчеткаРассылки;
            textBox6.Text = Properties.Settings.Default.Пароль;
            textBox5.Text = Properties.Settings.Default.БД;

        }

        private DataTable Zapros( string grup ) // ф-ция  запрос 
        {
            DataTable table = new DataTable();

            string databaseName = Directory.GetCurrentDirectory() + "\\mailer.db3";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM 'address' WHERE 'group' ='" + grup + "';", connection);// 
            SQLiteDataReader reader = command.ExecuteReader();

            table.Load(reader);

            connection.Close();

            return table;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) // ----------------  при закрытии формы
        {
            Properties.Settings.Default.СерверSMPT = textBox3.Text.ToString();
            Properties.Settings.Default.УчеткаРассылки = textBox4.Text.ToString();
            Properties.Settings.Default.Пароль = textBox6.Text.ToString();
            Properties.Settings.Default.БД = textBox5.Text.ToString();
            Properties.Settings.Default.Save();
        }


        private void button2_Click(object sender, EventArgs e)//---------------нажали кнопку ВЫБОР ФАЙЛА ДЛЯ ВЛОЖЕНИЯ
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                sr.Close();
            }

            textBox1.Text = openFileDialog1.FileName.ToString();  
        }
          
        private void button1_Click(object sender, EventArgs e) // -------------  нажали кнопку  СТАРТ 
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++) // перебираем все строки  dataGridView1
            {
                string adr = dataGridView1.Rows[i].Cells[2].Value.ToString();
                otprSMTP(adr);
            }
        }
         
        private void otprSMTP(string adr  ) // ----------------  ф-ция  отправки письма   ---------
        {
            SmtpClient Smtp = new SmtpClient(textBox3.Text.ToString() , 25);
            Smtp.Credentials = new NetworkCredential(textBox4.Text.ToString(), textBox6.Text.ToString() );
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress(adr);   // Эл адр кому
            Message.To.Add(new MailAddress(adr));  //
            Message.Subject = textBox2.Text.ToString();   // тема
            Message.Body = parsTeloP();     // тело письма

            try
            {
                Smtp.Send(Message);
            }
            catch (SmtpException)
            {
                MessageBox.Show("Ошибка! " + adr );
            }
        
        }

        private string parsTeloP() // --------------------- ф-ция   парсим РичТекстБокс и  формируем строковою переменную тела письма
        {
            string telop = "";
            foreach (string line in richTextBox1.Lines)
                telop = telop + " " + line + "\r\n";

            return telop;
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e) // ------  показываем закладку НАСТРОЙКИ
        {
            tabPage3.Parent = tabControl1;
            tabControl1.SelectedTab = tabPage3;
        }

        private void адреснаяКнигаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabPage2.Parent = tabControl1;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            
            opf.ShowDialog();
            
            string filename = opf.FileName;
              
            textBox5.Text = filename;
        }

        private void импортИзФайлаЕксельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)// нажали кнопку   "Показать выбранных"
        {
            string strZ;

            for (int i = 0; i < 8; i++ )
            {
                //checkBox3 
            }

            Zapros(checkBox7.Text.ToString());
        }

       

        
 




    }
}
//***************************************************************************************************

 







 







