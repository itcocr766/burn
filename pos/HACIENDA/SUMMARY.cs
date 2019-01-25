using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.HACIENDA
{
    public class SUMMARY
    {
        string currency;
        decimal exchangeRate;
        decimal totalTaxedService;
        decimal totalExemptService;
        decimal totalTaxedGoods;
        decimal totalExemptGoods;
        decimal totalTaxed;
        decimal totalExempt;
        decimal totalSale;
        decimal totalDiscounts;
        decimal totalNetSale;
        decimal totalTaxes;
        decimal totalVoucher;

        public string Currency { get => currency; set => currency = value; }
        public decimal ExchangeRate { get => exchangeRate; set => exchangeRate = value; }
        public decimal TotalTaxedService { get => totalTaxedService; set => totalTaxedService = value; }
        public decimal TotalExemptService { get => totalExemptService; set => totalExemptService = value; }
        public decimal TotalTaxedGoods { get => totalTaxedGoods; set => totalTaxedGoods = value; }
        public decimal TotalExemptGoods { get => totalExemptGoods; set => totalExemptGoods = value; }
        public decimal TotalTaxed { get => totalTaxed; set => totalTaxed = value; }
        public decimal TotalExempt { get => totalExempt; set => totalExempt = value; }
        public decimal TotalSale { get => totalSale; set => totalSale = value; }
        public decimal TotalDiscounts { get => totalDiscounts; set => totalDiscounts = value; }
        public decimal TotalNetSale { get => totalNetSale; set => totalNetSale = value; }
        public decimal TotalTaxes { get => totalTaxes; set => totalTaxes = value; }
        public decimal TotalVoucher { get => totalVoucher; set => totalVoucher = value; }
    }
}
