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
using POS.Modelo;
namespace POS.Vista
{
    public partial class productos : Form
    {
        private Form1 m_frm;
        public productos(Form1 frm)
        {
            InitializeComponent(); m_frm = frm;
    
        }



        private void productos_Load(object sender, EventArgs e)
        {
            //textBox4.Focus();
            cargarproductos();
            Activate();textBox4.Focus();
        }


        public void cargarproductos()
        {
            try
            {using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from items";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();


                }
                    

            }
            catch (Exception hju)
            {
                Mensaje.Error(hju,"42");
          

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
                    string query = "select * from items where Nombre like '" + textBox1.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();
                    
                }
                   

            }
            catch (Exception euju)
            {

                Mensaje.Error(euju,"71");
             
                 
            }
           
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from items where Familia like '" + textBox3.Text + "%'";
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from items where Codigo like '" + textBox4.Text + "%'";
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
                if (dataGridView1.Rows.Count > 0)
                {
                    this.Visible = false;

                    m_frm.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    m_frm.Activate();
                    m_frm.textBox1.Focus();
                    SendKeys.Send("{ENTER}");


                }

            }
            catch (Exception vf)
            {
                Mensaje.Error(vf, "118");

            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                try
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        this.Visible = false;

                        m_frm.textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                        m_frm.Activate();
                        m_frm.textBox1.Focus();
                        SendKeys.Send("{ENTER}");


                    }

                }
                catch (Exception vf)
                {
                    Mensaje.Error(vf, "118");

                }

            }
        }

        private void productos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F1)
            {
                Activate();
                textBox4.Focus();

            }
        }

        private void productos_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void productos_Click(object sender, EventArgs e)
        {
            //Activate();
        }
    }
}
