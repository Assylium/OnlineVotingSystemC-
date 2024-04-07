using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using System.ComponentModel.DataAnnotations;

namespace PRN221_VotingSystem.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var listEmail = AccountDAO.Instance.GetAll().Select(account => account.Email).ToList();

                if (!listEmail.Contains(Email))
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return Page();
                }

                return RedirectToPage("/Logins/OTP", new { email = Email });
            }

            return Page();
        }
    }
}
