using Prism.Commands;
using Prism.Mvvm;

namespace Trinca.Soccer.App.Models
{
    public class SettingsModel : BindableBase
    {
        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                SetProperty(ref _oldPassword, value);
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                SetProperty(ref _newPassword, value);
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        private string _newPasswordConfirmation;
        public string NewPasswordConfirmation
        {
            get => _newPasswordConfirmation;
            set
            {
                SetProperty(ref _newPasswordConfirmation, value);
                ChangePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand ChangePasswordCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
    }
}
