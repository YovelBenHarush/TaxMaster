using TaxMaster.BL;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class AnnualReportAdditionalDocsUploadViewModel : BaseViewModel
    {
        public string OutputPath { get; set; } = ReportSettings.GetOutputDir();

        public AnnualReportAdditionalDocsUploadViewModel()
        {
            Title = "העלאת מסמכים נוספים";
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Task.Delay(1000);
            //await Shell.Current.GoToAsync(nameof(AnnualReportIncomeDetailsView));
        }
    }
}
