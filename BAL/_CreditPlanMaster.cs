using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class _CreditPlanMaster
    {
        public int CreditPlanId { get; set; }
        public decimal PaymentAmount { get; set; }
        public int RemainingCredits { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public Boolean IsActive { get; set; }
        public int PaymentCredits { get; set; }
        public int Merchant_ID { get; set; }
    }

}
