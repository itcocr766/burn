using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Vista;
namespace POS
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
           

                Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MenuPrincipal());
            }
            catch (Exception efre)
            {
               
            }

          
        }
    }
}
