namespace TaxMaster
{
    public class DisclaimerPageViewModel : BaseViewModel
    {
        public DisclaimerModel DisclaimerModel { get; private set; }
        public DisclaimerPageViewModel()
        {
            DisclaimerModel = new DisclaimerModel
            {
                Title = "Disclaimer",
                DisclaimerText = "This is a disclaimer text",
                DisclaimerAprovalText = "I agree to the terms and conditions",
                DisclaimerAproval = false
            };
        }

        public override async void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(SelectReportType));
        }
    }
}
