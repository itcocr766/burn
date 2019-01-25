using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Vuelto
{
    public partial class vueltoenpantalla : Form
    {
        Form1 f1;
        public vueltoenpantalla(Form1 f)
        {
            InitializeComponent();
            f1 = f;
        }

        private void vueltoenpantalla_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Visible = false;
            }
        }

        private void vueltoenpantalla_Load(object sender, EventArgs e)
        {
            label2.Text = "₡" + f1.elvuelto;
        }
    }
}
