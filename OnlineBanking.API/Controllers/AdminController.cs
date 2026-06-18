using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly AdminService service;

        public AdminController()
        {
            service = new AdminService();
        }

        // POST: api/admin/login?username=admin&password=admin123
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(string username, string password)
        {
            bool result = service.AdminLogin(username, password);

            if (!result)
            {
                return BadRequest("Invalid Username or Password");
            }

            return Ok("Login Successful");
        }

        // GET: api/admin/pendingrequests
        [HttpGet]
        [Route("pendingrequests")]
        public IHttpActionResult GetPendingRequests()
        {
            var requests = service.GetPendingRequests();

            return Ok(requests);
        }

        // POST: api/admin/approve?requestId=1&adminId=1
        [HttpPost]
        [Route("approve")]
        public IHttpActionResult ApproveRequest(int requestId, int adminId)
        {
            bool result = service.ApproveRequest(requestId, adminId);

            if (!result)
            {
                return BadRequest("Request Not Found or Already Processed");
            }

            return Ok("Request Approved Successfully");
        }

        // POST: api/admin/reject?requestId=1&adminId=1&reason=Documents Missing
        [HttpPost]
        [Route("reject")]
        public IHttpActionResult RejectRequest(int requestId, int adminId, string reason)
        {
            bool result = service.RejectRequest(requestId, adminId, reason);

            if (!result)
            {
                return BadRequest("Request Not Found or Already Processed");
            }

            return Ok("Request Rejected Successfully");
        }

        // GET: api/admin/request/1
        [HttpGet]
        [Route("request/{requestId}")]
        public IHttpActionResult GetRequestById(int requestId)
        {
            var request = service.GetRequestById(requestId);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
