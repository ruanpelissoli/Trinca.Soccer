using System;
using System.Globalization;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Util
{
    public class DecimalConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "0";
            var thedecimal = (decimal)value;
            return thedecimal.ToString("N");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var strValue = value as string;
            if (string.IsNullOrEmpty(strValue))
                strValue = "0";
            decimal resultdecimal;
            return decimal.TryParse(strValue, out resultdecimal) ? resultdecimal : 0;
        }

    }
}
