using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuthWebSample.Models
{
    public class TwoFactorAuthentication
    {
        public string AuthenticatorUri { get; set; }

        public string SecretKey { get; set; }

        public string QrCodeImageUrl { get; set; }

        public string ManualEntrySetupCode { get; set; }

        public string ImgQrCode { get; set; }

        public string ManualSetupCode { get; set; }

        public TwoFactorAuthentication()
        {
            string secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", secretKey, 300, 300);

            this.QrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            this.ManualEntrySetupCode = setupInfo.ManualEntryKey;

            this.ImgQrCode = "data:image/png;base64," + QrCodeImageUrl;
            this.ManualSetupCode = ManualEntrySetupCode;
        }
    }
}
