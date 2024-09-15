using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using TaxMaster.Infra.Interfaces;

namespace TaxMaster.Infra;

public class ESPPFidelityParser
{
    public IEnumerable<ISellTransaction> ParseStockSalesTranscations(string pdfPath)
    {
        var transactions = new List<ISellTransaction>();
        if (File.Exists(pdfPath))
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                // Create a strategy for parsing text
                var strategy = new SimpleTextExtractionStrategy();

                // Extract text from the page
                string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(4), strategy);

                // Split the extracted content into lines (for table parsing)
                string[] lines = text.Split('\n');
                int j = 0;

                do
                {
                    var line = lines[j];
                    j++;
                    if (line.StartsWith("Date sold or transferred "))
                    {
                        break;
                    }
                }
                while (j < lines.Length);

                while (j < lines.Length)
                {
                    var transcationLine = lines[j];
                    if (transcationLine.StartsWith("*"))
                    {
                        break;
                    }

                    var sellTransaction = new SellTransaction();
                    sellTransaction.ShareIndex = "MSFT";
                    // You can further split by spaces or tabs to identify columns
                    string[] columns = transcationLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    sellTransaction.SellDate = DateTime.Parse(columns[0]);
                    sellTransaction.PurchaseDate = DateTime.Parse(columns[1]);
                    sellTransaction.PurchasePriceInUSD = double.Parse(columns[3].Replace("$", ""));
                    sellTransaction.SellPriceInUSD = double.Parse(columns[4].Replace("$", ""));
                    transactions.Add(sellTransaction);
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

    public double ParseDividend(string pdfPath)
    {
        double dividend = 0;
        if (File.Exists(pdfPath))
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                // Create a strategy for parsing text
                var strategy = new SimpleTextExtractionStrategy();

                // Extract text from the page
                string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(2), strategy);

                // Split the extracted content into lines (for table parsing)
                string[] lines = text.Split('\n');
                int j = 0;

                do
                {
                    var line = lines[j];
                    j++;
                    if (line.StartsWith("Transaction date"))
                    {
                        break;
                    }
                }
                while (j < lines.Length);

                while (j < lines.Length)
                {
                    var line = lines[j];
                    if (line.StartsWith("2 / 6"))
                    {
                        break;
                    }

                    string[] columns = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    dividend += double.Parse(columns[4].Replace("$", ""));
                    j++;
                }
            }
        }
        else
        {
            throw new FileNotFoundException("File does not exist.");
        }

        return dividend;
    }
}
