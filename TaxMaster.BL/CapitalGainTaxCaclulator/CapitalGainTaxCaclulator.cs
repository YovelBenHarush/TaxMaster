using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra;

namespace TaxMaster.BL
{
    public class CapitalGainTaxCaclulator
    {
        private static ExchangeCurrencyClient exchangeCurrencyClient = new();

        public async Task<IEnumerable<ISellTransactionWithTaxMetadata>> CalculateTax(IEnumerable<ISellTransaction> sellTransactions)
        {
            var result = sellTransactions.Select(async sellTransaction =>
            {
                var transaction = new SellTransactionWithTaxMetadata(sellTransaction);
                transaction.ExchangeRate = await CalculateExchangeRate(transaction.PurchaseDate, transaction.SellDate, transaction);
                return transaction;
            });

            return await Task.WhenAll(result);
        }

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
