using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBanking.Web.Models
{
    public class BeneficiaryViewModel
    {
        public int BeneficiaryId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAccountNo { get; set; }
        public string NickName { get; set; }
    }
}