using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/password")]
    public class PasswordController : ApiController
    {
        private readonly PasswordService service;

        public PasswordController()
        {
            service = new PasswordService();
        }

        // CHANGE LOGIN PASSWORD
        [HttpPost]
        [Route("change-login")]
        public IHttpActionResult ChangeLogin(string username, string oldPassword, string newPassword)
        {
            var result = service.ChangeLoginPassword(username, oldPassword, newPassword);

            if (result.Contains("Invalid"))
                return BadRequest(result);

            return Ok(result);
        }

        // CHANGE TRANSACTION PASSWORD
        [HttpPost]
        [Route("change-transaction")]
        public IHttpActionResult ChangeTransaction(string username, string oldPassword, string newPassword)
        {
            var result = service.ChangeTransactionPassword(username, oldPassword, newPassword);

            if (result.Contains("Invalid"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        [Route("api/password/forgot-password")]
        public IHttpActionResult ForgotPassword(string username, string newPassword)
        {
            bool result =
                service.ForgotPassword(username, newPassword);

            if (result)
                return Ok("Password Reset Successfully");

            return BadRequest("User Not Found");
        }
    }
}
