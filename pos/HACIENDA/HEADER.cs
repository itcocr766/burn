using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class HEADER
    {
        DateTime date;
        string termOfSale;
        int creditTerm;
        string paymentMethod;

        public DateTime Date { get => date; set => date = value; }
        public string TermOfSale { get => termOfSale; set => termOfSale = value; }
        public int CreditTerm { get => creditTerm; set => creditTerm = value; }
        public string PaymentMethod { get => paymentMethod; set => paymentMethod = value; }
    }
}
