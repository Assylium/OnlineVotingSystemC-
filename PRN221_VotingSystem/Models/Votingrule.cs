using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Votingrule
    {
        public Votingrule()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int VotingRuleId { get; set; }
        public string RuleName { get; set; } = null!;
        public string RuleDescription { get; set; } = null!;
        public string RuleType { get; set; } = null!;

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
