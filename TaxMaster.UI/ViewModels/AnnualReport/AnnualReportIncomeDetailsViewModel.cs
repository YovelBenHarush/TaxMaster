using TaxMaster.Infra.Entities;
using TaxMaster.Infra;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaxMaster.BL;

namespace TaxMaster
{
    public class AnnualReportIncomeDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<Pair> RegisteredPartner106 { get; set; }
        public ObservableCollection<Pair> Partner106 { get; set; }

        public bool IsMarried { get; set; }

        private AnnualReportWorker _annualReportWorker;

        public AnnualReportIncomeDetailsViewModel()
        {
            IsMarried = true; // ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married;
            _annualReportWorker = new AnnualReportWorker();
            var income = _annualReportWorker.GetIncomeDetails();

            RegisteredPartner106 = new ObservableCollection<Pair>();
            Partner106 = new ObservableCollection<Pair>();
            foreach (var item in income)
            {
                if (item.RegisteredPartnerIncomeDetails != null)
                {
                    RegisteredPartner106.Add(new Pair { Key = item.RegisteredPartnerIncomeDetails.Key, Value = item.RegisteredPartnerIncomeDetails.Value, Explanation = item.RegisteredPartnerIncomeDetails.Explanation });
                }

                if (item.PartnerIncomeDetails != null)
                {
                    Partner106.Add(new Pair { Key = item.PartnerIncomeDetails.Key, Value = item.PartnerIncomeDetails.Value, Explanation = item.PartnerIncomeDetails.Explanation });
                }
            }
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportAbroadAnnexView));
        }
    }

    public class Pair : INotifyPropertyChanged
    {
        private bool _mark;

        public string Key { get; set; }
        public string Value { get; set; }
        public string Explanation { get; set; }


        public bool Mark
        {
            get => _mark;
            set
            {
                if (_mark == value)
                    return;

                _mark = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
