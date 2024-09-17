using System;
using System.Globalization;

namespace TaxMaster
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Invert the boolean value and return it for visibility
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // You typically don't need this for one-way bindings, but we can implement it if necessary
            return !(bool)value;
        }
    }
}