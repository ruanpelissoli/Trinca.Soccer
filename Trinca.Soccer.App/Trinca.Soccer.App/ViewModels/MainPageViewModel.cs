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
            
            MessagingCenter.Subscribe<MatchPageViewModel, string>(this, Strings.TitleChange, OnTitleChanged);

            LogoutCommand = new DelegateCommand(LogoutCommandExecute);
        }

        private void OnTitleChanged(MatchPageViewModel source, string title)
        {
            Title = title;
        }

        private async void LogoutCommandExecute()
        {
            Settings.Clear();
            await NavigationService.NavigateAsync($"app:///{Routes.Login()}");
        }
    }
}
