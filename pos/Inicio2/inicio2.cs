using MySql.Data.MySqlClient;
using POS.Modelo;
using POS.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Inicio2
{
    public partial class inicio2 : Form
    {
        
        conexionabasedatos cnbd = new conexionabasedatos();
        public inicio2()
        {
            InitializeComponent();
        }

        private void inicio2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnbd.consultar2(textBox1, textBox2))
                {
                    this.Visible = false;
                    administracion ad = new administracion();
                    ad.Show(this);
                }
                else
                {
                    MessageBox.Show("Este usuario no existe o no tiene premisos", "Usuario no identificado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception exce)
            {
                Mensaje.Error(exce,"37");
            }
        }
    }
}
