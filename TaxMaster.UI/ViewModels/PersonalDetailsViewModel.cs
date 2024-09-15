using System.Collections.ObjectModel;
using TaxMaster.Infra.Entities;

namespace TaxMaster
{
    public class PersonalDetailsViewModel : BaseViewModel
    {
        private bool isMarried;
        public PersonalInfo PersonalInfo { get; set; }

        public ObservableCollection<int> Years { get; set; }
        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<string> FamilyStatus { get; set; }
        private string _selectedFamilyStatus;
        public string SelectedFamilyStatus
        {
            get => _selectedFamilyStatus;
            set
            {
                if (_selectedFamilyStatus != value)
                {
                    _selectedFamilyStatus = value;
                    if (value == "נשוי")
                    {
                        isMarried = true;
                    }
                    else
                    {
                        isMarried = false;
                    }

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsMarried));
                }
            }
        }

        public bool IsMarried => isMarried;
        public ObservableCollection<string> Genders { get; set; }

        public PersonalDetailsViewModel()
        {
            Years = new ObservableCollection<int>(Enumerable.Range(DateTime.Now.Year - 6, 7).Reverse());
            FamilyStatus = new ObservableCollection<string> { "רווק", "נשוי" };
            Genders = new ObservableCollection<string> { "ז", "נ" };

            SelectedFamilyStatus = FamilyStatus.First();
            SelectedYear = Years.First();

        }

        public override void OnNext()
        {
            throw new NotImplementedException();
        }
    }
}
