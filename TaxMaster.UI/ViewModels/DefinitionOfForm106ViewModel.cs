using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using TaxMaster.Infra;
using TaxMaster.Infra.Parsers;

namespace TaxMaster
{
    public class DefinitionOfForm106ViewModel : BaseViewModel
    {
        public ICommand PickFileCommandFiller { get; }
        public ICommand PickFileCommandPartner { get; }
        private Tax106FileParser _tax106FileParser;
        public Tax106FileViewModel Tax106FileFiller { get; private set; }
        public Tax106FileViewModel Tax106FilePartner { get; private set; }

        public DefinitionOfForm106ViewModel()
        {
            IsPreviousEnabled = true;

            _tax106FileParser = new Tax106FileParser();

            Tax106FileFiller = new Tax106FileViewModel();
            Tax106FilePartner = new Tax106FileViewModel();

            PickFileCommandFiller = new Command(() => Handle106Form(Tax106FileFiller));
            PickFileCommandPartner = new Command(() => Handle106Form(Tax106FilePartner));
        }

        public override void OnPrevious()
        {
            Shell.Current.GoToAsync("..");
        }

        public async override void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(FidelityEsppView));
        }

        private async void Handle106Form(Tax106FileViewModel tax106ViewModel)
        {
            var form106Path = await PickPdfFile();
            tax106ViewModel.internalTax106File = _tax106FileParser.Parse106File(form106Path);
        }

    }
}
