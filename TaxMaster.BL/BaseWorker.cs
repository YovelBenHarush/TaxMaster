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


        protected void SaveToOutputDir(string filePath)
        {

        }
    }
}

