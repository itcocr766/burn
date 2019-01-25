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

namespace POS.clientesprincipal
{
    public partial class climod : Form
    {
        string tipcid = "";


        public climod()
        {

            InitializeComponent();
        }
        private void climod_Load(object sender, EventArgs e)
        {
            cargar();
            comboBox1.SelectedIndex = 0;
        }


        public void cargar()
        {

            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Cedula from clientes";
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from clientes where Cedula like '" + cedula.Text + "%'";
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
                    mysql.cadenasql = "select * from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.lector = mysql.comando.ExecuteReader();
                    while (mysql.lector.Read())
                    {
                        nombre.Text = mysql.lector["Nombre"].ToString();
                        telefono.Text = mysql.lector["Telefono1"].ToString();
                        textBox1.Text = mysql.lector["Telefono2"].ToString();
                        correo.Text = mysql.lector["Correo"].ToString();
                        direccion.Text = mysql.lector["Direccion"].ToString();
                        

                    }
                    mysql.Dispose();
                    button1.Enabled = true;
                    direccion.Focus();
                }


            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }

        private void barcode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select Cedula,Nombre from clientes where Cedula like '" + cedula.Text + "%'";
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text == "Física")
                {
                    tipcid = "01";
                }
                else if (comboBox1.Text == "Jurídica")
                {
                    tipcid = "02";
                }
                else if (comboBox1.Text == "NITE")
                {
                    tipcid = "04";
                }
                else if (comboBox1.Text == "DIMEX")
                {
                    tipcid = "03";
                }
                if (dataGridView1.CurrentRow.Cells[0].Value.ToString()!="0"&&!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(telefono.Text) && !string.IsNullOrEmpty(correo.Text)&& !string.IsNullOrEmpty(direccion.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "update clientes set Telefono2='"+textBox1.Text+"',Telefono1='" + telefono.Text.Trim() + "'" +
                            ",Correo='" + correo.Text.Trim() + "',Direccion='" + direccion.Text.Trim() +
                            "',Nombre='"+nombre.Text+"',TipoCed='"+tipcid+"' where Cedula='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Solicitud procesada correctamente", "Acción realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button1.Enabled = false;
                        cedula.Focus();
                    }

                }



            }
            catch (Exception excep)
            {

                Mensaje.Error(excep, "102");
            }
        }

        private void dataGridView1_Click_1(object sender, EventArgs e)
        { string ticedula ="";
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select * from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.lector = mysql.comando.ExecuteReader();
                    while (mysql.lector.Read())
                    {
                        nombre.Text = mysql.lector["Nombre"].ToString();
                        telefono.Text = mysql.lector["Telefono1"].ToString();
                        textBox1.Text = mysql.lector["Telefono2"].ToString();
                        correo.Text = mysql.lector["Correo"].ToString();
                        direccion.Text = mysql.lector["Direccion"].ToString();
                        ticedula = mysql.lector["TipoCed"].ToString();
                       

                    }
                    mysql.Dispose();
                    button1.Enabled = true;
                    direccion.Focus();
                }

                if(ticedula=="01")
                {
                    ticedula = "Física";
                }
                else if (ticedula == "02")
                {
                    ticedula = "Jurídica";
                }
                if (ticedula == "03")
                {
                    ticedula = "DIMEX";
                }
                if (ticedula == "04")
                {
                    ticedula = "NITE";
                }

                comboBox1.Text = ticedula;



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
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select * from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            nombre.Text = mysql.lector["Nombre"].ToString();
                            telefono.Text = mysql.lector["Telefono1"].ToString();
                            textBox1.Text = mysql.lector["Telefono2"].ToString();
                            correo.Text = mysql.lector["Correo"].ToString();
                            direccion.Text = mysql.lector["Direccion"].ToString();
                           

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
    }
}
