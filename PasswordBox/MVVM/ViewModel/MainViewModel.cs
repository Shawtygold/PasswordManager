using PasswordBox.Core;
using PasswordBox.MVVM.View.Forms;
using PasswordBox.MVVM.ViewModel.FormsViewModel;
using PasswordBox.Services;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel
{
    class MainViewModel : Core.ViewModel
    {
        public MainViewModel(INavigationService navigation)
        {
            Navigation = navigation;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);

            Navigation.NavigateTo<LoginViewModel>();
        }

        #region Properties

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set { _navigation = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }

        private void Close(object obj) => Application.Current.Shutdown();
        private void Minimize(object obj) => Application.Current.MainWindow.WindowState = WindowState.Minimized;

        #endregion
    }
}
