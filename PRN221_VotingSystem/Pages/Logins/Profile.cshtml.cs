using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.Text.Json;


namespace PRN221_VotingSystem.Pages
{
    public class ProfileModel : PageModel
    {
        
        public Account Account { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public string Dob { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public IActionResult OnGet()
        {
            var accountJson = HttpContext.Session.GetString("account");
            if (accountJson == null)
            {
                return RedirectToPage("/Index");
            }

            Account = JsonSerializer.Deserialize<Account>(accountJson);
            return Page();
        }
    }
}
