using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class DETAIL
    {
        int number;
        CODE code;
        decimal quantity;
        string unitOfMeasure;
        string commercialUnitOfMeasure;
        string detail;
        decimal unitPrice;
        decimal totalAmount;
        decimal discount;
        string natureOfDiscount;
        decimal subTotal;
        List<TAX> tax;
        decimal totalLineAmount;

        public int Number { get => number; set => number = value; }
        public decimal Quantity { get => quantity; set => quantity = value; }
        public string UnitOfMeasure { get => unitOfMeasure; set => unitOfMeasure = value; }
        public string CommercialUnitOfMeasure { get => commercialUnitOfMeasure; set => commercialUnitOfMeasure = value; }
        public string Detail { get => detail; set => detail = value; }
        public decimal UnitPrice { get => unitPrice; set => unitPrice = value; }
        public decimal TotalAmount { get => totalAmount; set => totalAmount = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public string NatureOfDiscount { get => natureOfDiscount; set => natureOfDiscount = value; }
        public decimal SubTotal { get => subTotal; set => subTotal = value; }
        public decimal TotalLineAmount { get => totalLineAmount; set => totalLineAmount = value; }
        public CODE Code { get => code; set => code = value; }
        public List<TAX> Tax { get => tax; set => tax = value; }
    }
}
