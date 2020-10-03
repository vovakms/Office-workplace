using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace СистемаОтчетов
{
    public partial class Form1 : Form
    {
        string katPodr = "";
        string connectionString = "Dsn=" + Properties.Settings.Default.Istoch + ";uid=HTADMIN;srv=tcpip:/" + Properties.Settings.Default.ServPort + ";sn=tcpip:/" + Properties.Settings.Default.ServPort + ";ct=N;fixall=Y;msjet=N";
        
         
        public Form1()
        {
            InitializeComponent();
        }
 
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e) // Нажали в меню Сервис  пункт Настройки
        {
            Form2 F2 = new Form2();
            F2.Show();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) // выбрали строку в списке отчетов
        {
            DataTable dt = new DataTable();

            Clear(dataGridView1);
            Clear(dataGridView2);

             //string text = "var @g='" + comboBox1.Text.ToString() + "';";
            
            // запись в файл
            using (FileStream fstream = new FileStream("SQL\\" + katPodr + "\\" + listBox1.SelectedItem.ToString() + ".sql", FileMode.OpenOrCreate))
            {
                string god = comboBox1.Text.ToString();
                string kv = comboBox2.Text.ToString();

                fstream.Seek(8, SeekOrigin.Current ); //  
                byte[]  input = Encoding.Default.GetBytes(god);
                fstream.Write(input, 0, 4);

                fstream.Seek(12, SeekOrigin.Current); //  
                input = Encoding.Default.GetBytes(kv);
                fstream.Write(input, 0, 1); 
            }
              
            string tSQL = File.ReadAllText("SQL\\"+ katPodr +"\\" + listBox1.SelectedItem.ToString() +".sql"  );

            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter(tSQL, conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
         

        private void selectionPodr() //  отображение списка отчетов выбранного подразделения
        {
            label1.Text = "Отчеты " + katPodr;
            listBox1.Visible = true;
            listBox1.Items.Clear();
            
            foreach (string fullname in Directory.GetFiles("SQL\\" + katPodr))
                listBox1.Items.Add(Path.GetFileNameWithoutExtension(fullname));
        }

        // --------------------------------------- Выбор подразделения ------------------------------------------------------
        private void отАиСВToolStripMenuItem_Click(object sender, EventArgs e) //от_АиСВ     в меню  
        {
            katPodr = отАиСВToolStripMenuItem.Text.ToString(); selectionPodr( );
        }

        private void toolStripButton7_Click(object sender, EventArgs e) // от_АиСВ кнопка на панели инструментов 
        {
            katPodr = toolStripButton7.Text.ToString() ;   selectionPodr();
        }

        private void бухгалтерияToolStripMenuItem_Click(object sender, EventArgs e) // Бухгалтерия    в меню
        {
            katPodr = бухгалтерияToolStripMenuItem.Text.ToString();  selectionPodr();
        }
         
        private void toolStripButton1_Click(object sender, EventArgs e) // Бухгалтерия кнопка на панели инструментов 
        {
            katPodr = toolStripButton1.Text.ToString(); selectionPodr();
        }

        private void грСПРToolStripMenuItem_Click(object sender, EventArgs e) // гр_СПР в меню
        {
            katPodr = грСПРToolStripMenuItem.Text.ToString();
        }
        private void toolStripButton13_Click(object sender, EventArgs e) // гр_СПР на панели инструментов
        {
            katPodr = toolStripButton13.Text.ToString();
        }
//------------------------------------------------------------------------------------------------------------------
        public void Clear(DataGridView dataGridView)// ф-ия  очистки ДатаГрида----------------- ----------------
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                    dataGridView.Columns.Remove(dataGridView.Columns[i] );// Rows.Remove(dataGridView.Rows[i]);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)// Нажали кнопку експорт в Ексель
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            string[,] dtValue = new string[dataGridView1.Rows.Count + 1, dataGridView1.ColumnCount];

            for (int i = 0; i < dataGridView1.ColumnCount; i++) // подписываем столбики
            {
                dtValue[0, i] = dataGridView1.Columns[i].HeaderText; //
            }
             
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)  // 
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dtValue[i + 1, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            ExcelWorkSheet.get_Range("B3", "T" + (dataGridView1.Rows.Count + 2).ToString()).Value2 = dtValue;
            //ExcelWorkSheet.get_Range("B3", "T" + (dataGridView1.Rows.Count + 2).ToString()).NumberFormat = "@";
            //ExcelWorkSheet.get_Range("B3", "T" + (dataGridView1.Rows.Count + 2).ToString()).Font.Name = "Microsoft Sans Serif";
            //ExcelWorkSheet.get_Range("B3", "T" + (dataGridView1.Rows.Count + 2).ToString()).Font.Size = 10;

            //ExcelWorkSheet.get_Range("F", "F" + (dataGridView1.Rows.Count + 2).ToString()).NumberFormat = "0.00";

            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
 

        }

       
        

        







    }
}
