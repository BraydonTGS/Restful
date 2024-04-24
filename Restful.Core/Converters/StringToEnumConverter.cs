using System.Globalization;
using System.Windows.Data;

namespace Restful.Core.Converters
{
    public class StringToEnumConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue && targetType.IsEnum)
            {
                return Enum.Parse(targetType, strValue, true);
            }
            return null;
        }
    }
}
