using TaxMaster.Infra.Entities;

namespace TaxMaster.UI
{
    public class UserModel
    {
        public string Gender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ID { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public User ToUser()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                ID = ID,
                Gender = Gender == "ז" ? Infra.Entities.Gender.Male : Infra.Entities.Gender.Female,
            };
        }
    }
}
