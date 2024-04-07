using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.Linq;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        

        [BindProperty]
        public Account Account { get; set; }

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
            
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") == null)
                return Page();
            else
                return RedirectToPage("/Index");
        }

        public IActionResult OnPost()
        {
            var account = AccountDAO.Instance.GetAll().FirstOrDefault(a => a.Username == Account.Username && a.Password == Account.Password);

           
            if (account != null)
            {
                if (account.IsActive == 1)
                {
                    HttpContext.Session.SetString("account", JsonSerializer.Serialize(account));
                    TempData["IsLoggedIn"] = true;

                
                    HttpContext.Session.SetString("IsAdmin", account.IsAdmin == 1 ? "true" : "false");

                    if (account.IsAdmin == 1)
                    {
                        return RedirectToPage("/Admin/User/UserManager");
                    }
                    else if (account.IsMod == 1)
                    {
                        return RedirectToPage("/HomePage");
                    }
                    else
                    {
                        return RedirectToPage("/HomePage");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Your account is inactive.";
                    return Page();
                }
            }

            else
            {
                TempData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }


    }
}
