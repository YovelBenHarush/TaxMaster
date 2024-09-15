﻿namespace TaxMaster
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute(nameof(SelectReportType), typeof(SelectReportType));
            Routing.RegisterRoute(nameof(TaxAccountConfirmation), typeof(TaxAccountConfirmation));
            Routing.RegisterRoute(nameof(FirstActionSelectionView), typeof(FirstActionSelectionView));
            Routing.RegisterRoute(nameof(PersonalDetailsView), typeof(PersonalDetailsView));
            Routing.RegisterRoute(nameof(DefinitionOfForm106View), typeof(DefinitionOfForm106View));
            Routing.RegisterRoute(nameof(EsppDividendsView), typeof(EsppDividendsView));
            Routing.RegisterRoute(nameof(EsppSaleOfSharesView), typeof(EsppSaleOfSharesView));
            Routing.RegisterRoute(nameof(MainRSUView), typeof(MainRSUView));
            Routing.RegisterRoute(nameof(RsuDividendsView), typeof(RsuDividendsView));
            Routing.RegisterRoute(nameof(RsuStockSaleView), typeof(RsuStockSaleView));
            Routing.RegisterRoute(nameof(LifeInsuranceView), typeof(LifeInsuranceView));
            Routing.RegisterRoute(nameof(BirthAllowanceView), typeof(BirthAllowanceView));
            Routing.RegisterRoute(nameof(DonationsView), typeof(DonationsView));

        }
    }
}
