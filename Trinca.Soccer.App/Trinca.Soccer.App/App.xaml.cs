using Prism.Unity;
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

        protected override void OnInitialized()
        {
            InitializeComponent();
            
            ClientApi.Initialize();

            NavigationService.NavigateAsync(!Settings.IsLoggedIn ? Routes.Login() : Routes.Matches());
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage, LoginPageViewModel>();
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>();
            Container.RegisterTypeForNavigation<MatchesListPage, MatchesListPageViewModel>();
            Container.RegisterTypeForNavigation<NewMatchPage, NewMatchPageViewModel>();
            Container.RegisterTypeForNavigation<MatchPage, MatchPageViewModel>();
        }
    }
}
