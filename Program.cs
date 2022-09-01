using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGraphSample
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Dll.Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Dll.Uninitialize();
        }
    }
}
