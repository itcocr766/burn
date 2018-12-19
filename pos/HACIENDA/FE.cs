using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class FE
    {
        string companyAPI;
        


        KEY key;
        HEADER header;
        RECEIVER receiver;
        List<DETAIL> detail;
        SUMMARY summary;
        List<REFERENCE> reference;
        OTHER other;

        public string CompanyAPI { get => companyAPI; set => companyAPI = value; }
        public KEY Key { get => key; set => key = value; }
        public HEADER Header { get => header; set => header = value; }
        public RECEIVER Receiver { get => receiver; set => receiver = value; }
        public List<DETAIL> Detail { get => detail; set => detail = value; }
        public SUMMARY Summary { get => summary; set => summary = value; }
        public List<REFERENCE> Reference { get => reference; set => reference = value; }
        public OTHER Other { get => other; set => other = value; }
    }
}
