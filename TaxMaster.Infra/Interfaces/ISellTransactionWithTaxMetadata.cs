using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.Infra.Interfaces
{
    public interface ISellTransactionWithTaxMetadata : ISellTransaction
    {
        public double PurchasePriceInILS { get; }

        public double AdjustedPurchasePriceInILS { get; }

        public double SellPriceInILS { get; }

        public double TaxableProfitInILS { get; }

        public double ExchangeCurrencyAtPurchaseDate { get; set; }

        public double ExchangeCurrencyAtSellDate { get; set; }

        public double TaxRate { get; set; }

        public double ExchangeRate { get; set; }

        public double TaxAmountToPay { get; }
    }

    public class SellTransactionWithTaxMetadata : SellTransaction, ISellTransactionWithTaxMetadata
    {
        public SellTransactionWithTaxMetadata(ISellTransaction sellTransaction, double taxRate = 0.25)
        {
            ShareIndex = sellTransaction.ShareIndex;
            PurchasePriceInUSD = sellTransaction.PurchasePriceInUSD;
            SellPriceInUSD = sellTransaction.SellPriceInUSD;
            PurchaseDate = sellTransaction.PurchaseDate;
            SellDate = sellTransaction.SellDate;
            TaxRate = taxRate;
        }

        public double PurchasePriceInILS => PurchasePriceInUSD * ExchangeCurrencyAtPurchaseDate;

        public double AdjustedPurchasePriceInILS => PurchasePriceInUSD * ExchangeCurrencyAtPurchaseDate * ExchangeRate;

        public double SellPriceInILS => SellPriceInUSD * ExchangeCurrencyAtSellDate;

        public double TaxableProfitInILS => SellPriceInILS - AdjustedPurchasePriceInILS;

        public double ExchangeCurrencyAtPurchaseDate { get; set; }

        public double ExchangeCurrencyAtSellDate { get; set; }

        public double TaxRate { get; set; }

        public double ExchangeRate { get; set; }

        public double TaxAmountToPay => TaxableProfitInILS * TaxRate;
    }
}
