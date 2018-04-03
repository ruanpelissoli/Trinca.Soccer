using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Trinca.Soccer.App.CustomControls;
using Xamarin.Forms;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(Trinca.Soccer.App.CustomControls.Entry), typeof(Trinca.Soccer.App.Droid.Renderers.EntryRenderer))]
namespace Trinca.Soccer.App.Droid.Renderers
{
    public class EntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        public EntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
               
            }
        }
    }
}