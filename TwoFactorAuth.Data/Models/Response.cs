using System;
using System.Collections.Generic;
using System.Text;

namespace TwoFactorAuth.Data.Models
{
    public class Response : BaseResponse
    {
        public GenerateKeyResponse Object { get; set; }
    }

    public class GenerateKeyResponse
    {
        public string ManualSetupKey { get; set; }
        public string QrCodeUri { get; set; }
        public string SecretKey { get; set; }

        public GenerateKeyResponse(string setupKey, string qrCodeUri, string secretKey)
        {
            this.ManualSetupKey = setupKey;
            this.QrCodeUri = qrCodeUri;
            this.SecretKey = secretKey;
        }
    }
}
