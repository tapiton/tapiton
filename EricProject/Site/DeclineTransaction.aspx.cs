using BAL;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class DeclineTransaction : System.Web.UI.Page
    {
        _Transaction objTransaction = new _Transaction();
        DAL.Transaction sqlTransaction = new DAL.Transaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            objTransaction.TransactionId = Convert.ToInt32(Session["Transaction_Reference_Id"]);
            SqlDataReader drReferral = sqlTransaction.Referral_details(objTransaction);
            if (drReferral.Read())
            {
                txtCredits.Value = Convert.ToString(Convert.ToInt32(drReferral["REFERRER_CREDITS"]) + Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]) + Convert.ToInt32(drReferral["transaction_fee"]));
            }
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            objTransaction.TransactionId = Convert.ToInt32(Session["Transaction_Reference_Id"]);
            SqlDataReader drReferral = sqlTransaction.Referral_details(objTransaction);
            if (drReferral.Read())
            {
                if (drReferral["Customer_Credit_Status"].ToString() != "Declined")
                {
                    if (drReferral["Customer_Credit_Status"].ToString().Contains("Pending") || drReferral["Customer_Credit_Status"].ToString().Contains("Pending Vesting"))
                    {
                        try
                        {
                            //Update merchant credit by merchant id
                            _Merchant_Credits objMerchant_Credits = new _Merchant_Credits();
                            objMerchant_Credits.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString().ToString());
                            objMerchant_Credits.CreditDecline = Convert.ToInt32(drReferral["REFERRER_CREDITS"]) + Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]) + Convert.ToInt32(drReferral["transaction_fee"]);
                            sqlTransaction.UpdateMerchantCreditsDeclineByMerchantId(objMerchant_Credits);

                            //Update customer credit by transaction id
                            _Customer_Credits objCustomer_Credits = new _Customer_Credits();
                            objCustomer_Credits.TransactionId = Convert.ToInt32(Session["Transaction_Reference_Id"].ToString());
                            objCustomer_Credits.CustomerCreditDecline = Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]);
                            objCustomer_Credits.ReferralCreditDecline = Convert.ToInt32(drReferral["REFERRER_CREDITS"]);
                            sqlTransaction.UpdateCustomerCreditsByTransactionIdDecline(objCustomer_Credits);

                            //Insert credit transaction table for declined transaction
                            _Credit_Transaction objCreditTransaction = new _Credit_Transaction();
                            objCreditTransaction.Merchant_id = Convert.ToInt32(Session["MerchantID"].ToString());
                            objCreditTransaction.Amount = Convert.ToInt32(drReferral["REFERRER_CREDITS"]) + Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]) + Convert.ToInt32(drReferral["transaction_fee"]);
                            int result1 = 0;
                            result1 = sqlTransaction.InsertIntoCreditTransactionDeclineByTransactionId(objCreditTransaction);

                            //Insert into decline details table
                            _Declined_Credits obj = new _Declined_Credits();
                            obj.Transaction_Id = Convert.ToInt32(Session["Transaction_Reference_Id"].ToString());
                            obj.Decline_Credit_Transaction_Id = result1;
                            obj.Reason = txtReason.Text;
                            sqlTransaction.InsertIntoDeclineDetails(obj);

                            //Insert  merchant transaction table for declined transaction
                            _Merchant_Transaction objMerchantTransaction = new _Merchant_Transaction();
                            objMerchantTransaction.Credit_Transaction_Id = result1;
                            objMerchantTransaction.Credit_Plan_Id = 0;
                            objMerchantTransaction.Credit_Card_ID = 0;
                            int result2 = 0;
                            result2 = sqlTransaction.InsertIntoMerchant_Transaction(objMerchantTransaction);

                            //Insert  customer transaction table for declined transaction
                            _Customer_Transaction objCustomerTransaction = new _Customer_Transaction();
                            objCustomerTransaction.Credit_Transaction_id = result1;
                            objCustomerTransaction.Unredeemed_Credits_Remaining = Convert.ToDecimal("0.00"); 
                            objCustomerTransaction.Total_redeemed_Credits = Convert.ToDecimal("0.00"); 
                            int result3 = 0;
                            result3 = sqlTransaction.InsertIntoCustomer_Transaction(objCustomerTransaction);

                            //Update credit transaction table status
                            _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                            objCredit_Transaction.Transaction_id = Convert.ToInt32(Session["Transaction_Reference_Id"].ToString());
                            sqlTransaction.UpdateCreditTransactionStatusDeclineByTransactionId(objCredit_Transaction); 

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Credits has been declined successfully.');document.location.href='ManageCredits';", true);
                            txtReason.Text = "";
                        }
                        catch (Exception ex)
                        {
                            txtReason.Text = "";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Invalid operation.');", true);
                        }
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('This transaction has already declined.');", true);
                }
            }

        }
    }
}