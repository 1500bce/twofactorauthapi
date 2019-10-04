using System.ComponentModel.DataAnnotations;

namespace TwoFactorAuthAPI.TwoFactorAuth.Models
{
    public class GenerateKeysModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
