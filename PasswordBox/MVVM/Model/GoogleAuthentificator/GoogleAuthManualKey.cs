using Google.Authenticator;

namespace PasswordBox.MVVM.Model.GoogleAuthentificator
{
    internal class GoogleAuthManualKey
    {
        internal string Get(string issuer, string accountLogin, string accountSecretKey, HashType hashType = HashType.SHA256)
        {
            TwoFactorAuthenticator tfa = new(hashType);
            return tfa.GenerateSetupCode(issuer, accountLogin, accountSecretKey, false, 3).ManualEntryKey;
        }
    }
}
