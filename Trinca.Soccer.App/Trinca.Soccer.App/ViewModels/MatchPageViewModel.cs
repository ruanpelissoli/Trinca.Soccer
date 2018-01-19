using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Models;
using Prism.Services;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchPageViewModel : BaseViewModel
    {
        private MatchModel _match;
        public MatchModel Match
        {
            get => _match;
            set => SetProperty(ref _match, value);
        }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var matchId = parameters.GetValue<int>(Parameters.MatchId);
            Match = await ClientApi.Matches.GetById(matchId);

            var title = $"{Match.Place} - {Match.Date:dd/MM/yyyy hh:mm:ss}";

            MessagingCenter.Send(this, Strings.TitleChange, title);
            Title = title;
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Send(this, Strings.TitleChange, "Trinca Soccer");
        }
    }
}
