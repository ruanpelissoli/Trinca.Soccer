
using Android.App;
using Trinca.Soccer.App.Dependencies;
using Trinca.Soccer.App.Droid.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(StatusBarColor))]
namespace Trinca.Soccer.App.Droid.Helpers
{
    public class StatusBarColor : IStatusBarColor
    {
        public void SetColor(Color color)
        {
            ((Activity)Forms.Context).Window.SetStatusBarColor(color.AddLuminosity(-0.1).ToAndroid());
        }
    }
}