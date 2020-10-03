using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Shapes;

namespace ЗАГС_ЕИИС
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            DataTable DT = new DataTable();
            DT.Columns.Add("DSN");
            DT.Columns.Add("IP");

            InitializeComponent();

            textBox4.Text = Properties.Settings.Default.Источн1.ToString();
            textBox2.Text = Properties.Settings.Default.СерверБД1.ToString();
            textBox3.Text = Properties.Settings.Default.Логин1.ToString();
            textBox1.Text = Properties.Settings.Default.Пароль1.ToString();

            textBox21.Text = Properties.Settings.Default.Источн2.ToString();
            textBox22.Text = Properties.Settings.Default.СерверБД2.ToString();
            textBox23.Text = Properties.Settings.Default.Логин2.ToString();
            textBox24.Text = Properties.Settings.Default.Пароль2.ToString();

            radioButton1.IsChecked = Properties.Settings.Default.ОдинИсточн ;
            radioButton2.IsChecked = Properties.Settings.Default.ДваИсточн;


            RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\ODBC\\ODBC.INI");

            string[] subkeyName = key.GetSubKeyNames();
             
            foreach (string strValue in subkeyName)
            {
                if (strValue != "ODBC Data Sources")
                {
                    DataRow  workRow = DT.NewRow();
                    workRow[0] = strValue;
                    RegistryKey Key2 = key.OpenSubKey(strValue);
                    string SRV = Key2.GetValue("SRV").ToString();
                    workRow[1] = SRV;
                    DT.Rows.Add(workRow);
                }
            }
            
            dataGrid1.ItemsSource = DT.DefaultView;
             
        }

        private void window2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Источн1 = textBox4.Text;
            Properties.Settings.Default.СерверБД1 = textBox2.Text;
            Properties.Settings.Default.Логин1 = textBox3.Text;
            Properties.Settings.Default.Пароль1 = textBox1.Text;

            Properties.Settings.Default.Источн2 = textBox21.Text;
            Properties.Settings.Default.СерверБД2 = textBox22.Text;
            Properties.Settings.Default.Логин2 = textBox23.Text;
            Properties.Settings.Default.Пароль2 = textBox24.Text;

            Properties.Settings.Default.ОдинИсточн = radioButton1.IsChecked.Value ;
            Properties.Settings.Default.ДваИсточн  = radioButton2.IsChecked.Value ;

           

            Properties.Settings.Default.Save();
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible ;
            grid3.Visibility = Visibility.Hidden ;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            grid2.Visibility = Visibility.Visible ;
            grid3.Visibility = Visibility.Visible ;
        }

         
        private void button2_Click(object sender, RoutedEventArgs e)// кн. "Установить драйвер ODBC HyTech"
        {
            // Process proc = new  Process();
            //proc.StartInfo.FileName =   ".\\HyTechODBC\\odbcinst.exe";
            //proc.StartInfo.WorkingDirectory =   ".\\HyTechODBC\\";
            //proc.Start();
            //proc.WaitForExit();
             
            ProcessStartInfo startInfo = new ProcessStartInfo();
            // startInfo.CreateNoWindow = false;
            //startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Directory.GetCurrentDirectory() + "\\HyTechODBC\\";
            startInfo.FileName         = Directory.GetCurrentDirectory() + "\\HyTechODBC\\odbcinst.exe";
            startInfo.WindowStyle      = ProcessWindowStyle.Hidden;

            Process.Start(startInfo);
             

            //Process pR = new Process();
            //pR.StartInfo.WorkingDirectory = ".\\HyTechODBC\\";
            //pR.Start(".\\HyTechODBC\\odbcinst.exe");
        }

        private void button3_Click(object sender, RoutedEventArgs e) // кн. "Создать источник данных ODBC"
        {
            Process.Start(".\\HyTechODBC\\odbcad32.exe");
        }

        private void button41_Click(object sender, RoutedEventArgs e)// кн. "Закрыть"
        {
            window2.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e) // кн. "СОХРАНИТЬ"
        {
            if (!File.Exists(@"C:\Windows\system32\htodbc.dll"))
                File.Copy("htodbc.dll", @"C:\Windows\system32\htodbc.dll");


            if (radioButton1.IsChecked == true )
            {
                string[] fileLines = File.ReadAllLines("HyTech.reg");

                fileLines[28] = "\"" + textBox4.Text.ToString() + "\" = \"HyTech SQL Server ODBC Driver\"";
                fileLines[30] = @"[HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ODBC\ODBC.INI\" + textBox4.Text.ToString() + "]";
                fileLines[38] = "\"SRV\" = \"tcpip:/" + textBox2.Text.ToString() + "\"";
                fileLines[37] = "\"UID\" = \"" + textBox3.Text.ToString() + "\"";
                fileLines[36] = "\"PWD\" = \"" + textBox1.Text.ToString() + "\"";
                fileLines[35] = "\"SN\" = \"tcpip:/" + textBox2.Text.ToString() + "\"";
                File.WriteAllLines("HyTech.reg", fileLines);

                Process regeditProcess = Process.Start("HyTech.reg", "/S /q");
                regeditProcess.WaitForExit();
            }

            if (radioButton2.IsChecked == true)
            {
                string[] fileLines = File.ReadAllLines("HyTech.reg");

                fileLines[28] = "\"" + textBox4.Text.ToString() + "\" = \"HyTech SQL Server ODBC Driver\"";
                fileLines[30] = @"[HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ODBC\ODBC.INI\" + textBox4.Text.ToString() + "]";
                fileLines[38] = "\"SRV\" = \"tcpip:/" + textBox2.Text.ToString() + "\"";
                fileLines[37] = "\"UID\" = \"" + textBox3.Text.ToString() + "\"";
                fileLines[36] = "\"PWD\" = \"" + textBox1.Text.ToString() + "\"";
                fileLines[35] = "\"SN\" = \"tcpip:/" + textBox2.Text.ToString() + "\"";
                File.WriteAllLines("HyTech.reg", fileLines);
                Process regeditProcess1 = Process.Start("HyTech.reg", "/S /q");
                regeditProcess1.WaitForExit();

                fileLines[28] = "\"" + textBox21.Text.ToString() + "\" = \"HyTech SQL Server ODBC Driver\"";
                fileLines[30] = @"[HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\ODBC\ODBC.INI\" + textBox21.Text.ToString() + "]";
                fileLines[38] = "\"SRV\" = \"tcpip:/" + textBox22.Text.ToString() + "\"";
                fileLines[37] = "\"UID\" = \"" + textBox23.Text.ToString() + "\"";
                fileLines[36] = "\"PWD\" = \"" + textBox24.Text.ToString() + "\"";
                fileLines[35] = "\"SN\" = \"tcpip:/" + textBox22.Text.ToString() + "\"";
                File.WriteAllLines("HyTech.reg", fileLines);
                Process regeditProcess2 = Process.Start("HyTech.reg", "/S /q");
                regeditProcess2.WaitForExit();

            }

        }
    }
}
