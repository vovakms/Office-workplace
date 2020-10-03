using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
 
using System.Diagnostics;

using System.Net.NetworkInformation  ;

using System.Threading;

using System.Net;
using System.Net.Sockets;

namespace Органайзер
{
    public partial class Form1 : Form
    {
        private string UserName = System.Environment.UserName.ToString();
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;

        private delegate void UpdateLogCallback(string strMessage);// Необходимые для обновления формы с сообщениями от другого потока
        private delegate void CloseConnectionCallback(string strReason);// Необходимые для установки формы к «отключенного» состоянии из другого потока

        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;

        public Form1()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit); //На выходе из приложения, не забудьте отключить первый

            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            //notifyIcon1.Visible = false;// делаем невидимой нашу иконку в трее
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseClick);// добавляем Эвент или событие по   клику мышки 
            Resize += new System.EventHandler( Form1_Resize);// добавляем событие на изменение окна

            toolStrip3.Items[0].Visible = Properties.Settings.Default.v1;  // -- 1
            toolStrip3.Items[1].Visible = Properties.Settings.Default.v2;  // -- 2
            toolStrip3.Items[2].Visible = Properties.Settings.Default.v3;  // -- 3
            toolStrip3.Items[3].Visible = Properties.Settings.Default.v4;  // -- 4
            toolStrip3.Items[4].Visible = Properties.Settings.Default.v5;  // -- 5
            toolStrip3.Items[5].Visible = Properties.Settings.Default.v6;  // -- 6
            toolStrip3.Items[6].Visible = Properties.Settings.Default.v7;  // -- 7
            toolStrip3.Items[7].Visible = Properties.Settings.Default.v8;  // -- 8
            toolStrip3.Items[8].Visible = Properties.Settings.Default.v9;  // -- 9
            toolStrip3.Items[9].Visible = Properties.Settings.Default.v10;   // -- 10
            toolStrip3.Items[10].Visible = Properties.Settings.Default.v11;  // -- 11
            toolStrip3.Items[11].Visible = Properties.Settings.Default.v12;  // -- 12
            toolStrip3.Items[12].Visible = Properties.Settings.Default.v13;  // -- 13
            toolStrip3.Items[13].Visible = Properties.Settings.Default.v14;  // -- 14
            toolStrip3.Items[14].Visible = Properties.Settings.Default.v15;  // -- 15
            toolStrip3.Items[15].Visible = Properties.Settings.Default.v16;  // -- 16
            toolStrip3.Items[16].Visible = Properties.Settings.Default.v17;  // -- 17
            toolStrip3.Items[17].Visible = Properties.Settings.Default.v18;  // -- 18
            toolStrip3.Items[18].Visible = Properties.Settings.Default.v19;  // -- 19
            toolStrip3.Items[19].Visible = Properties.Settings.Default.v20;  // -- 20
            toolStrip3.Items[20].Visible = Properties.Settings.Default.v21;  // -- 21
            toolStrip3.Items[21].Visible = Properties.Settings.Default.v22;  // -- 22
            toolStrip3.Items[22].Visible = Properties.Settings.Default.v23;  // -- 23
            toolStrip3.Items[23].Visible = Properties.Settings.Default.v24;  // -- 24
            toolStrip3.Items[24].Visible = Properties.Settings.Default.v25;  // -- 25
            toolStrip3.Items[25].Visible = Properties.Settings.Default.v26;  // -- 26
            toolStrip3.Items[26].Visible = Properties.Settings.Default.v27;  // -- 27
            toolStrip3.Items[27].Visible = Properties.Settings.Default.v28;  // -- 28
            toolStrip3.Items[28].Visible = Properties.Settings.Default.v29;  // -- 29
            toolStrip3.Items[29].Visible = Properties.Settings.Default.v30;  // -- 30
            toolStrip3.Items[30].Visible = Properties.Settings.Default.v31;  // -- 31
            toolStrip3.Items[31].Visible = Properties.Settings.Default.v32;  // -- 32
            toolStrip3.Items[32].Visible = Properties.Settings.Default.v33;  // -- 33
            toolStrip3.Items[33].Visible = Properties.Settings.Default.v34;  // -- 34
            toolStrip3.Items[34].Visible = Properties.Settings.Default.v35;  // -- 35
            toolStrip3.Items[35].Visible = Properties.Settings.Default.v36;  // -- 36
            toolStrip3.Items[36].Visible = Properties.Settings.Default.v37;  // -- 37
            toolStrip3.Items[37].Visible = Properties.Settings.Default.v38;  // -- 38
            toolStrip3.Items[38].Visible = Properties.Settings.Default.v39;  // -- 39
            toolStrip3.Items[39].Visible = Properties.Settings.Default.v40;  // -- 40
            toolStrip3.Items[40].Visible = Properties.Settings.Default.v41;  // -- 41
            toolStrip3.Items[41].Visible = Properties.Settings.Default.v42;  // -- 42
            toolStrip3.Items[42].Visible = Properties.Settings.Default.v43;  // -- 43
            toolStrip3.Items[43].Visible = Properties.Settings.Default.v44;  // -- 44
            toolStrip3.Items[44].Visible = Properties.Settings.Default.v45;  // -- 45
            toolStrip3.Items[45].Visible = Properties.Settings.Default.v46;  // --46
            toolStrip3.Items[46].Visible = Properties.Settings.Default.v47;  // -- 47
            toolStrip3.Items[47].Visible = Properties.Settings.Default.v48;  // - 48
            toolStrip3.Items[48].Visible = Properties.Settings.Default.v49;  // -- 49
            toolStrip3.Items[49].Visible = Properties.Settings.Default.v50;  // -- 50

            toolStrip3.Items[0].Text = Properties.Settings.Default.n1;  // -- 1
            toolStrip3.Items[1].Text = Properties.Settings.Default.n2;  // -- 2
            toolStrip3.Items[2].Text = Properties.Settings.Default.n3;    // -- 3
            toolStrip3.Items[3].Text = Properties.Settings.Default.n4;  // -- 4
            toolStrip3.Items[4].Text = Properties.Settings.Default.n5;  // -- 5
            toolStrip3.Items[5].Text = Properties.Settings.Default.n6;  // -- 6
            toolStrip3.Items[6].Text = Properties.Settings.Default.n7;  // -- 7
            toolStrip3.Items[7].Text = Properties.Settings.Default.n8;  // -- 8
            toolStrip3.Items[8].Text = Properties.Settings.Default.n9;  // -- 9
            toolStrip3.Items[9].Text = Properties.Settings.Default.n10;   // -- 10
            toolStrip3.Items[10].Text = Properties.Settings.Default.n11;  // -- 11
            toolStrip3.Items[11].Text = Properties.Settings.Default.n12;  // -- 12
            toolStrip3.Items[12].Text = Properties.Settings.Default.n13;  // -- 13
            toolStrip3.Items[13].Text = Properties.Settings.Default.n14;  // -- 14
            toolStrip3.Items[14].Text = Properties.Settings.Default.n15;  // -- 15
            toolStrip3.Items[15].Text = Properties.Settings.Default.n16;  // -- 16
            toolStrip3.Items[16].Text = Properties.Settings.Default.n17;  // -- 17
            toolStrip3.Items[17].Text = Properties.Settings.Default.n18;  // -- 18
            toolStrip3.Items[18].Text = Properties.Settings.Default.n19;  // -- 19
            toolStrip3.Items[19].Text = Properties.Settings.Default.n20;  // -- 20
            toolStrip3.Items[20].Text = Properties.Settings.Default.n21;  // -- 21
            toolStrip3.Items[21].Text = Properties.Settings.Default.n22;  // -- 22
            toolStrip3.Items[22].Text = Properties.Settings.Default.n23;  // -- 23
            toolStrip3.Items[23].Text = Properties.Settings.Default.n24;  // -- 24
            toolStrip3.Items[24].Text = Properties.Settings.Default.n25;  // -- 25
            toolStrip3.Items[25].Text = Properties.Settings.Default.n26;  // -- 26
            toolStrip3.Items[26].Text = Properties.Settings.Default.n27;  // -- 27
            toolStrip3.Items[27].Text = Properties.Settings.Default.n28;  // -- 28
            toolStrip3.Items[28].Text = Properties.Settings.Default.n29;  // -- 29
            toolStrip3.Items[29].Text = Properties.Settings.Default.n30;  // -- 30
            toolStrip3.Items[30].Text = Properties.Settings.Default.n31;  // -- 31
            toolStrip3.Items[31].Text = Properties.Settings.Default.n32;  // -- 32
            toolStrip3.Items[32].Text = Properties.Settings.Default.n33;  // -- 33
            toolStrip3.Items[33].Text = Properties.Settings.Default.n34;  // -- 34
            toolStrip3.Items[34].Text = Properties.Settings.Default.n35;  // -- 35
            toolStrip3.Items[35].Text = Properties.Settings.Default.n36;  // -- 36
            toolStrip3.Items[36].Text = Properties.Settings.Default.n37;  // -- 37
            toolStrip3.Items[37].Text = Properties.Settings.Default.n38;  // -- 38
            toolStrip3.Items[38].Text = Properties.Settings.Default.n39;  // -- 39
            toolStrip3.Items[39].Text = Properties.Settings.Default.n40;  // -- 40
            toolStrip3.Items[40].Text = Properties.Settings.Default.n41;  // -- 41
            toolStrip3.Items[41].Text = Properties.Settings.Default.n42;  // -- 42
            toolStrip3.Items[42].Text = Properties.Settings.Default.n43;  // -- 43
            toolStrip3.Items[43].Text = Properties.Settings.Default.n44;  // -- 44
            toolStrip3.Items[44].Text = Properties.Settings.Default.n45;  // -- 45
            toolStrip3.Items[45].Text = Properties.Settings.Default.n46;  // --46
            toolStrip3.Items[46].Text = Properties.Settings.Default.n47;  // -- 47
            toolStrip3.Items[47].Text = Properties.Settings.Default.n48;  // - 48
            toolStrip3.Items[48].Text = Properties.Settings.Default.n49;  // -- 49
            toolStrip3.Items[49].Text = Properties.Settings.Default.n50;  // -- 50
            
            Directory.CreateDirectory("org");

            if ( Properties.Settings.Default.PanKnopSvern )
            {
                for (int i = 0; i < toolStrip3.Items.Count; i++)
                {
                 toolStrip3.Items[i].ToolTipText = toolStrip3.Items[i].Text;
                 toolStrip3.Items[i].Text = "";
                }
            }



      }

        private void Form1_Load(object sender, EventArgs e) // -------------- при загрузке формы
        {
            try
            {
                if (Connected == false)//Если не подключенны
                {
                    InitializeConnection();// подключаемься
                }
            }
            catch
            {
                label4.Text = "Подключить";
            }
             
            
            monthCalendar1.SelectionRange.Start  = DateTime.Today ;
            monthCalendar1.SelectionRange.End = DateTime.Today;

            label1.Text = DateTime.Today.ToString("dd MMMM yyyy");
            label3.Text = Environment.UserName;
            label7.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd MMMM yyyy");
            
            string locDB = "org\\" + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd") ;

            if ( File.Exists( locDB ) )  
                    richTextBox2.LoadFile( locDB ) ; //  показываем  в ричЕдит2
            else
                {
                    richTextBox2.Clear();
                    richTextBox2.SaveFile( locDB  , RichTextBoxStreamType.RichText);

                }


            Thread myThread = new Thread(func); //Создаем новый объект потока (Thread)
            myThread.Start(); //запускаем поток
             
              
        }

        private void func() // функция подключаемь ежедневник
        {
         //try
         //   {
             string pathF = Properties.Settings.Default.pKatEzhed.ToString() + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
                
                if ( File.Exists(pathF) ) // --- проверяем существует ли файл на сегодняшную дату
                 {
                     richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.LoadFile(pathF); }); //  показываем  в ричЕдит
                 }
                else
                {
                 richTextBox1.Invoke((MethodInvoker)delegate{ richTextBox1.Clear(); richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);  });
                }
                
           // }
            //catch
            //{
            //    richTextBox1.Invoke((MethodInvoker)delegate
            //    {
            //        richTextBox1.AppendText("\r\n       НЕТ   СЕТИ  проверь  VipNet");
            //    });
                 
            //    //richTextBox1.AppendText(" НЕТ   СЕТИ  проверь  VipNet");
            //    //Form3 frm = new Form3();
            //    //frm.Visible = true;
            //}
             
        }
         
        private void timer1_Tick(object sender, EventArgs e)// -------- таймер тикает ---------
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)//--------- таскаем форму за верхнюю панель
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void Form1_Resize(object sender, EventArgs e) //--------- при изменении размеров ФОРМЫ
        {
            if (WindowState == FormWindowState.Minimized) // проверяем наше окно, и если оно было свернуто, делаем событие   
            {
             ShowInTaskbar = false;// прячем наше окно из панели
             notifyIcon1.Visible = true;// делаем нашу иконку в трее активной

             //Hide();

             //notifyIcon1.BalloonTipTitle = "Программа была спрятана";
             //notifyIcon1.BalloonTipText = "Обратите внимание что программа была спрятана в трей и продолжит свою работу.";
             notifyIcon1.ShowBalloonTip(5000); // Параметром указываем количество миллисекунд, которое будет показываться подсказка
            }

 
        }

        private void button1_Click(object sender, EventArgs e)//------- кнопка закрытие формы 
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e) // ----------  помощь СПРАВКА
        {
            Form4 frm = new Form4();
            frm.Visible = true;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e) // ------------  клик по иконке в трее
        {
            //notifyIcon1.Visible = false;// делаем нашу иконку скрытой
            this.ShowInTaskbar = true;  // возвращаем отображение окна в панели
            WindowState = FormWindowState.Maximized;//разворачиваем окно  
        }

        private void toolStripButton10_Click(object sender, EventArgs e) // открываем форму НАСТРОЕК
        {
            Form2 frm = new Form2();
            frm.Visible = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e) // -- -------  выбор даты 
        {
            label7.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd MMMM yyyy");
            string locDB = "org\\" + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
            if (System.IO.File.Exists(locDB))
                        richTextBox2.LoadFile(locDB);
            else
            {
                richTextBox2.Clear();
                richTextBox2.SaveFile(locDB, RichTextBoxStreamType.RichText);
            }
           
           string pathF = Properties.Settings.Default.pKatEzhed.ToString() + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
           if (System.IO.File.Exists(pathF))
           {
                    richTextBox1.LoadFile(pathF);
                    richTextBox2.LoadFile("org\\" + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd"));
            }
            else
            {
                    richTextBox1.Clear();
                    richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);
                    richTextBox2.Clear();
                    richTextBox2.SaveFile("org\\" + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd"), RichTextBoxStreamType.RichText);
            }
         
        }

        private void button27_Click(object sender, EventArgs e) // -------- нажали кнопку "СОХРАНИТЬ" 
        {
            richTextBox2.SaveFile("org\\" + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd"), RichTextBoxStreamType.RichText);
          
            try
            {
                string pathF = Properties.Settings.Default.pKatEzhed.ToString() + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
                richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);
            }
            catch
            {
             Form3 frm = new Form3() ;
                frm.Visible = true ;
            }

        }
         
       

        private void button2_Click(object sender, EventArgs e)//------  желтая кнопка развернуть все форму на весь экран
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;

            //splitContainer1.SplitterDistance = 85;


        }

        

        private void toolStripButton7_Click(object sender, EventArgs e) 
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p1); //---- 1
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p2); //---- 2
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p3); //---- 3
        }

                private void toolStripButton18_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p4); //---- 4
        }
        
        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p5); //---- 5
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p6); //---- 6
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p7); //---- 7
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p8); //---- 8
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p9); //---- 9
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p10); //---- 10
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p11); //---- 11
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p12); //---- 12
        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p13); //---- 13
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p14); //---- 14
        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p15); //---- 15
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p16); //---- 16
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p17); //---- 17
        }

        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p18); //---- 18
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p19); //---- 19
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p20); //---- 20
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p21); //---- 21
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p22); //---- 22
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p23); //---- 23
        }

        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p24); //---- 24
        }

        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p25); //---- 25
        }

        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p26); //---- 26
        }

        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p27); //---- 27
        }

        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p28); //---- 28
        }

        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p29); //---- 29
        }

        private void toolStripButton44_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p30); //---- 30
        }

        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p31); //---- 31
        }

        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p32); //---- 32
        }

        private void toolStripButton47_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p33); //---- 33
        }

        private void toolStripButton48_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p34); //---- 34
        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p35); //---- 35
        }

        private void toolStripButton50_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p36); //---- 36
        }

        private void toolStripButton51_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p37); //---- 37  Administrator
        }

        private void toolStripButton52_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p38); //---- 38   HyTech DBMS Explorer
        }

        private void toolStripButton53_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p39); //---- 39 Быстрые отчеты  
        }

        private void toolStripButton54_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p40); //---- 40   Сверка ЗАГС-ЕИИС 
        }

        private void toolStripButton55_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p41); //---- 41  Консультант + 
        }

        private void toolStripButton56_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p42); //---- 42 Анкета ФСС "ВАШЕ МНЕНИЕ" 
        }

        private void toolStripButton57_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p43); //---- 43 
        }

        private void toolStripButton58_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p44); //---- 44 
        }

        private void toolStripButton59_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p45); //---- 45 
        }

        private void toolStripButton60_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p46); //---- 46
        }

        private void toolStripButton61_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p47); //---- 47 
        }

        private void toolStripButton62_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p48); //---- 48 
        }

        private void toolStripButton63_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p49); //---- 49 
        }

        private void toolStripButton64_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p50); //---- 50
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Properties.Settings.Default.toolStrip3 = this.Razer.Checked;

            //Properties.Settings.Default.Save();

            CloseConnection("Закрыли приложение");

           // Close();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton67_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton69_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButton1_Click(object sender, EventArgs e) // --------------сворачиваем панель КНОПОК
        {
           

        }

        private void toolStripButton2_Click(object sender, EventArgs e)// ------------ развернуть панель КНОПОК
        {
           
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.PanKnopSvern == true)
            {
                for (int i = 0; i < toolStrip3.Items.Count; i++)
                    toolStrip3.Items[i].Text = toolStrip3.Items[i].ToolTipText;

                // splitContainer1.SplitterDistance = 195;

                Properties.Settings.Default.PanKnopSvern = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                for (int i = 0; i < toolStrip3.Items.Count; i++)
                {
                    //toolStrip3.Items[i].Visible = false; 
                    toolStrip3.Items[i].ToolTipText = toolStrip3.Items[i].Text;
                    toolStrip3.Items[i].Text = "";
                }
                // splitContainer1.SplitterDistance = 50 ;

                Properties.Settings.Default.PanKnopSvern = true;
                Properties.Settings.Default.Save();
            }

        }
       
        //--------------------------------------------------------------------------------------------------------------


        public void OnApplicationExit(object sender, EventArgs e)// При выходе из приложения
        {
            if (Connected == true)
            {
                Connected = false;
                swSender.Close();
                srReceiver.Close();
                tcpServer.Close();
            }
        }

        private void InitializeConnection() //   подключились к серверу
        {
            ipAddr = IPAddress.Parse(Properties.Settings.Default.ipServ); //  IP-адрес из строки в объект IP-адрес
            int port = Convert.ToInt32(Properties.Settings.Default.portServ);

            tcpServer = new TcpClient();// Начать новое TCP соединение с сервером  
            tcpServer.Connect(ipAddr, port); // 

            Connected = true;//  подключены или нет

            label4.Text = "Подключен" ;

            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(UserName + ";" + Properties.Settings.Default.group);// пошел первый пакет ИмяЮзера;ГруппаЮзера   
            swSender.Flush();

            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));// Начните нить для приема сообщений и дальнейшего общения
            thrMessaging.Start();
        }

        private void ReceiveMessages() // Прием сообщений
        {
            srReceiver = new StreamReader(tcpServer.GetStream());  //  ответ от сервера
            string ConResponse = srReceiver.ReadLine();

            if (ConResponse[0] == '1') // Если первый символ является 1, соединение было успешным
            {
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "ПОДКЛЮЧИЛИСЬ!" }); // Update the form to tell it we are now connected
            }
            else
            {
                string Reason = "НЕТ связи: ";
                Reason += ConResponse.Substring(2, ConResponse.Length - 2);// Extract the reason out of the response message. The reason starts at the 3rd character
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason }); // Update the form with the reason why we couldn't connect
                return;
            }

            while (Connected) // Пока   успешное подключение, читаем входящий линий с сервера
            {
                try
                {
                    this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });// Показать сообщения 
                }
                catch
                {
                    label4.Text = "отключились от сервера";
                }

            }
        }

        private void UpdateLog(string strMessage) // Этот метод вызывается из другого потока, чтобы обновить TextBox  
        {
            if (strMessage.IndexOf("<listUsers>") > -1) // если пришел список юзеров то отображаем его в виде дерева (treeView1)
            {
                int sG = 0, sUs = 0;
                String[] wS = strMessage.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < wS.Length; i++)
                {

                    String[] wdS = wS[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    TreeNode treeNode, treeNode11; // , treeNodeGr
                    TreeNode[] ResUs, ResGr;

                    ResUs = treeView1.Nodes.Find(wdS[0], true);
                    ResGr = treeView1.Nodes.Find(wdS[1], true);
                    try
                    {
                        if (ResUs[0] != null) { }
                    }
                    catch
                    {

                        treeNode = treeView1.Nodes.Add(wdS[1]);// создаем узел  название ГРУППЫ // treeView1.Nodes.Add(wdS[1]).Nodes.Add(wdS[1]).Nodes.Add(wdS[1].Text);
                        treeNode.Name = wdS[1];
                        treeNode11 = treeNode.Nodes.Add(wdS[0]); // Создаем  подузел для него.
                        treeNode11.Name = wdS[0];
                        treeNode.ForeColor = ColorTranslator.FromHtml("#FF6A00");
                        sG++; sUs++;
                    }

                    //treeNodeGr = ResGr[0];
                    //treeView1.Nodes.IndexOf( treeNodeGr ) ;
                    ////tNGR11 = tNGR.Nodes.Add(wdS[0]); tNGR11.Name = wdS[0];
                    //if (  ResGr[0].Name != "")
                    //{ }


                    //treeNode = treeView1.Nodes.Add(wdS[1]);// создаем узел  название ГРУППЫ // treeView1.Nodes.Add(wdS[1]).Nodes.Add(wdS[1]).Nodes.Add(wdS[1].Text);
                    //treeNode.Name = wdS[1];
                    //treeNode11 = treeNode.Nodes.Add(wdS[0]); // Создаем  подузел для него.
                    //treeNode11.Name = wdS[0];
                    //treeNode.ForeColor = ColorTranslator.FromHtml("#FF6A00");
                    //sG++; sUs++;






                    // string rrr = treeView1.Nodes.IndexOf(     ;

                    ////if (rrr == "" )  // "System.Windows.Forms.TreeNode[]"
                    ////{
                    ////    
                    ////}



                    //if (treeView1.Nodes.Find(wdS[1],false) == true)
                    //    if (treeView1.Nodes.Find(wdS[1], true) == false)


                }
                label9.Text = sG.ToString();
                label10.Text = sUs.ToString();
            }
            else
            {
                textBox1.AppendText(strMessage + "\r\n"); // Добавляем текст сообщение 
            }

            //treeView1.Nodes.Add("1", "1");
            //treeView1.Nodes.Add("2", "2");
            //treeView1.Nodes["2"].Nodes.Add("2.1", "2.1");
            //treeView1.Nodes["2"].Nodes.Add("2.2", "2.2");
            //treeView1.Nodes["2"].Nodes.Add("2.3", "2.3");
            //treeView1.Nodes.Add("3", "3");
            //treeView1.Nodes.Add("4", "4");
            //treeView1.Nodes["2"].ForeColor = Color.Red;
            //treeView1.Nodes["2"].Nodes["2.2"].ForeColor = Color.Red;
            //treeView1.Nodes["2"].Name
        }


        private void CloseConnection(string Reason)  //------------------------ закрываем текущее соединение
        {
            textBox1.AppendText(Reason + "\r\n"); // Показываем причину, почему соединение завершено

            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();

            Application.Exit();

        }

        private void SendMessage()//------------------------------------------- отправка  сообщения
        {
            if (textBox2.Lines.Length >= 1)
            {
                string kanal = "общиий";
                swSender.WriteLine(kanal + ";" + Properties.Settings.Default.group + ";" + UserName + ";" + textBox2.Text);//  2  СООБЩЕНИЕ
                //swSender.WriteLine(kanal);//------------------------------- 3  канал
                //swSender.WriteLine(Properties.Settings.Default.group );// 4  группа
                //swSender.WriteLine(UserName);  //--------------------- 5    юзер

                swSender.Flush();
                textBox2.Lines = null;
            }
            textBox2.Text = "";
        }

        private void button26_Click(object sender, EventArgs e)
        {
            SendMessage(); // вызываем отправку сообщения 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) // если нажали клаишу "Enter"
                SendMessage();// отправляем сообщение
        }
        










    }
}
