using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.Pages
{
    public class CampaignDetailsModel : PageModel
    {

        public CampaignDetailsModel()
        {
            Campaign = new Campaign();
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Campaign Campaign { get; set; }

        public int CampaignId { get; set; }
        public List<Votingobject> Votingobjects { get; set; }
        public void OnGet()
        {
            Campaign = CampaignDAO.Instance.GetCampaignById(Id);
            CampaignId = Campaign.CampaignId;
            Votingobjects = VotingobjectDAO.Instance.GetVotingobjects(Id);
        }
    }
}
