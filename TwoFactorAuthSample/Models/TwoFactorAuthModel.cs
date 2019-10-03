using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuthSample.Models
{
    public class TwoFactorAuthModel
    {
        public string AuthenticatorUri { get; set; }

        public string SecretKey { get; set; }

        public string QrCodeImageUrl { get; set; }

        public string ManualEntrySetupCode { get; set; }

        public string ManualSetupCode { get; set; }

        public string ValidationMessage { get; set; }
    }
}
