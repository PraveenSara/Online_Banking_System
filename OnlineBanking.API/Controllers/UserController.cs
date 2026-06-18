using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly UserService service;

        public UserController()
        {
            service = new UserService();
        }

        // REGISTER
        [HttpPost]
        [Route("register")]
        public IHttpActionResult Register(string accountNumber, string username, string password, string transactionPassword)
        {
            bool result = service.Register(accountNumber, username, password, transactionPassword);

            if (!result)
                return BadRequest("Registration Failed (Check Account or Username)");

            return Ok("User Registered Successfully");
        }

        // LOGIN
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(string username, string password)
        {
            var result = service.Login(username, password);

            if (result == null)
                return BadRequest("Invalid Credentials");

            return Ok(result);
        }

        // GET USER
        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult GetUser(string username)
        {
            var user = service.GetUser(username);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
