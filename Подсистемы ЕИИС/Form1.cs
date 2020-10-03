using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

  
using System.IO; 
 
namespace Подсистемы_ЕИИС
{
    public partial class Form1 : Form
    {
 

        public Form1()
        {
            InitializeComponent();
             
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            //richTextBox2.Text = Txt.ReadTextFromTxtFile(Properties.Settings.Default.strEd49 + "\\" + e.Start.ToString("dd-MM-yyyy") + ".txt");
            
            tabControl1.SelectedIndex= 2  ;
            
            label3.Text = monthCalendar1.SelectionRange.Start.ToString("dd-MM-yyyy");
            label5.Text = monthCalendar1.SelectionRange.Start.ToString("dd-MM-yyyy");

            button33.Text = "Распределение \nльготных путёвок";
            button34.Text = "Материальные\nактивы";
            button30.Text = "Анкета\nстрахователя";
        }

        private void Form1_Shown(object sender, EventArgs e)//  При открытии основной формы
        {
            
           // splitContainer1.SplitterDistance = 45; // Делаем 1панель сплита 45пикс

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {
            Form2 fr2 = new Form2();
            fr2.Show();//Показываем форму
        }

        

        
        
        
        //---------------------------------------------------------------------------------


        private void button36_Click(object sender, EventArgs e) // При нажатии кнопки "СОХРАНИТЬ"
        {
            richTextBox2.SaveFile(Properties.Settings.Default.strEd49 + "\\" + monthCalendar1.SelectionStart.ToString("dd-MM-yyyy") + ".rtf", RichTextBoxStreamType.PlainText);
            Txt.SaveTextInTxtFile(Properties.Settings.Default.strEd49 + "\\" + monthCalendar1.SelectionStart.ToString("dd-MM-yyyy") + ".txt", richTextBox2.Text);

        }

        private void button37_Click(object sender, EventArgs e)
        {
            richTextBox3.SaveFile(Properties.Settings.Default.strEd49 + "\\" + monthCalendar1.SelectionStart.ToString("dd-MM-yyyy") + ".rtf", RichTextBoxStreamType.PlainText);
            Txt.SaveTextInTxtFile(Properties.Settings.Default.strEd49 + "\\" + monthCalendar1.SelectionStart.ToString("dd-MM-yyyy") + ".txt", richTextBox2.Text);

        }


          //--------------------------------------------------------------------------------------
        public class Txt
        {
            
            public static String ReadTextFromTxtFile(String Path)
            {
                StreamReader streamReader = new StreamReader(Path);
                String textFromFile = streamReader.ReadToEnd();
                streamReader.Close();
                return textFromFile;
            }
            public static void SaveTextInTxtFile(String Path, String TextForWrite)
            {
                StreamWriter streamWriter = new StreamWriter(Path);
                UTF8Encoding encoding = new UTF8Encoding();
                Byte[] encodedText = encoding.GetBytes(TextForWrite);

                String textForWrite = encoding.GetString(encodedText);
                streamWriter.Write(textForWrite);
                streamWriter.Close();
            } 
        }
        //----------------------------------------------------------------------------------------------------------------
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)// При выборе любой даты на календаре
        {
            label3.Text = e.Start.ToString("dd-MM-yyyy");
            label5.Text = e.Start.ToString("dd-MM-yyyy");

            richTextBox2.Clear();

            if (System.IO.File.Exists(Properties.Settings.Default.strEd49 + "\\" + e.Start.ToString("dd-MM-yyyy") + ".txt"      )    )
            {

                richTextBox2.Text = Txt.ReadTextFromTxtFile(Properties.Settings.Default.strEd49 + "\\" + e.Start.ToString("dd-MM-yyyy") + ".txt"    ); 
                
               //  richTextBox2.LoadFile(Properties.Settings.Default.strEd49 + "\\111.txt");
            }
             
            
        }//---------------------------------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)//----------------------------------------- Делопроизводство
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd1 ); 
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd1);
        }
        //------------------------------------------------------------------------------------------------------------------
        private void button10_Click(object sender, EventArgs e)//---------------------------------------------Бухгалтерия
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd2 );
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd2);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void button35_Click(object sender, EventArgs e)//---------------------------------------------Зарплата
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd3);
        }
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd3);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void button22_Click(object sender, EventArgs e)//-------------------------------------------Сводная БухОтчетность
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd4);
        }
        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd4);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void button39_Click(object sender, EventArgs e)//-------------------------------------------Материальные средства
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd5);
        }
        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd5);
        }
        //-------------------------------------------------------------------------------------------------------------------
        private void button34_Click(object sender, EventArgs e)//----------------------------------------------Материальные активы
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd6);
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.strEd6);
        }
        //-------------------------------------------------------------------------------------------------------------------















        private void button8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Обеспечение протезами\\Limb.exe");   // Обеспечение протезами
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Регистратор\\arm_reg.exe");   //Регистратор
        }

        private void button15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Бюджет\\Budget.exe");   //Бюджет
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Ревизор\\Rcheck.exe");   //Ревизор
        }

        

        

        private void button20_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Камеральные проверки\\kcheck.exe");//Камеральные проверки
        }

        private void button45_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Администратор\\Admin.exe");// Администратор
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Калькулятор\\mcalc.exe");//Калькулятор
        }

        private void button25_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Отдел кадров\\Staff.exe");//Отдел кадров
        }

        private void button24_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Камеральные_проверки_для_ПИЛОТА\\kcheck.exe");//Камеральные_проверки_для_ПИЛОТА
        }

        private void button43_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Регистратор_для_ПИЛОТА\\arm_reg.exe");//Регистратор_для_ПИЛОТА
        }

        private void button9_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Путевки\\health.exe");//Путевки
        }

        private void button19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Приём заявок на протезирование\\mblimb.exe");//Заявки на протезирование
        }

        

        

        private void button30_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Анкета страхователя\\InsAnk.exe");//Анкета страхователя
        }

        private void button29_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Камеральные проверки\\kcheck.exe");//Камеральные проверки
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Возмещение вреда\\mispay.exe");//Возмещение вреда
        }

        private void button12_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Реестр листков нетрудоспособности\\Reestr.exe");//Реестр листков нетрудоспособности
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Учет обеспечения бланками\\rglist.exe");//Учет обеспечения бланками
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Форма 4\\Arm_F4.exe");// Ф4
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Форма 6\\Arm_f6.exe");// Ф6
        }

        private void button42_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Учет СВТ\\Hard.exe");// Учет СВТ
        }

        private void button16_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\ШТАТНОЕ РАСПИСАНИЕ\\mantable.exe");// ШТАТНОЕ РАСПИСАНИЕ
        }

        private void button40_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\RapRep\\RapRep1.exe");// Быстрые отчеты
        }

        private void button32_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Справочник телефонов\\phones.exe");// Справочник телефонов
        }

        private void button28_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Профилактика\\prophyl.exe");// Профилактика
        }

        private void button13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Обмен ЦБ\\arm_cb.exe");//Обмен ЦБ
        }

        private void button18_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Оздоровление детей\\hkids.exe");// Оздоровление детей
        }

        private void button23_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("\\\\dbms2\\ЕИИС\\FILIAL\\Отчеты Ф1-НС\\f1ns_reg.exe");// Отчеты Ф1-НС
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Form2 form = new Form2();
            ////form.Show();//в обычном режиме
            //form.ShowDialog();//в модальном режиме  
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
           if (e.TabPageIndex == 1)  
                webBrowser1.Navigate(new Uri("http://r27.fss.ru"));
            
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string strPath =  Directory.GetFiles(@"C:\folder", "*.txt").ToString() ;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_SplitterMoved_1(object sender, SplitterEventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {

        }

        private void button42_Click_1(object sender, EventArgs e)
        {

        }

        private void button43_Click_1(object sender, EventArgs e)
        {

        }

        private void button38_Click_1(object sender, EventArgs e)
        {

        }

        private void button39_Click_1(object sender, EventArgs e)
        {

        }

        private void button40_Click_1(object sender, EventArgs e)
        {

        }

        private void button32_Click_1(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {

        }

        private void button34_Click_1(object sender, EventArgs e)
        {

        }

       

        private void button27_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click_1(object sender, EventArgs e)
        {

        }

        private void button30_Click_1(object sender, EventArgs e)
        {

        }

        

        private void button23_Click_1(object sender, EventArgs e)
        {

        }

        private void button24_Click_1(object sender, EventArgs e)
        {

        }

        private void button25_Click_1(object sender, EventArgs e)
        {

        }

        private void button16_Click_1(object sender, EventArgs e)
        {

        }

        private void button18_Click_1(object sender, EventArgs e)
        {

        }

        private void button19_Click_1(object sender, EventArgs e)
        {

        }

        private void button20_Click_1(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click_1(object sender, EventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {

        }

        private void button15_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void button10_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
         
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

       
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {

        }

       

        private void toolStripButton9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton27_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton29_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(splitContainer1.SplitterDistance == 200)
                splitContainer1.SplitterDistance = 42;
            else
                splitContainer1.SplitterDistance = 200;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MaximumSizeChanged(object sender, EventArgs e)
        {
             
        }

        private void Form1_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                 splitContainer1.SplitterDistance = 42;  
        }

       
        
        private void открытьToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(openFileDialog1.FileName);
                MessageBox.Show(sr.ReadToEnd());
                sr.Close();
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button40_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {

        }

        

        
        






    }
}
