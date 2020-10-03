using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
     
    public partial class WinПринт : Window
    {

        //ObservableCollection<Принтер> prn = new ObservableCollection<Принтер>();

        public WinПринт()
        {
            InitializeComponent();

            var sP = File.ReadAllText("Принтеры.json");
            var prints = JsonConvert.DeserializeObject<List<Принтер>>(sP);
            dg1.ItemsSource = prints;
            dg1.Items.Refresh();
        }
 
        private void but4_Click(object sender, RoutedEventArgs e) // нажали кн. "Сохранить"
        {
            var prns = dg1.ItemsSource;
            var sP = JsonConvert.SerializeObject(prns);
            File.WriteAllText("Принтеры.json", sP);

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
