using Google.Authenticator;
using PasswordBox.Interfaces.Google_Auth;

namespace PasswordBox.MVVM.Model
{
    internal class GoogleAuth : IGoogleAuthenticator
    {
        const string issuer = "PasswordBox";

        public string GetManualEntryKey(string login)
        {
            TwoFactorAuthenticator twoFactorAuthenticator = new(HashType.SHA256);
            SetupCode setupInfo = twoFactorAuthenticator.GenerateSetupCode(issuer, login, login, false, 3);
            return setupInfo.ManualEntryKey;
        }
        public bool Validate(string login, string twoFactorCode)
        {
            TwoFactorAuthenticator twoFactorAuthenticator = new(HashType.SHA256);
            return twoFactorAuthenticator.ValidateTwoFactorPIN(login, twoFactorCode);
        }
    }
}
