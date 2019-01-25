using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.cajerosprincipal;
using POS.clientes;
using POS.clientesprincipal;
using POS.productosprincipal;
using POS.vendedoresprincipal_;

namespace POS.Vista
{
    public partial class administracion : Form
    {
        Form1 f1;
        public administracion()
        {
            InitializeComponent();
        }

        private void administracion_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
            clienform clif = new clienform(f1);
            
            clif.bandera = false;

            clif.Show(this);
            clif.textBox5.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cajeross caj = new cajeross();
            caj.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            creavendedor cv = new creavendedor();
            cv.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            creaitem cit = new creaitem();
            cit.Show(this);
        }

        private void button10_Click(object sender, EventArgs e)
        {
       
        }

        private void button5_Click(object sender, EventArgs e)
        {
            climod clm = new climod();
            clm.Show(this);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            promod prm = new promod();
            prm.Show(this);
        }

        private void administracion_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void administracion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    clienform clf = new clienform(f1);
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F2)
                {
                    climod clf = new climod();
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F3)
                {
                    creaitem clf = new creaitem();
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F4)
                {
                    promod clf = new promod();
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F5)
                {
                    cajeross clf = new cajeross();
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F6)
                {
                    creavendedor clf = new creavendedor();
                    clf.Show(this);

                }
                else if (e.KeyCode == Keys.F7)
                {
                    vendmod clf = new vendmod();
                    clf.Show(this);

                }
                else if (e.KeyCode==Keys.F8)
                {
                    preciosespeciales pes = new preciosespeciales();
                    pes.Show(this);

                }
                
               

            }
            catch (Exception exce)
            {


            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            vendmod clf = new vendmod();
            clf.Show(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            preciosespeciales pe = new preciosespeciales();
            pe.Show(this);
        }
    }
}
