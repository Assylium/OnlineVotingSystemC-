using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;

namespace PRN221_VotingSystem.Pages
{
    public class CampaignPasswordModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(int campaignId, string password)
        {
            // Kiểm tra mật khẩu
            if (CampaignDAO.Instance.CheckCampaignPassword(campaignId, password))
            {
                // Nếu mật khẩu đúng, lưu mật khẩu vào session và chuyển hướng đến trang chỉnh sửa chiến dịch
                HttpContext.Session.SetString("CampaignPassword", password);
                return RedirectToPage("/EditCampaign", new { id = campaignId });
            }
            else
            {
                // Nếu mật khẩu không đúng, hiển thị thông báo lỗi
                ModelState.AddModelError("", "Incorrect password");
                return Page();
            }
        } 
        }
}
