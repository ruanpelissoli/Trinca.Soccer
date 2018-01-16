﻿using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Refit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Trinca.Soccer.App.ViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected INavigationService NavigationService;
        protected IPageDialogService DialogService;

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

        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService;

            Title = string.Empty;
        }

        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
            
        }

        protected async Task TryCatchAsync(Func<Task> _try)
        {
            ShowLoading = true;
            try
            {
                await _try();
            }
            catch (Exception ex)
            {
                await Catch(ex);
            }
            finally
            {
                ShowLoading = false;
            }
        }

        private async Task Catch(Exception exception)
        {
            var refiEx = exception as ApiException;
            if (refiEx != null)
            {
                if (refiEx.ReasonPhrase.Equals("Unauthorized"))
                {
                    await DialogService.DisplayAlertAsync("Erro!", "Usuário ou senha inválidos.", "Ok");
                }
            }
            Debug.WriteLine(exception.StackTrace);
        }
    }
}