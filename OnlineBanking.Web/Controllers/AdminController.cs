using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/admin/login?username={username}&password={password}",
                null
            ).Result;

            if (response.IsSuccessStatusCode)
            {
                Session["Admin"] = username;

                return RedirectToAction("PendingRequests");
            }

            ViewBag.Error = "Invalid Login";

            return View();
        }

        public ActionResult PendingRequests()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");

            HttpClient client = new HttpClient();

            var response = client.GetAsync(
                "https://localhost:44317/api/admin/pendingrequests"
            ).Result;

            var json = response.Content.ReadAsStringAsync().Result;

            var data =
                JsonConvert.DeserializeObject<List<dynamic>>(json);

            return View(data);
        }

        public ActionResult Approve(int requestId)
        {
            HttpClient client = new HttpClient();

            client.PostAsync(
                $"https://localhost:44317/api/admin/approve?requestId={requestId}&adminId=1",
                null
            ).Wait();

            return RedirectToAction("PendingRequests");
        }

        public ActionResult Reject(int requestId)
        {
            HttpClient client = new HttpClient();

            client.PostAsync(
                $"https://localhost:44317/api/admin/reject?requestId={requestId}&adminId=1&reason=Rejected",
                null
            ).Wait();

            return RedirectToAction("PendingRequests");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");
        }
    }
}