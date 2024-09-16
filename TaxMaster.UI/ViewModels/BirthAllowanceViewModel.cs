using System.Collections.ObjectModel;
using System.Windows.Input;
using TaxMaster.BL;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class BirthAllowanceViewModel : BaseViewModel
    {
        public ObservableCollection<string> Users { get; set; }
        private int _selectedUser;
        public int SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _birthAllowanceGuide;
        public string BirthAllowanceGuide
        {
            get => _birthAllowanceGuide;
            set
            {
                if (_birthAllowanceGuide != value)
                {
                    _birthAllowanceGuide = value;
                    OnPropertyChanged();
                }
            }
        }

        private readonly BirthAllowanceWorker _birthAllowanceWorker;
        private bool _isCalculating;
        public bool IsCalculating
        {
            get => _isCalculating;
            set
            {
                _isCalculating = value;
                OnPropertyChanged();
            }
        }

        private string _calcualteError;
        public string CalcualteError
        {
            get => _calcualteError;
            set
            {
                if (_calcualteError != value)
                {
                    _calcualteError = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _birthAllowanceFile;
        public string BirthAllowanceFile
        {
            get => _birthAllowanceFile;
            set
            {
                if (_birthAllowanceFile != value)
                {
                    _birthAllowanceFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _amount;
        public double Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _tax;
        public double Tax
        {
            get => _tax;
            set
            {
                if (_tax != value)
                {
                    _tax = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _shouldDisplayValues;
        public bool ShouldDisplayValues
        {
            get => _shouldDisplayValues;
            set
            {
                _shouldDisplayValues = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalcualteCommand { get; }
        public ICommand PickFileCommand { get; }
        public ICommand ResetFileCommand { get; }

        public BirthAllowanceViewModel()
        {
            Users = [AnnualReportConfiguration.RegisteredPartner.DisplayName];
            if(AnnualReportConfiguration.FamilyStatus == Infra.Entities.FamilyStatus.Married) Users.Add(AnnualReportConfiguration.Partner.DisplayName);

            PickFileCommand = new Command(PickBirthAllowanceFile);
            ResetFileCommand = new Command(ResetSelection);
            CalcualteCommand = new Command(Calcualte);

            IsCalculating = false;
            CalcualteError = string.Empty;

            BirthAllowanceGuide = GuidesConfigurations.EsppFidelityPdfUrl;
            _birthAllowanceWorker = new BirthAllowanceWorker();
            ShouldDisplayValues = false;
        }

        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(DonationsView));
        }

        private async void PickBirthAllowanceFile()
        {
            string file = await PickPdfFile();
            BirthAllowanceFile = file;
        }

        private void ResetSelection(object parameter)
        {
            BirthAllowanceFile = string.Empty;
            Amount = 0;
            Tax = 0;
            ShouldDisplayValues = false;
        }

        private void Calcualte()
        {
            CalcualteError = string.Empty;
            IsCalculating = true;

            try
            {
                var birthPayment = _birthAllowanceWorker.BirthPayment(BirthAllowanceFile);
                Amount = birthPayment.Amount;
                Tax = birthPayment.Tax;
                ShouldDisplayValues = true;
            }
            catch (Exception)
            {
                CalcualteError = "יש שיגאה הפרסור הקוץ, טען קובץ מחדש ונסה שנית";
            }

            IsCalculating = false;
        }
    }
}
