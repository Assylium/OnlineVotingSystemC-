using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRN221_VotingSystem.Models;

namespace PRN221_VotingSystem.DAL
{
    public class VoteDAO
    {
        private static VoteDAO instance = null;
        private static readonly object instanceLock = new object();
        private VoteDAO() { }
        public static VoteDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new VoteDAO();
                    }
                    return instance;
                }
            }
        }

        public int GetNoOfVotes(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                return dbContext.Votes.Count(ca => ca.CampaignId == campaignId);
            }
        }

        public Dictionary<string, int> GetLeadingOption(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var leadingObject = dbContext.Votes
                    .Where(v => v.CampaignId == campaignId)
                    .GroupBy(v => v.VotingObjectId)
                    .Select(g => new
                    {
                        ObjectId = g.Key,
                        TotalPoints = g.Sum(v => v.Point)
                    })
                    .OrderByDescending(g => g.TotalPoints)
                    .FirstOrDefault();

                if (leadingObject != null)
                {
                    var votingObject = dbContext.Votingobjects
                        .Where(vo => vo.VotingObjectId == leadingObject.ObjectId)
                        .FirstOrDefault();

                    if (votingObject != null)
                    {
                        var leadingOption = new Dictionary<string, int>();
                        leadingOption.Add(votingObject.VotingObjectName, leadingObject.TotalPoints);
                        return leadingOption;
                    }
                }

                return null;
            }
        }

        public DateTime? GetStartTimeById(int id)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var campaign = dbContext.Campaigns.Find(id);
                return campaign?.StartTime;
            }
        }

        public DateTime? GetEndTimeById(int id)
        {
            using (var dbContext = new PRN221_DBContext()) // Thay YourDbContext bằng đối tượng DbContext thực tế của bạn
            {
                var campaign = dbContext.Campaigns.Find(id);
                return campaign?.EndTime;
            }
        }

        public bool GetCampaignStatus(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext())
            {
                var campaign = dbContext.Campaigns.FirstOrDefault(c => c.CampaignId == campaignId);
                if (campaign != null)
                {
                    return campaign.Status.ToLower() == "closed";
                }
                return false; // Trả về false nếu không tìm thấy chiến dịch
            }
        }

        public Dictionary<string, int> GetCurrentVote(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext())
            {
                var currentVote = new Dictionary<string, int>();

                // Lấy danh sách đối tượng bầu cử trong chiến dịch
                var votingObjects = dbContext.Votingobjects
                    .Where(vo => vo.CampaignId == campaignId)
                    .ToList();

                // Đếm số phiếu bầu cho mỗi đối tượng
                foreach (var votingObject in votingObjects)
                {
                    var voteCount = dbContext.Votes
                        .Count(v => v.CampaignId == campaignId && v.VotingObjectId == votingObject.VotingObjectId);

                    currentVote.Add(votingObject.VotingObjectName, voteCount);
                }

                return currentVote;
            }
        }

        public Dictionary<string, int> GetCurrentResult(int campaignId)
        {
            using (var dbContext = new PRN221_DBContext())
            {
                var currentResult = new Dictionary<string, int>();

                // Lấy danh sách đối tượng bầu cử trong chiến dịch
                var votingObjects = dbContext.Votingobjects
                    .Where(vo => vo.CampaignId == campaignId)
                    .ToList();

                // Tính tổng số điểm cho mỗi đối tượng
                foreach (var votingObject in votingObjects)
                {
                    var totalPoints = dbContext.Votes
                        .Where(v => v.CampaignId == campaignId && v.VotingObjectId == votingObject.VotingObjectId)
                        .Sum(v => v.Point);

                    currentResult.Add(votingObject.VotingObjectName, totalPoints);
                }

                return currentResult;
            }
        }









    }
}
