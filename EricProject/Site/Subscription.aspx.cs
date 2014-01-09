using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using BAL;
using System.Data.SqlClient;
using BusinessObject;
using EricProject.LiveCreditCard;
using System.Configuration;
using System.IO;
using Encryption64;
namespace EricProject.Site
{
    public partial class Subscription : System.Web.UI.Page
    {
        _Merchant objMarchant = new _Merchant();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        string PublicKey = "";
        public string EmailContent = "";
        System.Threading.Thread threadSendMails;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["RenewSubscription"] = null;
            Session["IsSubscriptionEnd"] = null;
            if (Session["MerchantId"] == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");

        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Session["IsSubscriptionEnd"] = "Yes";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
        }

        protected void btncontinue_Click(object sender, EventArgs e)
        {
            DAL.Admin sqlobj = new DAL.Admin();

            //Check Merchant Credit Card Details Exist By ID
            _Merchant objMerchant = new _Merchant();
            objMerchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drCheckMerchantCreditCardDetailsExistByID = sqlobj.CheckMerchantCreditCardDetailsExistByID(objMerchant);
            while (drCheckMerchantCreditCardDetailsExistByID.Read())
            {
                if (drCheckMerchantCreditCardDetailsExistByID["CreditDetailsExists"].ToString() == "0")
                {
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AutoSubscription");
                }
            }

            //Check merchant credit card expired or not
            SqlDataReader drGetCreditCardExpired = sqlobj.GetCreditCardExpired();
            while (drGetCreditCardExpired.Read())
            {
                if (Convert.ToInt32(drGetCreditCardExpired["Merchant_Id"].ToString()) == Convert.ToInt32(Session["MerchantId"].ToString()))
                {
                    Session["IsSubscriptionEnd"] = "Yes";
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/CardDetails");
                }
            }

            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            obj.status = 1;
          
            sqlobj.update_Merchant_Subscription(obj);

            //Send mail when merchant all campaigns are Active with Susbcription payment made
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drMerchant = sqlobj.GetMerchantDetailsById(obj);
            while (drMerchant.Read())
            {
                obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader drCheckCampainsActiveInactiveByMerchantID = sqlobj.CheckCampainsActiveInactiveByMerchantID(obj);
                while (drCheckCampainsActiveInactiveByMerchantID.Read())
                {
                    if (drCheckCampainsActiveInactiveByMerchantID["Exists"].ToString() == "1")
                    {
                        //Take $9.99 from merchant account
                        bool transactionStatus = CreditCard();
                        if (transactionStatus)
                        {
                            //Update merchant_scheduler_check for successfull subscription
                            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                            sqlobj.UpdateMerchantSuccessfulSubscriptionDateByID(obj);

                            //ExtendMerchantPaidPeriodByMerchantIDOneMonth
                            ExtendMerchantPaidPeriodByMerchantIDOneMonth();

                            if (drMerchant["Paid_period_expiry_date"].ToString() == null || drMerchant["Paid_period_expiry_date"].ToString() == "" || Convert.ToDateTime(drMerchant["Paid_period_expiry_date"].ToString()).Year == 1900)
                            {
                                //Send mail when merchant all campaigns are Active with Susbcription payment made

                                StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Susbcription_Payment_Made.htm"));
                                EmailContent = SR.ReadToEnd();
                                EmailContent = EmailContent.Replace("{DATE1}", DateTime.Now.ToString("dd MMM yyyy")).Replace("{DATE11}", DateTime.Now.AddMonths(1).ToString("dd MMM yyyy"));
                                EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                                SR.Close();
                                EmailContent = GetEmailHeaderFooter(drMerchant["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                                comman.SendMail(drMerchant["Email_Id"].ToString(), "Susbcription payment made", EmailContent);
                            }
                            else
                            {
                                //Calculate referral time for referring other
                                obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                                SqlDataReader drGetTotalReferralsByMerchantID = sqlobj.GetTotalReferralsByMerchantID(obj);
                                string ReferralTime = "";
                                while (drGetTotalReferralsByMerchantID.Read())
                                {

                                    if (drGetTotalReferralsByMerchantID["Referrals"].ToString() == "0")
                                    {
                                        ReferralTime = "1 year";
                                    }
                                    else if (drGetTotalReferralsByMerchantID["Referrals"].ToString() == "1")
                                    {
                                        ReferralTime = "3 years";
                                    }
                                    else if (drGetTotalReferralsByMerchantID["Referrals"].ToString() == "2")
                                    {
                                        ReferralTime = "life time";
                                    }

                                }
                                //Send mail when merchant all campaigns are Active with Social Review Receipt
                                StreamReader SR;
                                if (ReferralTime == "")
                                {
                                    SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Social_Review_Reciept_LifeTime.htm"));
                                    EmailContent = SR.ReadToEnd();
                                    EmailContent = EmailContent.Replace("{DATE1}", DateTime.Now.ToString("dd MMM yyyy")).Replace("{DATE11}", DateTime.Now.AddMonths(1).ToString("dd MMM yyyy"));
                                }
                                else
                                {
                                    SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Social_Review_Reciept.htm"));
                                    EmailContent = SR.ReadToEnd();
                                    EmailContent = EmailContent.Replace("{DATE1}", DateTime.Now.ToString("dd MMM yyyy")).Replace("{DATE11}", DateTime.Now.AddMonths(1).ToString("dd MMM yyyy")).Replace("{XX year(s)}", ReferralTime);
                                    EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                                }
                                SR.Close();
                                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                                EmailContent = GetEmailHeaderFooter(drMerchant["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                                comman.SendMail(drMerchant["Email_Id"].ToString(), "Your " + ConfigurationManager.AppSettings["site_name"].ToString() + " Receipt", EmailContent);
                            }
                        }
                        else
                        {
                            //Send mail when merchant all campaigns are Active
                          
                            StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Subscription_Payment_Failed.htm"));
                            EmailContent = SR.ReadToEnd();
                            EmailContent = EmailContent.Replace("{DATE1}", DateTime.Now.ToString("MMM dd, yyyy"));
                            EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                            SR.Close();
                            EmailContent = GetEmailHeaderFooter(drMerchant["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                            comman.SendMail(drMerchant["Email_Id"].ToString(), "Subscription Payment Failed", EmailContent);

                            DAL.Transaction sqlTransaction = new DAL.Transaction();
                            _Transaction objTransaction1 = new _Transaction();
                            objTransaction1.MerchantId = Convert.ToInt32(Session["MerchantId"].ToString());
                            SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                            SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                            if (dr.Read())
                            {
                                if (drdetails.Read())
                                {
                                    StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/SupportMail.html"));
                                    EmailContent = SRSupport.ReadToEnd();
                                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", "9.99".ToString());
                                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToDecimal("9.99") * 100).ToString());
                                    EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                                    EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                                    EmailContent = EmailContent.Replace("{merchant_id}", (Session["MerchantId"].ToString()).ToString());
                                    EmailContent = EmailContent.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                                    EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                                    SRSupport.Close();
                                    EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                                    threadSendMails = new System.Threading.Thread(delegate()
                                    {
                                        //comman.SendMail("tanu_garg@seologistics.com", "Subscription payment failed", EmailContent);
                                        comman.SendMail("admin@tapiton.com", "Subscription payment failed", EmailContent);
                                    });
                                    threadSendMails.IsBackground = true;
                                    threadSendMails.Start();
                                }
                            }

                            //Send mail when merchant all campaigns are Active
                            SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Account_Hold_Subscription_Failure.htm"));
                            EmailContent = SR.ReadToEnd();
                            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                            EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                            SR.Close();
                            EmailContent = GetEmailHeaderFooter(drMerchant["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                            comman.SendMail(drMerchant["Email_Id"].ToString(), "Account put on hold due to subscription payment failure", EmailContent);

                            //Deactivate Merchant Account Status
                            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                            sqlobj.DeactivateMerchantAccountStatus(obj);

                            //Update Merchant Subscription Fails Date By MerchantID for Merchant_Scheduler_checks
                             obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                            sqlobj.UpdateMerchantSubscriptionFailsDateByID(obj);

                            //update_Merchant_Subscription to 0
                            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                            obj.status = 0;
                            sqlobj.update_Merchant_Subscription(obj);
                            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/PaymentSuccess/Failed");
                        }
                    }
                    else
                    {
                        //Send mail when merchant all campaigns are not Active
                        StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Free_Period_Ended.htm"));
                        EmailContent = SR.ReadToEnd();
                        SR.Close();
                        EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                        EmailContent = GetEmailHeaderFooter(drMerchant["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                        comman.SendMail(drMerchant["Email_Id"].ToString(), "Your free period has ended", EmailContent);

                        //Deactivate Merchant Account Status
                        objMerchant.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                        sqlobj.DeactivateMerchantAccountStatus(objMerchant);
                    }
                }
            }

            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
        }
        public void ExtendMerchantPaidPeriodByMerchantIDOneMonth()
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            DAL.Plugin objplugin = new DAL.Plugin();
            objplugin.ExtendMerchantPaidPeriodByMerchantID(obj);
        }
        public bool CreditCard()
        {
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString()); ;
            Credit_card objCredit = new Credit_card();
            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);
            ServiceSoapClient ws = new ServiceSoapClient();
            if (DRCredits.Read())
            {
                EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
                txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
                txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
                txn.Transaction_Type = "00";
                txn.Card_Number = "";
                txn.CardHoldersName = DRCredits["Cardholder_Name"].ToString();
                txn.DollarAmount = "9.99";
                txn.Expiry_Date = DRCredits["Expiry_Date"].ToString();
                txn.User_Name = "";
                txn.Secure_AuthResult = "";
                txn.Ecommerce_Flag = "";
                txn.XID = "";
                txn.CardType = "";
                txn.CAVV = "";
                txn.CAVV_Algorithm = "";
                txn.Reference_No = "";
                txn.Customer_Ref = "";
                txn.Reference_3 = "";
                txn.Client_IP = "";					                    //This value is only used for fraud investigation.
                txn.Client_Email = "saurabh_tyagi@seologistics.com";		//This value is only used for fraud investigation.
                txn.Language = "en";			//English="en" French="fr"
                txn.Track1 = "";
                txn.Track2 = "";
                txn.Authorization_Num = "";
                txn.Transaction_Tag = "";
                txn.VerificationStr1 = "";
                txn.VerificationStr2 = "123";
                txn.CVD_Presence_Ind = "";
                txn.Secure_AuthRequired = "";               
                txn.CardType = DRCredits["Card_Type"].ToString();
                txn.TransarmorToken = DRCredits["TransarmorToken"].ToString();

                TransactionResult result = ws.SendAndCommit(txn);
                if (result.Bank_Message == "Approved")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
            overlay.Style["display"] = "none";
            progressdiv.Style["display"] = "none";
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + HttpContext.Current.Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }

        protected void lbtnModifyCardDetails_Click(object sender, EventArgs e)
        {
            Session["IsSubscriptionEnd"] = "Yes";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/CardDetails");
        }
    }
}