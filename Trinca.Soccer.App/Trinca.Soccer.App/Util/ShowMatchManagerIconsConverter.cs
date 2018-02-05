using System;
using System.Globalization;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.Dto.Match;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Util
{
    public class ShowMatchManagerIconsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var match = (MatchOutputDto)value;

            if (match.CreatedBy == Settings.EmployeeId)
                return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
