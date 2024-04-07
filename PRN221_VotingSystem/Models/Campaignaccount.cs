using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Campaignaccount
    {
        public Campaignaccount()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
            NotificationReceiverAccounts = new HashSet<Notification>();
            NotificationSenderAccounts = new HashSet<Notification>();
            Threads = new HashSet<Thread>();
            Votes = new HashSet<Vote>();
        }

        public int CampaignAccountId { get; set; }
        public int CampaignId { get; set; }
        public int AccountId { get; set; }
        public byte IsHost { get; set; }
        public string? VotingResult { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Campaign Campaign { get; set; } = null!;
        public virtual Mod CampaignAccount { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Notification> NotificationReceiverAccounts { get; set; }
        public virtual ICollection<Notification> NotificationSenderAccounts { get; set; }
        public virtual ICollection<Thread> Threads { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
