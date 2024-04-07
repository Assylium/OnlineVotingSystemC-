using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.DAL
{
    public class CampaignDAO
    {
        private static CampaignDAO instance = null;
        private static readonly object instanceLock = new object();
        private CampaignDAO() { }
        public static CampaignDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CampaignDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Campaign> SearchCampaigns(string name)
        {
            List<Campaign> campaign = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    campaign = data.Campaigns.Where(x => x.CampaignName.ToLower().Contains(name)).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return campaign;
            }
        }
        public List<Campaign> GetAllCampaigns()
        {
            List<Campaign> campaign = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    campaign = data.Campaigns.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return campaign;
            }
        }

        public List<Campaign> GetHostCampaigns(int id)
        {
            List<Campaign> campaign = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    campaign = data.Campaigns.Where(x => x.CreatedBy == id && x.IsAccepted != 0).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return campaign;
            }
        }
        public List<Campaign> GetUnHostCampaigns(int id)
        {
            List<Campaign> campaign = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    campaign = data.Campaigns.Where(x => x.CreatedBy != id && x.IsAccepted != 0).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return campaign;
            }
        }


        public Campaign GetCampaignById(int id)
        {
            Campaign campaign = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    campaign = data.Campaigns.FirstOrDefault(x => x.CampaignId == id);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return campaign;
            }
        }
        public void AddCampaign(Campaign campaign)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    data.Campaigns.Add(campaign);
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void EditCampaign(Campaign updatedCampaign)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();

                    // Tìm chiến dịch cần chỉnh sửa trong cơ sở dữ liệu
                    var existingCampaign = data.Campaigns.Find(updatedCampaign.CampaignId);

                    if (existingCampaign == null)
                    {
                        throw new InvalidOperationException("Campaign not found");
                    }

                    // Cập nhật thông tin chiến dịch từ dữ liệu mới
                    existingCampaign.CampaignName = updatedCampaign.CampaignName;
                    existingCampaign.CampaignDescription = updatedCampaign.CampaignDescription;
                    existingCampaign.StartTime = updatedCampaign.StartTime;
                    existingCampaign.EndTime = updatedCampaign.EndTime;
                    existingCampaign.IsPublic = updatedCampaign.IsPublic;
                    existingCampaign.Password = updatedCampaign.Password;
                    existingCampaign.IsAccepted = 0;
                    // Lưu thay đổi vào cơ sở dữ liệu
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to edit campaign", ex);
                }
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
