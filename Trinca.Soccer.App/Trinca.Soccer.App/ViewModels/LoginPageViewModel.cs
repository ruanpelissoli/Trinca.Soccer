using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.App.Models;
using Trinca.Soccer.Dto.Login;

namespace Trinca.Soccer.App.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        
        public LoginModel Model { get; set; }       

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.AppName;

            Model = new LoginModel
            {
                LoginCommand = new DelegateCommand(LoginCommandExecute, LoginCanExecuteCommand)
            };
        }

        private async void LoginCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                var loginDto = new LoginInputDto
                {
                    Username = Model.Username,
                    Password = Model.Password
                };

                var loginOutput = await ClientApi.Employees.Login(loginDto);

                Settings.EmployeeId = loginOutput.Id;
                Settings.EmployeeName = loginOutput.Name;

                await NavigateTo($"{Routes.Matches(true)}");
            });
        }

        private bool LoginCanExecuteCommand()
        {
            return !string.IsNullOrEmpty(Model.Username) &&
                   !string.IsNullOrEmpty(Model.Password);
        }
    }
}
