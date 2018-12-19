using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Configuracion
{
    public partial class Contrasenaconfig : Form
    {
        public Contrasenaconfig()
        {
            InitializeComponent();
        }

        private void Contrasenaconfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return)
            {
                if (textBox1.Text.Trim()=="Intlog1$")
                {
                    this.Visible = false;
                    configuracion config = new configuracion();
                    config.Show();

                }
                else
                {

                    MessageBox.Show("Contraseña incorrecta");
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Visible = false;

            }
        }

        private void Contrasenaconfig_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (textBox1.Text.Trim() == "Intlog1$")
                {
                    this.Visible = false;
                    configuracion config = new configuracion();
                    config.Show(this);

                }
                else
                {

                    MessageBox.Show("Contraseña incorrecta");
                }

            }
            else if (e.KeyCode==Keys.Escape)
            {
                this.Visible = false;

            }
        }
    }
}
