using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TaxMaster
{
    public class LifeInsuranceViewModel : BaseViewModel
    {
        public ObservableCollection<InsuranceEntry> UserInsurances { get; set; }
        public ObservableCollection<InsuranceEntry> PartnerInsurances { get; set; }

        public ICommand AddInsuranceCommand { get; }
        public ICommand RemoveInsuranceCommand { get; }

        public LifeInsuranceViewModel()
        {
            UserInsurances = new ObservableCollection<InsuranceEntry>();
            PartnerInsurances = new ObservableCollection<InsuranceEntry>();

            AddInsuranceCommand = new Command<string>(AddInsurance);
            RemoveInsuranceCommand = new Command<object>(RemoveInsurance);
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
                UserInsurances.Remove(entry as InsuranceEntry);
            }
            else
            {
                PartnerInsurances.Remove(entry as InsuranceEntry);
            }
        }

        private void PickLocalCopy()
        {
            // Implement logic to pick a local copy of the life insurance
        }

        public override async void OnNext()
        {
            await Shell.Current.GoToAsync(nameof(BirthAllowanceView));
        }
    }

    public class InsuranceEntry
    {
        public string Company { get; set; }
        public string AnnualAmount { get; set; }
        public string PolicyPath { get; set; }
    }

}
