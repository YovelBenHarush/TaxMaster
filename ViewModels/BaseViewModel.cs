using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaxMaster
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        string title = string.Empty;
        public event PropertyChangedEventHandler? PropertyChanged;


        public string Title
        {
            get => title;
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged();
            }
        }

        public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
