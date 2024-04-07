using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.Pages
{
    public class EditCampaignModel : PageModel
    {
        
        public EditCampaignModel()
        {
           
            Campaign = new Campaign();
        }

        [BindProperty]
        public Campaign Campaign { get; set; }

        public IActionResult OnGet(int id)
        {
            Campaign = CampaignDAO.Instance.GetCampaignById(id);
            if (Campaign == null)
            {
                return RedirectToPage("/ReviewCampaign");
            }
            // Kiểm tra xem chiến dịch có phải là public và cần mật khẩu không
            if (Campaign.IsPublic == 2)
            {
                // Lấy mật khẩu đã nhập từ session
                var enteredPassword = HttpContext.Session.GetString("CampaignPassword");

                // Kiểm tra xem mật khẩu đã nhập có khớp với mật khẩu của chiến dịch không
                if (enteredPassword != null && enteredPassword == Campaign.Password)
                {
                    // Mật khẩu đúng, cho phép truy cập vào trang
                    return Page();
                }
                else
                {
                    // Mật khẩu không khớp hoặc chưa nhập mật khẩu, chuyển hướng đến trang nhập mật khẩu
                    return RedirectToPage("/CampaignPassword", new { campaignId = id });
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            try
            {
                CampaignDAO.Instance.EditCampaign(Campaign);
                return RedirectToPage("./HomePage");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                ModelState.AddModelError("", "Failed to edit campaign: " + ex.Message);
                return Page();
            }
        }

    }
}
