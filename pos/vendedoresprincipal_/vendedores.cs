using MySql.Data.MySqlClient;
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
namespace POS.vendedoresprincipal_
{
    public partial class vendedores : Form
    {
        private Form1 m_frm;
        public vendedores(Form1 frm)
        {
            InitializeComponent(); m_frm = frm;
        }

        private void vendedores_Load(object sender, EventArgs e)
        {
            cargarVendedores();
        }
        public void cargarVendedores()
        {

            try
            {using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from vendedores";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }
                   

            }
            catch (Exception hju)
            {
                Mensaje.Error(hju, "42");
                using (var mysql = new Mysql())
                {
                    if (mysql.con.State == ConnectionState.Open)
                    {

                        mysql.Dispose();
                    }

                }
                    
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from vendedores where Nombre like '" + textBox1.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }
                    

            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "71");

                using (var mysql = new Mysql())
                {
                    if (mysql.con.State == ConnectionState.Open)
                    {

                        mysql.Dispose();
                    }

                }
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    m_frm.comboBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    m_frm.textBox16.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
             


                    this.Visible = false;

                }

            }
            catch (Exception vf)
            {
                Mensaje.Error(vf, "118");

            }
        }
    }
}
