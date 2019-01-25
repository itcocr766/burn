using POS.Modelo;
using POS.Cierres;
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
    public partial class inicio3 : Form
    {
        conexionabasedatos cnbd = new conexionabasedatos();
        public inicio3()
        {
            InitializeComponent();
        }

        private void inicio3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnbd.consultar2(textBox1, textBox2))
                {
                    this.Visible = false;
                    Menucierres ad = new Menucierres();
                    ad.Show(this);
                }
                else
                {
                    MessageBox.Show("Este usuario no existe o no tiene premisos", "Usuario no identificado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception exce)
            {
                MessageBox.Show(exce.ToString());
                Mensaje.Error(exce, "37");
            }
        }
    }
}
