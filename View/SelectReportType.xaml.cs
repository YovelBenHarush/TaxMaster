namespace TaxMaster;

public partial class SelectReportType : ContentPage
{
	public SelectReportType()
	{
		InitializeComponent();
        DataTemplate dataTemplate = new(() => new SelectReportTypeViewModel());
    }
}