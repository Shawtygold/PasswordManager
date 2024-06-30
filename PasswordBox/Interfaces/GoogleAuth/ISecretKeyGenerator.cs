namespace PasswordBox.Interfaces.GoogleAuth
{
    internal interface ISecretKeyGenerator
    {
        string GenerateKey(string login);
    }
}
