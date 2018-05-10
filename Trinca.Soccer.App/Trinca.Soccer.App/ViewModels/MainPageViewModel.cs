using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;
        }        
    }
}
