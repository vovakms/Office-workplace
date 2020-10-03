using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;

using System.Data.SQLite;
using System.IO;
using System.Data.Common;

//using System.DirectoryServices;

namespace Почтовичок
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Ексель файлы |*.xls*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                sr.Close();
            }

            textBox1.Text = openFileDialog1.FileName.ToString();  
             
            ExcelObj.Application app = new ExcelObj.Application();
            ExcelObj.Workbook workbook;
            ExcelObj.Worksheet NwSheet;
            ExcelObj.Range ShtRange;
            DataTable dt = new DataTable();
             
            workbook = app.Workbooks.Open(textBox1.Text.ToString(), Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

             
            NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);//Устанавливаем номер листа из котрого будут извлекаться данные  Листы нумеруются от 1
                ShtRange = NwSheet.UsedRange;
                for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                {
                    dt.Columns.Add(new DataColumn((ShtRange.Cells[1, Cnum] as ExcelObj.Range).Value2.ToString()));
                }
                dt.AcceptChanges();

                string[] columnNames = new String[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    columnNames[0] = dt.Columns[i].ColumnName;
                }

                for (int Rnum = 2; Rnum <= ShtRange.Rows.Count; Rnum++)
                {
                    DataRow dr = dt.NewRow();
                    for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
                    {
                        if ((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                        {
                            dr[Cnum - 1] =  (ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }

                dataGridView1.DataSource = dt;
                app.Quit();
           
              //  Application.Exit();
             

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string grup = "", adr = "", abonent = "", regNom = "", note ="";

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                grup = dataGridView1.Rows[i].Cells[0].Value.ToString();
                adr = dataGridView1.Rows[i].Cells[1].Value.ToString();
                abonent = dataGridView1.Rows[i].Cells[2].Value.ToString();
                regNom = dataGridView1.Rows[i].Cells[3].Value.ToString();
                note = dataGridView1.Rows[i].Cells[4].Value.ToString();
                Vdb(grup, adr,  abonent,  regNom, note);
            }
        }

        private void Vdb(string grup, string adr, string abonent, string regNom, string note)
        {
            string DatbaseName = Directory.GetCurrentDirectory() + "\\mailer.db3";
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", DatbaseName));
            connection.Open();
            SQLiteCommand command;

            command = new SQLiteCommand("INSERT INTO 'address' ('group', 'mail','abonent','regNom','note') VALUES ('" + grup + "' ,'" + adr + "','" + abonent + "','" + regNom + "','" + note + "');", connection);
            command.ExecuteNonQuery(); 

          
        }






    }
}
