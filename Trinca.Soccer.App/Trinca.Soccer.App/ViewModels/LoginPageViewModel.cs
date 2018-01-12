using Prism.Commands;
using System;
using System.Diagnostics;
using Prism.Navigation;
using Prism.Services;
using Refit;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;

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

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Trinca Soccer";

            _dialogService = dialogService;
            LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand);
        }

        private async void LoginCommandExecute()
        {
            try
            {
                ShowLoading = true;

                var loginModel = new LoginModel
                {
                    Username = Username,
                    Password = Password
                };

                var employee = await ClientApi.Employees.Login(loginModel);
                Settings.UserId = employee.Id;

                await NavigationService.NavigateAsync($"app:///{Routes.Matches()}");
            }
            catch (Exception ex)
            {
                var exception = ex as ApiException;
                if (exception != null)
                {
                    var refiEx = exception;
                    if (refiEx.ReasonPhrase.Equals("Unauthorized"))
                    {
                        await _dialogService.DisplayAlertAsync("Erro!", "Usuário ou senha inválidos.", "Ok");
                    }
                }
                Debug.WriteLine(ex.StackTrace);
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
