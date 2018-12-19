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
namespace POS.Cierres
{
    public partial class metodos : Form
    {
        public metodos()
        {
            InitializeComponent();
        }

        private void metodos_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        public void establecermetodo()
        {
            try
            {
                using (var mysql=new Mysql22())
                {
                    if (comboBox1.SelectedIndex == 0)
                    {
                        mysql.conexion22();
                        mysql.cadenasql22 = "delete from Metodo";
                        mysql.comando22 = new MySqlCommand(mysql.cadenasql22, mysql.con22);
                        mysql.comando22.ExecuteNonQuery();
                        mysql.cadenasql22 = "insert into metodo(Metodo)values('F')";
                        mysql.comando22 = new MySqlCommand(mysql.cadenasql22, mysql.con22);
                        mysql.comando22.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Se establecio el método F para las transacciones");

                    }
                    else
                    {

                        mysql.conexion22();
                        mysql.cadenasql22 = "delete from Metodo";
                        mysql.comando22 = new MySqlCommand(mysql.cadenasql22, mysql.con22);
                        mysql.comando22.ExecuteNonQuery();
                        mysql.cadenasql22 = "insert into metodo(Metodo)values('P')";
                        mysql.comando22 = new MySqlCommand(mysql.cadenasql22, mysql.con22);
                        mysql.comando22.ExecuteNonQuery();
                        mysql.Dispose();
                        MessageBox.Show("Se establecio el método P para las transacciones");
                    }

                }
                    

            }
            catch (Exception err_mysql)
            {

                MessageBox.Show(err_mysql.ToString());
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            establecermetodo();
        }
    }
}
