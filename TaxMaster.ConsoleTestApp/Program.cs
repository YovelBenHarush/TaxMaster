using TaxMaster.BL;
using TaxMaster.Infra;
using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra.Parsers;

public static class Program
{
    public static async Task Main(string[] args)
    {
        //var esppFidelityClient = new ESPPFidelityParser();
        //var CustomTransactionSummaryFilePath = "C:\\Users\\lubalibu\\Documents\\fidlity\\CustomTransactionsummary2021.pdf";
        //var sellTransactions = esppFidelityClient.ParseStockSalesTranscations(CustomTransactionSummaryFilePath);
        //var esppDivident = esppFidelityClient.ParseDividend(CustomTransactionSummaryFilePath);

        var sellTransaction1 = new SellTransaction
        {
            ShareIndex = "MSFT",
            PurchasePriceInUSD = 1000,
            SellPriceInUSD = 1500,
            PurchaseDate = DateTime.Now - TimeSpan.FromDays(50),
            SellDate = DateTime.Now
        };

        var sellTransaction2 = new SellTransaction
        {
            ShareIndex = "MSFT",
            PurchasePriceInUSD = 1000,
            SellPriceInUSD = 1500,
            PurchaseDate = DateTime.Now - TimeSpan.FromDays(200),
            SellDate = DateTime.Now - TimeSpan.FromDays(180)
        };

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();

        var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax([sellTransaction1, sellTransaction2]);

        var parser = new Form1325Parser();
        parser.Generate1325Forms(sellTransactionsWithTaxMetadata, "123", Directory.GetCurrentDirectory());
    }
}