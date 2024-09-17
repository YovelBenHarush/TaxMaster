using System.Collections.ObjectModel;
using System.Windows.Input;
using TaxMaster.BL;

namespace TaxMaster
{
    public class ReportSelectionViewModel : BaseViewModel
    {
        private AnnualReportWorker _annualReportWorker;

        public ReportType ReportType { get; set; }

        public Command<ReportType> ToggleReportTypeCommand { get; }

        public ReportAction ReportAction { get; set; }

        public Command<ReportAction> ToggleReportActionCommand { get; }

        private string _selectedYear = "";
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

        public ObservableCollection<string> Years { get; set; }

        private ObservableCollection<string> _reports = new ObservableCollection<string>();
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

        private string _selectedReport = "";
        public string SelectedReport
        {
            get => _selectedReport;
            set
            {
                if (_selectedReport != value)
                {
                    _selectedReport = value;
                    YearHasReports = _selectedReport != "";
                    OnPropertyChanged();
                }
            }
        }

        private bool _yearHasReports = false;
        public bool YearHasReports
        {
            get => _yearHasReports;
            set
            {
                if (_yearHasReports != value)
                {
                    _yearHasReports = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReportSelectionViewModel()
        {
            _annualReportWorker = new AnnualReportWorker();

            Years = new ObservableCollection<string>(Enumerable.Range(DateTime.Now.Year - 6, 7).Reverse().Select(year => year.ToString()));

            ReportType = ReportType.AnnualReport;
            ReportAction = ReportAction.New;
            SelectedYear = Years[0];

            ToggleReportTypeCommand = new Command<ReportType>((reportType) =>
            {
                ReportType = reportType;
                OnPropertyChanged(nameof(ReportType));
            });

            ToggleReportActionCommand = new Command<ReportAction>((reportAction) =>
            {
                ReportAction = reportAction;
                OnPropertyChanged(nameof(ReportAction));
            });
        }

        public override string Title
        {
            get => "פעולות";
            set => base.Title = value;
        }

        private void OnSelectedYearChanged()
        {
            var reports = _annualReportWorker.GetExistingAnnualReports(int.Parse(SelectedYear));
            Reports = (reports == null || reports.Count == 0) ? new ObservableCollection<string>() : new ObservableCollection<string>(reports.Select(r => r.DisplayName));
            OnPropertyChanged(nameof(Reports));
            SelectedReport = Reports.Count == 0 ? "" : Reports[0];
        }

        public override async void OnNext()
        {
            if (ReportAction == ReportAction.Existing && SelectedReport == "")
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("", "לא קיים דוח עבור השנה הנבחרת. יש לבחור שנה עם דוח קיים או דוח חדש", "OK");
                }
                return;
            }

            await Shell.Current.GoToAsync(nameof(TaxAccountConfirmation));
        }
    }
}
