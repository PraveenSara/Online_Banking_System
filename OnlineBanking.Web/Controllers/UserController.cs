using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(
            string accountNumber,
            string username,
            string password,
            string transactionPassword)
        {
            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/user/register" +
                $"?accountNumber={accountNumber}" +
                $"&username={username}" +
                $"&password={password}" +
                $"&transactionPassword={transactionPassword}",
                null
            ).Result;

            ViewBag.Result =
                response.Content.ReadAsStringAsync().Result;

            return View("Register");
        }
    }
}