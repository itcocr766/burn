using POS.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Vista
{
    public partial class Progress : Form
    {
        Form1 f1 = new Form1();
        public Progress()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value<100)
            {
                progressBar1.Value += 4;
                label1.Text = "Cargando..."+progressBar1.Value+" %";
            }
            else
            {

                timer1.Enabled = false;
                this.Visible = false;
                Inicio ini = new Inicio();
                ini.Show();
                //this.Visible = false;
                //f1.Show();

            }


        }

        private void Progress_Load(object sender, EventArgs e)
        {
            
            timer1.Start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Progress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            //Application.Exit();
        }
    }
}
