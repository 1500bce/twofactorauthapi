using System;
using Google.Authenticator;

namespace TwoFactorAuth.Data.Models
{
    public class TwoFactorAuthKeys
    {        
        public string SecretKey { get; set; }
        
        public string Code { get; set; }
        
        public string Email { get; set; }        

        public static Response GenerateKeys(string email)
        {
            string secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            Response model = new Response();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            try
            {
                var setupInfo = tfa.GenerateSetupCode("BlotocolPH", email, secretKey, 300, 300);
                model.Object = new GenerateKeyResponse(setupInfo.ManualEntryKey, setupInfo.QrCodeSetupImageUrl, secretKey);
                model.Success = true;
            }
            catch
            {
                model.Success = false;
            }

            return model;
        }
        
        public static BaseResponse ValidateKeys(TwoFactorAuthKeys data)
        {
            BaseResponse response = new BaseResponse();
            if (!string.IsNullOrEmpty(data.SecretKey) || !string.IsNullOrEmpty(data.Code))
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                response.Success = tfa.ValidateTwoFactorPIN(data.SecretKey, data.Code, TimeSpan.FromSeconds(30));
            }
            if (response.Success)
            {
                response.Message = "";  
            }

            return response;
        }

    }
}
