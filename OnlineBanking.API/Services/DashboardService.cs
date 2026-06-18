using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class DashboardService
    {
        private readonly MyOnlineBankingDBEntities db;

        public DashboardService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // Get Account Summary by username
        public object GetDashboard(string username)
        {
            var user = db.UserLogins
                         .FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            var customer = db.Customers
                              .FirstOrDefault(c => c.CustomerId == user.CustomerId);

            var account = db.Accounts
                             .FirstOrDefault(a => a.CustomerId == user.CustomerId);

            if (customer == null || account == null)
                return null;

            return new
            {
                CustomerName = customer.FullName,
                AccountNumber = account.AccountNumber,
                AccountType = account.AccountType,
                Balance = account.Balance,
                AccountStatus = account.AccountStatus
            };
        }

        // Get Balance only
        public decimal? GetBalance(string username)
        {
            var user = db.UserLogins.FirstOrDefault(u => u.UserName == username);

            if (user == null)
                return null;

            var account = db.Accounts.FirstOrDefault(a => a.CustomerId == user.CustomerId);

            return account?.Balance;
        }

        public object GetProfile(string username)
        {
            var user = db.UserLogins.FirstOrDefault(u => u.UserName == username);

            if (user == null) return null;

            var customer = db.Customers.FirstOrDefault(c => c.CustomerId == user.CustomerId);

            return new
            {
                customer.FullName,
                customer.Email,
                customer.Mobile,
                customer.AadharNo,
                customer.Dob,
                customer.Address,
                customer.Occupation
            };
        }

        public object GetStatement(string username)
        {
            var user = db.UserLogins
                         .FirstOrDefault(x => x.UserName == username);

            if (user == null)
                return null;

            var account = db.Accounts
                            .FirstOrDefault(x => x.CustomerId == user.CustomerId);

            if (account == null)
                return null;

            return db.Transactions
                     .Where(t =>
                         t.FromAccountNo == account.AccountNumber ||
                         t.ToAccountNo == account.AccountNumber)
                     .OrderByDescending(t => t.TransactionDate)
                     .Select(t => new
                     {
                         t.TransactionReferenceNo,
                         t.FromAccountNo,
                         t.ToAccountNo,
                         t.Amount,
                         t.TransactionDate
                     })
                     .ToList();
        }
    }
}