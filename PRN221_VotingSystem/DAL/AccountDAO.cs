using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN221_VotingSystem.Models;
using System.Security.Principal;
using System.Text.Json.Nodes;

namespace PRN221_VotingSystem.DAL
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private static readonly object instanceLock = new object();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new AccountDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Account> GetAll()
        {
            List<Account> accounts = new();
            lock (instanceLock)
            {
                List<Account> list = new();
                try
                {
                    using var data = new PRN221_DBContext();
                    list = data.Accounts.ToList();

                    foreach (var account in list)
                    {
                        accounts.Add(account);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return accounts;
            }
        }
        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new();
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    accounts = data.Accounts.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return accounts;
            }
        }

        public void AddUser(Account account)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    data.Accounts.Add(account);
                    data.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Account GetByEmail(string email)
        {
            Account account = null;
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    account = data.Accounts.FirstOrDefault(a => a.Email == email);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                return account;
            }
        }

        public void UpdateAccount(Account updatedAccount)
        {
            lock (instanceLock)
            {
                try
                {
                    using var data = new PRN221_DBContext();
                    var existingAccount = data.Accounts.FirstOrDefault(a => a.UserId == updatedAccount.UserId);

                    if (existingAccount != null)
                    {
                        data.Entry(existingAccount).State = EntityState.Detached;
                        data.Accounts.Update(updatedAccount);
                        data.Entry(updatedAccount).State = EntityState.Modified;
                        data.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
