using Prism.Unity;
using System.Reflection;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
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
        public App()
        {
            this.InitializeComponent();
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            MainPage = new MainPage();

            ClientApi.Initialize();
            await EmbeddedResourceManager.Initialize(typeof(App).GetTypeInfo().Assembly);

            await NavigationService.NavigateAsync(!Settings.IsLoggedIn ? Routes.Login() : $"app:///{Routes.Matches()}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<CustomNavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
            Container.RegisterTypeForNavigation<MatchesListPage, MatchesListPageViewModel>();
            Container.RegisterTypeForNavigation<NewMatchPage, NewMatchPageViewModel>();
            Container.RegisterTypeForNavigation<MatchPage, MatchPageViewModel>();
            Container.RegisterTypeForNavigation<AddGuestPage, AddGuestPageViewModel>();            
        }

        
    }
}
