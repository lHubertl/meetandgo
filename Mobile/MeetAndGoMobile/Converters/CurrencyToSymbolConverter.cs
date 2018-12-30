using System;
using System.Globalization;
using MeetAndGo.Shared.Tools;
using Xamarin.Forms;

namespace MeetAndGoMobile.Converters
{
    internal class CurrencyToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string currency)
            {
                if (CurrencyTools.TryGetCurrencySymbol(currency, out var symbol))
                {
                    return symbol;
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
