using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TaxMaster.Infra.Entities
{
    public class PersonalInfo
    {
        public FamilyStatus FamilyStatus { get; set; }

        public User TheRegisteredPartner { get; set; }

        public User Partner { get; set; }
    }

    public enum FamilyStatus
    {
        Single,
        Married
    }

    public class User
    {
        public string Gender { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ID { get; set; }
    }
}
