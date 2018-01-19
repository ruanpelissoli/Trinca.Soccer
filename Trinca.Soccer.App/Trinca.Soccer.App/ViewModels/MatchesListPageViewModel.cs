using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Models;
using Prism.Services;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchesListPageViewModel : BaseViewModel
    {
        private ObservableCollection<MatchModel> _matches;
        public ObservableCollection<MatchModel> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<MatchModel> SelectedItemCommand { get; set; }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public MatchesListPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "Matches";

            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
            SelectedItemCommand = new DelegateCommand<MatchModel>(SelectedItemCommandExecute);
        }

        private async Task LoadMatches()
        {
            await TryCatchAsync(async () =>
            {
                IsRefreshing = true;
                Matches = new ObservableCollection<MatchModel>(await ClientApi.Matches.GetAll());
            });
            IsRefreshing = false;
        }

        private async void RefreshCommandExecute()
        {
            await LoadMatches();
        }

        private async void SelectedItemCommandExecute(MatchModel match)
        {
            if (match == null || match.Id == 0) return;

            await NavigationService.NavigateAsync(Routes.Match(match.Id));
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadMatches();
        }
    }
}
