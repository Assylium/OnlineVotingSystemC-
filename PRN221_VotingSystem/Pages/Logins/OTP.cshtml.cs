using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class OTPModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return RedirectToPage("/Logins/ForgotPassword");
            }

            if (HttpContext.Session.GetString("OTPForgotPassword") == null)
            {
                var OTPGenerate = OTPDAO.Instance.GenerateOTP();
                OTPDAO.Instance.SendMail(Email, OTPGenerate, "Your forgot password Otp is : ");
                Otp otp = new()
                {
                    Otp1 = OTPGenerate,
                    Email = this.Email,
                    TimeStamp = DateTime.Now,
                };
                OTPDAO.Instance.UpdateOrInsertToOTP(otp);
                HttpContext.Session.SetString("OTPForgotPassword", JsonSerializer.Serialize(otp));
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                string otpJson = HttpContext.Session.GetString("OTPForgotPassword");
                if (otpJson != null)
                {
                    Otp otp = JsonSerializer.Deserialize<Otp>(otpJson);
                    string OTPRegister = otp.Otp1;
                    if (OTP != OTPRegister)
                    {
                        TempData["ErrorMessage"] = "Invalid OTP";
                        return Page();
                    }
                    else
                    {
                        return RedirectToPage("/Logins/NewPassword", new { email = Email });
                    }
                }               
            }
            return Page();
        }
    }
}
