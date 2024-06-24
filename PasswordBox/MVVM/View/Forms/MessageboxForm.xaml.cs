using System.Windows;
using System.Windows.Input;

namespace PasswordBox.MVVM.View.Forms
{
    /// <summary>
    /// Логика взаимодействия для MessageboxForm.xaml
    /// </summary>
    public partial class MessageboxForm : Window
    {
        public MessageboxForm()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
