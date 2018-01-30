using System.Collections;
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
using System.Collections.Specialized;

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

        private const int LineHeight = 50;

        private int _playerListViewHeight;
        public int PlayerListViewHeight
        {
            get => _playerListViewHeight;
            set => SetProperty(ref _playerListViewHeight, value);
        }

        private int _teamsListViewHeight;
        public int TeamsListViewHeight
        {
            get => _teamsListViewHeight;
            set => SetProperty(ref _teamsListViewHeight, value);
        }

        public DelegateCommand JoinMatchCommand { get; set; }
        public DelegateCommand InviteGuestCommand { get; set; }
        public DelegateCommand LeaveMatchCommand { get; set; }
        public DelegateCommand<int?> AddToBlueTeamCommand { get; set; }
        public DelegateCommand<int?> AddToRedTeamCommand { get; set; }
        public DelegateCommand<int?> RemoveFromTeamCommand { get; set; }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            JoinMatchCommand = new DelegateCommand(JoinMatchCommandExecute);
            InviteGuestCommand = new DelegateCommand(InviteGuestCommandExecute);
            LeaveMatchCommand = new DelegateCommand(LeaveMatchCommandExecute);
            AddToBlueTeamCommand = new DelegateCommand<int?>(AddToBlueTeamCommandExecute);
            AddToRedTeamCommand = new DelegateCommand<int?>(AddToRedTeamCommandExecute);
            RemoveFromTeamCommand = new DelegateCommand<int?>(RemoveFromTeamCommandExecute);

            MessagingCenter.Subscribe<AddGuestPageViewModel, PlayerOutputDto>(this, Strings.UpdateMatchPage, OnUpdateMatchPage);
        }

        private async Task LoadMatch(int matchId)
        {
            await TryCatchAsync(async () =>
            {
                Match = await ClientApi.Matches.GetById(matchId);

                Title = $"{Match.Place} - {Match.Date:dd/MM/yyyy hh:mm:ss}";

                Players = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.NoTeam));
                BlueTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.BlueTeam));
                RedTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.RedTeam));

                JoinMatchIsVisibile = !Players.Select(s => s.EmployeeId).Contains(Settings.UserId) &&
                                      !BlueTeam.Select(s => s.EmployeeId).Contains(Settings.UserId) &&
                                      !RedTeam.Select(s => s.EmployeeId).Contains(Settings.UserId);

                Players.CollectionChanged += OnPlayerCollectionChanged;
                BlueTeam.CollectionChanged += OnTeamsCollectionChanged;
                RedTeam.CollectionChanged += OnTeamsCollectionChanged;

                PlayerListViewHeight = CalculateHeight(Players);
                TeamsListViewHeight = CalculateHeight(BlueTeam.Count >= RedTeam.Count ? BlueTeam : RedTeam);
            });
        }

        private void OnUpdateMatchPage(AddGuestPageViewModel source, PlayerOutputDto player)
        {
            Players.Add(player);
        }

        private async void JoinMatchCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var withBarbecue = false;
                if (Match.WithBarbecue)
                    withBarbecue = await DialogService.DisplayAlertAsync("Warning!", "With barbecue?", "Yes", "No");

                var playerInput = new PlayerInputDto
                {
                    Name = Match.Employee.Name,
                    EmployeeId = Settings.UserId,
                    MatchId = Match.Id,
                    WithBarbecue = withBarbecue
                };

                var playerOutput = await ClientApi.Players.Create(playerInput);

                Players.Add(playerOutput);
                JoinMatchIsVisibile = false;
            });
        }

        private async void InviteGuestCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var parameters = new NavigationParameters
                {
                    { Parameters.Match, Match }
                };

                await NavigationService.NavigateAsync(Routes.AddGuest(), parameters);
            });
        }

        private async void LeaveMatchCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                await ClientApi.Players.Delete(Settings.UserId);

                Players.Remove(Players.First(w => w.Id == Settings.UserId));
                JoinMatchIsVisibile = true;
            });
        }

        private async void AddToBlueTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.BlueTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.First(w => w.Id == player.Id));
                BlueTeam.Add(player);
            });
        }

        private async void AddToRedTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.RedTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.First(w => w.Id == player.Id));
                RedTeam.Add(player);
            });
        }

        private async void RemoveFromTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.NoTeam;
                await ClientApi.Players.Update(player);

                BlueTeam.Remove(BlueTeam.FirstOrDefault(w => w.Id == player.Id));
                RedTeam.Remove(RedTeam.FirstOrDefault(w => w.Id == player.Id));

                Players.Add(player);
            });
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var matchId = parameters.GetValue<int>(Parameters.MatchId);

            if (matchId > 0)
                await LoadMatch(matchId);
        }

        private void OnPlayerCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PlayerListViewHeight = CalculateHeight((ObservableCollection<PlayerOutputDto>)sender);
        }

        private void OnTeamsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TeamsListViewHeight = CalculateHeight((ObservableCollection<PlayerOutputDto>)sender);
        }

        private int CalculateHeight(ICollection playersList)
        {
            return playersList.Count * LineHeight;
        }
    }
}
