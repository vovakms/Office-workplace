using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Data.OleDb;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace ЗАГС_ЕИИС
{
    
    public partial class MainWindow : Window
    {
         string conStr1 = "";
         string conStr2 = "";
          
        public MainWindow()
        {
           InitializeComponent();
 
            TextBlock1.Text = Properties.Settings.Default.НаимОрг.ToString() ;
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
         conStr1 = "Dsn=" + Properties.Settings.Default.Источн1 + ";uid=" + Properties.Settings.Default.Логин1 + ";srv=tcpip:/" + Properties.Settings.Default.СерверБД1 + ";sn=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";ct=N;fixall=Y;msjet=N";
         conStr2 = "Dsn=" + Properties.Settings.Default.Источн2 + ";uid=" + Properties.Settings.Default.Логин2 + ";srv=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";sn=tcpip:/" + Properties.Settings.Default.СерверБД2 + ";ct=N;fixall=Y;msjet=N";
          
            if (Properties.Settings.Default.ОдинИсточн == true) // если один источник
                    conStr2 = conStr1;
               
             
            if (Properties.Settings.Default.ПервыйЗапПрог == true) // если  первый запуск программы
            {
                Properties.Settings.Default.ПервыйЗапПрог = false; // убиираем  флаг первого запуска
                Window1 win1 = new Window1(); 
                win1.Show();// показывыаем окно настройки
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e) // кн. Свернуть
        {
            Window1.WindowState = WindowState.Minimized ;
        }

        private void button3_Click(object sender, RoutedEventArgs e) // кн. Востановить
        {
            if (Window1.WindowState == WindowState.Normal)
                Window1.WindowState = WindowState.Maximized;
            else
                Window1.WindowState = WindowState.Normal;
        }
        
        private void button4_Click(object sender, RoutedEventArgs e) // кн. Закрыть
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

        private void button1_Click(object sender, RoutedEventArgs e)  // кн. Открыть   файл 
        {
            DataTable dt = new DataTable();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "Microsoft Excel (*.xls*)|*.xls*";
            ofd.Title = "Выберите документ для загрузки данных";
            //ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = ofd.FileName; // Path.GetDirectoryName(ofd.FileName);
               
                //spisFile(ofd);
                //foreach (string fN in ofd.FileNames)
                dt.Merge(Excel2dataTable(ofd.FileName));
                
                dataGrid2.ItemsSource = dt.DefaultView;
                label2.Content = dt.Rows.Count.ToString();
                label2.Visibility = Visibility.Visible;
                label3.Visibility = Visibility.Visible;
                
                label3.Content = "строк  ";
               
                tabControl1.SelectedIndex = 0;



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

        private void SaveWord() //  сохранение отчета в шаблон Ворда
        {
            DataTable dT = DataGrid2DataTable(dataGrid5);   // создаем и заполняем таблицу
           
            string imF = "D:\\АКТ_" + DateTime.Now.ToString("yyyy-MM-dd (H-mm-ss)") + ".docx";

            File.Copy(System.Windows.Forms.Application.StartupPath + "\\АКТ.docx", imF);
            //-----------------------------------------------------------------------------------------
            Word._Application application = new Word.Application(); //создаем обьект приложения word
            Word._Document document = new Word.Document();
            Object missingObj = System.Reflection.Missing.Value;
            Object trueObj = true;
            Object falseObj = false;

            Object templatePathObj = imF; // создаем путь к файлу  

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
             
            object findText3 = "%КолвЧелВФайле%";  //  выставляем количество человек в файлах ///////////////////
            // object replaceWith3 = (dataGrid2.Items.Count - dataGrid1.Items.Count - dataGrid1.Items.Count).ToString() ;
            object replaceWith3 = (dataGrid2.Items.Count - 1).ToString();
            object replace3 = 2 ;

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

            //Word.Table wordTableSpFile = document.Tables[1]; // заполняем 1 таблицу  
            //for (var j = 0; j < dTF.Rows.Count; j++)
            //{
            //    wordTableSpFile.Rows.Add(ref missingObj);
            //    wordTableSpFile.Cell(j + 2, 1).Range.Text = dTF.Rows[j][1].ToString();
            //}

            Word.Table wordTable = document.Tables[2]; // заполняем 2 тублицу
            for (var j = 0; j < dT.Rows.Count; j++)
             {
                wordTable.Rows.Add(ref missingObj);
                    wordTable.Cell(j + 3, 1).Range.Text = dT.Rows[j][0].ToString();
                    wordTable.Cell(j + 3, 2).Range.Text = dT.Rows[j][1].ToString();
                    wordTable.Cell(j + 3, 3).Range.Text = dT.Rows[j][2].ToString();
                    wordTable.Cell(j + 3, 4).Range.Text = dT.Rows[j][3].ToString();
                    wordTable.Cell(j + 3, 5).Range.Text = dT.Rows[j][4].ToString();
                    wordTable.Cell(j + 3, 6).Range.Text = dT.Rows[j][5].ToString();
                    wordTable.Cell(j + 3, 7).Range.Text = dT.Rows[j][6].ToString();
                    //wordTable.Cell(j + 3, 8).Range.Text = dT.Rows[j][7].ToString();
             }
                application.Visible = true; // показываем 
            //}
            //catch
            //{
            //    System.Windows.MessageBox.Show("Укажите файлы с ЗАГСа", "Ошибка при вводе", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
          
        private DataTable Excel2dataTable(string fN) // Екселевский файл выгружаем в DataTable
        {
            //DataTable dt = new DataTable();
            //string constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fN + ";Extended Properties='Excel 12.0 XML;HDR=YES;IMEX=1';";
           //OleDbConnection con = new  OleDbConnection(constr);
            //con.Open();
             //    DataSet ds = new System.Data.DataSet();
            //    DataTable schemaTable = con.GetOleDbSchemaTable( OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //    string sheet1 = (string)schemaTable.Rows[1].ItemArray[2];
            //    string select = string.Format("SELECT * FROM [{0}]", sheet1);
            //    OleDbDataAdapter ad = new  OleDbDataAdapter(select, con);
             //    ad.Fill(ds);
            //    dt = ds.Tables[0] ;
            //    dt.Columns.Add("Имя файла");
            //    foreach (DataRow sD in dt.Rows)
            //        sD[dt.Columns.Count - 1] = ИмяФайла(fN);
            //con.Close();
            //con.Dispose();
            //return dt ;
             
            //string sSheetName = null;
            //string sConnection = null;
            //DataTable dtTablesList = default(System.Data.DataTable);
            //OleDbCommand oleExcelCommand = default(OleDbCommand);
            //OleDbDataReader oleExcelReader = default(OleDbDataReader);
            //OleDbConnection oleExcelConnection = default(OleDbConnection);
            //sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fN + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
            //oleExcelConnection = new OleDbConnection(sConnection);
            //oleExcelConnection.Open();
            //dtTablesList = oleExcelConnection.GetSchema("Tables");

            //if (dtTablesList.Rows.Count > 0)
            //{
            //    sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
            //}

            //dtTablesList.Clear();
            //dtTablesList.Dispose();

            //if (!string.IsNullOrEmpty(sSheetName))
            //{
            //    oleExcelCommand = oleExcelConnection.CreateCommand();
            //    oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
            //    oleExcelCommand.CommandType = CommandType.Text;
            //    oleExcelReader = oleExcelCommand.ExecuteReader();
            //    //nOutputRow = 0;

            //    while (oleExcelReader.Read())
            //    {
            //    }
            //    oleExcelReader.Close();
            //}
            //oleExcelConnection.Close();
            //return dtTablesList;

             string sSheetName = null;
            //string sConnection = null;
            DataTable dtTablesList = default(System.Data.DataTable);
            //OleDbCommand oleExcelCommand = default(OleDbCommand);
            //OleDbDataReader oleExcelReader = default(OleDbDataReader);
             OleDbConnection oleExcelConnection = default(OleDbConnection);
             string sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fN + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
             oleExcelConnection = new OleDbConnection(sConnection);
             oleExcelConnection.Open();
             dtTablesList = oleExcelConnection.GetSchema("Tables");

             if (dtTablesList.Rows.Count > 0)
                 sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
              
            //string POCConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fN + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\";";

            OleDbConnection POCcon = new OleDbConnection(sConnection);
            OleDbCommand POCcommand = new OleDbCommand();
            DataTable dt = new DataTable();
            OleDbDataAdapter POCCommand = new OleDbDataAdapter("select * from [" + sSheetName + "] ", POCcon);
            POCCommand.Fill(dt);
            return dt;
        }

        private string ИмяФайла(string imF)          // делим строку на массив и берем последнюю ячейку
        {
            string[] mas = imF.Split('\\');
            imF = mas[mas.Length-1];
            return imF ;
        }
         
        private DataTable ПодключениеБД(string conODBC , string strSQL)
        {
            DataTable dt = new DataTable();
            
            OdbcConnection conn = new OdbcConnection(conODBC);
            conn.Open();
            OdbcDataAdapter da = new OdbcDataAdapter( strSQL , conn);
            da.Fill(dt);
            
            return dt; // возвращаем ДатаТАблу с результатами запроса
        }
   
        private void Sverka(System.Windows.Controls.DataGrid dg) // модуль сверки
        {
            label8.Visibility = Visibility.Visible;
            label9.Visibility = Visibility.Visible;
            label10.Visibility = Visibility.Visible;

            // UpdateLayout();

            Window3 win3 = new Window3();
            win3.Show();

            DataTable dtSovp = new DataTable();
            
            dtSovp.Columns.Add("ФИО (ЗАГС)");
            dtSovp.Columns.Add("ДатаРожд (ЗАГС)", typeof(string)  )  ;
            dtSovp.Columns.Add("ДатаСм (ЗАГС)") ;
            dtSovp.Columns.Add("Адрес регистрации (ЗАГС)");
            dtSovp.Columns.Add("х-----х");
            dtSovp.Columns.Add("ФИО (ФСС)");
            dtSovp.Columns.Add("ДатаРожд (ФСС)");
             
            DataTable dtA = DataGrid2DataTable(dataGrid2);
            
            DataTable dtB = DataGrid2DataTable(dg);

            foreach (DataRow rowA in dtA.Rows)
            {
              foreach (DataRow rowB in dtB.Rows)
              {
               string strF1 = rowA[dtA.Columns[5]].ToString().Replace("ё","е" ).Replace("Ё","Е") ; // Фамилия с ЗАГСа
               string strI1 = rowA[dtA.Columns[6]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Имя с ЗАГСа
               string strO1 = rowA[dtA.Columns[7]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Отчество с ЗАГСа
               
               string strF2 = rowB[dtB.Columns[0]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Фамилия с ЕИИС
               string strI2 = rowB[dtB.Columns[1]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Имя с ЕИИС
               string strO2 = rowB[dtB.Columns[2]].ToString().Replace("ё", "е").Replace("Ё", "Е"); // Отчество с ЕИИС

               if (String.Compare(strF1.ToUpper(), strF2.ToUpper(), true) == 0 & String.Compare(strI1.ToUpper(), strI2.ToUpper(), true) == 0 & String.Compare(strO1.ToUpper(), strO2.ToUpper(), true) == 0)// if (strF1.ToUpper() == strF2 & strI1.ToUpper() == strI2 )
               {
                 string strFIO1 = strF1.ToUpper() + " " + strI1.ToUpper() + " " + strO1.ToUpper(); // ФИО   с ЗАГСа
                 string strDR   = rowA[dtA.Columns[11]].ToString(); // дата рожд     с ЗАГСа
                 string strDS   = rowA[dtA.Columns[8]].ToString();  // дата смерти   с ЗАГСа
                 string strAdrR = rowA[dtA.Columns[12]].ToString(); // адрес регистр с ЗАГСа
                 string strFIO2 = strF2 + " " + strI2 + " " + strO2;//        ФИО с ЕИИС
                 string strDReiis = rowB[dtB.Columns[3]].ToString();// Дата рожд. с ЕИИС 
               
                 dtSovp.Rows.Add( strFIO1, strDR = (strDR != "") ? strDR.Substring(0, 10) : "",
                                          strDS = (strDS != "") ? strDS.Substring(0, 10) : "", 
                                          strAdrR ,  "---", strFIO2, strDReiis  );

                }
                    
               }
             }

          dataGrid5.ItemsSource = dtSovp.DefaultView;

          win3.Close();
          label8.Visibility = Visibility.Hidden;
          label9.Visibility = Visibility.Hidden;
          label10.Visibility = Visibility.Hidden; 
        }

        public static DataTable DataGrid2DataTable(System.Windows.Controls.DataGrid dg)
        {
           // DataTable dt = new DataTable();

          DataView view = (DataView) dg.ItemsSource;
          DataTable dt = DataViewAsDataTable(view);

            return dt;
        }

        public static DataTable DataViewAsDataTable(DataView dv)
{
    DataTable dt = dv.Table.Clone();
    foreach (DataRowView drv in dv)
       dt.ImportRow(drv.Row);
    return dt;
}
 
        public static DataTable DataGrid4DataTable(System.Windows.Controls.DataGrid dg)
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
         
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window2 win3 = new Window2();
            win3.Show();
        }
         
        private void button5_Click(object sender, RoutedEventArgs e) //  кн. Показать список "Получателей страховых выплат"
        {
             //dataGrid3.ItemsSource = ПодключениеБД(conStr1, "fix all; select LNAME, FNAME, MNAME, char(BDATE), MODPERSON from LQUEUE;").DefaultView;
                                                                                                                       //where SYSCODE = 0
            dataGrid3.ItemsSource = ПодключениеБД(conStr1, "fix all; select LNAME, FNAME,MNAME, BDATE, htStrReplace(char(SNILS),'D-A-B C') as СНИЛС, char(htStrReplace(INN,'C',''),12) as 'ИНН' , PADDR from MRECEIVE where SYSCODE = 0 ;").DefaultView;
            label4.Content = (dataGrid3.Items.Count - 1).ToString()  ;                              
        }

        private void button6_Click(object sender, RoutedEventArgs e) //  кн. Показать список "Заявок льготников"
        {
             
                //dataGrid4.ItemsSource = ПодключениеБД(conStr2, "fix all; select a.RQST_NUM as 'Ном', char(a.RQST_DATE) as 'Дата', b.LNAME as 'Фамилия', b.FNAME as 'Имя', b.MNAME as 'Отчество', char(a.RSLT_DATE) as 'Дата рожд.', char(b.BDATE) as 'Дата см.', b.MODPERSON as 'Отв', b.FULLNAME from LMBRQST a , LQUEUE b where a.ID_OWN = b.ID   ; ").DefaultView;   // and ( b.ID_FSS = " + Properties.Settings.Default.КодФилиала.ToString() + "  )  //  fix all;select a.RQST_NUM as '1_Ном', char(a.RQST_DATE) as '2_Дата', b.LNAME as '3_Фамилия', b.FNAME as '4_Имя', b.MNAME as '5_Отчество', char(b.BDATE) as '6_Дата рожд.', a.RSLT_DATE as '7_Дата  см.', b.MODPERSON as '8_Отв' from LMBRQST a , LQUEUE b where a.ID_OWN = b.ID and a.RSLT_DATE = '' ;
            dataGrid4.ItemsSource = ПодключениеБД(conStr2, "fix all; select   LNAME, FNAME, MNAME,  BDATE   from LQUEUE; ").DefaultView;   // and ( b.ID_FSS = " + Properties.Settings.Default.КодФилиала.ToString() + "  )  //  fix all;select a.RQST_NUM as '1_Ном', char(a.RQST_DATE) as '2_Дата', b.LNAME as '3_Фамилия', b.FNAME as '4_Имя', b.MNAME as '5_Отчество', char(b.BDATE) as '6_Дата рожд.', a.RSLT_DATE as '7_Дата  см.', b.MODPERSON as '8_Отв' from LMBRQST a , LQUEUE b where a.ID_OWN = b.ID and a.RSLT_DATE = '' ;
                label5.Content = (dataGrid4.Items.Count - 1).ToString();   
        }

        private void button7_Click(object sender, RoutedEventArgs e) //  кн. "Сверить" получатели страховых выплат  
        {
            Sverka(dataGrid3);

            label7.Content = (dataGrid5.Items.Count - 2).ToString();
        }
         
       

        private void button8_Click(object sender, RoutedEventArgs e) //  кн. "Сверить" Заявки льготников
        {
            Sverka(dataGrid4);
            label7.Content = (dataGrid5.Items.Count - 2).ToString();
        }

        private void button9_Click(object sender, RoutedEventArgs e) //  кн. "Создать АКТ"
        {
            SaveWord();
            
        }


    }


    
}
 