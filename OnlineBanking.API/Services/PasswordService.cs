using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class PasswordService
    {
        private readonly MyOnlineBankingDBEntities db;

        public PasswordService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // CHANGE LOGIN PASSWORD
        public string ChangeLoginPassword(string username, string oldPassword, string newPassword)
        {
            var user = db.UserLogins.FirstOrDefault(u =>
                u.UserName == username &&
                u.LoginPasswordHash == oldPassword);

            if (user == null)
                return "Invalid Old Password";

            user.LoginPasswordHash = newPassword;
            db.SaveChanges();

            return "Login Password Changed Successfully";
        }

        // CHANGE TRANSACTION PASSWORD
        public string ChangeTransactionPassword(string username, string oldPassword, string newPassword)
        {
            var user = db.UserLogins.FirstOrDefault(u =>
                u.UserName == username &&
                u.TransactionPasswordHash == oldPassword);

            if (user == null)
                return "Invalid Old Transaction Password";

            user.TransactionPasswordHash = newPassword;
            db.SaveChanges();

            return "Transaction Password Changed Successfully";
        }

        public bool ForgotPassword(string username,string newPassword)
        {
            var user = db.UserLogins
                         .FirstOrDefault(x =>
                             x.UserName == username);

            if (user == null)
                return false;

            user.LoginPasswordHash = newPassword;

            db.SaveChanges();

            return true;
        }

    }
}