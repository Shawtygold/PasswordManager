using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Toolkit.Uwp.Notifications;
using PasswordBox.Core;
using PasswordBox.Interfaces.GoogleAuth;
using PasswordBox.Interfaces.Notifications;
using PasswordBox.Interfaces.Repositories;
using PasswordBox.MVVM.Model;
using PasswordBox.MVVM.Model.AppContext;
using PasswordBox.MVVM.Model.Entities;
using PasswordBox.MVVM.Model.GoogleAuthentificator;
using PasswordBox.MVVM.Model.Repositories;
using PasswordBox.MVVM.Model.Validators;
using PasswordBox.Services;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    internal class LoginViewModel : Core.ViewModel
    {
        private readonly UserSignInValidator _userSignInValidator;
        private readonly IUserRepository _userRepository;
        private readonly IGoogleAuthenticatorService _googleAuthenticatorService; 
        private readonly INotificationService _notificationService;

        public LoginViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            _userRepository = new UserRepository(new ApplicationContext());
            _userSignInValidator = new UserSignInValidator();
            _googleAuthenticatorService = new GoogleAuthenticatorService();
            _notificationService = new ToastNotificationService();

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

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged(); }
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
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Please enter your login.");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Please enter your password.");
                return;
            }

            if (string.IsNullOrEmpty(TwoFactorCode))
            {
                MessageBox.Show("Please enter two factor code.");
                return;
            }

            User? userFromDb = null;
            try
            {
                // Получение пользователя из бд для получения его secret key 
                userFromDb = await _userRepository.GetByAsync(u => u.Login == Login);
                ArgumentNullException.ThrowIfNull(userFromDb);
            }
            catch (ArgumentNullException)
            { 
                MessageBox.Show("User with this login does not exist.");
                return;
            }
            catch (Exception e)
            {
                dynamic ex = e;
                ExceptionsLogger.Handle(ex);
                MessageBox.Show("Failed to login user.");
                return;
            }

            User user;
            try
            {
                user = new(Login, Password, userFromDb.SecretKey);
            }
            catch (ValidationException ex)
            {
                Messagebox.Show(ex.Message);
                return;
            }

            var result = await _userSignInValidator.ValidateAsync(user);

            if (!result.IsValid)
            {
                ErrorDisplay.ShowValidationErrorMessage(result.Errors);
                return;
            }
        
            if(!_googleAuthenticatorService.Validate(user.SecretKey, TwoFactorCode))
            {
                MessageBox.Show("Invalid two factor code.");
                return;
            }

            Navigation.NavigateTo<PasswordsViewModel>();

            _notificationService.Send(new ToastContentBuilder()
                .AddText($"Hello {Login}!")
                .AddText("Login successfully completed")
                .AddAppLogoOverride(new Uri(ConfigurationManager.AppSettings.Get("defaultNotificationLogo") ?? ""), ToastGenericAppLogoCrop.Circle));
        }
        private void NavigateToRegistration(object obj)
        {
            Navigation.NavigateTo<RegistrationViewModel>();
        }

        #endregion
    }
}
