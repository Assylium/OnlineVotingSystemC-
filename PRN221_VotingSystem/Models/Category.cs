using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Category
    {
        public Category()
        {
            Campaigns = new HashSet<Campaign>();
            Mods = new HashSet<Mod>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Mod> Mods { get; set; }
    }
}
