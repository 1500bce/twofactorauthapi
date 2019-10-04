using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuthAPI.TwoFactorAuth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoFactorAuthAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TwoFactorAuthController : ControllerBase
    {
        private readonly TwoFactorAuthSettings _twoFactorAuthSettings;

        public TwoFactorAuthController(IOptions<TwoFactorAuthSettings> twoFactorAuthSettings) => _twoFactorAuthSettings = twoFactorAuthSettings.Value;

        // generate qrcode and manual keys for 2fa
        [HttpPost]
        public IActionResult GenerateKeys([BindRequired, FromBody]GenerateKeysModel data)
        {
            var response = TwoFactorAuthKeys.GenerateKeys(data.Email, _twoFactorAuthSettings);
            return Ok(response);            
        }

        // verify codes entered by user
        [HttpPost]
        public IActionResult VerifyCode([BindRequired, FromBody]VerifyCodeModel data)
        {
            var response = TwoFactorAuthKeys.ValidateKeys(data);
            return Ok(response);
        }
    }
}
