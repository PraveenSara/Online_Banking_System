using OnlineBanking.Data;
using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class AccountRequestService
    {
        private MyOnlineBankingDBEntities db;

        public AccountRequestService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // Create New Account Request
        public string CreateRequest(AccountRequest request)
        {
            request.ServiceReferenceNo = GenerateReferenceNumber();
            request.Status = "Pending";
            request.RequestDate = DateTime.Now;

            db.AccountRequest.Add(request);
            db.SaveChanges();

            return request.ServiceReferenceNo;
        }

        // Track Status using Service Reference Number
        public string GetStatus(string serviceReferenceNo)
        {
            var request = db.AccountRequest
                            .FirstOrDefault(x =>
                                x.ServiceReferenceNo == serviceReferenceNo);

            if (request == null)
                return "Reference Number Not Found";

            return request.Status;
        }

        // Get Complete Request Details
        public AccountRequest GetRequestByReferenceNo(string serviceReferenceNo)
        {
            return db.AccountRequest
                     .FirstOrDefault(x =>
                         x.ServiceReferenceNo == serviceReferenceNo);
        }

        // Get All Requests (Useful for Admin)
        public List<AccountRequest> GetAllRequests()
        {
            return db.AccountRequest
                     .OrderByDescending(x => x.RequestDate)
                     .ToList();
        }

        // Get Pending Requests
        public List<AccountRequest> GetPendingRequests()
        {
            return db.AccountRequest
                     .Where(x => x.Status == "Pending")
                     .OrderByDescending(x => x.RequestDate)
                     .ToList();
        }

        // Generate Service Reference Number
        private string GenerateReferenceNumber()
        {
            return "SRN" +
                   DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}