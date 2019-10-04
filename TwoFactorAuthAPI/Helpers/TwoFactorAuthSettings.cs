namespace TwoFactorAuthAPI.Helpers
{
    public class TwoFactorAuthSettings
    {
        public int TOTPTimeout { get; set; }

        public string Issuer { get; set; }
    }
}
