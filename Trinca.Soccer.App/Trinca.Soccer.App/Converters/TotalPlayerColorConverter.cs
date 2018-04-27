using System;
using System.Globalization;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Converters
{
    public class TotalPlayerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Application.Current.Resources["DestructiveButtonColor"].ToString();

            var players = value.ToString().Split('/');

            var numberOfPlayer = int.Parse(players[0]);
            var totalPlayers = int.Parse(players[1]);

            if (numberOfPlayer >= totalPlayers)
                return Application.Current.Resources["MainButtonColor"].ToString();

            return Application.Current.Resources["DestructiveButtonColor"].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
