using MySql.Data.MySqlClient;
using POS.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS.Pedido
{
    public partial class pedidos : Form
    {
        StreamReader streamToPrint;
        Font printFont;
        string formatope;
        string len;
        string productos;
        StreamWriter facturawr;
        private Form1 m_frm;
        bool impri=false;
        public pedidos(Form1 frm)
        {
            InitializeComponent(); m_frm = frm;
        }

        private void pedidos_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter){

                dataGridView1.DataSource = null;
                cargarpedidos();
            }
          
        }

        public void cargarpedidos()
        {

            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from pedidos where Cliente='"+textBox1.Text.Trim()+"' and Estado='PENDIENTE'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception hju)
            {
                MessageBox.Show(hju.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                formatodepedido();
                imprimirpepe();
                dataGridView1.DataSource = null;
                this.Visible = false;
            }

               
        }
        public void facturado()
        {

            try
            {
                using (var mysql=new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "UPDATE pedidos SET Estado='FACTURADO' WHERE Numero='"+dataGridView1.CurrentRow.Cells[0].Value+"'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                 
                    mysql.Dispose();
                }
            }
            catch (Exception erfty)
            {
                MessageBox.Show(erfty.ToString());
            }
        }




        public void formatodepedido()
        {

            decimal impp = 0;
            decimal coni = 0;
            decimal sini = 0;
           
            productos = "";
            formatope = "";
            len = "";
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    //mysql.cadenasql = "select * from detallespedidos where NumeroFactura='" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                    mysql.cadenasql = "" +
                        "SELECT detallespedidos.NumeroFactura as num,detallespedidos.Impuesto as impos, detallespedidos.Cantidad as can,items.Codigo,items.Nombre as nom, detallespedidos.Precio as pre, detallespedidos.Impuesto  FROM detallespedidos,items WHERE detallespedidos.NumeroFactura='"+dataGridView1.Rows[0].Cells[0].Value.ToString()+"'  AND detallespedidos.Item=items.Codigo";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        while (lee.Read())
                        {
                            productos += "\n  " + lee["can"].ToString() +
                      "        ₡" +
                   string.Format("{0:N2}", double.Parse(lee["pre"].ToString()))
                   + "\n          " +lee["nom"].ToString();


                            if (lee["impos"].ToString() == "(G)")
                            {
                                coni += (((decimal.Parse(lee["pre"].ToString()))*(decimal.Parse(lee["pre"].ToString()))) / 0.13m);
                            }
                            else
                            {
                                sini += (decimal.Parse(lee["pre"].ToString()))*decimal.Parse(lee["can"].ToString());
                            }




                        }
                      
                    }

                    if (coni>0)
                    {
                        impp = ((coni * 13) / 100);
                    }
                    mysql.Dispose();
                }

                string direccion = "";
                string n = "";
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select Direccion,Nombre from clientes where Cedula='" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            direccion = lee["Direccion"].ToString();
                            n = lee["Nombre"].ToString();
                        }
                        else
                        {
                            direccion = "Guadalupe";
                            n = "Contado";
                        }

                    }
                    mysql.Dispose();
                }
                string t = "";
                string cove = "";
                string coca = "";
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    mysql.cadenasql = "select pedidos.Total as tot,pedidos.CodigoVendedor as cv,pedidos.CodigoCajero as ca,registro.Nombre as re,vendedores.Nombre as nv from pedidos,registro,vendedores where Numero='" + dataGridView1.CurrentRow.Cells[0].Value + "' and pedidos.CodigoCajero=registro.Codigo and pedidos.CodigoVendedor=vendedores.Codigo";
                    mysql.comando = new MySqlCommand(mysql.cadenasql, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        if (lee.Read())
                        {
                            t = lee["tot"].ToString();
                            cove = lee["nv"].ToString();
                            coca = lee["re"].ToString();
                        }
                        else
                        {
                            t = "0";
                            cove = "0";
                            coca = "0";
                        }

                    }
                
                    mysql.Dispose();
                }



                formatope = "Razón social:\nCUBIRAMI SOCIEDAD ANÓNIMA\nCed.Jurídica:3101125675\nCorreo:lanuevaunion@hotmail.com\nLugar:San José\nTeléfono:2224-7042" +
                  "\n    Fecha:     " + DateTime.Now.ToString() + "\n" +

                  "                Número de Pedido: " + dataGridView1.Rows[0].Cells[0].Value + "\n" +
                  "        Tipo de pago : " + comboBox1.SelectedItem.ToString() + "\n          Facturado por: " +
                  coca +
                  "\n              Cliente:\n" + n + "\n" +
                  "        Dirección:\n " + direccion + "\n" +
               "\nCantidad        Precio        \n" + "Artículo\n" +
              "----------------------------------------------------------" + productos +
              "\n         (E)=Exento              (G)=Gravado" 
                + dataGridView1.Rows.Count + "\nSUBTOTAL=                      ₡"+string.Format("{0:N2}",(coni+sini))
                +  "\nI.V.A.=                               ₡"+string.Format("{0:N2}",impp)
                     + "\nDESCUENTO=                   ₡"+0
                
                                          +
                                           "\nTOTAL=                             ₡"
              + string.Format("{0:N2}",decimal.Parse(t)) +
              "\n------------------------------MONTO-------------" +
              
              "\n           ARTICULOS CON I.V.I." +
              "\n           VENDEDOR : " + cove +
              "\n----Autorizada mediante resolución--------\n---------N° DGT‐R‐48‐2016 del 07---------\n----------de octubre de 2016.---------------\n----------¡Gracias por su compra!--------------\n********Esperamos servirle de nuevo ************";



                facturawr = new StreamWriter("pedido.txt");
                facturawr.WriteLine(formatope);
                facturawr.Flush();
                facturawr.Close();

               
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
                impri = false;
            }
        }




        public void imprimirpepe()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("pedido.txt");


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

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            cargarpedidopornumero();
        }
        public void cargarpedidopornumero()
        {
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    string query = "select * from pedidos where Numero='" + textBox2.Text.Trim() + "' and Estado='PENDIENTE'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, mysql.con);
                    mdaDatos.Fill(dtDatos);
                    dataGridView1.DataSource = dtDatos;
                    mysql.Dispose();

                }


            }
            catch (Exception hju)
            {
                MessageBox.Show(hju.ToString());

            }

        }

        private decimal GetTaxedPrice(decimal price, string gravado)
        {
            return (gravado == "(G)") ? price : price * 1.13m;
        }

        private void btnCargarDatos_Click(object sender, EventArgs e)
        {
            m_frm.limpiar();
            ConvertirANumeros canuj = new ConvertirANumeros();

            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    //string query = "select * from pedidos where Numero='" + textBox2.Text.Trim() + "' and Estado='PENDIENTE'";
                    string query =
                        "SELECT detallespedidos.Item AS codigo, detallespedidos.Cantidad AS cantidad, items.Nombre AS descripcion, detallespedidos.Precio AS precio," +
                        //" pedidos.Total AS Total, " +
                        "detallespedidos.Impuesto AS exoneracion " +
                        "FROM detallespedidos, items, pedidos " +
                        " WHERE detallespedidos.NumeroFactura='"+dataGridView1.CurrentRow.Cells[0].Value+ "'   and detallespedidos.Item=items.Codigo GROUP BY detallespedidos.Item";
                    mysql.comando = new MySqlCommand(query, mysql.con);
                    mysql.comando.ExecuteNonQuery();
                    using (MySqlDataReader lee = mysql.comando.ExecuteReader())
                    {
                        decimal price;
                        decimal tax;
                        decimal taxedPrice;
                        decimal untaxedPrice;
                        decimal total;
                        while (lee.Read())
                        {
                            taxedPrice = GetTaxedPrice(decimal.Parse(lee["precio"].ToString()), lee["exoneracion"].ToString());
                            tax = taxedPrice - taxedPrice/1.13m;
                            untaxedPrice = taxedPrice / 1.13m;
                            price = (lee["exoneracion"].ToString() == "(E)")  ? untaxedPrice : taxedPrice;
                            total = decimal.Parse(lee["cantidad"].ToString()) * price;
                            m_frm.dataGridView1.Rows.Add(lee["codigo"].ToString(), lee["cantidad"].ToString(), lee["descripcion"].ToString(), lee["precio"].ToString(), canuj.enletras(lee["Precio"].ToString()), untaxedPrice, price, total, tax, lee["exoneracion"].ToString());
                        }

                        m_frm.comboBox2.Text = "";
                        m_frm.comboBox2.SelectedText = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                        m_frm.comboBox2.Focus();
                        SendKeys.SendWait("{ENTER}");

                        m_frm.comboBox3.Text = "";
                        m_frm.comboBox3.SelectedText = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        m_frm.comboBox3.Focus();
                        SendKeys.SendWait("{ENTER}");
                    }
                        mysql.Dispose();
                }

                
                this.Visible = false;
                m_frm.calcularTotal();
            }
            catch (Exception hju)
            {
                MessageBox.Show(hju.ToString());
            }

           
            

        } 

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
