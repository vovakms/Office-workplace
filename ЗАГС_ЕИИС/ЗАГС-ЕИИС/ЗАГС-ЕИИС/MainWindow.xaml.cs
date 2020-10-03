using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;
using System.Data.OleDb;
//using System.Windows.Forms;

 

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
  
using System.Collections;
using System.Collections.Generic;


using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Data.Odbc;

namespace ЗАГС_ЕИИС
{
    
    public partial class MainWindow : Window
    {
        bool in1 = false;
        bool in2 = false;

        string conStr1 = "";
        string conStr2 = "";

        private Excel.Application excelapp;
        private Excel.Window      excelWindow;

        private Word.Application  wordapp;
        private Word.Window       wordWindow;

        private Excel.Application ExcelApp;
        private Excel.Workbook    WorkBookExcel;
        private Excel.Worksheet   WorkSheetExcel;
        private Excel.Range       RangeExcel;
         
        public MainWindow()
        {
            InitializeComponent();
 
        }
         
        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
         conStr1 = "Dsn=" + Properties.Settings.Default.Источн1 + ";uid=" + Properties.Settings.Default.Логин1 + ";srv=tcpip:/" + Properties.Settings.Default.СерверБД1 + ";sn=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";ct=N;fixall=Y;msjet=N";
         conStr2 = "Dsn=" + Properties.Settings.Default.Источн2 + ";uid=" + Properties.Settings.Default.Логин2 + ";srv=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";sn=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";ct=N;fixall=Y;msjet=N";

            if (Properties.Settings.Default.ОдинИсточн == true)
            {
                conStr2 = conStr1;
                Properties.Settings.Default.ОдинИсточн = false;
            }
            else
            {
                conStr2 = conStr1;
            }

            if (Properties.Settings.Default.ПервыйЗапПрог == true)
            {
                Properties.Settings.Default.ПервыйЗапПрог = false;
                Window1 win1 = new Window1();
                win1.Show();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)   // кн. Свернуть
        {
            Window1.WindowState = WindowState.Minimized ;
        }

        private void button3_Click(object sender, RoutedEventArgs e)   // кн. Востановить
        {
            if (Window1.WindowState == WindowState.Normal)
                Window1.WindowState = WindowState.Maximized;
            else
                Window1.WindowState = WindowState.Normal;
        }
        
        private void button4_Click(object sender, RoutedEventArgs e)   // кн. Закрыть
        {
            Window1.Close();
        }

        private void menuItem21_Click(object sender, RoutedEventArgs e)// кн. меню "Настройки"
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)//свернуть/развернуть окно двойным кликом по menu
        {
            if (Window1.WindowState == WindowState.Normal)
                Window1.WindowState = WindowState.Maximized;
            else
                Window1.WindowState = WindowState.Normal;
        }

        private void menu1_Copy_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)// перемещение формы взяв за меню
        {
            Window1.DragMove();
        }
        private void progressBar1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)// перемещение формы взяв за ПрогресБар
        {
            Window1.DragMove();
        }
        private void button1_Click(object sender, RoutedEventArgs e)  // кн. Открыть выбранные файлы
        {
             
            DataTable dt = new DataTable();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            ofd.Title = "Выберите документ для загрузки данных";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = System.IO.Path.GetDirectoryName(ofd.FileName);
               
                spisFile(ofd);
                foreach (string fN in ofd.FileNames)
                {
                    try
                    {
                        dt.Merge(Excel2dataTable(fN));
                    }
                    catch {
                        textBox2.AppendText("\n Откройте файл \n" + fN + "\n проверте поля с датами \n");
                        tabControl2.SelectedItem = tabItem5;
                    }
                    
                }
                dataGrid2.ItemsSource = dt.DefaultView;
                label2.Content = dt.Rows.Count.ToString();
                label2.Visibility = Visibility.Visible;
                label3.Visibility = Visibility.Visible;
                label4.Visibility = Visibility.Visible;
                label3.Content = "строк в выбранных файлах";
                label4.Content = "(первая строка в каждом файле не считается, т.к. служит для названий столбиков)";
                tabControl1.SelectedIndex = 0;
            }

            
        }
        
        private void ButTB1_Click(object sender, RoutedEventArgs e)   // кн. Сохранить в Excel
        {
            //textBlock2.Text = "Открываем файл Ексель";

            excelapp = new Excel.Application();
            excelapp.Visible = true;

            excelapp.SheetsInNewWorkbook = 3;
            excelapp.Workbooks.Add(Type.Missing);
            excelapp.SheetsInNewWorkbook = 5;
            excelapp.Workbooks.Add(Type.Missing);

        }

        private void Button_Click(object sender, RoutedEventArgs e)   // кн. Сохранить в Word
        {
            SaveWord();
           
        }

        private void button7_Click(object sender, RoutedEventArgs e)  // кн. Сверить
        {
            tabControl2.SelectedItem = tabItem6;

            if (tabControl1.SelectedIndex == 1)
            {
                Sverka1();
            }

            if (tabControl1.SelectedIndex == 2)
            {
                Sverka2();
            }


        }

        private void dataGrid2_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e) // происходит при создании столбика
        {
            if (e.PropertyType == typeof(DateTime))// проверяем столбик  и если он с датой  
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";// тогда задаем формат
        }
        
        private void gridSplitter1_MouseDoubleClick(object sender, MouseButtonEventArgs e)//складываем левую панельку
        {
            col0.Width = new GridLength(1);
        }

        private void SaveWord()
        {
            try
            {
                DataTable dT = DataGrid2DataTable(dataGrid5);   // создаем и заполняем таблицу
                DataTable dTF = DataGrid2DataTable(dataGrid1);
           

            string imF = "\\АКТ_" + DateTime.Now.ToString("yyyy-MM-dd (H-mm-ss)") + ".docx";

            File.Copy(System.Windows.Forms.Application.StartupPath + "\\АКТ.docx", textBox1.Text + imF);
            //-----------------------------------------------------------------------------------------
            Word._Application application = new Word.Application(); //создаем обьект приложения word
            Word._Document document = new Word.Document();
            Object missingObj = System.Reflection.Missing.Value;
            Object trueObj = true;
            Object falseObj = false;

            Object templatePathObj = textBox1.Text + imF; // создаем путь к файлу

            try  // если вылетим не на этом этапе, приложение останется открытым
            {
                document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj);
            }
            catch (Exception error) // вылитаем по ошибке
            {
                document.Close(ref falseObj, ref missingObj, ref missingObj);
                application.Quit(ref missingObj, ref missingObj, ref missingObj);
                document = null;
                application = null;
                throw error;
            }

            application.Selection.Find.ClearFormatting();        //Очищаем параметры поиска
            application.Selection.Find.Replacement.ClearFormatting();

            
            object findText = "%Дата%";                     //  выставляем ДАТУ ///////////////////
            object replaceWith = DateTime.Now.ToString("dd MMMM ") ;
            object replace = 2;
            application.Selection.Find.Execute(ref findText, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith,
            ref replace, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            object findText2 = "%КолвоФайлов%";     //  выставляем Количество ФАЙЛОВ ///////////////////
            object replaceWith2 = label.Content.ToString();
            object replace2 = 2;
            application.Selection.Find.Execute(ref findText2, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith2,
            ref replace2, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            object findText3 = "%КолвЧелВФайле%";  //  выставляем количество человек в файлах ///////////////////
            object replaceWith3 = (dataGrid2.Items.Count - dataGrid1.Items.Count - dataGrid1.Items.Count).ToString() ;
            object replace3 = 2;
            application.Selection.Find.Execute(ref findText3, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith3,
            ref replace3, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            object findText4 = "%КолПолСтрВыпл%";  //  выставляем количество получателей страховых выплат  //////////
            object replaceWith4 = (dataGrid3.Items.Count - 1).ToString();
            object replace4 = 2;
            application.Selection.Find.Execute(ref findText4, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith4,
            ref replace4, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            object findText5 = "%КолСовпСтр%";  //  выставляем количество совпавших строк  //////////
            object replaceWith5 = (dataGrid5.Items.Count - 1).ToString();
            object replace5 = 2;
            application.Selection.Find.Execute(ref findText5, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith5,
            ref replace5, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            object findText6 = "%Пользователь%";  //  выставляем Пользователь  //////////
            object replaceWith6 = Environment.UserName.ToString();
            object replace6 = 2;
            application.Selection.Find.Execute(ref findText6, ref missingObj, ref missingObj, ref missingObj,
            ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref missingObj, ref replaceWith6,
            ref replace6, ref missingObj, ref missingObj, ref missingObj, ref missingObj);/////////////////////

            Word.Table wordTableSpFile = document.Tables[1]; // заполняем 1 таблицу  
            for (var j = 0; j < dTF.Rows.Count; j++)
            {
                wordTableSpFile.Rows.Add(ref missingObj);
                wordTableSpFile.Cell(j + 2, 1).Range.Text = dTF.Rows[j][1].ToString();
             }

            Word.Table wordTable = document.Tables[2]; // заполняем 2 тублицу
            for (var j = 0; j < dT.Rows.Count; j++)
             {
                wordTable.Rows.Add(ref missingObj);
                    wordTable.Cell(j+3 ,  1).Range.Text = dT.Rows[j][0].ToString();
                    wordTable.Cell(j + 3, 2).Range.Text = dT.Rows[j][1].ToString();
                    wordTable.Cell(j + 3, 3).Range.Text = dT.Rows[j][5].ToString();
             }
                application.Visible = true; // показываем 
            }
            catch
            {
                System.Windows.MessageBox.Show("Укажите файлы с ЗАГСа", "Ошибка при вводе", MessageBoxButton.OK, MessageBoxImage.Error);
            }

 
        }
         
        private void spisFile(OpenFileDialog sF)     // отображение списка выбранных файлов
        {
            int i = 1;

            DataTable dtSF = new DataTable();
            dtSF.Columns.Add("n-n");
            dtSF.Columns.Add("Наименование файла");

            foreach (string p in sF.FileNames)
            {
                dtSF.Rows.Add(i++, ИмяФайла( p ) );
            }

            dataGrid1.ItemsSource = dtSF.DefaultView;
            label.Content = (i - 1).ToString()  ;
            label.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
        }
        
        private DataTable Excel2dataTable(string fN) // Екселевский файл выгружаем в DataTable
        {
            DataTable dt = new DataTable();
            string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fN + ";Extended Properties='Excel 12.0 XML;HDR=YES;IMEX=1';";

            OleDbConnection con = new  OleDbConnection(constr);
            con.Open();
            try
            {
                DataSet ds = new System.Data.DataSet();
            DataTable schemaTable = con.GetOleDbSchemaTable( OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheet1 = (string)schemaTable.Rows[0].ItemArray[2];
            string select = string.Format("SELECT * FROM [{0}]", sheet1);
 
            OleDbDataAdapter ad = new  OleDbDataAdapter(select, con);
           
                ad.Fill(ds);
                dt = ds.Tables[0] ;
                dt.Columns.Add("Имя файла");
                foreach (DataRow sD in dt.Rows)
                {
                    sD[dt.Columns.Count - 1] = ИмяФайла(fN);
                    
                }
            }
            catch
            {
                textBox2.AppendText("\n Откройте файл " + fN  + " проверте поля с датами \n");
                tabControl2.SelectedItem = tabItem5;
            }
            con.Close();
            con.Dispose();

            return dt ;
        }

        private string ИмяФайла(string imF)          // делим строку на массив и берем последнюю ячейку
        {
            string[] mas = imF.Split('\\');
            imF = mas[mas.Length-1];
            return imF ;
        }
         
        private DataTable ПодключениеБД(string strSQL)
        {
            DataTable dt = new DataTable();
            
            OdbcConnection conn = new OdbcConnection(conStr1);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter( strSQL , conn);
            da.Fill(dt);
           
            in1 = true;

            return dt;
        }
 
        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)// при переходе по закладкам
        {
            if (tabControl1.SelectedIndex == 1 && in1 == false) // Получатели страховых выплат
            {
                dataGrid3.ItemsSource = ПодключениеБД("fix all;select char(HDOCNUM) as '№Дела', char(HDOCDAT) as 'ДатаРегДела', LNAME as 'FAMILY', FNAME as 'NAME', MNAME 'FATHER', char(rtrim(INN,'C'), 12)  as 'ИНН', char(BDATE)  as 'ДатаРожд' from MRECEIVE where PSTOP == '0' ;").DefaultView;
            }

            if (tabControl1.SelectedIndex == 2 && in2 == false)// Заявки льготников
            {
                dataGrid4.ItemsSource = ПодключениеБД("fix all; select a.RQST_NUM as 'Ном', char(a.RQST_DATE) as 'Дата', b.LNAME as 'Фамилия', b.FNAME as 'Имя', b.MNAME as 'Отчество', char(a.RSLT_DATE) as 'Дата рожд.', char(b.BDATE) as 'Дата см.', b.MODPERSON as 'Отв' from LMBRQST a , LQUEUE b where  a.ID_OWN = b.ID and ( b.ID_FSS = 2711  ) ;").DefaultView;
            }

            if (tabControl1.SelectedIndex == 0)
            {
                   button7.Visibility = Visibility.Hidden;
                tabControl1.Background = new SolidColorBrush(Color.FromArgb(250, 0xF0, 0xF3, 0xB0));//#FFF0F3B0
            }

            if (tabControl1.SelectedIndex == 1)
            {
                label2.Visibility = Visibility.Visible;
                label3.Visibility = Visibility.Visible;
                label4.Visibility = Visibility.Hidden;
                label2.Content = (dataGrid3.Items.Count - 1).ToString();
                label3.Content = "получателей страховых выплат";
                if (dataGrid1.Items.Count > 0 & dataGrid2.Items.Count > 0)
                {
                    button7.Visibility = Visibility.Visible;
                    button7.Background = new SolidColorBrush(Color.FromArgb(250, 0x9C, 0xA0, 0xF9));// #FF9CA0F9  
                }
                tabControl1.Background = new SolidColorBrush(Color.FromArgb(250, 0x9C, 0xA0, 0xF9));// #FF 9C A0 F9
            }

            if (tabControl1.SelectedIndex == 2)
            {
                label2.Visibility = Visibility.Visible;
                label3.Visibility = Visibility.Visible;
                label4.Visibility = Visibility.Hidden;
                label2.Content = (dataGrid4.Items.Count - 1).ToString();
                label3.Content = "заявок льготников";
                if (dataGrid1.Items.Count > 0 & dataGrid2.Items.Count > 0)
                {
                   button7.Visibility = Visibility.Visible;
                   button7.Background = new SolidColorBrush(Color.FromArgb(250, 0x7A, 0xFB, 0x7A))  ;
                }
                tabControl1.Background = new SolidColorBrush(Color.FromArgb(250, 0x7A, 0xFB, 0x7A));// #FF 7A FB 7A
            }

        }
          
        private void Sverka1()
        {
            DataTable dtSovp = new DataTable();
            dtSovp.Columns.Add("Имя файла  ЗАГС");
            dtSovp.Columns.Add("ФИО в  ЗАГС");
            dtSovp.Columns.Add("Дата рожд в ЗАГС", typeof(string)  )  ;
            dtSovp.Columns.Add("Дата смерти ЗАГС") ;
            dtSovp.Columns.Add("х - х");
            dtSovp.Columns.Add("ФИО в ФСС");
             
            DataTable dtA = DataGrid2DataTable(dataGrid2);
            DataTable dtB = DataGrid2DataTable(dataGrid3);

            foreach (DataRow rowA in dtA.Rows)
            {
              foreach (DataRow rowB in dtB.Rows)
              {
               string strF1 = rowA[dtA.Columns[0]].ToString().Replace("ё","е" ).Replace("Ё","Е") ; // Фамилия с ЗАГСа
               if (strF1 != "")
               {
                string strI1 = rowA[dtA.Columns[1]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Имя с ЗАГСа
                string strF2 = rowB[dtB.Columns[2]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Фамилия с ЕИИС
                string strI2 = rowB[dtB.Columns[3]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Имя с ЕИИС
                
                if (strF1 == strF2 & strI1 == strI2 )
                {
                            string strO1   = rowA[dtA.Columns[2]].ToString();      // Отчество  с ЗАГСа
                            string strFile = rowA[dtA.Columns[14]].ToString();     // Файл      с ЗАГСа
                            string strFIO1 = strF1 + " " + strI1 + " " + strO1;    // ФИО       с ЗАГСа
                            string strDR   = rowA[dtA.Columns[3]].ToString();      // дата рожд с ЗАГСа
                            string strDS   = rowA[dtA.Columns[4]].ToString();      // дата смерти с ЗАГСа
                            string strO2 = rowB[dtB.Columns[4]].ToString();    // Отчество с ЕИИС
                            string strFIO2 = strF2 + " " + strI2 + " " + strO2;// ФИО с ЕИИС

                            dtSovp.Rows.Add(strFile, strFIO1, strDR = (strDR != "") ? strDR.Substring(0, 10) : "",
                                          strDS = (strDS != "") ? strDS.Substring(0, 10) : "",  "---",               strFIO2);

                        }
                    }
               }
             }

          dataGrid5.ItemsSource = dtSovp.DefaultView;

        }

        private void Sverka2()
        {
             
        }

        public static DataTable DataGrid2DataTable(System.Windows.Controls.DataGrid dg)
        {
           dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            string result = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);
            DataTable dt = new DataTable();
            
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;
        }

        private void tabControl2_SelectionChanged(object sender, SelectionChangedEventArgs e)// при переходе по закладкам нижняя tabControl2
        {
            switch (tabControl2.SelectedIndex)
            {
                case 0:
                    tabControl2.Background = new SolidColorBrush(Color.FromArgb(250, 0xBA, 0xF9, 0xF9));     
                    break;
                case 1:
                    tabControl2.Background = new SolidColorBrush(Color.FromArgb(250, 0xEA, 0xDE, 0x6C));
                    break;
                case 2:
                    tabControl2.Background = new SolidColorBrush(Color.FromArgb(250, 0xF5, 0xB2, 0xAE));   
                    break;
            }
        }



    }

    
}
 