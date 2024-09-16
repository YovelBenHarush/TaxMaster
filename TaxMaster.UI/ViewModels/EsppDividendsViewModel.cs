namespace TaxMaster
{
    public class EsppDividendsViewModel : BaseViewModel
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

        public Command<object> PickFileCommand { get; }
        public Command<object> ResetFileCommand { get; }

        public EsppDividendsViewModel()
        {
            // Local PDF file (place this file in your app's Resources folder)
            EsppFidelityGuide = GuidesConfigurations.EsppFidelityPdfUrl;

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
            ResetFileCommand = new Command<object>((parameter) => ResetSelection(parameter));
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
