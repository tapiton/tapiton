using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
using Encryption64;

public partial class Site_PaymentSuccess : System.Web.UI.Page
{
    DAL.Transaction sqlTransaction = new DAL.Transaction();
    int PlanCredit = 0;
    string PlanAmont = "";
    int TotalAvailableCredit = 0;
    string PublicKey = "";
    public string status = "";
    public Site_PaymentSuccess()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["TokenNumber"] != null)
        {
            if (Page.RouteData.Values["pid"].ToString() == "Failed")
            {
                status = "Failed";
                Session["Failed"] = "Failed";
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Credits");
            }
            else
            {
                status = "Completed";
                //Get other fields from credit plan id
                _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
                DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
                objCreditPlanMaster.CreditPlanId = Convert.ToInt32(Page.RouteData.Values["pid"].ToString());
                SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindCreditPlan(objCreditPlanMaster);
                while (drCreditPlan.Read())
                {
                    PlanCredit = Convert.ToInt32(drCreditPlan["Received_Credits"].ToString());
                    PlanAmont = drCreditPlan["Payment_Amount"].ToString();
                }
                //Add credit to merchant first
                _Merchant_Credits objMerchantCredits1 = new _Merchant_Credits();
                DAL.Plugin sqlMerchantCredits1 = new DAL.Plugin();
                objMerchantCredits1.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                objMerchantCredits1.AvailableCredit = PlanCredit;
                objMerchantCredits1.PendingCredit = 0;
                objMerchantCredits1.MonthlyFeeApplicable = false;
                try
                {
                    sqlMerchantCredits1.InsertIntoMerchant_Credits(objMerchantCredits1);
                }
                catch { }
                //Get total available credit from merchant credit after adding credits
                _Merchant_Credits objMerchantCredits2 = new _Merchant_Credits();
                DAL.Plugin sqlMerchantCredits2 = new DAL.Plugin();
                objMerchantCredits2.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                SqlDataReader drMerchantCredits2 = sqlMerchantCredits2.BindTotalAvailableMerchantCreditByMerchantId(objMerchantCredits2);
                try
                {
                    if (drMerchantCredits2.Read())
                    {
                        TotalAvailableCredit = Convert.ToInt32(drMerchantCredits2["TotalAvailableCredit"].ToString());
                    }

                }
                catch { }
                //Pending Credit
                _Transaction objTransaction1 = new _Transaction();
                objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantID"].ToString());
                SqlDataReader dr = sqlTransaction.CheckTransactionHistoryPendingByMerchantId(objTransaction1);
                int Credits = 0;
                int Temp = 0;
                while (dr.Read())
                {
                    Temp = TotalAvailableCredit;
                    Credits = Convert.ToInt32(dr["CREDITS"].ToString());
                    if (Credits < Temp)
                    {
                        Temp = Temp - Credits;
                        //Update credit transaction table status
                        _Credit_Transaction obj = new _Credit_Transaction();
                        obj.Merchant_id = Convert.ToInt32(Session["MerchantID"].ToString());
                        obj.CustomerTransactionID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());
                        try
                        {
                            sqlTransaction.UpdateCreditTransactionStatusByMerchantCustomerTransactionId(obj);
                        }
                        catch { }
                        //Update Merchant Credits for pending Credits
                        _Merchant_Credits objMerchantCredits = new _Merchant_Credits();
                        DAL.Plugin sqlMerchantCredits = new DAL.Plugin();
                        objMerchantCredits.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
                        //string Credits_Remaining = dr["Credits_Remaining"].ToString().Replace('-',' ').Trim();
                        //decimal DCredits_Remaining = decimal.Parse(Credits_Remaining);
                        //int iCredits_Remaining = Convert.ToInt32(DCredits_Remaining);
                        //objMerchantCredits.PendingCredit = iCredits_Remaining;
                        //objMerchantCredits.PurchaseCredit = PlanCredit;
                        objMerchantCredits.PendingCredit = Convert.ToInt32(dr["Credits"].ToString());
                        objMerchantCredits.PurchaseCredit = PlanCredit;
                        try
                        {
                            sqlMerchantCredits.UpdateMerchantCreditsByMerchantId(objMerchantCredits);
                        }
                        catch { }
                        //Update customer credits
                        _credit_details objcredit_details = new _credit_details();
                        DAL.Plugin sqlCreditDetails = new DAL.Plugin();
                        objcredit_details.Referral_ID = Convert.ToInt32(dr["Referrer_Id"].ToString());
                        objcredit_details.Customer_ID = Convert.ToInt32(dr["CUSTOMER_Id"].ToString());
                        objcredit_details.Referral_Credits = Convert.ToInt32(dr["REFERRER_CREDITS"].ToString());
                        objcredit_details.Customer_Credits = Convert.ToInt32(dr["CUSTOMER_CREDITS"].ToString());
                        objcredit_details.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
                        objcredit_details.Customer_Transaction_ID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());
                        try
                        {
                            sqlCreditDetails.UpdateCreditDetailsByReffferCustomerId(objcredit_details);
                        }
                        catch { }
                    }
                }
                //Pending Credit
                //Insert credit transaction table status
                _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                objCredit_Transaction.Transaction_id = 0;
                objCredit_Transaction.Customer_id = 0;
                objCredit_Transaction.Merchant_id = Convert.ToInt32(Session["MerchantID"].ToString());
                objCredit_Transaction.Amount = PlanCredit;
                objCredit_Transaction.Type = "Credit Purchase";
                objCredit_Transaction.Status = "Successful";
                objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                objCredit_Transaction.IS_Purchase = false;

                int result1 = 0;
                try
                {
                    result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                }
                catch { }
                //Insert  merchant transaction table
                _Transaction objTransaction = new _Transaction();
                objTransaction.CreditTransactionId = result1;
                objTransaction.CreditPlanId = Convert.ToInt32(Page.RouteData.Values["pid"].ToString());
                objTransaction.Credit_Card_ID = Convert.ToInt32(Session["creditID"]);
                int result2 = 0;
                try
                {
                    result2 = sqlTransaction.InsertInToMerchant_Transaction(objTransaction);
                    Session["result"] = result2;
                }
                catch { }
                _Merchant objMarchant = new _Merchant();
                DAL.Plugin sqlPlugin = new DAL.Plugin();
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader DrCredits = sqlPlugin.TotalCreditsOfMerchant(objMarchant);
                if (DrCredits.Read())
                {
                    Session["MerchantCredits"] = DrCredits["TotalAvailableCredit"].ToString();
                }
                DBAccess.InstanceCreation().disconnect();
                Session["TokenNumber"] = null;
                litTransactionId.Text = "#" + Session["result"].ToString();
                litAmount.Text = PlanAmont + " USD";
                litDate.Text = DateTime.Now.ToString("d MMM yyyy");
                litComments.Text = "You have purchased " + comman.FormatCredits(PlanCredit) + " credits.";
                string EmailContent = "";
                StreamReader SR = new StreamReader(Server.MapPath("~/EmailTemplate/Merchant/Merchant_Credit_Purchase_Manually.htm"));
                EmailContent = SR.ReadToEnd();
                EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(PlanCredit)).Replace("{AMOUNT}", string.Format("{0:0.00}", Convert.ToDecimal(PlanAmont))).Replace("{AVAILABLECREDITS}", comman.FormatCredits(TotalAvailableCredit));
                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                SR.Close();
                EmailContent = GetEmailHeaderFooter(Session["MerchantEmailId"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                System.Threading.Thread threadSendMails;
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(Session["MerchantEmailId"].ToString(), ConfigurationManager.AppSettings["site_name"].ToString() + " Receipt", EmailContent);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
        }
        else if (Page.RouteData.Values["pid"].ToString() == "Failed")
        {
            status = "Failed";
            Session["Failed"] = "Failed";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Credits");
        }
        else if (Page.RouteData.Values["pid"].ToString() == "CreditPurchase")
        {
            litTransactionId.Text = "#" + Session["Transaction_Reference_Id"].ToString();
            _Credit_Transaction objcredit = new _Credit_Transaction();
            objcredit.Transaction_id = Convert.ToInt32(Session["Transaction_Reference_Id"]);
            SqlDataReader Dr = sqlTransaction.GetPurchaseDetails(objcredit);
            if (Dr.Read())
            {
                litAmount.Text = Dr["Payment_Amount"] + " USD";
                litDate.Text = Convert.ToDateTime(Dr["Added_On"]).ToString("d MMM yyyy");
                if (Dr["TYPE"].ToString() == "Credit Purchase")
                {
                    litComments.Text = "You have purchased " + comman.FormatCredits(Dr["Credit"]) + " credits.";
                    purchasetype.Style["display"] = "block";
                    if (Dr["IS_Purchase"].ToString() == "True")
                        litPurType.Text = "Auto-replenish";
                    else
                        litPurType.Text = "Manual";
                }
                else if (Dr["TYPE"].ToString() == "Refund")
                {
                    litComments.Text = comman.FormatCredits(Dr["Credit"]) + " Credits Refunded.";
                    Creditcarddiv.Style["display"] = "block";
                    litcard.Text = "XXXXXXXXXXXX" + Dr["TransarmorToken"].ToString().Substring(12);
                }
                else if (Dr["TYPE"].ToString() == "Declined")
                    litComments.Text = "You have Declined " + comman.FormatCredits(Dr["Credit"]) + " credits.";

            }
        }
        else Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/ManageCredits");
    }
    public string GetEmailHeaderFooter(string Email)
    {
        //Header Footer Email Code
        StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
        string HeaderFooter = HeaderFooterSR.ReadToEnd();
        HeaderFooterSR.Close();
        string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
        HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
        HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
        HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
        return HeaderFooter;
        //Header Footer Email Code
    }
}

