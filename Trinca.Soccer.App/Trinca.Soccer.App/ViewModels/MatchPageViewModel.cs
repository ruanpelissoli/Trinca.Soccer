using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class MatchPageViewModel : BaseViewModel
    {
        private MatchModel _match;
        public MatchModel Match
        {
            get => _match;
            set => SetProperty(ref _match, value);
        }

        public MatchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "texte";
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var matchId = parameters.GetValue<int>(Parameters.MatchId);
            Match = await ClientApi.Matches.GetById(matchId);
        }
    }
}
