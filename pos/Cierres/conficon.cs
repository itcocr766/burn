using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Cierres
{
    public partial class conficon : Form
    {
        public conficon()
        {
            InitializeComponent();
        }

        private void conficon_Load(object sender, EventArgs e)
        {
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    if (textBox1.Text.Trim() == "posmaster1")
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
            catch (Exception ex)
            {
                MessageBox.Show("No hemos podido hacer la configuración en este momento","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
           
        }
    }
}
