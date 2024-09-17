﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaxMaster.Infra;

namespace TaxMaster
{
    public class LifeInsuranceViewModel : BaseViewModel
    {
        public ObservableCollection<InsuranceEntry> UserInsurances { get; set; }
        public ObservableCollection<InsuranceEntry> PartnerInsurances { get; set; }

        public ICommand AddInsuranceCommand { get; }
        public ICommand RemoveInsuranceCommand { get; }

        public ICommand PickFileCommand { get; }

        public bool IsNotSingle => ReportSettings.Configuration.FamilyStatus != Infra.Entities.FamilyStatus.Single;

        public LifeInsuranceViewModel()
        {
            UserInsurances = new ObservableCollection<InsuranceEntry>();
            PartnerInsurances = new ObservableCollection<InsuranceEntry>();

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
        }

        public override async void OnNext()
        {
            ReportSettings.Configuration.LifeInsurences.UserInsurances = UserInsurances.ToList();
            ReportSettings.Configuration.LifeInsurences.PartnerInsurances = PartnerInsurances.ToList();

            base.OnNext();

            await Shell.Current.GoToAsync(nameof(BirthAllowanceView));
        }
    }
}
