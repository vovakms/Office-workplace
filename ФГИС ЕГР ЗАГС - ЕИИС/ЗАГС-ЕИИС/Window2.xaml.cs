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
            textBoxFilial3.Text = Properties.Settings.Default.КодФилиала.ToString();
            textBoxFilial4.Text = Properties.Settings.Default.КодФилиала2.ToString();
            textBoxFilial.Text = Properties.Settings.Default.КодФилиала3.ToString();
            textBoxFilial5.Text = Properties.Settings.Default.КодФилиала4.ToString();
            textBoxFilial6.Text = Properties.Settings.Default.КодФилиала5.ToString();

            textBox2.Text = Properties.Settings.Default.НаимОрг.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.КодФилиала = textBoxFilial3.Text;
            Properties.Settings.Default.КодФилиала2 = textBoxFilial4.Text;
            Properties.Settings.Default.КодФилиала3 = textBoxFilial.Text;
            Properties.Settings.Default.КодФилиала4 = textBoxFilial5.Text;
            Properties.Settings.Default.КодФилиала5 = textBoxFilial6.Text;

            Properties.Settings.Default.НаимОрг    = textBox2.Text;
            Properties.Settings.Default.Save();
        }
    }
}
