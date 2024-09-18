using System.Windows.Input;

namespace TaxMaster
{
    public class TaxAccountConfirmationViewModel : BaseViewModel
    {
        public ICommand OpenLinkCommand { get; }

        public TaxAccountConfirmationViewModel()
        {
            OpenLinkCommand = new Command<string>(OpenLink);
        }

        public override string Title
        {
            get => "תיק במס הכנסה";
            set => base.Title = value;
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(PersonalDetailsView));
        }
    }
}
