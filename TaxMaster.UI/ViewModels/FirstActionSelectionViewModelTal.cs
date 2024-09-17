namespace TaxMaster
{
    public class FirstActionSelectionViewModelTal : BaseViewModel
    {
        public Command AnnualReport { get; }

        public FirstActionSelectionViewModelTal()
        {
            AnnualReport = new Command(OnAnnualReport);
        }

        public override string Title
        {
            get => "פעולות";
            set => base.Title = value;
        }

        public async void OnAnnualReport()
        {
            await Shell.Current.GoToAsync(nameof(SelectReportType));
        }
    }
}
