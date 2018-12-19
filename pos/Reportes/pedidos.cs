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
    public partial class pedidos : Form
    {
        public pedidos()
        {
            InitializeComponent();
        }

        private void pedidos_Load(object sender, EventArgs e)
        {

            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select * from pedidos";
                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(dataSet1.Pedidos);
                mysql.Dispose();
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
