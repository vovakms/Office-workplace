using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace ВыпискиМНС
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri(Directory.GetCurrentDirectory() + "\\HyTechODBC.html"));
            webBrowser2.Navigate(new Uri(Directory.GetCurrentDirectory() + "\\readme.html"));  // 
        }

        private void button1_Click(object sender, EventArgs e) // ------------нажали кнопку "СОХРАНИТЬ"
        {
            Properties.Settings.Default.NameDSN = textBox4.Text;
            Properties.Settings.Default.ServPort = textBox1.Text ;
            Properties.Settings.Default.Login = textBox2.Text;
            Properties.Settings.Default.Password = textBox3.Text;
            Properties.Settings.Default.Save();

            Close();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        



    }
}


 //XmlTextReader reader = new XmlTextReader(openFileDialog1.FileName.ToString());
 //           while (reader.Read())
 //           {
 //               switch (reader.NodeType)
 //               {
 //                   case XmlNodeType.Element: // Узел является элементом.
 //                       if (reader.Name == "СвЮЛ" || reader.Name == "СвОКВЭД")
 //                       {
 //                           textBox2.AppendText("<" + reader.Name);
 //                           if (reader.AttributeCount > 3) reader.MoveToAttribute(3);

 //                           //while (reader.MoveToNextAttribute()) // Чтение атрибутов. listBox1.Items[0]
 //                           //   textBox2.AppendText(" " + reader.Name + "='" + reader.Value + "'\r\n");
 //                           textBox2.AppendText(" " + reader.Name + "='" + reader.Value + "'");
 //                           textBox2.AppendText(">\r\n");
 //                       }
 //                       break;
 //                   case XmlNodeType.Text: // Вывести текст в каждом элементе.
 //                       textBox2.AppendText(reader.Value);
 //                       break;
 //                   case XmlNodeType.EndElement: // Вывести конец элемента.
 //                       if (reader.Name == "СвЮЛ" || reader.Name == "СвОКВЭД")
 //                       {
 //                           textBox2.AppendText("</" + reader.Name);
 //                           textBox2.AppendText(">\r\n");
 //                       }
 //                       break;
 //               }


 //           }