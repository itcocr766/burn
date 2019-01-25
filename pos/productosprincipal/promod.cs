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

namespace POS.productosprincipal
{
    public partial class promod : Form
    {
        public promod()
        {
            InitializeComponent();
        }

        private void promod_Load(object sender, EventArgs e)
        {
            cargar();
        }

        public void cargar()
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Codigo from items";
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
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Codigo,Nombre from items where Codigo like '" + barcode.Text + "%'";
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
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select * from items where Codigo='"+dataGridView1.CurrentRow.Cells[0].Value.ToString()+"'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.lector = mysql.comando.ExecuteReader();
                    while (mysql.lector.Read())
                    {
                        descripcion.Text = mysql.lector["Nombre"].ToString();
                        precio.Text = mysql.lector["Precio"].ToString();
                        cantidad.Text = mysql.lector["Cantidad"].ToString();
                       
                        impuesto.Text = mysql.lector["Impuesto"].ToString();
                        familia.Text = mysql.lector["Familia"].ToString();
                       

                    }
                    mysql.Dispose();
                    button1.Enabled = true;
                    descripcion.Focus();
                }


            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }


        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode==Keys.Up ||e.KeyCode==Keys.Down)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select * from items where Codigo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            descripcion.Text = mysql.lector["Nombre"].ToString();
                            precio.Text = mysql.lector["Precio"].ToString();
                            cantidad.Text = mysql.lector["Cantidad"].ToString();
                           
                            impuesto.Text = mysql.lector["Impuesto"].ToString();
                            familia.Text = mysql.lector["Familia"].ToString();
                            

                        }
                        mysql.Dispose();
                    }

                }
              


            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString())&&
                    !string.IsNullOrEmpty(descripcion.Text)&& !string.IsNullOrEmpty(impuesto.Text)&&
                    !string.IsNullOrEmpty(precio.Text)&&
                    !string.IsNullOrEmpty(cantidad.Text)&&
                    
                    !string.IsNullOrEmpty(familia.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "update items set Nombre='"+ descripcion.Text.Trim()+ "'" +
                            ",Impuesto='"+ impuesto.Text.Trim() + "',Precio='"+ precio.Text.Trim() + 
                            "',Cantidad='"+decimal.Parse(cantidad.Value.ToString()) + "',Familia='"+ familia.Text.Trim() + "' where Codigo='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Solicitud procesada correctamente","Acción realizada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        button1.Enabled = false;
                        barcode.Focus();
                    }

                }



            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }
    }
}
