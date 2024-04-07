using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;

namespace PRN221_VotingSystem.Pages.Admin.Campaign
{
    public class ViewAllCampaignModel : PageModel
    {
        public List<Models.Campaign> Campaigns { get; private set; }
        public ViewAllCampaignModel()
        {
            Campaigns = new List<Models.Campaign>();
        }
        public void OnGet()
        {
            Campaigns = CampaignDAO.Instance.GetAllCampaigns();
        }
    }
}
