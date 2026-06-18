using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/transaction")]
    public class TransactionController : ApiController
    {
        private readonly TransactionService service;

        public TransactionController()
        {
            service = new TransactionService();
        }

        // TRANSFER MONEY
        [HttpPost]
        [Route("transfer")]
        public IHttpActionResult Transfer(string fromAccount, string toAccount, decimal amount, string transactionMode)
        {
            var result = service.TransferMoney(fromAccount, toAccount, amount, transactionMode);

            if (result != "Transfer Successful")
                return BadRequest(result);

            return Ok(result);
        }

        // STATEMENT
        [HttpGet]
        [Route("statement")]
        public IHttpActionResult Statement(string accountNo, DateTime fromDate, DateTime toDate)
        {
            var data = service.GetStatement(accountNo, fromDate, toDate);
            return Ok(data);
        }

        // RECENT TRANSACTIONS (FOR DASHBOARD)
        [HttpGet]
        [Route("recent/{accountNo}")]
        public IHttpActionResult Recent(string accountNo)
        {
            var data = service.GetRecentTransactions(accountNo);
            return Ok(data);
        }
    }
}
