using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_VotingSystem.DAL;
using PRN221_VotingSystem.Models;
using System.Text.Json; // Import thư viện System.Text.Json

namespace PRN221_VotingSystem.Pages
{
    public class DashBoardModel : PageModel
    {
        private readonly DashBoardDAO _dashboardDAO;

        public DashBoardModel()
        {
            _dashboardDAO = DashBoardDAO.Instance;
        }

        [BindProperty]
        public Campaign Campaign { get; set; }
        // Khai báo các thuộc tính tương ứng với dữ liệu bạn muốn hiển thị trên trang Razor
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Voter { get; set; }
        public int VotedBallot { get; set; }
        public int Threads { get; set; }
        public int Comments { get; set; }

        public IActionResult OnGet(int id)
        {
            StartTime = _dashboardDAO.GetStartTimeById(id);
            EndTime = _dashboardDAO.GetEndTimeById(id);
            Voter = _dashboardDAO.CountVoterInCampaign(id);
            VotedBallot = _dashboardDAO.CountVotedBallot(id);
            Threads = _dashboardDAO.CountThreadInCampaign(id);
            // Khai báo một biến để lưu số lượng comment
            int totalComments = 0;

            // Lấy danh sách thread trong chiến dịch
            var threads = _dashboardDAO.GetThreadsInCampaign(id);

            // Duyệt qua mỗi thread để đếm số lượng comment của mỗi thread
            foreach (var thread in threads)
            {
                totalComments += _dashboardDAO.CountCommentInThread(thread.ThreadId);
            }

            // Gán số lượng comment đã đếm được vào thuộc tính Comments
            Comments = totalComments;
            // Tạo đối tượng chứa dữ liệu JSON
            var jsonData = new
            {
                StartTime,
                EndTime,
                Voter,
                VotedBallot,
                Threads,
                Comments
            };

            // Chuyển đổi đối tượng JSON sang chuỗi JSON
            var jsonString = JsonSerializer.Serialize(jsonData);

            // Gửi dữ liệu JSON sang trang Razor
            ViewData["JsonData"] = jsonString;

            return Page();
        }
    }
}
