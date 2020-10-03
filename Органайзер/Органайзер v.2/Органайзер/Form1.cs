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

namespace Органайзер
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            //notifyIcon1.Visible = false;// делаем невидимой нашу иконку в трее
            notifyIcon1.MouseDoubleClick += new MouseEventHandler(notifyIcon1_MouseClick);// добавляем Эвент или событие по 2му клику мышки, вызывая функцию  notifyIcon1_MouseDoubleClick
            Resize += new System.EventHandler(this.Form1_Resize);// добавляем событие на изменение окна

            button4.Visible = Properties.Settings.Default.v1;
            button5.Visible = Properties.Settings.Default.v2;
            button6.Visible = Properties.Settings.Default.v3;
            button7.Visible = Properties.Settings.Default.v4;
            button8.Visible = Properties.Settings.Default.v5;
            button9.Visible = Properties.Settings.Default.v6;
            button10.Visible = Properties.Settings.Default.v7;
            button11.Visible = Properties.Settings.Default.v8;
            button12.Visible = Properties.Settings.Default.v9;
            button13.Visible = Properties.Settings.Default.v10;
            button14.Visible = Properties.Settings.Default.v11;

            button25.Visible = Properties.Settings.Default.v12;
            button24.Visible = Properties.Settings.Default.v13;
            button23.Visible = Properties.Settings.Default.v14;
            button22.Visible = Properties.Settings.Default.v15;
            button21.Visible = Properties.Settings.Default.v16;
            button20.Visible = Properties.Settings.Default.v17;
            button19.Visible = Properties.Settings.Default.v18;
            button18.Visible = Properties.Settings.Default.v19;
            button17.Visible = Properties.Settings.Default.v20;
            button16.Visible = Properties.Settings.Default.v21;
            button15.Visible = Properties.Settings.Default.v22;

            button35.Visible = Properties.Settings.Default.v23;
            button34.Visible = Properties.Settings.Default.v24;
            button33.Visible = Properties.Settings.Default.v25;
            button32.Visible = Properties.Settings.Default.v26;
            button31.Visible = Properties.Settings.Default.v27;
            button30.Visible = Properties.Settings.Default.v28;
            button29.Visible = Properties.Settings.Default.v29;
            button28.Visible = Properties.Settings.Default.v30;

            button51.Visible = Properties.Settings.Default.v31;
            button50.Visible = Properties.Settings.Default.v32;
            button49.Visible = Properties.Settings.Default.v33;
            button48.Visible = Properties.Settings.Default.v34;
            button47.Visible = Properties.Settings.Default.v35;
            button46.Visible = Properties.Settings.Default.v36;
            button45.Visible = Properties.Settings.Default.v37;
            button44.Visible = Properties.Settings.Default.v38;
            button43.Visible = Properties.Settings.Default.v39;
            button42.Visible = Properties.Settings.Default.v40;
            button41.Visible = Properties.Settings.Default.v41;
            button40.Visible = Properties.Settings.Default.v42;
            button39.Visible = Properties.Settings.Default.v43;
            button38.Visible = Properties.Settings.Default.v44;
            button37.Visible = Properties.Settings.Default.v45;
            button36.Visible = Properties.Settings.Default.v46;
            button52.Visible = Properties.Settings.Default.v47;
            button55.Visible = Properties.Settings.Default.v48;
            button54.Visible = Properties.Settings.Default.v49;
            button53.Visible = Properties.Settings.Default.v50;
      }

        private void Form1_Load(object sender, EventArgs e)
        {
            monthCalendar1.SelectionRange.Start  = DateTime.Today ;
            monthCalendar1.SelectionRange.End = DateTime.Today;

            label1.Text = DateTime.Today.ToString("dd MMMM yyyy");
            label3.Text = Environment.UserName;
            label7.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd MMMM yyyy");


            string pathF = Properties.Settings.Default.pKatEzhed.ToString() + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd");
            if (System.IO.File.Exists(pathF))
                richTextBox1.LoadFile(pathF);
            else
            {
                richTextBox1.Clear();
                richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);

            }
             
            
             
             

        }

        private void timer1_Tick(object sender, EventArgs e)// -------- таймер тикает ---------
        {
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)//--------- таскаем форму за ферхнюю панель
        {
            base.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            this.WndProc(ref m);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) // проверяем наше окно, и если оно было свернуто, делаем событие   
            {
             this.ShowInTaskbar = false;// прячем наше окно из панели
             notifyIcon1.Visible = true;// делаем нашу иконку в трее активной
            }
        }

        private void button1_Click(object sender, EventArgs e)//------- кнопка закрытие формы 
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e) // ------------  клик по иконке в трее
        {
            //notifyIcon1.Visible = false;// делаем нашу иконку скрытой
            this.ShowInTaskbar = true;  // возвращаем отображение окна в панели
            WindowState = FormWindowState.Normal;//разворачиваем окно
        }

        private void toolStripButton10_Click(object sender, EventArgs e) // открываем форму НАСТРОЕК
        {
            Form2 frm = new Form2();
            frm.Visible = true;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e) // -- -------  выбор даты 
        {
            label7.Text = monthCalendar1.SelectionRange.Start.Date.ToString("dd MMMM yyyy");
            string pathF =  Properties.Settings.Default.pKatEzhed.ToString()  +  monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd")  ;
            if (System.IO.File.Exists(pathF))
                richTextBox1.LoadFile(pathF);
            else
            {
                richTextBox1.Clear();
                richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);

            }
   
        }

        private void button27_Click(object sender, EventArgs e) // --------сохраняем 
        {
            string pathF = Properties.Settings.Default.pKatEzhed.ToString() + monthCalendar1.SelectionRange.Start.Date.ToString("yyyy-MM-dd")  ;

            richTextBox1.SaveFile(pathF, RichTextBoxStreamType.RichText);
        }


        private void toolStripButton1_Click(object sender, EventArgs e) // -------------- сжимаем панель КнОПОК
        {
            button4.Text = ""; button4.Width = 60;
            button5.Text = ""; button5.Width = 60;
            button6.Text = ""; button6.Width = 60;
            button7.Text = ""; button7.Width = 60;
            button8.Text = ""; button8.Width = 60;
            button9.Text = ""; button9.Width = 60;
            button10.Text = ""; button10.Width = 60;
            button11.Text = ""; button11.Width = 60;
            button12.Text = ""; button12.Width = 60;
            button13.Text = ""; button13.Width = 60;
            button14.Text = ""; button14.Width = 60;
            button25.Text = ""; button25.Width = 60;
            button24.Text = ""; button24.Width = 60;
            button23.Text = ""; button23.Width = 60;
            button22.Text = ""; button22.Width = 60;
            button21.Text = ""; button21.Width = 60;
            button20.Text = ""; button20.Width = 60;
            button19.Text = ""; button19.Width = 60;
            button18.Text = ""; button18.Width = 60;
            button17.Text = ""; button17.Width = 60;
            button16.Text = ""; button16.Width = 60;
            button15.Text = ""; button15.Width = 60;

            button35.Text = ""; button35.Width = 60;
            button34.Text = ""; button34.Width = 60;
            button33.Text = ""; button33.Width = 60;
            button32.Text = ""; button32.Width = 60;
            button31.Text = ""; button31.Width = 60;
            button30.Text = ""; button30.Width = 60;
            button29.Text = ""; button29.Width = 60;
            button28.Text = ""; button28.Width = 60;

            button51.Text = ""; button51.Width = 60;
            button50.Text = ""; button50.Width = 60;
            button49.Text = ""; button49.Width = 60;
            button48.Text = ""; button48.Width = 60;
            button47.Text = ""; button47.Width = 60;
            button46.Text = ""; button46.Width = 60;
            button45.Text = ""; button45.Width = 60;
            button44.Text = ""; button44.Width = 60;
            button43.Text = ""; button43.Width = 60;
            button42.Text = ""; button42.Width = 60;
            button41.Text = ""; button41.Width = 60;
            button40.Text = ""; button40.Width = 60;
            button39.Text = ""; button39.Width = 60;
            button38.Text = ""; button38.Width = 60;
            button37.Text = ""; button37.Width = 60;
            button36.Text = ""; button36.Width = 60;
            button52.Text = ""; button52.Width = 60;
            button55.Text = ""; button55.Width = 60;
            button54.Text = ""; button54.Width = 60;
            button53.Text = ""; button53.Width = 60;
             
            splitContainer1.SplitterDistance = 85 ;
        }

        private void button2_Click(object sender, EventArgs e)//------  желтая кнопка развернуть все форму на весь экран
        {
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 85;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)// ------------ развернуть панель кнопок
        {
            button4.Text = Properties.Settings.Default.n1 ; button4.Width = 174; // 1
            button5.Text = Properties.Settings.Default.n2; button5.Width = 174; // 2
            button6.Text = Properties.Settings.Default.n3; button6.Width = 174; // 3
            button7.Text = Properties.Settings.Default.n4; button7.Width = 174; // 4
            button8.Text = Properties.Settings.Default.n5; button8.Width = 174; // 5
            button9.Text = Properties.Settings.Default.n6; button9.Width = 174; // 6
            button10.Text = Properties.Settings.Default.n7 ; button10.Width = 174; // 7
            button11.Text = Properties.Settings.Default.n8 ; button11.Width = 174; // 8
            button12.Text = Properties.Settings.Default.n9 ; button12.Width = 174; // 9
            button13.Text = Properties.Settings.Default.n10 ; button13.Width = 174; // 10
            button14.Text = Properties.Settings.Default.n11 ; button14.Width = 174; // 11
            button25.Text = Properties.Settings.Default.n12 ; button25.Width = 174; // 12
            button24.Text = Properties.Settings.Default.n13 ; button24.Width = 174; // 13
            button23.Text = Properties.Settings.Default.n14 ; button23.Width = 174; // 14 
            button22.Text = Properties.Settings.Default.n15 ; button22.Width = 174; // 15
            button21.Text = Properties.Settings.Default.n16 ; button21.Width = 174; // 16
            button20.Text = Properties.Settings.Default.n17 ; button20.Width = 174; // 17
            button19.Text = Properties.Settings.Default.n18 ; button19.Width = 174; // 18
            button18.Text = Properties.Settings.Default.n19 ; button18.Width = 174; // 19
            button17.Text = Properties.Settings.Default.n20 ; button17.Width = 174; // 20
            button16.Text = Properties.Settings.Default.n21 ; button16.Width = 174; // 21
            button15.Text = Properties.Settings.Default.n22 ; button15.Width = 174; // 22

            button35.Text = Properties.Settings.Default.n23 ; button35.Width = 174; // 23
            button34.Text = Properties.Settings.Default.n24 ; button34.Width = 174; // 24
            button33.Text = Properties.Settings.Default.n25 ; button33.Width = 174; // 25
            button32.Text = Properties.Settings.Default.n26 ; button32.Width = 174; // 26 
            button31.Text = Properties.Settings.Default.n27 ; button31.Width = 174; // 27
            button30.Text = Properties.Settings.Default.n28 ; button30.Width = 174; // 28
            button29.Text = Properties.Settings.Default.n29 ; button29.Width = 174; // 29
            button28.Text = Properties.Settings.Default.n30 ; button28.Width = 174; // 30

            button51.Text = Properties.Settings.Default.n31 ; button51.Width = 174; // 31
            button50.Text = Properties.Settings.Default.n32 ; button50.Width = 174; // 32
            button49.Text = Properties.Settings.Default.n33 ; button49.Width = 174; // 33
            button48.Text = Properties.Settings.Default.n34 ; button48.Width = 174; // 34
            button47.Text = Properties.Settings.Default.n35 ; button47.Width = 174; // 35
            button46.Text = Properties.Settings.Default.n36 ; button46.Width = 174; // 36
            button45.Text = Properties.Settings.Default.n37 ; button45.Width = 174; // 37
            button44.Text = Properties.Settings.Default.n38 ; button44.Width = 174; // 38
            button43.Text = Properties.Settings.Default.n39 ; button43.Width = 174; // 39
            button42.Text = Properties.Settings.Default.n40 ; button42.Width = 174; // 40
            button41.Text = Properties.Settings.Default.n41 ; button41.Width = 174; // 41
            button40.Text = Properties.Settings.Default.n42 ; button40.Width = 174; // 42
            button39.Text = Properties.Settings.Default.n43 ; button39.Width = 174; // 43
            button38.Text = Properties.Settings.Default.n44 ; button38.Width = 174; // 44
            button37.Text = Properties.Settings.Default.n45 ; button37.Width = 174; // 45
            button36.Text = Properties.Settings.Default.n46 ; button36.Width = 174; // 46
            button52.Text = Properties.Settings.Default.n47 ; button52.Width = 174; // 47
            button55.Text = Properties.Settings.Default.n48 ; button55.Width = 174; // 48
            button54.Text = Properties.Settings.Default.n49 ; button54.Width = 174; // 49
            button53.Text = Properties.Settings.Default.n50 ; button53.Width = 174; // 50

            splitContainer1.SplitterDistance = 195;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p1  ); // 1 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p2); // 2
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p3); // 3
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p4 ); // 4
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p5); // 5
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p6); // 6
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p7); // 7
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p8); // 8
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p9); // 9
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p10); // 10
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p11); // 11
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p12); // 12
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p13); // 13
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p14); // 14
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p15); // 15
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p16); // 16
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p17); // 17
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p18); // 18
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p19); // 19
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p20); // 20
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p21); // 21
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p22); // 22
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p23); // 23
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p24); // 24
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p25); // 25
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p26); // 26
        }

        private void button31_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p27); // 27
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p28); // 28
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p29); // 29
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p30); // 30
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p31); // 31
        }

        private void button50_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p32); // 32
        }

        private void button49_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p33); // 33
        }

        private void button48_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p34); // 34
        }

        private void button47_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p35); // 35
        }

        private void button46_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p36); // 36
        }

        private void button45_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p37); // 37
        }

        private void button44_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p38); // 38
        }

        private void button43_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p39); // 39
        }

        private void button42_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p40); // 40
        }

        private void button41_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p41); // 41
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p42); // 42
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p43); // 43
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p44); // 44
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p45); // 45
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p46); // 46
        }

        private void button52_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p47); // 47
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p48); // 48
        }

        private void button54_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p49); // 49
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Process.Start(Properties.Settings.Default.pKatEIIS + Properties.Settings.Default.p50); // 50
        }

        
       

        










    }
}
