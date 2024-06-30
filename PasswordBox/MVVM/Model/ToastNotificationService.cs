using Microsoft.Toolkit.Uwp.Notifications;
using PasswordBox.Interfaces.Notifications;

namespace PasswordBox.MVVM.Model
{
    internal class ToastNotificationService : INotificationService
    {
        public void Send(ToastContentBuilder notification)
        {
            notification.Show();
        }
    }
}
