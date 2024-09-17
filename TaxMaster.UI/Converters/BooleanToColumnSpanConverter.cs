using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace TaxMaster.UI
{
    public class BooleanToColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && isVisible)
            {
                return 1; // Show in one column
            }
            return 2; // Span across two columns
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
