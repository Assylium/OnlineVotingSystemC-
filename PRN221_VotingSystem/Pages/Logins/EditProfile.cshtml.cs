// EditProfileModel.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class EditProfileModel : PageModel
    {
        private readonly PRN221_DBContext _context;
        public EditProfileModel(PRN221_DBContext context)
        {
            _context = context;
        }
        public Account Account { get; set; }
        [BindProperty]
        public int UserId { get; set; }
        [BindProperty]
        public DateTime Dob {  get; set; }

        [BindProperty]
        public string FirstName { get; set; }
        

        [BindProperty]
        public string LastName { get; set; }
        [BindProperty]

        public string Job { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Address { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.UserId == id);
            FirstName = Account.FirstName;
            LastName = Account.LastName;
            Job = Account.Job;
            Gender = Account.Gender;
            Dob = Account.Dob;
            Phone = Account.Phone;
            Address = Account.Address;
            Email = Account.Email;
            if (Account == null)
            {   
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserId = Int32.Parse(Request.Query["id"]);
            var existingAccount = await _context.Accounts.FirstOrDefaultAsync(m => m.UserId == UserId);

            if (existingAccount == null)
            {
                return NotFound();
            }

            existingAccount.FirstName = FirstName;
            existingAccount.LastName = LastName;
            existingAccount.Dob = Dob;
            existingAccount.Job = Job;
            existingAccount.Gender = Gender;
            existingAccount.Email = Email;
            existingAccount.Phone = Phone;
            existingAccount.Address = Address;
            Account = existingAccount;
            try
            {
                await _context.SaveChangesAsync();

                var accountJson = JsonSerializer.Serialize(existingAccount);
                HttpContext.Session.SetString("account", accountJson);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Logins/Profile");
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.UserId == id);
        }
    }
}
