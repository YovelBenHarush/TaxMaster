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

            esppWorker.EsppFidelity(Tax1024File, CustomTransactionSummaryFile);
            await Task.Delay(10000);

            Application.Current.Dispatcher.Dispatch(() =>
            {
                IsCalculating = false;
            });

            CalcualteError = (DateTime.Now.Millisecond % 2 == 0) ? "יש שגיאה נא לתקן" : string.Empty;
        }

        public override void OnNext()
        {
            throw new NotImplementedException();
        }

        private async Task PickPdfFile(object parameter)
        {
            try
            {
                // FilePicker options to filter for PDF files
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Pdf,
                    PickerTitle = "Select a PDF file"
                });

                if (result != null)
                {
                    string fileType = parameter as string;
                    if (fileType.Equals("1024"))
                    {
                        Tax1024File = result.FullPath;
                    }
                    else if (fileType.Equals("CustomTransactionSummary"))
                    {
                        CustomTransactionSummaryFile = result.FullPath;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors (e.g., user cancels file picker or permission denied)
                Console.WriteLine($"Error picking file: {ex.Message}");
            }
        }

        private void ResetSelection(object parameter)
        {
            string fileType = parameter as string;
            if (fileType.Equals("1024"))
            {
                Tax1024File = string.Empty;
            }
            else if (fileType.Equals("CustomTransactionSummary"))
            {
                CustomTransactionSummaryFile = string.Empty;
            };
        }
    }
}
