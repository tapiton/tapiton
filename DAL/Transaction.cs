using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAL;
using System.Data.SqlClient;

namespace DAL
{
    public class Transaction
    {
        public int TransactionID;
        public int CustomerID;
        public int MerchantID;
        public string CustomerEmailID;
        public decimal CreditDetails;
        public int Campaigns_Id;
        public int Credit_Transactionnn_ID;
        public int InsertInToMerchant_Transaction(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.CreditTransactionId;
            values[1] = obj.CreditPlanId;
            values[2] = obj.Credit_Card_ID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToMerchant_Transaction", values);
            while (dr.Read())
                TransactionID = Convert.ToInt32(dr[0]);
            return TransactionID;
        }
        public void UpdateMerchantRefundCredits(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantId;
            values[1] = obj.RemainingCredits;
            int dr = sqlobj.ExecuteSqlHelper("UpdateMerchantRefundCredits", values);       
           
        }
        //public int MerchantTransactionDetails(_Transaction obj)
        //{
        //    var sqlobj = DBAccess.InstanceCreation();
        //    object[] values = new object[8];
        //    values[0] = obj.TransactionId;
        //    values[1] = obj.MerchantId;
        //    values[2] = obj.TransactionReferenceId;
        //    values[3] = obj.CreditPlanId;
        //    values[4] = obj.PaymentAmount;
        //    values[5] = obj.TransactionType;
        //    values[6] = obj.RemainingCredits;
        //    values[7] = obj.TransactionStatus;
        //    SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("MerchantTransactionDetails", values);
        //    while (dr.Read())
        //        TransactionID = Convert.ToInt32(dr[0]);
        //    return TransactionID;
        //}
        public SqlDataReader BindTransactionHistoryByMerchantId(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[5];
            values[0] = obj.MerchantId;
            values[1] = obj.DateFrom;
            values[2] = obj.DateTo;
            values[3] = obj.Top;
            values[4] = obj.Status;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTransactionHistoryByMerchantId", values);
            return DR;
        }

        public SqlDataReader CheckTransactionHistoryPendingByMerchantId(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("CheckTransactionHistoryPendingByMerchantId", values);
            return DR;
        }
        public SqlDataReader GetTotalCredits(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetTotalCredits", values);
            return DR;
        }
        public SqlDataReader GetDetails(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.MerchantId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("GetDetails", values);
            return DR;
        }
        public SqlDataReader Transaction_Details_Total(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.TransactionId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Transaction_Details_Total", values);
            return DR;
        }
        public SqlDataReader Referral_details(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.TransactionId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Referral_details", values);
            return DR;
        }
        public SqlDataReader Referral_details_Merchant(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.TransactionId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Referral_details_Merchant", values);
            return DR;
        }
        public SqlDataReader Bind_CustomerTransactionDetails(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.TransactionId;
            values[1] = obj.CustomerId;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("Bind_CustomerTransactionDetails", values);
            return DR;
        }
        //Bind TotalTransactionByMerchantId
        public SqlDataReader BindTotalTransactionByMerchantId(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[4];
            values[0] = obj.MerchantId;
            values[1] = obj.DateFrom;
            values[2] = obj.DateTo;
            values[3] = obj.Status;
           // values[4] = obj.Top;
            SqlDataReader DR = sqlobj.ExecuteSqlHelperDR("BindTotalTransactionByMerchantId", values);
            return DR;
        }
        //Bind TotalTransactionByMerchantId

        public void UpdateMerchantTransactionStatusByTransactionReferenceId(_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.TransactionReferenceId;
            values[1] = obj.TransactionStatus;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantTransactionStatusByTransactionReferenceId", values);
        }
        // Insert into credit Transaction
        public int InsertIntoCredit_Transaction(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[8];
            values[0] = obj.Transaction_id;
            values[1] = obj.Customer_id;
            values[2] = obj.Merchant_id;
            values[3] = obj.Amount;
            values[4] = obj.Type;
            values[5] = obj.Status;
            values[6] = obj.Amount_redeemed;
            values[7] = obj.IS_Purchase;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCredits_Transaction", values);
            while (dr.Read())
                Credit_Transactionnn_ID = Convert.ToInt32(dr[0]);
            return Credit_Transactionnn_ID;
        }
        public int InsertInToCredits_TransactionRefer(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[8];
            values[0] = obj.Transaction_id;
            values[1] = obj.Customer_id;
            values[2] = obj.Merchant_id;
            values[3] = obj.Amount;
            values[4] = obj.Type;
            values[5] = obj.Status;
            values[6] = obj.Amount_redeemed;
            values[7] = obj.IS_Purchase;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertInToCredits_TransactionRefer", values);
            while (dr.Read())
                Credit_Transactionnn_ID = Convert.ToInt32(dr[0]);
            return Credit_Transactionnn_ID;
        }
        //End Of insert into credit Transaction

        // Insert into Merchant Transaction
        public int InsertIntoMerchant_Transaction(_Merchant_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Credit_Transaction_Id;
            values[1] = obj.Credit_Plan_Id;
            values[2] = obj.Credit_Card_ID;
            int i = sqlobj.ExecuteSqlHelper("InsertInToMerchant_Transaction", values);
            return i;
        }

        //End Of insert into Merchant Transaction

        // Insert into Customer Transaction
        public int InsertIntoCustomer_Transaction(_Customer_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Credit_Transaction_id;
            values[1] = obj.Unredeemed_Credits_Remaining;
            values[2] = obj.Total_redeemed_Credits;
            int i = sqlobj.ExecuteSqlHelper("InsetIntoCustomer_Transaction", values);
            return i;
        }

        //End Of insert into Customer Transaction

        public void UpdateCustomerTransactionStatusByTransactionId(_Customer_Transaction obj)
        {
            //var sqlobj = DBAccess.InstanceCreation();
            //object[] values = new object[3];
            //values[0] = obj.ReferrarTransactionID;
            //values[1] = obj.CustomerTransactionID;
            //values[2] = obj.CustomerCreditStatus;
            //sqlobj.ExecuteSqlHelperDR("UpdateCustomerTransactionStatusByTransactionId", values);
        }
        //UpdateCreditTransactionStatusByCustomerTransactionId
        public void UpdateCreditTransactionStatusByCustomerTransactionId(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Transaction_id;
            values[1] = obj.Status;
            sqlobj.ExecuteSqlHelperDR("UpdateCreditTransactionStatusByCustomerTransactionId", values);
        }
        //UpdateCreditTransactionStatusByCustomerTransactionId

        // UpdateCreditTransactionStatusByMerchantCustomerTransactionId
        public void UpdateCreditTransactionStatusByMerchantCustomerTransactionId(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_id;
            values[1] = obj.CustomerTransactionID;
            sqlobj.ExecuteSqlHelperDR("UpdateCreditTransactionStatusByMerchantCustomerTransactionId", values);
        }
        //UpdateCreditTransactionStatusByMerchantCustomerTransactionId

        //InsertIntoDeclineDetails
        public void InsertIntoDeclineDetails(_Declined_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.Transaction_Id;
            values[1] = obj.Decline_Credit_Transaction_Id;
            values[2] = obj.Reason;
            sqlobj.ExecuteSqlHelperDR("InsertIntoDeclineDetails", values);
        }


        // UpdateMerchantCreditsDeclineByMerchantId
        public void UpdateMerchantCreditsDeclineByMerchantId(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_id;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantCreditsDeclineByMerchantId", values);
        }

        // UpdateMerchantCreditsDeclineByMerchantId
        public void UpdateMerchantCreditsDeclineByMerchantId(_Merchant_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.MerchantID;
            values[1] = obj.CreditDecline;
            sqlobj.ExecuteSqlHelperDR("UpdateMerchantCreditsDeclineByMerchantId", values);
        }

        // UpdateCustomerCreditsByTransactionIdDecline
        public void UpdateCustomerCreditsByTransactionIdDecline(_Customer_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[3];
            values[0] = obj.TransactionId;
            values[1] = obj.CustomerCreditDecline;
            values[2] = obj.ReferralCreditDecline;
            sqlobj.ExecuteSqlHelperDR("UpdateCustomerCreditsByTransactionIdDecline", values);
        }

        // UpdateCreditTransactionStatusDeclineByTransactionId
        public void UpdateCreditTransactionStatusDeclineByTransactionId(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_id;
            sqlobj.ExecuteSqlHelperDR("UpdateCreditTransactionStatusDeclineByTransactionId", values);
        }

        // BindTransactionDelinedReasonByTransactionId
        public SqlDataReader BindTransactionDelinedReasonByTransactionId(_Declined_Credits obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_Id;
            SqlDataReader dr=sqlobj.ExecuteSqlHelperDR("BindTransactionDelinedReasonByTransactionId", values);
            return dr;
        }

        // Insert into credit Transaction 
        public int InsertIntoCreditTransactionDeclineByTransactionId(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[2];
            values[0] = obj.Merchant_id;
            values[1] = obj.Amount;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("InsertIntoCreditTransactionDeclineByTransactionId", values);
            while (dr.Read())
                Credit_Transactionnn_ID = Convert.ToInt32(dr[0]);
            return Credit_Transactionnn_ID;
        }
        public SqlDataReader  GetPurchaseDetails(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_id;           
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetPurchaseDetails", values);            
            return dr;
        }
        public SqlDataReader GetRedeemDetails(_Credit_Transaction obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Transaction_id;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("GetRedeemDetails", values);
            return dr;
        }
        public SqlDataReader CheckrefundAmount(_credit_details obj)
        {
            var sqlobj = DBAccess.InstanceCreation();
            object[] values = new object[1];
            values[0] = obj.Merchant_ID;
            SqlDataReader dr = sqlobj.ExecuteSqlHelperDR("CheckrefundAmount", values);
            return dr;
        }
    }
}
