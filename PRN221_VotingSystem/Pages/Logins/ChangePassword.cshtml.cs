using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class ChangePasswordModel : PageModel
    {
        private readonly ILogger<ChangePasswordModel> _logger;

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public ChangePasswordModel(ILogger<ChangePasswordModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPost()
        {
            var loggedInAccount = JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("account"));         

            if (CurrentPassword != loggedInAccount.Password)
            {
                ModelState.AddModelError(nameof(CurrentPassword), "Current password is incorrect.");
                return Page();
            }

            if (NewPassword != ConfirmPassword)
            {
                ModelState.AddModelError(nameof(ConfirmPassword), "New password and confirm password do not match.");
                return Page();
            }

            try
            {
                using (var data = new PRN221_DBContext())
                {
                    var account = data.Accounts.FirstOrDefault(a => a.Username == loggedInAccount.Username);
                    if (account != null)
                    {
                        account.Password = NewPassword;
                        data.SaveChanges();
                    }
                }

                // Optionally, update the password in the session
                loggedInAccount.Password = NewPassword;
                HttpContext.Session.SetString("account", JsonSerializer.Serialize(loggedInAccount));

                // Clear the session and redirect to login
                HttpContext.Session.Remove("account");
                TempData["SuccessMessage"] = "Password changed successfully. Please login again.";
                return RedirectToPage("/Logins/Login");
            }
            catch (Exception ex)
            {
                // Handle exception
                ModelState.AddModelError("", "An error occurred while updating the password.");
                return Page();
            }
        }
    }
}
