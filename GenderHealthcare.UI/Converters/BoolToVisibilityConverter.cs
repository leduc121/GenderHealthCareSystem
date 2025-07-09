using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GenderHealthcare.UI.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type _, object __, CultureInfo ___) =>
            value is bool b && b ? Visibility.Visible : Visibility.Collapsed;

        public object ConvertBack(object value, Type _, object __, CultureInfo ___) =>
            throw new NotImplementedException();
    }


}
