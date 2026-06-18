using Newtonsoft.Json;
using OnlineBanking.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class BeneficiaryController : Controller
    {
        // GET: Benificiary
        public ActionResult Index()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public ActionResult Add(int customerId, string name, string accountNo, string nickname)
        {
            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/beneficiary/add?customerId={customerId}&name={name}&accountNo={accountNo}&nickname={nickname}",
                null
            ).Result;

            ViewBag.Result = response.Content.ReadAsStringAsync().Result;

            return View("Index");
        }

        public ActionResult List()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            HttpClient client = new HttpClient();

            int customerId = Convert.ToInt32(Session["CustomerId"]);

            var response = client.GetAsync(
                $"https://localhost:44317/api/beneficiary/list/{customerId}"
            ).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            var data =
                JsonConvert.DeserializeObject<List<BeneficiaryViewModel>>(json);

            return View(data);
            //return Content(json);
        }
    }
}