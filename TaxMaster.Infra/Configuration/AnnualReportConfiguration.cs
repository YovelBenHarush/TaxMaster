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


        public static void SaveToOutputDir(string sourcefilePath, string fileName)
        {
            try
            {
                // Ensure that the source file exists
                if (!File.Exists(sourcefilePath))
                {
                    throw new FileNotFoundException("Source file not found", sourcefilePath);
                }

                // Copy the file to the destination
                File.Copy(sourcefilePath, GetOutputFilePath(fileName));

                Console.WriteLine("File copied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
