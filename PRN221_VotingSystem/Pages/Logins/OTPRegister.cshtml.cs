using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class OTPRegisterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "OTP is required")]
        public string OTP { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Email) || HttpContext.Session.GetString("registerUser") == null)
            {
                return RedirectToPage("/Logins/Register");
            }

            if (HttpContext.Session.GetString("OTPRegister") == null)
            {
                var OTPGenerate = OTPDAO.Instance.GenerateOTP();
                OTPDAO.Instance.SendMail(Email, OTPGenerate, "Your register Otp is : ");
                Otp otp = new()
                {
                    Otp1 = OTPGenerate,
                    Email = this.Email,
                    TimeStamp = DateTime.Now,
                };
                OTPDAO.Instance.UpdateOrInsertToOTP(otp);
                HttpContext.Session.SetString("OTPRegister", JsonSerializer.Serialize(otp));
            }
            return Page();
        }
        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string otpJson = HttpContext.Session.GetString("OTPRegister");
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
                    if (HttpContext.Session.GetString("registerUser") == null)
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        string accountJson = HttpContext.Session.GetString("registerUser");
                        Account account = JsonSerializer.Deserialize<Account>(accountJson);
                        AccountDAO.Instance.AddUser(account);
                        return RedirectToPage("/HomePage");
                    }
                }
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
