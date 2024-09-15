namespace TaxMaster
{
    public class TaxAccountConfirmationViewModel : BaseViewModel
    {
        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(PersonalDetailsView));
        }
    }
}
