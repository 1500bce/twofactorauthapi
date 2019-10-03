using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TwoFactorAuth.Data.Entity;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuthAPI.Models;
using TwoFactorAuth.Core.Helpers.Extensions;

namespace TwoFactorAuthAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }
        public User Authenticate(AuthenticateUser user)
        {
            if (user.Username.HasValue() == false || user.Password.HasValue() == false)
            {
                return null;
            }
            

            var authenticatedUser = _context.Users
                                            .SingleOrDefault(x => x.Username.ToLower() == user.Username.ToLower());

            if (authenticatedUser == null)
            {
                return null;
            }

            // Check if password hash is verified or valid
            if (IsPasswordHashVerified(user.Password, authenticatedUser.PasswordHash, authenticatedUser.PasswordSalt) == false)
            {
                return null;
            }

            // Authentication successful
            return authenticatedUser;
        }

        private static bool IsPasswordHashVerified(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (password.HasValue() == false)
            {
                throw new ArgumentException(MessageHelper.PasswordIsInvalid, "password");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException(MessageHelper.InvalidPasswordHashLength, "passwordHash");
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException(MessageHelper.InvalidPasswordSaltLength, "passwordSalt");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public object GenerateToken(User authenticatedUser, AppSettings appSettings)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, authenticatedUser.Username),
                new Claim(JwtRegisteredClaimNames.Jti, authenticatedUser.Id.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(appSettings.TokenExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                authenticatedUser.Id,
                authenticatedUser.Username,
                Token = tokenString
            };
        }

        public void Add(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();            
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (password.HasValue() == false)
            {
                throw new ArgumentException(MessageHelper.PasswordIsInvalid, "password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
