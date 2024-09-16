using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaxMaster.Infra;

namespace TaxMaster
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string title = string.Empty;

        private bool isPreviousEnabled = true;

        public Command NextCommand { get; }
        public Command PreviousCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ILogger Logger => LoggerConfiguration.Logger;

        public BaseViewModel()
        {
            NextCommand = new Command(OnNext);
            PreviousCommand = new Command(OnPrevious);
        }

        public virtual string Title
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
            Shell.Current.GoToAsync("..");
        }

        public void OnPropertyChanged([CallerMemberName]string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
