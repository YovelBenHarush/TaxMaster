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
        public Gender Gender { get; set; }
    }
}
