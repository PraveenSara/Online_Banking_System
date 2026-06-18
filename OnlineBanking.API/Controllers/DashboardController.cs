using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        private readonly DashboardService service;

        public DashboardController()
        {
            service = new DashboardService();
        }

        // GET: api/dashboard/{username}
        [HttpGet]
        [Route("{username}")]
        public IHttpActionResult GetDashboard(string username)
        {
            var result = service.GetDashboard(username);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // GET: api/dashboard/balance/{username}
        [HttpGet]
        [Route("balance/{username}")]
        public IHttpActionResult GetBalance(string username)
        {
            var balance = service.GetBalance(username);

            if (balance == null)
                return NotFound();

            return Ok(balance);
        }

        [HttpGet]
        [Route("profile/{username}")]
        public IHttpActionResult GetProfile(string username)
        {
            var result = service.GetProfile(username);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("accountsummary/{username}")]
        public IHttpActionResult AccountSummary(string username)
        {
            var result = service.GetDashboard(username);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("statement/{username}")]
        public IHttpActionResult Statement(string username)
        {
            return Ok(service.GetStatement(username));
        }
    }
}
