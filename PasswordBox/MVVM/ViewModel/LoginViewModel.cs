using PasswordBox.Core;
using PasswordBox.MVVM.Model;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.Validators;
using PasswordBox.Services;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    internal class LoginViewModel : Core.ViewModel
    {
        private UserLoginValidator _userValidator;

        public LoginViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            _userValidator = new UserLoginValidator();

            LoginCommand = new RelayCommand(LogIn);
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

        public ICommand LoginCommand { get; set; }
        public ICommand NavigateToRegistrationCommand { get; set; }

        private async void LogIn(object obj)
        {
            // Валидация пользователя (проверка на правильность ввода полей
            // и на существование пользователя)
            User user = new(Login);
            var result = await _userValidator.ValidateAsync(user);
            if (!result.IsValid)
            {
                ErrorDisplay.ShowValidationErrorMessage(result.Errors);
                return;
            }

            if (string.IsNullOrEmpty(TwoFactorCode))
            {
                MessageBox.Show("Enter two factor code.");
                return;
            }

            // Валидация Two Factor Code
            GoogleAuth googleAuth = new();
            if(!googleAuth.Validate(Login, TwoFactorCode))
            {
                MessageBox.Show("Invalid two factor code.");
                return;
            }

            // Перенаправление на страницу с паролями и отправка уведомления
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
