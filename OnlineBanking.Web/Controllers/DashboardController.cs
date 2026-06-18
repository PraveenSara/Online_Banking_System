using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            string username = Session["Username"].ToString();

            HttpClient client = new HttpClient();

            var response = client.GetAsync(
                "https://localhost:44317/api/dashboard/" + username
            ).Result;

            var data = response.Content.ReadAsStringAsync().Result;

            ViewBag.Data = data;

            return View();
        }

        public ActionResult Profile()   
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            string username = Session["Username"].ToString();

            HttpClient client = new HttpClient();

            var response = client.GetAsync(
                "https://localhost:44317/api/dashboard/profile/" + username
            ).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            dynamic data = JObject.Parse(json);

            ViewBag.FullName = data.FullName;
            ViewBag.Email = data.Email;
            ViewBag.Mobile = data.Mobile;
            ViewBag.AadharNo = data.AadharNo;
            ViewBag.Dob = data.Dob;
            ViewBag.Address = data.Address;
            ViewBag.Occupation = data.Occupation;

            return View();
        }

        public ActionResult AccountSummary()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            string username = Session["Username"].ToString();

            HttpClient client = new HttpClient();

            var response = client.GetAsync(
                "https://localhost:44317/api/dashboard/accountsummary/" + username
            ).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            dynamic data = JObject.Parse(json);

            ViewBag.CustomerName = data.CustomerName;
            ViewBag.AccountNumber = data.AccountNumber;
            ViewBag.AccountType = data.AccountType;
            ViewBag.Balance = data.Balance;
            ViewBag.AccountStatus = data.AccountStatus;

            return View();
        }

        public ActionResult Statement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Statement(DateTime fromDate, DateTime toDate)
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            string username = Session["Username"].ToString();

            // Get account number first
            HttpClient client = new HttpClient();

            var accountResponse = client.GetAsync(
                "https://localhost:44317/api/dashboard/accountsummary/" + username
            ).Result;

            var accountJson = accountResponse.Content.ReadAsStringAsync().Result;

            dynamic accountData = JObject.Parse(accountJson);

            string accountNo = accountData.AccountNumber;

            // Call statement API
            var response = client.GetAsync(
                $"https://localhost:44317/api/transaction/statement?accountNo={accountNo}&fromDate={fromDate:yyyy-MM-dd}&toDate={toDate:yyyy-MM-dd}"
            ).Result;

            var statementJson = response.Content.ReadAsStringAsync().Result;

            ViewBag.Statement = JArray.Parse(statementJson);

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Login");
        }
    }
}