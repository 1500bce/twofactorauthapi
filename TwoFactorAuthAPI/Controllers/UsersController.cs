using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TwoFactorAuthAPI.Helpers;
using TwoFactorAuthAPI.Models;
using TwoFactorAuthAPI.Services.Users;

namespace TwoFactorAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {        
        private readonly IUserService _service;
        
        private readonly AppSettings _appSettings;
        
        public UsersController(IUserService userService,
                               IOptions<AppSettings> appSettings)
        {
            _service = userService;
            _appSettings = appSettings.Value;
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateUser user)
        {
            var authenticatedUser = _service.Authenticate(user);
            if (authenticatedUser == null)
            {
                return Unauthorized();
            }

            return Ok(_service.GenerateToken(authenticatedUser, _appSettings));
        }
    }
}