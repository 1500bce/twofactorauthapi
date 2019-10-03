//using Google.Authenticator;
using Newtonsoft.Json;
using System;
using Google.Authenticator;


namespace TwoFactorAuthAPI.Models
{
    public class TwoFactorAuthKeys
    {
        [JsonProperty("ManualSetupKey")]
        public string ManualSetupKey { get; set; }
        
        public string QrCodeUri { get; set; }

        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        public bool IsVerified { get; set; }
       
        public static TwoFactorAuthKeys GenerateKeys(string email)
        {
            string secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            TwoFactorAuthKeys model = new TwoFactorAuthKeys();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("BlotocolPH", email, secretKey, 300, 300);
            model.SecretKey = secretKey;
            model.QrCodeUri = setupInfo.QrCodeSetupImageUrl;
            model.ManualSetupKey = setupInfo.ManualEntryKey;

            return model;
        }

        //public static bool ValidateKeys(string setupKey, string code)
        public static BaseResponse ValidateKeys(TwoFactorAuthKeys data)
        {
            BaseResponse response = new BaseResponse();
            if(!string.IsNullOrEmpty(data.SecretKey) || !string.IsNullOrEmpty(data.Code))
            {
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                response.Success = tfa.ValidateTwoFactorPIN(data.SecretKey, data.Code);
            }
            if (response.Success)
            {
                response.Message = "";  // 
            }

            return response;          
        }
        
    }
    
    public class TwoFactorAuthObject
    {
        public TwoFactorAuthKeys Data { get; set; }
    }
    
}
