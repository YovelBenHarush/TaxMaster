using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Globalization;
using System.Transactions;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.Infra;

public class RsuEsopParser
{
    private static readonly string DateFormat = "dd/MM/yyyy";

    public IEnumerable<IRsuSellTransaction> ParseStockSalesTranscations(string pdfPath)
    {
        var transactions = new List<IRsuSellTransaction>();

        if (File.Exists(pdfPath))
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                // Create a strategy for parsing text
                var strategy = new SimpleTextExtractionStrategy();

                // Extract text from the page
                string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(1), strategy);

                // Split the extracted content into lines (for table parsing)
                string[] lines = text.Split('\n');
                int j = 0;

                do
                {
                    var line = lines[j];
                    j++;
                    if (line.StartsWith("הרבעה םוכס"))
                    {
                        break;
                    }
                }
                while (j < lines.Length);

                while (j < lines.Length)
                {
                    var transcationLine = lines[j];
                    if (transcationLine.StartsWith("Name"))
                    {
                        break;
                    }

                    var rsuSellTransaction = new RsuSellTransaction();
                    rsuSellTransaction.ShareIndex = "MSFT";
                    // You can further split by spaces or tabs to identify columns
                    string[] columns = transcationLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    rsuSellTransaction.SellDate = DateTime.ParseExact(columns[1], DateFormat, CultureInfo.InvariantCulture);
                    rsuSellTransaction.GrantDate = DateTime.ParseExact(columns[2], DateFormat, CultureInfo.InvariantCulture);
                    rsuSellTransaction.OrdinaryIncomeInNis = double.Parse(columns[9]);
                    rsuSellTransaction.PurchasePriceInNis = double.Parse(columns[9])+ double.Parse(columns[12]);
                    transactions.Add(rsuSellTransaction);
                    j++;
                }
            }
        }
        else
        {
            throw new FileNotFoundException("File does not exist.");
        }

        return transactions;
    }

    public (double dividendInNis, double dividendTaxInNis) ParseDividend(string pdfPath)
    {
        double dividendInNis = 0;
        double dividendTaxInNis =0;
        if (File.Exists(pdfPath))
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                // Create a strategy for parsing text
                var strategy = new SimpleTextExtractionStrategy();

                // Extract text from the page
                string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(1));

                // Split the extracted content into lines (for table parsing)
                string[] lines = text.Split('\n');
                int j = 0;

                do
                {
                    var line = lines[j];
                    j++;
                    if (line.StartsWith("םידספה זוזיק ינפל דנדיבידמ הסנכה 1"))
                    {
                        break;
                    }
                }
                while (j < lines.Length);

                dividendInNis = double.Parse(lines[j]);

                do
                {
                    var line = lines[j];
                    if (line.Contains("ץוח ןרקמ / דנדיבידמ רוקמב הכונש סמ 6"))
                    {
                        string[] columns = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        dividendTaxInNis = double.Parse(columns.Last());
                        break;
                    }
                    j++;
                }
                while (j < lines.Length);
            }
        }
        else
        {
            throw new FileNotFoundException("File does not exist.");
        }

        return (dividendInNis, dividendTaxInNis);
    }
}
