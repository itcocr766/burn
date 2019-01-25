using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class REFERENCE
    {
        string documentType;
        string documentNumber;
        string code;
        string reason;

        public string DocumentType { get => documentType; set => documentType = value; }
        public string DocumentNumber { get => documentNumber; set => documentNumber = value; }
        public string Code { get => code; set => code = value; }
        public string Reason { get => reason; set => reason = value; }
    }
}
