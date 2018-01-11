using Prism.Navigation;

namespace Trinca.Soccer.App.ViewModels
{
    public class LoadingGamesPageViewModel : BaseViewModel
    {
        public LoadingGamesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Games";
        }
    }
}
