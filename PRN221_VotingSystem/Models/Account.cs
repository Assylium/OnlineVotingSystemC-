using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Account
    {
        public Account()
        {
            Campaigns = new HashSet<Campaign>();
            Pinneds = new HashSet<Pinned>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Salt { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime Dob { get; set; }
        public string Gender { get; set; } = null!;
        public string Job { get; set; } = null!;
        public byte IsMod { get; set; }
        public byte IsAdmin { get; set; }
        public byte IsActive { get; set; }

        public virtual Campaignaccount? Campaignaccount { get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Pinned> Pinneds { get; set; }
    }
}
