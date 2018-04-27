using Trinca.Soccer.App.Dependencies;
using Trinca.Soccer.App.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarColor))]
namespace Trinca.Soccer.App.iOS.Helpers
{
    public class StatusBarColor : IStatusBarColor
    {
        public void SetColor(Color color)
        {
           
        }
    }
}