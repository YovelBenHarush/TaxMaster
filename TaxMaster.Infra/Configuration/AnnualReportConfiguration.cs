using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlTypes;
using TaxMaster.Infra.Entities;

namespace TaxMaster.Infra
{
    public static class ReportSettings
    {
        public static AnnualReportConfiguration Configuration = new();

        private static string baseOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        private static string configFileName = "config.json";

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
            var file = new FileInfo(GetOutputFilePath(configFileName));
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(GetOutputFilePath(configFileName), Configuration.Serialize());
        }

        public static void LoadConfiguration(string configDirRelativePath)
        {
            Configuration = AnnualReportConfiguration.Deserialize(File.ReadAllText(Path.Combine(baseOutputDir, configDirRelativePath, configFileName)));
        }

        public static void ClearConfiguration()
        {
            Configuration = new();
        }

        public static List<string> ListPersistentConfigurations(string year, string reportType)
        {
            var reports = new List<string>();
            var baseDir = new DirectoryInfo(baseOutputDir);
            if (baseDir.Exists)
            {
                foreach (var dir in baseDir.GetDirectories("*", SearchOption.AllDirectories))
                {
                    if (dir.FullName.Contains(Path.Combine(year, reportType), StringComparison.OrdinalIgnoreCase))
                    {
                        reports.Add(dir.FullName);
                    }
                }
            }

            return reports;
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

        public  EsppObject EsppObject { get; set; } = new EsppObject();

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
