using System;
using System.Globalization;
using Trinca.Soccer.Dto.Player;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Converters
{
    public class PlayerPictureUrlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            var player = (PlayerOutputDto)value;

            if (player.IsGuest)
                return "http://strongvoicespublishing.com/wp-content/uploads/2017/06/user.png";

            return player.Employee.PictureUrl;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
