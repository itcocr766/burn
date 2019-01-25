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
using MySql.Data.MySqlClient;
namespace POS.Reportes
{
    public partial class existencias : Form
    {
        public existencias()
        {
            InitializeComponent();
        }

        private void existencias_Load(object sender, EventArgs e)
        {
            reportViewer1.Clear();
            using (var mysql=new Mysql())
            {
                mysql.conexion();
                mysql.cadenasql = "select * from items";
                MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql,mysql.con);
                adapt.Fill(dataSet1.existencias);
                mysql.Dispose();
            }
                this.reportViewer1.RefreshReport();
        }
    }
}
