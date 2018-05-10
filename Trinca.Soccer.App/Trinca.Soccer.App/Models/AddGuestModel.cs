using Prism.Commands;
using Prism.Mvvm;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.App.Models
{
    public class AddGuestModel : BindableBase
    {
        private MatchOutputDto _match;
        public MatchOutputDto Match
        {
            get => _match;
            set => SetProperty(ref _match, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
                AddGuestCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _withBarbecue;
        public bool WithBarbecue
        {
            get => _withBarbecue;
            set => SetProperty(ref _withBarbecue, value);
        }

        public DelegateCommand AddGuestCommand { get; set; }
    }
}
