using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Odbc;
using System.IO;
using System.Data.OleDb;
using Word = Microsoft.Office.Interop.Word;


namespace ОтчетыHyTech
{
    public partial class Form1 : Form  //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-* 
    {
        string connectionString = "";
        string connectionStringXRO = "";

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();

        bool fl1 = false, fl2 = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string ser = Properties.Settings.Default.IPServer;
            string log = Properties.Settings.Default.LOGIN;
            string pas = Properties.Settings.Default.PASSWORD;
            //string conN = Properties.Settings.Default.ConODBC;
            string ser2 = Properties.Settings.Default.IPServer2;
            string log2 = Properties.Settings.Default.LOGIN2;
            string pas2 = Properties.Settings.Default.PASSWORD2;
            string conN2 = Properties.Settings.Default.ConODBC2;

            connectionString = "Dsn=" + Properties.Settings.Default.ConODBC + ";uid=" + log + ";srv=tcpip:/" + ser + ";sn=tcpip:/" + ser + ";ct=N;fixall=Y;msjet=N";
            connectionStringXRO = "Dsn=" + conN2 + ";uid=" + log2 + ";srv=tcpip:/" + ser2 + ";sn=tcpip:/" + ser + ";ct=N;fixall=Y;msjet=N";
        }

        private void button2_Click(object sender, EventArgs e) //------------------ нажали кнопку ">>>" выбор каталога -------------------------
        {
            DataTable dataTable = new DataTable();

            int dtR = 0, dtC = 0, nC = 0, nsR = 0, R, C, ii = 0, kf = 0;
            string path = null;
            string fExcel = "";

            richTextBox1.Clear(); // очищаем 

            toolStripProgressBar1.Value = 0;

            using (var dialog = new FolderBrowserDialog())
                if (dialog.ShowDialog() == DialogResult.OK)// если в диалоговом окне  выбрали какойто каталог
                {
                    path = dialog.SelectedPath;//  путь к выбранному каталогу
                    textBox1.Text = path;      // отображаем этот путь 
                }

            DirectoryInfo dinfo = new DirectoryInfo(path);
            FileInfo[] files = dinfo.GetFiles("*.xls*"); // отбираем только Ексель файлы

            listBox1.Items.Clear(); // очищаем список файлов
            toolStripStatusLabel1.Text = " ПОДОЖДИТЕ ИДЕТ ЗАГРУЗКА ФАЙЛОВ";
            foreach (FileInfo filenames in files)
            {  //цикл по списку файлов
                listBox1.Items.Add(filenames);    // показываем список файлов 
                fExcel = path + "\\" + filenames;

                dataTable = LoadFromExcel(fExcel, filenames.ToString()); //      загружаем файл в Табле
                dtR = dataTable.Rows.Count;        //Кол-во строк
                dtC = dataTable.Columns.Count;     //Кол-во столбиков
                if (dtC > nC) nC = dtC;           // Запоминаем наибольшее число столбиков

                nsR = nsR + dtR;    // количество строк во всех файлах
                kf++;

                toolStripProgressBar1.Value = toolStripProgressBar1.Value + (100 / files.Count());
            }

            label9.Text = kf.ToString();           // показываем количество файлов

            string[,] mas = new string[nsR, nC];  // задаем размер массива

            foreach (FileInfo filenames in files)  // перебираем файлы
            {
                fExcel = path + "\\" + filenames;   // формируем полное имя файла 
                dataTable = LoadFromExcel(fExcel, filenames.ToString()); // выгружаем содержимое файла в Табле
                R = dataTable.Rows.Count;         // получаем скоко строк
                C = dataTable.Columns.Count;      // и колонок

                for (int i = 0; i < R; i++)
                {
                    mas[ii, 0] = dataTable.Rows[i][C - 1].ToString();
                    for (int j = 0; j <= C - 2; j++)
                    {
                        mas[ii, j + 1] = dataTable.Rows[i][j].ToString();  // бежим по всем значениям Табле и перекидываем в массив
                    }
                    ii++;
                }
            }

            label4.Text = mas.GetLength(0).ToString(); // кол-во строк в массиве

            dataGridView1.RowCount = mas.GetLength(0); // указываем кол-во строк
            dataGridView1.ColumnCount = mas.GetLength(1);  // указываем кол-во столбиков
            for (int i = 0; i < mas.GetLength(0); i++)
                for (int j = 0; j < mas.GetLength(1); j++)
                    dataGridView1.Rows[i].Cells[j].Value = mas[i, j]; //пишем значения из массива в ячейки контролла

            label6.Text = dataGridView1.RowCount.ToString(); // показываем скоко строк в датаГриде
            toolStripProgressBar1.Value = 100;
            toolStripStatusLabel1.Text = "ОК , файлы загружены ,  список сформирован";
        }//----------------------------------------------------------------------------------------------------------------------------------------------------------------

        private void tabControl1_Selected(object sender, TabControlEventArgs e)//------ обработка событие выбор закладки-------------------------------------------------
        {
            toolStripProgressBar1.ForeColor = SystemColors.Control;
            toolStripProgressBar1.Value = 10;

            if (e.TabPageIndex == 0)// клик по закладке "Список с ЗАГСов"
            {
                toolStripStatusLabel1.Text = " Список с ЗАГСов ";

            }
            if (e.TabPageIndex == 1) // клик по закладке "Получатели страховых выплат"
            {
                toolStripStatusLabel1.Text = " Выборка Получатели страховых выплат ";
                if (fl1 == false)
                {
                    Clear(dataGridView2);
                    OdbcConnection conn = new OdbcConnection(connectionString);
                    conn.Open();
                    OdbcDataAdapter da = new OdbcDataAdapter("fix all;select char(HDOCNUM) as '№Дела', char(HDOCDAT) as 'ДатаРегДела', LNAME, FNAME, MNAME, char(rtrim(INN,'C'), 12)  as 'ИНН', char(BDATE)  as 'ДатаРожд' from MRECEIVE where PSTOP == '0' ;", conn);
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                    fl1 = true;
                }
                label3.Text = (dataGridView2.RowCount - 1).ToString();
            }
            if (e.TabPageIndex == 2)// клик по закладке "Заявки льготников"
            {
                toolStripStatusLabel1.Text = " Выборка Заявки льготников ";
                if (fl2 == false)
                {
                    Clear(dataGridView3);
                    OdbcConnection conn2 = new OdbcConnection(connectionStringXRO);
                    conn2.Open();
                    OdbcDataAdapter da2 = new OdbcDataAdapter("fix all; select a.RQST_NUM as 'Ном', char(a.RQST_DATE) as 'Дата', b.LNAME as 'Фамилия', b.FNAME as 'Имя', b.MNAME as 'Отчество', char(a.RSLT_DATE) as 'Дата рожд.', char(b.BDATE) as 'Дата см.', b.MODPERSON as 'Отв' from LMBRQST a , LQUEUE b where  a.ID_OWN = b.ID;", conn2);
                    da2.Fill(dt2);
                    dataGridView3.DataSource = dt2;
                    fl2 = true;
                }
                label4.Text = (dataGridView3.RowCount - 1).ToString();
            }

            toolStripProgressBar1.Value = 100;
        }///-------------------------------------------------------------------------------------------------------------------------------------------

        private void button1_Click_2(object sender, EventArgs e)//------------   Нажали кнопку "Сверить"  Получатели страховых выплат
        {
            string userNameWin = System.Environment.UserName;
            string[,] mas1 = new string[dataGridView1.RowCount, dataGridView1.ColumnCount];
            string[,] mas2 = new string[dataGridView2.RowCount, dataGridView2.ColumnCount];

            richTextBox1.Clear();
            toolStripStatusLabel1.Text = " Идет сверка ";

            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1[j, i].Value is System.DBNull) { }
                    else { mas1[i, j] = (string)dataGridView1[j, i].Value; }

            for (int i = 0; i < dataGridView2.RowCount; i++)
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    if (dataGridView2[j, i].Value is System.DBNull) { }
                    else { mas2[i, j] = (string)dataGridView2[j, i].Value; }

            richTextBox1.AppendText("АКТ контроля \n");
            richTextBox1.AppendText("сведений по умершим с получателями страховых выплат филиала № 11 \n");
            richTextBox1.AppendText("  от  " + DateTime.Now.ToString("dd MMMM yyyy") + " года 	\n  \n");

            richTextBox1.AppendText("Сведения отделов ЗАГС:	 \n  \n ");

            int kf = 0;
            foreach (FileInfo S in listBox1.Items) { richTextBox1.AppendText(S.ToString() + " 	\n"); kf++; }
            richTextBox1.AppendText("Всего   " + kf.ToString() + " файлов 	\n  \n");


            richTextBox1.AppendText("Количество получателей страховых выплат, участвующих  в сверке - " + (dataGridView2.RowCount - 1).ToString() + " чел.  	\n  	\n");
            richTextBox1.AppendText("Выявленные совпадения (проверяется совпадение фамилия и имени) 	\n 	\n  Совпадения	  	\n 	\n");
            richTextBox1.AppendText("_____________________________Сведения ОЗАГС ________________________|__________________________ Получатели страховых выплат	______________  	\n 	\n");

            int ns = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)//  ------------------   сверяем  массивы
                for (int j = 0; j < dataGridView2.RowCount; j++)
                    if (mas1[i, 1] == mas2[j, 2] && mas1[i, 2] == mas2[j, 3])
                    {
                        richTextBox1.AppendText((string)mas1[i, 0] + "       " + (string)mas1[i, 1] + " " + (string)mas1[i, 2] + " " + (string)mas1[i, 3] + "   ---    " + (string)mas2[j, 2] + " " + (string)mas2[j, 3] + " " + (string)mas2[j, 4] + " \n");
                        ns++;
                    }


            richTextBox1.AppendText("\n Количество совпавших строк   " + ns + " 	\n");
            richTextBox1.AppendText("Вывод :	\n\n");
            richTextBox1.AppendText("Сверка данных произведена " + DateTime.Now.ToString("dd MMMM yyyy") + "	\n");
            richTextBox1.AppendText("Ответственный  " + userNameWin + "	\n\n");


        }//-------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Clear(DataGridView dataGridView)//----------------- Процедура очистки Грида -----------------
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }//------------------------------------------------------------------------------------------------------

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)//----------
        {
            Form2 form = new Form2();
            form.ShowDialog();//в модальном режиме 
        }//----------------------------------------------------------------------------------

        DataTable LoadFromExcel(string filename, string fname)//--------------  ф-я загрузки Ексель файла в Табле    ----------------------------------------
        {
            string sheet1 = "";

            DataTable dataTable = new DataTable();
            DataSet dataSet = new DataSet("Tables"); // Создаем новый DataSet

            DataTable schemaTable = new DataTable();

            richTextBox2.Clear(); // очищаем 
             
            string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;extended properties=\"excel 8.0;hdr=no;IMEX=1\"; Data Source=" + filename;// Командная строка "подключения к Excel"

            OleDbConnection dbConnect = new OleDbConnection(ConnectionString);// Открываем соединение
            
                dbConnect.Open();
            
              schemaTable = dbConnect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });// Получаем список листов в файле
             
            if (fname.Substring(fname.Length - 3) == "xls")
                sheet1 = (string)schemaTable.Rows[1].ItemArray[2];         // Берем название 1-ого листа

            if (fname.Substring(fname.Length - 3) == "lsx")
                sheet1 = (string)schemaTable.Rows[0].ItemArray[2];         // Берем название 1-ого листа

            string select = String.Format("SELECT * FROM [{0}]", sheet1);     // Выбираем все данные с листа
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(select, dbConnect);

            dbConnect.Close();

            dataAdapter.Fill(dataTable); // Заполняем таблицу




            dataTable.TableName = sheet1.Substring(0, sheet1.Length - 1); // В конце от Экселя стоит символ '$'

            dataTable.Columns.Add("FileName", typeof(string));

            for (int i = 0; i < dataTable.Rows.Count; i++)
                dataTable.Rows[i][dataTable.Columns.Count - 1] = fname;

            dataSet.Tables.Add(dataTable);

            return dataTable;
        }//-----------------------------------------------------------------------------------------------------------------------------
 
        private void toolStripButton2_Click(object sender, EventArgs e)//-------------   нажали кнопку  в WORD   -----------------------------
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();//Создаем SaveFileDialog запрашиваем путь и имя файла для сохранения

            saveFile1.DefaultExt = "*.doc"; // Инициализация SaveFileDialog указать расширение doc файла.
            saveFile1.FileName = "АКТ контроля от " + DateTime.Now.ToString("dd.MM.yyyy_hh-mm-ss");
            saveFile1.Filter = "doc Files|*.doc";

            if (saveFile1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile1.FileName.Length > 0) //  если пользователь выбрал    имя файла из SaveFileDialog.
            {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText); //Сохранить содержимое RichTextBox в файл.

                Word.Application app = new Microsoft.Office.Interop.Word.Application();//процесс ворда
                Object docxFileName = saveFile1.FileName;//имя файла
                Object missing = Type.Missing;
                //открыли дркумент
                //app.Documents.Open(ref docxFileName, ref missing,
                //    ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing, ref missing, ref missing,
                //    ref missing, ref missing);
                app.Visible = true;
            }
        }//------------------------------------------------------------------------------------------------------------------------------------------------------

        private void button3_Click(object sender, EventArgs e)// нажали кнопку "СВЕРИТЬ"    заявки льготников
        {
            string[,] mas1 = new string[dataGridView1.RowCount, dataGridView1.ColumnCount];
            string[,] mas2 = new string[dataGridView3.RowCount, dataGridView3.ColumnCount];

            richTextBox1.Clear();
            toolStripStatusLabel1.Text = " Идет сверка ";

            for (int i = 0; i < dataGridView1.RowCount; i++)// ------------  Заполняем 1 масив
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1[j, i].Value is System.DBNull) { }
                    else { mas1[i, j] = (string)dataGridView1[j, i].Value; }

            for (int i = 0; i < dataGridView3.RowCount; i++)// ------------  Заполняем 2 масив
                for (int j = 0; j < dataGridView3.ColumnCount; j++)
                    if (dataGridView3[j, i].Value is System.DBNull) { }
                    else{  mas2[i, j] = Convert.ToString(dataGridView3[j, i].Value);  }

             richTextBox1.AppendText("АКТ контроля \n");
             richTextBox1.AppendText("сведений по умершим с заявками льготников филиала № 11 \n");
             richTextBox1.AppendText("  от  " + DateTime.Now.ToString("dd MMMM yyyy") + " 	\n");
             richTextBox1.AppendText(" 	\n");
             richTextBox1.AppendText("Сведения отделов  ЗАГС:	 \n");
             richTextBox1.AppendText(" 	\n");

             int kf = 0;
             foreach (FileInfo S in listBox1.Items) { richTextBox1.AppendText(S.ToString() + " 	\n"); kf++; }

             richTextBox1.AppendText("Всего " + kf.ToString() +" файлов   	\n");
             richTextBox1.AppendText("Количество заявок,  участвующих  в сверке  -	" + (dataGridView2.RowCount - 1).ToString() + "  	\n");
             richTextBox1.AppendText(" \n  (проверяется совпадение фамилии и имени)  	\n \n");
             richTextBox1.AppendText("   Сведения ОЗАГС                                       Льготник 	  	\n \n");
                        int ns = 0;
                        for (int i = 0; i < dataGridView1.RowCount; i++)//  ------------------   сверяем  массивы
                            for (int j = 0; j < dataGridView3.RowCount; j++)
                                if (mas1[i, 1].ToUpper() == mas2[j, 2] && mas1[i, 2].ToUpper() == mas2[j, 3] && mas1[i, 3].ToUpper() == mas2[j, 4])
                                {
                                    richTextBox1.AppendText((string)mas1[i, 0] + " " + (string)mas1[i, 1] + " " + (string)mas1[i, 2] + " " + (string)mas1[i, 3] + " " + (string)mas1[i, 4] + " " + (string)mas1[i, 5] + "   ---    " + (string)mas2[j, 2] + " " + (string)mas2[j, 3] + " " + (string)mas2[j, 4] + " " + (string)mas2[j, 6] + " \n");
                                    ns++;
                                    break;
                                }

                        //richTextBox1.AppendText("\n  Количество совпавших строк   " + ns);

                   
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
             
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e) // вторая кнопка  Ворда
        {
           // Word.Table table = Word.Tables.Add(_currentRange, numRows, numColumns, ref _missingObj, ref _missingObj);


        }
         
        }//-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }

 