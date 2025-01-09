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
        Console.WriteLine("Welcome to TaxMaster console app. Follow instructions to prepare 1325 files.");

        Broker broker = Broker.Unknown;
        var fidelityFilePath = string.Empty;
        var ibFilePath = string.Empty;

        Console.WriteLine("Select Broker: [1] Fidelity, [2] Interactive Brokers, [3] Both");
        var brokerSelection = Convert.ToInt32(Console.ReadLine());
        switch (brokerSelection)
        {
            case 1:
                broker = Broker.Fidelity;
                Console.WriteLine("Enter the path to the Fidelity PDF file");
                fidelityFilePath = Console.ReadLine();
                break;
            case 2:
                broker = Broker.IB;
                Console.WriteLine("Enter the path to the IBKR CSV file (instructions on how to generate the proper file can be found here: 'https://fintranslator.com/2022/07/11/ib-annual-statement-for-israel-tax-reporting/?fbclid=IwAR3nAZBwsx4xyYD1bn0o_A5Sqvboj3JzajbQeF2fSS0svoB6uDCv-Z6fpsE')");
                ibFilePath = Console.ReadLine();
                break;
            case 3:
                broker = Broker.Both;
                Console.WriteLine("Enter the path to the Fidelity PDF file");
                fidelityFilePath = Console.ReadLine();
                Console.WriteLine("Enter the path to the IBKR CSV file (instructions on how to generate the proper file can be found here: 'https://fintranslator.com/2022/07/11/ib-annual-statement-for-israel-tax-reporting/?fbclid=IwAR3nAZBwsx4xyYD1bn0o_A5Sqvboj3JzajbQeF2fSS0svoB6uDCv-Z6fpsE')");
                ibFilePath = Console.ReadLine();
                break;
            default:
                Console.WriteLine("Invalid broker or selection");
                return;
        }

        IEnumerable<ISellTransaction> fidelitySellTransactions = [];
        IEnumerable<ISellTransaction> ibSellTransactions = [];
        double fidelityDivident = 0;
        double ibDivident = 0;
        var esppFidelityClient = new ESPPFidelityParser();
        var IbkrClient = new IbkrEsppCsvParser();

        switch (broker)
        {
            case Broker.Fidelity:
                if (!ValidFilePath(fidelityFilePath))
                {
                    Console.WriteLine("Invalid file path");
                    return;
                }
                fidelitySellTransactions = esppFidelityClient.ParseStockSalesTranscations(fidelityFilePath);
                fidelityDivident = esppFidelityClient.ParseDividend(fidelityFilePath);
                break;
            case Broker.IB:
                if (!ValidFilePath(ibFilePath))
                {
                    Console.WriteLine("Invalid file path");
                    return;
                }
                ibSellTransactions = IbkrClient.ParseStockSalesTranscations(ibFilePath);
                ibDivident = IbkrClient.ParseDividend(ibFilePath);
                break;
            case Broker.Both:
                if (!ValidFilePath(fidelityFilePath) || !ValidFilePath(ibFilePath))
                {
                    Console.WriteLine("Invalid file path");
                    return;
                }
                fidelitySellTransactions = esppFidelityClient.ParseStockSalesTranscations(fidelityFilePath);
                fidelityDivident = esppFidelityClient.ParseDividend(fidelityFilePath);
                ibSellTransactions = IbkrClient.ParseStockSalesTranscations(ibFilePath);
                ibDivident = IbkrClient.ParseDividend(ibFilePath);
                break;
            default:
                Console.WriteLine("Invalid broker");
                return;
        }

        Console.WriteLine("\nParsed file(s) successfully.");

        var user = GetUser();

        Console.WriteLine("Generating 1325 forms...");

        var capitalGainTaxCaclulator = new CapitalGainTaxCaclulator();
        var sellTransactionsWithTaxMetadata = await capitalGainTaxCaclulator.CalculateTax(fidelitySellTransactions.Concat(ibSellTransactions));
        var parser = new Form1325Parser();
        var genratedFilesPaths = parser.Generate1325Forms(sellTransactionsWithTaxMetadata, user, Directory.GetCurrentDirectory());

        Console.WriteLine("\n=========================================================================================");
        Console.WriteLine("=========================================================================================\n");
        Console.WriteLine("Genaerated 1325 forms successfully!!!\n");
        Console.WriteLine("Generated 1325 forms:");
        Console.WriteLine($"[*] {genratedFilesPaths.FirstHalfFormPath}");
        Console.WriteLine($"[*] {genratedFilesPaths.SecondHalfFormPath}");
        Console.WriteLine("\n=========================================================================================");
        Console.WriteLine("=========================================================================================");

    }

    private static bool ValidFilePath(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !Path.Exists(filePath))
        {
            return false;
        }

        return true;
    }

    private static User GetUser()
    {
        Console.WriteLine("Gathering user information...");
        Console.WriteLine("Enter user id:");
        var id = Console.ReadLine();
        Console.WriteLine("Enter user first Name:");
        var firstName = Console.ReadLine();
        Console.WriteLine("Enter user last Name:");
        var lastName = Console.ReadLine();

        // Test code, to prevent it from crashing at the moments in case of bad user input.
        // Need to add an error handling for case of bad user info

        id = id ?? "123456789";
        firstName = firstName ?? "Israel";
        lastName = lastName ?? "Israeli";

        Console.WriteLine("Gathered user information successfully.");
        return new User(id, firstName, lastName);
    }
}