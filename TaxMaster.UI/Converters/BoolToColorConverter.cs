using System.Globalization;

namespace TaxMaster.UI
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert the string to a boolean: return true if string is not null or empty
            if (value is bool boolValue)
            {
                if (boolValue)
                {
                    return Colors.Green;
                }
                else
                {
                    return Colors.White;
                }
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // ConvertBack is usually not needed for this kind of one-way binding
            throw new NotImplementedException();
        }
    }
}
