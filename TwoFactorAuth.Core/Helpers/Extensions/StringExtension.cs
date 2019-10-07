namespace TwoFactorAuth.Core.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string ForceHttps(this string value)
        {
            UriBuilder uri = new UriBuilder(value);
            uri.Scheme = Uri.UriSchemeHttps;
            uri.Port = uri.Uri.IsDefaultPort ? uri.Port : -1;

            return uri.ToString();
        }

        public static string ConstructMessage(this string message, string value)
        {
            return string.Format(message, value);
        } 
    }
}
