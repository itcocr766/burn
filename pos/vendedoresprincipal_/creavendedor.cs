using POS.clientesprincipal;
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
using MySql.Data.MySqlClient;

namespace POS.vendedoresprincipal_
{
    public partial class creavendedor : Form
    {
        public creavendedor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && richTextBox1.Text != "")
            {
                guardarvendedor();
                

                textBox1.Text = "";
                textBox3.Text = "";
                textBox2.Text = "";
                richTextBox1.Text = "";
            }
            else
            {
                Errores.war();
            }
        }

       public void limpieza()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text="";
        }

        public void guardarvendedor()
        {
            long codigo = 0;
            try
            {

                using (var mysql = new Mysql())
                {

                    mysql.conexion2();
                    mysql.cadenasql = "select count(*) from vendedores";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    //mysql.lector = mysql.comando.ExecuteReader();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            codigo = Int64.Parse(lee["count(*)"].ToString()) + 1;

                        }
                        else
                        {
                            codigo = 1;

                        }

                    }
                       

                   
                    mysql.cadenasql = "insert into vendedores(Codigo,Nombre,Telefono,Correo,Direccion)values('"+codigo+"','" + textBox1.Text.ToUpper().Trim() + "','" + textBox3.Text.ToUpper().Trim() + "','" + textBox2.Text.ToUpper().Trim() + "','" + richTextBox1.Text + "')";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                  



                 

                    mysql.rol();
                    mysql.Dispose();
                    limpieza();
                    Errores.inf();


                }
               
                   
              
                   

            }
            catch (Exception ef)
            {
                MessageBox.Show(ef.ToString());
                //Errores.err();
            }
        }

        private void creavendedor_Load(object sender, EventArgs e)
        {
            try
            {
                //cargarconsecutivo();

            }
            catch (Exception uiy)
            {

                Errores.err();
            }
        }

       







    }
}
