using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class AnnualReportFirstStepViewModel : BaseViewModel
    {
        private int _year;
        public int Year
        {
            get => _year;
            set
            {
                if (_year != value)
                {
                    _year = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _id;
        public string ID
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public AnnualReportFirstStepViewModel()
        {
            OpenLinkCommand = new Command<string>(OpenLink);
            Year = ReportSettings.Configuration.Year;
            ID = ReportSettings.Configuration.RegisteredPartner.ID;

        }

        public ICommand OpenLinkCommand { get; }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportPersonalDetailsView));
        }
    }
}
