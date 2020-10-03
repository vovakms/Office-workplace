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

namespace Агрегатор
{
    class ViewModel : INotifyPropertyChanged
    {
         
        public ObservableCollection<string> ColPrint { get; set; }
        

        private string selectedItem { get; set; }

        public string SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyPropertyChanged("SelectedItem");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModel()
        {
            ColPrint = new ObservableCollection<string>();
            
            var sP = File.ReadAllText("Принтеры.json");
            var lP = JsonConvert.DeserializeObject<List<Принтер>>(sP);
            for (int i = 0; i < lP.Count; i++)
                ColPrint.Add(lP[i].IPadrPrint.ToString());
 
        }
    }

}
