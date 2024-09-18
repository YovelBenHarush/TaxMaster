using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System.Text.RegularExpressions;

namespace TaxMaster.Infra.Parsers
{
    public class Tax106FileParser
    {
        public Tax106File Parse106File(string pdfPath)
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
                    Tax106File tax106 = new Tax106File();
                    for (int j = 0; j < lines.Length; j++)
                    {
                        var hebrewText = lines[j];
                        // You can further split by spaces or tabs to identify columns
                        string[] columns = hebrewText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        if (hebrewText.Contains("(158/172)"))
                        {
                            tax106._158_172 = ParseNumber(columns[2]);
                        }
                        else if (hebrewText.Contains("(042)"))
                        {
                            tax106._042 = ParseNumber(columns[4]);
                        }
                        else if (hebrewText.Contains("(244/245)"))
                        {
                            tax106._244_245 = ParseNumber(columns[3]);
                        }
                        else if (hebrewText.Contains("(218/219)"))
                        {
                            tax106._218_219 = ParseNumber(columns[3]);
                        }
                        else if (hebrewText.Contains("(086/045)"))
                        {
                            tax106._086_045 = ParseNumber(columns[10]);
                        }
                        else if (hebrewText.Contains("(248/249)"))
                        {
                            tax106._248_249 = ParseNumber(columns[7]);
                        }
                        else if (hebrewText.Contains("(037/237)"))
                        {
                            tax106._037_237 = ParseNumber(columns[11]);
                        }
                    }

                    return tax106;
                }
            }

            return new Tax106File();
        }

        public long ParseNumber(string text)
        {
            // Remove all non-numeric characters
            string number = Regex.Replace(text, "[^0-9]", "");

            // Parse the number
            return long.Parse(number);
        }
    }
}
