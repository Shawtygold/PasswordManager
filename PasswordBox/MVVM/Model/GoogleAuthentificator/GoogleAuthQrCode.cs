using Google.Authenticator;
using PasswordBox.MVVM.Model.Converters;
using System.Windows.Media.Imaging;

namespace PasswordBox.MVVM.Model.GoogleAuthentificator
{
    internal class GoogleAuthQrCode
    {
        internal BitmapSource Get(string issuer, string accountLogin, string accountSecretKey, HashType hashType = HashType.SHA256)
        {
            TwoFactorAuthenticator tfa = new(hashType);
            string qrUrl = tfa.GenerateSetupCode(issuer, accountLogin, accountSecretKey, false, 3).QrCodeSetupImageUrl;
            return QrCodeConverter.ConvertQrCodeUrlToBitmapSource(qrUrl);
        }
    }
}
