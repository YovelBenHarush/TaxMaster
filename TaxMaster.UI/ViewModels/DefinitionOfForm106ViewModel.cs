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

        private string _tax106Guide;
        public string Tax106Guide
        {
            get => _tax106Guide;
            set
            {
                if (_tax106Guide != value)
                {
                    _tax106Guide = value;
                    OnPropertyChanged();
                }
            }
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

        private string _fillerName;
        public string FillerName
        {
            get => _fillerName;
            set
            {
                if (_fillerName != value)
                {
                    _fillerName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _partnerName;
        public string PartnerName
        {
            get => _partnerName;
            set
            {
                if (_partnerName != value)
                {
                    _partnerName = value;
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
        public bool IsPartnerSubmitted
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

        public ICommand DeleteTax106FileCommandFiller { get; }
        public ICommand DeleteTax106FileCommandPartner { get; }
        public ObservableCollection<Tax106File> Tax106FilesFiller { get; private set; }
        public ObservableCollection<Tax106File>? Tax106FilesPartner { get; private set; } = null;

        private Dictionary<Tax106File, string> _fillerFormPaths = new();
        private Dictionary<Tax106File, string> _partnerFormPaths = new();

        private Tax106FileParser _tax106FileParser = new Tax106FileParser();
        private Tax106FileWorker _tax106FileWorker = new Tax106FileWorker();

        public DefinitionOfForm106ViewModel()
        {
            Tax106Guide = GuidesConfigurations.Tax106GuidePdfUrl;

            Tax106FilesFiller = new ObservableCollection<Tax106File>(ReportSettings.Configuration.RegisteredPartner.Tax106FilesWrapper.taxFiles ?? new List<Tax106File>());
            PickFileCommandFiller = new Command(Add106FormFiller);
            SubmitFileFiller = new Command(Submit106FormFiller);
            DeleteTax106FileCommandFiller = new Command<object>(DeleteTax106FileFiller);
            FillerName = ReportSettings.Configuration.RegisteredPartner.DisplayName;
            if (ReportSettings.Configuration.RegisteredPartner.Tax106FilesWrapper.taxFiles != null && ReportSettings.Configuration.RegisteredPartner.Tax106FilesWrapper.taxFiles?.Count != 0)
            {
                ShouldShowTax106FileDetailsFiller = true;
            }

            if (ReportSettings.Configuration.FamilyStatus == FamilyStatus.Married)
            {
                ShouldShowPartner = true;
                Tax106FilesPartner = new ObservableCollection<Tax106File>(ReportSettings.Configuration.Partner.Tax106FilesWrapper.taxFiles ?? new List<Tax106File>());
                PickFileCommandPartner = new Command(Add106FormPartner);
                SubmitFilePartner = new Command(Submit106FormPartner);
                DeleteTax106FileCommandPartner = new Command<object>(DeleteTax106FilePartner);
                PartnerName = ReportSettings.Configuration.Partner.DisplayName;
                if (ReportSettings.Configuration.Partner.Tax106FilesWrapper.taxFiles != null && ReportSettings.Configuration.Partner.Tax106FilesWrapper.taxFiles?.Count != 0)
                {
                    ShouldShowTax106FileDetailsPartner = true;
                }
            }
        }

        private async void Add106FormFiller()
        {
            try
            {
                var form106Path = await PickPdfFile();
                var tax106File = _tax106FileParser.Parse106File(form106Path);

                Tax106FilesFiller.Add(tax106File);
                _fillerFormPaths[tax106File] = form106Path;

                ResetSubmittionFiller();
            }
            catch (Exception) { }
        }

        private void ResetSubmittionFiller()
        {
            ShouldShowTax106FileDetailsFiller = Tax106FilesFiller.Count != 0;
            IsFillerSubmitted = false;
            IsSubmittingFiller = false;
            SubmitButtonTextFiller = "הגש";
        }

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

        public void DeleteTax106FileFiller(object parameter)
        {
            var tax106File = parameter as Tax106File;
            Tax106FilesFiller.Remove(tax106File);
            _fillerFormPaths.Remove(tax106File);
            if (Tax106FilesFiller.Count == 0)
            {
                _tax106FileWorker.Submit(_fillerFormPaths.Values.ToList(), Tax106FilesFiller!.ToList(), isFiller: true);
            }
            ResetSubmittionFiller();
        }

        private async void Add106FormPartner()
        {
            try
            {
                var form106Path = await PickPdfFile();
                var tax106File = _tax106FileParser.Parse106File(form106Path);

                Tax106FilesPartner!.Add(tax106File);
                _partnerFormPaths[tax106File] = form106Path;

                ResetSubmittionPartner();
            }
            catch (Exception) { }
        }

        private void ResetSubmittionPartner()
        {
            ShouldShowTax106FileDetailsPartner = Tax106FilesPartner?.Count != 0;
            IsPartnerSubmitted = false;
            IsSubmittingPartner = false;
            SubmitButtonTextPartner = "הגש";
        }

        private async void Submit106FormPartner()
        {
            if (IsPartnerSubmitted == true)
            {
                return;
            }

            _tax106FileWorker.Submit(_partnerFormPaths.Values.ToList(), Tax106FilesPartner!.ToList(), isFiller: false);

            IsSubmittingPartner = true;

            await Task.Delay(1000);

            SubmitButtonTextPartner = "הוגש בהצלחה!";
            IsSubmittingPartner = false;
            IsPartnerSubmitted = true;
        }
        public void DeleteTax106FilePartner(object parameter)
        {
            var tax106File = parameter as Tax106File;
            Tax106FilesPartner!.Remove(tax106File!);
            _partnerFormPaths!.Remove(tax106File!);
            if (Tax106FilesPartner.Count == 0)
            {
                _tax106FileWorker.Submit(_partnerFormPaths.Values.ToList(), Tax106FilesPartner!.ToList(), isFiller: false);
            }
            ResetSubmittionPartner();
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
}
