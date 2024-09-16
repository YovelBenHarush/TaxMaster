using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.BL
{
    public class AnnualReportWorker
    {
        public List<AnnualReportDefinition> GetExistingAnnualReports(int year)
        {

            if(year >= 2022)
            {
                return new List<AnnualReportDefinition>
                {
                    new AnnualReportDefinition
                    {
                        DisplayName = $"{year} Annual Report",
                        Year = $"{year}",
                        ReportPath = $"C:\\Reports\\{year}\\AnnualReport.pdf"
                    }
                };
            }

            if (year >= 2020)
            {
                return new List<AnnualReportDefinition>
                {
                    new AnnualReportDefinition
                    {
                        DisplayName = $"{year} 023695874",
                        Year = $"{year}",
                        ReportPath = "C:\\Reports\\2020\\AnnualReport.pdf"
                    },
                    new AnnualReportDefinition
                    {
                        DisplayName = $"{year} 032698558",
                        Year = $"{year}",
                        ReportPath = "C:\\Reports\\2020\\AnnualReport.pdf"
                    },
                    new AnnualReportDefinition
                    {
                        DisplayName = $"{year} 023688548",
                        Year = $"{year}",
                        ReportPath = "C:\\Reports\\2020\\AnnualReport.pdf"
                    }
                };
            }

            return new List<AnnualReportDefinition>();
        }

        public AnnualReportDefinition CreateNewAnnualReports(string year)
        {
            return new AnnualReportDefinition
            {
                DisplayName = $"{year} 032698558",
                Year = year,
                ReportPath = $"C:\\Reports\\{year}\\AnnualReport.pdf"
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
