using Google.Authenticator;
using PasswordBox.Interfaces.GoogleAuth;
using System.Windows.Media.Imaging;

namespace PasswordBox.MVVM.Model.GoogleAuthentificator
{
    internal class GoogleAuthenticatorService : IGoogleAuthenticatorService
    {
        private const HashType hashType = HashType.SHA256;
        private readonly GoogleAuthValidator _googleAuthValidator;
        private readonly GoogleAuthManualKey _googleAuthManualKey;
        private readonly GoogleAuthQrCode _googleAuthQrCode;

        public GoogleAuthenticatorService()
        {
            _googleAuthValidator = new GoogleAuthValidator();
            _googleAuthManualKey = new GoogleAuthManualKey();
            _googleAuthQrCode = new GoogleAuthQrCode();
        }

        public BitmapSource GetQrCode(string issuer, string accountLogin, string accountSecretKey)
        {
            return _googleAuthQrCode.Get(issuer, accountLogin, accountSecretKey, hashType);
        }
        public string GetManualKey(string issuer, string accountLogin, string accountSecretKey)
        {
            return _googleAuthManualKey.Get(issuer, accountLogin, accountSecretKey, hashType);
        }
        public bool Validate(string accountSecretKey, string twoFactorCode)
        {
            return _googleAuthValidator.Validate(accountSecretKey, twoFactorCode, hashType);
        }
    }
}
