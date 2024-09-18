namespace TaxMaster
{
    public class AnnualReportGeneralDetailsViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportIncomeDetailsView));
        }
    }
}
