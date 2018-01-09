using Prism.Commands;
using System;

namespace Trinca.Soccer.App.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
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
            return !string.IsNullOrEmpty(Email) &&
                   !string.IsNullOrEmpty(Password);
        }
    }
}
