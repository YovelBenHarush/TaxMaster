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

        private async void OpenLink(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                await Launcher.OpenAsync(url);
            }
        }

        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(PersonalDetailsView));
        }
    }
}
