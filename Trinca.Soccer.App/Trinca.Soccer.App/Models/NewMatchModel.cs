using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Trinca.Soccer.App.Models
{
    public class NewMatchModel : BindableBase
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
    }
}
