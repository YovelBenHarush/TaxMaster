using TaxMaster.Infra;

namespace TaxMaster
{
    public class DonationsViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportFirstStepPage));
        }
    }
}
