using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwoFactorAuth.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoFactorAuthAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TwoFactorAuthController : ControllerBase
    {
        // generate qrcode and manual keys for 2fa
        [HttpPost]
        public IActionResult GenerateKeys([FromBody]TwoFactorAuthKeys data)
        {
            var response = TwoFactorAuthKeys.GenerateKeys(data.Email);
            return Ok(response);
        }

        // verify codes entered by user
        [HttpPost]
        public IActionResult VerifyCode([FromBody]TwoFactorAuthKeys data)
        {
            //bool result = TwoFactorAuthKeys.ValidateKeys(manualSetupKey, code);
            BaseResponse response = new BaseResponse();
            response = TwoFactorAuthKeys.ValidateKeys(data);
            return Ok(response);
        }
    }
}
