using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.Models;
using Thread = PRN221_VotingSystem.Models.Thread;

namespace PRN221_VotingSystem.DAL
{
    public class DashBoardDAO
    {
        private static DashBoardDAO instance = null;
        private static readonly object instanceLock = new object();
        private DashBoardDAO() { }
        public static DashBoardDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new DashBoardDAO();
                    }
                    return instance;
                }
            }
        }

        public DateTime? GetStartTimeById(int id)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var campaign = dbContext.Campaigns.Find(id);
                return campaign?.StartTime;
            }
        }

        public DateTime? GetEndTimeById(int id)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var campaign = dbContext.Campaigns.Find(id);
                return campaign?.EndTime;
            }
        }

        public int CountVoterInCampaign(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Campaignaccounts.Count(ca => ca.CampaignId == campaignId);
            }
        }

        // Phương thức để đếm số lượng bỏ phiếu trong một chiến dịch dựa trên ID
        public int CountVotedBallot(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Campaignaccounts.Count(ca => ca.CampaignId == campaignId && ca.VotingResult != null);
            }
        }

        public int CountThreadInCampaign(int campaignId )
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Threads.Count(t => t.CampaignId == campaignId);
            }
        }

        //public int CountCommentInThread(int threadId )
        //{
        //    using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
        //    {
        //        return dbContext.Comments.Count(c => c.ThreadId == threadId);
        //    }
        // }

        public List<Thread> GetThreadsInCampaign(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Threads.Where(t => t.CampaignId == campaignId).ToList();
            }

        }

        // Đếm số lượng comment trong một thread
        public int CountCommentInThread(int threadId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Comments.Count(c => c.ThreadId == threadId);
            }
        }

        public bool CheckCampaignPassword(int campaignId, string password)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var campaign = dbContext.Campaigns.Find(campaignId);

                // Kiểm tra xem mật khẩu đã cung cấp có đúng không
                return campaign != null && campaign.Password == password;
            }
        }

    }
}
