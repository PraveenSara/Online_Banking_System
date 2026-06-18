using OnlineBanking.API.Services;
using OnlineBanking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/accountrequest")]
    public class AccountRequestController : ApiController
    {
        private readonly AccountRequestService service;

        public AccountRequestController()
        {
            service = new AccountRequestService();
        }

        // POST: api/accountrequest/create
        [HttpPost]
        [Route("create")]
        public IHttpActionResult Create(AccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Request");
            }

            string referenceNo = service.CreateRequest(request);

            return Ok(new
            {
                Message = "Account Request Submitted Successfully",
                ServiceReferenceNo = referenceNo
            });
        }

        // GET: api/accountrequest/status/SRN123456
        [HttpGet]
        [Route("status/{referenceNo}")]
        public IHttpActionResult GetStatus(string referenceNo)
        {
            var status = service.GetStatus(referenceNo);

            return Ok(status);
        }

        // GET: api/accountrequest/details/SRN123456
        [HttpGet]
        [Route("details/{referenceNo}")]
        public IHttpActionResult GetRequestDetails(string referenceNo)
        {
            var request = service.GetRequestByReferenceNo(referenceNo);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }
    }
}
