using TaxMaster.Infra.Entities;

namespace TaxMaster.Infra
{
    public static class AnnualReportConfiguration
    {
        public static int Year { get; set; }

        public static FamilyStatus FamilyStatus { get; set; }

        public static User TheRegisteredPartner { get; set; }

        public static User Partner { get; set; }
    }
}
