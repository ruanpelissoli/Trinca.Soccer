using System;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Prism.Services;
using Trinca.Soccer.Dto.Match;

namespace Trinca.Soccer.App.ViewModels
{
    public class NewMatchPageViewModel : BaseViewModel
    {
        private string _place;
        public string Place
        {
            get => _place;
            set
            {
                SetProperty(ref _place, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                SetProperty(ref _date, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private TimeSpan _hour;
        public TimeSpan Hour
        {
            get => _hour;
            set
            {
                SetProperty(ref _hour, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private int _minimumPlayers;
        public int MinimumPlayers
        {
            get => _minimumPlayers;
            set
            {
                SetProperty(ref _minimumPlayers, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private decimal _value;
        public decimal Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _withBarbecue;
        public bool WithBarbecue
        {
            get => _withBarbecue;
            set
            {
                SetProperty(ref _withBarbecue, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private decimal _barbecueValue;
        public decimal BarbecueValue
        {
            get => _barbecueValue;
            set
            {
                SetProperty(ref _barbecueValue, value);
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand CreateCommand { get; set; }

        public NewMatchPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = "New Match";

            CreateCommand = new DelegateCommand(CreateCommandExecute, CreateCanExecuteCommand);
        }

        private async void CreateCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var match = new MatchInputDto
                {
                    CreatedBy = Settings.EmployeeId,
                    Date = Date.Add(Hour),
                    Place = Place,
                    MinimumPlayers = MinimumPlayers,
                    Value = Value,
                    WithBarbecue = WithBarbecue,
                    BarbecueValue = BarbecueValue,
                    CreateDate = DateTime.Now
                };

                await ClientApi.Matches.Create(match);

                await NavigationService.NavigateAsync(Routes.Matches());
            });
        }

        private bool CreateCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Place) &&
                   MinimumPlayers > 0 &&
                   Value > 0 && 
                   (!WithBarbecue || BarbecueValue > 0);
        }
    }
}
