using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Pinned
    {
        public int CampaignId { get; set; }
        public int UserId { get; set; }
        public byte IsPinned { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Account User { get; set; } = null!;
    }
}
