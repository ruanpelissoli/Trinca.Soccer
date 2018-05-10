using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Trinca.Soccer.App.API;
using Trinca.Soccer.App.Constants;
using Trinca.Soccer.App.Helpers;
using Trinca.Soccer.App.Models;

namespace Trinca.Soccer.App.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsModel Model { get; set; }

        public SettingsPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            Title = Strings.SettingsTitle;

            Model = new SettingsModel
            {
                ChangePasswordCommand = new DelegateCommand(ChangePasswordCommandExecute, ChangePasswordCanCommandExecute),
                LogoutCommand = new DelegateCommand(LogoutCommandExecute)
            };
        }
        private async void ChangePasswordCommandExecute()
        {
            await TryCatchAsync(async () =>
            {
                if(!Model.NewPassword.Equals(Model.NewPasswordConfirmation))
                {
                    await DialogService.DisplayAlertAsync("Warning!", "New password and password confirmation dont match.", "Ok");
                    return;
                }

                var checkPassword = await ClientApi.Employees.CheckPassword(Settings.EmployeeId, Model.OldPassword);

                if(!checkPassword)
                {
                    await DialogService.DisplayAlertAsync("Warning!", "Old password dont match with your current password.", "Ok");
                    return;
                }

                await ClientApi.Employees.ChangePassword(Settings.EmployeeId, Model.NewPassword);

                await DialogService.DisplayAlertAsync("Success!", "Password changed successfully.", "Ok");
            });
        }

        private bool ChangePasswordCanCommandExecute()
        {
            return !string.IsNullOrEmpty(Model.OldPassword) &&
                   !string.IsNullOrEmpty(Model.NewPassword) &&
                   !string.IsNullOrEmpty(Model.NewPasswordConfirmation);
        }

        private async void LogoutCommandExecute()
        {
            Settings.Clear();
            await NavigateTo($"{Routes.Login(true)}");
        }
    }
}
