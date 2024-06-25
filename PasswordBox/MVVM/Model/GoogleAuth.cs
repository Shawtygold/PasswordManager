using PasswordBox.Interfaces.Google_Auth;

namespace PasswordBox.MVVM.Model
{
    internal class GoogleAuth : IGoogleAuthenticator
    {
        const string issuer = "PasswordBox";

        private GoogleAuthValidator _validator;
        private GoogleAuthEntryKey _googleAuthEntryKey;

        public GoogleAuth()
        {
            _validator = new GoogleAuthValidator();
            _googleAuthEntryKey = new GoogleAuthEntryKey();
        }

        public string GetManualEntryKey(string login)
        {
            return _googleAuthEntryKey.GetEntryKey(issuer, login, login);
        }
        public bool Validate(string login, string twoFactorCode)
        {
            return _validator.Validate(login, twoFactorCode);
        }
    }
}
