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
using System.Data.OleDb;

using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

using System.Xml;
using Microsoft.Win32;
using System.Xml.XPath;

using System.Xml.Linq;
using System.Xml.Xsl;

namespace ВыпискиМНС
{
    public partial class Form1 : Form
    {
        string connectionString = "Dsn=" + Properties.Settings.Default.NameDSN + ";uid=" + Properties.Settings.Default.Login + ";srv=tcpip:/" + Properties.Settings.Default.ServPort + ";sn=tcpip:/" + Properties.Settings.Default.ServPort + ";ct=N;fixall=Y;msjet=N";// строка подключения к ЕИИС
        bool ofEIIS = false;// флаг из ЕИИС
        bool UrLi = false;  // флаг ЮрЛ

        DataTable dT = new DataTable();

        DataSet dS1;
        
        
        public Form1()
        {
            InitializeComponent();
             
             var date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
             dateTimePicker1.Value = date ;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)// при изменении размеров формы
        {
            Rectangle screenSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds;

            splitContainer2.SplitterDistance = screenSize.Size.Width - 150; //  screenSize.Size.Height
            
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)// ---- при изменении позиции в списке 
        {
            //tabControl1.SelectTab(tabPage2);
            //string[] separators = { " ; " };
            //string[] mS = (listBox1.SelectedItem.ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries);

            tabControl1.SelectedIndex = listBox1.SelectedIndex;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)// переключении закладок ТабКонтрола
        {
            listBox1.SelectedIndex = tabControl1.SelectedIndex;
        } 
        
        private void toolStripButton14_Click(object sender, EventArgs e)// кн "Настройки"
        {
            Form2 frm = new Form2();
            frm.Visible = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//  кн "Подключиться к БД ЕИИС"
        {
           

        }

        private void toolStripButton2_Click(object sender, EventArgs e)//  кн "Печать "
        {
            printDialog1.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)//  кн "Юридические лица из ЕИИС"
        {
            ofEIIS = true; UrLi = true; Clear(); 
            dateTimePicker1.Visible = true; dateTimePicker2.Visible = true; 
            label2.Visible = true;          label3.Visible = true;
            label1.Visible = false; textBox3.Visible = false; 
            
            dataGridView1.DataSource = Zapros( "ЮрЛица.sql" );

            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //                 dT.Columns.Add(dataGridView1.Columns[i].Name);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)//  кн "ИП из ЕИИС"
        {
            ofEIIS = true; UrLi = false; Clear();
            dateTimePicker1.Visible = true; dateTimePicker2.Visible = true;
            label2.Visible = true; label3.Visible = true;
            label1.Visible = false; textBox3.Visible = false; 
            
            dataGridView1.DataSource = Zapros( "ИП.sql" );

            //for (int i = 0; i < dataGridView1.Columns.Count; i++)
            //       dT.Columns.Add(dataGridView1.Columns[i].Name);
        }

        private void toolStripButton16_Click(object sender, EventArgs e)//кн "ЮЛ из XML-файла"
        {
            ofEIIS = false; UrLi = true; Clear();// устанавливаем флаги и очищаем 
            dateTimePicker1.Visible = false; dateTimePicker2.Visible = false;
            label2.Visible = false; label3.Visible = false;
            label1.Visible = true; textBox3.Visible = true;
            DataTable dTDoc = new DataTable(); // Табл 
             
            openFileDialog1.Filter = "Юридические лица (RUGFZ*.xml)|RUGFZ*.xml";// фильтр на октрываемые файлы

            if (openFileDialog1.ShowDialog() == DialogResult.OK)// если выбрали какой нибудь файл
            {
             toolStripStatusLabel1.Text = openFileDialog1.FileName.ToString();// показываем полный путь к файлу
             dS1 = new DataSet();                                 // делаем новый ДатаСет
             dS1.ReadXml(openFileDialog1.FileName);               // читаем файл в ДатаСет

             dTDoc = dS1.Tables[4].Clone();
             dTDoc.Merge(dS1.Tables[4]);
             //dTDoc.Columns.Add(dS1.Tables[4].Columns[3].ColumnName, typeof(String));//    ИНН
             //dTDoc.Columns.Add(dS1.Tables[4].Columns[7].ColumnName, typeof(String));//    ПолнНаимОПФ
             dTDoc.Columns.Add(dS1.Tables[5].Columns[2].ColumnName, typeof(String));//    НаимЮЛСокр
             dTDoc.Columns.Add(dS1.Tables[3].Columns[1].ColumnName, typeof(String));//   ИдДок
             dTDoc.Columns.Add(dS1.Tables[5].Columns[1].ColumnName, typeof(String));//   НаимЮЛПолн
                  
             for (int i = 0; i < dTDoc.Rows.Count; i++)
             {

              dTDoc.Rows[i][10] = dS1.Tables[5].Rows[i][2].ToString(); //    НаимЮЛСокр
              dTDoc.Rows[i][11] = dS1.Tables[3].Rows[i][1].ToString(); //    ИдДок
              dTDoc.Rows[i][12] = dS1.Tables[5].Rows[i][1].ToString(); //    НаимЮЛПолн
             }
                  
             dataGridView1.DataSource = dTDoc;
             dataGridView1.Columns["ДатаВып"].Visible = false;
             dataGridView1.Columns["ОГРН"].Visible = false;
             dataGridView1.Columns["ДатаОГРН"].Visible = false;
             dataGridView1.Columns["СпрОПФ"].Visible = false;
             dataGridView1.Columns["КодОПФ"].Visible = false;
             dataGridView1.Columns["ИдДок"].Visible = false;
             dataGridView1.Columns["НаимЮЛПолн"].Visible = false;
             dataGridView1.Update();

             //------------------------------------------- отображаем иформацию о файле ------------------------
             textBox1.AppendText("ИдФайла  \r\n");
             textBox1.AppendText(dS1.Tables[0].Rows[0][1].ToString() + "\r\n\r\n");
             textBox1.AppendText("ВерсФорм " + dS1.Tables[0].Rows[0][2].ToString() + "\r\n");
             textBox1.AppendText("ТипИнф     " + dS1.Tables[0].Rows[0][3].ToString() + "\r\n");
             textBox1.AppendText("ВерсПрог " + dS1.Tables[0].Rows[0][4].ToString() + "\r\n\r\n");
             textBox1.AppendText("КолДок   " + dS1.Tables[0].Rows[0][5].ToString() + "\r\n\r\n");

             textBox1.AppendText(dS1.Tables[1].Rows[0][1].ToString() + "\r\n");
             textBox1.AppendText(dS1.Tables[2].Rows[0][0].ToString() + "\r\n" + dS1.Tables[2].Rows[0][1].ToString() + "\r\n" + dS1.Tables[2].Rows[0][2].ToString() + "\r\n");
             textBox1.AppendText(dS1.Tables[1].Rows[0][2].ToString() + "\r\n");   
            
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)//кн "ИП из XML-файла"
        {
            ofEIIS = false; UrLi = false; Clear();
            dateTimePicker1.Visible = false; dateTimePicker2.Visible = false;
            label2.Visible = false; label3.Visible = false;
            label1.Visible = true; textBox3.Visible = true;
            DataTable dTDoc  ; // Табл 

            openFileDialog1.Filter = "Предприниматели (RIGFZ*.xml)|RIGFZ*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                toolStripStatusLabel1.Text = openFileDialog1.FileName.ToString();// показываем полный путь к файлу
                dS1 = new DataSet();          // делаем новый ДатаСет
                dS1.ReadXml(openFileDialog1.FileName);// читаем файл в ДатаСет
                 
                dTDoc = dS1.Tables[4].Clone();
                dTDoc.Merge(dS1.Tables[4]);
               
                dTDoc.Columns.Add(dS1.Tables[6].Columns[0].ColumnName, typeof(String));//  
                dTDoc.Columns.Add(dS1.Tables[6].Columns[1].ColumnName, typeof(String));//      
                dTDoc.Columns.Add(dS1.Tables[6].Columns[2].ColumnName, typeof(String));//      
                dTDoc.Columns.Add(dS1.Tables[3].Columns[1].ColumnName, typeof(String));//   ИдДок    
                 for (int i = 0; i < dTDoc.Rows.Count; i++)
                 {
                    dTDoc.Rows[i][8] = dS1.Tables[6].Rows[i][0].ToString(); //
                    dTDoc.Rows[i][9] = dS1.Tables[6].Rows[i][1].ToString(); //
                    dTDoc.Rows[i][10]= dS1.Tables[6].Rows[i][2].ToString(); //
                      
                     dTDoc.Rows[i][11] = dS1.Tables[3].Rows[i][1].ToString(); //    ИдДок
                 }

                dataGridView1.DataSource = dTDoc;
                dataGridView1.Update();
 
                //------------------------------------------- отображаем иформацию о файле ------------------------
                textBox1.AppendText("ИдФайла  \r\n");
                textBox1.AppendText(dS1.Tables[0].Rows[0][1].ToString() + "\r\n\r\n");
                textBox1.AppendText("ВерсФорм "   + dS1.Tables[0].Rows[0][2].ToString() + "\r\n");
                textBox1.AppendText("ТипИнф     " + dS1.Tables[0].Rows[0][3].ToString() + "\r\n");
                textBox1.AppendText("ВерсПрог "   + dS1.Tables[0].Rows[0][4].ToString() + "\r\n\r\n");
                textBox1.AppendText("КолДок   "   + dS1.Tables[0].Rows[0][5].ToString() + "\r\n\r\n");

                textBox1.AppendText(dS1.Tables[1].Rows[0][1].ToString() + "\r\n");
                textBox1.AppendText(dS1.Tables[2].Rows[0][0].ToString() + "\r\n" + dS1.Tables[2].Rows[0][1].ToString() + "\r\n" + dS1.Tables[2].Rows[0][2].ToString() + "\r\n");
                textBox1.AppendText(dS1.Tables[1].Rows[0][2].ToString() + "\r\n");
                 
            }
        }

        private void toolStripButton10_Click(object sender, EventArgs e)//кн "Открыть в  MS WORDE"
        {
            var app = new Word.Application();
            app.Visible = true;
            var doc = app.Documents.Add();
            var r = doc.Range();
            if (tabControl1.TabCount > 0)
            {
                foreach (Control ttt in tabControl1.SelectedTab.Controls)
                {
                    if (((RichTextBox)ttt).Name == "rtBox" + tabControl1.SelectedTab.Text.ToString())
                    {
                        r.Text = ((RichTextBox)ttt).Text;
                        //   ((RichTextBox)ttt).SaveFile(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString() + "\\" + tabControl1.SelectedTab.Text.ToString() + ".rtf");
                    }
                }
                //System.Diagnostics.Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory).ToString() + "\\" + tabControl1.SelectedTab.Text.ToString() + ".rtf");
            }
            
        }

        private void toolStripButton11_Click(object sender, EventArgs e)//кн "Открыть в MS Excel"
        {
            if (ofEIIS == true && UrLi == true) ToExcelOfEiisUrL();
            if (ofEIIS == true && UrLi == false) ToExcelOfEiisIp();
            if (ofEIIS == false && UrLi == true) ToExcelOfXmlUrL();
            if (ofEIIS == false && UrLi == false) ToExcelOfXmlIp();
        }

        private void toolStripButton18_Click(object sender, EventArgs e)//кн "Очистить список" 
        {
            listBox1.Items.Clear();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)//кн "Очистить"
        {
            Clear();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//двойной клик строки в ДатаГрид1
        {
            TabPage tP;
            RichTextBox rtBox;
            string innP ;
            if (ofEIIS == false && UrLi == true) // ЮЛ   
            {
                innP = dataGridView1.Rows[e.RowIndex].Cells["ИНН"].Value.ToString();
                if (dataGridView1.Rows[e.RowIndex].Cells["ИНН"].Value.ToString() == "") innP = "нет данных";

                listBox1.Items.Add(innP + " ; " + dataGridView1.Rows[e.RowIndex].Cells["НаимЮЛПолн"].Value.ToString() + " ; " + dataGridView1.Rows[e.RowIndex].Cells["ОГРН"].Value.ToString() + " ; " + dataGridView1.Rows[e.RowIndex].Cells["ИдДок"].Value.ToString());
             
                tP = new TabPage(dataGridView1.Rows[e.RowIndex].Cells["ИНН"].Value.ToString());
                tabControl1.TabPages.Add(tP);
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;

                rtBox = new RichTextBox();
                rtBox.Name = "rtBox" + dataGridView1.Rows[e.RowIndex].Cells["ИНН"].Value.ToString();
                rtBox.Width = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width; ;
                rtBox.Height = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Height; ;
                rtBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);
                
                tP.Controls.Add(rtBox);
                PreViewUL(dataGridView1.Rows[e.RowIndex].Cells["ИдДок"].Value.ToString(), rtBox, dataGridView1.Rows[e.RowIndex].Cells["ОГРН"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["НаимЮЛПолн"].Value.ToString());
                
            }

            if (ofEIIS == false && UrLi == false) // ИП
            {
                innP = dataGridView1.Rows[e.RowIndex].Cells["ИННФЛ"].Value.ToString();
               
                listBox1.Items.Add(innP + " ; " + dataGridView1.Rows[e.RowIndex].Cells["ИдДок"].Value.ToString());
                
                tP = new TabPage(dataGridView1.Rows[e.RowIndex].Cells["ИННФЛ"].Value.ToString());
                tabControl1.TabPages.Add(tP);
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                
                rtBox = new RichTextBox();
                rtBox.Name = "rtBox" + dataGridView1.Rows[e.RowIndex].Cells["ИННФЛ"].Value.ToString();
                rtBox.Width = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Width; ;
                rtBox.Height = tabControl1.TabPages[tabControl1.TabPages.Count - 1].Height; ;
                rtBox.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top);

                tP.Controls.Add(rtBox);
                PreViewIP(dataGridView1.Rows[e.RowIndex].Cells["ИдДок"].Value.ToString(), rtBox, dataGridView1.Rows[e.RowIndex].Cells["ОГРНИП"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["Фамилия"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["Имя"].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells["Отчество"].Value.ToString());
               
           }

            if (listBox1.Items.Count == 1)
                listBox1.SetSelected(0, true);
            else
                listBox1.SetSelected(listBox1.Items.Count - 1, true);
             
        }
         
        public void Clear()//-------------------- Очистка Грида и Списка и Предпросмотра  -----------------
        {
            listBox1.Items.Clear();
            textBox1.Clear();
            tabControl1.TabPages.Clear();

            while (dataGridView1.Rows.Count > 1)
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    dataGridView1.Rows.Remove(dataGridView1.Rows[i]);

            while (dT.Columns.Count > 0)
                for (int i = 0; i < dT.Columns.Count; i++)
                    dT.Columns.Remove(dT.Columns[i]);
        }
                
        private void ToExcelOfEiisUrL() // ------ в Ексель из ЕИИС ЮрЛица
        {
            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(Directory.GetCurrentDirectory() + "\\ВыпискаИзЕИИС_ЮрЛиц.xls");//   открываем книгу 
            Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];//Таблица.

            // dt = Zapros( "fix all; select * from    ) ;

            ObjWorkSheet.Cells[5, 3] = dT.Rows[0][9].ToString();
            ObjWorkSheet.Cells[8, 2] = dT.Rows[0][26].ToString();
            ObjWorkSheet.Cells[11, 2] = dT.Rows[0][24].ToString();
            ObjWorkSheet.Cells[20, 3] = dT.Rows[0][24].ToString();
            ObjWorkSheet.Cells[21, 3] = dT.Rows[0][1].ToString();
            ObjWorkSheet.Cells[22, 3] = dT.Rows[0][2].ToString();
            ObjWorkSheet.Cells[23, 3] = dT.Rows[0][12].ToString();
            ObjWorkSheet.Cells[24, 3] = dT.Rows[0][25].ToString();

            ObjExcel.Visible = true;      //  делаем эксель видимым
            ObjExcel.UserControl = true;  //  доступной 
        }

        private void ToExcelOfEiisIp() // ------- в Ексель из ЕИИС ИП
        {
            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(Directory.GetCurrentDirectory() + "\\ВыпискаИзЕИИС_ИП.xls");//   открываем книгу 
            Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];//Таблица.

            // dt = Zapros( "fix all; select * from    ) ;

            ObjWorkSheet.Cells[5, 3] = dT.Rows[0][9].ToString();

            ObjExcel.Visible = true; ObjExcel.UserControl = true;
        }

        private void ToExcelOfXmlUrL() // ------- в Ексель из XML ЮрЛица
        {
            string yVL = "";

            string[] separators = { " ; " };
            string[] mS = (listBox1.SelectedItem.ToString()).Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(Directory.GetCurrentDirectory() + "\\ВыпискаИзXML_ЮрЛиц.xls");//   открываем книгу 
            Excel.Worksheet ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];//Таблица.
             
            ObjWorkSheet.Cells[5, 3] = dS1.Tables[0].Rows[0][1].ToString()  ; // Наименование файла
            ObjWorkSheet.Cells[8, 2] = mS[1]; // полное наименование юридического лица
            ObjWorkSheet.Cells[11, 2] = mS[2] ; // основной государственный регистрационный номер

            Excel.Range rg = (Excel.Range)ObjWorkSheet.Rows[1, Type.Missing]; //    кроем переменную
//**********************************************************************************
            int nS = 97  ; string dateDoc = "";
            yVL = "A" + nS.ToString();
            var cells = ObjWorkSheet.get_Range("A1", "C1");
            foreach (DataRow docRow in dS1.Tables[3].Rows)// перебираем все Документы
            {
                if (docRow["ИдДок"].ToString() == mS[3]) // если нашли 
                {
                    foreach (DataRow SvULRow in docRow.GetChildRows("Документ_СвЮЛ"))
                    {
                     ObjWorkSheet.Cells[19, 3] = SvULRow["ОГРН"].ToString();// ОГРН
                     ObjWorkSheet.Cells[20, 3] = SvULRow["ИНН"].ToString() ;// ИНН
                     ObjWorkSheet.Cells[21, 3] = SvULRow["КПП"].ToString() ;// КПП    
                     
                     dateDoc = SvULRow["ДатаВып"].ToString() ; 
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегОрг"))   
                     {
                         ObjWorkSheet.Cells[23, 3] = orderRow["НаимНО"].ToString();//Регистрирующий орган, в котором находится регистрационное дело 
                     }
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвНаимЮЛ"))
                     {
                         ObjWorkSheet.Cells[24, 3] = orderRow["НаимЮЛПолн"].ToString();// Полное наименование юридического лица  
                         ObjWorkSheet.Cells[25, 3] = orderRow["НаимЮЛСокр"].ToString();// Сокращённое наименование юридического лица
                     }
                      
                     ObjWorkSheet.Cells[26, 3] = " нет данных "  ;// Фирменное наименование юридического лица
 // 2.)_____ Сведения об организационно-правовой форме
                     ObjWorkSheet.Cells[29, 3] = SvULRow["КодОПФ"].ToString();     //  Код классификатора: ОКОПФ, КОПФ  
                     ObjWorkSheet.Cells[30, 3] = SvULRow["ПолнНаимОПФ"].ToString();//   Наименование значения
                     
                     //ObjWorkSheet.Cells[31, 3] = "-";
                     //ObjWorkSheet.Cells[32, 3] = "-";
 // 3.) ____ Сведения об адресе юридического лица           
                    
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвАдресЮЛ"))// Сведения об адресе юридического лица
                     {
                         foreach (DataRow detailRow in orderRow.GetChildRows("СвАдресЮЛ_АдресРФ"))
                         {
                             ObjWorkSheet.Cells[33, 3] = "Адрес постоянно действующего исполнительного органа";// Вид адреса
                             ObjWorkSheet.Cells[34, 3] = "нет данных";//Наименование органа, адрес которого является адресом юридического лица
                             ObjWorkSheet.Cells[35, 3] = detailRow["Индекс"].ToString(); // Почтовый индекс
                             foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Регион"))
                             {
                                 ObjWorkSheet.Cells[36, 3] = subDetRow["НаимРегион"].ToString() + " " + subDetRow["ТипРегион"].ToString()  ;
                             }
                             foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Район"))
                             {
                                 ObjWorkSheet.Cells[37, 3] = subDetRow["НаимРайон"].ToString() + " " + subDetRow["ТипРайон"].ToString();
                             }
                             foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_НаселПункт"))
                             {
                                 ObjWorkSheet.Cells[38, 3] = subDetRow["НаимНаселПункт"].ToString() + " " + subDetRow["ТипНаселПункт"].ToString();
                             }
                             foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Город"))
                             {
                                 ObjWorkSheet.Cells[38, 3] = subDetRow["НаимГород"].ToString() + " " + subDetRow["ТипГород"].ToString()  ;
                             }
                             foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Улица"))
                             {
                                 ObjWorkSheet.Cells[39, 3] = subDetRow["НаимУлица"].ToString() + " " + subDetRow["ТипУлица"].ToString()  ;
                             }
                             ObjWorkSheet.Cells[40, 3] = detailRow["Дом"].ToString();            // Дом
                             try {   ObjWorkSheet.Cells[41, 3] =  detailRow["Корпус"].ToString(); }// Корпус
                             catch {ObjWorkSheet.Cells[41, 3] = "-"  ; }
                             ObjWorkSheet.Cells[42 , 3] = "-";
                             ObjWorkSheet.Cells[42, 3] =  detailRow["Кварт"].ToString()  ;   //Квартира
                          }
                     }
  // ------ Сведения о капитале -------------                
                     ObjWorkSheet.Cells[45, 3] = "нет данных";
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУстКап")) 
                     {
                         ObjWorkSheet.Cells[45, 2] = orderRow["НаимВидКап"].ToString()   ;
                         ObjWorkSheet.Cells[45, 3] =   orderRow["СумКап"].ToString()  ;
                     }
                     
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвОбрЮЛ"))//---------- Сведения об образовании юридического лица   
                     {
                        foreach (DataRow detailRow in orderRow.GetChildRows("СвОбрЮЛ_СпОбрЮЛ"))
                         {
                             ObjWorkSheet.Cells[22, 3] = detailRow["НаимСпОбрЮЛ"].ToString();// Статус юридического лица
                             ObjWorkSheet.Cells[48, 3] = detailRow["НаимСпОбрЮЛ"].ToString()  ;//Вид регистрации
                         }
                         ObjWorkSheet.Cells[49, 3] = orderRow["ОГРН"].ToString();//  ОГРН
                         ObjWorkSheet.Cells[50, 3] = orderRow["ДатаОГРН"].ToString();// Дата регистрации
                         try
                         {
                            // ObjWorkSheet.Cells[nS++, 3] = orderRow["НаимРО"].ToString();
                               if (orderRow["НаимРО"].ToString() == "" )    ObjWorkSheet.Cells[51, 3] = "нет данных"; 
                                }//Регистрирующий орган, зарегистрировавший создание ЮЛ
                           catch { ObjWorkSheet.Cells[51, 3] = "нет данных"; }
                         
                     }
                      
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвПрекрЮЛ"))// Сведения о прекращении деятельности юридического лица
                     {

                         foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_СпПрекрЮЛ"))
                         {
                             try
                             {
                                 ObjWorkSheet.Cells[54, 3] = orderRow["НаимСпПрекрЮЛ"].ToString(); //      Способ прекращения деятельности
                             }
                             catch { ObjWorkSheet.Cells[54, 3] = "нет данных"; }
                         }
                         foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_ГРНДата"))
                         {
                             ObjWorkSheet.Cells[54, 3] = orderRow["ГРН"].ToString() ;// Регистрационный номер
                         }
                         ObjWorkSheet.Cells[54, 3] = orderRow["ДатаПрекрЮЛ"].ToString()  ;// Дата регистрации
                         foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_СвРегОрг"))
                         {
                             ObjWorkSheet.Cells[54, 3] = detailRow["НаимНО"].ToString();// Регистрирующий орган, зарегистрировавший прекращение деятельности ЮЛ
                         }

                     }
                    ObjWorkSheet.Cells[54, 3] = "нет данных";
                         
//10.)_______________ Сведения о ЮЛ, правопреемником которых является данное ЮЛ 
                    ObjWorkSheet.Cells[57, 3] = "нет данных";
                          
                    //11) ________________Сведения о юридических лицах, правопреемниках данного ЮЛ 
                    ObjWorkSheet.Cells[60, 3] = "нет данных";
                         
//12.) _______________ Сведения о лицензиях
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвЛицензия"))
                    {
                        ObjWorkSheet.Cells[63, 3] = orderRow["НомЛиц"].ToString(); // Номер лицензии
                        ObjWorkSheet.Cells[63, 3] = orderRow["НомЛиц"].ToString();
                        ObjWorkSheet.Cells[63, 3] = orderRow["НомЛиц"].ToString();
                        foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_НаимЛицВидДеят"))
                        {
                            ObjWorkSheet.Cells[63, 3] = detailRow["НаимЛицВидДеят"].ToString();// НаимЛицВидДеят
                        }
                        foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_МестоДейстЛиц"))
                        {
                            try { ObjWorkSheet.Cells[63, 3] = detailRow["МестоДейстЛиц"].ToString(); } // МестоДейстЛиц
                            catch { }
                        }
                    }
                    ObjWorkSheet.Cells[63, 3] = "нет данных";

                    //  13) ________________Сведения об обособленных подразделениях ЮЛ
                   ObjWorkSheet.Cells[66, 3] = "нет данных";


                   //  14) ________________Сведения об управляющей компании
                   ObjWorkSheet.Cells[69, 3] = "нет данных";
  
  //  16) ________________ Сведения о постановке на учёт в МНС
                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУчетНО"))
                     {
                         ObjWorkSheet.Cells[72, 3] =  orderRow["ДатаПостУч"].ToString()  ; // Дата постановки на учёт
                         ObjWorkSheet.Cells[73, 3] =  "нет данных"; // Дата снятия с учёта
                         ObjWorkSheet.Cells[74, 3] = "нет данных";
                         ObjWorkSheet.Cells[75, 3] = "нет данных";
                         foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвНО"))
                         {
                             ObjWorkSheet.Cells[74, 3] =  detailRow["НаимНО"].ToString();// Орган МНС   
                             ObjWorkSheet.Cells[75, 3] =   detailRow["КодНО"].ToString();//  Код МНС        
                         }
                     }
 
//17) ________________ Сведения о регистрации в ПФ
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегПФ"))
                        {
                            ObjWorkSheet.Cells[78, 3] = orderRow["РегНомПФ"].ToString()  ;// Регистрационный номер в ПФ
                            ObjWorkSheet.Cells[79, 3] = orderRow["ДатаРег"].ToString()  ; // Дата постановки на учёт 
                            ObjWorkSheet.Cells[80, 3] = "нет данных";// Дата снятия с учёта    
                            ObjWorkSheet.Cells[81, 3] = "нет данных";
                            ObjWorkSheet.Cells[82, 3] = "нет данных";
                             foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвОргПФ"))
                            {
                                ObjWorkSheet.Cells[81, 3] = detailRow["НаимПФ"].ToString()  ;  // Наименование органа, в кот-м зарег-н  
                                ObjWorkSheet.Cells[82, 3] = detailRow["КодПФ"].ToString();  //
                            }
                       }
  
 //18) ________________Сведения о регистрации в ФОМС 
                        ObjWorkSheet.Cells[85, 3] = "нет данных";
                         
//19 _________________Сведения о регистрации в ФСС
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегФСС"))
                        {
                           ObjWorkSheet.Cells[88, 3] = orderRow["РегНомФСС"].ToString() ; // Регистрационный номер
                           ObjWorkSheet.Cells[89, 3] = orderRow["ДатаРег"].ToString() ; //  Дата постановки на учёт 
                           ObjWorkSheet.Cells[90, 3] ="НЕТ ДАННЫХ " ; //  Дата снятия с учёта 
                        }

   
 ////////////////////////////////////////////  Сведения об учредителях  /////////////////////////////////////////////
                         
                        int uRUL = 0, uIUL = 0, uFL = 0; 
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУчредит"))  
                        {
                          
   // Сведения об учредителях - российских ЮЛ
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                            ObjWorkSheet.Cells[nS++, 2] = "Сведения об учредителях - российских ЮЛ";
                            nS = nS + 1;
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвУчредит_УчрЮЛРос")) 
                            {
                                rg = (Excel.Range)ObjWorkSheet.Rows[nS, Type.Missing];
                                for (int i = 0; i < 3; i++)
                                    rg.Insert(Excel.XlInsertShiftDirection.xlShiftDown);// вставляем   строки

                                foreach (DataRow subDetRow in detailRow.GetChildRows("УчрЮЛРос_НаимИННЮЛ"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "ИНН"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["ИНН"].ToString();
                                    ObjWorkSheet.Cells[nS, 2] = "НаимЮЛПолн"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НаимЮЛПолн"].ToString();
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("УчрЮЛРос_ДоляУстКап"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "НоминСтоим"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НоминСтоим"].ToString();
                                }
                                nS = nS + 1;   uRUL++; 
                            }

                            nS = nS + 2;

   // 8.2) __________________Сведения об учредителях - иностранных ЮЛ  
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                            ObjWorkSheet.Cells[nS++, 2] = "Сведения об учредителях - иностранных ЮЛ";
                            
                            nS = nS + 2;

   // Сведения об учредителях - физических лицах
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                            (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                            ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                            ObjWorkSheet.Cells[nS++, 2] = "Сведения об учредителях - физических лицах";
                            nS = nS + 1;  
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвУчредит_УчрФЛ")) 
                            {
                                rg = (Excel.Range)ObjWorkSheet.Rows[nS, Type.Missing];
                                for (int i = 0; i < 17; i++)
                                    rg.Insert(Excel.XlInsertShiftDirection.xlShiftDown);// вставляем 17 строк

                                foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_СвФЛ"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Фамилия"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["Фамилия"].ToString(); // Фамилия
                                    ObjWorkSheet.Cells[nS, 2] = "Имя"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["Имя"].ToString();    // Имя
                                    ObjWorkSheet.Cells[nS, 2] = "Отчество"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["Отчество"].ToString(); // Отчество
                                    ObjWorkSheet.Cells[nS, 2] = "ИНН"; ObjWorkSheet.Cells[nS, 3] = "нет данных"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["ИННФЛ"].ToString();  // ИНН   
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_УдЛичнФЛ"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Вид документа удостоверяющего личность"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НаимДок"].ToString(); //   Вид документа удостоверяющего личность
                                    //ObjWorkSheet.Cells[nS, 2] = "Серия документа"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["СерНомДок"].ToString();// Серия документа  Номер документа
                                    ObjWorkSheet.Cells[nS, 2] = "Номер документа"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["СерНомДок"].ToString();// Серия документа  Номер документа
                                    ObjWorkSheet.Cells[nS, 2] = "Дата выдачи документа"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["ДатаДок"].ToString();     // Дата выдачи документа
                                    ObjWorkSheet.Cells[nS, 2] = "Кем выдан документ"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["ВыдДок"].ToString();      // Кем выдан документ
                                    //ObjWorkSheet.Cells[nS++, 3] = subDetRow["КодВыдДок"].ToString()  ; 
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_АдресМЖРФ"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Почтовый индекс"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["Индекс"].ToString(); // Почтовый индекс
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Регион")) // Субъект РФ
                                    {
                                        ObjWorkSheet.Cells[nS, 2] = "Регион"; ObjWorkSheet.Cells[nS++, 3] = pudSubDetRow["НаимРегион"].ToString() + "  " + pudSubDetRow["ТипРегион"].ToString();
                                    }
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Город")) // Город
                                    {
                                        ObjWorkSheet.Cells[nS, 2] = "Город "; ObjWorkSheet.Cells[nS++, 3] = pudSubDetRow["НаимГород"].ToString() + "  " + pudSubDetRow["ТипГород"].ToString();
                                    }
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Улица")) // Улица
                                    {
                                        ObjWorkSheet.Cells[nS, 2] = "Улица"; ObjWorkSheet.Cells[nS++, 3] = pudSubDetRow["НаимУлица"].ToString() + "  " + pudSubDetRow["ТипУлица"].ToString();
                                    }
                                    ObjWorkSheet.Cells[nS, 2] = "Дом"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["Дом"].ToString();// Дом
                                    ObjWorkSheet.Cells[nS, 2] = "Корпус";
                                    try { ObjWorkSheet.Cells[nS++, 3] = subDetRow["Корпус"].ToString(); } // Корпус
                                    catch { ObjWorkSheet.Cells[nS++, 3] = "-  "; }
                                    ObjWorkSheet.Cells[nS, 2] = "Квартира  "; ObjWorkSheet.Cells[nS++, 3] = "-  " + subDetRow["Кварт"].ToString(); // Квартира
                                    ObjWorkSheet.Cells[nS, 2] = "Адрес местонахождения в стране, резидентом которой является учредитель"; ObjWorkSheet.Cells[nS++, 3] = "-  ";//   Адрес местонахождения в стране, резидентом которой является учредитель
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_ДоляУстКап"))
                                    {
                                        ObjWorkSheet.Cells[nS, 2] = "Размер вклада в уставной капитал учредителя (в рублях)"; ObjWorkSheet.Cells[nS++, 3] = "-   " + pudSubDetRow["НоминСтоим"].ToString(); // Размер вклада в уставной капитал учредителя (в рублях)
                                    }
                                }
                                nS = nS + 2;   uFL++;
                            }
                        }

                         cells = ObjWorkSheet.get_Range(yVL, "C" + (nS-2).ToString());

                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                         
                        ObjWorkSheet.Cells[93, 3] =   uRUL.ToString() ;// Количество учредителей - российских ЮЛ
                        ObjWorkSheet.Cells[94, 3] =    uIUL.ToString() ;// Количество учредителей - иностранных ЮЛ 
                        ObjWorkSheet.Cells[95, 3] =    uFL.ToString() ;// Количество учредителей - физических лиц 
 ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                        //9)___Сведения о физических лицах, имеющих право действовать от имени ЮЛ  
                        
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                        ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                        ObjWorkSheet.Cells[nS++, 2] = "Сведения о физических лицах, имеющих право действовать от имени ЮЛ";
                        yVL = "A" + nS.ToString();
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СведДолжнФЛ"))
                        {
                            foreach (DataRow detailRow in orderRow.GetChildRows("СведДолжнФЛ_СвФЛ"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "Фамилия"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Фамилия"].ToString();// Фамилия
                                ObjWorkSheet.Cells[nS, 2] = "Имя"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Имя"].ToString(); // Имя
                                ObjWorkSheet.Cells[nS, 2] = "Отчество"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Отчество"].ToString(); // Отчество
                                ObjWorkSheet.Cells[nS, 2] = "ИНН"; ObjWorkSheet.Cells[nS++, 3] = detailRow["ИННФЛ"].ToString(); //  ИНН
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СведДолжнФЛ_УдЛичнФЛ"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "Вид документа удостоверяющего личность"; ObjWorkSheet.Cells[nS++, 3] = detailRow["НаимДок"].ToString();// Вид документа удостоверяющего личность
                                ObjWorkSheet.Cells[nS, 2] = "Серия документа"; ObjWorkSheet.Cells[nS++, 3] = detailRow["СерНомДок"].ToString();// Серия документа
                                ObjWorkSheet.Cells[nS, 2] = "Номер документа"; ObjWorkSheet.Cells[nS++, 3] = detailRow["СерНомДок"].ToString();// Номер документа
                                ObjWorkSheet.Cells[nS, 2] = "Дата выдачи документа"; ObjWorkSheet.Cells[nS++, 3] = detailRow["ДатаДок"].ToString();// Дата выдачи документа
                                ObjWorkSheet.Cells[nS, 2] = "Кем выдан документ"; ObjWorkSheet.Cells[nS++, 3] = detailRow["ВыдДок"].ToString();// Кем выдан документ
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СведДолжнФЛ_АдресМЖРФ"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "Почтовый индекс"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Индекс"].ToString(); // Почтовый индекс
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресМЖРФ_Регион"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Субъект РФ"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НаимРегион"].ToString() + " " + subDetRow["ТипРегион"].ToString(); // Субъект РФ
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресМЖРФ_Город"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Город"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НаимГород"].ToString(); // Город
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресМЖРФУлица"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Улица"; ObjWorkSheet.Cells[nS++, 3] = subDetRow["НаимУлица"].ToString();// Улица
                                }
                                ObjWorkSheet.Cells[nS, 2] = "Дом"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Дом"].ToString(); // Дом
                                
                                    ObjWorkSheet.Cells[nS, 2] = "Корпус";
                                    try
                                    {
                                    ObjWorkSheet.Cells[nS++, 3] = detailRow["Корпус"].ToString();// Корпус
                                }
                                catch {
                                    ObjWorkSheet.Cells[nS++, 3] = "-";// Корпус
                                }

                                ObjWorkSheet.Cells[nS, 2] = "Квартира"; ObjWorkSheet.Cells[nS++, 3] = detailRow["Кварт"].ToString();// Квартира   
                                ObjWorkSheet.Cells[nS++, 2] = "Адрес местонахождения в стране, резидентом которой является учредитель"; // Адрес местонахождения в стране, резидентом которой является учредитель
                                ObjWorkSheet.Cells[nS++, 2] = "Телефонный код города"; //Телефонный код города
                                ObjWorkSheet.Cells[nS++, 2] = "Номер телефона"; //Номер телефона
                                ObjWorkSheet.Cells[nS++, 2] = "Номер факса"; //Номер факса
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресМЖРФ_СвДолжн"))
                                {
                                    ObjWorkSheet.Cells[nS, 2] = "Должность"; ObjWorkSheet.Cells[nS++, 3] = detailRow["НаимДолжн"].ToString();//Должность
                                }


                            }


                        }

                        cells = ObjWorkSheet.get_Range(yVL, "C" + (nS - 1).ToString());
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


    //-----------  ОКВЭД'ы ---------------
                        nS = nS + 1;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                        ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                        ObjWorkSheet.Cells[nS++, 2] = "ОКВЭД'ы";
                        yVL = "A" + nS.ToString();
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвОКВЭД"))
                        {
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДОсн"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "ОКВЭД осн.";
                                ObjWorkSheet.Cells[nS++, 3] = detailRow["КодОКВЭД"].ToString();  //  ОКВЭД осн.
                            }
                           // p = nS;
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДДоп"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "ОКВЭД доп.";
                                ObjWorkSheet.Cells[nS++, 3] = detailRow["КодОКВЭД"].ToString(); //  ОКВЭД доп.
                                rg = (Excel.Range)ObjWorkSheet.Rows[nS, Type.Missing]; rg.Insert(Excel.XlInsertShiftDirection.xlShiftDown);// вставляем строку
                            }
                           // if (p == nS) { ObjWorkSheet.Cells[nS, 3] = "ОКВЭД доп."; ObjWorkSheet.Cells[nS, 3] = "нет данных"; }
                        }

                        cells = ObjWorkSheet.get_Range(yVL, "C" + (nS - 1).ToString());
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


                        nS = nS + 2;

    //  15) ________________Сведения о записях в ЕГРЮЛ
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
                        (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3;
                        ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing);
                        ObjWorkSheet.Cells[nS , 2] = "Сведения о записях в ЕГРЮЛ";
                         nS = nS + 2; 
                        yVL = "A" + nS.ToString();
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвЗапЕГРЮЛ"))
                        {
                            rg = (Excel.Range)ObjWorkSheet.Rows[nS, Type.Missing];
                            for (int i = 0; i < 5; i++)
                                rg.Insert(Excel.XlInsertShiftDirection.xlShiftDown);// вставляем 5  строк

                            ObjWorkSheet.Cells[nS, 2] = "Государственный регистрационный номер записи";
                            ObjWorkSheet.Cells[nS++, 3] = orderRow["ГРН"].ToString();
                            ObjWorkSheet.Cells[nS, 2] = "Дата внесения записи ";
                            ObjWorkSheet.Cells[nS++, 3] = orderRow["ДатаЗап"].ToString();
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвРегОрг"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "Регистрирующий орган, осуществивший данный вид регистрации  ";
                                ObjWorkSheet.Cells[nS++, 3] = detailRow["НаимНО"].ToString();
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_ВидЗап"))
                            {
                                ObjWorkSheet.Cells[nS, 2] = "Вид регистрации";
                                ObjWorkSheet.Cells[nS++, 3] = detailRow["НаимВидЗап"].ToString();
                            }
                            ObjWorkSheet.Cells[nS, 2] = "Сведения о состоянии записи";
                            ObjWorkSheet.Cells[nS++, 3] = "НЕТ ДАННЫХ";

                            nS = nS + 1;
                            rg = (Excel.Range)ObjWorkSheet.Rows[nS, Type.Missing];
                            rg.Insert(Excel.XlInsertShiftDirection.xlShiftDown);
                        }

                        cells = ObjWorkSheet.get_Range(yVL, "C" + (nS - 1).ToString());
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние вертикальные
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // внутренние горизонтальные            
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // верхняя внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // правая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous; // левая внешняя
                        cells.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

  ////20) ________________ Сведения о банковских счетах ЮЛ
                     
  //                      (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Bold = true;
  //                      (ObjWorkSheet.Cells[nS, 2] as Excel.Range).Font.Size = 12;
  //                      (ObjWorkSheet.Cells[nS, 2] as Excel.Range).HorizontalAlignment = 3 ;
  //                      ObjWorkSheet.Range[ObjWorkSheet.Cells[nS, 2], ObjWorkSheet.Cells[nS, 3]].Merge(Type.Missing ) ;
  //                      ObjWorkSheet.Cells[nS++, 2] = "Сведения о банковских счетах ЮЛ";
                          
  //                     ObjWorkSheet.Cells[nS , 3] = "нет данных";

  //                     foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегОрг"))
  //                         ObjWorkSheet.Cells[nS , 3] =  orderRow["НаимНО"].ToString()  ;
                       
  //                     nS = nS + 2;
                    }
                }//if

                


            }
            //*********************************************************************
            nS++;
            yVL = "A" + nS;
            
            ObjWorkSheet.Cells[nS, 2] = "Должность ответственного лица";
            ObjWorkSheet.Cells[nS++, 3] = dS1.Tables[1].Rows[0][1].ToString();
            ObjWorkSheet.Cells[nS, 2] = "Фамилия, Имя, Отчество";
            ObjWorkSheet.Cells[nS++, 3] = dS1.Tables[2].Rows[0][0].ToString() + " " + dS1.Tables[2].Rows[0][1].ToString() + " " + dS1.Tables[2].Rows[0][2].ToString();
            ObjWorkSheet.Cells[nS, 2] = "Телефон";
            ObjWorkSheet.Cells[nS++, 3] = dS1.Tables[1].Rows[0][2].ToString(); 
            ObjWorkSheet.Cells[nS, 2] = "Дата формирования документа в МНС";
            ObjWorkSheet.Cells[nS++, 3] = dateDoc;
            ObjWorkSheet.Cells[nS, 2] = "Дата формирования выписки";
            ObjWorkSheet.Cells[nS++, 3] = DateTime.Now.ToShortDateString();

            cells = ObjWorkSheet.get_Range(yVL, "C" + (nS - 1).ToString());
            cells.Font.Bold = true;
            cells.Font.Size = 12;

            ObjExcel.Visible = true;      //  делаем эксель видимым
            ObjExcel.UserControl = true;  //  доступной 
        }

        private void ToExcelOfXmlIp() // -------  в Ексель из XML ИП
        {
         
        }

        DataTable Zapros(string FileNameSQL)//--------- 
        {
            string[] s = File.ReadAllLines(FileNameSQL);
            s[0] = "var @dat1='" + dateTimePicker1.Value.ToString("dd-MM-yyyy") + "';";
            s[1] = "var @dat2='" + dateTimePicker2.Value.ToString("dd-MM-yyyy") + "';";
            File.WriteAllLines(FileNameSQL, s);
            string strZapr = File.ReadAllText(FileNameSQL);
            try
            {
                OdbcConnection conn = new OdbcConnection(connectionString);
                conn.Open();
                OdbcDataAdapter da = new OdbcDataAdapter(strZapr, conn);
                da.Fill(dT);

                return dT;
            }
            catch {
                MessageBox.Show("Нет подключения к БД ЕИИС, проверьте Настройки и конфигурацию ViPNet ");
                return null;
            }
        }

        private void PreViewUL(string idDoc, RichTextBox Box, string ogrn, string polnName)//----- предпросмотр ЮЛ
        {
         Box.SelectionFont = new Font("Tahoma", 12, FontStyle.Bold);
         Box.AppendText("                                     ВЫПИСКА                           \r\n");
         Box.AppendText("                из Единого государственного реестра юридических лиц    \r\n");

         Box.AppendText("Наименование файла:  " + dS1.Tables[0].Rows[0][1].ToString() + "\r\n"); //  RUM_27040_160102_46		

         Box.AppendText("Настоящая выписка содержит сведения о юридическом лице  \r\n	");
         Box.AppendText(polnName + "\r\n");
         Box.AppendText("(полное наименование юридического лица)	           \r\n ");

         Box.AppendText("                          " + ogrn + "\r\n");
         Box.AppendText("(основной государственный регистрационный номер)            \r\n ");
         Box.AppendText("включенные в Единый государственный реестр юридических лиц по месту	\r\n");
         Box.AppendText("нахождения данного юридического лица, по следующим показателям:	     \r\n");
             


            foreach (DataRow docRow in dS1.Tables[3].Rows)// перебираем все Документы
            {
                if (docRow["ИдДок"].ToString() == idDoc) // если нашли 
                {
                    Box.AppendText("ИдДок:  " + docRow["ИдДок"].ToString() + "\r\n\r\n");
                    foreach (DataRow SvULRow in docRow.GetChildRows("Документ_СвЮЛ"))
                    {
                        Box.AppendText("\r\n   1) ________________Основные идентифицирующие сведения о юридическом лице\r\n");   
                        Box.AppendText("       ИНН         " + SvULRow["ИНН"].ToString() + "\r\n");
                        Box.AppendText("       ОГРН        " + SvULRow["ОГРН"].ToString() + "\r\n");
                        Box.AppendText("       КПП         " + SvULRow["КПП"].ToString() + "\r\n");
                        Box.AppendText("\r\n   2) ________________Сведения об организационно-правовой форме\r\n"); 
                        Box.AppendText("       КодОПФ      " + SvULRow["КодОПФ"].ToString() + "\r\n");     //    
                        Box.AppendText("       ПолнНаимОПФ " + SvULRow["ПолнНаимОПФ"].ToString() + "\r\n");// Сведения об организационно-правовой форме

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвОКВЭД")) //-----------  ОКВЭД'ы ---------------
                        {
                            Box.AppendText("\r\n   3) ________________ОКВЭД'ы \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДОсн"))
                            {
                                Box.AppendText("     ОКВЭД осн. " + detailRow["КодОКВЭД"].ToString() + "\r\n");
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДДоп"))
                            {
                                Box.AppendText("     ОКВЭД доп. " + detailRow["КодОКВЭД"].ToString() + "\r\n");
                            }
                        }

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвАдресЮЛ"))// Сведения об адресе юридического лица
                        {
                            Box.AppendText("\r\n   4) ________________Сведения об адресе юридического лица \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвАдресЮЛ_АдресРФ"))
                            {
                                Box.AppendText("     Индекс  " + detailRow["Индекс"].ToString() + "\r\n");
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Регион"))
                                {
                                    Box.AppendText("     Субъект РФ " + subDetRow["НаимРегион"].ToString() + " " + subDetRow["ТипРегион"].ToString() + "\r\n");
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Город"))
                                {
                                    Box.AppendText("     Город " + subDetRow["НаимГород"].ToString() + " " + subDetRow["ТипГород"].ToString() + "\r\n");
                                }
                                foreach (DataRow subDetRow in detailRow.GetChildRows("АдресРФ_Улица"))
                                {
                                    Box.AppendText("     Улица " + subDetRow["НаимУлица"].ToString() + " " + subDetRow["ТипУлица"].ToString() + "\r\n");
                                }
                                Box.AppendText("     Номер дома  " + detailRow["Дом"].ToString() + "\r\n");
                                try{Box.AppendText("     Корпус  " + detailRow["Корпус"].ToString() + "\r\n");}catch { }
                                Box.AppendText("     Квартира  " + detailRow["Кварт"].ToString() + "\r\n");

                            }
                        }

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУстКап")) // ------ Сведения о капитале -------------
                        {
                            Box.AppendText("\r\n   5) ________________Сведения о капитале\r\n");
                            Box.AppendText("     Размер капитала (в рублях)   " + orderRow["НаимВидКап"].ToString() + " " + orderRow["СумКап"].ToString() + "\r\n");
                        }

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвОбрЮЛ"))//---------- Сведения об образовании юридического лица   
                        {
                            Box.AppendText("\r\n   6) ________________Сведения об образовании юридического лица\r\n");
                            Box.AppendText("     ОГРН или регистрационный номер для ЮЛ созданных до 01.07.2002  " + orderRow["ОГРН"].ToString() + "\r\n");// + "  " + orderRow["РегНом"].ToString()
                            Box.AppendText("     Дата регистрации  " + orderRow["ДатаОГРН"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОбрЮЛ_СпОбрЮЛ"))
                            {
                                Box.AppendText("     Вид регистрации  " + detailRow["НаимСпОбрЮЛ"].ToString() + "\r\n");
                            }
                            try { Box.AppendText("     Регистрирующий орган, зарегистрировавший создание ЮЛ  " + orderRow["НаимРО"].ToString() + "\r\n"); }catch{}
                        }

                        Box.AppendText("\r\n   7) ________________Сведения о прекращении деятельности юридического лица\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвПрекрЮЛ"))
                        {
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_СпПрекрЮЛ"))
                            {
                                try
                                {
                                    Box.AppendText("     Способ прекращения деятельности   " + orderRow["НаимСпПрекрЮЛ"].ToString() + "\r\n");
                                }
                                catch { Box.AppendText("     Способ прекращения деятельности   -  нет данных  \r\n"); }
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_ГРНДата"))
                            {
                                Box.AppendText("     Регистрационный номер   " + orderRow["ГРН"].ToString() + "\r\n");
                            }
                            Box.AppendText("     Дата регистрации   " + orderRow["ДатаПрекрЮЛ"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвПрекрЮЛ_СвРегОрг"))
                            {
                                Box.AppendText("     Регистрирующий орган, зарегистрировавший прекращение деятельности ЮЛ   " + detailRow["НаимНО"].ToString() + "\r\n");
                            }
                        }
                        // 
                        // СвПредш
                        // СвДоляООО
                        // СвДолжнФЛ
                         
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУчредит"))//  Сведения об учредителях   
                        {
                            Box.AppendText("\r\n   8) ________________Сведения об учредителях\r\n");
                            Box.AppendText("     Количество учредителей - российских  ЮЛ   - \r\n");
                            Box.AppendText("     Количество учредителей - иностранных ЮЛ   - \r\n");
                            Box.AppendText("     Количество учредителей - физических  лиц  - \r\n");

                            Box.AppendText("\r\n     8.1) __________________Сведения об учредителях - российских ЮЛ\r\n");

                            Box.AppendText("\r\n     8.2) __________________Сведения об учредителях - иностранных ЮЛ\r\n");

                            Box.AppendText("\r\n     8.3) __________________Сведения об учредителях - физических лицах\r\n");// Сведения об учредителях - физических лицах
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвУчредит_УчрФЛ"))
                            {
                              foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_СвФЛ"))
                              { 
                                  Box.AppendText("     Фамилия   " + subDetRow["Фамилия"].ToString() + "\r\n");
                                  Box.AppendText("     Имя       " + subDetRow["Имя"].ToString() + "\r\n");
                                  Box.AppendText("     Отчество  " + subDetRow["Отчество"].ToString() + "\r\n");
                                  Box.AppendText("     ИНН       " + subDetRow["ИННФЛ"].ToString() + "\r\n");
                              }
                              foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_СвРождФЛ"))
                              {
                                  Box.AppendText("     Дата рождения   " + subDetRow["ДатаРожд"].ToString() + "\r\n");
                                  Box.AppendText("     Место рождения  " + subDetRow["МестоРожд"].ToString() + "\r\n");   
                              }
                              foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_УдЛичнФЛ"))
                              {
                                  Box.AppendText("     КодВидДок   " + subDetRow["КодВидДок"].ToString() + "\r\n");
                                  Box.AppendText("     НаимДок  " + subDetRow["НаимДок"].ToString() + "\r\n");
                                  Box.AppendText("     СерНомДок   " + subDetRow["СерНомДок"].ToString() + "\r\n");
                                  Box.AppendText("     ДатаДок  " + subDetRow["ДатаДок"].ToString() + "\r\n");
                                  Box.AppendText("     ВыдДок   " + subDetRow["ВыдДок"].ToString() + "\r\n");
                                  Box.AppendText("     КодВыдДок  " + subDetRow["КодВыдДок"].ToString() + "\r\n"); 
                              }
                              foreach (DataRow subDetRow in detailRow.GetChildRows("УчрФЛ_АдресМЖРФ"))
                              {
                                  Box.AppendText("     КодРегион    " + subDetRow["КодРегион"].ToString() + "\r\n");
                                  Box.AppendText("     КодАдрКладр  " + subDetRow["КодАдрКладр"].ToString() + "\r\n");
                                  Box.AppendText("     Дом          " + subDetRow["Дом"].ToString() + "\r\n");
                                  try { Box.AppendText("     Корпус       " + subDetRow["Корпус"].ToString() + "\r\n"); }catch { }
                                  Box.AppendText("     Кварт        " + subDetRow["Кварт"].ToString() + "\r\n"); 
                               foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Регион"))
                               {
                                   Box.AppendText("     ТипРегион   " + pudSubDetRow["ТипРегион"].ToString() + "\r\n");
                                   Box.AppendText("     НаимРегион  " + pudSubDetRow["НаимРегион"].ToString() + "\r\n"); 
                               }
                               foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Город"))
                               {
                                   Box.AppendText("     ТипГород    " + pudSubDetRow["ТипГород"].ToString() + "\r\n");
                                   Box.AppendText("     НаимГород   " + pudSubDetRow["НаимГород"].ToString() + "\r\n"); 
                               }
                               foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_Улица"))
                               {
                                   Box.AppendText("     ТипУлица    " + pudSubDetRow["ТипУлица"].ToString() + "\r\n");
                                   Box.AppendText("     НаимУлица   " + pudSubDetRow["НаимУлица"].ToString() + "\r\n"); 
                               }
                               foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресМЖРФ_ДоляУстКап"))
                               {
                                   Box.AppendText("     НоминСтоим    " + pudSubDetRow["НоминСтоим"].ToString() + "\r\n"); 
                                   foreach (DataRow pudPudSubDetRow in pudSubDetRow.GetChildRows("ДоляУстКап_РазмерДоли"))
                                   {
                                      foreach (DataRow pud3PudSubDetRow in pudPudSubDetRow.GetChildRows("РазмерДоли_Процент"))
                                      {
                                      Box.AppendText("     Процент    " + pud3PudSubDetRow["Процент"].ToString() + "\r\n");
                                      }
                                   }
                               }
                               Box.AppendText("- - - - - - - - - - - - - - - - \r\n");
                              }//..................................................................................................

                            }
                        }

                        Box.AppendText("\r\n   9) ________________Сведения о физических лицах, имеющих право действовать от имени ЮЛ \r\n");//Сведения о физических лицах, имеющих право действовать от имени ЮЛ 

                        Box.AppendText("\r\n  10) ________________Сведения о ЮЛ, правопреемником которых является данное ЮЛ \r\n");
                        Box.AppendText("\r\n  11) ________________Сведения о юридических лицах, правопреемниках данного ЮЛ \r\n");
                        
  Box.AppendText("\r\n  12) ________________Сведения о лицензиях \r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвЛицензия")) 
                        {
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_НаимЛицВидДеят"))
                            {
                                Box.AppendText("     НаимЛицВидДеят  " + detailRow["НаимЛицВидДеят"].ToString() + "\r\n");
                            }
                            
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_МестоДейстЛиц"))
                            {
                                try { Box.AppendText("     МестоДейстЛиц  " + detailRow["МестоДейстЛиц"].ToString() + "\r\n"); }
                                catch { }
                            }
                        }

                        Box.AppendText("\r\n  13) ________________Сведения об обособленных подразделениях ЮЛ \r\n");
                        Box.AppendText("\r\n  14) ________________Сведения об управляющей компании \r\n");

                        Box.AppendText("\r\n  15) ________________Сведения о записях в ЕГРЮЛ \r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвЗапЕГРЮЛ"))
                        {
                            Box.AppendText("     Государственный регистрационный номер записи  " + orderRow["ГРН"].ToString() + "\r\n");
                            Box.AppendText("     Дата внесения записи  " + orderRow["ДатаЗап"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвРегОрг"))
                            {
                                Box.AppendText("     Регистрирующий орган, осуществивший данный вид регистрации  " + detailRow["НаимНО"].ToString() + "\r\n");
                            }
                            Box.AppendText("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  \r\n");
                        }

                        Box.AppendText("\r\n  16) ________________Сведения о постановке на учёт в МНС\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвУчетНО"))
                        {
                            Box.AppendText("     Дата постановки на учёт  " + orderRow["ДатаПостУч"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта       \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвНО"))
                            {
                                Box.AppendText("     Орган МНС                " + detailRow["НаимНО"].ToString() + "\r\n");
                                Box.AppendText("     Код МНС                  " + detailRow["КодНО"].ToString() + "\r\n");
                            }
                        }

                        Box.AppendText("\r\n  17) ________________Сведения о регистрации в ПФ\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегПФ"))
                        {
                            Box.AppendText("     Регистрационный номер в ПФ              " + orderRow["РегНомПФ"].ToString() + "\r\n");
                            Box.AppendText("     Дата постановки на учёт                 " + orderRow["ДатаРег"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта                      \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвОргПФ"))
                            {
                                Box.AppendText("     Наименование органа, в кот-м зарег-н ИП " + detailRow["НаимПФ"].ToString() + "\r\n"); 
                            }
                        }
                     
                        Box.AppendText("\r\n  18) ________________Сведения о регистрации в ФОМС\r\n");
                         
                        Box.AppendText("\r\n  19) ________________Сведения о регистрации в ФСС\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегФСС"))
                        {
                            Box.AppendText("     Регистрационный номер     " + orderRow["РегНомФСС"].ToString() + "\r\n");
                            Box.AppendText("     Дата постановки на учёт  " + orderRow["ДатаРег"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта        \r\n");
                        }


                        Box.AppendText("\r\n  20) ________________Сведения о банковских счетах ЮЛ\r\n"); 

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвНаимЮЛ"))
                        {
                            Box.AppendText("     НаимЮЛПолн " + orderRow["НаимЮЛПолн"].ToString() + "\r\n");
                            Box.AppendText("     НаимЮЛСокр " + orderRow["НаимЮЛСокр"].ToString() + "\r\n");
                        }

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвЮЛ_СвРегОрг"))
                        {
                            Box.AppendText("     НаимНО " + orderRow["НаимНО"].ToString() + "\r\n");
                        }


                        

                        
                        




                         
                    }
                }
            }
        }

        private void PreViewIP(string idDoc, RichTextBox Box, string ogrnip, string fio) //------- предпросмотр ИП
        {
            Box.AppendText("                                     ВЫПИСКА                           \r\n");
            Box.AppendText("          из Единого государственного реестра индивидуальных предпринимателей    \r\n");

            Box.AppendText("Наименование файла:  " + "dS1.Tables[0].Rows[0][1].ToString()" + "\r\n"); //  RUM_27040_160102_46		

            Box.AppendText("Настоящая выписка содержит сведения о индивидуальном предпринимателе  \r\n	");
            Box.AppendText(fio + "\r\n");
            Box.AppendText("                  (ФИО индивидуального предпринимателя)	           \r\n ");

            Box.AppendText("                          " + ogrnip + "          \r\n");
            Box.AppendText("(основной государственный регистрационный номер)            \r\n ");
            Box.AppendText("включенные в Единый государственный реестр индивидуальных предпринимателей по месту	\r\n");
            Box.AppendText("нахождения данного индивидуального предпринимателя, по следующим показателям:	     \r\n");

            foreach (DataRow docRow in dS1.Tables[3].Rows)// перебираем все Документы
            {
                if (docRow["ИдДок"].ToString() == idDoc) // если нашли 
                {
                    Box.AppendText("ИдДок:  " + docRow["ИдДок"].ToString() + "\r\n\r\n");
                    foreach (DataRow SvULRow in docRow.GetChildRows("Документ_СвИП"))
                    {
                        Box.AppendText("\r\n   1) _____Основные идентифицирующие сведения о индивидуальном предпринимателе\r\n");
                        Box.AppendText("       ИНН страхователя  " + SvULRow["ИННФЛ"].ToString() + "\r\n");
                        Box.AppendText("       Дата ОГРН ИП      " + SvULRow["ДатаОГРНИП"].ToString() + "\r\n");
                        Box.AppendText("       Состояние в МНС   " + SvULRow["ОГРНИП"].ToString() + "\r\n");
                        Box.AppendText("       ОГРН ИП           " + SvULRow["ОГРНИП"].ToString() + "\r\n");
                        Box.AppendText("       Вид ИП            " + SvULRow["НаимВидИП"].ToString() + "\r\n");
                        Box.AppendText("       Регистрирующий орган в кот-м нах-ся регистрационное дело " + SvULRow["ОГРНИП"].ToString() + "\r\n");

                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвОКВЭД")) //---   ОКВЭД'ы ---------------
                        {
                            Box.AppendText("\r\n   2) ________________ОКВЭД'ы \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДОсн"))
                            {
                                Box.AppendText("     ОКВЭД осн. " + detailRow["КодОКВЭД"].ToString() + "\r\n");
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвОКВЭД_СвОКВЭДДоп"))
                            {
                                Box.AppendText("     ОКВЭД доп. " + detailRow["КодОКВЭД"].ToString() + "\r\n");
                            }
                        }

                        Box.AppendText("\r\n   3) ________________Сведения о физическом лице\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвФЛ"))
                        {
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвФЛ_ФИОРус"))
                            {
                                Box.AppendText("Фамилия                     " + detailRow["Фамилия"].ToString() + "\r\n");
                                Box.AppendText("Имя                         " + detailRow["Имя"].ToString() + "\r\n");
                                Box.AppendText("Отчество                    " + detailRow["Отчество"].ToString() + "\r\n");
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвФЛ_ФИОЛат"))
                            {
                                Box.AppendText("Фамилия латинскими буквами  " + detailRow["Фамилия"].ToString() + "\r\n");
                                Box.AppendText("Имя латинскими буквами      " + detailRow["Имя"].ToString() + "\r\n");
                                Box.AppendText("Отчество латинскими буквами " + detailRow["Отчество"].ToString() + "\r\n");
                            }
                            Box.AppendText("Пол                         " + orderRow["Пол"].ToString() + "\r\n");
                        }
                        foreach (DataRow detailRow in SvULRow.GetChildRows("СвИП_СвРожд"))
                        {
                            Box.AppendText("Дата рождения               " + detailRow["ДатаРожд"].ToString() + "\r\n");
                            Box.AppendText("Место рождения              " + detailRow["МестоРожд"].ToString() + "\r\n");
                        }

                        Box.AppendText("\r\n   4) ________________Сведения о гражданстве \r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвГражд"))//  
                        {
                            Box.AppendText("     Вид гражданства  " + orderRow["ВидГражд"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвГражд_СвАдрМЖ"))
                            {
                                foreach (DataRow subDetRow in detailRow.GetChildRows("СвАдрМЖ_АдресРФ"))
                                {
                                    Box.AppendText("     Почтовый индекс   " + subDetRow["Индекс"].ToString() + "\r\n");
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресРФ_Регион"))
                                    {
                                        Box.AppendText("     Регион            " + pudSubDetRow["НаимРегион"].ToString() + "  " + pudSubDetRow["ТипРегион"].ToString() + "\r\n");
                                    }
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресРФ_Город"))
                                    {
                                        Box.AppendText("     Город            " + pudSubDetRow["НаимГород"].ToString() + "  " + pudSubDetRow["ТипГород"].ToString() + "\r\n");
                                    }
                                    foreach (DataRow pudSubDetRow in subDetRow.GetChildRows("АдресРФ_Улица"))
                                    {
                                        Box.AppendText("     Улица            " + pudSubDetRow["НаимУлица"].ToString() + "  " + pudSubDetRow["ТипУлица"].ToString() + "\r\n");
                                    }
                                    Box.AppendText("     Дом   " + subDetRow["Дом"].ToString() + "\r\n");
                                    Box.AppendText("     Корпус   " + subDetRow["Корпус"].ToString() + "\r\n");
                                    Box.AppendText("     Квартира   " + subDetRow["Кварт"].ToString() + "\r\n");
                                }
                            }
                        }


                        Box.AppendText("\r\n   6) ________________Сведения документа, удостоверяющего личность \r\n");
                        Box.AppendText("\r\n   7) ________________Сведения документа, подтверждающего право на проживание в РФ \r\n");

                        Box.AppendText("\r\n   8) ________________Сведения документа, подтвержд. приобретение дееспособности несовершеннолетним\r\n");
                        Box.AppendText("\r\n  9) ________________Сведения о лицензиях \r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвЛицензия"))
                        {
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            Box.AppendText("     НомЛиц  " + orderRow["НомЛиц"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_НаимЛицВидДеят"))
                            {
                                Box.AppendText("     НаимЛицВидДеят  " + detailRow["НаимЛицВидДеят"].ToString() + "\r\n");
                            }
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЛицензия_МестоДейстЛиц"))
                            {
                                Box.AppendText("     МестоДейстЛиц  " + detailRow["МестоДейстЛиц"].ToString() + "\r\n");
                            }
                        }


                        Box.AppendText("\r\n   10) ________________Сведения о рег. ФЗ в качестве ИП до 01.01.2004\r\n");
                        Box.AppendText("\r\n   11) ________________Сведения о записях в ЕГРИП\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвЗапЕГРЮЛ"))
                        {
                            Box.AppendText("     Государственный регистрационный номер записи  " + orderRow["ГРН"].ToString() + "\r\n");
                            Box.AppendText("     Дата внесения записи  " + orderRow["ДатаЗап"].ToString() + "\r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвРегОрг"))
                            {
                                Box.AppendText("     Регистрирующий орган, осуществивший данный вид регистрации  " + detailRow["НаимНО"].ToString() + "\r\n");
                            }
                            Box.AppendText("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  \r\n");
                        }

                        Box.AppendText("\r\n   12) ________________Сведения о постановке на учёт в МНС\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвУчетНО"))
                        {
                            Box.AppendText("     Дата постановки на учёт  " + orderRow["ДатаПостУч"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта       \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвНО"))
                            {
                                Box.AppendText("     Орган МНС                " + detailRow["НаимНО"].ToString() + "\r\n");
                                Box.AppendText("     Код МНС                  " + detailRow["КодНО"].ToString() + "\r\n");
                            }
                        }

                        Box.AppendText("\r\n   13) ________________Сведения о регистрации в ПФ\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвРегПФ"))
                        {
                            Box.AppendText("     Регистрационный номер в ПФ              " + orderRow["РегНомПФ"].ToString() + "\r\n");
                            Box.AppendText("     Дата постановки на учёт                 " + orderRow["ДатаРег"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта                      \r\n");
                            foreach (DataRow detailRow in orderRow.GetChildRows("СвЗапЕГРЮЛ_СвОргПФ"))
                            {
                                Box.AppendText("     Наименование органа, в кот-м зарег-н ИП " + detailRow["НаимПФ"].ToString() + "\r\n");
                            }
                        }

                        Box.AppendText("\r\n   14) ________________Сведения о регистрации в ФОМС\r\n");

                        Box.AppendText("\r\n   15) ________________Сведения о регистрации в ФСС\r\n");
                        foreach (DataRow orderRow in SvULRow.GetChildRows("СвИП_СвРегФСС"))
                        {
                            Box.AppendText("     Регистрационный номер     " + orderRow["РегНомФСС"].ToString() + "\r\n");
                            Box.AppendText("     Дата постановки на учёт  " + orderRow["ДатаРег"].ToString() + "\r\n");
                            Box.AppendText("     Дата снятия с учёта        \r\n");
                        }

                        Box.AppendText("\r\n   16) ________________Сведения о банковских счетах ЮЛ\r\n");


                    }


                } //  if


            }// foreach


        }







    }
}




 
