using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace POS.Modelo
{
    public class Mysql : IDisposable
    {

        public MySqlConnection con;
        public MySqlCommand comando = null;
        public MySqlDataReader lector;
        public string cadenasql = "";
        public MySqlTransaction trans;
        public void conexion()
        {
            con = new MySqlConnection("server="+
                ConfigurationManager.AppSettings["server"]+";port="+
                ConfigurationManager.AppSettings["port"]+";username="+
                ConfigurationManager.AppSettings["user"]+";password="+
                ConfigurationManager.AppSettings["pass"]+ ";SslMode=none;database="+
                ConfigurationManager.AppSettings["db"]);
            //con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=Intlog6151$%;SslMode=none;database=carniceriadecimal");
            //con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;SslMode=none;database=carniceriadecimal");
            con.Open();
            
        }
        public void conexion2()
        {
            con = new MySqlConnection("server=" +
               ConfigurationManager.AppSettings["server"] + ";port=" +
               ConfigurationManager.AppSettings["port"] + ";username=" +
               ConfigurationManager.AppSettings["user"] + ";password=" +
               ConfigurationManager.AppSettings["pass"] + ";SslMode=none;database=" +
               ConfigurationManager.AppSettings["db"]);
            //con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=Intlog6151$%;SslMode=none;database=carniceriadecimal");
            //con = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;SslMode=none;database=carniceriadecimal");
            con.Open();
            trans=con.BeginTransaction();
        }
      


        public void Dispose()
        {
            cadenasql = null;
            comando = null;
            con.Close();
            
        }
      
        public void rol()
        {
            try
            {
                //Dispose();
                //con.Open();
                trans.Commit();
            }
            catch (Exception e)
            {
                
                trans.Rollback();
                MessageBox.Show("Rollback\n", e.ToString());
            }
            
            
        }



    }
}


