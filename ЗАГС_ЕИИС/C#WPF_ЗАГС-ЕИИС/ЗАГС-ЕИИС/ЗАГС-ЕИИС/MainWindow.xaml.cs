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

using System.Windows.Forms;
//using System.IO; 

namespace ЗАГС_ЕИИС
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)// Открыть выбранные файлы
        {
            OpenFileDialog fD = new OpenFileDialog();
             
            fD.Multiselect = true;
            fD.Filter = "Файлы Ексель|*.xls*";
            if (fD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = System.IO.Path.GetDirectoryName(fD.FileName);  
                spisFile(fD);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)// Свернуть
        {
            Window1.WindowState = WindowState.Minimized ;
        }

        private void button3_Click(object sender, RoutedEventArgs e)// Востановить
        {
            if (Window1.WindowState == WindowState.Normal)
                Window1.WindowState = WindowState.Maximized;
            else
                Window1.WindowState = WindowState.Normal;
        }

        private void menu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Window1.WindowState == WindowState.Normal)
                Window1.WindowState = WindowState.Maximized;
            else
                Window1.WindowState = WindowState.Normal;
        }

        private void button4_Click(object sender, RoutedEventArgs e)// Закрыть
        {
            Window1.Close();
        }

        private void menu_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e) // перемещение формы взяв за меню
        {
            Window1.DragMove() ;
        }

        //-------------------------------------------------------
        private void spisFile(OpenFileDialog sF )
        {
            int i = 1;

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("n-n");
            dt.Columns.Add("Наименование файла");
            dt.Columns.Add("Кол-во строк");
            foreach (string p in sF.FileNames)
            {
                dt.Rows.Add(i++, System.IO.Path.GetFileName(p), " ");
                fileEx(p.ToString());
            }
            dataGrid1.ItemsSource = dt.DefaultView;
        }

        private int fileEx(string fP)
        {
            int kS = 0;

            textBox2.AppendText(fP + "\n" );

            return kS;
        }

       
    }
}
