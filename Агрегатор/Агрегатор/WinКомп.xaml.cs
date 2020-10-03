using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

namespace Агрегатор
{
     
    public partial class WinКомп : Window
    {
        public WinКомп()
        {
            InitializeComponent();

            var sC = File.ReadAllText("Компьютеры.json");
            var comps = JsonConvert.DeserializeObject<List<Компьютер>>(sC);
            dg1.ItemsSource = comps;
            dg1.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e) // сканировать диапазон
        {

        }
         
        private void but1_Click(object sender, RoutedEventArgs e) // кн. "Сохранить"
        {
            var comps = dg1.ItemsSource;
            var sС = JsonConvert.SerializeObject(comps);
            File.WriteAllText("Компьютеры.json", sС);

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }

}
