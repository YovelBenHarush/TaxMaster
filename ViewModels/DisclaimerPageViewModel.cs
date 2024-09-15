namespace TaxMaster
{
    public class DisclaimerPageViewModel : BaseViewModel
    {
        public DisclaimerModel DisclaimerModel { get; private set; }
        public Command NextCommand { get; }

        public DisclaimerPageViewModel()
        {
            DisclaimerModel = new DisclaimerModel
            {
                Title = "Disclaimer",
                DisclaimerText = "This is a disclaimer text",
                DisclaimerAprovalText = "I agree to the terms and conditions",
                DisclaimerAproval = false
            };

            NextCommand = new Command(OnNext);
        }

        private async void OnNext()
        {
            // Navigate to the next step
            await Shell.Current.GoToAsync(nameof(SelectReportType));
        }
    }
}
