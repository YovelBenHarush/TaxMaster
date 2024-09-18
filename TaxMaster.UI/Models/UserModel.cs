using TaxMaster.Infra.Entities;

namespace TaxMaster.UI
{
    public class UserModel
    {
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
            };
        }

        public static UserModel FromUser(User user)
        {
            return new UserModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.ID,
            };
        }
        public void UpdateUser(User user)
        {
            user.FirstName = FirstName;
            user.LastName = LastName;
            user.ID = Id;
        }
    }
}
