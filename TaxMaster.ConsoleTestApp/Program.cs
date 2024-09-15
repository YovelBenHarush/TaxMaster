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
                double exchangeRate = await exchangeCurrencyClient.GetExchangeRateAsync(DateTime.Now - TimeSpan.FromDays(-i));
                Console.WriteLine($"Exchange rate: {exchangeRate}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get exchange rate: {ex.Message}");
            }
        }
    }
}