using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordBox.MVVM.Model
{
    internal class GoogleAuthEntryKey
    {
        public string GetEntryKey(string issuer, string accountLogin, string accountSecretKey)
        {
            TwoFactorAuthenticator twoFactorAuthenticator = new(HashType.SHA256);
            SetupCode setupInfo = twoFactorAuthenticator.GenerateSetupCode(issuer, accountLogin, accountSecretKey, false, 3);
            return setupInfo.ManualEntryKey;
        }
    }
}
