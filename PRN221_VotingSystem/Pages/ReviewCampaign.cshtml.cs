using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.Pages
{
    public class ReviewCampaignModel : PageModel
    {
        public List<Votingobject> votingobjects { get; set; }

        public IActionResult OnGet(string question, int id)
        {
            VotingobjectDAO.Instance.AddQuestion(id, question);
            votingobjects = VotingobjectDAO.Instance.GetVotingobjects(id);
            return Page();
        }
        public IActionResult OnPost()
        {
            return RedirectToAction("homepage");
        }
    }
}
