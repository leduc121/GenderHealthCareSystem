using System;
using System.Globalization;
using System.Windows.Data;

namespace GenderHealthcare.UI.Converters
{
    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status && parameter is string param)
            {
                var options = param.Split('|');
                return status ? options[0] : options[1];
            }
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}