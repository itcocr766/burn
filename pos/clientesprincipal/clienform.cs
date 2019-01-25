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

namespace POS.clientes
{
    public partial class clienform : Form
    {
        Form1 f1;
        string tipcid;
       public bool bandera=false;
      
        public clienform(Form1 f)
        {
            InitializeComponent();f1 = f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox4.Text != "" && textBox5.Text != "")
            {
                guardarcliente();
               
                if (bandera)
                {
                    bandera = false;
                    f1.textBox9.Text = textBox3.Text;
                    f1.textBox10.Text = textBox2.Text;
                    this.Visible = false;
                    f1.Visible = true;

                }
             
           
              
            }
            else
            {

                Errores.war();
            }
        }

        private void guardarcliente()
        {
            try
            {

                if (comboBox1.Text=="Física")
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


                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "insert into clientes(Cedula,Nombre,Telefono1,Telefono2,Correo,Direccion,TipoCed)values('" + Int64.Parse(textBox5.Text.Trim()) + "','" + textBox1.Text.ToUpper().Trim() + "','" + textBox3.Text.ToUpper().Trim() + "','"+textBox6.Text+"','" + textBox2.Text.ToUpper().Trim() + "','" + textBox4.Text.ToUpper().Trim() + "','"+tipcid+"')";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mysql.Dispose();
                    limpieza();
                    Errores.inf();
                }



                 

                    
                

            }
            catch (Exception guardarcliente)
            {

                MessageBox.Show(guardarcliente.ToString());
               
            }
        }

        private void clienform_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F7)
            {
                button1.PerformClick();
            }
        }

        private void clienform_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        public void limpieza()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
    }
}
