using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class TAX
    {
        string code;
        decimal rate;
        decimal amount;
        EXONERATION exoneration;

        public string Code { get => code; set => code = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public EXONERATION Exoneration { get => exoneration; set => exoneration = value; }
    }
}
