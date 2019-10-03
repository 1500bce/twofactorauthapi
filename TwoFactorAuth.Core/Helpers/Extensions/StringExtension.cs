using System;
using System.Collections.Generic;
using System.Text;

namespace TwoFactorAuth.Core.Helpers.Extensions
{
    public static class StringExtension
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
