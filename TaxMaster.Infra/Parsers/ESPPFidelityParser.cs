using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.IO;

namespace TaxMaster.Infra;

public class ESPPFidelityParser
{
    public void ParsePdf(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Open the PDF document
            PdfDocument document = PdfReader.Open(filePath, PdfDocumentOpenMode.Import);

            // Iterate through the pages
            for (int i = 0; i < document.PageCount; i++)
            {
                PdfPage page = document.Pages[i];
                // Process each page (e.g., extract text, images, etc.)
                // For demonstration, we'll just print the page number
                Console.WriteLine($"Processing page {i + 1}");
            }
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
    }

}
