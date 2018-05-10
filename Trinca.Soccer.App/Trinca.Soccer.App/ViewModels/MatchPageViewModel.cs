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
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchPageViewModel : BaseViewModel
    {
        public MatchModel Model { get; set; }

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

        private bool _showPlayersList;
        public bool ShowPlayersList
        {
            get => _showPlayersList;
            set => SetProperty(ref _showPlayersList, value);
        }

        private bool _showBarbecueValue;
        public bool ShowBarbecueValue
        {
            get => _showBarbecueValue;
            set => SetProperty(ref _showBarbecueValue, value);
        }

        public MatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Model = new MatchModel {
                JoinMatchCommand = new DelegateCommand(JoinMatchCommandExecute),
                InviteGuestCommand = new DelegateCommand(InviteGuestCommandExecute),
                LeaveMatchCommand = new DelegateCommand(LeaveMatchCommandExecute),
                AddToYellowTeamCommand = new DelegateCommand<int?>(AddToYellowTeamCommandExecute),
                AddToBlackTeamCommand = new DelegateCommand<int?>(AddToBlackTeamCommandExecute),
                RemoveFromTeamCommand = new DelegateCommand<int?>(RemoveFromTeamCommandExecute)
            };

            MessagingCenter.Subscribe<AddGuestPageViewModel, PlayerOutputDto>(this, Strings.UpdateMatchPage, OnUpdateMatchPage);
        }

        private async Task LoadMatch(int matchId)
        {
            await TryCatchAsync(async () =>
            {
                Model.Match = await ClientApi.Matches.GetById(matchId);

                Title = Strings.MatchTitle;

                InitializeLists();

                RefreshMatchValues(false);
            });
        }

        private void InitializeLists()
        {
            Model.Players = new ObservableCollection<PlayerOutputDto>(Model.Match.Players.Where(w => w.TeamId == ETeams.NoTeam));
            Model.YellowTeam = new ObservableCollection<PlayerOutputDto>(Model.Match.Players.Where(w => w.TeamId == ETeams.YellowTeam));
            Model.BlackTeam = new ObservableCollection<PlayerOutputDto>(Model.Match.Players.Where(w => w.TeamId == ETeams.BlackTeam));

            JoinMatchIsVisibile = !Model.Players.Select(s => s.EmployeeId).Contains(Settings.EmployeeId) &&
                                  !Model.YellowTeam.Select(s => s.EmployeeId).Contains(Settings.EmployeeId) &&
                                  !Model.BlackTeam.Select(s => s.EmployeeId).Contains(Settings.EmployeeId);

            Model.Players.CollectionChanged += OnPlayerCollectionChanged;
            Model.YellowTeam.CollectionChanged += OnTeamACollectionChanged;
            Model.BlackTeam.CollectionChanged += OnTeamBCollectionChanged;

            PlayerListViewHeight = CalculateHeight(Model.Players);
            TeamAListViewHeight = CalculateHeight(Model.YellowTeam);
            TeamBListViewHeight = CalculateHeight(Model.BlackTeam);

            ShowPlayersList = Model.Players.Any();

            Model.TotalPlayers = $"{Model.Match.Players.Count}/{Model.Match.MinimumPlayers}";
        }

        private void OnUpdateMatchPage(AddGuestPageViewModel source, PlayerOutputDto player)
        {
            Model.Players.Add(player);
            RefreshMatchValues();
        }

        private async void JoinMatchCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var withBarbecue = false;
                if (Model.Match.WithBarbecue)
                    withBarbecue = await DialogService.DisplayAlertAsync("Warning!", "With barbecue?", "Yes", "No");

                var playerInput = new PlayerInputDto
                {
                    Name = Settings.EmployeeName,
                    EmployeeId = Settings.EmployeeId,
                    MatchId = Model.Match.Id,
                    WithBarbecue = withBarbecue
                };

                var playerOutput = await ClientApi.Players.Create(playerInput);

                Model.Players.Add(playerOutput);
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
                    { Parameters.Match, Model.Match }
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

                if (Model.Players.Any(w => w.Id == player.Id))
                    Model.Players.Remove(Model.Players.First(w => w.Id == player.Id));

                if (Model.YellowTeam.Any(w => w.Id == player.Id))
                    Model.YellowTeam.Remove(Model.Players.First(w => w.Id == player.Id));

                if (Model.BlackTeam.Any(w => w.Id == player.Id))
                    Model.BlackTeam.Remove(Model.Players.First(w => w.Id == player.Id));

                JoinMatchIsVisibile = true;
            });
        }

        private async void AddToYellowTeamCommandExecute(int? playerId)
        {
            ShowLoading = true;
            await TryCatchAsync(async () =>
            {
                if (!playerId.HasValue) return;
                var player = await ClientApi.Players.GetById(playerId.Value);

                player.TeamId = ETeams.YellowTeam;
                await ClientApi.Players.Update(player);

                Model.Players.Remove(Model.Players.First(w => w.Id == player.Id));
                Model.YellowTeam.Add(player);

                ShowLoading = false;
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

                Model.Players.Remove(Model.Players.First(w => w.Id == player.Id));
                Model.BlackTeam.Add(player);
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

                Model.YellowTeam.Remove(Model.YellowTeam.FirstOrDefault(w => w.Id == player.Id));
                Model.BlackTeam.Remove(Model.BlackTeam.FirstOrDefault(w => w.Id == player.Id));

                Model.Players.Add(player);
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
            ShowPlayersList = Model.Players.Any();
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
                    Model.Match = await ClientApi.Matches.GetById(Model.Match.Id);

                var playersCount = Model.Match.Players.Count == 0 ? 1 : Model.Match.Players.Count;
                var playersWithBarbecueCount = Model.Match.Players.Count(w => w.WithBarbecue) == 0 ? 1 : Model.Match.Players.Count(w => w.WithBarbecue);

                var totalValueEach = Math.Round(Model.Match.Value / playersCount, 2);
                Model.TotalMatchValueEach = totalValueEach.ToString("N");

                var totalBarbecueValueEach = Math.Round(Model.Match.BarbecueValue / playersWithBarbecueCount, 2);
                Model.TotalBarbecueValueEach = totalBarbecueValueEach.ToString("N");

                var loggedPlayer = Model.Players.FirstOrDefault(w => w.EmployeeId == Settings.EmployeeId);

                if (loggedPlayer == null)
                    Model.TotalValue = (Model.Match.Value + (Model.Match.WithBarbecue ? totalBarbecueValueEach : 0M)).ToString("N");
                else
                {
                    Model.TotalValue = (Model.Match.Value + (Model.Match.WithBarbecue && loggedPlayer.WithBarbecue ? totalBarbecueValueEach : 0M)).ToString("N");
                    ShowBarbecueValue = Model.Match.WithBarbecue && loggedPlayer.WithBarbecue;
                }

                ShowPlayersList = Model.Players.Any();
            });
        }
    }
}
