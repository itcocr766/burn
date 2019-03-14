using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using POS.HACIENDA;
using POS.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Anular
{
    public partial class AnulaFactura : Form
    {
        logs log = new logs();
        static Respuesta respuesta;
        List<DETAIL> detalles;
        string eltipo;
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
        string json;
        ENVIO enviarfactura;
        Form1 formuno;
        decimal price;
        decimal canti;
        public AnulaFactura(Form1 f1)
        {
            InitializeComponent(); detalles = new List<DETAIL>();
            enviarfactura = new ENVIO();
            formuno = f1;
        }

        private void AnulaFactura_Load(object sender, EventArgs e)
        {
            cargarfacs();
           
        }

        public void cargarfacs()
        {

            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    mysql.cadenasql = "select * from factura";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Hubo un error a conectar a la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private async void button1_Click(object sender, EventArgs e)
        {
            int conta = 0;
            price = 0;
            canti=0;
            try
            {
                if (dataGridView1.Rows.Count>0&&!string.IsNullOrEmpty(richTextBox1.Text))
                {
                    using (var mysql = new Mysql())
                    {
                        mysql.conexion();

                        mysql.cadenasql = "update factura set Total='0' where Numero='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                        mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                        mysql.comando.ExecuteNonQuery();

                        mysql.Dispose();


                    }


                    DialogResult result = MessageBox.Show("Desea crear una nota de crédito para esta factura?", "Error",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {




                        FE f = new FE()
                        {
                            CompanyAPI = ConfigurationManager.AppSettings["companiapi"]


                        };



                        f.Key = new KEY()
                        {
                            Branch = ConfigurationManager.AppSettings["sucursal"],
                            Terminal = "001",
                            Type = "03",
                            Voucher = string.Concat("14", (Int64.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString())).ToString()),
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

                        //hacer consultas de receiver


                        if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "0")
                        {
                            f.Receiver = new RECEIVER()
                            {
                                Name = "Contado",
                                Identification = new IDENTIFICATION
                                {
                                    Type = "01",
                                    Number = "000000000"

                                },
                                Email = "Contado@correo.com"

                            };

                            eltipo = "04";

                        }
                        else
                        {
                            using (var mysql = new Mysql())
                            {
                                mysql.conexion();

                                mysql.cadenasql = "select Cedula,Nombre,Correo from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";
                                mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                                mysql.comando.ExecuteNonQuery();
                                mysql.lector = mysql.comando.ExecuteReader();
                                while (mysql.lector.Read())
                                {
                                    f.Receiver = new RECEIVER()
                                    {
                                        Name = mysql.lector["Nombre"].ToString(),
                                        Identification = new IDENTIFICATION
                                        {
                                            Type = "01",
                                            Number = dataGridView1.CurrentRow.Cells[2].Value.ToString()

                                        },
                                        Email = mysql.lector["Correo"].ToString()

                                    };

                                }
                                eltipo = "01";
                                mysql.Dispose();
                            }

                        }




                        detalles.Clear();
                        f.Detail = null;


                        using (var mysql = new Mysql())
                        {
                            mysql.conexion();

                            mysql.cadenasql = "select * from detalles where NumeroFactura='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                            mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                            mysql.comando.ExecuteNonQuery();
                            mysql.lector = mysql.comando.ExecuteReader();
                            while (mysql.lector.Read())
                            {

                                if (mysql.lector["Impuesto"].ToString() == "(G)")
                                {

                                    price = decimal.Parse(mysql.lector["Precio"].ToString());
                                    canti = decimal.Parse(mysql.lector["Cantidad"].ToString());
                                    unitprice = Math.Round((price / 1.13m), 2);
                                    impuestofe = Math.Round(((price * canti) - (unitprice * canti)), 2);
                                    totaltaxedgoodsfe += Math.Round(((unitprice * canti)), 2);
                                    totalsalesfe += Math.Round(((totaltaxedgoodsfe + totalexcemptgoodsfe)), 2);
                                    totalnetsalesfe += Math.Round(((totalsalesfe - discountfe)), 2);
                                    totaltaxedfe += Math.Round(((unitprice * canti)), 2);
                                    impuestototal += Math.Round(impuestofe, 2);


                                    losimpuestos = new TAX()
                                    {
                                        Code = "01",
                                        Rate = 13.0m,
                                        Amount = Math.Round(impuestofe, 2),
                                        Exoneration = null
                                    };

                                    totalamountfe = Math.Round(((unitprice * canti)), 2);
                                    discountfe = 0;
                                    subtotalfe = Math.Round(((totalamountfe - discountfe)), 2);
                                    totallineamountfe = Math.Round((subtotalfe + impuestofe), 2);
                                    DETAIL detail = new DETAIL()
                                    {
                                        Number = conta += 1,
                                        Code = new CODE
                                        {
                                            Type = "04",
                                            Code = mysql.lector["Item"].ToString()
                                        },
                                        Tax = new List<TAX>()
                                        {
                                            losimpuestos
                                        },

                                        Quantity = decimal.Parse(mysql.lector["Cantidad"].ToString()),
                                        UnitOfMeasure = "kg",
                                        CommercialUnitOfMeasure = null,
                                        Detail = dataGridView1.CurrentRow.Cells[2].Value.ToString(),
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

                                    impuestofe = 0;

                                    unitprice = decimal.Parse(mysql.lector["Precio"].ToString());
                                    canti = decimal.Parse(mysql.lector["Cantidad"].ToString());
                                    totaltaxedgoodsfe += 0;
                                    totalexcemptgoodsfe += Math.Round(((unitprice * canti)), 2);
                                    totalexcemptfe += Math.Round(((unitprice * canti)), 2);
                                    totalsalesfe += Math.Round(((totaltaxedgoodsfe + totalexcemptgoodsfe)), 2);
                                    totalnetsalesfe += Math.Round(((totalsalesfe - discountfe)), 2);
                                    totaltaxedfe += 0;
                                    totalamountfe = Math.Round(((unitprice * canti)), 2);
                                    discountfe = 0;


                                    subtotalfe = Math.Round(((totalamountfe - discountfe)), 2);
                                    totallineamountfe = Math.Round((subtotalfe + impuestofe), 2);
                                    DETAIL detail = new DETAIL()
                                    {
                                        Number = conta += 1,
                                        Code = new CODE
                                        {
                                            Type = "04",
                                            Code = mysql.lector["Item"].ToString()
                                        },


                                        Quantity = decimal.Parse(mysql.lector["Cantidad"].ToString()),
                                        UnitOfMeasure = "kg",
                                        CommercialUnitOfMeasure = null,
                                        Detail = dataGridView1.CurrentRow.Cells[2].Value.ToString(),
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
                            f.Detail = detalles;
                            mysql.Dispose();

                        }



                        using (var mysql = new Mysql())
                        {
                            mysql.conexion();
                            mysql.cadenasql = "select Consecutivo from hacienda where Comprobante='" + dataGridView1.CurrentRow.Cells[0].Value.ToString() + "'";
                            mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                            mysql.comando.ExecuteNonQuery();
                            mysql.lector = mysql.comando.ExecuteReader();

                            while (mysql.lector.Read())
                            {

                                REFERENCE refe = new REFERENCE()
                                {
                                    DocumentType = eltipo,
                                    DocumentNumber = mysql.lector["Consecutivo"].ToString(),
                                    Code = "01",
                                    Reason = richTextBox1.Text
                                };


                                f.Reference = new List<REFERENCE>()
                            {
                                refe
                            };

                            }
                            mysql.Dispose();
                        }





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
                        SendInvoicesAGC(f);
                        await log.guardarlog("Se realizo una anulacion:  por el usuario: " + formuno.facturando.Text, "" + DateTime.Now.ToString("yyyy-MM-dd") + "", "" + DateTime.Now.ToString("HH:mm:ss tt") + "", "Transaccion", dataGridView1.CurrentRow.Cells["Total"].Value.ToString());


                    }



                    cargarfacs(); MessageBox.Show("La factura ha sido anulada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Asegurese de seleccionar una factura y escribir el motivo de anulación correspondiente","No hay suficientes datos",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                
                

            }

            

            catch (Exception es)
            {
                MessageBox.Show("Hubo un error a conectar a la base de datos" + es.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        public void debug2(string strDebugText2)
        {

            try
            {
                dynamic res = JsonConvert.DeserializeObject<dynamic>(strDebugText2);
                dynamic cod = res.codificacion;
                //System.Diagnostics.Debug.Write(strDebugText2 + Environment.NewLine);
                //textBox17.Text = "";
                //textBox17.Text = id[0].fullname + Environment.NewLine;
                if (res.status == 1 )
                {
                    MessageBox.Show("correcto");
                    //clave = cod.clave;
                    //consecutivo = cod.consecutivo;
                    //impri = true;
                }
                else if (res.status == 2)
                {
                    MessageBox.Show("respuesta 2: el documento ya fue procesado");
                }
                else
                {

                    MessageBox.Show("Resultado no válido por la siguiente razón \n" + res.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //impri = false;
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from factura where Numero like '" + textBox1.Text + "%'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception euju)
            {

                Mensaje.Error(euju, "71");


            }
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            
        }
    }
}