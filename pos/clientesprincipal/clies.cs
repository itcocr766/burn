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
namespace POS.clientesprincipal
{
    public partial class clies : Form
    {
        private Form1 m_frm;
        public clies(Form1 frm)
        {
            InitializeComponent(); m_frm = frm;
        }

        private void clientes_Load(object sender, EventArgs e)
        {
            cargarClientes();
        }


        public void cargarClientes()
        {

            try
            {using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from clientes";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }
                   

            }
            catch (Exception hju)
            {
                Mensaje.Error(hju, "42");

              
                   
            }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from clientes where Nombre like '" + textBox1.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "71");
              
                   
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count>0)
                {
                    m_frm.comboBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    m_frm.textBox17.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    m_frm.textBox9.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    m_frm.textBox10.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


                    this.Visible = false;

                }
             
            }
            catch (Exception vf)
            {
                Mensaje.Error(vf, "118");

            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    ced = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            //    nombre = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            //    te = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //    co = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            //}
            //catch (Exception vf)
            //{
            //    Mensaje.Error(vf,"118");

            //}
           
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
