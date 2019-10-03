using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFactorAuth.Data.Entity;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuthAPI.Models;

namespace TwoFactorAuthAPI.Services.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        User Authenticate(AuthenticateUser user);

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="authenticatedUser"></param>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        object GenerateToken(User authenticatedUser, AppSettings appSettings);

        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        void Add(User user, string password);        

    }
}
