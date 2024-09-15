namespace TaxMaster
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(SelectReportType), typeof(SelectReportType));
            Routing.RegisterRoute(nameof(TaxAccountConfirmation), typeof(TaxAccountConfirmation));
        }
    }
}
