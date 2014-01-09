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
using EricProject.Master;
using Encryption64;

public partial class Master_SiteMaster : System.Web.UI.MasterPage
{
    public string name = "";
    public string username = "";
    public string profileImage = "";
    public string followersCount = "";
    public string noOfTweets = "";
    public string recentTweet = "";
    //public string PageURl;
    string consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
    string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    _plugin objPlugin = new _plugin();
    string PublicKey = "";
    public Master_SiteMaster()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Request["oauth_token"] != null)
        {
            Session["oauth_token"] = Request["oauth_token"];
        }
        hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
        if (Session["MerchantID"] == null)
        {
            if (Request.Cookies["MerchantEmail"] != null && Request.Cookies["MerchantId"] != null && Request.Cookies["MerchantEmail"].Value != "" && Request.Cookies["MerchantId"].Value != "")
            {
                Session["MerchantEmailId"] = Request.Cookies["MerchantEmail"].Value;
                Session["MerchantID"] = Request.Cookies["MerchantId"].Value;
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard");
            }

            if (Request.Cookies["CustomerEmail"] != null && Request.Cookies["CustomerID"].Value != "" && Request.Cookies["CustomerEmail"].Value != "")
            {
                Session["CustomerID"] = Request.Cookies["CustomerID"].Value;
                Session["CustomerEmailId"] = Request.Cookies["CustomerEmail"].Value;
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/Dashboard");
            }
        }

        if (Session["CustomerEmailId"] != null)
        {
            string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
            li_HowItWorks_Footer.Visible = false;
            if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "site/customer/login") || (currentURL == selfURL + "site/customer/login?"))
            {
                Session["BottomLinkVisibleForCustomer"] = "Yes";
            }
            else
            {
                Session["BottomLinkVisibleForCustomer"] = null;
            }
        }
        if (Session["BottomLinkVisibleForCustomer"] != null)
        {
            if (Session["CustomerEmailId"] != null)
            {
                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
                if (Session["CustomerEmailIdNew"] == null)
                {
                    if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "site/customer/login") || (currentURL == selfURL + "site/customer/login?"))
                    {
                        Session["Isblank"] = null;
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/Dashboard");
                    }
                }
                if (Session["CustomerEmailIdNew"] != null)
                {
                    if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "site/customer/login") || (currentURL == selfURL + "site/customer/login?"))
                    {
                        Session["Isblank"] = null;
                    }
                    else
                    {
                        Session["Isblank"] = "Fill";
                    }
                    if (Session["CustomerEmailId"].ToString() == Session["CustomerEmailIdNew"].ToString())
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/Dashboard");
                    }
                    else
                    {
                        Session["Isblank"] = "Fill";
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/LoginRedirect");
                    }
                }
            }
        }
        if (Session["MerchantEmailId"] != null)
        {
            string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
            string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
            li_HowItWorks_Footer.Visible = false;
            if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "Site/Merchant/Login") || (currentURL == selfURL + "Site/Merchant/Login?"))
            {
                Session["BottomLinkVisibleForMerchant"] = "Yes";
            }
            else
            {
                Session["BottomLinkVisibleForMerchant"] = null;
            }
        }
        if (Session["BottomLinkVisibleForMerchant"] != null)
        {
            if (Session["MerchantEmailId"] != null)
            {
                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
                if (Session["MerchantEmailNew"] == null)
                {
                    if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "Site/Merchant/Login") || (currentURL == selfURL + "Site/Merchant/Login?"))
                    {
                        Session["Isblank"] = null;
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard");
                    }
                }
                if (Session["MerchantEmailIdNew"] != null)
                {
                    if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "Site/Merchant/Login") || (currentURL == selfURL + "Site/Merchant/Login?"))
                    {
                        Session["Isblank"] = null;
                    }
                    else
                    {
                        Session["Isblank"] = "Fill";
                    }
                    if (Session["MerchantEmailId"].ToString() == Session["MerchantEmailIdNew"].ToString())
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard");
                    }
                    else
                    {
                        Session["Isblank"] = "Fill";
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/LoginRedirect");
                    }
                }
            }
        }


        if (Session["MerchantID"] != null)
        {
            EricProject.UC.SiteMerchantHeader menumerchant = (EricProject.UC.SiteMerchantHeader)Page.LoadControl("~/UC/MenuMerchant.ascx");
            panel1.Controls.Add(menumerchant);
        }
        else if (Session["CustomerID"] != null)
        {
            EricProject.UC.MenuCustomer menucustomer = (EricProject.UC.MenuCustomer)Page.LoadControl("~/UC/MenuCustomer.ascx");
            panel1.Controls.Add(menucustomer);
        }
        else
        {
            MenuUC menuUC = (MenuUC)Page.LoadControl("~/UC/MenuUC.ascx");
            panel1.Controls.Add(menuUC);
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
        HeaderFooter = HeaderFooter.Replace("{pageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
        HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
        HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
        return HeaderFooter;
        //Header Footer Email Code
    }

    protected void btnCreateAccount_Click(object sender, EventArgs e)
    {
        if (Request["oauth_token"] != null)
        {
            //Insert Into Customer master
            try
            {
                _plugin objPlugin1 = new _plugin();
                objPlugin1.EmailID = txtTwitterEmail.Value;
                objPlugin1.Password = "";

                DAL.Plugin sqlPlugin = new DAL.Plugin();
                SqlDataReader drPlugin1 = sqlPlugin.CheckCustomerLogin(objPlugin1);
                while (drPlugin1.Read())
                {
                    if (drPlugin1["Customer_Id"].ToString() != "0")
                    {
                        if (Convert.ToBoolean(drPlugin1["Is_Twitter"].ToString()) == false)
                        {
                            _plugin objCredits = new _plugin();
                            objCredits.EmailID = txtTwitterEmail.Value;
                            SqlDataReader drcredits = sqlPlugin.Is_credits(objCredits);
                            if (drcredits.Read())
                            {
                                if (Convert.ToInt32(drcredits["Credits"].ToString()) > 0)
                                {
                                    string EmailContent = "";
                                    string URL = Server.MapPath("~/EmailTemplate/CustomerThanks/CustomerActivation.htm");
                                    StreamReader SR = new StreamReader(URL);
                                    EmailContent = SR.ReadToEnd();
                                    SR.Close();
                                    EmailContent = EmailContent.Replace("{Link}", "Please <a href='"+ConfigurationManager.AppSettings["pageURL"]+"Site/Custometer/Activation?u=" + Server.UrlEncode(txtTwitterEmail.Value + "^" + 2) + "'>Click here</a> To Activate Your Account.<br/><br/>");
                                    EmailContent = GetEmailHeaderFooter(txtTwitterEmail.Value).Replace("{BODYCONTENT}", EmailContent);
                                    comman.SendMail(txtTwitterEmail.Value, "Activation Mail", EmailContent);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please check your email to activate');window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Login" + "';", true);
                                }
                                else
                                {
                                    _plugin objPlugin = new _plugin();
                                    objPlugin.Customer_ID = 0;
                                    objPlugin.FirstName = Session["CustomerName"] + "";
                                    objPlugin.LastName = "";
                                    objPlugin.EmailID = txtTwitterEmail.Value;
                                    objPlugin.Password = "";
                                    objPlugin.IsActive = false;
                                    objPlugin.Address = "";
                                    objPlugin.City = "";
                                    objPlugin.State = "";
                                    objPlugin.Country_ID = "";
                                    objPlugin.Zip = 1;
                                    objPlugin.PhoneNumber = "";
                                    objPlugin.IsFacebook = false;
                                    objPlugin.IsTwitter = true;
                                    objPlugin.Status = "2";
                                    objPlugin.Facebook_Id = "";
                                    SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);

                                    Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                                    Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                                }
                            }
                        }
                        else
                        {
                            _plugin objPlugin = new _plugin();
                            objPlugin.Customer_ID = 0;
                            objPlugin.FirstName = Session["CustomerName"] + "";
                            objPlugin.LastName = "";
                            objPlugin.EmailID = txtTwitterEmail.Value;
                            objPlugin.Password = "";
                            objPlugin.IsActive = false;
                            objPlugin.Address = "";
                            objPlugin.City = "";
                            objPlugin.State = "";
                            objPlugin.Country_ID = "";
                            objPlugin.Zip = 1;
                            objPlugin.PhoneNumber = "";
                            objPlugin.IsFacebook = false;
                            objPlugin.IsTwitter = true;
                            objPlugin.Status = "2";
                            objPlugin.Facebook_Id = "";
                            SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);

                            Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                            Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                        }
                    }
                    else
                    {
                        _plugin objPlugin = new _plugin();
                        objPlugin.Customer_ID = 0;
                        objPlugin.FirstName = Session["CustomerName"] + "";
                        objPlugin.LastName = "";
                        objPlugin.EmailID = txtTwitterEmail.Value;
                        objPlugin.Password = "";
                        objPlugin.Address = "";
                        objPlugin.City = "";
                        objPlugin.State = "";
                        objPlugin.Country_ID = "";
                        objPlugin.Zip = 1;
                        objPlugin.PhoneNumber = "";
                        objPlugin.IsFacebook = false;
                        objPlugin.IsTwitter = true;
                        objPlugin.IsActive = false;
                        objPlugin.Twitter_Id = Session["TwitterId"] + "";
                        SqlDataReader dr = sqlPlugin.InsertIntoCustomer_MasterTwitter(objPlugin);
                        while (dr.Read())
                        {
                            Session["CustomerID"] = dr["Customer_ID"].ToString();
                            Session["CustomerEmailId"] = dr["Email_Id"].ToString();
                            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                        }
                    }
                }
                DBAccess.InstanceCreation().disconnect();
            }
            catch { }
            //Insert Into Customer master
        }
    }

}
