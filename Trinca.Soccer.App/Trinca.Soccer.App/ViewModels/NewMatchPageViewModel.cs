using System;
using Prism.Commands;
using Prism.Navigation;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class NewMatchPageViewModel : BaseViewModel
    {
        private string _place;
        public string Place
        {
            get => _place;
            set => SetProperty(ref _place, value);
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        private TimeSpan _hour;
        public TimeSpan Hour
        {
            get => _hour;
            set => SetProperty(ref _hour, value);
        }

        private int _minimumPlayers;
        public int MinimumPlayers
        {
            get => _minimumPlayers;
            set => SetProperty(ref _minimumPlayers, value);
        }

        private decimal _value;
        public decimal Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private bool _withBarbecue;
        public bool WithBarbecue
        {
            get => _withBarbecue;
            set => SetProperty(ref _withBarbecue, value);
        }

        private decimal _barbecueValue;
        public decimal BarbecueValue
        {
            get => _barbecueValue;
            set => SetProperty(ref _barbecueValue, value);
        }

        public DelegateCommand CreateCommand { get; set; }

        public NewMatchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Match";

            CreateCommand = new DelegateCommand(CreateCommandExecute);
        }

        private async void CreateCommandExecute()
        {
            ShowLoading = true;
            try
            {
                var match = new MatchModel
                {

                };

                await ClientApi.Matches.Create(match);

                await NavigationService.NavigateAsync(Routes.Matches());
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ShowLoading = false;
            }
        }
    }
}
