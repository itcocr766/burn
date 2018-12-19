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
using POS.clientes;
using POS.Inicio2;
using POS.Cierres;
namespace POS
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Progress p = new Progress();
            p.Show(this);
            p.Activate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            inicio2 ini = new inicio2();
            ini.Show(this);
            ini.Activate();
            //this.WindowState = FormWindowState.Minimized;
            //administracion ini = new administracion();
            //ini.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            inicio3 mc = new inicio3();
            mc.Show(this);
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Application.Exit();

            }
            catch (Exception d)
            {

            }
           
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.CadetBlue;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = Color.CadetBlue;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.CadetBlue;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(85, 94, 103);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(85, 94, 103);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(85, 94, 103);
        }
    }
}
