using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.DAL
{
    public class PinDAO
    {
        private static PinDAO instance = null;
        private static readonly object instanceLock = new object();
        private PinDAO() { }

        public static PinDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new PinDAO();
                    }
                    return instance;
                }
            }
        }


        public void PinCampaign(int userId, int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                // Kiểm tra sự tồn tại của cặp userId và campaignId trong bảng Pinned
                var existingPin = dbContext.Pinneds.FirstOrDefault(p => p.UserId == userId && p.CampaignId == campaignId);

                // Nếu không tìm thấy cặp userId và campaignId trong bảng Pinned, thêm mới vào
                if (existingPin == null)
                {
                    var pin = new Pinned { UserId = userId, CampaignId = campaignId, IsPinned = 1 };
                    dbContext.Pinneds.Add(pin);
                    dbContext.SaveChanges();
                }
            }
        }


        public void UnpinCampaign(int userId, int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Sử dụng dbContext để thực hiện truy vấn
            {
                var pin = dbContext.Pinneds.FirstOrDefault(p => p.UserId == userId && p.CampaignId == campaignId);
                if (pin != null)
                {
                    dbContext.Pinneds.Remove(pin);
                    dbContext.SaveChanges();
                }
            }
        }

        public bool IsCampaignPinned(int userId, int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Sử dụng dbContext để thực hiện truy vấn
            {
                return dbContext.Pinneds.Any(p => p.UserId == userId && p.CampaignId == campaignId);
            }
        }

        public List<Campaign> GetPinnedCampaigns(int userId)
        {
            using (var dbContext = new PRN221_DBContext())
            {
                return dbContext.Campaigns
                    .Where(c => dbContext.Pinneds.Any(p => p.UserId == userId && p.CampaignId == c.CampaignId))
                    .ToList();
            }
        }

    }
}
