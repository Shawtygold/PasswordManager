using Microsoft.Toolkit.Uwp.Notifications;

namespace PasswordBox.Interfaces.Notifications
{
    internal interface INotificationService
    {
        void Send(ToastContentBuilder notification);
    }
}
