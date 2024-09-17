using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class DisclaimerPageViewModel : BaseViewModel
    {
        public DisclaimerModel DisclaimerModel { get; private set; }

        public ICommand ToggleLogsApprovalCommand { get; }

        public ICommand ToggleDisclaimerApprovalCommand { get; }

        public ICommand OpenEmailCommand { get; }

        public DisclaimerPageViewModel()
        {
            DisclaimerModel = new DisclaimerModel
            {
                Title = "Disclaimer",
                DisclaimerApproval = false
            };

            IsPreviousEnabled = false;

            ToggleLogsApprovalCommand = new Command(() =>
            {
                LogsApproval = !LogsApproval;
            });

            ToggleDisclaimerApprovalCommand = new Command(() =>
            {
                DisclaimerApproval = !DisclaimerApproval;
            });

            OpenEmailCommand = new Command(() => {
                try
                {
                    var emailMessage = new EmailMessage
                    {
                        Subject = "Tax master is awesome!",
                        To = new List<string> { DisclaimerModel.DisclaimerEmail }
                    };
                    Email.ComposeAsync(emailMessage);
                }
                catch (Exception ex)
                {
                    // Handle exception if email client is not available
                    Console.WriteLine(ex.Message);
                }
            });
        }

        public override string Title
        {
            get => "ברוכים הבאים ל- Tax Master!";
            set => base.Title = value;
        }

        public bool LogsApproval
        {
            get => DisclaimerModel.LogsApproval;
            set
            {
                DisclaimerModel.LogsApproval = value;
                OnPropertyChanged();
            }
        }

        public bool DisclaimerApproval
        {
            get => DisclaimerModel.DisclaimerApproval;
            set
            {
                DisclaimerModel.DisclaimerApproval = value;
                OnPropertyChanged();
            }
        }

        public override async void OnNext()
        {
            ReportSettings.SaveConfiguration();

            if (!DisclaimerModel.DisclaimerApproval)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "אנא אשר/י את תנאי המערכת", "OK");
                }
                return;
            }

            if (DisclaimerModel.LogsApproval)
            {
                // Save the user's approval
                LoggerConfiguration.Logger = FileLogger.CreateLogger("logs");
            }

            await Shell.Current.GoToAsync(nameof(FirstActionSelectionView));
        }
    }
}
