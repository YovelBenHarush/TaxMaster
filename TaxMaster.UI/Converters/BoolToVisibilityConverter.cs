using System;
using System.Globalization;

namespace TaxMaster
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value; // If true, return true (visible), otherwise false (hidden).
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // You typically don't need this for one-way bindings.
        }
    }
}