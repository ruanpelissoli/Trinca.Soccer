using Prism.Navigation;
using Prism.Services;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
           Title = "Trinca Soccer";
        }
    }
}
