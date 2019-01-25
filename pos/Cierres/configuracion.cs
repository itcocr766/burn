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

namespace POS.Cierres
{
    public partial class configuracion : Form
    {
        string cadenacie = "";
        public configuracion()
        {
            InitializeComponent();
        }

        private void configuracion_Load(object sender, EventArgs e)
        {
            
        }

        public void guardarconfig()
        {
            try
            {
                //cadenacie = "server=127.0.0.1;port=3306;username=root;password=;SslMode = none;database=" + textBox5.Text.Trim();
                cadenacie = "server=127.0.0.1;port=3306;username=root;password=;SslMode = none;database=" + textBox5.Text.Trim();
                //cadena = "server=tiendaspz.local;port=3306;username=root;password=123456789;SslMode = none;database=" + textBox5.Text.Trim();
                //cadena = "server=localhost;port=3306;username=root;password=;SslMode = none;database=" + textBox5.Text.Trim();
                //cadena = "server=db4free.net;port=3306;username=arsoftcr;password=Rivipe19866;SslMode = none;database=" + textBox5.Text.Trim();
                Configuration conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                conf.AppSettings.Settings.Remove("cadena22");
                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                conf.AppSettings.Settings.Add("cadena22", cadenacie);
                conf.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("Configuracion exitosa. Ya puede cambiar el estado de la base de datos en : " + textBox5.Text);

            }
            catch (Exception guardarconfig)
            {
                MessageBox.Show("No pudimos conectar a la base de datos. \nPor favor intente de nuevo en unos instantes");

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardarconfig();
        }
    }
}
