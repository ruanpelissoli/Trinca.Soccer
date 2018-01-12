using Prism.Navigation;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchesPageViewModel : BaseViewModel
    {
        public MatchesPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Trinca Soccer";
        }
    }
}
