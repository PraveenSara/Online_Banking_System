using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.API.Services
{
    public class TransactionService
    {
        private readonly MyOnlineBankingDBEntities db;

        public TransactionService()
        {
            db = new MyOnlineBankingDBEntities();
        }

        // TRANSFER MONEY
        public string TransferMoney(string fromAccount, string toAccount, decimal amount, string transactionMode)
        {
            var sender = db.Accounts.FirstOrDefault(a => a.AccountNumber == fromAccount);
            var receiver = db.Accounts.FirstOrDefault(a => a.AccountNumber == toAccount);

            if (sender == null || receiver == null)
                return "Invalid Account Number";

            if (sender.Balance < amount)
                return "Insufficient Balance";

            // Debit sender
            sender.Balance -= amount;

            // Credit receiver
            receiver.Balance += amount;

            // Create transaction record
            Transaction txn = new Transaction
            {
                TransactionReferenceNo = GenerateRef(),
                FromAccountNo = fromAccount,
                ToAccountNo = toAccount,
                Amount = amount,
                TransactionMode = transactionMode,
                TransactionDate = DateTime.Now,
                Remarks = "Fund Transfer"
            };

            db.Transactions.Add(txn);

            db.SaveChanges();

            return "Transfer Successful";
        }

        // ACCOUNT STATEMENT
        public List<Transaction> GetStatement(string accountNo,
                                      DateTime fromDate,
                                      DateTime toDate)
        {
            return db.Transactions
                     .Where(t =>
                         (t.FromAccountNo == accountNo ||
                          t.ToAccountNo == accountNo)
                         &&
                         t.TransactionDate >= fromDate &&
                         t.TransactionDate <= toDate)
                     .OrderByDescending(t => t.TransactionDate)
                     .ToList();
        }

        // TRANSACTION HISTORY (LATEST 10)
        public List<Transaction> GetRecentTransactions(string accountNo)
        {
            return db.Transactions
                     .Where(t => t.FromAccountNo == accountNo || t.ToAccountNo == accountNo)
                     .OrderByDescending(t => t.TransactionDate)
                     .Take(10)
                     .ToList();
        }

        private string GenerateRef()
        {
            return "TXN" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }
}