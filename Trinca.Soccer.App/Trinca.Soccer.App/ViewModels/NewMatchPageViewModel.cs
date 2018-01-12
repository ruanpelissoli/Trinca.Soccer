using Prism.Navigation;

namespace Trinca.Soccer.App.ViewModels
{
    public class NewMatchPageViewModel : BaseViewModel
    {
        public NewMatchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Match";
        }
    }
}
