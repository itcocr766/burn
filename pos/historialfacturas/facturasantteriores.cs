using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using POS.Modelo;


namespace POS.historialfacturas
{
    public partial class facturasantteriores : Form
    {
        Font printFont;
        string productos;
        string formato;
        string documentoelectronico;
        StreamWriter facturawr;
        StreamReader streamToPrint;
        public facturasantteriores()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from factura where Fecha between '"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' and '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd") + "'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();


                }


            }
            catch (Exception hju)
            {
                MessageBox.Show(hju.Message);


            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;

                if (e.KeyCode==Keys.Enter)
                {
                    
                        using (var mysql = new Mysql())
                        {
                            mysql.conexion();
                            DataTable dtDatos = new DataTable();
                            string query = "select * from factura where Numero='" + textBox1.Text + "'";
                            MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                            mdaDatos.Fill(dtDatos);
                            dataGridView1.DataSource = dtDatos;
                            mysql.Dispose();


                        }
                  

                }
             


            }
            catch (Exception hju)
            {
                MessageBox.Show(hju.Message);


            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //try
            //{

            //        using (var mysql = new Mysql())
            //        {
            //            mysql.conexion();
            //            DataTable dtDatos = new DataTable();
            //            string query = "select * from factura where Numero like '" + textBox1.Text + "%'";
            //            MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
            //            mdaDatos.Fill(dtDatos);
            //            dataGridView1.DataSource = dtDatos;
            //            mysql.Dispose();


            //        }


                



            //}
            //catch (Exception hju)
            //{
            //    MessageBox.Show(hju.Message);


            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            formatodeactura();
            imprimir();
            
        }


        public void formatodeactura()
        {

            productos = "";
            formato = "";
            string fecha = "";
            string clave = "";
            string cons = "";
            string numerorecibido = "";
            string facturado = "";
            string tipopago = "";
            string cliente = "";
            string vendedor = "";
            string total = "";
            string descrip = "";
            string cant = "";
            string prec = "";
            string item = "";
            string subtotal="";
            string impues="";
            string description = "";
        
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "select * from factura,detalles where factura.Numero='" + dataGridView1.CurrentRow.Cells[0].Value + "' AND detalles.NumeroFactura=factura.Numero";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {

                            while (lee.Read())
                            {
                                fecha = lee["Fecha"].ToString();
                                numerorecibido = lee["Numero"].ToString();
                                facturado = lee["CodigoCajero"].ToString();
                                tipopago = lee["TipoPago"].ToString();
                                cliente = lee["Cliente"].ToString();
                                vendedor = lee["CodigoVendedor"].ToString();
                                total = lee["Total"].ToString();
                            }

                        }
                        mysql.Dispose();


                    }//fin del using

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "select Nombre from registro where Codigo='"+facturado+"'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {

                            while (lee.Read())
                            {
                              
                                facturado = lee["Nombre"].ToString();
                             
                            }

                        }
                        mysql.Dispose();


                    }//fin del using

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "select Nombre from vendedores where Codigo='" + vendedor + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {

                            while (lee.Read())
                            {

                                vendedor = lee["Nombre"].ToString();

                            }

                        }
                        mysql.Dispose();


                    }//fin del using

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "select Nombre from clientes where Cedula='" + cliente + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {

                            while (lee.Read())
                            {

                                cliente = lee["Nombre"].ToString();

                            }

                        }
                        mysql.Dispose();


                    }//fin del using



                    string direccion = "";
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select Direccion from clientes where Cedula='" + cliente + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            if (lee.Read())
                            {
                                direccion = lee["Direccion"].ToString();
                            }
                            else
                            {
                                direccion = "Guadalupe";
                            }

                        }
                        mysql.Dispose();
                    }//fin del using


                 //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        //mysql.cadenasql = "select * from items,detalles where detalles.Item=items.Codigo and detalles.Item";
                        //mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        //mysql.comando.ExecuteNonQuery();
                        //using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        //{
                        //    while (lee.Read())
                        //    {
                        //        description = lee["Nombre"].ToString();
                        //        //prec = lee["Precio"].ToString();
                        //        //cant = lee["Cantidad"].ToString();
                        //        //item = lee["Item"].ToString();
                        //        //productos = " \n" + productos.ToString() + string.Format("{0:N3}", cant) + "         " + string.Format("{0:N2}", prec) + " \n" + item.ToString() + " \n";
                        //    }
                        //}

                        mysql.cadenasql = "select * from detalles,items where detalles.NumeroFactura='" + numerorecibido+"'" +
                            "and detalles.Item=items.Codigo";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                prec = lee["Precio"].ToString();
                                cant = lee["Cantidad"].ToString();
                                item = lee["Item"].ToString();
                                description = lee["Nombre"].ToString();
                                productos= " \n" + productos.ToString() +string.Format("{0:N3}",cant) +"         "+string.Format("{0:N2}",prec) + " \n" + description  +  " \n";
                            }
                        }
                        mysql.Dispose();
                    }//fin del using
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select * from items,detalles where detalles.Item=items.Codigo and detalles.Item and items.Codigo='"+item+"'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();

                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                descrip = lee["Nombre"].ToString();
                            }


                        }
                        mysql.Dispose();
                    }//fin del using

                    productos += "\n  " + string.Format("{0:N2}", double.Parse(cant)) +
                                   "        ₡" + string.Format("{0:N2}", double.Parse(prec))
                                       + "\n          " + descrip;


                    /////////////////////////////////////////////////////////////////////////////////////////////

                    documentoelectronico = "";
                    if (cliente == "0")
                    {
                        documentoelectronico = "Tiquete Electrónico";
                    }
                    else
                    {
                        documentoelectronico = "Factura Electrónica";
                    }


                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "select Clave, Consecutivo from hacienda where Comprobante='" + numerorecibido + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                clave = lee["Clave"].ToString();
                                cons = lee["Consecutivo"].ToString();
                               
                            }
                           

                        }
                        mysql.Dispose();
                    }//fin del using

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "SELECT (SELECT IFNULL(((SUM(Precio))/1.13),0) FROM `detalles` WHERE NumeroFactura='"+numerorecibido+"' AND Impuesto='(G)')+(SELECT (IFNULL((SUM(Precio)),0)) FROM `detalles` WHERE NumeroFactura='"+numerorecibido+"' AND Impuesto='(E)') AS subtotal";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                subtotal = lee["subtotal"].ToString();

                            }


                        }
                        mysql.Dispose();
                    }//fin del using

                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "SELECT (((SELECT IFNULL(((SUM(Precio))/1.13),0) FROM `detalles` WHERE NumeroFactura='11' AND Impuesto='(G)')*13)/100) as impuesto";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                impues = lee["impuesto"].ToString();

                            }


                        }
                        mysql.Dispose();
                    }//fin del using


                    formato = "Razón social:\nCUBIRAMI SOCIEDAD ANÓNIMA\nCed.Jurídica:3101125675\nCorreo:lanuevaunion@hotmail.com\nLugar:San José\nTeléfono:2224-7042" +
                          "\n    Fecha:     " + fecha + "\n" +
                           "                      No.Factura:\n" + "             " + cons + "\n" +
                          "                        Clave:\n" + clave.Substring(0, 25) + "\n" +
                          clave.Substring(25) + "\n" +
                          "                Comprobante: " + numerorecibido + "\n" +
                          "                Tipo de documento:\n" + documentoelectronico + "\n" +
                          "        Tipo de pago : " + tipopago + "\n          Facturado por: " +
                          facturado +
                          "\n              Cliente:\n" + cliente + "\n" +
                          "        Dirección:\n " + direccion + "\n" +
                       "\nCantidad        Precio        \n" + "Artículo\n" +
                      "----------------------------------------------------------" + productos +
                      "\n         (E)=Exento              (G)=Gravado" +
                                                     "\nSUBTOTAL=                      ₡"
                        + string.Format("{0:N2}",double.Parse(subtotal)) + "\nI.V.A.=                               ₡"
                            + string.Format("{0:N2}", double.Parse(impues)) + "\nDESCUENTO=                   ₡"
                        + 0
                                                   + "\nTOTAL=                             ₡"
                      + string.Format("{0:N2}",double.Parse(total)) +

                      "\n           ARTICULOS CON I.V.I." +
                      "\n           VENDEDOR : " + vendedor +
                      "\n----Autorizada mediante resolución--------\n---------N° DGT‐R‐48‐2016 del 07---------\n----------de octubre de 2016.---------------\n----------¡Gracias por su compra!--------------\n********Esperamos servirle de nuevo ************";


                    facturawr = new StreamWriter("reFactura.txt");
                    facturawr.WriteLine(formato, FileAccess.ReadWrite);
                    facturawr.Flush();
                    facturawr.Close();
                }//fin del if
                else
                {
                    MessageBox.Show("Faltan datos", "Faltan datos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                dataGridView1.DataSource = null;

            }//fin del try
            catch (Exception err_0016)
            {
                Mensaje.Error(err_0016, "1189");


            }

        }








        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("reFactura.txt");


                try
                {
                    printFont = new Font("Segoe UI", 10);
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler
                       (this.printDocument1_PrintPage);
                    pd.Print();
                }
                finally
                {
                    streamToPrint.Close();
                }
            }
            catch (Exception err_004)
            {
                Mensaje.Error(err_004, "955");


            }
        }



        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;


            float xPos = 0;

            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;





            string line = null;



            try
            {

                linesPerPage = e.MarginBounds.Height /
                   printFont.GetHeight(e.Graphics);


                while (count < linesPerPage &&
                   ((line = streamToPrint.ReadLine()) != null))
                {
                    yPos = (topMargin - 100) + (count *
                       printFont.GetHeight(e.Graphics));




                    e.Graphics.DrawString(line, printFont, Brushes.Black,
                    //leftMargin - 5, yPos, new StringFormat());
                    leftMargin - 80, yPos, new StringFormat());
                    count++;
                }


                if (line != null)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;

            }
            catch (Exception err_005)
            {
                Mensaje.Error(err_005, "1014");

            }


        }



    }
}
