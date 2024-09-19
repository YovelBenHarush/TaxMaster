using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class AnnualReportFirstStepViewModel : BaseViewModel
    {
        private string _guide;
        public string Guide
        {
            get => _guide;
            set
            {
                if (_guide != value)
                {
                    _guide = value;
                    OnPropertyChanged();
                }
            }
        }


        public AnnualReportFirstStepViewModel()
        {

            Guide = GuidesConfigurations.AnnualReportFirstStepGuidePdfUrl;
        }


        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportPersonalDetailsView));
        }
    }
}
