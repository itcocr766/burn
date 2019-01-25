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
namespace POS.cajerosprincipal
{
    public partial class cajeross : Form
    {
        conexionabasedatos cndb = new conexionabasedatos();
        public cajeross()
        {
            InitializeComponent();
        }

        private void cajeross_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                cndb.insertar(textBox1,textBox2,check);
                limpieza();
            }
            catch (Exception xce)
            {
                Mensaje.Error(xce,"35");

            }
           
        }

       public void limpieza()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
