namespace TaxMaster
{
    public class MainRSUViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(LifeInsuranceView));
        }
    }
}
