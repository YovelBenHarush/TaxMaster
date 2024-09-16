using System.Collections.ObjectModel;
using TaxMaster.BL;

namespace TaxMaster
{
    public class SelectReportTypeViewModel : BaseViewModel
    {
        private AnnualReportWorker _annualReportWorker;

        public Command NewReport { get; }
        public Command OldReport { get; }

        public ObservableCollection<string> Years { get; set; }

        private string _selectedYear;
        public string SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnSelectedYearChanged();
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> _reports;
        public ObservableCollection<string> Reports
        {
            get => _reports;
            set
            {
                if (_reports != value)
                {
                    _reports = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _selectedReport;
        public string SelectedReport
        {
            get => _selectedReport;
            set
            {
                if (_selectedReport != value)
                {
                    _selectedReport = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ThereAreOldReports => Reports == null ? false : Reports.Count > 0;

        public SelectReportTypeViewModel()
        {
            _annualReportWorker = new AnnualReportWorker();
            NewReport = new Command(OnNewReport);
            OldReport = new Command(OnOldReport);

            // Replace the problematic line with the following
            Years = new ObservableCollection<string>(Enumerable.Range(DateTime.Now.Year - 6, 7).Reverse().Select(year => year.ToString()));
        }

        private void OnSelectedYearChanged()
        {
            var reports = _annualReportWorker.GetExistingAnnualReports(int.Parse(SelectedYear));
            SelectedReport = null;
            Reports = (reports == null || reports.Count == 0) ? new ObservableCollection<string>() : new ObservableCollection<string>(reports.Select(r => r.DisplayName));
            OnPropertyChanged(nameof(ThereAreOldReports));
        }

        private async void OnNewReport()
        {
            // Navigate to the next step
            await Shell.Current.GoToAsync(nameof(TaxAccountConfirmation));
        }

        private async void OnOldReport()
        {
            // Navigate to the next step
            await Shell.Current.GoToAsync(nameof(TaxAccountConfirmation));
        }

        public override void OnNext()
        {
            throw new NotImplementedException();
        }
    }
}
