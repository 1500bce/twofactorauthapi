using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TwoFactorAuthWebSample.Models;
using Google.Authenticator;

namespace TwoFactorAuthWebSample.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            string secretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", secretKey, 300, 300);

            string qrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            string manualEntrySetupCode = setupInfo.ManualEntryKey;

            string imgQrCode = "data:image/png;base64," + qrCodeImageUrl;
            string lblManualSetupCode = manualEntrySetupCode;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TwoFactorAuth()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
