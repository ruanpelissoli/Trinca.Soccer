using Trinca.Soccer.App.iOS.CustomRenderers;
using Trinca.Soccer.App.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace Trinca.Soccer.App.iOS.CustomRenderers
{
    public class CustomNavigationPageRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("Rubik-Medium", 20);
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
            }
        }
    }
}