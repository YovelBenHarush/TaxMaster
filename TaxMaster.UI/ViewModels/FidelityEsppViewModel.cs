using System.Windows.Input;
using TaxMaster.BL;

namespace TaxMaster
{
    public class FidelityEsppViewModel : BaseViewModel
    {
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

        private readonly EsppWorker esppWorker;
        public ICommand CalcualteCommand { get; }
        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }

        public FidelityEsppViewModel()
        {
            // Local PDF file (place this file in your app's Resources folder)
            EsppFidelityGuide = GuidesConfigurations.EsppFidelityPdfUrl;

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
            CalcualteCommand = new Command(Calcualte);

            IsCalculating = false;
            CalcualteError = string.Empty;

            esppWorker = new EsppWorker();
        }

        private async void Calcualte()
        {
            Application.Current.Dispatcher.Dispatch(() =>
            {
                CalcualteError = string.Empty;
                IsCalculating = true;
            });

            await esppWorker.EsppFidelityAsync(Tax1024File, CustomTransactionSummaryFile);
            await Task.Delay(10000);

            Application.Current.Dispatcher.Dispatch(() =>
            {
                IsCalculating = false;
            });

            CalcualteError = (DateTime.Now.Millisecond % 2 == 0) ? "יש שגיאה נא לתקן" : string.Empty;
        }

        public async override void OnNext()
        {
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
            };
        }
    }
}
