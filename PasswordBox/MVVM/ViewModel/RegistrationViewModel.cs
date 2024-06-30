﻿using Microsoft.Toolkit.Uwp.Notifications;
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
using PasswordBox.MVVM.ViewModel.FormsViewModel;
using PasswordBox.Services;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PasswordBox.MVVM.ViewModel
{
    internal class RegistrationViewModel : Core.ViewModel
    {
        private readonly string _appName = ConfigurationManager.AppSettings.Get("AppName") ?? "PasswordBox";
        private readonly UserValidator _userValidator; 
        private readonly IUserRepository _userRepository;
        private readonly IGoogleAuthenticatorService _googleAuthenticatorService;
        private readonly ISecretKeyGenerator _secretKeyGenerator;
        private readonly INotificationService _notificationService;

        public RegistrationViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            _userValidator = new UserValidator();
            _userRepository = new UserRepository(new ApplicationContext());
            _googleAuthenticatorService = new GoogleAuthenticatorService();
            _secretKeyGenerator = new SecretKeyGenerator();
            _notificationService = new ToastNotificationService();

            RegisterCommand = new RelayCommand(Register);
            NavigateToLoginCommand = new RelayCommand(NavigateToLogin);
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

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; set; }
        public ICommand NavigateToLoginCommand { get; set; }
        private async void Register(object obj)
        {
            string secretKey = _secretKeyGenerator.GenerateKey(Login);
            string manualEntryKey = _googleAuthenticatorService.GetManualKey(_appName, Login, secretKey);
            BitmapSource qrCode = _googleAuthenticatorService.GetQrCode(_appName, Login, secretKey);

            // Создание окна с Qr и ручным кодами
            QrCodeForm form = new();
            form.DataContext = new QrCodeFormViewModel(qrCode, manualEntryKey);
            form.ShowDialog();

            // Валидация пользователя
            User user = new(Login, Password, secretKey);
            var result = await _userValidator.ValidateAsync(user);
            if (!result.IsValid)
            {
                ErrorDisplay.ShowValidationErrorMessage(result.Errors);
                return;
            }

            try
            {
                // Добавление пользователя в бд
                await _userRepository.AddAsync(user);
            }
            catch(Exception e)
            {
                dynamic ex = e;
                ExceptionsLogger.Handle(ex);
                MessageBox.Show("Failed to register user.");
                return;
            }

            _notificationService.Send(new ToastContentBuilder()
                .AddText("Registration successfully completed!")
                .AddAppLogoOverride(new Uri(@"C:\Users\user\Pictures\AppIcons\Financebox\FinanceNotificationIcon.png"), ToastGenericAppLogoCrop.Circle));

            Navigation.NavigateTo<LoginViewModel>();
        }
        private void NavigateToLogin(object obj)
        {
            Navigation.NavigateTo<LoginViewModel>();
        }

        #endregion
    }
}
