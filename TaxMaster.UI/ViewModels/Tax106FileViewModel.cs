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

    public long Field_158_172
    {
        get => _internalTax106File?._158_172 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._158_172, value, nameof(Field_158_172));
            }
        }
    }

    // Add similar properties for each field you need to expose
    public long Field_042
    {
        get => _internalTax106File?._042 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._042, value, nameof(Field_042));
            }
        }
    }

    public long Field_244_245
    {
        get => _internalTax106File?._244_245 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._244_245, value, nameof(Field_244_245));
            }
        }
    }

    public long Field_218_219
    {
        get => _internalTax106File?._218_219 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._218_219, value, nameof(Field_218_219));
            }
        }
    }

    public long Field_086_045
    {
        get => _internalTax106File?._086_045 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._086_045, value, nameof(Field_086_045));
            }
        }
    }

    public long Field_248_249
    {
        get => _internalTax106File?._248_249 ?? 0;
        set
        {
            if (_internalTax106File != null)
            {
                SetProperty(ref _internalTax106File._248_249, value, nameof(Field_248_249));
            }
        }
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
