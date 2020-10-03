using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Подсистемы_ЕИИС
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

         private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.strEd1;
            textBox2.Text = Properties.Settings.Default.strEd2;
            textBox3.Text = Properties.Settings.Default.strEd3;
            textBox4.Text = Properties.Settings.Default.strEd4;
            textBox5.Text = Properties.Settings.Default.strEd5;
            textBox6.Text = Properties.Settings.Default.strEd6;
            textBox7.Text = Properties.Settings.Default.strEd7;
            textBox8.Text = Properties.Settings.Default.strEd8;
            textBox9.Text = Properties.Settings.Default.strEd9;
            textBox10.Text = Properties.Settings.Default.strEd10;
            textBox11.Text = Properties.Settings.Default.strEd11;
            textBox12.Text = Properties.Settings.Default.strEd12;
            textBox13.Text = Properties.Settings.Default.strEd13;
            textBox14.Text = Properties.Settings.Default.strEd14;
            textBox15.Text = Properties.Settings.Default.strEd15;
            textBox16.Text = Properties.Settings.Default.strEd16;
            textBox17.Text = Properties.Settings.Default.strEd17;
            textBox18.Text = Properties.Settings.Default.strEd18;
            textBox19.Text = Properties.Settings.Default.strEd19;
            textBox20.Text = Properties.Settings.Default.strEd20;
            textBox21.Text = Properties.Settings.Default.strEd21;
            textBox22.Text = Properties.Settings.Default.strEd22;
            textBox23.Text = Properties.Settings.Default.strEd23;
            textBox24.Text = Properties.Settings.Default.strEd24;
            textBox25.Text = Properties.Settings.Default.strEd25;
            textBox26.Text = Properties.Settings.Default.strEd26;
            textBox27.Text = Properties.Settings.Default.strEd27;
            textBox28.Text = Properties.Settings.Default.strEd28;
            textBox29.Text = Properties.Settings.Default.strEd29;
            textBox30.Text = Properties.Settings.Default.strEd30;
            textBox31.Text = Properties.Settings.Default.strEd31;
            textBox32.Text = Properties.Settings.Default.strEd32;
            textBox33.Text = Properties.Settings.Default.strEd33;
            textBox34.Text = Properties.Settings.Default.strEd34;
            textBox35.Text = Properties.Settings.Default.strEd35;
            textBox36.Text = Properties.Settings.Default.strEd36;
            textBox37.Text = Properties.Settings.Default.strEd37;
            textBox38.Text = Properties.Settings.Default.strEd38;
            textBox39.Text = Properties.Settings.Default.strEd39;
            textBox40.Text = Properties.Settings.Default.strEd40;
            textBox41.Text = Properties.Settings.Default.strEd41;
            textBox42.Text = Properties.Settings.Default.strEd42;
            textBox43.Text = Properties.Settings.Default.strEd43;
            textBox44.Text = Properties.Settings.Default.strEd44;
            textBox45.Text = Properties.Settings.Default.strEd45;
            textBox46.Text = Properties.Settings.Default.strEd46;
            textBox47.Text = Properties.Settings.Default.strEd47;
            textBox48.Text = Properties.Settings.Default.strEd48;
            textBox49.Text = Properties.Settings.Default.strEd49;
            textBox50.Text = Properties.Settings.Default.strEd50;
            textBox51.Text = Properties.Settings.Default.strEd51;
            textBox52.Text = Properties.Settings.Default.strEd52;
             
 
        }
         
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.strEd1 = textBox1.Text;
            Properties.Settings.Default.strEd2 = textBox2.Text;
            Properties.Settings.Default.strEd3 = textBox3.Text;
            Properties.Settings.Default.strEd4 = textBox4.Text;
            Properties.Settings.Default.strEd5 = textBox5.Text;
            Properties.Settings.Default.strEd6 = textBox6.Text;
            Properties.Settings.Default.strEd7 = textBox7.Text;
            Properties.Settings.Default.strEd8 = textBox8.Text;
            Properties.Settings.Default.strEd9 = textBox9.Text;
            Properties.Settings.Default.strEd10 = textBox10.Text;
            Properties.Settings.Default.strEd11 = textBox11.Text;
            Properties.Settings.Default.strEd12 = textBox12.Text;
            Properties.Settings.Default.strEd13 = textBox13.Text;
            Properties.Settings.Default.strEd14 = textBox14.Text;
            Properties.Settings.Default.strEd15 = textBox15.Text;
            Properties.Settings.Default.strEd16 = textBox16.Text;
            Properties.Settings.Default.strEd17 = textBox17.Text;
            Properties.Settings.Default.strEd18 = textBox18.Text;
            Properties.Settings.Default.strEd19 = textBox19.Text;
            Properties.Settings.Default.strEd20 = textBox20.Text;
            Properties.Settings.Default.strEd21 = textBox21.Text;
            Properties.Settings.Default.strEd22 = textBox22.Text;
            Properties.Settings.Default.strEd23 = textBox23.Text;
            Properties.Settings.Default.strEd24 = textBox24.Text;
            Properties.Settings.Default.strEd25 = textBox25.Text;
            Properties.Settings.Default.strEd26 = textBox26.Text;
            Properties.Settings.Default.strEd27 = textBox27.Text;
            Properties.Settings.Default.strEd28 = textBox28.Text;
            Properties.Settings.Default.strEd29 = textBox29.Text;
            Properties.Settings.Default.strEd30 = textBox30.Text;
            Properties.Settings.Default.strEd31 = textBox31.Text;
            Properties.Settings.Default.strEd32 = textBox32.Text;
            Properties.Settings.Default.strEd33 = textBox33.Text;
            Properties.Settings.Default.strEd34 = textBox34.Text;
            Properties.Settings.Default.strEd35 = textBox35.Text;
            Properties.Settings.Default.strEd36 = textBox36.Text;
            Properties.Settings.Default.strEd37 = textBox37.Text;
            Properties.Settings.Default.strEd38 = textBox38.Text;
            Properties.Settings.Default.strEd39 = textBox39.Text;
            Properties.Settings.Default.strEd40 = textBox40.Text;
            Properties.Settings.Default.strEd41 = textBox41.Text;
            Properties.Settings.Default.strEd42 = textBox42.Text;
            Properties.Settings.Default.strEd43 = textBox43.Text;
            Properties.Settings.Default.strEd44 = textBox44.Text;
            Properties.Settings.Default.strEd45 = textBox45.Text;
            Properties.Settings.Default.strEd46 = textBox46.Text;
            Properties.Settings.Default.strEd47 = textBox47.Text;
            Properties.Settings.Default.strEd48 = textBox48.Text;
            Properties.Settings.Default.strEd49 = textBox49.Text;
            Properties.Settings.Default.strEd50 = textBox50.Text;
            Properties.Settings.Default.strEd51 = textBox51.Text;
            Properties.Settings.Default.strEd52 = textBox52.Text; 
            Properties.Settings.Default.Save();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void textBox49_TextChanged(object sender, EventArgs e)
        {

        }

        

       
        private void button1_Click(object sender, EventArgs e)   //Делопроизводство
        {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //Бухгалтерия
        {
            openFileDialog1.ShowDialog();
            textBox2.Text = openFileDialog1.FileName.ToString();
        }

        private void button3_Click(object sender, EventArgs e) // Зарплата
        {
            openFileDialog1.ShowDialog();
            textBox3.Text = openFileDialog1.FileName.ToString();
        }

        private void button4_Click(object sender, EventArgs e) // Сводная БухОтчетность
        {
            openFileDialog1.ShowDialog();
            textBox4.Text = openFileDialog1.FileName.ToString();
        }

        private void button5_Click(object sender, EventArgs e) // Материальные средства
        {
            openFileDialog1.ShowDialog();
            textBox5.Text = openFileDialog1.FileName.ToString();
        }

        private void button6_Click(object sender, EventArgs e) // Материальные активы
        {
            openFileDialog1.ShowDialog();
            textBox6.Text = openFileDialog1.FileName.ToString();
        }

        private void button7_Click(object sender, EventArgs e) // Реестр листков нетрудоспособности
        {
            openFileDialog1.ShowDialog();
            textBox7.Text = openFileDialog1.FileName.ToString();
        }

        private void button8_Click(object sender, EventArgs e) //Регистратор
        {
            openFileDialog1.ShowDialog();
            textBox8.Text = openFileDialog1.FileName.ToString();
        }

        private void button9_Click(object sender, EventArgs e) // Учет обеспечения бланками
        {
            openFileDialog1.ShowDialog();
            textBox9.Text = openFileDialog1.FileName.ToString();
        }

        private void button10_Click(object sender, EventArgs e) // Профилактика
        {
            openFileDialog1.ShowDialog();
            textBox10.Text = openFileDialog1.FileName.ToString();
        }

        private void button11_Click(object sender, EventArgs e) // Отдел кадров
        {
            openFileDialog1.ShowDialog();
            textBox11.Text = openFileDialog1.FileName.ToString();
        }

        private void button12_Click(object sender, EventArgs e) // Анкета страхователя
        {
            openFileDialog1.ShowDialog();
            textBox12.Text = openFileDialog1.FileName.ToString();
        }

        private void button13_Click(object sender, EventArgs e)// Ф-4
        {
            openFileDialog1.ShowDialog();
            textBox13.Text = openFileDialog1.FileName.ToString();
        }

        private void button14_Click(object sender, EventArgs e) // Ф-6
        {
            openFileDialog1.ShowDialog();
            textBox14.Text = openFileDialog1.FileName.ToString();
        }

        private void button15_Click(object sender, EventArgs e) // Камеральные проверки
        {
            openFileDialog1.ShowDialog();
            textBox15.Text = openFileDialog1.FileName.ToString();
        }

        private void button16_Click(object sender, EventArgs e)// Справочник ОКВЭД-ОКОНХ
        {
            openFileDialog1.ShowDialog();
            textBox16.Text = openFileDialog1.FileName.ToString();
        }

        private void button17_Click(object sender, EventArgs e) // Заявки на протезирование
        {
            openFileDialog1.ShowDialog();
            textBox17.Text = openFileDialog1.FileName.ToString();
        }

        private void button18_Click(object sender, EventArgs e)// Путевки
        {
            openFileDialog1.ShowDialog();
            textBox18.Text = openFileDialog1.FileName.ToString();
        }

        private void button19_Click(object sender, EventArgs e) // Обеспечение протезами
        {
            openFileDialog1.ShowDialog();
            textBox19.Text = openFileDialog1.FileName.ToString();
        }

        private void button20_Click(object sender, EventArgs e) // Возмещение вреда
        {
            openFileDialog1.ShowDialog();
            textBox20.Text = openFileDialog1.FileName.ToString();
        }

        private void button21_Click_1(object sender, EventArgs e) // Распределение льготных путевок
        {
            openFileDialog1.ShowDialog();
            textBox21.Text = openFileDialog1.FileName.ToString();
        }

        private void button22_Click(object sender, EventArgs e) // Отчеты Ф1-НС
        {
            openFileDialog1.ShowDialog();
            textBox22.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button23_Click(object sender, EventArgs e) // Учет СВТ
        {
            openFileDialog1.ShowDialog();
            textBox23.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button24_Click(object sender, EventArgs e) // Ревизор
        {
            openFileDialog1.ShowDialog();
            textBox24.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button25_Click(object sender, EventArgs e) // Обмен ЦБ
        {
            openFileDialog1.ShowDialog();
            textBox25.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button26_Click(object sender, EventArgs e)// Телефонный справочник
        {
            openFileDialog1.ShowDialog();
            textBox26.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button27_Click(object sender, EventArgs e) // Шофер
        {
            openFileDialog1.ShowDialog();
            textBox27.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button28_Click(object sender, EventArgs e)// Оздоровление детей
        {
            openFileDialog1.ShowDialog();
            textBox28.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button29_Click(object sender, EventArgs e) // Штатное расписание
        {
            openFileDialog1.ShowDialog();
            textBox29.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button30_Click(object sender, EventArgs e) // Калькулятор
        {
            openFileDialog1.ShowDialog();
            textBox30.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button39_Click(object sender, EventArgs e) // Бюджет
        {
            openFileDialog1.ShowDialog();
            textBox50.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button40_Click(object sender, EventArgs e) // ПИЛОТ камеральные проверки
        {
            openFileDialog1.ShowDialog();
            textBox51.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button41_Click(object sender, EventArgs e) // ПИЛОТ Регистратор
        {
            openFileDialog1.ShowDialog();
            textBox52.Text = openFileDialog1.FileName.ToString(); 
        }

        private void button37_Click(object sender, EventArgs e) //Каталог с описаниями обновлений
        {
            folderBrowserDialog1.ShowDialog();
            textBox48.Text = folderBrowserDialog1.SelectedPath.ToString();
        }

        private void button38_Click(object sender, EventArgs e) // Каталог ежедневника
        {
            folderBrowserDialog1.ShowDialog();
            textBox49.Text = folderBrowserDialog1.SelectedPath.ToString();
        }
        

       
        
    }
}
