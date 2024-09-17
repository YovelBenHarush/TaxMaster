using TaxMaster.Infra;

namespace TaxMaster
{
    public class DonationsViewModel : BaseViewModel
    {
        public override void OnNext()
        {
            ReportSettings.SaveConfiguration();
        }
    }
}
