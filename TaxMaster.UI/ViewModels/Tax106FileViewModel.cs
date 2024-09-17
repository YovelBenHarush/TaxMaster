using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TaxMaster.Infra;

public class Tax106FileViewModel : INotifyPropertyChanged
{
    public Tax106File internalTax106File = new Tax106File();

    public long Field_158_172
    {
        get => internalTax106File._158_172;
        set => SetProperty(ref internalTax106File._158_172, value, nameof(Field_158_172));
    }

    // Add similar properties for each field you need to expose
    public long Field_042
    {
        get => internalTax106File._042;
        set => SetProperty(ref internalTax106File._042, value, nameof(Field_042));
    }

    public long Field_244_245
    {
        get => internalTax106File._244_245;
        set => SetProperty(ref internalTax106File._244_245, value, nameof(Field_244_245));
    }

    public long Field_218_219
    {
        get => internalTax106File._218_219;
        set => SetProperty(ref internalTax106File._218_219, value, nameof(Field_218_219));
    }

    public long Field_086_045
    {
        get => internalTax106File._086_045;
        set => SetProperty(ref internalTax106File._086_045, value, nameof(Field_086_045));
    }

    public long Field_248_249
    {
        get => internalTax106File._248_249;
        set => SetProperty(ref internalTax106File._248_249, value, nameof(Field_248_249));
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
        Debug.WriteLine(internalTax106File);
        return true;
    }
}
