using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using BAL;
using DAL;
using System.Web.Script.Serialization;
using BusinessObject;
using System.IO;
using Encryption64;
namespace EricProject.WebServices
{
    /// <summary>
    /// Summary description for MerchantService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class MerchantService : System.Web.Services.WebService
    {
        string PublicKey = "";
        public MerchantService()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        [WebMethod(enableSession: true)]
        public _Merchant GetMerchantDetails()
        {
            _Merchant merchant = new _Merchant();
            merchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drMerchantDetails = sqlPlugin.BindMerchantById(merchant);
            if (drMerchantDetails.Read())
            {
                merchant.FirstName = drMerchantDetails["First_Name"].ToString();
                merchant.LastName = drMerchantDetails["Last_Name"].ToString();
                merchant.EmailID = drMerchantDetails["Email_Id"].ToString();
                merchant.CompanyName = drMerchantDetails["Company_Name"].ToString();
                merchant.StreetAddress = drMerchantDetails["Street_Address"].ToString();
                merchant.City = drMerchantDetails["City"].ToString();
                merchant.State = drMerchantDetails["State"].ToString();
                merchant.CountryID = drMerchantDetails["Country_Id"].ToString();
                merchant.Zip = drMerchantDetails["zip"].ToString();
                merchant.PhoneNumber = drMerchantDetails["Phone_Number"].ToString();
                merchant.Fax = drMerchantDetails["Fax"].ToString();
                merchant.pending_Credit_duration = Convert.ToInt32(drMerchantDetails["pending_Credit_duration"]);
                merchant.ECom_platformID = Convert.ToInt16(drMerchantDetails["Ecom_PlatformId"]);
                merchant.Website = drMerchantDetails["Website"].ToString();
                merchant.Social_Referral_Id = drMerchantDetails["Social_Referral_Id"].ToString();
            }
            return merchant;
        }
        [WebMethod(enableSession: true)]
        public int MerchantForgetPassword(string EmailAddress)
        {
            SqlDataReader sDR = sqlPlugin.MerchantForgetPassword(EmailAddress);
            if (!sDR.HasRows)
                return 0;
            _Merchant merchant = new _Merchant();
            merchant.EmailID = EmailAddress;
            if (sDR.Read())
            {
                merchant.Password = sDR["Password"].ToString();
            }
            string EmailContent = "";
            string URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/ForgotPassword.html");
            StreamReader SR = new StreamReader(URL);
            EmailContent = SR.ReadToEnd();
            SR.Close();
            EmailContent = EmailContent.Replace("{PASSWORD}", merchant.Password);
            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            EmailContent = GetEmailHeaderFooter(EmailAddress).Replace("{BODYCONTENT}", EmailContent);
            comman.SendMail(EmailAddress, ConfigurationManager.AppSettings["site_name"].ToString()+" Password Request", EmailContent);
            return 1;
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
}
