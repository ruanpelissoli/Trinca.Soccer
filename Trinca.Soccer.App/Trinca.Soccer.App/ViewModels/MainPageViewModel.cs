using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public DelegateCommand LogoutCommand { get; set; }

        private bool _joinMatchIsVisibile;
        public bool JoinMatchIsVisibile
        {
            get => _joinMatchIsVisibile;
            set => SetProperty(ref _joinMatchIsVisibile, value);
        }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;
            
            LogoutCommand = new DelegateCommand(LogoutCommandExecute);
        }

        private async void LogoutCommandExecute()
        {
            Settings.Clear();
            await NavigationService.NavigateAsync($"app:///{Routes.Login()}");
        }
    }
}
