using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class AdminService
    {
        private readonly MyOnlineBankingDBEntities db;

        public AdminService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // Admin Login
        public bool AdminLogin(string username, string password)
        {
            var admin = db.Admins
                          .FirstOrDefault(a =>
                              a.UserName == username &&
                              a.PasswordHash == password);

            return admin != null;
        }

        // Get All Pending Requests
        public List<AccountRequest> GetPendingRequests()
        {
            return db.AccountRequest
                     .Where(r => r.Status == "Pending")
                     .OrderByDescending(r => r.RequestDate)
                     .ToList();
        }

        // Approve Account Request
        public bool ApproveRequest(int requestId, int adminId)
        {
            var request = db.AccountRequest
                            .FirstOrDefault(r => r.RequestId == requestId);

            if (request == null)
                return false;

            if (request.Status != "Pending")
                return false;

            // Update Request Status
            request.Status = "Approved";
            request.ReviewedBy = adminId;
            request.ReviewedDate = DateTime.Now;

            // Create Customer
            Customer customer = new Customer
            {
                FullName = request.FullName,
                Email = request.Email,
                Mobile = request.Mobile,
                Dob = request.Dob,
                AadharNo = request.AadharNo,
                Occupation = request.Occupation,
                Address = request.Address,
                CreatedDate = DateTime.Now
            };

            db.Customers.Add(customer);
            db.SaveChanges();

            // Create Account
            Account account = new Account
            {
                AccountNumber = GenerateAccountNumber(),
                CustomerId = customer.CustomerId,
                AccountType = "Savings",
                Balance = 10000,
                AccountStatus = "Active",
                CreatedDate = DateTime.Now
            };

            db.Accounts.Add(account);
            db.SaveChanges();

            db.SaveChanges();

            return true;
        }

        // Reject Account Request
        public bool RejectRequest(int requestId, int adminId, string reason)
        {
            var request = db.AccountRequest
                            .FirstOrDefault(r => r.RequestId == requestId);

            if (request == null)
                return false;

            if (request.Status != "Pending")
                return false;

            request.Status = "Rejected";
            request.ReviewedBy = adminId;
            request.ReviewedDate = DateTime.Now;
            request.RejectionReason = reason;

            db.SaveChanges();

            return true;
        }

        // Get Request By Id
        public AccountRequest GetRequestById(int requestId)
        {
            return db.AccountRequest
                     .FirstOrDefault(r => r.RequestId == requestId);
        }

        // Generate Account Number
        private string GenerateAccountNumber()
        {
            return "100" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}