using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaxMaster.Infra.Entities;

namespace TaxMaster.Infra
{
    public static class ReportSettings
    {
        public static AnnualReportConfiguration Configuration = new();

        private static string baseOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");

        public static string GetOutputDir()
        {
            return Path.Combine(baseOutputDir, ReportSettings.Configuration.RegisteredPartner.ID, ReportSettings.Configuration.Year.ToString(), "AnnualReport");
        }

        public static string GetOutputFilePath(string fileName)
        {
            return Path.Combine(GetOutputDir(), fileName);
        }


        public static void SaveToOutputDir(string filePath)
        {

        }

        public static void SaveConfiguration()
        {
            var file = new FileInfo(GetOutputFilePath("config.json"));
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(GetOutputFilePath("config.json"), Configuration.Serialize());
        }

        public static void LoadConfiguration()
        {
            Configuration = AnnualReportConfiguration.Deserialize(File.ReadAllText(GetOutputFilePath("config.json")));
        }
    }

    public class AnnualReportConfiguration
    {
        public int Year { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public User RegisteredPartner { get; set; } = new User();

        public User Partner { get; set; } = new User();

        public TaxBirthPaymentFile BirthPayment { get; set; } = new TaxBirthPaymentFile();

        public LifeInsurences LifeInsurences { get; set; } = new LifeInsurences();

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static AnnualReportConfiguration Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<AnnualReportConfiguration>(json);
        }
    }
}
