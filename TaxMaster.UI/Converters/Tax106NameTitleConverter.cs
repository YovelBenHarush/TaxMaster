using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace TaxMaster.UI
{
    public class Tax106NameTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "טען טופס 106 - " + (value ?? string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
