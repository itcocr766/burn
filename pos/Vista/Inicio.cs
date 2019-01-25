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
using POS.Configuracion;
using MySql.Data.MySqlClient;
namespace POS.Vista
{
    public partial class Inicio : Form
    {
        conexionabasedatos cnbd = new conexionabasedatos();
        Form1 f1 = new Form1();
        string codigocajero = "";
        public Inicio()
        {
            InitializeComponent();
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        public string codigo()
        {
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select Codigo from registro where Nombre='" + textBox1.Text.Trim() + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.lector = mysql.comando.ExecuteReader();
                    if (mysql.lector.Read())
                    {

                        codigocajero = mysql.lector["Codigo"].ToString();
                    }
                    mysql.Dispose();

                }

            }
            catch (Exception lou)
            {
                Mensaje.Error(lou, "49");
             
                    

            }
           
            return codigocajero;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cnbd.consultar(textBox1,textBox2))
                {
                    this.Visible = false;
                    f1.Show();
                    f1.facturando.Text = textBox1.Text.Trim();
                   

                }
                else
                {

                    MessageBox.Show("Este usuario no existe","Usuario no identificado",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

            }
            catch (Exception err_sqlite003)
            {
                Mensaje.Error(err_sqlite003, "82");
              
                

            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Inicio_KeyDown(object sender, KeyEventArgs e)
        {
            eventos(e);
        }

        public void eventos(KeyEventArgs er)
        {
          
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
          //  eventos(e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            eventos(e);
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void configuraciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Contrasenaconfig contra = new Contrasenaconfig();
            contra.Show(this);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cnbd.consultar(textBox1, textBox2))
                {
                    //MessageBox.Show("si");
                    this.Visible = false;
                    f1.Show();
                    f1.facturando.Text = textBox1.Text.Trim();
                    f1.textBox4.Text = codigo();

                }
                else
                {

                    MessageBox.Show("Este usuario no existe", "Usuario no identificado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception err_sqlite003)
            {
                Mensaje.Error(err_sqlite003, "146");
             

            }
        }
    }
}
