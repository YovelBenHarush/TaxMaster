using TaxMaster.BL;

namespace TaxMaster
{
    public class AnnualReportGeneralDetailsViewModel : BaseViewModel
    {
        private bool _shouldDisplayCapitalGain;
        public bool ShouldDisplayCapitalGain
        {
            get => _shouldDisplayCapitalGain;
            set
            {
                _shouldDisplayCapitalGain = value;
                OnPropertyChanged();
            }
        }

        private bool _shouldDisplayAbroadAnnex;
        private AnnualReportWorker _annualReportWorker;

        public bool ShouldDisplayAbroadAnnex
        {
            get => _shouldDisplayAbroadAnnex;
            set
            {
                _shouldDisplayAbroadAnnex = value;
                OnPropertyChanged();
            }
        }

        public AnnualReportGeneralDetailsViewModel()
        {
            _annualReportWorker = new AnnualReportWorker();
            ShouldDisplayAbroadAnnex = _annualReportWorker.HasDividend() || _annualReportWorker.HasCapitalGain();
            ShouldDisplayCapitalGain = _annualReportWorker.HasCapitalGain();
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportIncomeDetailsView));
        }
    }
}
