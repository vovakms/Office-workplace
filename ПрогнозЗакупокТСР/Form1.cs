using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace ТСР
{
    public partial class Form1 : Form
    {
        
        string connectionString = "Dsn=" + Properties.Settings.Default.Istoch + ";uid=HTADMIN;srv=tcpip:/" + Properties.Settings.Default.ServPort + ";sn=tcpip:/" + Properties.Settings.Default.ServPort + ";ct=N;fixall=Y;msjet=N";

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).TotalDays.ToString();
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)//  при изменениии даты1
        {
            textBox1.Text = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).TotalDays.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)//  при изменениии даты1 
        {
            textBox1.Text = (dateTimePicker2.Value.Date - dateTimePicker1.Value.Date).TotalDays.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e) // нажали кнопку "Расчитать потребность"---  ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        {
            string strVibN = "", strVibR = "", strFil = "", strZak = "";
            Clear(dataGridView1);
            Clear(dataGridView2);
            richTextBox1.Clear();

            toolStripProgressBar1.Value = 10;

            if (comboBox1.SelectedIndex.ToString() == "0") strVibN = "";
            if (comboBox1.SelectedIndex.ToString() == "1") strVibN = " and LMBTREE.ID_OWN = '316'";// выбор Номенклатуры
            if (radioButton3.Checked == true && comboBox2.Text == "Филиал 11") strFil = " and (LQUEUE.ID_FSS='2711' or LQUEUE.ID_FSS='2700'or LQUEUE.ID_FSS='27') ";
            if (radioButton3.Checked == true && comboBox2.Text == "Филиал 5") strFil = " and LQUEUE.ID_FSS='2705' ";
            if (radioButton3.Checked == true && comboBox2.Text == "Филиал 7") strFil = " and LQUEUE.ID_FSS='2707' ";
            if (radioButton3.Checked == true && comboBox2.Text == "Филиал 8") strFil = " and LQUEUE.ID_FSS='2708' ";
            if (radioButton3.Checked == true && comboBox2.Text == "Филиал 9") strFil = " and LQUEUE.ID_FSS='2709' ";
            if (radioButton4.Checked == true && comboBox3.Text == "Все районы") strVibR = "";
            if (radioButton4.Checked == true && comboBox3.Text == "8,11,102") strVibR = " and (LQUEUE.RN_CODE = '8' or LQUEUE.RN_CODE = '11' or LQUEUE.RN_CODE = '102') ";

            if (radioButton2.Checked == true) strZak = " and LMBRQST.RSLT_DATE = ''  ";

            toolStripProgressBar1.Value = 20;

            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter("fix all; select LMBRQST.RQST_NUM as 'НомЗаявки', char(LMBRQST.RQST_DATE,10) as 'Дата под.заявки', char(LMBRQST.LSTN_DATE,10) as 'Дата прин.реш.', char(LMBRQST.RSLT_DATE,10) as 'Дата удовл.', ('0') as 'НаДень' ,('0') as 'НаКонтр', char(rtrim(LQUEUE.SNILS, 'C'), 11) as 'СНИЛС', LQUEUE.LNAME as 'Фамилия', LQUEUE.FNAME as 'Имя', LQUEUE.MNAME as 'Отчество', char(LQUEUE.BDATE,10) as 'Дата Рожд.',LQUEUE.STATE as 'Состояние', char(LQUEUE.STATE_DATE,10) as 'Дата Состояния', LQUEUE.DOCSER as 'Серия' , LQUEUE.DOCNUM as 'Номер', char(LQUEUE.DOCDATE,10) as 'Дата выд.док', LQUEUE.DOCORG as 'Орг. выд. док.', LQUEUE.ADDR as 'Адрес (по регистрации)'  ,          LMBTREE.NAME as 'Номенклатура', LMBRQST.IPR_COUNT, LMBRQST.REMARK as 'Примечание', LMBRQST.MOD_USER, LQUEUE.ID_FSS,   LQUEUE.RN_CODE, char(LMBRQST.IPR_DATE,10) as 'ДатаВыдачиИПР', LMBRQST.IPR_UNLIM as '1-Бесроч_(0-Сроч)', LMBRQST.IPR_LIMIT as 'СрокДействияИПР', char(LMBRQST.IPR_DATE + LMBRQST.IPR_LIMIT,10) as 'ДатаОбеспИПР', ('-') as 'Ошибки' from  LMBRQST, LQUEUE, LMBTREE where LMBRQST.ID_OWN = LQUEUE.ID and LMBRQST.CTGL_ID = LMBTREE.ID and LMBRQST.RQST_DATE > '" + dateTimePicker3.Value.Date.ToString("dd-MM-yyyy") + "' " + strVibN + strFil + strZak + strVibR + ";", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            toolStripProgressBar1.Value = 50;

            //OdbcConnection conn2 = new OdbcConnection(connectionString);  
            OdbcDataAdapter da2 = new OdbcDataAdapter("fix all; select LMBRQST.RQST_NUM,  LMBTREE.NAME, count(LMBTREE.NAME) as 'KOL', ('0') as 'KOL2'  from  LMBRQST, LQUEUE,  LMBTREE where LMBRQST.ID_OWN = LQUEUE.ID and LMBRQST.CTGL_ID = LMBTREE.ID and LMBRQST.RQST_DATE > '" + dateTimePicker3.Value.Date.ToString("dd-MM-yyyy") + "'" + strVibN + strFil + strZak + strVibR + " group by LMBTREE.NAME ;  results table '**_e'; select NAME  as 'Номенклатура', KOL as 'Кол-воЗаявок', KOL2 as 'Итог' from _e ;", conn); //
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            toolStripProgressBar1.Value = 70;

            //dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[0].Width = 300;
            toolStripStatusLabel1.Text = "Количество видов номенклатуры " + (dataGridView2.Rows.Count - 1).ToString();

            label8.Text = "Количество заявок  " + (dataGridView1.Rows.Count - 1).ToString();

            toolStripProgressBar1.Value = 100;
            //--|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||


            //string pattern = @"(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[12])\.(19[0-9][0-9]|2010|200[0-9])";
            string pattern = @"(0[1-9]|[12][0-9]|3[01])\.([0-9][0-9])\.([0-9][0-9])";

            //richTextBox1.Clear();

            CultureInfo provider = CultureInfo.InvariantCulture;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) // ----- ПЕРЕБИРАЕМ 
            {
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "365") { dataGridView1.Rows[i].Cells[4].Value = "1"; dataGridView1.Rows[i].Cells[5].Value = textBox1.Text.ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "360") { dataGridView1.Rows[i].Cells[4].Value = "1"; dataGridView1.Rows[i].Cells[5].Value = textBox1.Text.ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "730") { dataGridView1.Rows[i].Cells[4].Value = "2"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 2).ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "720") { dataGridView1.Rows[i].Cells[4].Value = "2"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 2).ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "1095") { dataGridView1.Rows[i].Cells[4].Value = "3"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 3).ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "1080") { dataGridView1.Rows[i].Cells[4].Value = "3"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 3).ToString(); }

                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "12") { dataGridView1.Rows[i].Cells[4].Value = "0.03"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 0.03).ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "24") { dataGridView1.Rows[i].Cells[4].Value = "0.06"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 0.06).ToString(); }
                if (dataGridView1.Rows[i].Cells[19].Value.ToString() == "122") { dataGridView1.Rows[i].Cells[4].Value = "0.33"; dataGridView1.Rows[i].Cells[5].Value = (Convert.ToInt32(textBox1.Text) * 0.33).ToString(); }

                string s = dataGridView1.Rows[i].Cells[20].Value.ToString(); //------------------ проверяем ПРИМЕЧАНИЕ ---------------------------------------------------------------

                if (s != "" && (s.StartsWith("об") || s.StartsWith("ОБ") || s.StartsWith("Об")))
                {
                    richTextBox1.AppendText(dataGridView1.Rows[i].Cells[20].Value.ToString() + "\n"); 
                    Regex r = new Regex(pattern);
                    Match m = r.Match(s);

                    if (m.Success)
                    {
                        try
                        {
                            DateTime StrDat = Convert.ToDateTime(m.ToString());// конвертируем первую с левой стороны дату в ПРИМЕЧАНИИ
                            if ((StrDat.Date > dateTimePicker1.Value.Date) && (StrDat.Date < dateTimePicker2.Value.Date))// проверем вхождение в период контракта
                            {
                                dataGridView1.Rows[i].Cells[5].Value = (Math.Abs((dateTimePicker2.Value.Date - StrDat.Date).TotalDays) * Convert.ToDouble(dataGridView1.Rows[i].Cells[4].Value)).ToString();
                            }
                        }
                        catch (Exception)
                        {
                            dataGridView1.Rows[i].Cells[28].Value = "ошибка";
                        }

                    }

                }
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //? ? ? ? ? ? ? ? ? ?? ? ?? ?????????????????????????????????????????????????????????????????????????????????????
                //            заявка 0-Срочная 1-Бесрочная                                               |           Дата Обеспечения ИПР                       ДатаКонтракта С                                            Дата Обеспечения ИПР                           ДатаКонтракта ПО  
                if ((dataGridView1.Rows[i].Cells[25].Value.ToString() == "0") && (Convert.ToDateTime(dataGridView1.Rows[i].Cells[27].Value.ToString()) > dateTimePicker1.Value.Date) && (Convert.ToDateTime(dataGridView1.Rows[i].Cells[27].Value.ToString()) < dateTimePicker2.Value.Date))//   проверяем на "Срок действия ИПР"
                {
                    //                                              ДатаКонтракта ПО                                         Дата Обеспечения ИПР      
                    dataGridView1.Rows[i].Cells[5].Value = (dateTimePicker2.Value.Date - Convert.ToDateTime(dataGridView1.Rows[i].Cells[27].Value.ToString())).TotalDays.ToString();
                }
                //?????????????????????????????????????????????????????????????????????????????????????????????????????
            }

            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)// ---   перебираем dataGridView2    подсчитываем  по каждому виду номенклатуре
            {
                //int os = 0; //
                double os = 0;
                for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
                {
                    //          Номеклатура2                                                 Номеклатура1      
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString() == dataGridView1.Rows[j].Cells[18].Value.ToString())
                    {
                         //if (int.TryParse(dataGridView1.Rows[j].Cells[5].Value.ToString(), out os)) // проверяем число ли в строке
                         //{
                          //os = os + Convert.ToInt32(dataGridView1.Rows[j].Cells[5].Value.ToString());// суммируем количество 
                          os = os + Convert.ToDouble(dataGridView1.Rows[j].Cells[5].Value);
                          dataGridView2.Rows[i].Cells[2].Value = os.ToString();  // показываем в dataGridView2 
                         //}
                    }
                }
            }//------------------------------------------------------------------------------------------------------------------
            

        }//---------------------------------------------------------------------------------------------------

        private void button2_Click(object sender, EventArgs e)
        {

        }

        public void Clear(DataGridView dataGridView)// ф-ия  очистки ДатаГрида-----------------------------------------------------------
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }//-------------------------------------------------------------------------------------------------------------------------------------

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e) //  нажали в меню Сервис Настройки-------------------------------------------------------
        {
            Form2 F2 = new Form2();
            F2.Show();
        }

        private void toolStripButton2_Click_1(object sender, EventArgs e) // Нажали кнопку Экспортировать в Ексель  dataGridView1   ----------------------------------------------
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            string[,] dtValue = new string[dataGridView1.Rows.Count +1 , dataGridView1.ColumnCount];

            dtValue[0, 0] = dataGridView1.Columns[0].HeaderText; // 
            dtValue[0, 1] = dataGridView1.Columns[1].HeaderText; //
            dtValue[0, 2] = dataGridView1.Columns[2].HeaderText; //
            dtValue[0, 3] = dataGridView1.Columns[3].HeaderText; //
            dtValue[0, 4] = dataGridView1.Columns[4].HeaderText; //
            dtValue[0, 5] = dataGridView1.Columns[5].HeaderText; //
            dtValue[0, 6] = dataGridView1.Columns[6].HeaderText; //
            dtValue[0, 7] = dataGridView1.Columns[7].HeaderText; //
            dtValue[0, 8] = dataGridView1.Columns[8].HeaderText; //
            dtValue[0, 9] = dataGridView1.Columns[9].HeaderText; //
            dtValue[0, 10] = dataGridView1.Columns[10].HeaderText; //
            dtValue[0, 11] = dataGridView1.Columns[11].HeaderText; //
            dtValue[0, 12] = dataGridView1.Columns[12].HeaderText; //
            dtValue[0, 13] = dataGridView1.Columns[13].HeaderText; //
            dtValue[0, 14] = dataGridView1.Columns[14].HeaderText; //
            dtValue[0, 15] = dataGridView1.Columns[15].HeaderText; //
            dtValue[0, 16] = dataGridView1.Columns[16].HeaderText; //
            dtValue[0, 17] = dataGridView1.Columns[17].HeaderText; // 
            dtValue[0, 18] = dataGridView1.Columns[18].HeaderText; //
            dtValue[0, 19] = dataGridView1.Columns[19].HeaderText; //
            dtValue[0, 20] = dataGridView1.Columns[20].HeaderText; //
            dtValue[0, 21] = dataGridView1.Columns[21].HeaderText; //
            dtValue[0, 22] = dataGridView1.Columns[22].HeaderText; //
            dtValue[0, 23] = dataGridView1.Columns[23].HeaderText; //
            dtValue[0, 24] = dataGridView1.Columns[24].HeaderText; //
            dtValue[0, 25] = dataGridView1.Columns[25].HeaderText; //
            dtValue[0, 26] = dataGridView1.Columns[26].HeaderText; //
            dtValue[0, 27] = dataGridView1.Columns[27].HeaderText; //
            dtValue[0, 28] = dataGridView1.Columns[28].HeaderText; //


            for (int i = 0; i < dataGridView1.Rows.Count-1  ; i++)  // 
            {
                for (int j = 0; j < dataGridView1.ColumnCount  ; j++)
                {
                    dtValue[i+1, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
             
            ExcelWorkSheet.get_Range("A1", "AC" + (dataGridView1.Rows.Count + 1).ToString()).Value2 = dtValue;
            ExcelWorkSheet.get_Range("A1", "AC" + (dataGridView1.Rows.Count + 1).ToString()).NumberFormat = "@";
            ExcelWorkSheet.get_Range("A1", "AC" + (dataGridView1.Rows.Count + 1).ToString()).Font.Name = "Microsoft Sans Serif";
            ExcelWorkSheet.get_Range("A1", "AC" + (dataGridView1.Rows.Count + 1).ToString()).Font.Size = 10;
            ExcelWorkSheet.get_Range("A1", "AC" + (dataGridView1.Rows.Count + 1).ToString()).Columns.ColumnWidth = 20;
            ExcelWorkSheet.get_Range("A1", "A1").Rows.RowHeight = 30;
            
            
            //ExcelWorkSheet.Range["A1", "AC"].HorizontalAlignment = Excel.Constants.xlCenter;
            //ExcelWorkSheet.Range["A1", "AC"].VerticalAlignment = Excel.Constants.xlCenter;

            
            //ExcelWorkSheet.get_Range("A1", "AC").Rows.Font.Bold = true;
            
            
            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
             
            //Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            //Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;

            //ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);//Книга

            //ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1); //Таблица
              
            //for (int i = -1; i < dataGridView1.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dataGridView1.ColumnCount; j++)
            //    {
            //        if (i == -1)
            //            ExcelApp.Cells[1, j + 1] = dataGridView1.Columns[j].Name;
            //        else
            //            ExcelApp.Cells[i +2, j + 1] = dataGridView1.Rows[i].Cells[j].Value;
            //    }
            //}

            //ExcelApp.Visible = true;  //Вызываем нашу созданную эксельку
            //ExcelApp.UserControl = true;  
        }

        private void toolStripButton3_Click(object sender, EventArgs e) // Нажали кнопку на панели меню "Импорт в Word"-------------------------------------------------------
        {
            richTextBox1.AppendText("_______________________________________ Наименование ______________________________________________ Кол-во заявок _________ Треб.кол-во _______ \n"); 
            
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                richTextBox1.AppendText(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).PadRight(130) + "___" );
                richTextBox1.AppendText( Convert.ToString(dataGridView2.Rows[i].Cells[1].Value).PadRight(10) + "___");
                richTextBox1.AppendText(Convert.ToString(dataGridView2.Rows[i].Cells[2].Value).PadRight(10) + "___\n");
            }
        }
         
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
            ExcelApp.SheetsInNewWorkbook = 1;
            Microsoft.Office.Interop.Excel.Workbook ExcelWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ExcelWorkSheet;
            ExcelWorkBook = ExcelApp.Workbooks.Add(System.Reflection.Missing.Value);
            ExcelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelWorkBook.Worksheets.get_Item(1);

            string[,] dtValue = new string[dataGridView2.Rows.Count+1, dataGridView2.ColumnCount];

            dtValue[0, 0] = dataGridView2.Columns[0].HeaderText; // 
            dtValue[0, 1] = dataGridView2.Columns[1].HeaderText; // 
            dtValue[0, 2] = dataGridView2.Columns[2].HeaderText; // 
            
            for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount  ; j++)
                {
                    dtValue[i+1, j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }

            ExcelWorkSheet.get_Range("B3", "D" + (dataGridView2.Rows.Count + 2).ToString()).Value2 = dtValue;
            ExcelWorkSheet.get_Range("B3", "D" + (dataGridView2.Rows.Count + 2).ToString()).NumberFormat = "@";
            ExcelWorkSheet.get_Range("B3", "D" + (dataGridView2.Rows.Count + 2).ToString()).Font.Name = "Microsoft Sans Serif";
            ExcelWorkSheet.get_Range("B3", "D" + (dataGridView2.Rows.Count + 2).ToString()).Font.Size = 10;

            //Вызываем нашу созданную эксельку.
            ExcelApp.Visible = true;
            ExcelApp.UserControl = true;
        }




     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter("fix all; select * from LQUEUE;", conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void списокГосударственныхКонтрактовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String strSQL = "fix all;select LQUEUE.LNAME as 'Фамилия (л-ника)' , LQUEUE.FNAME as 'Имя (л-ника)' ,LQUEUE.MNAME as 'Отчество (л-ника)' ,LQUEUE.BDATE as 'Дата р-ия (л-ника)' ,LMBRQST.IPR_NUM,LMBRQST.IPR_DATE,LMBRQST.IPR_COUNT as 'Кол-во реком. по ИПР',LMBRQST.IPR_LENGTH,LMBRQST.IPR_SERV,LMBRQST.IPR_UNLIM,LMBRQST.IPR_LIMIT,LMBRQST.BUY_COUNT,LMBRQST.BUY_DATE,LMBRQST.RQST_DATE,LMBRQST.LSTN_DATE,LMBRQST.RSLT_DATE,LMBRQST.WORK_TYPE,LMBRQST.EXEC_TYPE,LMBRQST.DENIED,LMBRQST.CLOSE_RSN,LMBRQST.CTGH_ID,LMBRQST.CTGL_ID,LMBRQST.CODE_ID,LMBRQST.COST,LMBRQST.PAY,LMBRQST.PAY_COUNT,LMBRQST.REMARK from  LMBRQST, LQUEUE where LMBRQST.ID_OWN = LQUEUE.ID  ;";
            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter(strSQL, conn);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
 

        private void остаткиПоЗаявкамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String strSQL = " fix all; select * from  LMBAGR ;";
            OdbcConnection conn = new OdbcConnection(connectionString);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter(strSQL, conn);

            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void toolStripSeparator44_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButton4.Checked = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButton3.Checked = true;
        }

       

     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
