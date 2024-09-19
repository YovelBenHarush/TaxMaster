namespace TaxMaster
{
    public class AnnualReportCapitalGainViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportAdditionalDocsUploadView));
        }
    }
}
