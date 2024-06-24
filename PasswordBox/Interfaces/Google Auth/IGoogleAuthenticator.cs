namespace PasswordBox.Interfaces.Google_Auth
{
    internal interface IGoogleAuthenticator
    {
        string GetManualEntryKey(string login);
        bool Validate(string login, string twoFactorCode);
    }
}
