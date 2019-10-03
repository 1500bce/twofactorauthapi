using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuthAPI.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }

        public int TokenExpiration { get; set; }
    }
}
