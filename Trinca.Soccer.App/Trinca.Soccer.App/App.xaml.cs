using Prism.Unity;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.ViewModels;
using Trinca.Soccer.App.Views;
using Xamarin.Forms;

namespace Trinca.Soccer.App
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override void OnInitialized()
        {
            InitializeComponent();

            ApiClient.ApiClient.Initialize();

            NavigationService.NavigateAsync(Routes.Login());
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
