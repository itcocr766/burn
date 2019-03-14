
using POS.Control;
using POS.Modelo;
using POS.Vista;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Configuration;
using POS.Contrasena;
using POS.clientesprincipal;
using POS.cedulas;
using POS.vendedoresprincipal_;
using Newtonsoft.Json;
using POS.clientes;
using POS.Inicio2;
using System.Runtime.InteropServices;
using POS.HACIENDA;
using POS.Anular;
using System.Globalization;
using POS.Pedido;
using Microsoft.CSharp.RuntimeBinder;
using System.Net;
using POS.Vuelto;
using POS.historialfacturas;

namespace POS
{
    public partial class Form1 : Form
    {
        logs log = new logs();
        public string elvuelto;
        string documentoelectronico;
        static Respuesta respuesta;
        string cadena1, cadena2;
        List<DETAIL> detalles;
        string json;
        long idFac;
        string clave;
        string consecutivo;
        public static string descuentomayor = "";
        string impostetot = "";
        string numeroRecibido = "";
        ConvertirANumeros canuj;
        ControlFactura c = new ControlFactura();
        StreamReader streamToPrint;
        Font printFont;
        string formato;
        string productos;
        buscarcedula rClient;
        ENVIO enviarfactura;
        public static string cedula = "";
        public static string nombrec = "";
        string len;
        StreamWriter facturawr;
        ControlFactura crfa = new ControlFactura();
        public static string cajero = "";
        string impuestoenespanol = "";
        decimal descuentos = 0;
        public static DataGridViewComboBoxCell combo;
        conexionabasedatos cndb = new conexionabasedatos();
        public static bool apartado;
        public static bool impri;
        public string consecutivoFE;
        decimal impuestofe;
        decimal unitprice;
        TAX losimpuestos;
        decimal totalamountfe;
        decimal discountfe;
        decimal subtotalfe;
        decimal totallineamountfe;
        decimal totaltaxedgoodsfe;
        decimal totaltaxedfe;
        decimal totalexcemptgoodsfe;
        decimal totalexcemptfe;
        decimal totalnetsalesfe;
        decimal totalsalesfe;
        decimal impuestototal;
        decimal cantited;
        decimal united;
        logs logos = new logs();
     
        
       
        public Form1()
        {
            InitializeComponent();

            try
            {

                rClient = new buscarcedula();
                canuj = new ConvertirANumeros();
                detalles = new List<DETAIL>();
                enviarfactura = new ENVIO();
                //respuesta = new Respuesta();
               
            }
            catch (Exception err_25)
            {
                Mensaje.Error(err_25, "61");

            }


        }

        #region consultarmetodo

        //public void metodo()
        //{
        //    try
        //    {
        //        using (var mysql = new Mysql())
        //        {
        //            mysql.conexion();
        //            mysql.cadenasql = "select Metodo from metodo";
        //            mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
        //            mysql.comando.ExecuteNonQuery();
        //            mysql.lector = mysql.comando.ExecuteReader();

        //            if (mysql.lector.Read())
        //            {
        //                if (mysql.lector["Metodo"].ToString() == "F")
        //                {
        //                    textBox8.Text = "F";
        //                }
        //                else
        //                {
        //                    textBox8.Text = "P";
        //                }

        //            }
        //            mysql.Dispose();
        //        }
        //    }
        //    catch (Exception ewf)
        //    {
        //        Mensaje.Error(ewf, "98");
              


        //    }

        //}




        #endregion
        #region load



        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {

                comboBox2.Focus();
               
                this.StartPosition = FormStartPosition.CenterScreen;
                comboBox1.SelectedIndex = 0;
                

                timer1.Interval = 10;
                timer1.Start();

                //comboBox1.SelectedIndex = 0;

                label7.Text = DateTime.Now.ToShortDateString();
                label8.Text = DateTime.Now.ToShortTimeString();


                comboBox2.DataSource = cargarVendedores();
                comboBox2.DisplayMember = "Codigo";
                comboBox2.ValueMember = "Codigo";

                comboBox3.DataSource = cargarClientes();
                comboBox3.DisplayMember = "Cedula";
                comboBox3.ValueMember = "Cedula";

                textBox1.DataSource = cargaitem();
                textBox1.DisplayMember = "Codigo";
                textBox1.ValueMember = "Codigo";





            }
            catch (Exception err_005)
            {
                Mensaje.Error(err_005, "168");


            }
        }

        #endregion
        #region meterfactura
        public async  void meterFactura()
        {

            string type = "";
            string tipoCed = "";
            try
            {

                impuestofe = 0;
                unitprice = 0;
                totalamountfe = 0;
                united = 0;
                cantited = 0;
                discountfe = 0;
                totallineamountfe = 0;
                subtotalfe = 0;
                totaltaxedgoodsfe = 0;
                totaltaxedfe = 0;
                totalexcemptfe = 0;
                totalexcemptgoodsfe = 0;
                totalnetsalesfe = 0;
                totalsalesfe = 0;
                impuestototal = 0;


                //if (!string.IsNullOrEmpty(comboBox4.Text))
                //{
                //    if (comboBox4.Text=="Física")
                //    {
                //        tipoCed = "01";
                //    }
                //    else if (comboBox4.Text=="Jurídica")
                //    {
                //        tipoCed = "02";
                //    }
                //    else
                //    {

                //        tipoCed = "03";
                //    }
                //}


                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                using (var mysql = new Mysql())
                {
                    mysql.conexion2();
                    mysql.cadenasql = "select Max(Numero) from factura where Numero like '"+textBox4.Text+"%'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();

                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            if (lee["Max(Numero)"].ToString() != "")
                            {
                                idFac = Int64.Parse(string.Concat(textBox4.Text, (Int64.Parse(lee["Max(Numero)"].ToString().Substring(1)) + 1).ToString()));
                            }
                            else
                            {
                                idFac = Int64.Parse(string.Concat(textBox4.Text, "1"));

                            }


                            //MessageBox.Show(numeroRecibido);

                        }




                    }


                    mysql.cadenasql = "select TipoCed from clientes where Cedula='"+comboBox3.Text+"'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader tip = mysql.comando.ExecuteReader())
                    {
                        while (tip.Read())
                        {
                            tipoCed = tip["TipoCed"].ToString();
                        }
                    }


                        /////////////////////////////////////////////////////////////////////////////


                        mysql.cadenasql = "insert into factura(Numero,Fecha,Cliente,Total,CodigoCajero,TipoPago,NumerodeComprobante,CodigoVendedor,Tipo)values('"
                            + idFac + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " "
                            + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "','" + comboBox3.Text.Trim() +
                            "','" + decimal.Parse(textBox5.Text.Trim()) + "','" + textBox4.Text + "','" + comboBox1.Text +
                            "','" + textBox7.Text.Trim() + "','" + comboBox2.Text + "','"+tipoCed+"')";

                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    for (int count = 0; count < dataGridView1.Rows.Count; count++)
                    {
                        mysql.cadenasql = "INSERT INTO `detalles`(`NumeroFactura`, `Cliente`, `Item`, `Cantidad`, `Precio`,`Impuesto`) VALUES ('" + idFac + "','" +
                            comboBox3.Text.Trim() + "','" + dataGridView1.Rows[count].Cells[0].Value + "','" +
                            dataGridView1.Rows[count].Cells[1].Value + "','" +
                            dataGridView1.Rows[count].Cells[3].Value + "','" + dataGridView1.Rows[count].Cells[9].Value + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();


                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////
                    mysql.cadenasql = "select Max(Numero) from factura where CodigoCajero='" + textBox4.Text + "' AND Cliente='" + comboBox3.Text.Trim() + "' AND Total='" + decimal.Parse(textBox5.Text.Trim()) + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();

                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            if (lee["Max(Numero)"].ToString() != "")
                            {
                                numeroRecibido = lee["Max(Numero)"].ToString();
                            }
                            else
                            {
                                numeroRecibido = "0";

                            }


                            //MessageBox.Show(numeroRecibido);

                        }

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    }

                    //MessageBox.Show("Factura guardada con éxito");
                    /////////////////////////////////////////////////////FEEEEEEEEEEEEEEEEE///////////////////////////////////////////////////
                    FE f = new FE()
                    {
                        
                        CompanyAPI = ConfigurationManager.AppSettings["companiapi"]

                    };

                    if (comboBox3.Text == "0")
                    {
                        type = "04";
                    }
                    else
                    {
                        type = "01";

                    }


                    f.Key = new KEY()
                    {
                        Branch = ConfigurationManager.AppSettings["sucursal"],
                        Terminal = "001",
                        Type = type,
                        Voucher = numeroRecibido,
                        Country = "506",
                        Situation = "1"

                    };

                    f.Header = new HEADER()
                    {
                        Date = DateTime.Now.Date,
                        TermOfSale = "01",
                        CreditTerm = 0,
                        PaymentMethod = "01"
                    };

                    if (comboBox3.Text.Substring(0) == "0")
                    {

                        f.Receiver = new RECEIVER()
                        {
                            Name = textBox17.Text,
                            Identification = new IDENTIFICATION
                            {
                                Type = "01",
                                Number = "000000000"

                            },
                            Email = textBox10.Text

                        };
                    }
                    else
                    {

                        f.Receiver = new RECEIVER()
                        {
                            Name = textBox17.Text,
                            Identification = new IDENTIFICATION
                            {
                                Type = tipoCed,
                                Number = comboBox3.Text

                            },
                            Email = textBox10.Text

                        };
                    }



                    detalles.Clear();
                    for (int r = 0; r < dataGridView1.Rows.Count; r++)
                    {
                        if (dataGridView1.Rows[r].Cells[9].Value.ToString() == "(G)")
                        {


                            cantited = decimal.Parse(dataGridView1.Rows[r].Cells[1].Value.ToString());
                            united = decimal.Parse(dataGridView1.Rows[r].Cells[3].Value.ToString());

                            unitprice = Math.Round((united / 1.13m), 2);
                            impuestofe = Math.Round(((united * cantited) - (unitprice * cantited)), 2);
                            totaltaxedgoodsfe += Math.Round(((unitprice * cantited)), 2);
                            totalsalesfe += Math.Round(((totaltaxedgoodsfe + totalexcemptgoodsfe)), 2);
                            totalnetsalesfe += Math.Round(((totalsalesfe - discountfe)), 2);
                            totaltaxedfe += Math.Round(((unitprice * cantited)), 2);
                            impuestototal += Math.Round(impuestofe, 2);




                            losimpuestos = new TAX()
                            {
                                Code = "01",
                                Rate = 13.0m,
                                Amount = Math.Round(impuestofe, 2),
                                Exoneration = null
                            };

                            totalamountfe = Math.Round(((unitprice * cantited)), 2);
                            discountfe = 0;
                            subtotalfe = Math.Round(((totalamountfe - discountfe)), 2);
                            totallineamountfe = Math.Round((subtotalfe + impuestofe), 2);






                            DETAIL detail = new DETAIL()
                            {
                                Number = r + 1,
                                Code = new CODE
                                {
                                    Type = "04",
                                    Code = dataGridView1.Rows[r].Cells[0].Value.ToString()
                                },
                                Tax = new List<TAX>()
                            {
                                losimpuestos
                            },

                                Quantity = decimal.Parse(dataGridView1.Rows[r].Cells[1].Value.ToString()),
                                UnitOfMeasure = "kg",
                                CommercialUnitOfMeasure = null,
                                Detail = dataGridView1.Rows[r].Cells[2].Value.ToString(),
                                UnitPrice = unitprice,
                                TotalAmount = totalamountfe,
                                Discount = discountfe,
                                NatureOfDiscount = "",
                                SubTotal = subtotalfe,
                                TotalLineAmount = totallineamountfe

                            };
                            detalles.Add(detail);
                        }
                        else
                        {
                            cantited = decimal.Parse(dataGridView1.Rows[r].Cells[1].Value.ToString());
                            united = decimal.Parse(dataGridView1.Rows[r].Cells[3].Value.ToString());

                            impuestofe = 0;

                            unitprice = united;

                            totaltaxedgoodsfe += 0;


                            totaltaxedfe += 0;
                            totalamountfe = Math.Round(((unitprice * cantited)), 2);
                            discountfe = 0;
                            totalexcemptgoodsfe += Math.Round(((unitprice * cantited)), 2);
                            totalexcemptfe += Math.Round(((unitprice * cantited)), 2);
                            totalsalesfe += Math.Round(((totaltaxedgoodsfe + totalexcemptgoodsfe)), 2);
                            totalnetsalesfe += Math.Round(((totalsalesfe - discountfe)), 2);


                            subtotalfe = Math.Round(((totalamountfe - discountfe)), 2);
                            totallineamountfe = Math.Round((subtotalfe + impuestofe), 2);



                            DETAIL detail = new DETAIL()
                            {
                                Number = r + 1,
                                Code = new CODE
                                {
                                    Type = "04",
                                    Code = dataGridView1.Rows[r].Cells[0].Value.ToString()
                                },


                                Quantity = decimal.Parse(dataGridView1.Rows[r].Cells[1].Value.ToString()),
                                UnitOfMeasure = "kg",
                                CommercialUnitOfMeasure = null,
                                Detail = dataGridView1.Rows[r].Cells[2].Value.ToString(),
                                UnitPrice = unitprice,
                                TotalAmount = totalamountfe,
                                Discount = discountfe,
                                NatureOfDiscount = "",
                                SubTotal = subtotalfe,
                                TotalLineAmount = totallineamountfe

                            };
                            detalles.Add(detail);
                        }






                    }
                    f.Detail = null;


                    f.Detail = detalles;


                    //f.Reference=new List<REFERENCE>()
                    //{

                    //};



                    f.Summary = new SUMMARY()
                    {
                        Currency = "CRC",
                        ExchangeRate = 1,
                        TotalTaxedService = 0,
                        TotalExemptService = 0,
                        TotalTaxedGoods = Math.Round(totaltaxedgoodsfe, 2),
                        TotalExemptGoods = Math.Round(totalexcemptgoodsfe, 2),
                        TotalTaxed = Math.Round(totaltaxedfe, 2),
                        TotalExempt = Math.Round(totalexcemptfe, 2),
                        TotalSale = Math.Round((totaltaxedgoodsfe + totalexcemptgoodsfe), 2),
                        TotalDiscounts = 0,
                        TotalNetSale = Math.Round((totaltaxedgoodsfe + totalexcemptgoodsfe), 2),
                        TotalTaxes = Math.Round(impuestototal, 2),
                        TotalVoucher = Math.Round((totaltaxedgoodsfe + totalexcemptgoodsfe + impuestototal), 2)
                    };

                    json = JsonConvert.SerializeObject(f, Newtonsoft.Json.Formatting.Indented);


                    string strJSON = string.Empty;

                    await log.guardarlog("Se realizo una factura:  por el usuario: " + facturando.Text, "" + DateTime.Now.ToString("yyyy-MM-dd") + "", "" + DateTime.Now.ToString("HH:mm:ss tt") + "", "Transaccion", textBox5.Text);
                    SendInvoicesAGC(f);

                    if (!string.IsNullOrEmpty(respuesta.codificacion.clave))
                    {
                        clave = respuesta.codificacion.clave;
                        consecutivo = respuesta.codificacion.consecutivo;
                        mysql.cadenasql = "INSERT INTO `hacienda`(`Clave`, `Consecutivo`, `Comprobante`) VALUES ('" + clave + "','" + consecutivo + "','" + numeroRecibido + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();

                        mysql.rol();
                        mysql.Dispose();
                        impri = true;
                    }
                    else
                    {
                        impri = false;
                        mysql.Dispose();

                        MessageBox.Show("No pudimos comunicarnos con el Ministerio de hacienda por favor verifique que la factura :" + numeroRecibido.ToString() +
                            "no haya sido almacenada en la base de datos interna y anulela de ser necesario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }





                    textBox1.Visible = true;

                    ///////////////////////////////////////////FEEEEEEEEEEEEEEEEEEE////////////////////////////////////////////////////////////////////////


                }

               //await log.guardarlog("Se realizo una factura:  por el usuario: " + facturando.Text, "" + DateTime.Now.ToString("yyyy-MM-dd") + "", "" + DateTime.Now.ToString("HH:mm:ss tt") + "", "Transaccion", textBox5.Text);

            }
            catch (Exception ftr)
            {
                //Mensaje.Error(ftr, "191");
                MessageBox.Show(ftr.ToString());

                impri = false;

            }




        }




        public static Respuesta SendInvoicesAGC(FE factura)

        {




            string urlAPI = ConfigurationManager.AppSettings["endpoint"];

            string api = urlAPI + "/api/invoice";



            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlAPI);



            httpWebRequest.ContentType = "application/json";

            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 5000;


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))

            {

                string json = JsonConvert.SerializeObject(factura);



                //Logs.SaveJson(json, company, factura.numero);



                streamWriter.WriteAsync(json);

            }



            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))

            {

                var result = streamReader.ReadToEnd();



                //Logs.SaveAnswer(result, company,factura.numero);



                respuesta = JsonConvert.DeserializeObject<Respuesta>(result);

                //MessageBox.Show(respuesta.codificacion.clave+"...."+respuesta.codificacion.consecutivo);
                return respuesta;

            }

        }


        #endregion
        #region metersale
        public void metersale()
        {


            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion2();
                    mysql.cadenasql = "select Max(Numero) from sales where Numero like '"+textBox4.Text+"%'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();


                    

                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            if (lee.Read())
                            {
                                if (lee["Max(Numero)"].ToString() != "")
                                {
                                    idFac = Int64.Parse(string.Concat(textBox4.Text, (Int64.Parse(lee["Max(Numero)"].ToString().Substring(1)) + 1).ToString()));
                                }
                                else
                                {
                                    idFac = Int64.Parse(string.Concat(textBox4.Text, "1"));

                                }


                                //MessageBox.Show(numeroRecibido);

                            }




                        }








                        /////////////////////////////////////////////////////////////////////////////


                        mysql.cadenasql = "insert into sales(Numero,Fecha,Cliente,Total,CodigoCajero,TipoPago,NumerodeComprobante,CodigoVendedor,Tipo)values('"
                       + idFac + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " "
                       + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "','" + comboBox3.Text.Trim() +
                       "','" + decimal.Parse(textBox5.Text.Trim()) + "','" + textBox4.Text + "','" + comboBox1.Text +
                       "','" + textBox7.Text.Trim() + "','" + comboBox2.Text + "','P')";

                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    for (int count = 0; count < dataGridView1.Rows.Count; count++)
                    {
                        mysql.cadenasql = "INSERT INTO `detallessales`(`NumeroFactura`, `Cliente`, `Item`, `Cantidad`, `Precio`,`Impuesto`) VALUES ('" + idFac + "','" +
                            comboBox3.Text.Trim() + "','" + dataGridView1.Rows[count].Cells[0].Value + "','" +
                            dataGridView1.Rows[count].Cells[1].Value + "','" +
                            dataGridView1.Rows[count].Cells[3].Value + "','" + dataGridView1.Rows[count].Cells[9].Value + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();



             
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////
                    mysql.cadenasql = "select Max(Numero) from sales where CodigoCajero='" + textBox4.Text + "' AND Cliente='" + comboBox3.Text.Trim() + "' AND Total='" + decimal.Parse(textBox5.Text.Trim()) + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();

                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            if (lee["Max(Numero)"].ToString() != "")
                            {
                                numeroRecibido = lee["Max(Numero)"].ToString();
                            }
                            else
                            {
                                numeroRecibido = "0";

                            }




                        }

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                    }
                    mysql.rol();
                    mysql.Dispose();

                    impri = true;
                    textBox1.Visible = true;
                    //MessageBox.Show("Factura guardada con éxito");
                }



            }
            catch (Exception ftr)
            {
                Mensaje.Error(ftr, "191");


                impri = false;

            }


        }


        #endregion
        #region metodo llenar
        public void llenar(string codigo)
        {


            decimal cantidad = 0;
            decimal precio = 0;
            decimal presespe = 0;
            decimal totalsinimpuesto = 0;
            decimal totalconimpuesto = 0;
            decimal total = 0;
            decimal impuestounidad = 0;
            decimal exentounidad = 0;
            bool existe = false;
            bool presesp = false;
            bool impos = false;
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    mysql.cadenasql = "select * from especiales where Item='" + codigo + "' and Cliente='" + comboBox3.Text + "'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    mdaDatos.Fill(dtDatos);
                    if (dtDatos.Rows.Count > 0)
                    {
                        using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                        {
                            while (lee.Read())
                            {
                                presespe = decimal.Parse(lee["Precio"].ToString());
                                presesp = true;
                            }

                        }
                    }
                    else
                    {
                        presesp = false;
                    }
                    mysql.Dispose();
                }




                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select * from items where Codigo='" + codigo + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();


                    using (MySqlDataReader lec = mysql.comando.ExecuteReader())
                    {
                        if(lec.Read())
                        {


                            if (lec["Impuesto"].ToString() == "Impuesto")
                            {
                                impos = true;
                            }
                            else
                            {
                                impos = false;
                            }

                            for (int y = 0; y < dataGridView1.Rows.Count; y++)
                            {



                                if (textBox1.Text.Trim() == dataGridView1.Rows[y].Cells[0].Value.ToString())
                                {

                                    existe = true;


                                }






                            }


                            if (existe == true)
                            {
                                for (int y = 0; y < dataGridView1.Rows.Count; y++)
                                {
                                    if (dataGridView1.Rows[y].Cells[0].Value.ToString() == textBox1.Text.Trim())
                                    {
                                        dataGridView1.Rows[y].Cells[1].Value = decimal.Parse(dataGridView1.Rows[y].Cells[1].Value.ToString()) + decimal.Parse(textBox2.Text.Trim());

                                        dataGridView1.Rows[y].Cells[7].Value = (decimal.Parse(dataGridView1.Rows[y].Cells[6].Value.ToString()) * decimal.Parse(dataGridView1.Rows[y].Cells[1].Value.ToString()));
                                        if (impos)
                                        {
                                            dataGridView1.Rows[y].Cells[8].Value = (decimal.Parse(dataGridView1.Rows[y].Cells[3].Value.ToString()) - (decimal.Parse(dataGridView1.Rows[y].Cells[3].Value.ToString()) / 1.13m)) * decimal.Parse(dataGridView1.Rows[y].Cells[1].Value.ToString());
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[y].Cells[8].Value = decimal.Parse(dataGridView1.Rows[y].Cells[3].Value.ToString()) * decimal.Parse(dataGridView1.Rows[y].Cells[1].Value.ToString());

                                        }
                                        //dataGridView1.Rows[y].Cells[10].Value =(double.Parse(dataGridView1.Rows[y].Cells[10].Value.ToString())*Int32.Parse(dataGridView1.Rows[y].Cells[1].Value.ToString()));

                                        agregarFila();

                                    }




                                }

                            }

                            else if (existe == false)
                            {
                                string totsin = "";
                                if (impos)
                                {
                                    if (presesp)
                                    {
                                        precio = presespe;
                                        totsin = string.Format("{0:N3}", (precio / 1.13m));
                                        totalsinimpuesto = decimal.Parse(totsin);
                                        cantidad = decimal.Parse(textBox2.Text.Trim());
                                        impuestounidad = (precio - (precio / 1.13m)) * cantidad;
                                        impuestoenespanol = "(G)";

                                        totalconimpuesto = precio;
                                        total = totalconimpuesto * cantidad;
                                        dataGridView1.Rows.Add(lec["Codigo"], cantidad, lec["Nombre"], precio, canuj.enletras(precio.ToString()), totalsinimpuesto, totalconimpuesto, (totalconimpuesto * cantidad), impuestounidad, impuestoenespanol);
                                        agregarFila();
                                    }
                                    else
                                    {

                                        precio = decimal.Parse(lec["Precio"].ToString());
                                        totsin = string.Format("{0:N3}", (precio / 1.13m));
                                        totalsinimpuesto = decimal.Parse(totsin);
                                        cantidad = decimal.Parse(textBox2.Text.Trim());
                                        impuestounidad = (precio - (precio / 1.13m)) * cantidad;
                                        impuestoenespanol = "(G)";

                                        totalconimpuesto = precio;
                                        total = totalconimpuesto * cantidad;
                                        dataGridView1.Rows.Add(lec["Codigo"], cantidad, lec["Nombre"], precio, canuj.enletras(precio.ToString()), totalsinimpuesto, totalconimpuesto, (totalconimpuesto * cantidad), impuestounidad, impuestoenespanol);
                                        agregarFila();
                                    }

                                }
                                else if (impos == false)
                                {

                                    if (presesp)
                                    {

                                        precio = presespe;
                                        totalsinimpuesto = precio;
                                        cantidad = decimal.Parse(textBox2.Text.Trim());
                                        exentounidad = precio * cantidad;
                                        impuestoenespanol = "(E)";
                                        totalconimpuesto = precio;
                                        total = totalconimpuesto * cantidad;
                                        dataGridView1.Rows.Add(lec["Codigo"], cantidad, lec["Nombre"], precio, canuj.enletras(precio.ToString()), totalsinimpuesto, totalconimpuesto, (totalconimpuesto * cantidad), exentounidad, impuestoenespanol);
                                        agregarFila();
                                    }
                                    else
                                    {
                                        precio = decimal.Parse(lec["Precio"].ToString());
                                        totalsinimpuesto = precio;
                                        cantidad = decimal.Parse(textBox2.Text.Trim());
                                        exentounidad = precio * cantidad;
                                        impuestoenespanol = "(E)";
                                        totalconimpuesto = precio;
                                        total = totalconimpuesto * cantidad;
                                        dataGridView1.Rows.Add(lec["Codigo"], cantidad, lec["Nombre"], precio, canuj.enletras(precio.ToString()), totalsinimpuesto, totalconimpuesto, (totalconimpuesto * cantidad), exentounidad, impuestoenespanol);
                                        agregarFila();
                                    }
                                }
                               



                            }
                            else
                            {

                                MessageBox.Show("Ya No hay suficiente inventario para este item");

                            }



                            textBox1.Focus();
                        }


                        else
                        {

                            MessageBox.Show("No hemos podido encontrar este producto,verifique que sea un producto existente");
                            textBox1.Text = "";
                            textBox1.Focus();
                        }


                    }

                    mysql.Dispose();

                }

            


                   

                    

                
                   


            }
            catch (Exception err_006)
            {
                Mensaje.Error(err_006, "416");

              
                   
            }


        }


        #endregion
    #region calcular el total
        public void calcularTotal()
        {

            decimal totalt = 0;
            decimal totalimp = 0;
            descuentos = 0;
           // MessageBox.Show(totalt.ToString());
            try
            {
                for (int restadescuento=0;restadescuento<dataGridView1.Rows.Count;restadescuento++)
                {

                    totalt += decimal.Parse(dataGridView1.Rows[restadescuento].Cells[7].Value.ToString());
                    
                    

                    if (dataGridView1.Rows[restadescuento].Cells[9].Value.ToString()=="(G)")
                    {

                        totalimp += decimal.Parse(dataGridView1.Rows[restadescuento].Cells[7].Value.ToString());
                    }
                    
                }



                textBox5.Text = string.Format("{0:N2}",(totalt));
                textBox6.Text = string.Format("{0:N2}", totalimp-(totalimp / 1.13m)); 


            }
            catch (Exception err_0010)
            {
                Mensaje.Error(err_0010, "467");
              
                if (dataGridView1.Rows.Count >0)
                {
                    textBox5.Text = string.Format("{0:N2}", (totalt ));
                    textBox6.Text = string.Format("{0:N2}", totalimp-(totalimp / 1.13m));
                }
                else
                {
                    textBox5.Text = "0";
                    textBox6.Text = "0";
                }
                

            }

        }
        #endregion
       #region agregar el producto a la lista
        public void agregarFila()//metodo que se ejecuta cuando se agrega una fila
        {
            try
            {

                if (dataGridView1.Rows[0].Cells[0].Value != null)
                {
                    decimal sumad = 0;
                    decimal monto = 0;
                    decimal cantidadaf = 0;


                    for (int t = 0; t < dataGridView1.Rows.Count; t++)
                    {

                        monto = decimal.Parse(dataGridView1.Rows[t].Cells[5].Value.ToString());
                        cantidadaf = decimal.Parse(dataGridView1.Rows[t].Cells[1].Value.ToString());


                    

                        sumad += (monto * cantidadaf);
                   


                }
                if (sumad > 0)
                {
                    textBox3.Text = string.Format("{0:N2}",sumad);
                       
                        calcularTotal();
                }
                    else
                    {

                    textBox3.Text = "0";
                   
                    textBox5.Text = "0";
                   
                    calcularTotal();
                }



            }
                    else
                    {


                textBox3.Text = "0";
               
                textBox5.Text = "0";
            }

            }
            catch (Exception err_008)
            {

                Mensaje.Error(err_008, "553");
              


                textBox3.Text = "0";
               
                textBox5.Text = "0";


            }
        }
        #endregion
       
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                elvuelto = "";
              



                impri = false;

              
                if (comboBox1.Text != "" && comboBox3.Text.Trim() != "" && textBox17.Text != "" && textBox16.Text != "" && dataGridView1.Rows.Count > 0)
                {



                   
                    meterFactura();
                    formatodeactura();

                    if (impri)
                    {


                        for (int imp = 0; imp < Int32.Parse(numericUpDown1.Value.ToString()); imp++)
                        {
                            imprimir();

                        }

                        elvuelto = textBox15.Text;
                        comboBox2.Enabled = true;
                        comboBox3.Enabled = true;
                        limpiar();
                        vueltoenpantalla vp = new vueltoenpantalla(this);
                        vp.ShowDialog();
                    }
                    else
                    {

                        MessageBox.Show("Por favor vuelva  a intentar imprimir la factura", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                }
                else
                {
                    Mensaje_Warning("Parece que no todos los datos estan correctos. \nPor favor verifique que el tipo de pago y toda la informacion este correcta");
                }

               
                timer3.Stop();
                textBox15.BackColor = Color.WhiteSmoke;
               
            }
            catch (Exception err_0018)
            {


                MessageBox.Show("Hubo una colisión en la base de datos y se bloqueo el acceso para evitar duplicados. Por favor intente de nuevo "+err_0018.ToString());

                textBox15.BackColor = Color.WhiteSmoke;
              
                timer3.Stop();

            }
        }

       
      
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
            try
            {
                eventos(e);
                textBox14.Text = "0";
                textBox15.Text = "0";
                button3.Enabled = false;
            }
            catch (Exception err_0011)
            {
                Mensaje.Error(err_0011, "716");

               
                    
            }
        }
      

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                agregarFila();

            }
            catch (Exception err_21)
            {
                Mensaje.Error(err_21, "733");
               
            }
        }
     
  
        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            try
            {

                if (dataGridView1.Rows.Count>0)
                {

                    agregarFila();

                }
                else
                {

                    textBox3.Text = "0";
                   
                    textBox5.Text = "0";
                  
                }
              



            }
            catch (Exception err_0013)
            {
                Mensaje.Error(err_0013, "765");
               

            }
        }
       
        
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


     
           
          


        }

 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F7)
            {
                clies cli = new clies(this);
                cli.Show(this);

            }else if (e.KeyCode == Keys.F3)
            {
                if (comboBox1.Text== "Efectivo")
                {
                    comboBox1.Text = "Tarjeta";
                }
                else
                {

                    comboBox1.Text = "Efectivo";
                }

            }
            else if (e.KeyCode == Keys.F6)
            {

                productos pro = new productos(this);
                pro.Show(this);
            }
            else if (e.KeyCode == Keys.F9)
            {

                textBox14.Focus();
            }
            else if (e.KeyCode == Keys.F7)
            {
                clies cl = new clies(this);
                cl.Show(this);

            }
            else if (e.KeyCode == Keys.F8)
            {

                vendedores vd = new vendedores(this);
                vd.Show(this);
            }
            else if (e.KeyCode == Keys.F5)
            {

                inicio2 in2 = new inicio2();
                in2.Show(this);
            }
            else if (e.KeyCode == Keys.F10)
            {
                button3.PerformClick();
               
            }
            else if (e.KeyCode==Keys.F12)
            {
                //if (textBox8.Text=="F")
                //{
                //    textBox8.Text = "P";
                //    MessageBox.Show("Se actualizó el método a P correctamente","Solicitud procesada correctamente",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //}
                //else
                //{
                //    textBox8.Text = "F";
                //    MessageBox.Show("Se actualizó el método a F correctamente", "Solicitud procesada correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
            }
           

        }
      
        
        #region metodos impresion

        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("Factura.txt");

                
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


        public void imprimir2()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("pedi.txt");


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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                if (e.CloseReason==CloseReason.UserClosing)
                    e.Cancel = true;

            }
            catch (Exception err)
            {
                Mensaje.Error(err, "1031");
              
            }
         
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            
           

        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
          
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick_2(object sender, EventArgs e)
        {
          
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           
        }

        public void formatodeactura()
        {

            productos = "";
            formato = "";
            len = "";
            try
            {

               




                for (int da = 0; da < dataGridView1.Rows.Count; da++)
                {
                    len = dataGridView1.Rows[da].Cells[2].Value.ToString().Trim();
                  

                   

                        productos += "\n  " + dataGridView1.Rows[da].Cells[1].Value.ToString() +
                          "        ₡" +
                       string.Format("{0:N2}",double.Parse(dataGridView1.Rows[da].Cells[7].Value.ToString()))
                       + "\n          " + dataGridView1.Rows[da].Cells[2].Value.ToString() ;

                    
                

                }

                string direccion = "";
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select Direccion from clientes where Cedula='"+comboBox3.Text+"'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql,mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee=mysql.comando.ExecuteReader())
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
                }

                documentoelectronico = "";
                if (comboBox3.Text=="0")
                {
                    documentoelectronico = "Tiquete Electrónico";
                }
                else
                {
                    documentoelectronico = "Factura Electrónica";
                }


            
                    formato = "Razón social:\nCUBIRAMI SOCIEDAD ANÓNIMA\nCed.Jurídica:3101125675\nCorreo:lanuevaunion@hotmail.com\nLugar:San José\nTeléfono:2224-7042" +
                      "\n    Fecha:     " + DateTime.Now.ToString() + "\n" +
                       "                      No.Factura:\n" + "             " + consecutivo + "\n" +
                      "                        Clave:\n" + clave.Substring(0, 25) + "\n" +
                      clave.Substring(25) + "\n" +
                      "                Comprobante: " + numeroRecibido + "\n" +
                      "                Tipo de documento:\n" + documentoelectronico + "\n" +
                      "        Tipo de pago : " + comboBox1.SelectedItem.ToString() + "\n          Facturado por: " +
                      facturando.Text +
                      "\n              Cliente:\n" + textBox17.Text.Trim() + "\n" +
                      "        Dirección:\n " + direccion + "\n" +
                   "\nCantidad        Precio        \n" + "Artículo\n" +
                  "----------------------------------------------------------" + productos +
                  "\n         (E)=Exento              (G)=Gravado" +
                                                 "\nARTICULOS=                      "
                    + dataGridView1.Rows.Count + "\nSUBTOTAL=                      ₡"
                    + textBox3.Text.Trim() + "\nI.V.A.=                               ₡"
                        + textBox6.Text.Trim() + "\nDESCUENTO=                   ₡"
                    + string.Format("{0:N3}", descuentos)
                                               + "\nTOTAL=                             ₡"
                  + textBox5.Text.Trim() +
                  "\n------------------------------MONTO-------------" +
                  "\nPAGA CON:                        ₡"
                  + string.Format("{0:n0}", double.Parse(textBox14.Text.Trim())) +
                  "\nVUELTO=                           ₡"
                  + textBox15.Text.Trim() +
                  "\n           ARTICULOS CON I.V.I." +
                  "\n           VENDEDOR : " + textBox16.Text.Trim() +
                  "\n----Autorizada mediante resolución--------\n---------N° DGT‐R‐48‐2016 del 07---------\n----------de octubre de 2016.---------------\n----------¡Gracias por su compra!--------------\n********Esperamos servirle de nuevo ************";
                

                facturawr = new StreamWriter("Factura.txt");
                facturawr.WriteLine(formato,FileAccess.ReadWrite);
                facturawr.Flush();
                facturawr.Close();






            }
            catch (Exception err_0016)
            {
                Mensaje.Error(err_0016, "1189");
               

            }

        }








        #endregion
        #region consifguracion de mensajes
        public void Mensaje_Warning(string m)
        {
            MessageBox.Show(m,"Mensaje de error",MessageBoxButtons.OK,MessageBoxIcon.Warning);


        }
        public void Mensaje_Error(string m)
        {
            MessageBox.Show(m, "Mensaje de error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        #endregion
        #region limpiar

        public void limpiar()
        {
       

            try
            {


                while (dataGridView1.Rows.Count>=1)
                {

                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);
                }

                 
                    textBox2.Text = "0.1";
                    textBox3.Text = "0";
                textBox6.Text = "0";
                    textBox5.Text = "0";
                textBox14.Text = "0";
                textBox15.Text = "0";
                
                textBox3.Text = "0";
                descuentos = 0;
                //button3.Enabled = false;
                
                textBox1.Visible = true;
                //textBox2.Visible = true;
                textBox1.Focus();
                timer2.Stop();
                timer3.Stop();
                textBox1.Text = "";
                comboBox3.Text = "";
                textBox17.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                comboBox2.Text = "";
                textBox16.Text = "";
                dataGridView1.Enabled = true;
                numericUpDown1.Value = 1;
                textBox15.BackColor = Color.WhiteSmoke;
                textBox15.ForeColor = Color.White;
                textBox7.Text = "0";
                comboBox1.SelectedIndex = -1;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox2.Focus();
                totaltaxedgoodsfe = 0;
                totaltaxedfe = 0;
                totalexcemptgoodsfe = 0;
                totalexcemptfe = 0;
                button3.Visible = false;
             
                label11.Visible = false;
            
                textBox14.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox2.Focus();
            }
            catch (Exception err_20)
            {
                Mensaje.Error(err_20, "1419");
               
            }


        }
        #endregion




        public void formatodeacturapedido(string ped)
        {
            
            productos = "";
            formato = "";
            len = "";
            try
            {






                for (int da = 0; da < dataGridView1.Rows.Count; da++)
                {
                    len = dataGridView1.Rows[da].Cells[2].Value.ToString().Trim();


                    productos += "\n  " + dataGridView1.Rows[da].Cells[1].Value.ToString() +
                      "        ₡" +
                   string.Format("{0:N2}", double.Parse(dataGridView1.Rows[da].Cells[7].Value.ToString()))
                   + "\n          " + dataGridView1.Rows[da].Cells[2].Value.ToString();


                  

                }

                string direccion = "";
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select Direccion from clientes where Cedula='" + comboBox3.Text + "'";
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
                }

               



                formato = "Razón social:\nCUBIRAMI SOCIEDAD ANÓNIMA\nCed.Jurídica:3101125675\nCorreo:lanuevaunion@hotmail.com\nLugar:San José\nTeléfono:2224-7042" +
                  "\n    Fecha:     " + DateTime.Now.ToString() + "\n" +
                 
                  "                Número de Pedido: " + ped + "\n" +
                  "        Tipo de pago : " + comboBox1.SelectedItem.ToString() + "\n          Facturado por: " +
                  facturando.Text +
                  "\n              Cliente:\n" + textBox17.Text.Trim() + "\n" +
                  "        Dirección:\n " + direccion + "\n" +
               "\nCantidad        Precio        \n" + "Artículo\n" +
              "----------------------------------------------------------" + productos +
              "\n         (E)=Exento              (G)=Gravado" +
                                             "\nARTICULOS=                      "
                + dataGridView1.Rows.Count + "\nSUBTOTAL=                      ₡"
                + textBox3.Text.Trim() + "\nI.V.A.=                               ₡"
                    + textBox6.Text.Trim() + "\nDESCUENTO=                   ₡"
                + string.Format("{0:N3}", descuentos)
                                           + "\nTOTAL=                             ₡"
              + textBox5.Text.Trim() +
              "\n------------------------------MONTO-------------" +
              "\nPAGA CON:                        ₡"
              + string.Format("{0:n0}", double.Parse(textBox14.Text.Trim())) +
              "\nVUELTO=                           ₡"
              + textBox15.Text.Trim() +
              "\n           ARTICULOS CON I.V.I." +
              "\n           VENDEDOR : " + textBox16.Text.Trim() +
              "\n----Autorizada mediante resolución--------\n---------N° DGT‐R‐48‐2016 del 07---------\n----------de octubre de 2016.---------------\n----------¡Gracias por su compra!--------------\n********Esperamos servirle de nuevo ************";


                facturawr = new StreamWriter("pedi.txt");
                facturawr.WriteLine(formato, FileAccess.ReadWrite);
                facturawr.Flush();
                facturawr.Close();






            }
            catch (Exception err_0016)
            {
                Mensaje.Error(err_0016, "1189");


            }

        }





        public void eventos(KeyEventArgs er)
        {
            cadena1 = "";
            cadena2 = "";
            string can = "";
            textBox2.Text = "";
            if (er.KeyCode==Keys.Enter)
            {
                
                
                if (textBox1.Text.Length>12)
                {
                    cadena1 = textBox1.Text.Substring(0, 7);
                    cadena2 = textBox1.Text.Substring(7, 5);

                    textBox11.Text = cadena1;
                    textBox12.Text = cadena2.Substring(0,2);
                    textBox13.Text = cadena2.Substring(2,3);
                 
                    if (Int64.Parse(cadena2)>0)
                    {
                        can = cadena2.Insert(2, ".");
                        //MessageBox.Show(cadena1 + "  " + cadena2 + "  " + textBox1.Text.Length);
                        textBox2.Text = can;
                        //MessageBox.Show(cadena1);
                        //llenar(cadena1);
                        llenar(cadena1);


                        textBox14.BackColor = Color.FromArgb(192, 255, 192);
                        timer2.Interval = 100;
                        timer2.Start();
                        if (dataGridView1.Rows.Count>0)
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count-1;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("cadena de entrada no valida");
                    }


                }
                else
                {
                    //MessageBox.Show("cadena de entrada no valida");
                }



            }
            else if(er.KeyCode==Keys.F9)
            {
                
              
                timer2.Stop();
                textBox14.Visible = true;
                textBox14.Focus();
            }
          
            else if (er.KeyCode == Keys.F2)
            {
                
            }





        }
     
        #region llenar combos
        public DataTable cargarVendedores()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql=new Mysql())
                {

                    mysql.conexion();

                    string query = "SELECT * FROM vendedores";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();
                }
                  
            }
            catch (Exception ekk)
            {

                Mensaje.Error(ekk, "1482");
               
                   
            }
        
            return dt;
        }

        public DataTable cargaritems()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();

                    string query = "SELECT * FROM items";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();

                }
                   

            }
            catch (Exception   jskjd)
            {
                Mensaje.Error(jskjd, "1509");
             
                    

            }
          
            return dt;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
       
           
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
           
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        public DataTable cargarClientes()
        {
            DataTable dt = new DataTable();
            try
            {

                using (var mysql=new Mysql())
                {
                    mysql.conexion();

                    string query = "SELECT * FROM clientes";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();


                }
                    
            }
            catch (Exception klsk)
            {

                Mensaje.Error(klsk, "1569");
               
                    
            }
          
            return dt;
        }
        public DataTable cargarDescuentos()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();

                    string query = "SELECT * FROM descuentos";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();

                }
                    

            }
            catch (Exception lsksk)
            {

                Mensaje.Error(lsksk, "1602");
               
                  
            }
           
            return dt;



        }
        public DataTable cargaitem()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql=new Mysql())
                {

                    mysql.conexion();

                    string query = "SELECT * FROM items";
                    MySqlCommand cmd = new MySqlCommand(query, mysql.con);
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    adap.Fill(dt);
                    mysql.Dispose();

                }

                   

            }
            catch (Exception lop)
            {
                Mensaje.Error(lop, "1632");
              
                    


            }
          
            return dt;



        }



        private void comboBox4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          


        }

        private void comboBox4_KeyDown_1(object sender, KeyEventArgs e)
        {


         
        }




        #endregion
      
        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
           
          
            
          
        }
       
        
     
        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (dataGridView1.DataSource!=null&&e.KeyCode == Keys.Enter&& Int64.Parse(dataGridView1.CurrentRow.Cells[5].Value.ToString()) <= 50&&
                    dataGridView1.CurrentCell==dataGridView1.CurrentRow.Cells[5])
                {

                  
                    calcularTotal();
                  

                }
                //else if (e.KeyCode == Keys.Delete)
                //{


                //}
                //else if(dataGridView1.CurrentCell != dataGridView1.CurrentRow.Cells[5]&& dataGridView1.DataSource != null)
                //{

                    
                //}
                //else if (dataGridView1.DataSource==null)
                //{

                //}
                //else
                //{

                //    MessageBox.Show("El valor digitado no puede ser mayor a 50");
                //}

            }
            catch (Exception lope)
            {
                Mensaje.Error(lope, "1888");
               

            }

           
         
        }
     
       
        private void textBox14_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {

                //textBox14.Text = string.Format("{0:N0}", double.Parse(textBox14.Text.Trim()));

                if (e.KeyCode == Keys.Enter)
                {
                    textBox1.Visible = false;
                    textBox2.Visible = false;

                    if (decimal.Parse(textBox14.Text.Trim()) >= decimal.Parse(textBox5.Text) && decimal.Parse(textBox14.Text.Trim()) > 0)
                    {
                        button3.Visible = true;
                        button3.Enabled = true;
                        button6.Visible = true;
                        label24.Visible = true;
                        label11.Visible = true;
                        textBox15.BackColor = Color.FromArgb(255, 128, 128);
                        textBox15.ForeColor = Color.White;

                        timer3.Interval = 100;
                        timer3.Start();
                        textBox15.Text = string.Format("{0:N3}", (decimal.Parse(textBox5.Text.Trim()) - decimal.Parse(textBox14.Text.Trim())));
                        //textBox1.Focus();

                    }
                    else
                    {
                        textBox15.BackColor = Color.WhiteSmoke; textBox15.ForeColor = Color.White;
                        MessageBox.Show("El monto recibido debe ser mayor o igual a el monto total");
                        timer3.Interval = 1000;
                        timer3.Start();
                    }

                    textBox14.Focus();


                }
                else if (e.KeyCode == Keys.F3)
                {



                    textBox7.Focus();






                }


                
            }
            catch (Exception textBox14_KeyDown)
            {

                Mensaje.Error(textBox14_KeyDown, "2022");
                timer3.Stop();
                textBox15.BackColor = Color.WhiteSmoke; textBox15.ForeColor = Color.White;
            }
          
        }
     
 
        private void textBox1_KeyDown_1(object sender, KeyEventArgs e)
        {
           
        }
       
        #region debugoutout metodo
        private void debugOutput(string strDebugText)
        {
            try
            {
                dynamic results = JsonConvert.DeserializeObject<dynamic>(strDebugText);
                dynamic id = results.results;
                //System.Diagnostics.Debug.Write(strDebugText + Environment.NewLine);
                //txtResponse.Text = txtResponse.Text + strDebugText + Environment.NewLine;
                textBox17.Text = "";
                textBox17.Text = id[0].fullname;

                //txtResponse.SelectionStart = txtResponse.TextLength;
                //txtResponse.ScrollToCaret();


            }
            
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        public void debug2(string strDebugText2)
        {

            try
            {
                dynamic res = JsonConvert.DeserializeObject<dynamic>(strDebugText2);
                dynamic cod = res.codificacion;
            
                if (res.status == 1)
                {
                    clave = cod.clave;
                    consecutivo = cod.consecutivo;
                    impri = true;
                }
                else
                {

                    MessageBox.Show("Resultado no válido por la siguiente razón \n"+res.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    impri = false;
                }
                   
               


            }
            catch (RuntimeBinderException re)
            {
                //RECORDAR QUE ESTO ES PORQUE NO SE RECIBIO NADA POR PARTE DE HACIENDA
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion
     
        private void comboBox3_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                using (var rClient= new buscarcedula())
                {
                    rClient.endPoint = "https://apis.gometa.org/cedulas/" + comboBox3.Text.Trim();
                    debugOutput("RESTClient Object created.");

                    string strJSON = string.Empty;

                    strJSON = rClient.makeRequest();

                    debugOutput(strJSON);
                    rClient.Dispose();

                }
                    

              

            }

                       

            
            catch (Exception comboBox3_KeyDown)
            {
                Mensaje.Error(comboBox3_KeyDown, "2117");
              
                   

            }
        }
        
      
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                long cun = 0;
                if (e.KeyCode == Keys.Return)
                {

                    using (var mysql = new Mysql())
                    {

                        mysql.conexion();
                        mysql.cadenasql = "select Nombre from vendedores where Codigo='" + comboBox2.Text.Trim() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            cun += 1;

                        }
                        if (cun > 0)
                        {
                            textBox16.Text = mysql.lector["Nombre"].ToString();

                        }
                        else
                        {
                            MessageBox.Show("El vendedor que busca no existe");
                            textBox16.Text = "";

                        }
                        comboBox3.Focus();
                        mysql.Dispose();
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {

                    vendedores vend = new vendedores(this);
                    vend.Show();

                }
                

                       
               
              
               
            }
            catch (Exception comboBox2_KeyDown)
            {
                Mensaje.Error(comboBox2_KeyDown, "2157");
             
                   

            }
         
        }
       
      
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode==Keys.Return)
                {
                    using (var rClient = new buscarcedula())
                    {
                        rClient.endPoint = "https://apis.gometa.org/cedulas/" + comboBox3.Text.Trim();
                        debugOutput("RESTClient Object created.");

                        string strJSON = string.Empty;

                        strJSON = rClient.makeRequest();

                        debugOutput(strJSON);
                        rClient.Dispose();

                    }

                }
              



            }




            catch (Exception comboBox3_KeyDown)
            {
                Mensaje.Error(comboBox3_KeyDown, "2117");



            }
        }
     
   
        private void comboBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            bool band = false;
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{
                //    textBox17.Text = "";
                //    if (comboBox3.Text.Substring(0)=="0")
                //    {
                //        //MessageBox.Show(comboBox3.Text.Length.ToString());

                //            using (var mysql= new Mysql())
                //            {
                //                mysql.conexion();
                //                mysql.cadenasql = "SELECT * FROM `clientes` WHERE Cedula='" + comboBox3.Text.Trim() + "'";
                //                mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                //                using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                //                {


                //                    if (lee.Read())
                //                    {
                //                    textBox17.Text = lee["Nombre"].ToString();
                //                        textBox9.Text = lee["Telefono1"].ToString();
                //                        textBox10.Text = lee["Correo"].ToString();
                //                    comboBox3.Enabled = false;
                //                        textBox1.Focus();
                //                    }
                //                    else
                //                    {

                //                        comboBox3.Text = "0";
                //                        textBox17.Text = "Contado";
                //                        textBox9.Text = "Contado";
                //                        textBox10.Text = "Contado@correo.com";
                //                    comboBox3.Enabled = false;
                //                    textBox1.Focus();
                //                    }

                //                }

                //            }



                //    }

                //    else
                //    {

                if (e.KeyCode==Keys.Enter)
                {using (var mysql=new Mysql())
                    {
                        mysql.conexion();
                        mysql.cadenasql = "SELECT * FROM `clientes` WHERE Cedula='" + comboBox3.Text.Trim() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        using (MySqlDataReader dr=mysql.comando.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                comboBox3.Text= dr["Cedula"].ToString();
                                textBox17.Text = dr["Nombre"].ToString();
                                textBox9.Text = dr["Telefono1"].ToString();
                                textBox10.Text = dr["Correo"].ToString();
                                //comboBox3.Enabled = false;
                                band = true;
                                textBox1.Focus();
                            }
                        }
                          
                    }
                    if (band)
                    {

                    }
                    else
                    {


                        rClient.endPoint = "https://apis.gometa.org/cedulas/" + comboBox3.Text.Trim();
                        debugOutput("RESTClient Object created.");

                        string strJSON = string.Empty;

                        strJSON = rClient.makeRequest();

                        debugOutput(strJSON);

                        if (!string.IsNullOrEmpty(textBox17.Text))
                        {
                            using (var mysql = new Mysql())
                            {
                                mysql.conexion();
                                mysql.cadenasql = "SELECT * FROM `clientes` WHERE Cedula='" + comboBox3.Text.Trim() + "'";
                                mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                                mysql.lector = mysql.comando.ExecuteReader();
                                if (mysql.lector.Read())
                                {
                                    textBox9.Text = mysql.lector["Telefono1"].ToString();
                                    textBox10.Text = mysql.lector["Correo"].ToString();
                                    comboBox3.Enabled = false;
                                    textBox1.Focus();
                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show("Este cliente no existe, desea crearlo?",
                                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                        cedula = comboBox3.Text;
                                        nombrec = textBox17.Text.Trim();
                                        clienform cfm = new clienform(this);
                                        cfm.textBox5.Text = cedula;
                                        cfm.textBox1.Text = nombrec;
                                        this.Visible = false;
                                        cfm.bandera = true;
                                        cfm.Show(this);
                                        //this.Visible = true;
                                        //this.SendToBack();
                                        comboBox3.Enabled = false;
                                        cfm.Focus();
                                        cfm.textBox2.Focus();
                                        //SendKeys.Send("{%}{Tab}");
                                        //NativeMethods.SetWindowTop(cfm.Handle);

                                    }



                                }

                                mysql.Dispose();

                            }
                        }


                    }


                    comboBox3.Enabled = false;
                }

                //    }


                //}



              


            }




            catch (Exception comboBox3_KeyDown)
            {
                Mensaje.Error(comboBox3_KeyDown, "2117");



            }
        }
       
      
        private void comboBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                int cun = 0;
                if (e.KeyCode == Keys.Enter)
                {

                    using (var mysql = new Mysql())
                    {

                        mysql.conexion();
                        mysql.cadenasql = "select Nombre from vendedores where Codigo='" + comboBox2.Text.Trim() + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();
                        mysql.lector = mysql.comando.ExecuteReader();
                        while (mysql.lector.Read())
                        {
                            cun += 1;

                        }
                        if (cun > 0)
                        {
                            textBox16.Text = mysql.lector["Nombre"].ToString();
                            //comboBox2.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show("El vendedor que busca no existe");
                            textBox16.Text = "";

                        }
                        
                        mysql.Dispose();
                        comboBox2.Enabled = false;
                        comboBox3.Focus();
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {

                    vendedores vend = new vendedores(this);
                    vend.Show();

                }




                

            }
            catch (Exception comboBox2_KeyDown)
            {
                Mensaje.Error(comboBox2_KeyDown, "2157");



            }
        }
       
      
        private void comboBox2_Enter(object sender, EventArgs e)
        {
           comboBox2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox2_Leave(object sender, EventArgs e)
        {
            comboBox2.BackColor = Color.WhiteSmoke;
        }

        private void textBox16_Enter(object sender, EventArgs e)
        {
            textBox16.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox3_Enter(object sender, EventArgs e)
        {
            comboBox3.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox17_Enter(object sender, EventArgs e)
        {
            textBox17.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            textBox9.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            textBox10.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox12_Enter(object sender, EventArgs e)
        {
            
        }

        private void textBox14_Enter(object sender, EventArgs e)
        {
            textBox14.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void textBox16_Leave(object sender, EventArgs e)
        {
            textBox16.BackColor = Color.WhiteSmoke;
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            comboBox3.BackColor = Color.WhiteSmoke;
        }

        private void textBox17_Leave(object sender, EventArgs e)
        {
            textBox17.BackColor = Color.WhiteSmoke;
        }

        private void textBox9_Leave(object sender, EventArgs e)
        {
            textBox9.BackColor = Color.WhiteSmoke;
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            textBox10.BackColor = Color.WhiteSmoke;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.WhiteSmoke;
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
           
        }

        private void textBox14_Leave(object sender, EventArgs e)
        {
            textBox14.BackColor = Color.WhiteSmoke;
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            comboBox1.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            comboBox1.BackColor = Color.WhiteSmoke;
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Application.ExitThread();

        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
        }
        private void timer1_Tick(object sender, EventArgs e)
        {


            label8.Text = DateTime.Now.ToShortTimeString();


        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                cadena1 = "";
                cadena2 = "";
                string can = "";
                if (e.KeyCode == Keys.Enter)
                {


                    if (textBox1.Text.Length > 12)
                    {
                        cadena1 = textBox1.Text.Substring(0, 6);
                        cadena2 = textBox1.Text.Substring(6, 6);
                        if (Int64.Parse(cadena2) > 0)
                        {
                            can = cadena2.Insert(3, ".");
                            //MessageBox.Show(cadena1 + "  " + cadena2 + "  " + textBox1.Text.Length);
                            textBox2.Text = can;
                            llenar(textBox1.Text.Trim());



                            textBox14.BackColor = Color.FromArgb(192, 255, 192);
                            timer2.Interval = 100;
                            timer2.Start();

                        }
                        else
                        {
                            MessageBox.Show("cadena de entrada no valida");
                        }


                    }
                    else
                    {
                        MessageBox.Show("cadena de entrada no valida");
                    }
                }
            }
            catch (Exception hn)
            {

                MessageBox.Show(hn.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            this.BackgroundImage = this.pictureBox5.BackgroundImage = global::POS.Properties.Resources.Logo_Itco_vector_05;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            this.BackgroundImage = this.pictureBox5.BackgroundImage = global::POS.Properties.Resources.Logo_Itco_vector_03;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.itcoint.com/");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void agregarUnClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //cls.Show();
        }
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {





        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //Form2 f34 = new Form2();
            //f34.Show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // c.refrescarClientes();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_2(object sender, EventArgs e)
        {
         
            //MessageBox.Show(cajero);

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }
        private void button5_Click_3(object sender, EventArgs e)
        {
            imprimir();
        }
        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox6.Visible == true)
            {

                pictureBox6.Visible = false;
            }
            else
            {
                pictureBox6.Visible = true;

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text=="Tarjeta")
            {
                label10.Visible = true;

                textBox7.Visible = true;
              
                textBox7.Text = "0";
                textBox7.Focus();
            }
            else
            {

                label10.Visible = false;
               
                textBox7.Visible = false;
                textBox7.Text = "0";
                textBox14.Focus();
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
           
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Focus();
              
                

            }
            catch (Exception hsjs)
            {

                Mensaje.Error(hsjs, "2254");
              
                  
            }
          
        }

        private void timer2_Tick_3(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click_3(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private  void button4_Click_4(object sender, EventArgs e)
        {
            contr ctr = new contr();
            ctr.Show(this);
           //await logos.guardarlog("se presiono el boton anular ",DateTime.Now.ToString("yyyy-MM-dd") + "", "" + DateTime.Now.ToString("HH:mm:ss tt") + "", "Evento","monto");
        }

        private void panelHaciendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facturasantteriores fh = new facturasantteriores();
            fh.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(comboBox3.Text) && dataGridView1.Rows.Count>0&& !string.IsNullOrEmpty(comboBox2.Text))
                {
                    meterpedido();
                   
                    for (int imp = 0; imp < Int32.Parse(numericUpDown1.Value.ToString()); imp++)
                    {
                        imprimir2();

                    }
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Faltan datos","Faltan datos",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                }
            
               

            }
            catch (Exception ece)
            {
                MessageBox.Show(ece.ToString());
            }
        }

        public void meterpedido()
        {
            try
            {

                using (var mysql = new Mysql())
                {
                    mysql.conexion2();
                    mysql.cadenasql = "select Max(Numero) from pedidos where Numero like '" + textBox4.Text + "%'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();




                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            if (lee["Max(Numero)"].ToString() != "")
                            {
                                idFac = Int64.Parse(string.Concat(textBox4.Text, (Int64.Parse(lee["Max(Numero)"].ToString().Substring(1)) + 1).ToString()));
                            }
                            else
                            {
                                idFac = Int64.Parse(string.Concat(textBox4.Text, "1"));

                            }


                            //MessageBox.Show(numeroRecibido);

                        }




                    }








                    /////////////////////////////////////////////////////////////////////////////


                    mysql.cadenasql = "insert into pedidos(Numero,Fecha,Cliente,Total,CodigoCajero,CodigoVendedor,Estado)values('"
                   + idFac + "','" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " "
                   + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "','" + comboBox3.Text.Trim() +
                   "','" + decimal.Parse(textBox5.Text.Trim()) + "','" + textBox4.Text + "','" + comboBox2.Text +
                   "','PENDIENTE')";

                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    ///////////////////////////////////////////////////////////////////////////////////////////
                    for (int count = 0; count < dataGridView1.Rows.Count; count++)
                    {
                        mysql.cadenasql = "INSERT INTO `detallespedidos`(`NumeroFactura`, `Cliente`, `Item`, `Cantidad`, `Precio`,`Impuesto`) VALUES ('" + idFac + "','" +
                            comboBox3.Text.Trim() + "','" + dataGridView1.Rows[count].Cells[0].Value + "','" +
                            dataGridView1.Rows[count].Cells[1].Value + "','" +
                            dataGridView1.Rows[count].Cells[3].Value + "','" + dataGridView1.Rows[count].Cells[9].Value + "')";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();

                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////
                    mysql.cadenasql = "select Max(Numero) from pedidos where CodigoCajero='" + textBox4.Text + "' AND Cliente='" + comboBox3.Text.Trim() + "' AND Total='" + decimal.Parse(textBox5.Text.Trim()) + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();

                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            if (lee["Max(Numero)"].ToString() != "")
                            {
                                numeroRecibido = lee["Max(Numero)"].ToString();
                            }
                            else
                            {
                                numeroRecibido = "0";

                            }




                        }

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                       

                        


                    }

                        mysql.rol();
                    mysql.Dispose();

                    formatodeacturapedido(numeroRecibido);
                    textBox1.Visible = true;
                    MessageBox.Show("El pedido///"+numeroRecibido+"///se guardó con éxito","Solicitud procesada",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    limpiar();
                }

                log.guardarlog("Se realizo un pedido:  por el usuario: " + facturando.Text, "" + DateTime.Now.ToString("yyyy-MM-dd") + "", "" + DateTime.Now.ToString("HH:mm:ss tt") + "", "Transaccion", textBox5.Text);

            }
            catch (Exception ftr)
            {
                Mensaje.Error(ftr, "191");


              

            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pedidos ped = new pedidos(this);
            ped.Show(this);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {

                eventos(e);




            }
            catch (Exception err_0011)
            {
                Mensaje.Error(err_0011, "2054");



            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) ||char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void panel8_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) ||char.IsControl(e.KeyChar))
            {
                if (textBox12.TextLength<2 ||char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                if (textBox13.TextLength < 3 || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (textBox11.TextLength==7&& textBox12.TextLength ==2
                    && textBox13.TextLength == 3)
                {
                    textBox1.Text = textBox11.Text+textBox12.Text + textBox13.Text+"8";
                    textBox1.Focus();
                    //SendKeys.Send("{ENTER}");
                }
                else
                {
                    MessageBox.Show("Formato incorrecto", "Formato incorrecto",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }

            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox11.TextLength == 7 && textBox12.TextLength == 2
                  && textBox13.TextLength == 3)
                {
                    textBox1.Text = textBox11.Text + textBox12.Text + textBox13.Text + "8";
                    textBox1.Focus();
                    //SendKeys.Send("{ENTER}");
                }
                else
                {
                    MessageBox.Show("Formato incorrecto", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                if (textBox11.TextLength < 7 || char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox11.TextLength == 7 && textBox12.TextLength == 2
                  && textBox13.TextLength == 3)
                {
                    textBox1.Text = textBox11.Text + textBox12.Text + textBox13.Text + "8";
                    textBox1.Focus();
                    //SendKeys.Send("{ENTER}");
                }
                else
                {
                    MessageBox.Show("Formato incorrecto", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {

                //textBox14.Text = string.Format("{0:N0}", double.Parse(textBox14.Text.Trim()));

                if (e.KeyCode == Keys.Enter)
                {

                    if (!string.IsNullOrEmpty(textBox7.Text)&& !string.IsNullOrEmpty(comboBox1.Text)&&
                        !string.IsNullOrEmpty(textBox1.Text)&& !string.IsNullOrEmpty(comboBox2.Text)&&
                        !string.IsNullOrEmpty(comboBox3.Text)&& !string.IsNullOrEmpty(textBox16.Text)&&
                        !string.IsNullOrEmpty(comboBox3.Text)&& !string.IsNullOrEmpty(textBox17.Text)&&
                        !string.IsNullOrEmpty(textBox9.Text)&& !string.IsNullOrEmpty(textBox10.Text))
                    {
                        textBox1.Visible = false;



                        button3.Visible = true;
                        button3.Enabled = true;
                        button6.Visible = true;
                        label24.Visible = true;
                        label11.Visible = true;
                        textBox14.Enabled = false;
                        textBox15.BackColor = Color.FromArgb(255, 128, 128);
                        textBox15.ForeColor = Color.White;

                        timer3.Interval = 100;
                        timer3.Start();
                        textBox15.Text = string.Format("{0:N3}", (decimal.Parse(textBox5.Text.Trim()) - decimal.Parse(textBox14.Text.Trim())));




                        textBox14.Focus();

                    }

                    else
                    {

                        MessageBox.Show("Por favor verifique la información del vendedor,cliente,númer de comprobante, tipo de pago y código de producto e intente de nuevo","Faltan datos",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }

                   


                }
             



            }
            catch (Exception textBox14_KeyDown)
            {

                Mensaje.Error(textBox14_KeyDown, "2022");
                timer3.Stop();
                textBox15.BackColor = Color.WhiteSmoke; textBox15.ForeColor = Color.White;
            }
        }
    }
}
