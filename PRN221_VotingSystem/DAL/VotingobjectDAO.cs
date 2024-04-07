using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.DAL
{
    public class VotingobjectDAO
    {
        private static VotingobjectDAO instance = null;
        private static readonly object instanceLock = new object();
        private VotingobjectDAO() { }
        public static VotingobjectDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VotingobjectDAO();
                    }
                    return instance;
                }
            }
        }
        public void AddVotingObject(Votingobject votingobject)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    data.Votingobjects.Add(votingobject);
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        public List<Votingobject> GetVotingobjects(int campaignid)
        {
            List<Votingobject> votelist = new List<Votingobject>();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    votelist = data.Votingobjects.Where(x => x.CampaignId == campaignid).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return votelist;
            }
        }

        public void AddQuestion(int id, string question)
        {
            lock (instanceLock)
            {
                try
                {
                    var votingobjects = new Votingobject();
                    using var data = new PRN221_DBContext();
                    votingobjects = data.Votingobjects.FirstOrDefault(x => x.CampaignId == id);
                    votingobjects.Question = question;
                    data.Votingobjects.Update(votingobjects);
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
