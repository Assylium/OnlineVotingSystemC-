using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int CampaignAccountId { get; set; }
        public int ThreadId { get; set; }
        public int? CommentId { get; set; }
        public int LikeType { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Campaignaccount CampaignAccount { get; set; } = null!;
        public virtual Comment? Comment { get; set; }
        public virtual Thread Thread { get; set; } = null!;
    }
}
