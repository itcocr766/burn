using MySql.Data.MySqlClient;
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

namespace POS.Reportes
{
    public partial class clis : Form
    {
        public clis()
        {
            InitializeComponent();
        }

        private void clientes_Load(object sender, EventArgs e)
        {

            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select * from clientes";
                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(dataSet1.clientes);
                mysql.Dispose();
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
