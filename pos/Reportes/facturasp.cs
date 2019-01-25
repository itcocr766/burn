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
    public partial class facturasp : Form
    {
        public facturasp()
        {
            InitializeComponent();
        }

        private void facturasp_Load(object sender, EventArgs e)
        {

            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select * from sales";
                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(dataSet1.PfacturasP);
                mysql.Dispose();
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
