using System.Windows;

namespace PasswordBox.MVVM.ViewModel.FormsViewModel
{
    /// <summary>
    /// Логика взаимодействия для QrCodeForm.xaml
    /// </summary>
    public partial class QrCodeForm : Window
    {
        public QrCodeForm()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
