namespace TaxMaster
{
    public class AnnualReportPersonalDetailsViewModel : BaseViewModel
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


        public AnnualReportPersonalDetailsViewModel()
        {
            Guide = GuidesConfigurations.AnnualReportPersonalDetailsGuidePdfUrl;
        }


        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportGeneralDetailsView));
        }
    }
}
