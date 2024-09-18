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

        private readonly RsuWorker rsuWorker;
        public ICommand CalcualteCommand { get; }
        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }

        public MainRSUViewModel()
        {
            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
            CalcualteCommand = new Command(Calcualte);

            IsCalculating = false;
            CalcualteError = string.Empty;

            rsuWorker = new RsuWorker();
        }

        public override string Title
        {
            get => "ESOP MSFT RSU";
            set => base.Title = value;
        }

        private async void Calcualte()
        {
            Application.Current.Dispatcher.Dispatch(() =>
            {
                CalcualteError = string.Empty;
                IsCalculating = true;
            });


            var results = await rsuWorker.RsuEsopAsync(EsopTransactionsReportFile, Tax867File);

            Application.Current.Dispatcher.Dispatch(() =>
            {
                IsCalculating = false;
            });

            CalcualteError = (DateTime.Now.Millisecond % 2 == 0) ? "יש שגיאה נא לתקן" : string.Empty;
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
            };
        }
    }
}
