using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.Constants;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;
            
            MessagingCenter.Subscribe<MatchPageViewModel, string>(this, Strings.TitleChange, OnTitleChanged);
        }

        private void OnTitleChanged(MatchPageViewModel source, string title)
        {
            Title = title;
        }
    }
}
