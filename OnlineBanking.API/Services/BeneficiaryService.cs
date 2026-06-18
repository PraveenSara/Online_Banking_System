using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class BeneficiaryService
    {
        private readonly MyOnlineBankingDBEntities db;

        public BeneficiaryService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // ADD BENEFICIARY
        public string AddBeneficiary(int customerId, string name, string accountNo, string nickname)
        {
            var existingAccount = db.Accounts.FirstOrDefault(a => a.AccountNumber == accountNo);

            if (existingAccount == null)
                return "Invalid Beneficiary Account";

            var alreadyExists = db.Beneficiaries.FirstOrDefault(b =>
                b.CustomerId == customerId &&
                b.BeneficiaryAccountNo == accountNo);

            if (alreadyExists != null)
                return "Beneficiary Already Exists";

            Beneficiary ben = new Beneficiary
            {
                CustomerId = customerId,
                BeneficiaryName = name,
                BeneficiaryAccountNo = accountNo,
                NickName = nickname,
                AddedDate = DateTime.Now
            };

            db.Beneficiaries.Add(ben);
            db.SaveChanges();

            return "Beneficiary Added Successfully";
        }

        // GET ALL BENEFICIARIES
        public object GetBeneficiaries(int customerId)
        {
            return db.Beneficiaries
             .Where(b => b.CustomerId == customerId)
             .Select(b => new
             {
                 b.BeneficiaryId,
                 b.BeneficiaryName,
                 b.BeneficiaryAccountNo,
                 b.NickName,
                 b.AddedDate
             })
             .ToList();
        }

        // DELETE BENEFICIARY
        public string DeleteBeneficiary(int beneficiaryId)
        {
            var ben = db.Beneficiaries.FirstOrDefault(b => b.BeneficiaryId == beneficiaryId);

            if (ben == null)
                return "Not Found";

            db.Beneficiaries.Remove(ben);
            db.SaveChanges();

            return "Deleted Successfully";
        }
    }
}