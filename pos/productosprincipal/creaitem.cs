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
namespace POS.productosprincipal
{
    public partial class creaitem : Form
    {
        public creaitem()
        {
            InitializeComponent();
        }

        private void creaitem_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            promod prm = new promod();
            prm.Show(this);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertar();
        }


        public void insertar()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text)&& !string.IsNullOrEmpty(textBox2.Text)&& 
                    
                    
                    !string.IsNullOrEmpty(textBox7.Text) && 
                    !string.IsNullOrEmpty(richTextBox1.Text) && !string.IsNullOrEmpty(comboBox1.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql =
                            "insert into items(Codigo,Familia,Nombre,Precio,UnidadMedida,Impuesto,Cantidad)values('"
                            + textBox2.Text.Trim() + "','" + textBox1.Text.Trim() + "','" +
                            richTextBox1.Text.ToUpper().Trim() + "','" + decimal.Parse(textBox7.Text.Trim()) + "','"
                            + textBox3.Text + "','" + comboBox1.Text.Trim() + "','"+decimal.Parse(numericUpDown1.Value.ToString())+"')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.Dispose();
                        limpieza();
                        MessageBox.Show("La solicitud se proceso correctamente", "Acción realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {

                    MessageBox.Show("Falta información","Faltan datos",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
               
                  

            }
            catch (Exception exec)
            {

                Mensaje.Error(exec,"67");

            }
           
        }


        public void limpieza()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox7.Text = "";
            richTextBox1.Text = "";
        }
    }
}
