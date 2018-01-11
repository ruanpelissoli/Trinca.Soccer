using Prism.Commands;
using System;
using Trinca.Soccer.App.Models;

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

        public LoginPageViewModel()
        {
            Title = "Trinca Soccer";

            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand);
        }

        private async void LoginCommandExecute()
        {
            try
            {
                var loginModel = new LoginModel
                {
                    Username = Username,
                    Password = Password
                };

                await ApiClient.ApiClient.Employees.Login(loginModel);
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                ShowLoading = false;
            }
        }

        private bool LoginCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Username) &&
                   !string.IsNullOrEmpty(Password);
        }
    }
}
