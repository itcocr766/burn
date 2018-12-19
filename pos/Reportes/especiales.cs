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
    public partial class especiales : Form
    {
        public especiales()
        {
            InitializeComponent();
        }

        private void especiales_Load(object sender, EventArgs e)
        {

            reportViewer1.Clear();
            using (var mysql = new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select especiales.Item as nei,items.Nombre as ni,especiales.Cliente as ec,clientes.Nombre as cn,especiales.Precio as ep from especiales,clientes,items where especiales.Cliente=clientes.Cedula AND especiales.Item=items.Codigo";
                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                adapt.Fill(dataSet1.especiales);
                mysql.Dispose();
            }
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
