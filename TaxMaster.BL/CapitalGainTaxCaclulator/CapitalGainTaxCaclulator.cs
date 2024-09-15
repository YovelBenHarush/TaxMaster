using TaxMaster.BL.CapitalGainTaxCaclulator.Interfaces;
using TaxMaster.Infra;

namespace TaxMaster.BL.CapitalGainTaxCaclulator
{
    public class CapitalGainTaxCaclulator
    {
        private static ExchangeCurrencyClient exchangeCurrencyClient = new();

        public async Task<ISellTransactionWithTaxMetadata> CalculateTax(ISellTransaction sellTransaction)
        {
            var result = new SellTransactionWithTaxMetadata(sellTransaction);

            result.ExchangeRate = await CalculateExchangeRate(result.PurchaseDate, result.SellDate);
            result.TaxAmount = CalculateTaxAmount(result, result.ExchangeRate);

            return result;
        }

        private async Task<double> CalculateExchangeRate(DateTime purchaseDate, DateTime sellDate)
        {
            var sellRate = await exchangeCurrencyClient.GetExchangeRate(sellDate);
            var purchaseRate = await exchangeCurrencyClient.GetExchangeRate(purchaseDate);

            var rawExchangeRate = sellRate / purchaseRate;

            return rawExchangeRate < 1 ? 1 : rawExchangeRate;
        }

        private double CalculateTaxAmount(ISellTransactionWithTaxMetadata sellTransaction, double exchangeRate)
        {
            double adjustedProfit = (sellTransaction.SellPrice - sellTransaction.PurchasePrice * exchangeRate);
            double taxAmount = adjustedProfit * sellTransaction.TaxRate;

            return taxAmount;
        }
    }
}
