using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;

namespace TaxMaster.BL
{
    public class ReportWorker
    {
        public List<ReportDefinition> GetExistingReports(string year, ReportType reportType)
        {
            var existingReports = ReportSettings.ListPersistentConfigurations($"{year}", reportType.ToString());

            return existingReports.Select(reportPath => CreateNewReports(year, reportPath, reportType)).ToList();
        }

        public ReportDefinition CreateNewReports(string year, string reportPath, ReportType reportType)
        {
            var directoryParts = reportPath.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);

            return new ReportDefinition
            {
                DisplayName = Path.Combine(directoryParts[^3], directoryParts[^2], directoryParts[^1]),
                Year = year,
                ReportPath = reportPath,
                ReportType = reportType
            };
        }
    }

    public class ReportDefinition
    {
        public ReportType ReportType { get; set; }
        public string DisplayName { get; set; }
        public string Year { get; set; }
        public string ReportPath { get; set; }
    }

    public enum ReportType
    {
        AnnualReport,
        HalfAnnualReport
    }
}
