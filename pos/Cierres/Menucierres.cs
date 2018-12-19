using POS.Reportes;
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
    public partial class Menucierres : Form
    {
        public Menucierres()
        {
            InitializeComponent();
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conficon confi = new conficon();
            confi.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cierre ci = new Cierre();
            ci.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menureportes mr = new menureportes();
            mr.Show(this);
        }
    }
}
