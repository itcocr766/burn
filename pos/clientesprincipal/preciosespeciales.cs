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
    public partial class preciosespeciales : Form
    {
        public preciosespeciales()
        {
            InitializeComponent();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                if (e.KeyCode==Keys.Enter)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        string query = "SELECT * FROM especiales where Cliente='" + comboBox1.Text + "'";

                        MySqlDataAdapter adap = new MySqlDataAdapter(query, mysql.con);
                        adap.Fill(dt);
                        dataGridView1.DataSource = dt;



                        mysql.cadenasql = "select * from clientes where Cedula='"+comboBox1.Text+"'";
                        mysql.comando  = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                richTextBox1.Text = lee["Nombre"].ToString();
                              
                            }
                        }

                        label2.Visible = true;
                        comboBox2.Visible = true;
                        richTextBox1.Visible = true;
                        comboBox1.Enabled = false;
                        label5.Visible = true;
                  
                        comboBox2.Focus();
                        mysql.Dispose();
                    }

                }

               
            }
            catch (Exception efr)
            {
                MessageBox.Show(efr.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                label2.Visible = false;
                label3.Visible = false;
                comboBox2.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;
          
            }
        
        }

        private void preciosespeciales_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = cargarClientes();
            comboBox1.DisplayMember = "Cedula";
            comboBox1.ValueMember = "Cedula";


            comboBox2.DataSource = cargaritems();
            comboBox2.DisplayMember = "Codigo";
            comboBox2.ValueMember = "Codigo";
        }

        public object cargarClientes()
        {
            DataTable dt = new DataTable();
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    string query = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();


                }

            }
            catch (Exception klsk)
            {

                Mensaje.Error(klsk, "1569");


            }

            return dt;
        }


        public object cargaritems()
        {
            DataTable dt = new DataTable();
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    string query = "SELECT * FROM items";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();


                }

            }
            catch (Exception klsk)
            {

                Mensaje.Error(klsk, "1569");


            }

            return dt;
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
           
            try
            {
                if (e.KeyCode==Keys.Enter)
                {

                    using (var mysql=new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "select * from items where Codigo='" + comboBox2.Text + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                richTextBox2.Text = lee["Nombre"].ToString();
                                textBox2.Text =string.Format("{0:N2}",double.Parse(lee["Precio"].ToString()));
                            }
                        }


                        mysql.Dispose();
                        label3.Visible = true;
                        textBox1.Visible = true;
                        richTextBox2.Visible = true;
                        label6.Visible = true;
                        label4.Visible = true;
                        textBox2.Visible = true;
                        button1.Visible = true;
                        comboBox2.Enabled = false;

                    }



                    
                }
                   
                
            }
            catch (Exception efr)
            {
                MessageBox.Show(efr.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label2.Visible = false;
                label3.Visible = false;
                comboBox2.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Int64 count = 0;
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select count(*) from especiales where Item='"+comboBox2.Text+"' and Cliente='"+comboBox1.Text+"'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql,mysql.con);
                        mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee=mysql.comando.ExecuteReader())
                    {
                        while (lee.Read())
                        {
                            count = Int64.Parse(lee["count(*)"].ToString());
                        }
                    }
                    mysql.Dispose();
                
                }


                if (count<1)
                {

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "insert into especiales (Item,Cliente,Precio,Descripcion) values (@i,@c,@p,@no)";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.Parameters.Add("@i", MySqlDbType.String).Value = comboBox2.Text;
                        mysql.comando.Parameters.Add("@c", MySqlDbType.Int64).Value = comboBox1.Text;
                        mysql.comando.Parameters.Add("@p", MySqlDbType.Decimal).Value = textBox1.Text;
                        mysql.comando.Parameters.Add("@no", MySqlDbType.String).Value = richTextBox2.Text;
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Solicitud procesada correctamente", "Solicitud procesada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label2.Visible = false;
                        label3.Visible = false;
                        comboBox2.Visible = false;
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        richTextBox1.Visible = false;
                        richTextBox2.Visible = false;
                        label5.Visible = false;
                        label6.Visible = false;
                        label4.Visible = false;
                        button1.Visible = false;
                        comboBox1.Focus();
                        dataGridView1.DataSource = null;
                    }
                }
                else
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "update especiales set Precio=@p where Item='"+comboBox2.Text+"' and Cliente='"+comboBox1.Text+"'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    
                        mysql.comando.Parameters.Add("@p", MySqlDbType.Decimal).Value = textBox1.Text;
                      
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Solicitud procesada correctamente", "Solicitud procesada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        label2.Visible = false;
                        label3.Visible = false;
                        comboBox2.Visible = false;
                        textBox1.Visible = false;
                        textBox2.Visible = false;
                        comboBox1.Enabled = true;
                        comboBox2.Enabled = true;
                        richTextBox1.Visible = false;
                        richTextBox2.Visible = false;
                        label5.Visible = false;
                        label6.Visible = false;
                        label4.Visible = false;
                        button1.Visible = false;
                        comboBox1.Focus();
                        dataGridView1.DataSource = null;
                    }
                }


                //using (var mysql = new Mysql())
                //{
                //    mysql.conexion();

                //    string query = "SELECT * FROM especiales where Cliente='" + comboBox1.Text + "'";

                //    MySqlDataAdapter adap = new MySqlDataAdapter(query, mysql.con);
                //    adap.Fill(dt);
                //    dataGridView1.DataSource = dt;
                //    mysql.Dispose();
                //}



                }
            catch (MySqlException ms)
            {
                MessageBox.Show(ms.Message);
                //MessageBox.Show("Los datos suministrados no son correctos. " +
                //    "\nEs posible que este producto ya tenga un precio especial para este cliente", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            catch (Exception eds)
            {
                MessageBox.Show(eds.ToString()+"eds", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                label2.Visible = false;
                label3.Visible = false;
                comboBox2.Visible = false;
                textBox1.Visible = false;
                button1.Visible = false;

            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) ||char.IsControl(e.KeyChar) ||e.KeyChar=='.')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar) || char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
