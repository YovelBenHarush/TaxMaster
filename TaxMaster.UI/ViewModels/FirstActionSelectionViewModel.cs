namespace TaxMaster
{
    public class FirstActionSelectionViewModel : BaseViewModel
    {

        public Command AnnualReport { get; }

        public FirstActionSelectionViewModel()
        {
            AnnualReport = new Command(OnAnnualReport);
        }

        public async void OnAnnualReport()
        {
            await Shell.Current.GoToAsync(nameof(SelectReportType));
        }
    }
}
