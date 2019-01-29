using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Modelo
{
    public class Mysql22:IDisposable
    {
        public MySqlConnection con22;
        public MySqlCommand comando22 = null;
        public MySqlDataReader lector22;
        public string cadenasql22 = "";
        public void conexion22()
        {
            con22 = new MySqlConnection("server=" +
              ConfigurationManager.AppSettings["server"] + ";port=" +
              ConfigurationManager.AppSettings["port"] + ";username=" +
              ConfigurationManager.AppSettings["user"] + ";password=" +
              ConfigurationManager.AppSettings["pass"] + ";SslMode=none;database=" +
              ConfigurationManager.AppSettings["db"]);
            //con22 = new MySqlConnection(ConfigurationManager.AppSettings["cadena22"]);
            //con22 = new MySqlConnection("server=192.168.0.254;port=3306;username=root;password=Intlog6151$%;SslMode=none;database=carniceriadecimal");
            //con22 = new MySqlConnection("server=127.0.0.1;port=3306;username=root;password=;SslMode=none;database=carniceriadecimal");
            //con22.Open();
        }
        public void Dispose()
        {
            cadenasql22 = null;
            comando22 = null;
            con22.Close();

        }
    }
}
