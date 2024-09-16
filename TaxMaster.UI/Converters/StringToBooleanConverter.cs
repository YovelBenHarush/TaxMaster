using System.Globalization;

namespace TaxMaster.UI
{
    public class StringToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the string to a boolean: return true if string is not null or empty
            if (value is string stringValue)
            {
                return !string.IsNullOrEmpty(stringValue);
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack is usually not needed for this kind of one-way binding
            throw new NotImplementedException();
        }
    }
}
