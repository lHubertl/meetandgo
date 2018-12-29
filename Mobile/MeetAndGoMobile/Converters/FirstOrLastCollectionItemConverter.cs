using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace MeetAndGoMobile.Converters
{
    internal class FirstOrLastCollectionItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<object> collection)
            {
                if (parameter is string strParameter)
                {
                    if (string.Equals(strParameter, "last", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return collection.LastOrDefault();
                    }
                }

                return collection.FirstOrDefault();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
