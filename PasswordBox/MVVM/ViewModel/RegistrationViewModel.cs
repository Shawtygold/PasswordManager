using PasswordBox.Core;
using PasswordBox.MVVM.Model;
using PasswordBox.Services;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    internal class RegistrationViewModel : Core.ViewModel
    {
        public RegistrationViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            RegisterGoogleAuthCommand = new RelayCommand(RegisterGoogleAuth);
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

        #endregion

        #region Commands

        public ICommand RegisterGoogleAuthCommand { get; set; }
        private void RegisterGoogleAuth(object obj)
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show("Enter login.");
                return;
            }

            GoogleAuth googleAuth = new();
            Messagebox.Show($"Please enter this code in google authenticator app \"{googleAuth.GetManualEntryKey(Login)}\".");
            

        }

        #endregion
    }
}
