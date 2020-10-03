using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat
{
 
    public partial class MainWindow : Window
    {
        private  NotifyIcon _notifyIcon = null ;

        Window1 win1 = new Window1();

        public ObservableCollection<UsersChat> Images { get; set; } = new ObservableCollection<UsersChat>();

        delegate void ChatEvent(string content);
        private ChatEvent AddMes;
        private Socket ServerSocket;
        private Thread listenThread;
        private string ServerIP = "127.0.0.1";
        private int ServerPort = 2222;

        string CompName = Environment.MachineName;
        string nickName = "";
        
        public MainWindow()
        {
            InitializeComponent();

            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            Height = screenHeight; 
             
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            Top = (screenHeight - this.Height); //  / 0x00000002
            Left = (screenWidth - this.Width); // / 0x00000002

            AddMes = new ChatEvent(AddMessage);
            nickName = Environment.UserName + " " + CompName + " " + DateTime.Now.TimeOfDay.ToString("mmss");//  дописка к логину для тестирования чтобы можно было на одной тачке запускать много экземляров
           // ServerIP = TextBox2.Text.ToString();
        }
         
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IPAddress temp = IPAddress.Parse(ServerIP); // преобразовываем IPадресс из строкового вида
            ServerSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);//новый сокет
            ServerSocket.Connect(new IPEndPoint(temp, ServerPort)); // подключаемся
            if (ServerSocket.Connected) // если подключение 
            {
                RichTextBox1.Document.Blocks.Add(new Paragraph(new Run(DateTime.Now.ToLongTimeString() + " Подключились. "  )));
                listenThread = new Thread(listner); // создаем новый поток 
                listenThread.IsBackground = true;  // в фоновом режиме
                listenThread.Start();             // запускаем поток
                Send($"#setname|{nickName}");    //отправляем на сервер свой логин
            }

            Rol2Tray(); // сворачиваемся в трей
        }

        private void TextBox1_KeyUp(object sender, System.Windows.Input.KeyEventArgs e) // нажали клавишу на клавиатуре
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(TextBox1.Text))//Если нажали Enter и строка не пустая
            {
                Send($"#message|{TextBox1.Text}");// отсылаем строку на сервер
                TextBox1.Text = string.Empty; // очищаем поле ввода сообщения
            }
        }
         
        private void AddMessage(string Content) // Вывод сообщения
        {
            if (!Dispatcher.CheckAccess()){Dispatcher.Invoke(new ChatEvent(AddMes), Content); return;}
            
            RichTextBox1.Document.Blocks.InsertBefore(RichTextBox1.Document.Blocks.FirstBlock,
                          new Paragraph(new Run(DateTime.Now.ToLongTimeString() + " " + Content)));
        }
        public void Send(byte[] buffer)
        {
            try { ServerSocket.Send(buffer); } catch { }
        }
        public void Send(string Buffer)
        {
            try { ServerSocket.Send(Encoding.Unicode.GetBytes(Buffer)); } catch { }
        }
        public void listner()
        {
            try
            {
                while (ServerSocket.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = ServerSocket.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch {  RichTextBox1.AppendText("\n Связь с сервером прервана");            }
        }
        public void handleCommand(string cmd)
        {
            string[] commands = cmd.Split('#');     // разбираем строку 
            int countCommands = commands.Length;    // количество 
            for (int i = 0; i < countCommands; i++) // перебираем 
            {
                try
                {
                    string currentCommand = commands[i];
                    if (currentCommand.Contains("msg")) { string[] Arguments = currentCommand.Split('|'); AddMessage(Arguments[1]); continue; }
                    
                    if (currentCommand.Contains("userlist"))
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            string[] Users = currentCommand.Split('|')[1].Split(',');// получаем массив логинов пользователей
                            Images.Clear();// очищаем колекцию
                            for (int j = 0; j < Users.Length; j++)//  перебираем массив пользователей
                            {
                                string pathAva = Environment.CurrentDirectory.ToString() + @"\img4.png";// формируем путь к своей аватарке
                                if (!string.IsNullOrEmpty(Users[j]))
                                    Images.Add(new UsersChat(Users[j], pathAva, CompName));
                            }
                            ListView1.ItemsSource = Images; // коллекцию в ЛистView
                        }));
                    }

                }
                catch (Exception exp) { RichTextBox1.AppendText("\n ошибка: " + exp.Message); }
            }
        }
         
        private void Rol2Tray() //   метод создания иконки в трее и ее отображение 
        {
            if (  _notifyIcon == null) {
                _notifyIcon = new NotifyIcon();
                _notifyIcon.Icon = Properties.Resources.ResourceManager.GetObject("icTr1") as Icon;

                _notifyIcon.Visible = true;
            }
            _notifyIcon.MouseClick += (sndr, args) => //  клик по иконке в трее
            {
                if ( args.Button == MouseButtons.Left ) {  
                     Show(); // показать
                     WindowState = WindowState.Normal; // 
                }
                if (args.Button == MouseButtons.Right)// правой клик
                {
                    if (win1 == null)
                        win1 = new Window1();
 
                    win1.Show();
                }
            };
            Hide(); // спрятать окно
        }

        private void Window_Closed(object sender, EventArgs e) // при закрытии окна
        {
            if (_notifyIcon != null) // проверяем 
            {
                _notifyIcon.Dispose(); // освобождаем 
            }
        }

        //private void button5_Click(object sender, RoutedEventArgs e) // показываем панель настройки IP-сервера
        //{
        //    if (ListView1.Visibility == Visibility.Visible)
        //    {
        //        ListView1.Visibility = Visibility.Hidden;
        //        Label1.Visibility = Visibility.Visible;
        //        //TextBox2.Visibility = Visibility.Visible;
        //        //button6.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        ListView1.Visibility = Visibility.Visible;
        //        Label1.Visibility = Visibility.Hidden;
        //        //TextBox2.Visibility = Visibility.Hidden;
        //        //button6.Visibility = Visibility.Hidden;
        //    }
        //}
         
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)// кликнули левой кн.м.
        {
            DragMove(); //перемещаем форму

            if (e.ClickCount == 2) // прячем форму
                Hide();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)// клик правой кнопкой по форме
        {
            
        }
    }

    public class UsersChat
    {
        public UsersChat(string name, string path, string comp)
        {
            Name = name;
            Path = path;
            Comp = comp;
        }

        public string Name { get; set; }
        public string Path { get; set; }
        public string Comp { get; set; }
        public string IPComp { get; set; }
    }


}



//System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];

//<Setter Property = "Background" >
//              < LinearGradientBrush EndPoint="0.3,1" StartPoint="0,0">
//                  <GradientStop Color = "Black" Offset="0"/>
//                  <GradientStop Color = "#FFF3F2F2" Offset="1"/>
//              </LinearGradientBrush>
//          </Setter>


//private void SetIconToMainApplication() //   метод создания иконки в трее и ее отображение 
//{
//    if (_notifyIcon == null)
//    {
//        _notifyIcon = new NotifyIcon();
//        _notifyIcon.Icon = Properties.Resources.ResourceManager.GetObject("icTr1") as Icon;

//        _notifyIcon.Visible = true;
//    }
//    _notifyIcon.MouseClick += (sndr, args) => //  клик по иконке в трее
//    {
//        System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
//        messageBoxCS.AppendFormat("{0} = {1}", "Button", args.Button);
//        messageBoxCS.AppendLine();
//        messageBoxCS.AppendFormat("{0} = {1}", "Clicks", args.Clicks);
//        messageBoxCS.AppendLine();
//        messageBoxCS.AppendFormat("{0} = {1}", "X", args.X);
//        messageBoxCS.AppendLine();
//        messageBoxCS.AppendFormat("{0} = {1}", "Y", args.Y);
//        messageBoxCS.AppendLine();
//        messageBoxCS.AppendFormat("{0} = {1}", "Delta", args.Delta);
//        messageBoxCS.AppendLine();
//        messageBoxCS.AppendFormat("{0} = {1}", "Location", args.Location);
//        messageBoxCS.AppendLine();
//        System.Windows.Forms.MessageBox.Show(messageBoxCS.ToString(), "MouseClick Event");

//        Show(); // показать
//        WindowState = WindowState.Normal; // 
//    };
//    Hide(); // спрятать окно
//}