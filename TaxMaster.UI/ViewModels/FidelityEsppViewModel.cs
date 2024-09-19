using System.Windows.Input;
using TaxMaster.BL;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class FidelityEsppViewModel : BaseViewModel
    {
        public string RegisteredPartnerName => ReportSettings.Configuration.RegisteredPartner.FirstName;

        public string PartnerName => ReportSettings.Configuration.Partner.FirstName;

        private string _esppFidelityGuide;
        public string EsppFidelityGuide
        {
            get => _esppFidelityGuide;
            set
            {
                if (_esppFidelityGuide != value)
                {
                    _esppFidelityGuide = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _customTransactionSummaryFile;
        public string CustomTransactionSummaryFile
        {
            get => _customTransactionSummaryFile;
            set
            {
                if (_customTransactionSummaryFile != value)
                {
                    _customTransactionSummaryFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _customTransactionSummaryPartnerFile;
        public string CustomTransactionSummaryPartnerFile
        {
            get => _customTransactionSummaryPartnerFile;
            set
            {
                if (_customTransactionSummaryPartnerFile != value)
                {
                    _customTransactionSummaryPartnerFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tax1024File;
        public string Tax1024File
        {
            get => _tax1024File;
            set
            {
                if (_tax1024File != value)
                {
                    _tax1024File = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _tax1024PartnerFile;
        public string Tax1024PartnerFile
        {
            get => _tax1024PartnerFile;
            set
            {
                if (_tax1024PartnerFile != value)
                {
                    _tax1024PartnerFile = value;
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

        private string _report_1325_1_File;
        public string Report_1325_1_File
        {
            get => _report_1325_1_File;
            set
            {
                if (_report_1325_1_File != value)
                {
                    _report_1325_1_File = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _report_1325_1_PartnerFile;
        public string Report_1325_1_PartnerFile
        {
            get => _report_1325_1_PartnerFile;
            set
            {
                if (_report_1325_1_PartnerFile != value)
                {
                    _report_1325_1_PartnerFile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _report_1325_2_File;
        public string Report_1325_2_File
        {
            get => _report_1325_2_File;
            set
            {
                if (_report_1325_2_File != value)
                {
                    _report_1325_2_File = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _report_1325_2_PartnerFile;
        public string Report_1325_2_PartnerFile
        {
            get => _report_1325_2_PartnerFile;
            set
            {
                if (_report_1325_2_PartnerFile != value)
                {
                    _report_1325_2_PartnerFile = value;
                    OnPropertyChanged();
                }
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

        private readonly EsppWorker esppWorker;
        public Command<string> CalcualteCommand { get; }
        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }

        public Command<string> OpenPdfFileCommand { get; }

        public bool IsMarried => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public FidelityEsppViewModel()
        {
            // Local PDF file (place this file in your app's Resources folder)
            EsppFidelityGuide = GuidesConfigurations.EsppFidelityPdfUrl;

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
            CalcualteCommand = new Command<string>(Calcualte);
            OpenPdfFileCommand = new Command<string>(OpenPdfFile);

            IsCalculating = false;
            ShouldDisplayValues = false;
            CalcualteError = string.Empty;

            IsCalculatingPartner = false;
            ShouldDisplayValuesPartner = false;
            CalcualteErrorPartner = string.Empty;

            esppWorker = new EsppWorker();
        }

        public override string Title
        {
            get => "Fidelity ESPP";
            set => base.Title = value;
        }

        private async void Calcualte(string userType)
        {
            // TODO
            //var isRegisteredPartner2 = userType.Equals("RegisteredPartner");
            //if (isRegisteredPartner2)
            //{
            //    ShouldDisplayValues = true;
            //}
            //else
            //{
            //    ShouldDisplayValuesPartner = true;
            //}
            //return;

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
                    var results = await esppWorker.EsppFidelityAsync(isRegisteredPartner, Tax1024File, CustomTransactionSummaryFile);
                    Report_1325_1_File = results.FirstHalfOfYearStockSaleReport;
                    Report_1325_2_File = results.SecondHalfOfYearStockSaleReport;
                    Dividend = results.DividendInUsd;
                }
                else
                {
                    var results = await esppWorker.EsppFidelityAsync(isRegisteredPartner, Tax1024PartnerFile, CustomTransactionSummaryPartnerFile);
                    Report_1325_1_PartnerFile = results.FirstHalfOfYearStockSaleReport;
                    Report_1325_2_PartnerFile = results.SecondHalfOfYearStockSaleReport;
                    DividendPartner = results.DividendInUsd;
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
            await Shell.Current.GoToAsync(nameof(MainRSUView));
        }

        private async Task PickPdfFile(object parameter)
        {
            if (parameter is string fileType)
            {
                string file = await PickPdfFile();
                UpdateValue(fileType, file);
            }
        }

        private async void OpenPdfFile(string parameter)
        {
            if (parameter.Equals("1325_1"))
            {
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Report_1325_1_File)
                });
            }
            else if (parameter.Equals("1325_2"))
            {
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Report_1325_2_File)
                });
            }
            else if (parameter.Equals("1325_1_Partner"))
            {
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Report_1325_1_PartnerFile)
                });
            }
            else if (parameter.Equals("1325_2_Partner"))
            {
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(Report_1325_2_PartnerFile)
                });
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
            if (fileType.Equals("1024"))
            {
                Tax1024File = value;
            }
            else if (fileType.Equals("CustomTransactionSummary"))
            {
                CustomTransactionSummaryFile = value;
                Calcualte("RegisteredPartner");
            }
            else if (fileType.Equals("1024Partner"))
            {
                Tax1024PartnerFile = value;
            }
            else if (fileType.Equals("CustomTransactionSummaryPartner"))
            {
                CustomTransactionSummaryPartnerFile = value;
                Calcualte("Partner");
            };
        }
    }
}
