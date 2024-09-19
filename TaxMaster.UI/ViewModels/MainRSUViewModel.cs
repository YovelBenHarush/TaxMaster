using System.Windows.Input;
using TaxMaster.BL;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class MainRSUViewModel : BaseViewModel
    {
        private string _esopTransactionsReportFile;
        public string EsopTransactionsReportFile
        {
            get => _esopTransactionsReportFile;
            set
            {
                if (_esopTransactionsReportFile != value)
                {
                    _esopTransactionsReportFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _esopTransactionsReportPartnerFile;
        public string EsopTransactionsReportPartnerFile
        {
            get => _esopTransactionsReportPartnerFile;
            set
            {
                if (_esopTransactionsReportPartnerFile != value)
                {
                    _esopTransactionsReportPartnerFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tax867File;
        public string Tax867File
        {
            get => _tax867File;
            set
            {
                if (_tax867File != value)
                {
                    _tax867File = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tax867PartnerFile;
        public string Tax867PartnerFile
        {
            get => _tax867PartnerFile;
            set
            {
                if (_tax867PartnerFile != value)
                {
                    _tax867PartnerFile = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private bool _isCalculatingPartner;
        public bool IsCalculatingPartner
        {
            get => _isCalculatingPartner;
            set
            {
                _isCalculatingPartner = value;
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

        private string _calcualteErrorPartner;
        public string CalcualteErrorPartner
        {
            get => _calcualteErrorPartner;
            set
            {
                if (_calcualteErrorPartner != value)
                {
                    _calcualteErrorPartner = value;
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

        private bool _shouldDisplayValuesPartner;
        public bool ShouldDisplayValuesPartner
        {
            get => _shouldDisplayValuesPartner;
            set
            {
                _shouldDisplayValuesPartner = value;
                OnPropertyChanged();
            }
        }

        private double _dividend;
        public double Dividend
        {
            get => _dividend;
            set
            {
                if (_dividend != value)
                {
                    _dividend = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _dividendPartner;
        public double DividendPartner
        {
            get => _dividendPartner;
            set
            {
                if (_dividendPartner != value)
                {
                    _dividendPartner = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _dividendTax;
        public double DividendTax
        {
            get => _dividendTax;
            set
            {
                if (_dividendTax != value)
                {
                    _dividendTax = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _dividendTaxPartner;
        public double DividendTaxPartner
        {
            get => _dividendTaxPartner;
            set
            {
                if (_dividendTaxPartner != value)
                {
                    _dividendTaxPartner = value;
                    OnPropertyChanged();
                }
            }
        }


        private readonly RsuWorker rsuWorker;
        public Command<string> CalcualteCommand { get; }
        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }
        public bool IsMarried => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public MainRSUViewModel()
        {
            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
            CalcualteCommand = new Command<string>(Calcualte);

            IsCalculating = false;
            CalcualteError = string.Empty;

            rsuWorker = new RsuWorker();
        }

        public override string Title
        {
            get => "ESOP MSFT RSU";
            set => base.Title = value;
        }

        private async void Calcualte(string userType)
        {
            var isRegisteredPartner = userType.Equals("RegisteredPartner");
            if (isRegisteredPartner)
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    CalcualteError = string.Empty;
                    IsCalculating = true;
                    ShouldDisplayValues = false;
                });
            }
            else
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    CalcualteErrorPartner = string.Empty;
                    IsCalculatingPartner = true;
                    ShouldDisplayValuesPartner = false;
                });
            }

            try
            {
                if (isRegisteredPartner)
                {
                    var results = await rsuWorker.RsuEsopAsync(isRegisteredPartner, EsopTransactionsReportFile, Tax867File);
                    Dividend = results.DividendInNis;
                    DividendTax = results.DividendTaxInNis;
                }
                else
                {
                    var results = await rsuWorker.RsuEsopAsync(isRegisteredPartner, EsopTransactionsReportFile, Tax867File);
                    DividendPartner = results.DividendInNis;
                    DividendTaxPartner = results.DividendTaxInNis;
                }

            }
            catch (Exception ex)
            {
                var errorMessage = "יש שגיאה בקריאת הקובץ, טען קובץ מחדש ונסה שנית";

                if (isRegisteredPartner)
                {
                    Application.Current.Dispatcher.Dispatch(() =>
                    {
                        IsCalculating = false;
                        CalcualteError = errorMessage;
                        ShouldDisplayValues = false;
                    });
                }
                else
                {
                    Application.Current.Dispatcher.Dispatch(() =>
                    {
                        IsCalculatingPartner = false;
                        CalcualteErrorPartner = errorMessage;
                        ShouldDisplayValuesPartner = false;
                    });
                }

                return;
            }

            if (isRegisteredPartner)
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    IsCalculating = false;
                    ShouldDisplayValues = true;
                });
            }
            else
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    IsCalculatingPartner = false;
                    ShouldDisplayValuesPartner = true;
                });
            }
        }

        public async override void OnNext()
        {
            ReportSettings.SaveConfiguration();
            await Shell.Current.GoToAsync(nameof(LifeInsuranceView));
        }

        private async Task PickPdfFile(object parameter)
        {
            if (parameter is string fileType)
            {
                string file = await PickPdfFile();
                UpdateValue(fileType, file);
            }
        }

        private void ResetSelection(object parameter)
        {
            if (parameter is string fileType)
            {
                UpdateValue(fileType, string.Empty);
            }
        }

        private void UpdateValue(string fileType, string value)
        {
            if (fileType.Equals("867"))
            {
                Tax867File = value;
            }
            else if (fileType.Equals("EsopTransactionsReport"))
            {
                EsopTransactionsReportFile = value;
            }
            else if (fileType.Equals("867Partner"))
            {
                Tax867PartnerFile = value;
            }
            else if (fileType.Equals("EsopTransactionsReportPartner"))
            {
                EsopTransactionsReportPartnerFile = value;
            }
        }
    }
}
