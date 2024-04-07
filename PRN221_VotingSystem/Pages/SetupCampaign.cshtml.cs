using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.Security.Claims;
using System.Text.Json;

namespace PRN221_VotingSystem.Pages
{
    public class SetupCampaignModel : PageModel
    {
        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        [BindProperty]
        public int Hour { get; set; }

        [BindProperty]
        public int Minute { get; set; }

        [BindProperty]
        public int Second { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public byte VoteSecurity { get; set; }

        [BindProperty]
        public string PasswordInput { get; set; }

        public List<Votingrule>  votelist = new List<Votingrule>();

        public List<Category> categories = new List<Category>();
        public async Task<IActionResult> OnGetAsync()
        {
            categories = CategoryDAO.Instance.GetAllCategories();
            votelist = VotingruleDAO.Instance.GetAllRules();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // Save the uploaded image to the server
            string base64Image = null;
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Image.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();
                    base64Image = Convert.ToBase64String(imageBytes);
                }
            }
            var serializedAccount = HttpContext.Session.GetString("account");
            var account = JsonSerializer.Deserialize<Account>(serializedAccount);
            var userid = account.UserId;
            // Create a new campaign object and populate its properties
            var campaign = new Campaign
            {
                CampaignName = Title,
                CampaignDescription = Description,
                CampaignImg = "data:image/jpeg;base64," +base64Image,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(Hour).AddMinutes(Minute).AddSeconds(Second),
                VotingRuleId = Id,
                IsPublic = VoteSecurity,
                Password = VoteSecurity == 2 ? PasswordInput : null,
                CreatedBy = userid,
                Status = "Open",
                IsAccepted = 0
            };

            CampaignDAO.Instance.AddCampaign(campaign);
            var ruletype = VotingruleDAO.Instance.FindById(Id).RuleType;
            return RedirectToPage("/Detail", new { id = campaign.CampaignId, type = ruletype, security = VoteSecurity });
        }
    }
}
