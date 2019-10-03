using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Authenticator;
using System.ComponentModel.DataAnnotations;
using TwoFactorAuthSample.Models;

namespace TwoFactorAuthSample.Pages
{
    public class IndexModel : PageModel
    {
        public string AuthenticatorUri { get; set; }

        public string SecretKey { get; set; }

        public string QrCodeImageUrl { get; set; }

        public string ManualEntrySetupCode { get; set; }
       
        public string ManualSetupCode { get; set; }

        public string ValidationMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = "user@example.com";

        [BindProperty]
        public string Code { get; set; }

        public IndexModel()
        {

        }

        //public void OnGet()
        //{
        //    if (string.IsNullOrEmpty(EmailAddress))
        //    {
        //        Response.Redirect("/?email=" + EmailAddress);
        //    }
        //}

        public IActionResult OnGet()
        {
            if(!(Request.Query["redirect"] == "1"))
            {
                string url = "/?email=user@example.com" + "&redirect=1";
                return Redirect(url);
            }

            TwoFactorAuthModel model = new TwoFactorAuthModel();
            this.SecretKey = "abcdefghij"; /*Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);*/

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string email = Request.Query["redirect"];
            if (!string.IsNullOrEmpty(Request.Query["email"]))
            {
                email = Request.Query["email"];
            }
            var setupInfo = tfa.GenerateSetupCode("TestTwoFactor", email, SecretKey, false, 300);

            this.QrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            this.ManualEntrySetupCode = setupInfo.ManualEntryKey;

            this.ManualSetupCode = ManualEntrySetupCode;

            return Page();
        }

        public void OnPost()
        {
            //TwoFactorAuthModel model = new TwoFactorAuthModel();
            //this.SecretKey = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            //TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            //string e = "user@example.com";
            //if (!string.IsNullOrEmpty(Request.Query["email"]))
            //{
            //    e = Request.Query["email"];
            //}
            //var setupInfo = tfa.GenerateSetupCode("TestTwoFactor", e, SecretKey, false, 300);

            //this.QrCodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            //this.ManualEntrySetupCode = setupInfo.ManualEntryKey;

            //this.ManualSetupCode = ManualEntrySetupCode;
            if (!string.IsNullOrEmpty(Request.Form["Code"]))
            {
                VerifyCode();
            }
        }

        public void VerifyCode()
        {            
            string code = Request.Form["Code"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            var result = tfa.ValidateTwoFactorPIN(Request.Form["SecretKey"], code);

            if (result)
            {
                this.ValidationMessage = code + " is valid";
            }
            else
            {
                this.ValidationMessage = code + " is not valid";
            }
        }
    }
}
