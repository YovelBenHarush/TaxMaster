using TaxMaster.Infra.Entities;

namespace TaxMaster.Infra
{
    public static class AnnualReportConfiguration
    {
        public static int Year { get; set; }

        public static FamilyStatus FamilyStatus { get; set; }

        public static User RegisteredPartner { get; set; } = new User();

        public static User Partner { get; set; } = new User();
    }
}
