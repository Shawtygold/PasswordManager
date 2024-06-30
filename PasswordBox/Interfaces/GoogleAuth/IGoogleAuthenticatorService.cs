using System.Windows.Media.Imaging;

namespace PasswordBox.Interfaces.GoogleAuth
{
    internal interface IGoogleAuthenticatorService
    {
        BitmapSource GetQrCode(string issuer, string accountLogin, string accountSecretKey);
        string GetManualKey(string issuer, string accountLogin, string accountSecret);
        bool Validate(string accountSecretKey, string twoFactorCode);
    }
}
