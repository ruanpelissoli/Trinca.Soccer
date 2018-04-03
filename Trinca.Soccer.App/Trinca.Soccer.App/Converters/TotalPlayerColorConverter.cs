using System;
using System.Globalization;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Converters
{
    public class TotalPlayerColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var players = value.ToString().Split('/');

            var numberOfPlayer = int.Parse(players[0]);
            var totalPlayers = int.Parse(players[1]);

            if (numberOfPlayer >= totalPlayers)
                return "#21FD88";

            return "#FF4747";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
