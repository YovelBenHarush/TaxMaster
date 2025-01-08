using TaxMaster.BL;
using TaxMaster.Infra;
using TaxMaster.Infra.Contracts;
using TaxMaster.Infra.Entities;
using TaxMaster.Infra.Interfaces;
using TaxMaster.Infra.Parsers;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Broker broker = Broker.Unknown;
        var filePath = string.Empty;

        if (args.Length == 0)
        {
            Console.WriteLine("Select Broker: [1] Fidelity, [2] IB");
            var brokerSelection = Console.ReadLine();
            if (brokerSelection == "1")
            {
                broker = Broker.Fidelity;
                Console.WriteLine("Enter the path to the Fidelity PDF file");
            }
            else if (brokerSelection == "2")
            {
                broker = Broker.IB;
                Console.WriteLine("Enter the path to the IB Excel file");
            }
            else
            {
                Console.WriteLine("Invalid broker");
                return;
            }

            filePath = Console.ReadLine();
            if (!Path.Exists(filePath))
            {
                Console.WriteLine("Invalid file path");
                return;
            }
        }

        if (args.Length == 1)
        {
            Console.WriteLine("Tax Master Console usage:");
            Console.WriteLine("Broker");
            Console.WriteLine("File path");
            return;
        }

        if (args.Length > 2)
        {
            Console.WriteLine("Invalid number of arguments");
            return;
        }

        IEnumerable<ISellTransaction> sellTransactions;
        double esppDivident = 0;
        switch (broker) {
            case Broker.Fidelity:
                var esppFidelityClient = new ESPPFidelityParser();
                sellTransactions = esppFidelityClient.ParseStockSalesTranscations(filePath);
                esppDivident = esppFidelityClient.ParseDividend(filePath);
                break;
            case Broker.IB:
                Console.WriteLine("IB broker not yet supported");
                return;
            default:
                Console.WriteLine("Invalid broker");
                return;
        }

        var user = GetUser();

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();
        var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(sellTransactions);
        var parser = new Form1325Parser();
        parser.Generate1325Forms(sellTransactionsWithTaxMetadata, user, Directory.GetCurrentDirectory());
    }

    private static User GetUser()
    {
        return new User
        {
            ID = "12345678",
            FirstName = "Test",
            LastName = "Test",
        };
    }
}