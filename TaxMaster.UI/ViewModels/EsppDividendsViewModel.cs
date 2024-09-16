namespace TaxMaster
{
    public class EsppDividendsViewModel : BaseViewModel
    {
        private string _pdfUrl;

        public string PdfUrl
        {
            get => _pdfUrl;
            set
            {
                if (_pdfUrl != value)
                {
                    _pdfUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public EsppDividendsViewModel()
        {
            // Local PDF file (place this file in your app's Resources folder)
            PdfUrl = GuidesConfigurations.EsppFidelityPdfUrl;
        }


        public override void OnNext()
        {
            throw new NotImplementedException();
        }
    }
}
