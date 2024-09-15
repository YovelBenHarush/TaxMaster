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
            if (!DisclaimerModel.DisclaimerAproval)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "אנא אשר את תנאי המערכת", "Ok");
                }
                return;
            }

            await Shell.Current.GoToAsync(nameof(FirstActionSelectionView));
        }
    }
}
