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


public partial class Site_CustomerLogin : System.Web.UI.Page
{
    string PublicKey = "";
    public Site_CustomerLogin()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    public string name = "";
    public string username = "";
    public string profileImage = "";
    public string followersCount = "";
    public string noOfTweets = "";
    public string recentTweet = "";
    Twitter twitterobj = new Twitter();
    string consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
    string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();

        if (!IsPostBack)
        {

            if (Request["oauth_token"] == null)
            {
                string callbackUrl = ConfigurationManager.AppSettings["pageURL"] + "Site/TwitterSuccess.aspx";
                string requestToken = twitterobj.GetRequestToken(consumerKey, consumerSecret, callbackUrl);

                Uri authenticationUri = twitterobj.BuildAuthorizationUri(requestToken);
                twitterurl.Value = authenticationUri.AbsoluteUri;
            }

            if (Request.QueryString["oauth_token"] != null)
            {
                _plugin objPlugin = new _plugin();
                DAL.Plugin sqlPlugin = new DAL.Plugin();
                Twitterizer.OAuthTokenResponse accessTokenResponse = twitterobj.GetAccessToken(consumerKey, consumerSecret,
                                                                          Request.QueryString["oauth_token"],
                                                                          Request.QueryString["oauth_verifier"]);
                objPlugin.Twitter_Id = accessTokenResponse.UserId.ToString();
                Session["TwitterId"] = accessTokenResponse.UserId.ToString();
                Session["CustomerName"] = accessTokenResponse.ScreenName;
                SqlDataReader drCustomerTwitterLogin = sqlPlugin.CheckCustomerTwitterLogin(objPlugin);
                while (drCustomerTwitterLogin.Read())
                {
                    Session["CustomerEmailId"] = drCustomerTwitterLogin["Email_Id"].ToString();
                    Session["CustomerID"] = drCustomerTwitterLogin["Customer_Id"].ToString();
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                }
                DBAccess.InstanceCreation().disconnect();
            }
        }

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
    protected void btnLoginCustomer_Click(object sender, EventArgs e)
    {

        _plugin objPlugin = new _plugin();
        objPlugin.EmailID = txtEmail.Value;
        objPlugin.Password = txtPassword.Text;
        objPlugin.IsFacebook = false;
        objPlugin.IsTwitter = false;
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        SqlDataReader drPlugin = sqlPlugin.CheckCustomerLoginPagecus(objPlugin);
        if (drPlugin.Read())
        {
            if (drPlugin["Customer_ID"].ToString() != "0")
            {
                if (drPlugin["Password"].ToString() == "")
                {
                    divloginaccount.Visible = true;
                    DivCustomerLoginMsg.Visible = false;
                    loginaccount.InnerHtml = "There is a temporary account setup for you.<span style='text-decoration: underline;cursor:pointer;' onclick='window.location.href=\""+ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SignUp\";'>Click here</span> to sign up and activate it.";
                    return;
                }

                if (Convert.ToBoolean(drPlugin["Is_Active"].ToString()) == false)
                {
                    string EmailContent = "";
                    //string URL = Server.MapPath("~/EmailTemplate/CustomerThanks/CustomerThanks.htm");
                    //StreamReader SR = new StreamReader(URL);
                    //EmailContent = SR.ReadToEnd();
                    //SR.Close();
                    EmailContent = GetEmailHeaderFooter(txtEmail.Value).Replace("{BODYCONTENT}", " <a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/Custometer/Activation?u=" + Server.UrlEncode(txtEmail.Value + "^" + 3) + "'>Click here</a> To Activate Your Account.<br/><br/>");

                    comman.SendMail(txtEmail.Value, "Activation Mail", EmailContent);
                    divloginaccount.Visible = true;
                    loginaccount.InnerHtml = "You have not yet clicked on the activation link. A new link has been sent to your email address.";
                }
                else
                {
                    if (txtPassword.Text == drPlugin["Password"].ToString())
                    {
                        DivCustomerLoginMsg.Visible = false;
                        divloginaccount.Visible = false;
                        if (CbRememberMe.Checked)
                        {
                            Session["FirstName"] = drPlugin["First_Name"].ToString();
                            Response.Cookies["CustomerEmail"].Value = txtEmail.Value;
                            Response.Cookies["CustomerPassword"].Value = txtPassword.Text;
                            Response.Cookies["CustomerID"].Value = drPlugin["Customer_ID"].ToString();
                            Response.Cookies["CustomerEmail"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["CustomerPassword"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddDays(7);
                        }
                        else
                        {
                            Session["FirstName"] = drPlugin["First_Name"].ToString();
                            Response.Cookies["CustomerEmail"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["CustomerPassword"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["CustomerID"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Session["CustomerID"] = drPlugin["Customer_ID"].ToString();
                        Session["CustomerEmailId"] = drPlugin["Email_Id"].ToString();
                        Session["CustomerPassword"] = drPlugin["Password"].ToString();
                        if (Request.QueryString.Count > 0)
                            Response.Redirect(Request.QueryString[0]);
                        else
                            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");


                    }
                    else
                    {
                        DivCustomerLoginMsg.Visible = true;
                        divloginaccount.Visible = false;
                        CustomerLoginMsg.InnerHtml = "Incorrect login credentials.";
                    }
                    //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/CustomerCreditDetails.aspx");
                }
            }
            else
            {
                DivCustomerLoginMsg.Visible = true;
                divloginaccount.Visible = false;
                CustomerLoginMsg.InnerHtml = "Incorrect login credentials.";
            }
        }

        else
        {

            DivCustomerLoginMsg.Visible = false;
            divloginaccount.Visible = true;
            loginaccount.InnerHtml = "Incorrect login credentials.";

        }
        DBAccess.InstanceCreation().disconnect();
    }
}
