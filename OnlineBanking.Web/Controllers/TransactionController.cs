using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            return View();
            
        }
        [HttpPost]
        public ActionResult Transfer(string fromAccount,
                             string toAccount,
                             decimal amount,
                             string transactionMode)
        {

            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/transaction/transfer?fromAccount={fromAccount}&toAccount={toAccount}&amount={amount}&transactionMode={transactionMode}",
                null
            ).Result;

            if (response.IsSuccessStatusCode)
            {
                ViewBag.FromAccount = fromAccount;
                ViewBag.ToAccount = toAccount;
                ViewBag.Amount = amount;
                ViewBag.Mode = transactionMode;

                return View("Receipt");
            }

            ViewBag.Result = response.Content.ReadAsStringAsync().Result;

            return View("Index");
        }

        public ActionResult TransferToPayee(string accountNo)
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            ViewBag.PayeeAccount = accountNo;

            return View();
        }
    }
}