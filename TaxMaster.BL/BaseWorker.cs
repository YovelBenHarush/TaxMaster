using Microsoft.Extensions.Logging;
using System.Xml.Linq;
using TaxMaster.Infra;

namespace TaxMaster.BL
{
    public class BaseWorker
    {
        private readonly string baseOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        public ILogger Logger => LoggerConfiguration.Logger;

        public string GetOutputDir()
        {
            return Path.Combine(baseOutputDir, AnnualReportConfiguration.RegisteredPartner.ID, AnnualReportConfiguration.Year.ToString(),"AnnualReport");
        }

        public string GetOutputFilePath(string fileName)
        {
            return Path.Combine(GetOutputDir(), fileName);
        }


        protected void SaveToOutputDir(string SourcefilePath, string fileName)
        {
            try
            {
                // Ensure that the source file exists
                if (!File.Exists(SourcefilePath))
                {
                    throw new FileNotFoundException("Source file not found", SourcefilePath);
                }

                // Copy the file to the destination
                File.Copy(SourcefilePath, GetOutputFilePath(fileName));

                Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

    }
}

