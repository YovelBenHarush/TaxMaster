using System.Windows.Input;

namespace TaxMaster
{
    public class TaxAccountConfirmationViewModel : BaseViewModel
    {
        public ICommand OpenLinkCommand { get; }

        public string TaxLetter => "שלום רב,\nלאור הכנסות מהמעסיק (מיקרוסופט), וכן הכנסות מחול, אבקש לפתוח לי תיק מסוג 93 לצורך הגשת דוח מס. , \nבברכה,\n** השם שלכם **";

        public ICommand CopyTextCommand { get; }

        public TaxAccountConfirmationViewModel()
        {
            OpenLinkCommand = new Command<string>(OpenLink);

            CopyTextCommand = new Command(async () => await CopyTextToClipboard());
        }

        public override string Title
        {
            get => "תיק במס הכנסה";
            set => base.Title = value;
        }

        private async Task CopyTextToClipboard()
        {
            await Clipboard.SetTextAsync(TaxLetter);

            await Application.Current.MainPage.DisplayAlert("", "הטקסט הועתק", "OK");
        }

        public async override void OnNext()
        {
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(PersonalDetailsView));
        }
    }
}
