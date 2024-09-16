namespace TaxMaster
{
    public class LifeInsuranceViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(BirthAllowanceView));
        }
    }
}
