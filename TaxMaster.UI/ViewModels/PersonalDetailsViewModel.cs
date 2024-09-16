using iText.StyledXmlParser.Jsoup.Helper;
using System.Collections.ObjectModel;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;
using TaxMaster.UI;

namespace TaxMaster
{
    public class PersonalDetailsViewModel : BaseViewModel
    {
        private bool isMarried;

        public UserModel TheRegisteredPartner { get; set; }

        public UserModel Partner { get; set; }


        public ObservableCollection<int> Years { get; set; }
        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> FamilyStatues { get; set; }
        private string _selectedFamilyStatus;
        public string SelectedFamilyStatus
        {
            get => _selectedFamilyStatus;
            set
            {
                if (_selectedFamilyStatus != value)
                {
                    _selectedFamilyStatus = value;
                    if (value == "נשוי")
                    {
                        isMarried = true;
                    }
                    else
                    {
                        isMarried = false;
                    }

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsMarried));
                }
            }
        }

        public bool IsMarried => isMarried;
        public ObservableCollection<string> Genders { get; set; }

        public PersonalDetailsViewModel()
        {
            Years = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year - 6, 7).Reverse());
            FamilyStatues = new ObservableCollection<string> { "רווק", "נשוי" };
            Genders = new ObservableCollection<string> { "ז", "נ" };

            SelectedFamilyStatus = FamilyStatues.First();
            SelectedYear = Years.First();

            TheRegisteredPartner = new UserModel();
            Partner = new UserModel();

            IsPreviousEnabled = true;
        }

        public async override void OnNext()
        {
            if (!IsValidate())
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "אנא מלא את כל הפרטיים האישיים לפי המשך התהליך", "Ok");
                }
                return;
            }

            AnnualReportConfiguration.Year = SelectedYear;
            AnnualReportConfiguration.FamilyStatus = isMarried ? FamilyStatus.Married : FamilyStatus.Single;
            AnnualReportConfiguration.RegisteredPartner = TheRegisteredPartner.ToUser();
            if (isMarried)
            {
                AnnualReportConfiguration.RegisteredPartner = Partner.ToUser();
            }

            await Shell.Current.GoToAsync(nameof(DefinitionOfForm106View));
        }

        private bool IsValidate()
        {
            if(string.IsNullOrEmpty(TheRegisteredPartner.FirstName) || string.IsNullOrEmpty(TheRegisteredPartner.LastName) || string.IsNullOrEmpty(TheRegisteredPartner.ID) || string.IsNullOrEmpty(TheRegisteredPartner.Gender))
            {
                return false;
            }
            if (IsMarried)
            {
                if (string.IsNullOrEmpty(Partner.FirstName) || string.IsNullOrEmpty(Partner.LastName) || string.IsNullOrEmpty(Partner.ID) || string.IsNullOrEmpty(Partner.Gender))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
