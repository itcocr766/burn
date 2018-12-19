using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.clientesprincipal
{
    public static class Errores
    {
        public static void err()
        {
            MessageBox.Show("Algo salió mal. Por favor intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public static void inf()
        {
            MessageBox.Show("La solicitud fue procesada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void war()
        {
            MessageBox.Show("Falta información", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
