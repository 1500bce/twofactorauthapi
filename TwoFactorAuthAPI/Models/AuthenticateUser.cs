using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoFactorAuthAPI.Models
{
    public class AuthenticateUser
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
