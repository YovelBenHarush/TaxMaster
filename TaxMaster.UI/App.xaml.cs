namespace TaxMaster.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(TaxAccountConfirmation), typeof(TaxAccountConfirmation));
            Routing.RegisterRoute(nameof(ReportSelectionView), typeof(ReportSelectionView));
            Routing.RegisterRoute(nameof(PersonalDetailsView), typeof(PersonalDetailsView));
            Routing.RegisterRoute(nameof(DefinitionOfForm106View), typeof(DefinitionOfForm106View));
            Routing.RegisterRoute(nameof(FidelityEsppView), typeof(FidelityEsppView));
            Routing.RegisterRoute(nameof(MainRSUView), typeof(MainRSUView));
            Routing.RegisterRoute(nameof(LifeInsuranceView), typeof(LifeInsuranceView));
            Routing.RegisterRoute(nameof(BirthAllowanceView), typeof(BirthAllowanceView));
            Routing.RegisterRoute(nameof(DonationsView), typeof(DonationsView));

            Routing.RegisterRoute(nameof(AnnualReportFirstStepPage), typeof(AnnualReportFirstStepPage));
            Routing.RegisterRoute(nameof(AnnualReportAbroadAnnexView), typeof(AnnualReportAbroadAnnexView));
            Routing.RegisterRoute(nameof(AnnualReportCapitalGainView), typeof(AnnualReportCapitalGainView));
            Routing.RegisterRoute(nameof(AnnualReportGeneralDetailsView), typeof(AnnualReportGeneralDetailsView));
            Routing.RegisterRoute(nameof(AnnualReportIncomeDetailsView), typeof(AnnualReportIncomeDetailsView));
            Routing.RegisterRoute(nameof(AnnualReportPersonalDetailsView), typeof(AnnualReportPersonalDetailsView));
            Routing.RegisterRoute(nameof(AnnualReportAdditionalDocsUploadView), typeof(AnnualReportAdditionalDocsUploadView));
        }
    }
}
