using Microsoft.Toolkit.Uwp.Notifications;

namespace PasswordBox.MVVM.Model
{
    internal class NotificationService
    {
        internal static void SendWelcomeNotification(string login)
        {
            var notification = new ToastContentBuilder();
            notification.AddText($"Hello {login}!");
            notification.AddText("Login successfully completed.");
            notification.AddAppLogoOverride(new Uri(@"C:\Users\user\Pictures\AppIcons\Financebox\FinanceNotificationIcon.png"), ToastGenericAppLogoCrop.Circle);
            notification.Show();
        }
    }
}
