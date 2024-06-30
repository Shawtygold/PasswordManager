using Google.Authenticator;

namespace PasswordBox.MVVM.Model.GoogleAuthentificator
{
    internal class GoogleAuthValidator
    {
        public bool Validate(string accountSecretKey, string twoFactorCode, HashType hashType = HashType.SHA256)
        {
            TwoFactorAuthenticator twoFactorAuthenticator = new(hashType);
            return twoFactorAuthenticator.ValidateTwoFactorPIN(accountSecretKey, twoFactorCode);
        }
    }
}
