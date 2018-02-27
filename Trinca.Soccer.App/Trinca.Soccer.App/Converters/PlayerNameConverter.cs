using System;
using System.Globalization;
using Trinca.Soccer.Dto.Player;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Converters
{
    public class PlayerNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var player = (PlayerOutputDto)value;

            if (player.IsGuest)
                return player.GuestName;

            return player.Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
