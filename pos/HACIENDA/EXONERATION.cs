using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class EXONERATION
    {
        string documentType;
        string documentNumber;
        string institutionName;
        DateTime issueDate;
        decimal taxAmount;
        decimal purchasePercentage;

        public string DocumentType { get => documentType; set => documentType = value; }
        public string DocumentNumber { get => documentNumber; set => documentNumber = value; }
        public string InstitutionName { get => institutionName; set => institutionName = value; }
        public DateTime IssueDate { get => issueDate; set => issueDate = value; }
        public decimal TaxAmount { get => taxAmount; set => taxAmount = value; }
        public decimal PurchasePercentage { get => purchasePercentage; set => purchasePercentage = value; }
    }
}
