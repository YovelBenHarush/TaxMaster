using iText.StyledXmlParser.Jsoup.Helper;
using System.Collections.ObjectModel;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;
using TaxMaster.UI;

namespace TaxMaster
{
    public class PersonalDetailsViewModel : BaseViewModel
    {
        public UserModel RegisteredPartner { get; set; }

        public UserModel Partner { get; set; }

        public ObservableCollection<string> MaritalStatuses { get; set; }

        private string _selectedMaritalStatus = "רווק";
        public string SelectedMaritalStatus
        {
            get => _selectedMaritalStatus;
            set
            {
                if (_selectedMaritalStatus != value)
                {
                    _selectedMaritalStatus = value;
                    if (value == "נשוי")
                    {
                        _isMarried = true;
                    }
                    else
                    {
                        _isMarried = false;
                    }

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsMarried));
                }
            }
        }

        private bool _isMarried = false;
        public bool IsMarried => _isMarried;

        public string SelectedYear => ReportSettings.Configuration.Year.ToString();

        public ObservableCollection<string> Genders { get; set; }

        public PersonalDetailsViewModel()
        {
            MaritalStatuses = new ObservableCollection<string> { "רווק", "נשוי" };
            Genders = new ObservableCollection<string> { "זכר", "נקבה" };

            SelectedMaritalStatus = MaritalStatuses.First();

            RegisteredPartner = new UserModel();
            Partner = new UserModel();
        }

        public override string Title
        {
            get => "פרטים אישיים";
            set => base.Title = value;
        }

        public async override void OnNext()
        {
            if (!Validate())
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "אנא מלא/י את כל הפרטיים האישיים", "OK");
                }
                return;
            }

            ReportSettings.Configuration.FamilyStatus = _isMarried ? FamilyStatus.Married : FamilyStatus.Single;
            ReportSettings.Configuration.RegisteredPartner = RegisteredPartner.ToUser();
            if (_isMarried)
            {
                ReportSettings.Configuration.RegisteredPartner = Partner.ToUser();
            }

            base.OnNext();
            await Shell.Current.GoToAsync(nameof(DefinitionOfForm106View));
        }

        private bool Validate()
        {
            if(string.IsNullOrEmpty(RegisteredPartner.FirstName) || string.IsNullOrEmpty(RegisteredPartner.LastName) || string.IsNullOrEmpty(RegisteredPartner.Id) || string.IsNullOrEmpty(RegisteredPartner.Gender))
            {
                return false;
            }
            if (IsMarried)
            {
                if (string.IsNullOrEmpty(Partner.FirstName) || string.IsNullOrEmpty(Partner.LastName) || string.IsNullOrEmpty(Partner.Id) || string.IsNullOrEmpty(Partner.Gender))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
