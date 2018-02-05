using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Dto.Player;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
	public class AddGuestPageViewModel : BaseViewModel
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

        public AddGuestPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AddGuestTitle;

            AddGuestCommand = new DelegateCommand(AddGuestCommandExecute, AddGuestCanExecuteCommand);
        }

        private async Task AddGuest()
        {
            await TryCatchAsync(async () =>
            {
                var playerInput = new PlayerInputDto
                {
                    Name = Name,
                    EmployeeId = Settings.EmployeeId,
                    IsGuest = true,
                    WithBarbecue = WithBarbecue,
                    MatchId = Match.Id
                };

                var playerOutput = await ClientApi.Players.Create(playerInput);

                MessagingCenter.Send(this, Strings.UpdateMatchPage, playerOutput);

                await NavigationService.GoBackAsync();
            });
        }

        private async void AddGuestCommandExecute()
        {
            await AddGuest();
        }

        private bool AddGuestCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Name);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Match = parameters.GetValue<MatchOutputDto>(Parameters.Match);            
        }
    }
}
