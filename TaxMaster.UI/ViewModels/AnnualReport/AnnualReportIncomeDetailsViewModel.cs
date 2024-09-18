namespace TaxMaster
{
    public class AnnualReportIncomeDetailsViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportAbroadAnnexView));
        }
    }
}
