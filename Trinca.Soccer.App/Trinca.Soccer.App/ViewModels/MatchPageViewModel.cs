using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Prism.Services;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Dto.Player;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchPageViewModel : BaseViewModel
    {
        private MatchOutputDto _match;
        public MatchOutputDto Match
        {
            get => _match;
            set => SetProperty(ref _match, value);
        }

        private ObservableCollection<PlayerOutputDto> _players;
        public ObservableCollection<PlayerOutputDto> Players
        {
            get => _players;
            set => SetProperty(ref _players, value);
        }

        private bool _joinMatchIsVisibile;
        public bool JoinMatchIsVisibile
        {
            get => _joinMatchIsVisibile;
            set => SetProperty(ref _joinMatchIsVisibile, value);
        }

        public DelegateCommand JoinMatchCommand { get; set; }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            JoinMatchCommand = new DelegateCommand(JoinMatchCommandExecute);
        }

        private async Task LoadMatch(int matchId)
        {
            await TryCatchAsync(async () =>
            {
                Match = await ClientApi.Matches.GetById(matchId);

                var title = Match.Place;

                MessagingCenter.Send(this, Strings.TitleChange, title);
                Title = title;

                Players = new ObservableCollection<PlayerOutputDto>(Match.Players);

                JoinMatchIsVisibile = !Players.Select(s => s.EmployeeId).Contains(Settings.UserId);
            });
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var matchId = parameters.GetValue<int>(Parameters.MatchId);
            await LoadMatch(matchId);
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Send(this, Strings.TitleChange, "Trinca Soccer");
        }

        private async void JoinMatchCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var playerInput = new PlayerInputDto
                {
                    Name = Match.Employee.Name,
                    EmployeeId = Settings.UserId,
                    MatchId = Match.Id
                };

                var playerOutput = await ClientApi.Players.Create(playerInput);

                Players.Add(playerOutput);
                JoinMatchIsVisibile = false;
            });
        }
    }
}
