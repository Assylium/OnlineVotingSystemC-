using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Thread
    {
        public Thread()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
        }

        public int ThreadId { get; set; }
        public int CampaignAccountId { get; set; }
        public int CampaignId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Campaignaccount CampaignAccount { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
