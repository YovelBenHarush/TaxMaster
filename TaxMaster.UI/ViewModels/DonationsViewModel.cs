using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class DonationsViewModel : BaseViewModel
    {
        public ObservableCollection<DonationEntry> RegisteredPartnerDonations { get; }
        public ObservableCollection<DonationEntry> PartnerDonations { get; }


        public ICommand AddDonationCommand { get; }

        public ICommand RemoveDonationCommand { get; }

        public ICommand PickFileCommand { get; }

        public bool IsNotSingle => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public DonationsViewModel()
        {
            Title = "תרומות";
            RegisteredPartnerDonations = new ObservableCollection<DonationEntry>(ReportSettings.Configuration.RegisteredPartner.Donations.DonationsList);
            PartnerDonations = new ObservableCollection<DonationEntry>(ReportSettings.Configuration.Partner.Donations.DonationsList);

            AddDonationCommand = new Command<string>(AddDonation);
            RemoveDonationCommand = new Command<object>(RemoveDonation);

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
        }

        private void AddDonation(string parameter)
        {
            var newEntry = new DonationEntry();
            if (parameter == "RegisteredPartner")
            {
                RegisteredPartnerDonations.Add(newEntry);
            }
            else
            {
                PartnerDonations.Add(newEntry);
            }
        }

        private void RemoveDonation(object entry)
        {
            if (RegisteredPartnerDonations.Contains(entry))
            {
                RegisteredPartnerDonations.Remove((DonationEntry)entry);
            }
            else
            {
                PartnerDonations.Remove((DonationEntry)entry);
            }
        }

        private async Task PickPdfFile(object entry)
        {
            ((DonationEntry)entry).ReciptPath = await PickPdfFile();

            if (RegisteredPartnerDonations.Contains(entry))
            {
                RegisteredPartnerDonations.Remove((DonationEntry)entry);
                RegisteredPartnerDonations.Add((DonationEntry)entry);
            }
            else
            {
                PartnerDonations.Remove((DonationEntry)entry);
                PartnerDonations.Add((DonationEntry)entry);
            }
        }

        public async override void OnNext()
        {
            for (int i = 0; i < RegisteredPartnerDonations.Count; i++)
            {
                if (ReportSettings.Configuration.RegisteredPartner.Donations.DonationsList.Count <= i || RegisteredPartnerDonations[i] != ReportSettings.Configuration.RegisteredPartner.Donations.DonationsList[i])
                {
                    var copy = ReportSettings.SaveToOutputDir(RegisteredPartnerDonations[i].ReciptPath, $"{ReportSettings.Configuration.RegisteredPartner.ID}_donation_{i}.pdf");
                    if (!string.IsNullOrEmpty(copy))
                    {
                        RegisteredPartnerDonations[i].ReciptPath = copy;
                    }
                }
            }
            ReportSettings.Configuration.RegisteredPartner.Donations.DonationsList = [.. RegisteredPartnerDonations];

            for (int i = 0; i < PartnerDonations.Count; i++)
            {
                if (ReportSettings.Configuration.Partner.Donations.DonationsList.Count <= i || PartnerDonations[i] != ReportSettings.Configuration.Partner.Donations.DonationsList[i])
                {
                    var copy = ReportSettings.SaveToOutputDir(PartnerDonations[i].ReciptPath, $"{ReportSettings.Configuration.Partner.ID}_donation_{i}.pdf");
                    if (!string.IsNullOrEmpty(copy))
                    {
                        PartnerDonations[i].ReciptPath = copy;
                    }
                }
            }
            ReportSettings.Configuration.Partner.Donations.DonationsList = [.. PartnerDonations];

            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportFirstStepPage));
        }
    }
}
