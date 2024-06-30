using PasswordBox.Interfaces.GoogleAuth;

namespace PasswordBox.MVVM.Model.GoogleAuthentificator
{
    internal class SecretKeyGenerator : ISecretKeyGenerator
    {
        public string GenerateKey(string login)
        {
            return login;
        }
    }
}
