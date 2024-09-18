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
            get => "טופס מס 106";
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
        public bool IsFillerSubmitted
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
                    OnPropertyChanged();
                }
            }
        }

        private string _submitButtonTextFiller = "הגש";
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
                    OnPropertyChanged();
                }
            }
        }

        private string _submitButtonTextPartner = "הגש";
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
        public ICommand? PickFileCommandPartner { get; }
        public ICommand SubmitFileFiller { get; }
        public ICommand? SubmitFilePartner { get; }
        public ObservableCollection<Tax106File> Tax106FilesFiller { get; private set; }
        public ObservableCollection<Tax106File>? Tax106FilesPartner { get; private set; } = null;

        private Dictionary<Tax106File, string> _fillerFormPaths = new();
        private Dictionary<Tax106File, string> _partnerFormPaths = new();

        private Tax106FileParser _tax106FileParser = new Tax106FileParser();
        private Tax106FileWorker _tax106FileWorker = new Tax106FileWorker();

        public DefinitionOfForm106ViewModel()
        {
            Tax106FilesFiller = new ObservableCollection<Tax106File>(ReportSettings.Configuration.Tax106Files.User106);
            PickFileCommandFiller = new Command(Add106FormFiller);
            SubmitFileFiller = new Command(Submit106FormFiller);
            if (ReportSettings.Configuration.Tax106Files.User106.Count() != 0)
            {
                ShouldShowTax106FileDetailsFiller = true;
            }

            //if (ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married)
            //{
            //    Tax106FilesPartner = new ObservableCollection<Tax106File>(ReportSettings.Configuration.Tax106Files.Partner106);
            //    PickFileCommandPartner = new Command(Add106FormPartner);
            //    SubmitFilePartner = new Command(Submit106FormPartner);
            //    if (ReportSettings.Configuration.Tax106Files.Partner106.Count() != 0)
            //    {
            //        ShouldShowTax106FileDetailsPartner = true;
            //    }
            //}
        }

        private async void Add106FormFiller()
        {
            var form106Path = await PickPdfFile();
            var tax106File = _tax106FileParser.Parse106File(form106Path);

            Tax106FilesFiller.Add(tax106File);
            _fillerFormPaths[tax106File] = form106Path;

            ResetSubmittionFiller();
        }

        private void ResetSubmittionFiller()
        {
            ShouldShowTax106FileDetailsFiller = true;
            IsFillerSubmitted = false;
            IsSubmittingFiller = false;
            SubmitButtonTextFiller = "הגש";
        }

        //private async void Handle106Form(Tax106FormViewModelWrapper tax106ViewModelWrapper)
        //{
        //    try
        //    {
        //        var form106Path = await PickPdfFile();
        //        tax106ViewModelWrapper.Tax106File.InternalTax106File = _tax106FileParser.Parse106File(form106Path);

        //        if (ReferenceEquals(tax106ViewModelWrapper, Tax106FileFiller))
        //        {
        //            ShouldShowTax106FileDetailsFiller = true;
        //            _fillerFormPath = form106Path;
        //            IsFillerSubmmited = false;
        //            SubmitButtonTextFiller = "Submit";
        //        }
        //        else if (ReferenceEquals(tax106ViewModelWrapper, Tax106FilePartnerWrapper))
        //        {
        //            ShouldShowTax106FileDetailsPartner = true;
        //            _partnerFormPath = form106Path;
        //            IsPartnerSubmmited = false;
        //            SubmitButtonTextPartner = "Submit";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        private async void Submit106FormFiller()
        {
            if (IsFillerSubmitted == true)
            {
                return;
            }

            _tax106FileWorker.Submit(_fillerFormPaths.Values.ToList(), Tax106FilesFiller.ToList(), isFiller: true);

            IsSubmittingFiller = true;

            await Task.Delay(1000);

            SubmitButtonTextFiller = "הוגש בהצלחה!";
            IsSubmittingFiller = false;
            IsFillerSubmitted = true;
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
