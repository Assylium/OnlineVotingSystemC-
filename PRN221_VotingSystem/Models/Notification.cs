using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int ReceiverAccountId { get; set; }
        public string Content { get; set; } = null!;
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? SenderAccountId { get; set; }
        public DateTime? Timestamp { get; set; }
        public byte? IsRead { get; set; }

        public virtual Comment? Comment { get; set; }
        public virtual Thread? Post { get; set; }
        public virtual Campaignaccount ReceiverAccount { get; set; } = null!;
        public virtual Campaignaccount? SenderAccount { get; set; }
    }
}
