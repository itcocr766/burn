using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace POS.cedulas
{
    public enum httpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }
    public class buscarcedula:IDisposable
    {
        public string endPoint { get; set; }
        public httpVerb httpMethod { get; set; }
        HttpWebRequest request;
        public string strResponseValue = "";
        HttpWebResponse response;
        public buscarcedula()
        {
            endPoint = "";
            httpMethod = httpVerb.GET;
        }

        public string makeRequest()
        {

             strResponseValue = string.Empty;

             request = (HttpWebRequest)WebRequest.Create(endPoint);

            request.Method = httpMethod.ToString();

             response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();


                //Proecess the resppnse stream... (could be JSON, XML or HTML etc..._

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            strResponseValue = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strResponseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return strResponseValue;
        }

        public void Dispose()
        {
            strResponseValue = null;
            request = null;
            response = null;
        }
    }
}
