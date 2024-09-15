using TaxMaster.BL.CapitalGainTaxCaclulator;
using TaxMaster.BL.CapitalGainTaxCaclulator.Interfaces;
using TaxMaster.TaxMaster.Infra;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var exchangeCurrencyClient = new ExchangeCurrencyClient();

        for (int i = 0; i < 10; i++)
        {
            try
            {
                double exchangeRate = await exchangeCurrencyClient.GetExchangeRate(DateTime.Now - TimeSpan.FromDays(-i));
                Console.WriteLine($"Exchange rate: {exchangeRate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get exchange rate: {ex.Message}");
            }
        }

        var sellTransaction = new SellTransaction
        {
            ShareIndex = "MSFT",
            PurchasePrice = 1000,
            SellPrice = 1500,
            PurchaseDate = DateTime.Now - TimeSpan.FromDays(50),
            SellDate = DateTime.Now
        };

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();

        ISellTransactionWithTaxMetadata sellTransactionWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransaction);

        Console.WriteLine(
            $"Share index: {sellTransactionWithTaxMetadata.ShareIndex}, " +
            $"Purchase price: {sellTransactionWithTaxMetadata.PurchasePrice}, " +
            $"Sell price: {sellTransactionWithTaxMetadata.SellPrice}, " +
            $"Purchase date: {sellTransactionWithTaxMetadata.PurchaseDate}, " +
            $"Sell date: {sellTransactionWithTaxMetadata.SellDate}, " +
            $"Tax rate: {sellTransactionWithTaxMetadata.TaxRate}, " +
            $"Exchange rate: {sellTransactionWithTaxMetadata.ExchangeRate}, " +
            $"Tax amount: {sellTransactionWithTaxMetadata.TaxAmount}");
    }
}