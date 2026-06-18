using OnlineBanking.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineBanking.API.Controllers
{
    [RoutePrefix("api/beneficiary")]
    public class BeneficiaryController : ApiController
    {
        private readonly BeneficiaryService service;

        public BeneficiaryController()
        {
            service = new BeneficiaryService();
        }

        // ADD BENEFICIARY
        [HttpPost]
        [Route("add")]
        public IHttpActionResult Add(int customerId, string name, string accountNo, string nickname)
        {
            var result = service.AddBeneficiary(customerId, name, accountNo, nickname);

            if (result != "Beneficiary Added Successfully")
                return BadRequest(result);

            return Ok(result);
        }

        // GET ALL BENEFICIARIES
        [HttpGet]
        [Route("list/{customerId}")]
        public IHttpActionResult List(int customerId)
        {
            return Ok(service.GetBeneficiaries(customerId));
        }

        // DELETE BENEFICIARY
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var result = service.DeleteBeneficiary(id);

            if (result != "Deleted Successfully")
                return BadRequest(result);

            return Ok(result);
        }

    }
}
