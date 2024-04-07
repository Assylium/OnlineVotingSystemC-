using PRN221_VotingSystem.Models;
using System.Data;

namespace PRN221_VotingSystem.DAL
{
    public class VotingruleDAO
    {
        private static VotingruleDAO instance = null;
        private static readonly object instanceLock = new object();
        private VotingruleDAO() { }
        public static VotingruleDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VotingruleDAO();
                    }
                    return instance;
                }
            }
        }
        public List<Votingrule> GetAllRules()
        {
            List<Votingrule> votingrules = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    votingrules = data.Votingrules.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return votingrules;
            }
        }
        public Votingrule FindById(int id)
        {
            var rule = new Votingrule();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    rule = data.Votingrules.FirstOrDefault(x => x.VotingRuleId == id);
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return rule;
            }
        }
    }
}
