using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using TaxMaster.BL;
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

        private bool _isFillerSubmitted = false;
        public bool IsFillerSubmmited
        {
            get
            {
                return _isFillerSubmitted;
            }
            set
            {
                if (_isFillerSubmitted != value)
                {
                    _isFillerSubmitted = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSubmittingFiller = false;
        public bool IsSubmittingFiller
        {
            get => _isSubmittingFiller;
            set
            {
                if (_isSubmittingFiller != value)
                {
                    _isSubmittingFiller = value;
                    IsNotSubmittingFiller = !IsSubmittingFiller;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isNotSubmittingFiller = true;
        public bool IsNotSubmittingFiller
        {
            get => _isNotSubmittingFiller;
            set
            {
                if (_isNotSubmittingFiller != value)
                {
                    _isNotSubmittingFiller = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _submitButtonTextFiller = "Submit";
        public string SubmitButtonTextFiller
        {
            get => _submitButtonTextFiller;
            set
            {
                if (_submitButtonTextFiller != value)
                {
                    _submitButtonTextFiller = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isPartnerSubmitted = false;
        public bool IsPartnerSubmmited
        {
            get
            {
                return _isPartnerSubmitted;
            }
            set
            {
                if (_isPartnerSubmitted != value)
                {
                    _isPartnerSubmitted = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSubmittingPartner = false;
        public bool IsSubmittingPartner
        {
            get => _isSubmittingPartner;
            set
            {
                if (_isSubmittingPartner != value)
                {
                    _isSubmittingPartner = value;
                    IsNotSubmittingPartner = !IsSubmittingPartner;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isNotSubmittingPartner = true;
        public bool IsNotSubmittingPartner
        {
            get => _isNotSubmittingPartner;
            set
            {
                if (_isNotSubmittingPartner != value)
                {
                    _isNotSubmittingPartner = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _submitButtonTextPartner = "Submit";
        public string SubmitButtonTextPartner
        {
            get => _submitButtonTextPartner;
            set
            {
                if (_submitButtonTextPartner != value)
                {
                    _submitButtonTextPartner = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand PickFileCommandFiller { get; }
        public ICommand PickFileCommandPartner { get; }
        public ICommand SubmitFileFiller { get; }
        public ICommand SubmitFilePartner { get; }
        public Tax106FormViewModelWrapper Tax106FileFillerWrapper { get; private set; }
        public Tax106FormViewModelWrapper Tax106FilePartnerWrapper { get; private set; }

        private Tax106FileParser _tax106FileParser;
        private Tax106FileWorker _tax106FileWorker;

        private string _fillerFormPath;
        private string? _partnerFormPath = null;

        public DefinitionOfForm106ViewModel()
        {
            //ReportSettings.Configuration.FamilyStatus = FamilyStatus.Married;
            ReportSettings.Configuration.RegisteredPartner.FirstName = "Saar";
            ReportSettings.Configuration.RegisteredPartner.LastName = "Ofek";
            IsPreviousEnabled = true;
            _tax106FileParser = new Tax106FileParser();
            _tax106FileWorker = new Tax106FileWorker();


            Tax106FileFillerWrapper = new Tax106FormViewModelWrapper(new Tax106FileViewModel(), ReportSettings.Configuration.RegisteredPartner.DisplayName, true);
            PickFileCommandFiller = new Command(() => Handle106Form(Tax106FileFillerWrapper));
            SubmitFileFiller = new Command(async () => await Submit106FormFiller());

            if (ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married)
            {

                ShouldShowPartner = true;
                Tax106FilePartnerWrapper = new Tax106FormViewModelWrapper(new Tax106FileViewModel(), ReportSettings.Configuration.Partner.DisplayName, true);
                PickFileCommandPartner = new Command(() => Handle106Form(Tax106FilePartnerWrapper));
                SubmitFilePartner = new Command(async () => await Submit106FormPartner());
            }

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

        private async void Handle106Form(Tax106FormViewModelWrapper tax106ViewModelWrapper)
        {
            try
            {
                var form106Path = await PickPdfFile();
                tax106ViewModelWrapper.Tax106File.InternalTax106File = _tax106FileParser.Parse106File(form106Path);

                if (ReferenceEquals(tax106ViewModelWrapper, Tax106FileFillerWrapper))
                {
                    ShouldShowTax106FileDetailsFiller = true;
                    _fillerFormPath = form106Path;
                    IsFillerSubmmited = false;
                    SubmitButtonTextFiller = "Submit";
                }
                else if (ReferenceEquals(tax106ViewModelWrapper, Tax106FilePartnerWrapper))
                {
                    ShouldShowTax106FileDetailsPartner = true;
                    _partnerFormPath = form106Path;
                    IsPartnerSubmmited = false;
                    SubmitButtonTextPartner = "Submit";
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task Submit106FormFiller()
        {
            if (IsFillerSubmmited == true)
            {
                return;
            }

            _tax106FileWorker.Submit(_fillerFormPath, Tax106FileFillerWrapper.Tax106File.InternalTax106File!, isFiller: false);

            try
            {
                IsSubmittingFiller = true;
                SubmitButtonTextFiller = string.Empty;

                // Simulate a delay for the loading animation
                await Task.Delay(1000);

                _tax106FileWorker.Submit(_fillerFormPath, Tax106FileFillerWrapper.Tax106File.InternalTax106File!, isFiller: false);
                IsFillerSubmmited = true;

                SubmitButtonTextFiller = "Submitted!";
                Tax106FileFillerWrapper.IsNotSubmitted = false;
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                IsSubmittingFiller = false;
            }
        }

        private async Task Submit106FormPartner()
        {
            if (IsPartnerSubmmited == true)
            {
                return;
            }

            _tax106FileWorker.Submit(_partnerFormPath, Tax106FilePartnerWrapper.Tax106File.InternalTax106File!, isFiller: false);

            try
            {
                IsSubmittingPartner = true;
                SubmitButtonTextPartner = string.Empty;

                // Simulate a delay for the loading animation
                await Task.Delay(1000);

                _tax106FileWorker.Submit(_partnerFormPath, Tax106FilePartnerWrapper.Tax106File.InternalTax106File!, isFiller: false);
                IsPartnerSubmmited = true;

                SubmitButtonTextPartner = "Submitted!";
                Tax106FilePartnerWrapper.IsNotSubmitted = false;
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            finally
            {
                IsSubmittingPartner = false;
            }
        }
    }

    public class Tax106FormViewModelWrapper : INotifyPropertyChanged
    {
        private Tax106FileViewModel _tax106File;
        public Tax106FileViewModel Tax106File
        {
            get => _tax106File;
            set
            {
                if (_tax106File != value)
                {
                    _tax106File = value;
                    OnPropertyChanged(nameof(Tax106File));
                }
            }
        }

        private bool _isNotSubmitted = true;
        public bool IsNotSubmitted
        {
            get => _isNotSubmitted;
            set
            {
                if (_isNotSubmitted != value)
                {
                    _isNotSubmitted = value;
                    OnPropertyChanged(nameof(IsNotSubmitted));
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private string _name;

        public Tax106FormViewModelWrapper(Tax106FileViewModel tax106File, string name, bool isSubmitted)
        {
            _tax106File = tax106File;
            _isNotSubmitted = isSubmitted;
            Name = name;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
