namespace TwoFactorAuth.Core.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string ForceHttps(this string value)
        {
            return value.Replace("http", "https");
        }

        public static string ConstructMessage(this string message, string value)
        {
            return string.Format(message, value);
        } 
    }
}
