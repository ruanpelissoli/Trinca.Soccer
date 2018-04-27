using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;

namespace Trinca.Soccer.App.ViewModels
{
    public class CustomNavigationPageViewModel : BaseViewModel
    {
        public DelegateCommand LogoutCommand { get; set; }

        public CustomNavigationPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            LogoutCommand = new DelegateCommand(LogoutCommandExecute);
        }

        private async void LogoutCommandExecute()
        {
            Settings.Clear();
            await NavigationService.NavigateAsync($"app:///{Routes.Login()}");
        }
    }
}
