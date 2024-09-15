namespace TaxMaster;

public partial class DisclaimerPage : ContentPage
{
    public DisclaimerPage()
    {
        InitializeComponent();
        DataTemplate dataTemplate = new(() => new DisclaimerPageViewModel());
    }
}