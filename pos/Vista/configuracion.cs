using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Vista
{
    public partial class configuracion : Form
    {
        public configuracion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            configuraciondecadenas("server",textBox2.Text);
            configuraciondecadenas("port", textBox1.Text);
            configuraciondecadenas("user", textBox3.Text);
            configuraciondecadenas("pass", textBox4.Text);
            configuraciondecadenas("db", textBox5.Text);

            MessageBox.Show(ConfigurationManager.AppSettings["server"]
                +"\n"+ ConfigurationManager.AppSettings["port"]+
                "\n"+ ConfigurationManager.AppSettings["user"]+
                "\n"+ ConfigurationManager.AppSettings["pass"]+"\n"+
                ConfigurationManager.AppSettings["db"], "Listo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        public void configuraciondecadenas(string cadena,string value)
        {
            try
            {
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                conf.AppSettings.Settings.Remove(cadena);
                conf.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");
                conf.AppSettings.Settings.Add(cadena, value);
                conf.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection("appSettings");

              

            }
            catch (Exception k)
            {

                MessageBox.Show(k.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Hand);
            }
        }

        private void configuracion_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            configuraciondecadenas("companiapi", textBox2.Text);
            configuraciondecadenas("sucursal", textBox1.Text);
            configuraciondecadenas("endpoint", textBox3.Text);
          

            MessageBox.Show(ConfigurationManager.AppSettings["companiapi"]
                + "\n" + ConfigurationManager.AppSettings["sucursal"] +
                "\n" + ConfigurationManager.AppSettings["endpoint"], "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
