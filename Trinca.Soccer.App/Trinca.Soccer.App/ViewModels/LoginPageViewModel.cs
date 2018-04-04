using System;
using System.Reflection;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.Dto.Login;

namespace Trinca.Soccer.App.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                SetProperty(ref _username, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand LoginCommand { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand);
        }

        private async void LoginCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var loginDto = new LoginInputDto
                {
                    Username = Username,
                    Password = Password
                };

                var loginOutput = await ClientApi.Employees.Login(loginDto);

                Settings.EmployeeId = loginOutput.Id;
                Settings.EmployeeName = loginOutput.Name;

                await NavigationService.NavigateAsync($"app:///{Routes.Matches()}");
            });
        }

        private bool LoginCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Password);
        }
    }
}
