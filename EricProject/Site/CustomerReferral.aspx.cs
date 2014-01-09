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
using System.Web.Services;
using Encryption64;


public partial class Site_CustomerReferral : System.Web.UI.Page
{
    string PublicKey = "";
    System.Threading.Thread threadSendMails;
    public Site_CustomerReferral()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    _MerchantReferral ObjMerchantReferral = new _MerchantReferral();
    DAL.MerchantReferral sqlMerchantReferral = new DAL.MerchantReferral();
    public string URLID = ""; int ID = 0;
    public string SiteName = ConfigurationManager.AppSettings["site_name"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtmessage.InnerText = "I've received rewards for referring customers to other businesses using " + SiteName + ". I like your business and would love to refer friends to you as well.";
        if (Session["CustomerID"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Login");
        if (!IsPostBack)
        {
            Session["CheckRefreshCustomer"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }

    }
    protected void SaveMerchantReferralDetail(String EMail)
    {
        ObjMerchantReferral.Merchant_Referral_ID = 0;
        ObjMerchantReferral.Referrer_ID = Convert.ToInt32(Session["CustomerID"].ToString());
        ObjMerchantReferral.Name = "";
        ObjMerchantReferral.Email_Address = EMail;
        //ObjMerchantReferral.Website_Url = txtWebsiteURL.Value;
        ObjMerchantReferral.Message = txtmessage.Value;
        ObjMerchantReferral.Status = "Invited, not registered";
        ObjMerchantReferral.Referral_Merchant_ID = 0;
        ObjMerchantReferral.ReferralType = "Customer";
        int ID = sqlMerchantReferral.InsertMerchantReferral(ObjMerchantReferral);
        URLID = ID.ToString();

    }
    protected void ClearText()
    {
        txtEmail.Value = "";
        //txtWebsiteURL.Value = "";
        // txtmessage.Value = "";
    }
    public void Sendmail(String EMail)
    {

        string StrUrl = ConfigurationManager.AppSettings["pageURL"] + "Site/Home/" + ID;
        string StrMsg = "";
        StrMsg += "<table>";
        StrMsg += "<tr><td>";
        StrMsg += "Dear,";
        StrMsg += "</tr></td>";
        StrMsg += "<tr><td>" + txtmessage.InnerText + "</tr></td>";
        StrMsg += "<tr><td><a href='" + StrUrl + "'>Here's a link to a 6-month free trial:</a></td></tr>";
        StrMsg += "<tr><td>Please let me know if you decide to join.<br/></td></tr>";
        StrMsg += "<tr><td>&nbsp;</td></tr>";
        StrMsg += "<tr><td>Regards,</td></tr><tr><td>" + Session["FirstName"].ToString() + "</td></tr>";
        StrMsg += "</table>";
        StrMsg = GetEmailHeaderFooter((EMail.ToString())).Replace("{BODYCONTENT}", StrMsg);

        comman.SendMail(EMail.ToString(), "Referral Incentives?", StrMsg);
    }
    public string GetEmailHeaderFooter(string Email)
    {
        //Header Footer Email Code
        StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer_Without_Regards.htm"));
        string HeaderFooter = HeaderFooterSR.ReadToEnd();
        HeaderFooterSR.Close();
        string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
        HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
        HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
        HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
        return HeaderFooter;
        //Header Footer Email Code
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
      
        if (Session["CheckRefreshCustomer"].ToString() == ViewState["CheckRefreshCustomer"].ToString())
        {
            string[] EMailAddress = txtEmail.Value.Split(',');
            string EmailSuccess = "";
            string EmailFail = "";
            string EmailSameCustomer = "";
            int i = 0, j = 0,z=0;
            foreach (string EMail in EMailAddress)
            {
                if (Session["CustomerEmailId"].ToString() == EMail.Trim())
                {                   
                    EmailSameCustomer = EMail;                   
                    Session["CheckRefreshCustomer"] = Server.UrlDecode(System.DateTime.Now.ToString());                 
                }
                else
                {
                    if (Check_Merchant(EMail.Trim()) != 0)
                    {
                        Sendmail(EMail);
                        SaveMerchantReferralDetail(EMail);
                        if (i == 0)
                            EmailSuccess = EMail;
                        else
                            EmailSuccess += ", " + EMail;
                        Session["CheckRefreshCustomer"] = Server.UrlDecode(System.DateTime.Now.ToString());
                        i++;
                    }
                    else
                    {
                        if (j == 0)
                            EmailFail = EMail;
                        else
                            EmailFail += ", " + EMail;
                        Session["CheckRefreshCustomer"] = Server.UrlDecode(System.DateTime.Now.ToString());
                        j++;
                        //ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Merchant with the same email address already registered with us.')", true);
                    }
                }
            }
            if (EmailSuccess != "")
                lblresultSuccess.Text = "An email has been sent successfully to " + EmailSuccess;
            else
                lblresultSuccess.Text = "";
            if (EmailFail != "")
                lblresultFail.Text = "Merchant with the same email address already registered with us. ( " + EmailFail + " )";
            else
                lblresultFail.Text = "";
            if (EmailSameCustomer != "")
                lblResultSameEmail.Text = "Customer cannot refer himself as a merchant. ( " + EmailSameCustomer + " )";
            else
                lblResultSameEmail.Text = "";
            ClearText();
            EmailSuccess = "";
            EmailFail = "";
            return;
        }
    }

    protected int Check_Merchant(string Email)
    {
        ObjMerchantReferral.Email_Address = Email;
        ID = sqlMerchantReferral.CheckEmail(ObjMerchantReferral);
        return ID;
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefreshCustomer"] = Session["CheckRefreshCustomer"];
    }
}
