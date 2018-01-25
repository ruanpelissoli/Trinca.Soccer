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
using Trinca.Soccer.Dto.Enums;

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

        private ObservableCollection<PlayerOutputDto> _blueTeam;
        public ObservableCollection<PlayerOutputDto> BlueTeam
        {
            get => _blueTeam;
            set => SetProperty(ref _blueTeam, value);
        }

        private ObservableCollection<PlayerOutputDto> _redTeam;
        public ObservableCollection<PlayerOutputDto> RedTeam
        {
            get => _redTeam;
            set => SetProperty(ref _redTeam, value);
        }

        private bool _joinMatchIsVisibile;
        public bool JoinMatchIsVisibile
        {
            get => _joinMatchIsVisibile;
            set => SetProperty(ref _joinMatchIsVisibile, value);
        }

        public DelegateCommand JoinMatchCommand { get; set; }
        public DelegateCommand<int?> AddToBlueTeamCommand { get; set; }
        public DelegateCommand<int?> AddToRedTeamCommand { get; set; }
        public DelegateCommand<int?> RemoveFromTeamCommand { get; set; }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            JoinMatchCommand = new DelegateCommand(JoinMatchCommandExecute);
            AddToBlueTeamCommand = new DelegateCommand<int?>(AddToBlueTeamCommandExecute);
            AddToRedTeamCommand = new DelegateCommand<int?>(AddToRedTeamCommandExecute);
            RemoveFromTeamCommand = new DelegateCommand<int?>(RemoveFromTeamCommandExecute);
        }

        private async Task LoadMatch(int matchId)
        {
            await TryCatchAsync(async () =>
            {
                Match = await ClientApi.Matches.GetById(matchId);

                var title = Match.Place;

                MessagingCenter.Send(this, Strings.TitleChange, title);
                Title = title;

                Players = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.NoTeam));
                BlueTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.BlueTeam));
                RedTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.RedTeam));

                JoinMatchIsVisibile = !Players.Select(s => s.EmployeeId).Contains(Settings.UserId) &&
                                      !BlueTeam.Select(s => s.EmployeeId).Contains(Settings.UserId) &&
                                      !RedTeam.Select(s => s.EmployeeId).Contains(Settings.UserId);
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

        private async void AddToBlueTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.BlueTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.Where(w => w.Id == player.Id).First());
                BlueTeam.Add(player);
            });
        }

        private async void AddToRedTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.RedTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.Where(w => w.Id == player.Id).First());
                RedTeam.Add(player);
            });
        }

        private async void RemoveFromTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.NoTeam;
                await ClientApi.Players.Update(player);

                BlueTeam.Remove(BlueTeam.Where(w => w.Id == player.Id).FirstOrDefault());
                RedTeam.Remove(RedTeam.Where(w => w.Id == player.Id).FirstOrDefault());

                Players.Add(player);
            });
        }
    }
}
