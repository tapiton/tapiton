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
using System.Xml;
using oAuthExample;
using System.IO;
using Encryption64;

public partial class Site_CustomerForgetPassword : System.Web.UI.Page
{
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    string PublicKey = "";
    public Site_CustomerForgetPassword()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
       
            string EmailAddress = txtForgetEmail.Text;
            SqlDataReader sDR = sqlPlugin.CustomerForgetPassword(EmailAddress);
            if (!sDR.HasRows)
            {
                DivCustomerLoginMsg.Visible = true;
                CustomerLoginMsg.InnerHtml = "No account found with '" + EmailAddress + "' email.";
                return;
            }
            string Password = "";
            if (sDR.Read())
            {
                Password = sDR["Password"].ToString();
            }
            if (Password == "")
            {
                DivCustomerLoginMsg.Visible = true;
                CustomerLoginMsg.InnerHtml = "There is a temporary account setup for you. <span style='text-decoration: underline; cursor: pointer' onclick='window.location.href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Site/SignUp" + "\"'>Click here</span> to sign up and activate it.";
                return;
            }
            string EmailContent = "";
            string URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/ForgotPassword.html");
            StreamReader SR = new StreamReader(URL);
            EmailContent = SR.ReadToEnd();
            SR.Close();
            EmailContent = EmailContent.Replace("{PASSWORD}", Password);
            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            EmailContent = GetEmailHeaderFooter(EmailAddress).Replace("{BODYCONTENT}", EmailContent);
            comman.SendMail(EmailAddress, ConfigurationManager.AppSettings["site_name"].ToString() + " Password Request", EmailContent);
            DivCustomerLoginMsg.Visible = true;
            CustomerLoginMsg.InnerHtml = "We have sent your password to your registered email address.";
        
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
