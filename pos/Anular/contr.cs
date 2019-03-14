using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Modelo;
namespace POS.Anular
{
    public partial class contr : Form
    {
        conexionabasedatos cnbd = new conexionabasedatos();

        public contr()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnbd.consultar2(textBox1, textBox2))
                { Form1 f = new Form1();
                    this.Visible = false;
                    AnulaFactura ad = new AnulaFactura(f);
                    ad.Show(this);
                }
                else
                {
                    MessageBox.Show("Este usuario no existe o no tiene permisos", "Usuario no identificado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception exce)
            {
                Mensaje.Error(exce, "37");
            }
        }
    }
}
