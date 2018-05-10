using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;

namespace Trinca.Soccer.App.ViewModels
{
    public class CustomNavigationPageViewModel : BaseViewModel
    {
        public DelegateCommand GoToConfigurationPageCommand { get; set; }

        public CustomNavigationPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToConfigurationPageCommand = new DelegateCommand(GoToConfigurationPageCommandExecute);
        }

        private async void GoToConfigurationPageCommandExecute()
        {
            await NavigateTo($"{Routes.Configuration()}");
        }
    }
}
