using System.Collections.ObjectModel;
using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class DonationsViewModel : BaseViewModel
    {
        public ObservableCollection<DonationEntry> Donations { get; }

        public ICommand AddDonationCommand { get; }

        public ICommand RemoveDonationCommand { get; }

        public ICommand PickFileCommand { get; }

        public bool IsNotSingle => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public DonationsViewModel()
        {
            Title = "תרומות";
            Donations = new ObservableCollection<DonationEntry>(ReportSettings.Configuration.Donations.DonationsList);

            AddDonationCommand = new Command(AddDonation);
            RemoveDonationCommand = new Command<object>(RemoveDonation);

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
        }

        private void AddDonation()
        {
            var newEntry = new DonationEntry();
            Donations.Add(newEntry);
        }

        private void RemoveDonation(object entry)
        {
            if (Donations.Contains(entry))
            {
                Donations.Remove((DonationEntry)entry);
            }
        }

        private async Task PickPdfFile(object entry)
        {
            ((DonationEntry)entry).ReciptPath = await PickPdfFile();

            if (Donations.Contains(entry))
            {
                Donations.Remove((DonationEntry)entry);
                Donations.Add((DonationEntry)entry);
            }
        }

        public async override void OnNext()
        {
            ReportSettings.Configuration.Donations.DonationsList = [.. Donations];

            base.OnNext();
            await Shell.Current.GoToAsync(nameof(AnnualReportFirstStepPage));
        }
    }
}
