﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Prism.Services;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchesListPageViewModel : BaseViewModel
    {
        private ObservableCollection<MatchListOutputDto> _matches;
        public ObservableCollection<MatchListOutputDto> Matches
        {
            get => _matches;
            set => SetProperty(ref _matches, value);
        }

        public DelegateCommand RefreshCommand { get; set; }
        public DelegateCommand<MatchListOutputDto> SelectedItemCommand { get; set; }

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
            SelectedItemCommand = new DelegateCommand<MatchListOutputDto>(SelectedItemCommandExecute);
        }

        private async Task LoadMatches()
        {
            await TryCatchAsync(async () =>
            {
                IsRefreshing = true;
                Matches = new ObservableCollection<MatchListOutputDto>(await ClientApi.Matches.GetAll());
            });
            IsRefreshing = false;
        }

        private async void RefreshCommandExecute()
        {
            await LoadMatches();
        }

        private async void SelectedItemCommandExecute(MatchListOutputDto match)
        {
            if (match == null || match.Id == 0) return;

            await NavigationService.NavigateAsync(Routes.Match(match.Id), useModalNavigation: false);
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadMatches();
        }
    }
}
