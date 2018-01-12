using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Refit;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.App.Models;

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

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public MatchesListPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Matches";

            RefreshCommand = new DelegateCommand(RefreshCommandExecute);
        }

        private async Task LoadMatches()
        {
            try
            {
                Matches = new ObservableCollection<MatchModel>(await ClientApi.Matches.GetAll());
            }
            catch (Exception ex)
            {
                var exception = ex as ApiException;
                if (exception != null)
                {
                    var refitEx = exception;
                    if (refitEx.ReasonPhrase.Equals("Unauthorized"))
                    {
                        Settings.Clear();
                        await NavigationService.NavigateAsync($"app:///{Routes.Login()}");
                    }
                }
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private async void RefreshCommandExecute()
        {
            IsRefreshing = true;
            await LoadMatches();
            IsRefreshing = false;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            await LoadMatches();
        }
    }
}
