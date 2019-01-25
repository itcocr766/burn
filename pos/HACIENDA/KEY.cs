using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class KEY
    {
        string branch;
        string terminal;
        string type;
        string voucher;
        string country;
        string situation;

        public string Branch { get => branch; set => branch = value; }
        public string Terminal { get => terminal; set => terminal = value; }
        public string Type { get => type; set => type = value; }
        public string Voucher { get => voucher; set => voucher = value; }
        public string Country { get => country; set => country = value; }
        public string Situation { get => situation; set => situation = value; }
    }
}
