using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Contrasena
{
    public partial class contrasena : Form
    {
       
        public contrasena()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Return)
            {
                if (textBox1.Text=="latinosadm")
                {
                    this.Visible = false;
                    
                }
                else
                {
                    this.Visible = false;
                    Form1.descuentomayor = "0";
                }

            }
            
            
        }

        private void contrasena_Load(object sender, EventArgs e)
        {

        }
    }
}
