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

        private bool _isMarried = ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married;
        public bool IsMarried => _isMarried;

        private string _bankManagementApprovalFile = ReportSettings.Configuration.BankManagementApprovalFile;
        public string BankManagementApprovalFile
        {
            get => _bankManagementApprovalFile;
            set
            {
                if (_bankManagementApprovalFile != value)
                {
                    _bankManagementApprovalFile = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedYear => ReportSettings.Configuration.Year.ToString();

        public ObservableCollection<string> Genders { get; set; }

        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }

        public PersonalDetailsViewModel()
        {
            MaritalStatuses = new ObservableCollection<string> { "רווק", "נשוי" };
            Genders = new ObservableCollection<string> { "זכר", "נקבה" };

            SelectedMaritalStatus = ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married ? "נשוי" : "רווק";
            RegisteredPartner = UserModel.FromUser(ReportSettings.Configuration.RegisteredPartner);
            Partner = UserModel.FromUser(ReportSettings.Configuration.Partner);
            BankManagementApprovalFile = ReportSettings.Configuration.BankManagementApprovalFile;

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
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
            RegisteredPartner.UpdateUser(ReportSettings.Configuration.RegisteredPartner);
            if (_isMarried)
            {
                Partner.UpdateUser(ReportSettings.Configuration.Partner);
            }

            var copy = ReportSettings.SaveToOutputDir(BankManagementApprovalFile, $"{ReportSettings.Configuration.RegisteredPartner.ID}_bank_management_approval.pdf");
            if (!string.IsNullOrEmpty(copy))
            {
                BankManagementApprovalFile = copy;
            }
            ReportSettings.Configuration.BankManagementApprovalFile = BankManagementApprovalFile;

            base.OnNext();
            await Shell.Current.GoToAsync(nameof(DefinitionOfForm106View));
        }

        private bool Validate()
        {
            if (string.IsNullOrEmpty(RegisteredPartner.FirstName) || string.IsNullOrEmpty(RegisteredPartner.LastName) || string.IsNullOrEmpty(RegisteredPartner.Id))
            {
                return false;
            }
            if (IsMarried)
            {
                if (string.IsNullOrEmpty(Partner.FirstName) || string.IsNullOrEmpty(Partner.LastName) || string.IsNullOrEmpty(Partner.Id) )
                {
                    return false;
                }
            }

            return true;
        }

        private async Task PickPdfFile(object parameter)
        {
            if (parameter is string fileType)
            {
                string file = await PickPdfFile();
                UpdateValue(file);
            }
        }

        private void ResetSelection(object parameter)
        {
            if (parameter is string fileType)
            {
                UpdateValue(string.Empty);
            }
        }

        private void UpdateValue(string value)
        {
            BankManagementApprovalFile = value;
        }
    }
}
