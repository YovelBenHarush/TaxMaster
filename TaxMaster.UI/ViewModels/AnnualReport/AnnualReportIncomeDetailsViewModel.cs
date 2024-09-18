using TaxMaster.Infra.Entities;
using TaxMaster.Infra;
using System.Collections.ObjectModel;

namespace TaxMaster
{
    public class AnnualReportIncomeDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<Pair> RegisteredPartner106 { get; set; }
        public ObservableCollection<Pair> Partner106 { get; set; }

        public bool IsMarried { get; set; }


        public AnnualReportIncomeDetailsViewModel()
        {
            IsMarried = ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married;

            RegisteredPartner106 = new ObservableCollection<Pair>
            {
                new Pair { Key = "Value1A", Value = "Value2A" },
                new Pair { Key = "Value1B", Value = "Value2B" },
                new Pair { Key = "Value1C", Value = "Value2C" }
            };

            Partner106 = new ObservableCollection<Pair>
            {
                new Pair { Key = "Value1A", Value = "Value2A" },
                new Pair { Key = "Value1B", Value = "Value2B" },
                new Pair { Key = "Value1C", Value = "Value2C" }
            };
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportAbroadAnnexView));
        }
    }

    public class Pair
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
