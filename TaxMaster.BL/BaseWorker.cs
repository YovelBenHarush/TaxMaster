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
            return ReportSettings.GetOutputDir();
        }

        public string GetOutputFilePath(string fileName)
        {
            return ReportSettings.GetOutputFilePath(fileName);
        }


        protected void SaveToOutputDir(string sourcefilePath, string filePath)
        {
            ReportSettings.SaveToOutputDir(sourcefilePath, filePath);
        }
    }
}

