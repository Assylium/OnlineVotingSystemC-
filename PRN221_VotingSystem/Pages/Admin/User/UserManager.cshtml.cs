using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.Pages.Admin.User
{
    public class UserManagerModel : PageModel
    {
        public List<Account> Accounts { get; private set; }
        public UserManagerModel()
        {
            Accounts = new List<Account>();
        }
        public IActionResult OnGet()
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToPage("/HomePage");
            }

            Accounts = AccountDAO.Instance.GetAllAccounts();
            return Page();
        }
        public void OnPostSearch(string userName)
        {
            var listAccount = AccountDAO.Instance.GetAllAccounts();
            Accounts = listAccount.Where(u => u.Username.Contains(userName)).ToList();
        }
        public void OnPostDelete(int userId)
        {
            var listAccount = AccountDAO.Instance.GetAllAccounts();
            var accountToBeDeleted = listAccount.FirstOrDefault(u => u.UserId == userId);
            try
            {
                using var data = new PRN221_DBContext();
                data.Accounts.Remove(accountToBeDeleted);
                data.SaveChanges();
                Accounts = AccountDAO.Instance.GetAllAccounts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
