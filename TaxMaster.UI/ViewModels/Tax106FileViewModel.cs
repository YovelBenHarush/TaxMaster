using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TaxMaster.Infra;

public class Tax106FileViewModel : INotifyPropertyChanged
{
    private Tax106File? _internalTax106File = null;

    public Tax106File? InternalTax106File
    {
        get => _internalTax106File;
        set => SetProperty(ref _internalTax106File, value, nameof(InternalTax106File));
    }

    // Add similar properties for each field you need to expose
    public Tax106FileViewModel(Tax106File? tax106File)
    {
        InternalTax106File = tax106File;
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T storage, T value, string propertyName)
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
            return false;

        storage = value;
        OnPropertyChanged();
        Debug.WriteLine(_internalTax106File);
        return true;
    }

    protected bool SetProperty(ref long storage, long value, string propertyName)
    {
        if (!long.TryParse(value.ToString(), out long validValue))
        {
            validValue = 0;
        }

        if (storage == validValue)
            return false;

        storage = validValue;
        OnPropertyChanged();
        Debug.WriteLine(_internalTax106File);
        return true;
    }
}
