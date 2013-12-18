using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
   public class _MerchantReferral
    {
       public int Merchant_Referral_ID { get; set; }
       public int Referrer_ID { get; set; }
       public string Name { get; set; }
       public string Email_Address { get; set; }
       public string Website_Url { get; set; }
       public string Message { get; set; }
       public string Status { get; set; }
       public int Referral_Merchant_ID { get; set; }
       public string ReferralType { get; set; }
       public DateTime AddedOn { get; set; }
    }
   public class _MerchantID
   {
       public int Merchant_ID { get; set; }
   }
   public class _MerchantRefID
   {
       public int Merchant_Referral_ID { get; set; }
   }
}
