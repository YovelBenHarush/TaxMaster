using System.Windows.Input;
using TaxMaster.BL;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class AnnualReportAdditionalDocsUploadViewModel : BaseViewModel
    {
        public string OutputPath { get; set; } = ReportSettings.GetOutputDir();
        public ICommand OpenFileExplorerCommand { get; }
        public AnnualReportAdditionalDocsUploadViewModel()
        {
            Title = "העלאת מסמכים נוספים";
            OpenFileExplorerCommand = new Command(OpenFileExplorer);
        }

        private void OpenFileExplorer()
        {
#if WINDOWS
            // Only works on Windows to open the folder path in File Explorer
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = OutputPath,
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
#endif
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Task.Delay(1000);
            //await Shell.Current.GoToAsync(nameof(AnnualReportIncomeDetailsView));
        }
    }
}
