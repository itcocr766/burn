using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Interop.QBFC13;
using POS.Control;

namespace POS.Vista
{
    public partial class Form2 : Form
    {
        ControlFactura cg = new ControlFactura();

        IMsgSetRequest requestMsgSetfr;
        bool sessionBegun = false;
        bool connectionOpen = false;
        QBSessionManager sessionManager = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            coni();


            requestMsgSetfr = sessionManager.CreateMsgSetRequest("US", 13, 0);
            requestMsgSetfr.Attributes.OnError = ENRqOnError.roeContinue;


            ICustomerQuery CustomerQueryRq = requestMsgSetfr.AppendCustomerQueryRq();

            CustomerQueryRq.metaData.SetValue(ENmetaData.mdMetaDataAndResponseData);

            IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSetfr);
            


            //richTextBox1.Text += responseMsgSet.ToXMLString();

            cerrar();
        }

        public void coni()
        {

            sessionManager = new QBSessionManager();

            sessionManager.OpenConnection("", "ITCO");
            connectionOpen = true;
            sessionManager.BeginSession("", ENOpenMode.omDontCare);
            sessionBegun = true;
        }

        public void cerrar()
        {

            try
            {
                sessionManager.EndSession();
                sessionBegun = false;
                sessionManager.CloseConnection();
                connectionOpen = false;

            }
            catch (Exception err_002)
            {

                MessageBox.Show("Se produjo el error err_002");
               // logs("Se produjo el error err_002 ");

            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            
                MessageBox.Show(e.KeyData.ToString());

            
        }
    }
}
