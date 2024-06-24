using PasswordBox.Core;
using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.ViewModel.FormsViewModel
{
    class MessageboxViewModel : Core.ViewModel
    {
        public MessageboxViewModel(string message)
        {
            Message = message;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
            OkCommand = new RelayCommand(Ok);
        }

        #region Properties

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }
        public ICommand OkCommand { get; set; }

        private void Close(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.Close();
        }
        private void Minimize(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.WindowState = WindowState.Minimized;
        }
        private void Ok(object obj)
        {
            if (obj is not Window wnd)
                return;

            wnd.Close();
        }

        #endregion
    }
}
