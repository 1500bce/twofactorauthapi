namespace TwoFactorAuthAPI.Models
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
    }
}