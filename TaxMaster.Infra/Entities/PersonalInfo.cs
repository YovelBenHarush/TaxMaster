namespace TaxMaster.Infra.Entities
{
    public enum FamilyStatus
    {
        Single,
        Married
    }
    public enum Gender
    {
        Female,
        Male
    }

    public class User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ID { get; set; } = string.Empty;
        public string DisplayName => $"{FirstName} {LastName}";
        public Gender Gender { get; set; }
        public TaxBirthPaymentFile BirthPayment { get; set; } = new TaxBirthPaymentFile();

        public List<InsuranceEntry> LifeInsurences { get; set; } = new List<InsuranceEntry>();

        public EsppObject EsppObject { get; set; } = new EsppObject();

        public RsuEsopObject RsuEsopObject { get; set; } = new RsuEsopObject();

        public List<Tax106File> Tax106Files { get; set; } = new List<Tax106File>();

        public Tax106File MargeTax106File { get; set; } = new Tax106File();

    }
}
