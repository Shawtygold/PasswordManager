using PasswordBox.Core;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PasswordBox.MVVM.ViewModel.FormsViewModel
{
    internal class QrCodeFormViewModel : Core.ViewModel
    {
        public QrCodeFormViewModel(ImageSource qrCode, string manualEntryKey)
        {
            QrCode = qrCode;
            ManualKey = manualEntryKey;

            CloseCommand = new RelayCommand(Close);
            MinimizeCommand = new RelayCommand(Minimize);
        }

        #region Properties

        private string _manualKey;
        public string ManualKey
        {
            get { return _manualKey; }
            set { _manualKey = value; OnPropertyChanged(); }
        }

        private ImageSource _qrCode;
        public ImageSource QrCode
        {
            get { return _qrCode; }
            set { _qrCode = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MinimizeCommand { get; set; }

        private void Close(object obj)
        {
            if (obj is not Window form)
                return;

            form.Close();
        }
        private void Minimize(object obj)
        {
            if (obj is not Window form)
                return;

            form.WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
