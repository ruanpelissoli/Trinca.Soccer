using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.App.Models;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.Dto.Player;
using Xamarin.Forms;

namespace Trinca.Soccer.App.ViewModels
{
    public class AddGuestPageViewModel : BaseViewModel
	{
	    public AddGuestModel Model { get; set; }

        public AddGuestPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AddGuestTitle;

            Model = new AddGuestModel
            {
                AddGuestCommand = new DelegateCommand(AddGuestCommandExecute, AddGuestCanExecuteCommand)
            };
        }

        private async Task AddGuest()
        {
            await TryCatchAsync(async () =>
            {
                var playerInput = new PlayerInputDto
                {
                    Name = Model.Name,
                    EmployeeId = Settings.EmployeeId,
                    IsGuest = true,
                    WithBarbecue = Model.WithBarbecue,
                    MatchId = Model.Match.Id
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
            return !string.IsNullOrEmpty(Model.Name);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            Model.Match = parameters.GetValue<MatchOutputDto>(Parameters.Match);            
        }
    }
}
