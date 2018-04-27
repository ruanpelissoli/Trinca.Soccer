using Trinca.Soccer.App.Dependencies;
using Xamarin.Forms;

namespace Trinca.Soccer.App.Helpers
{
    public static class StatusBarColor
    {
        public static void Set(string color)
        {
            var nav = DependencyService.Get<IStatusBarColor>();

            var xamColor = Color.FromHex(color);
            nav?.SetColor(xamColor.AddLuminosity(-0.01));
        }
    }
}
