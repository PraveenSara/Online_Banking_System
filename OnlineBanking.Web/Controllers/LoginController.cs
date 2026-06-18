using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace OnlineBanking.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/user/login?username={username}&password={password}", null
            ).Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;

                dynamic data = JObject.Parse(json);

                Session["Username"] = data.UserName.ToString();
                Session["CustomerId"] = (int)data.CustomerId;
                Session["AccountNumber"] = data.AccountNumber.ToString();

                return RedirectToAction("Index", "Dashboard");
                //return Content(json);
            }

            ViewBag.Error = "Invalid Login";
            return View("Index");
        }
    }
}