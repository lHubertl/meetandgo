using System;
using System.Globalization;
using Xamarin.Forms;

namespace MeetAndGoMobile.Converters
{
    internal class CurrencyToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string currency)
            {
                // TODO:
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
