using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.BL.CapitalGainTaxCaclulator.Interfaces
{
    public interface ISellTransactionWithTaxMetadata : ISellTransaction
    {
        public double TaxRate { get; set; }

        public double ExchangeRate { get; set; }

        public double TaxAmount { get; set; }
    }

    public class SellTransactionWithTaxMetadata : SellTransaction, ISellTransactionWithTaxMetadata
    {
        public SellTransactionWithTaxMetadata(ISellTransaction sellTransaction, double taxRate = 0.25)
        {
            ShareIndex = sellTransaction.ShareIndex;
            PurchasePrice = sellTransaction.PurchasePrice;
            SellPrice = sellTransaction.SellPrice;
            PurchaseDate = sellTransaction.PurchaseDate;
            SellDate = sellTransaction.SellDate;
            TaxRate = taxRate;
        }

        public double TaxRate { get; set; }

        public double ExchangeRate { get; set; }

        public double TaxAmount { get; set; }
    }
}
