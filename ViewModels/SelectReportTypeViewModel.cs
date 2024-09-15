namespace TaxMaster
{
    public class SelectReportTypeViewModel
    {
        public Command NewReport { get; }
        public Command OldReport { get; }

        public SelectReportTypeViewModel()
        {
            NewReport = new Command(OnNewReport);
            OldReport = new Command(OnOldReport);
        }

        private async void OnNewReport()
        {
            // Navigate to the next step
            await Shell.Current.GoToAsync(nameof(TaxAccountConfirmation));
        }

        private async void OnOldReport()
        {
            // Navigate to the next step
            await Shell.Current.GoToAsync(nameof(TaxAccountConfirmation));
        }
    }
}
