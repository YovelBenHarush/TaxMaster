using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;

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

        public List<IncomeDetailsProperties> GetIncomeDetails()
        {
            return new List<IncomeDetailsProperties>
            {
                new IncomeDetailsProperties
                {
                    RegisteredPartnerIncomeDetails = new IncomeDetails
                    {
                        Key = "סעיף 158",
                        Value = "45667",
                        Explanation = "חלק מטופס 106"
                    },
                    PartnerIncomeDetails = new IncomeDetails
                    {
                       Key = "סעיף 172",
                        Value = "45667",
                        Explanation = "חלק מטופס 106"
                    }
                },
                new IncomeDetailsProperties
                {
                    RegisteredPartnerIncomeDetails = new IncomeDetails
                    {
                        Key = "סעיף 244",
                        Value = "45667",
                        Explanation = "חלק מטופס 106"
                    },
                    PartnerIncomeDetails = new IncomeDetails
                    {
                       Key = "סעיף 245",
                        Value = "45667",
                        Explanation = "חלק מטופס 106"
                    }
                }
            };
        }
    }

    public class AnnualReportDefinition
    {
        public string DisplayName { get; set; }
        public string Year { get; set; }
        public string ReportPath { get; set; }
    }

    public class IncomeDetailsProperties
    {
        public IncomeDetails RegisteredPartnerIncomeDetails { get; set; }
        public IncomeDetails PartnerIncomeDetails { get; set; }
    }

    public class IncomeDetails
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Explanation { get; set; }
    }
}
