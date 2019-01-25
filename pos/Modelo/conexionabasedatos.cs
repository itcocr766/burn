using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Configuration;
namespace POS.Modelo
{
    class conexionabasedatos
    {
        string result = "";
        string result2 = "";
      
        public conexionabasedatos()
        {

            
        }

        public void insertar(TextBox textbox1,TextBox textbox2,CheckBox check)
        {
            try
            {
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(textbox2.Text);
                result = Convert.ToBase64String(encryted);
               
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    if (textbox1.Text != "" && textbox2.Text != "" && check.Checked == true)
                    {

                        mysql.cadenasql = "insert into registro(Nombre,Contrasena,Admin) values('" + textbox1.Text.Trim() + "','" + result + "'," + 1 + ");";

                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);

                        mysql.comando.ExecuteNonQuery();





                        textbox1.Text = "";
                        textbox2.Text = "";
                        check.Checked = false;
                        mensaje("Usuario agregado exitosamente");
                    }
                    else if (textbox1.Text != "" && textbox2.Text != "" && check.Checked == false)
                    {
                        mysql.cadenasql = "insert into registro(Nombre,Contrasena,Admin) values('" + textbox1.Text.Trim() + "','" + result + "'," + 0 + ");";

                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);

                        mysql.comando.ExecuteNonQuery();




                        textbox1.Text = "";
                        textbox2.Text = "";
                        mensaje("Usuario agregado exitosamente");
                    }
                    else
                    {

                        mensaje2("Rellene todos los campos");

                    }

                    mysql.Dispose();

                }
                   
            }
            catch (Exception err_sqlite02)
            {

                MessageBox.Show(err_sqlite02.ToString());
              
            }

           
        }


       


        public bool consultar(TextBox tex1,TextBox tex2)
        {
            bool existeUser=false;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(tex2.Text);
            result = Convert.ToBase64String(encryted);
            
            using (var mysql=new Mysql())
            {
                mysql.conexion();

                mysql.cadenasql = "select * from registro where Nombre='" + tex1.Text.Trim() + "' and Contrasena='" + result + "'";


                mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);

                mysql.comando.ExecuteNonQuery();
                mysql.lector = mysql.comando.ExecuteReader();


                if (mysql.lector.Read())
                {

                    existeUser = true;
                }






                mysql.Dispose();

            }
               

            return existeUser;

        }


        public bool consultar2(TextBox tex1, TextBox tex2)
        {
            bool existeUser = false;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(tex2.Text);
            result2 = Convert.ToBase64String(encryted);

            using (var mysql = new Mysql())
            {
                mysql.conexion();

                mysql.cadenasql = "select * from registro where Nombre='" + tex1.Text.Trim() + "' and Contrasena='" + result2 + "'";


                mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);

                mysql.comando.ExecuteNonQuery();
                mysql.lector = mysql.comando.ExecuteReader();


                while (mysql.lector.Read())
                {
                    //byte[] decryted = Convert.FromBase64String(mysql.lector["Contrasena"].ToString());
                    //result = System.Text.Encoding.Unicode.GetString(decryted);
                    if (Int64.Parse(mysql.lector["Admin"].ToString())==1)
                    {
                        existeUser = true;
                    }
                    else
                    {
                        MessageBox.Show("Solo administradores pueden ingresar","Acceso no permitido",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                   
                }






                mysql.Dispose();

            }


            return existeUser;

        }

        public void mensaje(string me)
        {

            MessageBox.Show(me,"",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
        public void mensaje2(string me)
        {

            MessageBox.Show(me, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
