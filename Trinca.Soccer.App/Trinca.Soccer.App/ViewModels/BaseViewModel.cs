using Prism.Mvvm;

namespace Trinca.Soccer.App.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private bool _showLoading;
        public bool ShowLoading
        {
            get => _showLoading;
            set => SetProperty(ref _showLoading, value);
        }

        public BaseViewModel()
        {
            Title = string.Empty;
        }
    }
}
