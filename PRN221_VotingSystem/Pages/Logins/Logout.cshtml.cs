using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_VotingSystem.Pages.Logins
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Clear session variables related to authentication
            HttpContext.Session.Clear();

            // Redirect to the index page after logout
            return RedirectToPage("/Index");
        }
    }
}
