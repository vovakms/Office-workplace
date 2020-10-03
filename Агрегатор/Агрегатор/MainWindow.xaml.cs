using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
 
namespace Агрегатор
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            

            if (File.Exists("Пользователи.json") == false)
            {
                StreamWriter sw = new StreamWriter("Пользователи.json");
                sw.WriteLine(@"[{""Имя пользователя"":""Вася"",""Подразделение"":""Бухгалтерия"",""Примечание"":""В отпуске с 01-01-2001""}]");
                sw.Close();
            }

            if (File.Exists("Компьютеры.json") == false)
            {
                StreamWriter sw = new StreamWriter("Компьютеры.json");
                sw.WriteLine(@"[{""IPadrComp"":""192.168.1.1"",""NameComp"":"""",""ModelComp"":"""",""NoteComp"":""""}]");
                sw.Close();
            }

            if (File.Exists("Принтеры.json") == false)
            {
                StreamWriter sw = new StreamWriter("Принтеры.json");
                sw.WriteLine(@"[{""IPadrPrint"":""192.168.1.1"",""NamePrint"":"""",""ModelPrint"":"""",""NotePrint"":""""}]");
                sw.Close();
            }

            if (File.Exists("Сервера.json") == false)
            {
                StreamWriter sw = new StreamWriter("Сервера.json");
                sw.WriteLine(@"[{""IPadrServ"":""192.168.1.1"",""NameServ"":"""",""ModelServ"":"""",""NoteServ"":""""}]");
                sw.Close();
            }

            if (File.Exists("Телефоны.json") == false)
            {
                StreamWriter sw = new StreamWriter("Телефоны.json");
                sw.WriteLine(@"[{""IPadrTel"":""192.168.1.1"",""NameTel"":"""",""ModelTel"":"""",""NoteTel"":""""}]");
                sw.Close();
            }

            if (File.Exists("Видео.json") == false)
            {
                StreamWriter sw = new StreamWriter("Видео.json");
                sw.WriteLine(@"[{""IPadrVideo"":""192.168.1.1"",""NameVideo"":"""",""ModelVideo"":"""",""NoteVideo"":""""}]");
                sw.Close();
            }
             
            InitializeComponent();

            //var sP = File.ReadAllText("Принтеры.json");
            //var prints = JsonConvert.DeserializeObject<List<Принтер>>(sP);
            //lb1.ItemsSource = prints;
            //lb1.Items.Refresh();

            var sC = File.ReadAllText("Компьютеры.json");
            var comps = JsonConvert.DeserializeObject<List<Компьютер>>(sC);
            listComp.ItemsSource = comps;
            listComp.Items.Refresh();
        }

         
         

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) // Открыть Видеонаблюдение
        {
            WinВидео win = new WinВидео();
            win.Show();
        }
         
         

        private void mi1_Click(object sender, RoutedEventArgs e) // меню "Пользоатели"
        {
            WinПольз win = new WinПольз();
            win.Show();
        }

        private void mi2_Click(object sender, RoutedEventArgs e) // меню "Компьютеры"
        {
            WinКомп win = new WinКомп();
            win.Show();
        }
       
        private void mi3_Click(object sender, RoutedEventArgs e) // меню "Принтеры"
        {
            WinПринт win = new WinПринт();
            win.Show();
        }

        private void mi4_Click(object sender, RoutedEventArgs e) // меню "Сервера"
        {
            WinСерв win = new WinСерв();
            win.Show();
        }

        private void mi5_Click(object sender, RoutedEventArgs e) // меню "Телефоны"
        {
            WinТел win = new WinТел();
            win.Show();
        }

        private void mi6_Click(object sender, RoutedEventArgs e) // меню "Видео"
        {
            WinВидео win = new WinВидео();
            win.Show();
        }

        private void mi41_Click(object sender, RoutedEventArgs e) // меню "О программе"
        {
            WinОПрог win = new WinОПрог();
            win.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new  ViewModel();
        }

        private void mi11_Click(object sender, RoutedEventArgs e) // меню "Открыть"
        {
            Window1 win = new Window1();
            win.Show();
        }

        private void lb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Uri strU = new Uri("http://" + lb2.Content.ToString()); 
            wb1.Source = strU;
        }
         
        private void mi21_Click(object sender, RoutedEventArgs e)
        {
            Window2 win = new Window2();
            win.Show();
        }
    }


     

}
