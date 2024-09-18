namespace TaxMaster
{
    public class AnnualReportAbroadAnnexViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportCapitalGainView));
        }
    }
}
