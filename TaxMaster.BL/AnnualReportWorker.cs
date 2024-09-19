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
            var registeredPartner = ReportSettings.Configuration.RegisteredPartner;
            var partner = ReportSettings.Configuration.Partner;
            var incomeDetails = new List<IncomeDetailsProperties>();


            // Add 106 data
            var registeredPartner106 = registeredPartner?.Tax106FilesWrapper.GetMerged106();
            var partner106 = partner?.Tax106FilesWrapper.GetMerged106();

            // Add _158_172
            AddIncomeDetail("158", "172", registeredPartner106?._158_172, partner106?._158_172, "חלק מטופס 106", incomeDetails);

            // Add _042
            AddIncomeDetail("042", "042", registeredPartner106?._042, partner106?._042, "חלק מטופס 106", incomeDetails);

            // Add _244_245
            AddIncomeDetail("244", "245", registeredPartner106?._244_245, partner106?._244_245, "חלק מטופס 106", incomeDetails);

            // Add _218_219
            AddIncomeDetail("218", "219", registeredPartner106?._218_219, partner106?._218_219, "חלק מטופס 106", incomeDetails);

            // Add _086_045
            AddIncomeDetail("086", "045", registeredPartner106?._086_045, partner106?._086_045, "חלק מטופס 106", incomeDetails);

            // Add _248_249
            AddIncomeDetail("248", "249", registeredPartner106?._248_249, partner106?._248_249, "חלק מטופס 106", incomeDetails);

            // Add _037_237
            AddIncomeDetail("037", "237", registeredPartner106?._037_237, partner106?._037_237, "חלק מטופס 106", incomeDetails);

            // Add _193_093
            AddIncomeDetail("193", "093", registeredPartner106?._193_093, partner106?._193_093, "חלק מטופס 106", incomeDetails);


            // Birth Payment 
            var registeredPartnerBirthPayment = registeredPartner?.BirthPayment;
            var partnerBirthPayment = partner?.BirthPayment;

            AddIncomeDetail("250", "270", registeredPartnerBirthPayment?.Amount, partnerBirthPayment?.Amount, "דמי לידה מחושבים מטופס דמי הלידה", incomeDetails);

            // Life Insurances

            // Donations

            return incomeDetails;
        }

        private void AddIncomeDetail<T>(string registeredKey, string partnerKey, T? registeredValue, T? partnerValue, string explanation, List<IncomeDetailsProperties> incomeDetails)
            where T : struct, IComparable
        {
            var incomeDetailsProperties = new IncomeDetailsProperties();

            // Handle the registered partner income details
            if (registeredValue.HasValue && !IsZero(registeredValue.Value))
            {
                incomeDetailsProperties.RegisteredPartnerIncomeDetails = new IncomeDetails
                {
                    Key = $"סעיף {registeredKey}",
                    Value = registeredValue.Value.ToString() ?? string.Empty,
                    Explanation = explanation
                };
            }

            // Handle the partner income details
            if (partnerValue.HasValue && !IsZero(partnerValue.Value))
            {
                incomeDetailsProperties.PartnerIncomeDetails = new IncomeDetails
                {
                    Key = $"סעיף {partnerKey}",
                    Value = partnerValue.Value.ToString() ?? string.Empty,
                    Explanation = explanation
                };
            }

            incomeDetails.Add(incomeDetailsProperties);
        }

        private bool IsZero<T>(T value) where T : IComparable
        {
            return value.CompareTo(default(T)) == 0;
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
