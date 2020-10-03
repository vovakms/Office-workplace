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
using System.Windows.Shapes;

namespace Chat
{
    
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            Top = (screenHeight - this.Height); //  / 0x00000002
            Left = (screenWidth - this.Width); // / 0x00000002
        }

        private void button2_Click(object sender, RoutedEventArgs e) // сохранить
        {
            Properties.Settings.Default.IPserver = textBox1.Text.ToString();
            Properties.Settings.Default.Save();

            //Close(); // закрываем форму
            Hide();
        }

        private void button1_Click(object sender, RoutedEventArgs e)// закрыть 
        {
            Environment.Exit(0);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
