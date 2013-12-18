using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class _Transaction
    {
        public int TransactionId { get; set; }
        public int MerchantId { get; set; }
        public int TransactionReferenceId { get; set; }
        public int CreditPlanId { get; set; }
        public string PaymentAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionStatus { get; set; }
        public string RemainingCredits { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int CustomerId { get; set; }
        public int CreditTransactionId { get; set; }
        public int Status { get; set; }
        public int Top { get; set; }
        public int Credit_Card_ID { get; set; }
        public int offer_Id { get; set; }
    }

    public class _Customer_Transaction
    {
        public int Customer_Transaction_id { get; set; }
        public int Credit_Transaction_id { get; set; }
        public decimal  Unredeemed_Credits_Remaining { get; set; }
        public decimal Total_redeemed_Credits { get; set; }
    }
    public class _Merchant_Transaction
    {
        public int Merchant_Transaction_Id { get; set; }
        public int Credit_Transaction_Id { get; set; }
        public int Credit_Plan_Id { get; set; }
        public decimal Credits_Remaining { get; set; }
        public int Credit_Card_ID { get; set;}
    }
    public class _Credit_Transaction
    {
        public int Credit_transaction_id { get; set; }
        public DateTime Date { get; set; }
        public int Transaction_id { get; set; }
        public int Customer_id { get; set; }
        public int Merchant_id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Amount_redeemed { get; set; }
        public DateTime Vesting_Date { get; set; }
        public DateTime Date_Added { get; set; }
        public DateTime Date_Updated { get; set; }
        public int CustomerTransactionID { get; set; }
        public Boolean IS_Purchase { get; set; }
        public int Offer_ID { get; set; }
    }

    public class _Declined_Credits
    {
        public int Decline_Details_Id { get; set; }
        public int Transaction_Id { get; set; }
        public decimal Decline_Credits { get; set; }
        public string Reason { get; set; }
        public DateTime Added_On { get; set; }
        public decimal Decline_Credit_Transaction_Id { get; set; }
    }

}
