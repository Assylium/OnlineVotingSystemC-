using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Comment
    {
        public Comment()
        {
            Likes = new HashSet<Like>();
            Notifications = new HashSet<Notification>();
        }

        public int CommentId { get; set; }
        public int CampaignAccountId { get; set; }
        public int ThreadId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public int? ParentCommentId { get; set; }

        public virtual Campaignaccount CampaignAccount { get; set; } = null!;
        public virtual Thread Thread { get; set; } = null!;
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
