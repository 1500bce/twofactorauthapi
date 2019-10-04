using System.ComponentModel.DataAnnotations;

namespace TwoFactorAuthAPI.TwoFactorAuth.Models
{
    public class VerifyCodeModel
    {
        [Required]
        public string SecretKey { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer number.")]
        public string Code { get; set; }
    }
}
