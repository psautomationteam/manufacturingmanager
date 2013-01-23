using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaoHien.UI;
using BaoHien.Properties;
using DAL.Helper;
using BaoHien.UI.PrintPreviewCustom;

namespace Com.Baohien
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingManager.CheckRegistry();
            if (!BaoHienRepository.testCurrentDBConnection())
            {                
                Application.Run(new DBConfiguration());
            }
            else
            {
                Application.Run(new Login());//To do: will replace by login form
            }
            
            
        }
    }
}
