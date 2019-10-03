using System;
using System.Collections.Generic;
using System.Text;
using TwoFactorAuth.Data.Entity;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuthAPI.Services.Users;
using System.Linq;

namespace TwoFactorAuth.Core.Helpers
{
    public class DbInitializerHelper
    {
        public static void CreateDefaultData(DataContext context)
        {
            context.Database.EnsureCreated();

            AddDefaultUsers(context);
        }

        private static void AddDefaultUsers(DataContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var service = new UserService(context);

            var adminUser = new User
            {
                Username = "admin",
            };
            service.Add(adminUser, "blotocol");

        }
    }
}
