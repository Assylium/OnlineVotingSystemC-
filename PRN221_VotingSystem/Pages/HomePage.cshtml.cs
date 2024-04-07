using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Collections.Generic;

namespace PRN221_VotingSystem.Pages
{
    public class HomePageModel : PageModel
    {
        [BindProperty]
        public string Search { get; set; }
        public List<Campaign> HostCampaigns { get; private set; }
        public List<Campaign> UnHostCampaigns { get; private set; }
        public List<Campaign> SearchCampaigns { get; private set; }
        public List<Campaign> PinnedCampaigns { get; private set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("account") == null)
            {
                TempData["ErrorMessage"] = "You must be logged in to access the HomePage page.";
                return RedirectToPage("/Logins/Login");
            }
            else
            {
                var serializedAccount = HttpContext.Session.GetString("account");
                var account = JsonSerializer.Deserialize<Account>(serializedAccount);
                var userid = account.UserId;
                HostCampaigns = CampaignDAO.Instance.GetHostCampaigns(userid);
                UnHostCampaigns = CampaignDAO.Instance.GetUnHostCampaigns(userid);

                // Lấy danh sách các chiến dịch đã được ghim cho người dùng hiện tại
                PinnedCampaigns = PinDAO.Instance.GetPinnedCampaigns(userid);
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                SearchCampaigns = CampaignDAO.Instance.SearchCampaigns(Search.ToLower().Trim());
            }
            else
            {
                // Replace the hardcoded user ID with the actual user ID
                var serializedAccount = HttpContext.Session.GetString("account");
                var account = JsonSerializer.Deserialize<Account>(serializedAccount);
                var userid = account.UserId;
                HostCampaigns = CampaignDAO.Instance.GetHostCampaigns(userid);
                UnHostCampaigns = CampaignDAO.Instance.GetUnHostCampaigns(userid);
            }
            return Page();
        }




        public IActionResult OnPostPinCampaign(int campaignId)
{
    var serializedAccount = HttpContext.Session.GetString("account");

    // Kiểm tra xem có dữ liệu tài khoản không
    if (!string.IsNullOrEmpty(serializedAccount))
    {
        try
        {
            var account = JsonSerializer.Deserialize<Account>(serializedAccount);
            var userId = account.UserId;

            // Kiểm tra xem chiến dịch đã được ghim hay chưa
            if (PinDAO.Instance.IsCampaignPinned(userId, campaignId))
            {
                // Nếu đã ghim, thực hiện unpin
                PinDAO.Instance.UnpinCampaign(userId, campaignId);
            }
            else
            {
                // Nếu chưa ghim, thực hiện pin
                PinDAO.Instance.PinCampaign(userId, campaignId);
            }

            return RedirectToPage();
        }
        catch (JsonException ex)
        {
            return Content("Error: " + ex.Message);
        }
    }
    else
    {
        // Xử lý trường hợp không có thông tin tài khoản trong session
        // Trả về một trang lỗi hoặc thực hiện các xử lý khác theo nhu cầu
        return Content("Error: No account information found in session.");
    }
}

    }


     
    
}
