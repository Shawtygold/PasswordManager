using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.View.Forms
{
    /// <summary>
    /// Логика взаимодействия для PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        public PasswordDialog()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
