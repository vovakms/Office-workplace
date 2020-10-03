using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace СистемаОтчетов
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        // Изменения 30-09-2020     10:17

    }
}
