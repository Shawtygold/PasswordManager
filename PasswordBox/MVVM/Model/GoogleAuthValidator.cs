using Google.Authenticator;

namespace PasswordBox.MVVM.Model
{
    internal class GoogleAuthValidator
    {
        public bool Validate(string login, string twoFactorCode)
        {
            TwoFactorAuthenticator twoFactorAuthenticator = new(HashType.SHA256);
            return twoFactorAuthenticator.ValidateTwoFactorPIN(login, twoFactorCode);
        }
    }
}
