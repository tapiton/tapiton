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


public partial class Site_MerchantReferral : System.Web.UI.Page
{
    string PublicKey = "";
    System.Threading.Thread threadSendMails;
    public Site_MerchantReferral()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    _MerchantReferral ObjMerchantReferral = new _MerchantReferral();
    DAL.MerchantReferral sqlMerchantReferral = new DAL.MerchantReferral();
    public string URLID = ""; int ID = 0;
    public string companyname = ConfigurationManager.AppSettings["site_name"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        txtmessage.InnerText = "I've been using " + companyname + " and I think it might work well for your business too. It's a referral platform that turns your customers into advocates and incentivizes them to make word of mouth referrals.<br /><br />    Sign up with this link to get a 6 month free trial: ";
        if (Session["MerchantId"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");
        if (!IsPostBack)
        {
            Session["CheckRefreshMerchant"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
    }
    protected void SaveMerchantReferralDetail(String EMail)
    {

        ObjMerchantReferral.Merchant_Referral_ID = 0;
        ObjMerchantReferral.Referrer_ID = Convert.ToInt32(Session["MerchantId"].ToString());
        ObjMerchantReferral.Name = "";
        ObjMerchantReferral.Email_Address = EMail;
        ObjMerchantReferral.Message = txtmessage.Value;
        ObjMerchantReferral.Status = "Invited, not registered";
        ObjMerchantReferral.Referral_Merchant_ID = 0;
        ObjMerchantReferral.ReferralType = "Merchant";
        int ID = sqlMerchantReferral.InsertMerchantReferral(ObjMerchantReferral);
        URLID = ID.ToString();

    }
    protected void ClearText()
    {
        // txtFirstName.Value = "";
        txtEmail.Value = "";
        //txtmessage.Value = "I've been using SocialRefferal and I think it might work well for your business too. It's a referral platform that turns your customers into advocates and incentivizes them to make word of mouth referrals.<br/><br/>Sign up with this link to get a 6 month free trial:<br/><br/>http://www.asdfasdlfkasj.com/afsda";

    }
    public void Sendmail(String EMail)
    {

        string StrUrl = ConfigurationManager.AppSettings["pageURL"] + "Site/Home/" + ID;
        string StrMsg = "";
        StrMsg += "<table>";
        StrMsg += "<tr><td>";
        StrMsg += "Hi,";
        StrMsg += "</td></tr>";
        StrMsg += "<tr><td>" + txtmessage.InnerText + "</tr></td>";
        StrMsg += "<tr><td><br/>";
        StrMsg += "Url : <a href='" + StrUrl + "' >" + StrUrl + "</a>";
        StrMsg += "<br/></td></tr>";
        StrMsg += "<tr><td>&nbsp;</td></tr>";
        StrMsg += "<tr><td>Regards,</td></tr><tr><td>" + Session["FirstName"].ToString() + "</td></tr>";
        StrMsg += "</table>";
        StrMsg = GetEmailHeaderFooter((EMail.ToString())).Replace("{BODYCONTENT}", StrMsg);
        comman.SendMail(EMail, "Merchant Referral", StrMsg);
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
        if (Session["CheckRefreshMerchant"].ToString() == ViewState["CheckRefreshMerchant"].ToString())
        {
            string[] EMailAddress = txtEmail.Value.Split(',');
            string EmailSuccess = "";
            string EmailFail = "";
            int i = 0,j=0;
            foreach (string EMail in EMailAddress)
            {
                if (Check_Merchant(EMail.Trim()) != 0)
                {
                    Sendmail(EMail);
                    SaveMerchantReferralDetail(EMail);
                    if (i == 0)
                        EmailSuccess = EMail;
                    else
                        EmailSuccess += ", " + EMail;
                    Session["CheckRefreshMerchant"] = Server.UrlDecode(System.DateTime.Now.ToString());
                    i++;
                }
                else
                {
                    if (j == 0)
                        EmailFail = EMail;
                    else
                        EmailFail += ", " + EMail;
                    Session["CheckRefreshMerchant"] = Server.UrlDecode(System.DateTime.Now.ToString());
                    j++;
                    //ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Merchant with the same email address already registered with us.')", true);
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
            ClearText();
            EmailSuccess = "";
            EmailFail = "";
            return;
        }
    }
    protected void BindStatus()
    {

    }
    protected int Check_Merchant(string Email)
    {
        ObjMerchantReferral.Email_Address = Email;
        ID = sqlMerchantReferral.CheckEmail(ObjMerchantReferral);
        return ID;
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearText();
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefreshMerchant"] = Session["CheckRefreshMerchant"];
    }
}
