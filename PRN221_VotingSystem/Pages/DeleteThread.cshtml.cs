using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;

namespace PRN221_VotingSystem.Pages
{
    public class DeleteThreadModel : PageModel
    {
        private readonly ThreadDAO _threadDAO;

        public DeleteThreadModel(ThreadDAO threadDAO)
        {
            _threadDAO = threadDAO;
        }

        // Định nghĩa thuộc tính để lưu trữ ID của thread cần xóa
        [BindProperty(SupportsGet = true)]
        public int ThreadId { get; set; }

        // Phương thức xử lý khi nhận yêu cầu GET
        public IActionResult OnGet()
        {
            // Kiểm tra nếu không có ID của thread được cung cấp
            if (ThreadId == 0)
            {
                return NotFound();
            }

            // Lấy thông tin thread từ cơ sở dữ liệu bằng ID
            var thread = _threadDAO.GetThreadById(ThreadId);

            // Kiểm tra xem thread có tồn tại không
            if (thread == null)
            {
                return NotFound();
            }

            // Chuyển dữ liệu thread tới view để hiển thị thông tin trên giao diện
            ViewData["ThreadTitle"] = thread.Title;
            ViewData["ThreadDescription"] = thread.Comments;

            return Page();
        }

        // Phương thức xử lý khi nhận yêu cầu POST (xóa thread)
        public IActionResult OnPost()
        {
            try
            {
                // Gọi phương thức xóa thread từ DAO
                _threadDAO.DeleteThread(ThreadId);

                // Chuyển hướng người dùng về trang danh sách thread sau khi xóa thành công
                return RedirectToPage("/ReviewCampaign");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                TempData["ErrorMessage"] = "Failed to delete thread: " + ex.Message;
                return RedirectToPage("/Error");
            }
        }
    }
}
    