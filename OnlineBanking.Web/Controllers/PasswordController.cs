using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class PasswordController : Controller
    {
        // GET: Password
        public ActionResult Index()
        {

            if (Session["Username"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public ActionResult ChangeLogin(string username, string oldPassword, string newPassword)
        {
            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/password/change-login?username={username}&oldPassword={oldPassword}&newPassword={newPassword}",
                null
            ).Result;

            ViewBag.Result = response.Content.ReadAsStringAsync().Result;

            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string username, string newPassword)
        {


            HttpClient client = new HttpClient();

            var response = client.PostAsync(
                $"https://localhost:44317/api/password/forgot-password?username={username}&newPassword={newPassword}",
                null
            ).Result;

            ViewBag.Result = response.Content.ReadAsStringAsync().Result;

            return View("ForgotPassword");
        }
    }
}