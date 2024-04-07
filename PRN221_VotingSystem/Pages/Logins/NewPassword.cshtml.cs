using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_VotingSystem.Pages
{
    public class NewPasswordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirmation password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public Account Account { get; set; }

        public IActionResult OnGet()
        {
            Account = AccountDAO.Instance.GetByEmail(Email);
            if (string.IsNullOrEmpty(Email) || Account == null)
            {
                return RedirectToPage("/Logins/ForgotPassword");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Account = AccountDAO.Instance.GetByEmail(Email);
                Account.Password = Password;
                AccountDAO.Instance.UpdateAccount(Account);
                return RedirectToPage("/Logins/Login");
            }

            return Page();
        }
    }
}
