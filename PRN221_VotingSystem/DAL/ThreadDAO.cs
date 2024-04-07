
using PRN221_VotingSystem.Models;
using Thread = PRN221_VotingSystem.Models.Thread;

namespace PRN221_VotingSystem.DAL
{
    public class ThreadDAO
    {
        private static ThreadDAO instance = null;
        private static readonly object instanceLock = new object();
        private ThreadDAO() { }

        public static ThreadDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ThreadDAO();
                    }
                    return instance;
                }
            }
        }

        // Thêm một thread mới vào cơ sở dữ liệu
        public void AddThread(Thread thread)
        {
            try
            {
                using var context = new PRN221_DBContext();
                context.Threads.Add(thread);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add thread", ex);
            }
        }

        // Lấy danh sách tất cả các thread từ cơ sở dữ liệu
        public List<Thread> GetAllThreads()
        {
            try
            {
                using var context = new PRN221_DBContext();
                return context.Threads.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve threads", ex);
            }
        }

        // Lấy một thread từ cơ sở dữ liệu theo ID
        public Thread GetThreadById(int threadId)
        {
            try
            {
                using var context = new PRN221_DBContext();
                return context.Threads.Find(threadId);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve thread", ex);
            }
        }

        // Chỉnh sửa thông tin của một thread trong cơ sở dữ liệu
        public void EditThread(Thread updatedThread)
        {
            try
            {
                using var context = new PRN221_DBContext();
                var existingThread = context.Threads.Find(updatedThread.ThreadId);
                if (existingThread == null)
                {
                    throw new InvalidOperationException("Thread not found");
                }

                existingThread.Title = updatedThread.Title;
                existingThread.Content = updatedThread.Content;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to edit thread", ex);
            }
        }

        // Xóa một thread từ cơ sở dữ liệu
        public void DeleteThread(int threadId)
        {
            try
            {
                using var context = new PRN221_DBContext();
                var threadToDelete = context.Threads.Find(threadId);
                if (threadToDelete == null)
                {
                    throw new InvalidOperationException("Thread not found");
                }

                context.Threads.Remove(threadToDelete);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete thread", ex);
            }
        }
    }
}
