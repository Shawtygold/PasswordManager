using PasswordBox.Core;
using PasswordBox.MVVM.Model;
using PasswordBox.Services;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    internal class LoginViewModel : Core.ViewModel
    {
        public LoginViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            ContinueCommand = new RelayCommand(Continue);
            NavigateToRegistrationCommand = new RelayCommand(NavigateToRegistration);
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged(); }
        }

        private string _twoFactorCode;
        public string TwoFactorCode
        {
            get { return _twoFactorCode; }
            set { _twoFactorCode = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand ContinueCommand { get; set; }
        public ICommand NavigateToRegistrationCommand { get; set; }

        private void Continue(object obj)
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Enter login.");
                return;
            }

            if (string.IsNullOrEmpty(TwoFactorCode))
            {
                MessageBox.Show("Enter two factor code.");
                return;
            }

            GoogleAuth googleAuth = new();
            if(!googleAuth.Validate(Login, TwoFactorCode))
            {
                MessageBox.Show("Invalid Login or Two factor code.");
                return;
            }

            Navigation.NavigateTo<PasswordsViewModel>();
            NotificationService.SendWelcomeNotification(Login);
        }
        private void NavigateToRegistration(object obj)
        {
            Navigation.NavigateTo<RegistrationViewModel>();
        }

        #endregion
    }
}
