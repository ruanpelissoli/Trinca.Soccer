using Android.Content;
using Android.Graphics;
using Android.Support.V7.Widget;
using Trinca.Soccer.App.Droid.Renderers;
using Trinca.Soccer.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationPageRenderer))]
namespace Trinca.Soccer.App.Droid.Renderers
{
    public class CustomNavigationPageRenderer : NavigationPageRenderer
    {
        private Toolbar _toolbar;

        public CustomNavigationPageRenderer(Context context)
                     : base(context)
        {
        }

        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Toolbar))
            {
                _toolbar = (Toolbar)child;
                _toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }

        private void Toolbar_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();

            if (view == typeof(AppCompatTextView))
            {
                var textView = (AppCompatTextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, "Rubik-Medium.ttf");
                textView.Typeface = spaceFont;
                _toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
        }
    }
}