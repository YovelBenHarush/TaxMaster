﻿using iText.StyledXmlParser.Jsoup.Parser;
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

        private bool _birthAllowanceRadioButton;
        public bool BirthAllowanceRadioButton
        {
            get => _birthAllowanceRadioButton;
            set
            {
                _birthAllowanceRadioButton = value;
                OnPropertyChanged();
            }
        }

        private bool _numberApproval;
        public bool NumberApproval
        {
            get => _numberApproval;
            set
            {
                _numberApproval = value;
                OnPropertyChanged();
            }
        }

        private bool isWebViewVisible;

        public bool IsWebViewVisible
        {
            get => isWebViewVisible;
            set
            {
                isWebViewVisible = value;
                OnPropertyChanged(nameof(IsWebViewVisible));
            }
        }

        public ICommand PickFileCommand { get; }
        public ICommand ResetFileCommand { get; }
        public ICommand ToggleNumberApprovalCommand { get; }
        public ICommand ToggleWebViewCommand { get; }


        public BirthAllowanceViewModel()
        {
            Users = [ReportSettings.Configuration.RegisteredPartner.DisplayName];
            if (ReportSettings.Configuration.IsMarried)
            {
                Users.Add(ReportSettings.Configuration.Partner.DisplayName);
            }

            PickFileCommand = new Command(PickBirthAllowanceFile);
            ResetFileCommand = new Command(ResetSelection);

            ToggleNumberApprovalCommand = new Command(() =>
            {
                NumberApproval = !NumberApproval;
            });
            ToggleWebViewCommand = new Command(() =>
            {
                IsWebViewVisible = !IsWebViewVisible;
            });

            IsCalculating = false;
            CalcualteError = string.Empty;

            BirthAllowanceGuide = GuidesConfigurations.EsppFidelityPdfUrl;
            _birthAllowanceWorker = new BirthAllowanceWorker();
            ShouldDisplayValues = false;
            BirthAllowanceRadioButton = false;
            NumberApproval = false;

            var registeredPartner = ReportSettings.Configuration.RegisteredPartner;
            var partner = ReportSettings.Configuration.Partner;
            var registeredPartnerBirthPayment = registeredPartner?.BirthPayment;
            var partnerBirthPayment = partner?.BirthPayment;

            if (registeredPartnerBirthPayment != null && !string.IsNullOrEmpty(registeredPartnerBirthPayment.FilePath))
            {
                SelectedUser = 0;
                BirthAllowanceFile = registeredPartnerBirthPayment.FilePath;
                Amount = registeredPartnerBirthPayment.Amount;
                Tax = registeredPartnerBirthPayment.Tax;
                ShouldDisplayValues = true;
                BirthAllowanceRadioButton = true;
            }
            else if (partnerBirthPayment != null && ReportSettings.Configuration.IsMarried && !string.IsNullOrEmpty(partnerBirthPayment.FilePath))
            {
                SelectedUser = 1;
                BirthAllowanceFile = partnerBirthPayment.FilePath;
                Amount = partnerBirthPayment.Amount;
                Tax = partnerBirthPayment.Tax;
                ShouldDisplayValues = true;
                BirthAllowanceRadioButton = true;
            }
        }

        public async override void OnNext()
        {
            if (BirthAllowanceRadioButton)
            {
                if (string.IsNullOrEmpty(BirthAllowanceFile))
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "אנא בחר/י טופס דמי לידה ובצע חישוב עבורו", "OK");
                    }
                    return;
                }

                if (!NumberApproval)
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("", "אנא אשר/י את ערכי דמי הלידה", "OK");
                    }
                    return;
                }

                var getUser = (ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Married) ?
                    ReportSettings.Configuration.RegisteredPartner :
                    (SelectedUser == 0) ? ReportSettings.Configuration.RegisteredPartner : ReportSettings.Configuration.Partner;

                getUser.BirthPayment = new TaxBirthPaymentFile
                {
                    Tax = Tax,
                    Amount = Amount,
                    FilePath = BirthAllowanceFile,
                    UserId = getUser.ID
                };
            }

            base.OnNext();
            await Shell.Current.GoToAsync(nameof(DonationsView));
        }

        private async void PickBirthAllowanceFile()
        {
            string file = await PickPdfFile();
            BirthAllowanceFile = file;
            Calcualte();
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
