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
        }
    }
}
