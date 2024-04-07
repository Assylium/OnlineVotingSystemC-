using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.DAL
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Category> GetAllCategories()
        {
            List<Category> categories = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    categories = data.Categories.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return categories;
            }
        }
    }
}
