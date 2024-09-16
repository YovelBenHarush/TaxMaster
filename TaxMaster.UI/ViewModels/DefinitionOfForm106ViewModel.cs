namespace TaxMaster
{
    public class DefinitionOfForm106ViewModel : BaseViewModel
    {
        public DefinitionOfForm106ViewModel()
        {
            IsPreviousEnabled = true;
        }

        public override void OnPrevious()
        {
            Shell.Current.GoToAsync("..");
        }

        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(EsppDividendsView));
        }
    }
}
