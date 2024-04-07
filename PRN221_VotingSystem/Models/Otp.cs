using System;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Models
{
    public partial class Otp
    {
        public int OtpId { get; set; }
        public string Otp1 { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime TimeStamp { get; set; }
    }
}
