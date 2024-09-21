using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class LifeInsuranceViewModel : BaseViewModel
    {
        public ObservableCollection<InsuranceEntry> UserInsurances { get; }
        public ObservableCollection<InsuranceEntry> PartnerInsurances { get; }

        public ICommand AddInsuranceCommand { get; }

        public ICommand RemoveInsuranceCommand { get; }

        public ICommand PickFileCommand { get; }

        public bool IsNotSingle => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public LifeInsuranceViewModel()
        {
            Title = "נתוני ביטוחי חיים";

            UserInsurances =
                [
                    new()
                ];
            PartnerInsurances =
                [
                new()
                ];

            if (ReportSettings.Configuration?.RegisteredPartner?.LifeInsurences?.InsurencesList != null)
            {
                UserInsurances = new ObservableCollection<InsuranceEntry>(ReportSettings.Configuration.RegisteredPartner.LifeInsurences.InsurencesList);
                PartnerInsurances = new ObservableCollection<InsuranceEntry>(ReportSettings.Configuration.Partner.LifeInsurences.InsurencesList);
            }

            if (UserInsurances.Count == 0)
            {
                UserInsurances =
                [
                    new()
                ];
            }

            if (PartnerInsurances.Count == 0)
            {
                PartnerInsurances =
                [
                    new()
                ];
            }

            AddInsuranceCommand = new Command<string>(AddInsurance);
            RemoveInsuranceCommand = new Command<object>(RemoveInsurance);

            PickFileCommand = new Command<object>(async (parameter) => await PickPdfFile(parameter));
        }

        private void AddInsurance(string parameter)
        {
            var newEntry = new InsuranceEntry();
            if (parameter == "User")
            {
                UserInsurances.Add(newEntry);
            }
            else
            {
                PartnerInsurances.Add(newEntry);
            }
        }

        private void RemoveInsurance(object entry)
        {
            if (UserInsurances.Contains(entry))
            {
                UserInsurances.Remove((InsuranceEntry)entry);
            }
            else
            {
                PartnerInsurances.Remove((InsuranceEntry)entry);
            }
        }

        private async Task PickPdfFile(object entry)
        {
            ((InsuranceEntry)entry).PolicyPath = await PickPdfFile();

            if (UserInsurances.Contains(entry))
            {
                UserInsurances.Remove((InsuranceEntry)entry);
                UserInsurances.Add((InsuranceEntry)entry);
            }
            else
            {
                PartnerInsurances.Remove((InsuranceEntry)entry);
                PartnerInsurances.Add((InsuranceEntry)entry);
            }
        }

        public override async void OnNext()
        {
            for (int i = 0; i < UserInsurances.Count; i++)
            {
                if (ReportSettings.Configuration.RegisteredPartner.LifeInsurences.InsurencesList.Count <= i || UserInsurances[i].PolicyPath != ReportSettings.Configuration.RegisteredPartner.LifeInsurences.InsurencesList[i].PolicyPath)
                {
                    var copy = ReportSettings.SaveToOutputDir(UserInsurances[i].PolicyPath, $"{ReportSettings.Configuration.RegisteredPartner.ID}_life_insurance_policy_{i}.pdf");
                    if (!string.IsNullOrEmpty(copy))
                    {
                        UserInsurances[i].PolicyPath = copy;
                    }
                }
            }

            for (int i = 0; i < PartnerInsurances.Count; i++)
            {
                if (ReportSettings.Configuration.Partner.LifeInsurences.InsurencesList.Count <= i || PartnerInsurances[i].PolicyPath != ReportSettings.Configuration.Partner.LifeInsurences.InsurencesList[i].PolicyPath)
                {
                    var copy = ReportSettings.SaveToOutputDir(PartnerInsurances[i].PolicyPath, $"{ReportSettings.Configuration.Partner.ID}_life_insurance_policy_{i}.pdf");
                    if (!string.IsNullOrEmpty(copy))
                    {
                        PartnerInsurances[i].PolicyPath = copy;
                    }
                }
            }

            ReportSettings.Configuration.RegisteredPartner.LifeInsurences.InsurencesList = [.. UserInsurances];
            ReportSettings.Configuration.Partner.LifeInsurences.InsurencesList = [.. PartnerInsurances];

            base.OnNext();

            await Shell.Current.GoToAsync(nameof(BirthAllowanceView));
        }
    }
}
