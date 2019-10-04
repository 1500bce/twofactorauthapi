using System;
using Google.Authenticator;
using TwoFactorAuth.Core.Responses;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuth.Core.Helpers.Extensions;

namespace TwoFactorAuthAPI.TwoFactorAuth.Models
{
    public class TwoFactorAuthKeys
    {
        public static object GenerateKeys(string email, TwoFactorAuthSettings settings)
        {
            string secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            Response model = new Response();
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            tfa.DefaultClockDriftTolerance = TimeSpan.FromSeconds(settings.TOTPTimeout);
            var setupInfo = tfa.GenerateSetupCode("BlotocolPH", email, secretKey, 300, 300);
            model.Object = new GenerateKeyResponse(setupInfo.ManualEntryKey, setupInfo.QrCodeSetupImageUrl.ForceHttps(), secretKey);
            model.Success = true;
            return model;   
        }
        
        public static object ValidateKeys(VerifyCodeModel data)
        {
            bool response = false;
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            response = tfa.ValidateTwoFactorPIN(data.SecretKey, data.Code, TimeSpan.FromSeconds(30));

            if (response)
            {
                return new SuccessResponse();
            }
            else
            {
                ErrorResponse error = new ErrorResponse();
                error.ErrorMessages.Add(MessageHelper.InvalidInput.ConstructMessage(data.Code));
                return error;
            }          
            
        }

    }
}
