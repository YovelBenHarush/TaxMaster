using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;

namespace TaxMaster.BL
{
    public class AnnualReportWorker
    {
        public List<AnnualReportDefinition> GetExistingAnnualReports(string year)
        {
            var existingReports = ReportSettings.ListPersistentConfigurations($"{year}", "AnnualReport");

            return existingReports.Select(reportPath => CreateNewAnnualReports(year, reportPath)).ToList();
        }

        public AnnualReportDefinition CreateNewAnnualReports(string year, string reportPath)
        {
            var directoryParts = reportPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return new AnnualReportDefinition
            {
                DisplayName = Path.Combine(directoryParts[^3], directoryParts[^2], directoryParts[^1]),
                Year = year,
                ReportPath = reportPath
            };
        }
    }

    public class AnnualReportDefinition
    {
        public string DisplayName { get; set; }
        public string Year { get; set; }
        public string ReportPath { get; set; }
    }
}
