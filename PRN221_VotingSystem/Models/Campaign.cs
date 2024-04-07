using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public class Campaign
    {
        public Campaign()
        {
            Campaignaccounts = new HashSet<Campaignaccount>();
            Pinneds = new HashSet<Pinned>();
            Votes = new HashSet<Vote>();
            Votingobjects = new HashSet<Votingobject>();
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = null!;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string CampaignDescription { get; set; } = null!;
        public int? CampaignCategory { get; set; }
        public string CampaignImg { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public int? VotingRuleId { get; set; }
        public byte? IsPublic { get; set; }
        public string? Password { get; set; }
        public string Status { get; set; } = null!;
        public byte IsAccepted { get; set; }

        public virtual Category? CampaignCategoryNavigation { get; set; }
        public virtual Account? CreatedByNavigation { get; set; }
        public virtual Votingrule? VotingRule { get; set; }
        public virtual ICollection<Campaignaccount> Campaignaccounts { get; set; }
        public virtual ICollection<Pinned> Pinneds { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Votingobject> Votingobjects { get; set; }
    }
}
