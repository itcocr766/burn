using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
   public class logs
    {
        public async  Task guardarlog(string descripcion, string fecha, string hora, string tipo, string monto)
        {
            try
            {
               await Task.Run(() =>
               {
                   var client = new RestClient("https://carniceriaunion-2dcc4.firebaseio.com/log.json");
                   var request = new RestRequest(Method.POST);
                   request.AddHeader("cache-control", "no-cache");
                   request.AddHeader("Content-Type", "application/json");
                   request.AddParameter("undefined", "{\n\t\"Descripcion\":\"" + descripcion + "\",\n\t\"Fecha\":\"" + fecha + "\",\n\t\"Tipo\":\"" + tipo + "\",\n\t\"Hora\":\"" + hora + "\",\n\t\"Monto\":\"" + monto + "\"\n}", ParameterType.RequestBody);
                   IRestResponse response = client.Execute(request);

               }
                   
         
                
                );
              
              
            }
            catch (Exception l)
            {

            }
        }
    }
}
