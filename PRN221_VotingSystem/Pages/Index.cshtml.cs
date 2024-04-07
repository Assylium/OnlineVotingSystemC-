using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PRN221_VotingSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public List<Vote> MyVotes { get; set; }
        public void OnGet()
        {
            // Mô phỏng dữ liệu
            MyVotes = new List<Vote>
        {
            new Vote { Title = "Vote 1", Description = "Description 1" },
            new Vote { Title = "Vote 2", Description = "Description 2" },
            // Thêm các bài vote khác
        };
        }

        public class Vote
        {
            public string Title { get; set; }
            public string Description { get; set; }
            // Thêm các thuộc tính khác của bài vote
        }
    }
}
