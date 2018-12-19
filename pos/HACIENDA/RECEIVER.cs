using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class RECEIVER
    {
        string name;
        IDENTIFICATION identification;
        string email;

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public IDENTIFICATION Identification { get => identification; set => identification = value; }
    }
}
