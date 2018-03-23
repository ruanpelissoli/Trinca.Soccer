using System;
using System.Globalization;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism.Unity;
using Microsoft.Practices.Unity;
using PCLAppConfig;
using ImageCircle.Forms.Plugin.Droid;

namespace Trinca.Soccer.App.Droid
{
    [Activity(Label = "Trinca Soccer", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            AppDomain.CurrentDomain.UnhandledException += (sender, unhandledExceptionEventArgs) =>
            {
                Console.WriteLine(new Exception("UnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception).StackTrace);
            };

            global::Xamarin.Forms.Forms.Init(this, bundle);

            ImageCircleRenderer.Init();
            try
            {
                ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);
            }
            catch { }

            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}

