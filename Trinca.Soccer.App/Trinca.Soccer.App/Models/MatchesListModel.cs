using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.App.Models
{
    public class MatchesListModel : BindableBase
    {
        private ObservableCollection<MatchListOutputDto> _matches;
        public ObservableCollection<MatchListOutputDto> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<MatchListOutputDto> SelectedItemCommand { get; set; }       
    }
}
