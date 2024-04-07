using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PRN221_VotingSystem.Models;
using System;
using System.Collections.Generic;
using PRN221_VotingSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using static PRN221_VotingSystem.Pages.IndexModel;
using System.Security.Claims;

namespace PRN221_VotingSystem.Pages
{
    public class CampaignStatisticModel : PageModel
    {
        public int CampaignId { get; set; }
        public bool IsClosed { get; set; }
        public bool HasEnd { get; set; }
        public DateTime? EndTime { get; set; }
        public int NoOfVotes { get; set; }
        public Dictionary<string, int> CurrentResult { get; set; }

        public string CurrentResultJson { get; set; }
        public Dictionary<string, int> MyCurrentVote { get; set; }
        public string MyCurrentVoteJson { get; set; }

        public Dictionary<string, int> LeadingOption { get; set; }

        private readonly VoteDAO _voteDAO;

        public CampaignStatisticModel()
        {
            _voteDAO = VoteDAO.Instance;
        }

        public IActionResult OnGet(int campaignId)
        {


            // Lấy số lượng phiếu bầu
            NoOfVotes = _voteDAO.GetNoOfVotes(campaignId);

            LeadingOption = _voteDAO.GetLeadingOption(campaignId);

            // Lấy phiếu bầu hiện tại
            MyCurrentVote = _voteDAO.GetCurrentVote(campaignId);
            MyCurrentVoteJson = JsonConvert.SerializeObject(MyCurrentVote);

            // Lấy kết quả
            CurrentResult = _voteDAO.GetCurrentResult(campaignId);
            CurrentResultJson = JsonConvert.SerializeObject(CurrentResult);

            IsClosed = _voteDAO.GetCampaignStatus(campaignId);
            EndTime = _voteDAO.GetEndTimeById(campaignId);

            HasEnd = DateTime.Now > EndTime;

            CampaignId = campaignId;

            return Page();
        }
    }
}
