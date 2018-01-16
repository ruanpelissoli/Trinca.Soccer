using Prism.Navigation;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService) : base(navigationService)
        {
           Title = "Trinca Soccer";
        }
    }
}
