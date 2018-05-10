using System;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Prism.Services;
using Trinca.Soccer.Dto.Match;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class NewMatchPageViewModel : BaseViewModel
    {
        public NewMatchModel Model { get; set; }

        public NewMatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.NewMatchTitle;

            Model = new NewMatchModel
            {
                CreateCommand = new DelegateCommand(CreateCommandExecute, CreateCanExecuteCommand)
            };
        }

        private async void CreateCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var match = new MatchInputDto
                {
                    CreatedBy = Settings.EmployeeId,
                    Date = new DateTime(Model.Date.Year, Model.Date.Month, Model.Date.Day, Model.Hour.Hours, Model.Hour.Minutes, Model.Hour.Seconds),
                    Place = Model.Place,
                    MinimumPlayers = Model.MinimumPlayers,
                    Value = Model.Value,
                    WithBarbecue = Model.WithBarbecue,
                    BarbecueValue = Model.BarbecueValue,
                    CreateDate = DateTime.Now
                };

                await ClientApi.Matches.Create(match);

                await NavigateTo(Routes.Matches());
            });
        }

        private bool CreateCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Model.Place) &&
                   Model.MinimumPlayers > 0 &&
                   Model.Value > 0 && 
                   (!Model.WithBarbecue || Model.BarbecueValue > 0);
        }
    }
}
