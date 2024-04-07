using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Votingobject
    {
        public Votingobject()
        {
            Votes = new HashSet<Vote>();
        }

        public int VotingObjectId { get; set; }
        public string VotingObjectName { get; set; } = null!;
        public string VotingObjectType { get; set; } = null!;
        public int CampaignId { get; set; }
        public string? ImgPath { get; set; }
        public string? Question { get; set; }
        public string? Description { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
