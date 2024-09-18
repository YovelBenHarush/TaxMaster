using TaxMaster.Infra.Entities;

namespace TaxMaster.UI
{
    public class UserModel
    {
        public string Gender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Id { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public User ToUser()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                ID = Id,
                Gender = Gender == "זכר" ? Infra.Entities.Gender.Male : Infra.Entities.Gender.Female,
            };
        }

        public static UserModel FromUser(User user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.ID,
                Gender = user.Gender == Infra.Entities.Gender.Male ? "זכר" : "נקבה",
            };
        }
    }
}
