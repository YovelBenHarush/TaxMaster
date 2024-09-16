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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ID { get; set; }
        public Gender Gender { get; set; }
    }
}
