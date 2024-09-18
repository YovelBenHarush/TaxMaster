namespace TaxMaster
{
    public class AnnualReportPersonalDetailsViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportGeneralDetailsView));
        }
    }
}
