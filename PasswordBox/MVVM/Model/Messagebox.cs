using PasswordBox.MVVM.View.Forms;
using PasswordBox.MVVM.ViewModel.FormsViewModel;

namespace PasswordBox.MVVM.Model
{
    class Messagebox
    {
        public static void Show(string message)
        {
            MessageboxForm form = new();
            form.DataContext = new MessageboxViewModel(message);
            form.Show();
        }
    }
}
