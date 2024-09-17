using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using TaxMaster.Infra;
using TaxMaster.Infra.Entities;
using TaxMaster.Infra.Parsers;

namespace TaxMaster
{
    public class DefinitionOfForm106ViewModel : BaseViewModel
    {
        public override string Title
        {
            get => "Tax Form 106";
            set => base.Title = value;
        }

        private bool _shouldShowTax106FileDetailsFiller = false;
        public bool ShouldShowTax106FileDetailsFiller
        {
            get
            {
                return _shouldShowTax106FileDetailsFiller;
            }
            set
            {
                if (_shouldShowTax106FileDetailsFiller != value)
                {
                    _shouldShowTax106FileDetailsFiller = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _shouldShowTax106FileDetailsPartner = false;
        public bool ShouldShowTax106FileDetailsPartner
        {
            get
            {
                return _shouldShowTax106FileDetailsPartner;
            }
            set
            {
                if (_shouldShowTax106FileDetailsPartner != value)
                {
                    _shouldShowTax106FileDetailsPartner = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _shouldShowPartner = false;
        public bool ShouldShowPartner
        {
            get
            {
                return _shouldShowPartner;
            }
            set
            {
                if (_shouldShowPartner != value)
                {
                    _shouldShowPartner = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand PickFileCommandFiller { get; }
        public ICommand PickFileCommandPartner { get; }
        private Tax106FileParser _tax106FileParser;
        public Tax106FileViewModel Tax106FileFiller { get; private set; }
        public Tax106FileViewModel Tax106FilePartner { get; private set; }

        public DefinitionOfForm106ViewModel()
        {
            AnnualReportConfiguration.FamilyStatus = FamilyStatus.Married;
            IsPreviousEnabled = true;
            _tax106FileParser = new Tax106FileParser();

            ShouldShowPartner = AnnualReportConfiguration.FamilyStatus == FamilyStatus.Married;

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
            base.OnNext();
            await Shell.Current.GoToAsync(nameof(FidelityEsppView));
        }

        private async void Handle106Form(Tax106FileViewModel tax106ViewModel)
        {
            try
            {
                var form106Path = await PickPdfFile();
                tax106ViewModel.InternalTax106File = _tax106FileParser.Parse106File(form106Path);

                if (ReferenceEquals(tax106ViewModel, Tax106FileFiller))
                {
                    ShouldShowTax106FileDetailsFiller = true;
                }
                else if (ReferenceEquals(tax106ViewModel, Tax106FilePartner))
                {
                    ShouldShowTax106FileDetailsPartner = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
