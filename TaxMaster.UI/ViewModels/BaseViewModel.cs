using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaxMaster
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string title = string.Empty;

        private bool isPreviousEnabled = false;

        public Command NextCommand { get; }
        public Command PreviousCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public BaseViewModel()
        {
            NextCommand = new Command(OnNext);
            PreviousCommand = new Command(OnPrevious);
        }

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

        public bool IsPreviousEnabled
        {
            get => isPreviousEnabled;
            set
            {
                if (isPreviousEnabled == value)
                    return;
                isPreviousEnabled = value;
                OnPropertyChanged();
            }
        }

        public virtual void OnNext()
        {
            return;
        }

        public virtual void OnPrevious()
        {
            return;
        }

        public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
