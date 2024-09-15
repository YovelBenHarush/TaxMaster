using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.BL.CapitalGainTaxCaclulator
{
    public class CapitalGainTaxCaclulator
    {
        private static ExchangeCurrencyClient exchangeCurrencyClient = new();

        public async Task<ISellTransactionWithTaxMetadata> CalculateTax(ISellTransaction sellTransaction)
        {
            var result = new SellTransactionWithTaxMetadata(sellTransaction);

            result.ExchangeRate = await CalculateExchangeRate(result.PurchaseDate, result.SellDate, result);

            return result;
        }

        private async Task<double> CalculateExchangeRate(DateTime purchaseDate, DateTime sellDate, ISellTransactionWithTaxMetadata transaction)
        {
            var sellRate = await exchangeCurrencyClient.GetExchangeRate(sellDate);
            var purchaseRate = await exchangeCurrencyClient.GetExchangeRate(purchaseDate);

            transaction.ExchangeCurrencyAtPurchaseDate = purchaseRate;
            transaction.ExchangeCurrencyAtSellDate = sellRate;

            var rawExchangeRate = sellRate / purchaseRate;

            return rawExchangeRate < 1 ? 1 : rawExchangeRate;
        }
    }
}
