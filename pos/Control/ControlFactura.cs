using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using POS.Modelo;

namespace POS.Control
             //comentario nuevo
{
    class ControlFactura
    {


        
        public void logs(string l)
        {
            try
            {
                StreamWriter log;
                StreamReader re;
                re = new StreamReader("logs.txt");
                string datos;

                if (File.Exists("logs.txt"))
                {
                    datos = re.ReadToEnd();
                    re.Close();
                    log = new StreamWriter("logs.txt");
                    log.WriteLine("\n" + datos + "\n" + l);
                    log.Flush();
                    log.Close();
                }
                else
                {
                    log = new StreamWriter("logs.txt");


                    log.WriteLine(l);
                    log.Flush();
                    log.Close();

                }







            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }

        }
    }
}
