using TaxMaster.BL;
using TaxMaster.Infra;
using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra.Parsers;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var esppFidelityClient = new ESPPFidelityParser();
        var CustomTransactionSummaryFilePath = "C:\\Users\\lubalibu\\Documents\\fidlity\\CustomTransactionsummary2021.pdf";
        var sellTransactions = esppFidelityClient.ParseStockSalesTranscations(CustomTransactionSummaryFilePath);
        var esppDivident = esppFidelityClient.ParseDividend(CustomTransactionSummaryFilePath);

        var sellTransaction = new SellTransaction
        {
            ShareIndex = "MSFT",
            PurchasePriceInUSD = 1000,
            SellPriceInUSD = 1500,
            PurchaseDate = DateTime.Now - TimeSpan.FromDays(50),
            SellDate = DateTime.Now
        };

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();

        var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransactions);

        var parser = new Form1325Parser();
        parser.Generate1325Form(sellTransactionsWithTaxMetadata, Directory.GetCurrentDirectory());
    }
}