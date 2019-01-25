using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Reportes;
namespace POS.Cierres
{
    public partial class menureportes : Form
    {
        public menureportes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            facturasF ff = new facturasF();
            ff.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            existencias ex = new existencias();
            ex.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            facturasp fp = new facturasp();
            fp.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pedidos pe = new pedidos();
            pe.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clis cl = new clis();
            cl.Show(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            especiales espe = new especiales();
            espe.Show(this);
        }
    }
}
