using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;

namespace TaxMaster.Infra.Parsers
{
    public class BirthPaymentFileParser
    {
        public TaxBirthPaymentFile ParseBirthPaymentFile(string pdfPath)
        {
            using (PdfReader pdfReader = new PdfReader(pdfPath))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            {
                // Loop through the pages
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    // Create a strategy for parsing text
                    var strategy = new SimpleTextExtractionStrategy();

                    // Extract text from the page
                    string text = PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(i), strategy);

                    // Split the extracted content into lines (for table parsing)
                    string[] lines = text.Split('\n');
                    TaxBirthPaymentFile taxBirthPaymentFile = new TaxBirthPaymentFile();
                    for (int j = 0; j < lines.Length; j++)
                    {
                        var hebrewText = lines[j];
                        // You can further split by spaces or tabs to identify columns
                        string[] columns = hebrewText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (hebrewText.Contains("ש י ל מ נ ו ל ך ג מ ל א ו ת א מ ה ו ת ב ר ו ט ו ב ס ך"))
                        {
                            taxBirthPaymentFile.Amount = ParseNumber(columns[28]);
                        }
                        else if (hebrewText.Contains("מ ה ת ג מ ו ל י ם ב ר ו ט ו נ ו כ ה מ ס ה כ נ ס ה ב ס ך"))
                        {
                            taxBirthPaymentFile.Tax = ParseNumber(columns[29]);
                        }
                    }

                    return taxBirthPaymentFile;
                }
            }

            return new TaxBirthPaymentFile();
        }

        public double ParseNumber(string text)
        {
            // Remove all non-numeric characters
            string number = Regex.Replace(text, "[^0-9]", "");

            // Parse the number
            return double.Parse(number);
        }
    }
}
