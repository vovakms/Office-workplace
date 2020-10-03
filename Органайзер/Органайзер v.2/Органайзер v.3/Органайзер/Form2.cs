using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Органайзер
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e) // ---------------загрузка формы НАСТРОЙКИ
        {
         if(Properties.Settings.Default.PervZapusk == true ){   //-------------------------- проверяем ПЕРВЫЙ ли это запуск 
             Properties.Settings.Default.PervZapusk = false; // отмечаем что первый запуск настроек уже был
             Properties.Settings.Default.Save();
         }   
         else
         {
             textBox1.Text = Properties.Settings.Default.pKatEIIS;//  каталог ЕИИС
             textBox104.Text = Properties.Settings.Default.pKatEzhed ; // каталог БД Ежедневника
             textBox102.Text = Properties.Settings.Default.ipServ ;   //  ip сервера
             textBox103.Text = Properties.Settings.Default.portServ;   // порт сервервера
             
             textBox2.Text = Properties.Settings.Default.n1;   checkBox1.Checked = Properties.Settings.Default.v1;   textBox3.Text = Properties.Settings.Default.p1;// 1
             textBox4.Text = Properties.Settings.Default.n2;   checkBox2.Checked = Properties.Settings.Default.v2;   textBox5.Text = Properties.Settings.Default.p2;// 2
             textBox6.Text = Properties.Settings.Default.n3;   checkBox3.Checked = Properties.Settings.Default.v3;   textBox7.Text = Properties.Settings.Default.p3;// 3
             textBox8.Text = Properties.Settings.Default.n4;   checkBox4.Checked = Properties.Settings.Default.v4;   textBox9.Text = Properties.Settings.Default.p4;// 4
             textBox10.Text = Properties.Settings.Default.n5;  checkBox5.Checked = Properties.Settings.Default.v5;   textBox11.Text = Properties.Settings.Default.p5;// 5
             textBox12.Text = Properties.Settings.Default.n6;  checkBox6.Checked = Properties.Settings.Default.v6;   textBox13.Text = Properties.Settings.Default.p6;// 6
             textBox14.Text = Properties.Settings.Default.n7;  checkBox7.Checked = Properties.Settings.Default.v7;   textBox15.Text = Properties.Settings.Default.p7;// 7
             textBox16.Text = Properties.Settings.Default.n8;  checkBox8.Checked = Properties.Settings.Default.v8;   textBox17.Text = Properties.Settings.Default.p8;// 8
             textBox18.Text = Properties.Settings.Default.n9;  checkBox9.Checked = Properties.Settings.Default.v9;   textBox19.Text = Properties.Settings.Default.p9;// 9
             textBox20.Text = Properties.Settings.Default.n10; checkBox10.Checked = Properties.Settings.Default.v10; textBox21.Text = Properties.Settings.Default.p10;// 10
             
             textBox40.Text = Properties.Settings.Default.n11; checkBox20.Checked = Properties.Settings.Default.v11; textBox41.Text = Properties.Settings.Default.p11;// 11
             textBox38.Text = Properties.Settings.Default.n12;  checkBox19.Checked = Properties.Settings.Default.v12; textBox3.Text = Properties.Settings.Default.p12;// 12
             textBox36.Text = Properties.Settings.Default.n13;  checkBox18.Checked = Properties.Settings.Default.v13; textBox3.Text = Properties.Settings.Default.p13;// 13
             textBox34.Text = Properties.Settings.Default.n14;  checkBox17.Checked = Properties.Settings.Default.v14; textBox3.Text = Properties.Settings.Default.p14;// 14
             textBox32.Text = Properties.Settings.Default.n15;  checkBox16.Checked = Properties.Settings.Default.v15; textBox3.Text = Properties.Settings.Default.p15;// 15
             textBox30.Text = Properties.Settings.Default.n16;  checkBox15.Checked = Properties.Settings.Default.v16; textBox3.Text = Properties.Settings.Default.p16;// 16
             textBox28.Text = Properties.Settings.Default.n17;  checkBox14.Checked = Properties.Settings.Default.v17; textBox3.Text = Properties.Settings.Default.p17;// 17
             textBox26.Text = Properties.Settings.Default.n18; checkBox13.Checked = Properties.Settings.Default.v18; textBox3.Text = Properties.Settings.Default.p18;// 18
             textBox24.Text = Properties.Settings.Default.n19; checkBox12.Checked = Properties.Settings.Default.v19; textBox3.Text = Properties.Settings.Default.p19;// 19
             textBox22.Text = Properties.Settings.Default.n20; checkBox11.Checked = Properties.Settings.Default.v20; textBox3.Text = Properties.Settings.Default.p20;// 20
            
             textBox60.Text = Properties.Settings.Default.n21; checkBox30.Checked = Properties.Settings.Default.v21; textBox61.Text = Properties.Settings.Default.p21;// 21
             textBox58.Text = Properties.Settings.Default.n22; checkBox29.Checked = Properties.Settings.Default.v22; textBox59.Text = Properties.Settings.Default.p22;// 22
             textBox56.Text = Properties.Settings.Default.n23; checkBox28.Checked = Properties.Settings.Default.v23; textBox57.Text = Properties.Settings.Default.p23;// 23
             textBox54.Text = Properties.Settings.Default.n24; checkBox27.Checked = Properties.Settings.Default.v24; textBox55.Text = Properties.Settings.Default.p24;// 24
             textBox52.Text = Properties.Settings.Default.n25; checkBox26.Checked = Properties.Settings.Default.v25; textBox53.Text = Properties.Settings.Default.p25;// 25
             textBox50.Text = Properties.Settings.Default.n26; checkBox25.Checked = Properties.Settings.Default.v26; textBox51.Text = Properties.Settings.Default.p26;// 26
             textBox48.Text = Properties.Settings.Default.n27; checkBox24.Checked = Properties.Settings.Default.v27; textBox49.Text = Properties.Settings.Default.p27;// 27
             textBox46.Text = Properties.Settings.Default.n28; checkBox23.Checked = Properties.Settings.Default.v28; textBox47.Text = Properties.Settings.Default.p28;// 28
             textBox44.Text = Properties.Settings.Default.n29; checkBox22.Checked = Properties.Settings.Default.v29; textBox45.Text = Properties.Settings.Default.p29;// 29
             textBox42.Text = Properties.Settings.Default.n30; checkBox21.Checked = Properties.Settings.Default.v30; textBox43.Text = Properties.Settings.Default.p30;// 30
             
             textBox80.Text = Properties.Settings.Default.n31; checkBox40.Checked = Properties.Settings.Default.v31; textBox81.Text = Properties.Settings.Default.p31;// 31
             textBox78.Text = Properties.Settings.Default.n32; checkBox39.Checked = Properties.Settings.Default.v32; textBox79.Text = Properties.Settings.Default.p32;// 32
             textBox76.Text = Properties.Settings.Default.n33; checkBox38.Checked = Properties.Settings.Default.v33; textBox77.Text = Properties.Settings.Default.p33;// 33
             textBox74.Text = Properties.Settings.Default.n34; checkBox37.Checked = Properties.Settings.Default.v34; textBox75.Text = Properties.Settings.Default.p34;// 34
             textBox72.Text = Properties.Settings.Default.n35; checkBox36.Checked = Properties.Settings.Default.v35; textBox73.Text = Properties.Settings.Default.p35;// 35
             textBox70.Text = Properties.Settings.Default.n36; checkBox35.Checked = Properties.Settings.Default.v36; textBox71.Text = Properties.Settings.Default.p36;// 36
             textBox68.Text = Properties.Settings.Default.n37; checkBox34.Checked = Properties.Settings.Default.v37; textBox69.Text = Properties.Settings.Default.p37;// 37
             textBox66.Text = Properties.Settings.Default.n38; checkBox33.Checked = Properties.Settings.Default.v38; textBox67.Text = Properties.Settings.Default.p38;// 38
             textBox64.Text = Properties.Settings.Default.n39; checkBox32.Checked = Properties.Settings.Default.v39; textBox65.Text = Properties.Settings.Default.p39;// 39
             textBox62.Text = Properties.Settings.Default.n40; checkBox31.Checked = Properties.Settings.Default.v40; textBox63.Text = Properties.Settings.Default.p40;// 40
             
             textBox100.Text = Properties.Settings.Default.n41; checkBox50.Checked = Properties.Settings.Default.v41; textBox101.Text = Properties.Settings.Default.p41;// 41
             textBox98.Text = Properties.Settings.Default.n42; checkBox49.Checked = Properties.Settings.Default.v42; textBox99.Text = Properties.Settings.Default.p42;// 42
             textBox96.Text = Properties.Settings.Default.n43; checkBox48.Checked = Properties.Settings.Default.v43; textBox97.Text = Properties.Settings.Default.p43;// 43
             textBox94.Text = Properties.Settings.Default.n44; checkBox47.Checked = Properties.Settings.Default.v44; textBox95.Text = Properties.Settings.Default.p44;// 44
             textBox92.Text = Properties.Settings.Default.n45; checkBox46.Checked = Properties.Settings.Default.v45; textBox93.Text = Properties.Settings.Default.p45;// 45
             textBox90.Text = Properties.Settings.Default.n46; checkBox45.Checked = Properties.Settings.Default.v46; textBox91.Text = Properties.Settings.Default.p46;// 46
             textBox88.Text = Properties.Settings.Default.n47; checkBox44.Checked = Properties.Settings.Default.v47; textBox89.Text = Properties.Settings.Default.p47;// 47
             textBox86.Text = Properties.Settings.Default.n48; checkBox43.Checked = Properties.Settings.Default.v48; textBox87.Text = Properties.Settings.Default.p48;// 48
             textBox84.Text = Properties.Settings.Default.n49; checkBox42.Checked = Properties.Settings.Default.v49; textBox85.Text = Properties.Settings.Default.p49;// 49
             textBox82.Text = Properties.Settings.Default.n50; checkBox41.Checked = Properties.Settings.Default.v50; textBox83.Text = Properties.Settings.Default.p50;// 50

             
         }
        }

        private void button1_Click(object sender, EventArgs e) // -----------------------нажали кнопку "СОХРАНИТЬ  СДЕЛАННЫЕ ИЗМЕНЕНИЯ В НАСТРОЙКАХ"
        {
            Properties.Settings.Default.pKatEIIS = textBox1.Text;//  каталог ЕИИС
            Properties.Settings.Default.pKatEzhed = textBox104.Text.ToString()   ; // каталог БД Ежедневника
            Properties.Settings.Default.ipServ = textBox102.Text;   //  ip сервера
            Properties.Settings.Default.portServ = textBox103.Text;   // порт сервервера

            Properties.Settings.Default.n1=textBox2.Text;			Properties.Settings.Default.v1	=checkBox1.Checked	;		Properties.Settings.Default.p1	=textBox3.Text	;//	1
            Properties.Settings.Default.n2=textBox4.Text;			Properties.Settings.Default.v2	=checkBox2.Checked	;		Properties.Settings.Default.p2	=textBox5.Text	;//	2
            Properties.Settings.Default.n3=textBox6.Text;			Properties.Settings.Default.v3	=checkBox3.Checked	;		Properties.Settings.Default.p3	=textBox7.Text	;//	3
Properties.Settings.Default.n4=textBox8.Text;			Properties.Settings.Default.v4	=checkBox4.Checked	;		Properties.Settings.Default.p4	=textBox9.Text	;//	4
Properties.Settings.Default.n5=textBox10.Text;			Properties.Settings.Default.v5	=checkBox5.Checked	;		Properties.Settings.Default.p5	=textBox11.Text	;//	5
Properties.Settings.Default.n6=textBox12.Text;			Properties.Settings.Default.v6	=checkBox6.Checked	;		Properties.Settings.Default.p6	=textBox13.Text	;//	6
Properties.Settings.Default.n7=textBox14.Text;			Properties.Settings.Default.v7	=checkBox7.Checked	;		Properties.Settings.Default.p7	=textBox15.Text	;//	7
Properties.Settings.Default.n8=textBox16.Text;			Properties.Settings.Default.v8	=checkBox8.Checked	;		Properties.Settings.Default.p8	=textBox17.Text	;//	8
Properties.Settings.Default.n9=textBox18.Text;			Properties.Settings.Default.v9	=checkBox9.Checked	;		Properties.Settings.Default.p9	=textBox19.Text	;//	9
Properties.Settings.Default.n10=textBox20.Text;			Properties.Settings.Default.v10	=checkBox10.Checked	;		Properties.Settings.Default.p10	=textBox21.Text	;//	10
				 				
Properties.Settings.Default.n11=textBox40.Text;			Properties.Settings.Default.v11	=checkBox20.Checked	;		Properties.Settings.Default.p11	=textBox41.Text	;//	11
Properties.Settings.Default.n12=textBox38.Text;			Properties.Settings.Default.v12	=checkBox19.Checked	;		Properties.Settings.Default.p12	=textBox3.Text	;//	12
Properties.Settings.Default.n13=textBox36.Text;			Properties.Settings.Default.v13	=checkBox18.Checked	;		Properties.Settings.Default.p13	=textBox3.Text	;//	13
Properties.Settings.Default.n14=textBox34.Text;			Properties.Settings.Default.v14	=checkBox17.Checked	;		Properties.Settings.Default.p14	=textBox3.Text	;//	14
Properties.Settings.Default.n15=textBox32.Text;			Properties.Settings.Default.v15	=checkBox16.Checked	;		Properties.Settings.Default.p15	=textBox3.Text	;//	15
Properties.Settings.Default.n16=textBox30.Text;			Properties.Settings.Default.v16	=checkBox15.Checked	;		Properties.Settings.Default.p16	=textBox3.Text	;//	16
Properties.Settings.Default.n17=textBox28.Text;			Properties.Settings.Default.v17	=checkBox14.Checked	;		Properties.Settings.Default.p17	=textBox3.Text	;//	17
Properties.Settings.Default.n18=textBox26.Text;			Properties.Settings.Default.v18	=checkBox13.Checked	;		Properties.Settings.Default.p18	=textBox3.Text	;//	18
Properties.Settings.Default.n19=textBox24.Text;			Properties.Settings.Default.v19	=checkBox12.Checked	;		Properties.Settings.Default.p19	=textBox3.Text	;//	19
Properties.Settings.Default.n20=textBox22.Text;			Properties.Settings.Default.v20	=checkBox11.Checked	;		Properties.Settings.Default.p20	=textBox3.Text	;//	20
										
Properties.Settings.Default.n21	=textBox60.Text	;	Properties.Settings.Default.v21	=checkBox30.Checked	;		Properties.Settings.Default.p21	=textBox61.Text	;//	21
Properties.Settings.Default.n22	=textBox58.Text	;	Properties.Settings.Default.v22	=checkBox29.Checked	;		Properties.Settings.Default.p22	=textBox59.Text	;//	22
Properties.Settings.Default.n23	=textBox56.Text	;	Properties.Settings.Default.v23	=checkBox28.Checked	;		Properties.Settings.Default.p23	=textBox57.Text	;//	23
Properties.Settings.Default.n24	=textBox54.Text	;	Properties.Settings.Default.v24	=checkBox27.Checked	;		Properties.Settings.Default.p24	=textBox55.Text	;//	24
Properties.Settings.Default.n25	=textBox52.Text	;	Properties.Settings.Default.v25	=checkBox26.Checked	;		Properties.Settings.Default.p25	=textBox53.Text	;//	25
Properties.Settings.Default.n26	=textBox50.Text	;	Properties.Settings.Default.v26	=checkBox25.Checked	;		Properties.Settings.Default.p26	=textBox51.Text	;//	26
Properties.Settings.Default.n27	=textBox48.Text	;	Properties.Settings.Default.v27	=checkBox24.Checked	;		Properties.Settings.Default.p27	=textBox49.Text	;//	27
Properties.Settings.Default.n28	=textBox46.Text	;	Properties.Settings.Default.v28	=checkBox23.Checked	;		Properties.Settings.Default.p28	=textBox47.Text	;//	28
Properties.Settings.Default.n29	=textBox44.Text	;	Properties.Settings.Default.v29	=checkBox22.Checked	;		Properties.Settings.Default.p29	=textBox45.Text	;//	29
Properties.Settings.Default.n30	=textBox42.Text	;	Properties.Settings.Default.v30	=checkBox21.Checked	;		Properties.Settings.Default.p30	=textBox43.Text	;//	30
										
           Properties.Settings.Default.n31	=textBox80.Text	;	Properties.Settings.Default.v31	=checkBox40.Checked	;		Properties.Settings.Default.p31	=textBox81.Text	;//	31
           Properties.Settings.Default.n32	=textBox78.Text	;	Properties.Settings.Default.v32	=checkBox39.Checked	;		Properties.Settings.Default.p32	=textBox79.Text	;//	32
           Properties.Settings.Default.n33	=textBox76.Text	;	Properties.Settings.Default.v33	=checkBox38.Checked	;		Properties.Settings.Default.p33	=textBox77.Text	;//	33
           Properties.Settings.Default.n34	=textBox74.Text	;	Properties.Settings.Default.v34	=checkBox37.Checked	;		Properties.Settings.Default.p34	=textBox75.Text	;//	34
           Properties.Settings.Default.n35	=textBox72.Text	;	Properties.Settings.Default.v35	=checkBox36.Checked	;		Properties.Settings.Default.p35	=textBox73.Text	;//	35
           Properties.Settings.Default.n36	=textBox70.Text	;	Properties.Settings.Default.v36	=checkBox35.Checked	;		Properties.Settings.Default.p36	=textBox71.Text	;//	36
           Properties.Settings.Default.n37	=textBox68.Text	;	Properties.Settings.Default.v37	=checkBox34.Checked	;		Properties.Settings.Default.p37	=textBox69.Text	;//	37
           Properties.Settings.Default.n38	=textBox66.Text	;	Properties.Settings.Default.v38	=checkBox33.Checked	;		Properties.Settings.Default.p38	=textBox67.Text	;//	38
           Properties.Settings.Default.n39	=textBox64.Text	;	Properties.Settings.Default.v39	=checkBox32.Checked	;		Properties.Settings.Default.p39	=textBox65.Text	;//	39
           Properties.Settings.Default.n40	=textBox62.Text	;	Properties.Settings.Default.v40	=checkBox31.Checked	;		Properties.Settings.Default.p40	=textBox63.Text	;//	40
										

            Properties.Settings.Default.n41	=textBox100.Text;	Properties.Settings.Default.v41	=checkBox50.Checked	;		Properties.Settings.Default.p41	=textBox101.Text	;//	41
            Properties.Settings.Default.n42	=textBox98.Text	;	Properties.Settings.Default.v42	=checkBox49.Checked	;		Properties.Settings.Default.p42	=textBox99.Text	;//	42
            Properties.Settings.Default.n43	=textBox96.Text	;	Properties.Settings.Default.v43	=checkBox48.Checked	;		Properties.Settings.Default.p43	=textBox97.Text	;//	43
            Properties.Settings.Default.n44	=textBox94.Text	;	Properties.Settings.Default.v44	=checkBox47.Checked	;		Properties.Settings.Default.p44	=textBox95.Text	;//	44
            Properties.Settings.Default.n45	=textBox92.Text	;	Properties.Settings.Default.v45	=checkBox46.Checked	;		Properties.Settings.Default.p45	=textBox93.Text	;//	45
            Properties.Settings.Default.n46	=textBox90.Text	;	Properties.Settings.Default.v46	=checkBox45.Checked	;		Properties.Settings.Default.p46	=textBox91.Text	;//	46
            Properties.Settings.Default.n47	=textBox88.Text	;	Properties.Settings.Default.v47	=checkBox44.Checked	;		Properties.Settings.Default.p47	=textBox89.Text	;//	47
            Properties.Settings.Default.n48	=textBox86.Text	;	Properties.Settings.Default.v48	=checkBox43.Checked	;		Properties.Settings.Default.p48	=textBox87.Text	;//	48
            Properties.Settings.Default.n49	=textBox84.Text	;	Properties.Settings.Default.v49	=checkBox42.Checked	;		Properties.Settings.Default.p49	=textBox85.Text	;//	49
            Properties.Settings.Default.n50	=textBox82.Text	;	Properties.Settings.Default.v50	=checkBox41.Checked	;		Properties.Settings.Default.p50  =textBox83.Text	;//	50
            Properties.Settings.Default.Save();

            Close();
        }

        private void button4_Click(object sender, EventArgs e) // ---------  выбираем путь к каталогу БД Ежедневника
        {
             if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
               textBox104.Text = folderBrowserDialog1.SelectedPath;
         }

        private void button2_Click(object sender, EventArgs e) // -----------  выбираем путь к каталогу подсистем ЕИИС
        {
            //folderBrowserDialog1.RootFolder. =  "\\dbms2\ЕИИС\FILIAL";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Restart();
        }

        private void textBox103_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label61_Click(object sender, EventArgs e)
        {

        }









    }
}






