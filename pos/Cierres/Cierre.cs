using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.Modelo;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.IO;
using System.Drawing.Printing;

namespace POS.Cierres
{
    public partial class Cierre : Form
    {
        bool impri;
       
        string formato;
        StreamWriter facturawr;
        StreamReader streamToPrint;
        Font printFont;
        decimal bg;
        decimal bsg;
        decimal im;
        public Cierre()
        {
            InitializeComponent();
        }

        private void Cierre_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(dateTimePicker1.Value.ToString());
            
            
        }
        public void cargarFactura()
        {
            try
            {
                using (var mysql= new Mysql())
                {
                    mysql.conexion();
                    DataTable dtDatos = new DataTable();
                    mysql.cadenasql = "select * from factura where Tipo='Factura' and Fecha between '" + dateTimePicker1.Value.ToString("yyyy-MM-dd hh:mm:ss") + "' and '"+ dateTimePicker2.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'";
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

       

        private void button7_Click(object sender, EventArgs e)
        {
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            formatodeactura();

            if (impri)
            {
                imprimir();
                dataGridView1.DataSource = null;
                button6.Enabled = false;
                button1.Enabled = false;
                button5.Enabled = false;
                this.Visible = false;
            }
           

        }

        #region metodos impresion
        public void formatodeactura()
        {
            impri = false;
            formato = "";

            try
            {
               


                    formato = "Cubirami                    La Unión\n\n" +
                              "CAJA                       ADMINISTRADOR\n" +
                              "Número Cierre        0\n" +
                     
                              "FECHA CIERRE        "+DateTime.Now.ToShortDateString()+"\n" +
                              "---------------------------------------------------------\n"+
                              "\nTOTAL BRUTO GRAVADO= "+textBox1.Text+
                                "\nTOTAL EXCENTO= "+textBox2.Text+
                                "\nTOTAL IMPUESTO= "+textBox3.Text+
                              "\nFONDO CAJA            0\n" +
                              "---------------------------------------------------------\n\n"+
                              "NOTAS\n\n" +
                              "________________________________________\n\n" +
                              "________________________________________\n\n" +
                              "FIRMA CAJERO           FIRMA ADMIN\n\n" +
                              "________________       _________________\n\n" +
                              "Impreso el "+DateTime.Now.ToString();


                    facturawr = new StreamWriter("Cierre.txt");
                    facturawr.WriteLine(formato);
                    facturawr.Flush();
                    facturawr.Close();





                impri = true;



            }
            catch (Exception err_0016)
            {

                impri = false;
                MessageBox.Show(err_0016.ToString());
                Mensaje.Error(err_0016, "329");



            }

        }

        public void imprimir()
        {

            try
            {



                streamToPrint = new StreamReader
                    ("Cierre.txt");


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
                Mensaje.Error(err_004, "128");

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
                Mensaje.Error(err_005, "186");

            }


        }
        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            gravados();
            excentos();
            button6.Enabled = false;
            button1.Enabled = true;
        }

        public void gravados()      {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    mysql.cadenasql = "SELECT f.Fecha,d.NumeroFactura,(d.Precio*d.Cantidad) AS BrutoGravado,ROUND((((d.Precio*d.Cantidad)/1.13)),2) AS Brutosingravar,ROUND(((((d.Precio*d.Cantidad)/1.13))*13)/100,2) AS Impuesto FROM factura f,detalles d where f.Numero=d.NumeroFactura AND d.Impuesto='(G)' AND f.Fecha BETWEEN '"+dateTimePicker1.Value.ToString("yyyy-MM-dd")+"' AND '"+dateTimePicker2.Value.ToString("yyyy-MM-dd")+"'";
                    MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql,mysql.con);
                    adapt.Fill(dt);
                    dataGridView1.DataSource = dt;
                    mysql.Dispose();

                }

            }
            catch (Exception huy)
            {
                MessageBox.Show(huy.ToString());
                button1.Enabled = false;
                button6.Enabled = false;

            }
        }

        public void excentos()
        {
            DataTable dt = new DataTable();
            try
            {
                using (var mysql = new Mysql())
                {
                    mysql.conexion();

                    mysql.cadenasql = "SELECT f.Fecha,d.NumeroFactura,0 AS BrutoGravado, ROUND((((d.Precio * d.Cantidad))), 2) AS Brutosingravar,0 AS Impuesto FROM factura f,detalles d where f.Numero = d.NumeroFactura AND d.Impuesto = '(E)' AND f.Fecha BETWEEN '"+dateTimePicker1.Value.ToString("yyyy-MM-dd") +"' and '"+dateTimePicker2.Value.ToString("yyyy-MM-dd") +"'";
                    MySqlDataAdapter adapt = new MySqlDataAdapter(mysql.cadenasql, mysql.con);
                    adapt.Fill(dt);
                    dataGridView2.DataSource = dt;
                    mysql.Dispose();

                }





            }
            catch (Exception huy)
            {
                MessageBox.Show(huy.ToString());
                button1.Enabled = false;
                button6.Enabled = false;

            }
            
        }


       



        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bg = 0;
            bsg = 0;
            im = 0;
            try
            {
                for (int y=0;y<dataGridView1.Rows.Count;y++)
                {
                    if (dataGridView1.Rows.Count>0)
                    {
                        bg += decimal.Parse(dataGridView1.Rows[y].Cells[2].Value.ToString());
                        bsg += decimal.Parse(dataGridView1.Rows[y].Cells[3].Value.ToString());
                        im += decimal.Parse(dataGridView1.Rows[y].Cells[4].Value.ToString());
                    }

                }

                for (int z=0;z<dataGridView2.Rows.Count;z++)
                {
                    if (dataGridView2.Rows.Count>0)
                    {
                        bsg += decimal.Parse(dataGridView2.Rows[z].Cells[3].Value.ToString());
                    }

                }


                textBox1.Text = string.Format("{0:N2}",bg);
                textBox2.Text = string.Format("{0:N2}", bsg);
                textBox3.Text = string.Format("{0:N2}", im);
                button5.Enabled = true;
            }
            catch (Exception fre)
            {
                MessageBox.Show(fre.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void cargarfacs()
        {
            
        }

        private void textBox15_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void maskedTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
