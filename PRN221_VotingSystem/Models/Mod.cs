using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Mod
    {
        public int ModId { get; set; }
        public string? ModName { get; set; }
        public string? ModEmail { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Campaignaccount? Campaignaccount { get; set; }
    }
}
