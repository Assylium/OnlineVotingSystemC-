using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Vote
    {
        public int VoteId { get; set; }
        public int VoterId { get; set; }
        public int VotingObjectId { get; set; }
        public int CampaignId { get; set; }
        public int Point { get; set; }

        public virtual Campaign CampaignNavigation { get; set; } = null!;
        public virtual Campaignaccount Voter { get; set; } = null!;
        public virtual Votingobject VotingObject { get; set; } = null!;
    }
}
