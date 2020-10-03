using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace КонверторLDIF2CSV
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

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Файлы LDIF (*.ldif)|*.ldif";
            dialog.FilterIndex = 2;

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                txtBox1.Text = filename;
                ParsLDIF();
            }
        }

        private void ParsLDIF()
        {
            int counter = 1;
            string line;

            DataTable table = new DataTable();
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "№";
            table.Columns.Add(column);

            for (int i = 1; i < 12; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "k" + i.ToString();
                table.Columns.Add(column);
            }

            string[] NameLDIF = txtBox1.Text.Split('\\');
            
            FileStream fs = new FileStream(txtBox1.Text + ".csv", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fs);

            int j = -1;
            StreamReader file = new System.IO.StreamReader(txtBox1.Text);
            DataRow row = null;
            while ((line = file.ReadLine()) != null)
            {
                j++;
                if (line != "")
                {
                    var pars = line.Split(':');
                    if (j == 0)
                    {
                        row = table.NewRow();
                        row[0] = counter.ToString();
                        row[1] = (pars[1] != "") ? pars[1] : pars[2];
                        streamWriter.Write((pars[1] != "") ? pars[1] + ";" : pars[2] + ";");
                    }
                    else
                    {
                        row[j + 1] = (pars[1] != "") ? pars[1] : pars[2];
                        streamWriter.Write((pars[1] != "") ? pars[1] + ";" : pars[2] + ";");
                    }
                }
                else
                {
                    table.Rows.Add(row);
                    j = -1;
                    counter++;
                    streamWriter.WriteLine();
                }
            }

            streamWriter.Close();
            fs.Close();

            DataGrid1.ItemsSource = table.DefaultView;

            MessageBox.Show("Файл   \"" + NameLDIF[NameLDIF.Length - 1] + "\"  конвертирован в \""+ NameLDIF[NameLDIF.Length - 1] + ".csv\"  и сохранен");
        }



    }
}
