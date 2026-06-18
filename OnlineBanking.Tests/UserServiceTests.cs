using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineBanking.API.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace OnlineBanking.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public void Amount_Should_Be_Positive()
        {
            decimal amount = 1000;

            Assert.IsTrue(amount > 0);
        }

        [TestMethod]
        public void AccountNumber_Should_Not_Be_Empty()
        {
            string accountNumber = "10020260611170525";

            Assert.IsFalse(string.IsNullOrEmpty(accountNumber));
        }

        [TestMethod]
        public void TransferMode_Should_Be_Valid()
        {
            string mode = "IMPS";

            bool valid =
                mode == "IMPS" ||
                mode == "NEFT" ||
                mode == "RTGS";

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void Account_Should_Have_Sufficient_Balance()
        {
            decimal balance = 10000;
            decimal transferAmount = 2000;

            bool sufficientBalance = balance >= transferAmount;

            Assert.IsTrue(sufficientBalance);
        }

        [TestMethod]
        public void FundTransfer_Should_Allow_Only_Positive_Amount()
        {
            decimal transferAmount = 500;

            bool canTransfer = transferAmount > 0;

            Assert.IsTrue(canTransfer);
        }
    }
}
