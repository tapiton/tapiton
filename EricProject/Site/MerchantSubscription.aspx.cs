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
using EricProject.CreditCardDemo1;
using System.Configuration;
using System.IO;
using Encryption64;
namespace EricProject.Site
{
    public partial class MerchantSubscription : System.Web.UI.Page
    {
        _Merchant objMarchant = new _Merchant();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        string PublicKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["IsSubscriptionEnd"] = null;
            if (Session["MerchantId"] == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "site/home");

        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Session["IsSubscriptionEnd"] = "Yes";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
        }

        protected void btncontinue_Click(object sender, EventArgs e)
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            obj.status = 1;
            DAL.Admin sqlobj = new DAL.Admin();
            sqlobj.update_Merchant_Subscription(obj);
            CreditCard();
            ExtendMerchantPaidPeriodByMerchantIDOneMonth();

            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
        }
        public void ExtendMerchantPaidPeriodByMerchantIDOneMonth()
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            obj.Paid_period_expiry_date = DateTime.Now.AddMonths(1);
            DAL.Plugin objplugin = new DAL.Plugin();
            objplugin.ExtendMerchantPaidPeriodByMerchantID(obj);
        }
        public void CreditCard()
        {
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString()); ;
            Credit_card objCredit = new Credit_card();
            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);
            ServiceSoapClient ws = new ServiceSoapClient();
            if (DRCredits.Read())
            {
                try
                {
                    EricProject.CreditCardDemo1.Transaction txn = new EricProject.CreditCardDemo1.Transaction();
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
                    txn.Currency = "$";
                    txn.CardType = DRCredits["Card_Type"].ToString();
                    txn.TransarmorToken = DRCredits["TransarmorToken"].ToString();

                    TransactionResult result = ws.SendAndCommit(txn);
                    if (result.Bank_Message == "Approved")
                    {
                        DAL.Admin sqlAdmin = new DAL.Admin();
                        _Merchant objMarchant = new _Merchant();
                        objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                        objMarchant.status = 1;
                        sqlAdmin.update_Merchant_Subscription(objMarchant);
                        comman.SendMail("tanu_garg@seologistics.com", "Subscription made successfully", "Merchant ID : " + Session["MerchantId"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    comman.SendMail("tanu_garg@seologistics.com", "Error in subscription on", ex.Message);
                }
            }
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