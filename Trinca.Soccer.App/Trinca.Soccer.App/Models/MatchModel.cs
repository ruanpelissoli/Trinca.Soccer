using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Dto.Player;

namespace Trinca.Soccer.App.Models
{
    public class MatchModel : BindableBase
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

        private string _totalMatchValueEach;
        public string TotalMatchValueEach
        {
            get => $"R$ {_totalMatchValueEach}";
            set => SetProperty(ref _totalMatchValueEach, value);
        }

        private string _totalBarbecueValueEach;
        public string TotalBarbecueValue
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
    }
}
