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

namespace ЗАГС_ЕИИС
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            textBoxFilial.Text = Properties.Settings.Default.КодФилиала.ToString();
            textBox2.Text = Properties.Settings.Default.НаимОрг.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.КодФилиала = textBoxFilial.Text;
            Properties.Settings.Default.НаимОрг    = textBox2.Text;
            Properties.Settings.Default.Save();
        }
    }
}
