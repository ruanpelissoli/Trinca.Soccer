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
using System;

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

        private ObservableCollection<PlayerOutputDto> _yellowTeam;
        public ObservableCollection<PlayerOutputDto> YellowTeam
        {
            get => _yellowTeam;
            set => SetProperty(ref _yellowTeam, value);
        }

        private ObservableCollection<PlayerOutputDto> _blackTeam;
        public ObservableCollection<PlayerOutputDto> BlackTeam
        {
            get => _blackTeam;
            set => SetProperty(ref _blackTeam, value);
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

        private int _teamAListViewHeight;
        public int TeamAListViewHeight
        {
            get => _teamAListViewHeight;
            set => SetProperty(ref _teamAListViewHeight, value);
        }

        private int _teamBListViewHeight;
        public int TeamBListViewHeight
        {
            get => _teamBListViewHeight;
            set => SetProperty(ref _teamBListViewHeight, value);
        }

        private string _totalMatchValueEach;
        public string TotalMatchValueEach
        {
            get => $"R$ {_totalMatchValueEach}";
            set => SetProperty(ref _totalMatchValueEach, value);
        }

        private string _totalBarbecueValueEach;
        public string TotalBarbecueValueEach
        {
            get => $"R$ {_totalBarbecueValueEach}";
            set => SetProperty(ref _totalBarbecueValueEach, value);
        }

        private string _totalValue;
        public string TotalValue
        {
            get => $"R$ {_totalValue}";
            set => SetProperty(ref _totalValue, value);
        }


        private bool _showBarbecueValue;
        public bool ShowBarbecueValue
        {
            get => _showBarbecueValue;
            set => SetProperty(ref _showBarbecueValue, value);
        }

        private string _totalPlayers;
        public string TotalPlayers
        {
            get => _totalPlayers;
            set => SetProperty(ref _totalPlayers, value);            
        }

        public DelegateCommand JoinMatchCommand { get; set; }
        public DelegateCommand InviteGuestCommand { get; set; }
        public DelegateCommand LeaveMatchCommand { get; set; }
        public DelegateCommand<int?> AddToYellowTeamCommand { get; set; }
        public DelegateCommand<int?> AddToBlackTeamCommand { get; set; }
        public DelegateCommand<int?> RemoveFromTeamCommand { get; set; }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            JoinMatchCommand = new DelegateCommand(JoinMatchCommandExecute);
            InviteGuestCommand = new DelegateCommand(InviteGuestCommandExecute);
            LeaveMatchCommand = new DelegateCommand(LeaveMatchCommandExecute);
            AddToYellowTeamCommand = new DelegateCommand<int?>(AddToYellowTeamCommandExecute);
            AddToBlackTeamCommand = new DelegateCommand<int?>(AddToBlackTeamCommandExecute);
            RemoveFromTeamCommand = new DelegateCommand<int?>(RemoveFromTeamCommandExecute);

            MessagingCenter.Subscribe<AddGuestPageViewModel, PlayerOutputDto>(this, Strings.UpdateMatchPage, OnUpdateMatchPage);
        }

        private async Task LoadMatch(int matchId)
        {
            await TryCatchAsync(async () =>
            {
                Match = await ClientApi.Matches.GetById(matchId);

                Title = "Match Details";

                InitializeLists();

                RefreshMatchValues(false);
            });
        }

        private void InitializeLists()
        {
            Players = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.NoTeam));
            YellowTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.YellowTeam));
            BlackTeam = new ObservableCollection<PlayerOutputDto>(Match.Players.Where(w => w.TeamId == ETeams.BlackTeam));

            JoinMatchIsVisibile = !Players.Select(s => s.EmployeeId).Contains(Settings.EmployeeId) &&
                                  !YellowTeam.Select(s => s.EmployeeId).Contains(Settings.EmployeeId) &&
                                  !BlackTeam.Select(s => s.EmployeeId).Contains(Settings.EmployeeId);

            Players.CollectionChanged += OnPlayerCollectionChanged;
            YellowTeam.CollectionChanged += OnTeamACollectionChanged;
            BlackTeam.CollectionChanged += OnTeamBCollectionChanged;

            PlayerListViewHeight = CalculateHeight(Players);
            TeamAListViewHeight = CalculateHeight(YellowTeam);
            TeamBListViewHeight = CalculateHeight(BlackTeam);

            TotalPlayers = $"{Match.Players.Count}/{Match.MinimumPlayers}";
        }

        private void OnUpdateMatchPage(AddGuestPageViewModel source, PlayerOutputDto player)
        {
            Players.Add(player);
            RefreshMatchValues();
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
                    Name = Settings.EmployeeName,
                    EmployeeId = Settings.EmployeeId,
                    MatchId = Match.Id,
                    WithBarbecue = withBarbecue
                };

                var playerOutput = await ClientApi.Players.Create(playerInput);

                Players.Add(playerOutput);
                JoinMatchIsVisibile = false;

                RefreshMatchValues();
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
                var player = await ClientApi.Players.GetByEmployeeId(Settings.EmployeeId);

                await ClientApi.Players.Delete(player.Id);

                if (Players.Any(w => w.Id == player.Id))
                    Players.Remove(Players.First(w => w.Id == player.Id));

                if (YellowTeam.Any(w => w.Id == player.Id))
                    YellowTeam.Remove(Players.First(w => w.Id == player.Id));

                if (BlackTeam.Any(w => w.Id == player.Id))
                    BlackTeam.Remove(Players.First(w => w.Id == player.Id));

                JoinMatchIsVisibile = true;
            });
        }

        private async void AddToYellowTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.YellowTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.First(w => w.Id == player.Id));
                YellowTeam.Add(player);
            });
        }

        private async void AddToBlackTeamCommandExecute(int? playerId)
        {
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.BlackTeam;
                await ClientApi.Players.Update(player);

                Players.Remove(Players.First(w => w.Id == player.Id));
                BlackTeam.Add(player);
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

                YellowTeam.Remove(YellowTeam.FirstOrDefault(w => w.Id == player.Id));
                BlackTeam.Remove(BlackTeam.FirstOrDefault(w => w.Id == player.Id));

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

        private void OnTeamACollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TeamAListViewHeight = CalculateHeight((ObservableCollection<PlayerOutputDto>)sender);
        }

        private void OnTeamBCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TeamBListViewHeight = CalculateHeight((ObservableCollection<PlayerOutputDto>)sender);
        }

        private int CalculateHeight(ICollection playersList)
        {
            return playersList.Count * LineHeight;
        }

        private async void RefreshMatchValues(bool refreshMatch = true)
        {
            await TryCatchAsync(async () =>
            {
                if (refreshMatch)
                    Match = await ClientApi.Matches.GetById(Match.Id);

                var playersCount = Match.Players.Count == 0 ? 1 : Match.Players.Count;
                var playersWithBarbecueCount = Match.Players.Count(w => w.WithBarbecue) == 0 ? 1 : Match.Players.Count(w => w.WithBarbecue);

                var totalValueEach = Math.Round(Match.Value / playersCount, 2);
                TotalMatchValueEach = totalValueEach.ToString("N");

                var totalBarbecueValueEach = Math.Round(Match.BarbecueValue / playersWithBarbecueCount, 2);
                TotalBarbecueValueEach = totalBarbecueValueEach.ToString("N");

                var loggedPlayer = Players.FirstOrDefault(w => w.EmployeeId == Settings.EmployeeId);

                if (loggedPlayer == null)
                    TotalValue = (totalValueEach + (Match.WithBarbecue ? totalBarbecueValueEach : 0M)).ToString("N");
                else
                {
                    TotalValue = (totalValueEach + (Match.WithBarbecue && loggedPlayer.WithBarbecue ? totalBarbecueValueEach : 0M)).ToString("N");
                    ShowBarbecueValue = Match.WithBarbecue && loggedPlayer.WithBarbecue;
                }                
            });
        }
    }
}
