using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OnlineBanking.Web.Controllers
{
    public class AccountRequestController : Controller
    {
        // GET: AccountRequest
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Submit(
            string FullName,
            string Email,
            string Mobile,
            string Dob,
            string AadharNo,
            string Occupation,
            string Address)
        {
            //if (string.IsNullOrWhiteSpace(FullName))
            //{
            //    ViewBag.Error = "Full Name is required";
            //    return View("Index");
            //}

            //if (Mobile.Length != 10)
            //{
            //    ViewBag.Error = "Mobile Number must be 10 digits";
            //    return View("Index");
            //}

            //if (AadharNo.Length != 12)
            //{
            //    ViewBag.Error = "Aadhar Number must be 12 digits";
            //    return View("Index");
            //}


            HttpClient client = new HttpClient();

            string json = @"{
                ""FullName"":""" + FullName + @""",
                ""Email"":""" + Email + @""",
                ""Mobile"":""" + Mobile + @""",
                ""Dob"":""" + Dob + @""",
                ""AadharNo"":""" + AadharNo + @""",
                ""Occupation"":""" + Occupation + @""",
                ""Address"":""" + Address + @"""
            }";

            StringContent content =
                new StringContent(json, Encoding.UTF8, "application/json");

            var response = client.PostAsync(
                "https://localhost:44317/api/accountrequest/create",
                content
            ).Result;

            var responceJson = response.Content.ReadAsStringAsync().Result;

            dynamic data = Newtonsoft.Json.Linq.JObject.Parse(responceJson);

            ViewBag.Result = "Account Request Submitted Successfully";
            ViewBag.ReferenceNo = data.ServiceReferenceNo.ToString();

            return View("Index");
        }
    }
}