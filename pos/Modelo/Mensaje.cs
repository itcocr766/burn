using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Modelo
{
    public static class Mensaje
    {
        public static void Error(Exception ef,string numerodelinea)
        {
            MessageBox.Show("Error en la línea "+ numerodelinea + "  " + ef.Message+"  "+ef.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


    }
}
