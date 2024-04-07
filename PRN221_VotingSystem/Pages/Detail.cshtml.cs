using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.Pages
{
    public class DetailModel : PageModel
    {
        [BindProperty]
        public string Question { get; set; }

        [BindProperty]
        public IFormFile image { get; set; }

        [BindProperty]
        public string description { get; set; }

        [BindProperty]
        public string name { get; set; }
        public int Security { get; set; }
        public List<Votingobject> votelist { get; set; }

        public void OnGet(int id, string type, int security)
        {
            Security = security;
            votelist = VotingobjectDAO.Instance.GetVotingobjects(id);
        }

        public async Task<IActionResult> OnPostAsync(string action)
        {
            int campaignId = int.Parse(Request.Query["id"]);
            if (action == "Add")
            {
                string base64Image = null;
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.CopyToAsync(memoryStream);
                        byte[] imageBytes = memoryStream.ToArray();
                        base64Image = Convert.ToBase64String(imageBytes);
                    }
                }
                string ruleName = Request.Query["type"];
                var vote = new Votingobject
                {
                    Description = description,
                    VotingObjectName = name,
                    ImgPath = "data:image/jpeg;base64," + base64Image,
                    CampaignId = campaignId,
                    VotingObjectType = ruleName
                };
                VotingobjectDAO.Instance.AddVotingObject(vote);
                return RedirectToPage("/Detail", new { id = campaignId, type = ruleName, security = ruleName });
            }
            else
            {
                return RedirectToPage("/ReviewCampaign", new { id = campaignId , question = Question});
            }
        }
    }
}
