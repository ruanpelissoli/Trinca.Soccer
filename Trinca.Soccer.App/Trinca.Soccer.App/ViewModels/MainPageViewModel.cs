using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public DelegateCommand GoToConfigurationPageCommand { get; set; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;

            GoToConfigurationPageCommand = new DelegateCommand(GoToConfigurationPageCommandExecute);
        }

        private async void GoToConfigurationPageCommandExecute()
        {
            await NavigateTo($"{Routes.Configuration()}", modal: true);
        }
    }
}
