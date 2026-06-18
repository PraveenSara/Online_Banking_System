using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class UserService
    {
        private readonly MyOnlineBankingDBEntities db;

        public UserService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        

        // REGISTER NET BANKING
        public bool Register(string accountNumber, string username, string loginPassword, string transactionPassword)
        {
            // check account exists
            var account = db.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
                return false;

            // check already registered
            var existing = db.UserLogins.FirstOrDefault(u => u.UserName == username);

            if (existing != null)
                return false;

            UserLogin user = new UserLogin
            {
                UserName = username,
                CustomerId = account.CustomerId,
                LoginPasswordHash = loginPassword, 
                TransactionPasswordHash = transactionPassword,
                FailedAttempts = 0,
                Status = "Active",
                LastLogin = null
            };

            db.UserLogins.Add(user);
            db.SaveChanges();

            return true;
        }

        // LOGIN
        public object Login(string username, string password)
        {
            var user = db.UserLogins.FirstOrDefault(u =>
                u.UserName == username &&
                u.LoginPasswordHash == password);

            if (user == null)
                return null;

            user.LastLogin = DateTime.Now;
            user.FailedAttempts = 0;

            db.SaveChanges();

            var account = db.Accounts
                    .FirstOrDefault(a => a.CustomerId == user.CustomerId);

            return new
            {
                UserName = user.UserName,
                CustomerId = user.CustomerId,
                AccountNumber = account.AccountNumber
            };
        }

        // GET USER INFO
        public UserLogin GetUser(string username)
        {
            return db.UserLogins.FirstOrDefault(u => u.UserName == username);
        }
    }
}