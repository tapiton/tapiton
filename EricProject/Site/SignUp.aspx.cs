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

public partial class Site_SignUp : System.Web.UI.Page
{
    int alreadyRegistered = 0;
      string PublicKey = "";
      public Site_SignUp()
    {
        PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["FirstName"] != null && !Page.RouteData.Values["FirstName"].ToString().Trim().ToLower().Contains("cookieenable"))
        {
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            //EncryptDecrypt ED = new EncryptDecrypt();
            string DecryptedValue;
            DecryptedValue = Page.RouteData.Values["FirstName"].ToString();
            alreadyRegistered = 1;
            txtFirstName.Value = DecryptedValue.Split('^')[0];
            txtEmail.Value = DecryptedValue.Split('^')[1];
        }
        if (Page.RouteData.Values["FirstName"] != null && Page.RouteData.Values["FirstName"].ToString().Trim().ToLower().Contains("cookieenable"))
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"].ToString() + "site/CookieEnable");
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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        _plugin objPlugin1 = new _plugin();
        objPlugin1.EmailID = txtEmail.Value;
        objPlugin1.Password = txtPassword.Text;
        //objPlugin1.IsFacebook = false;
        //objPlugin1.IsTwitter = false;

        DAL.Plugin sqlPlugin = new DAL.Plugin();
        SqlDataReader drPlugin1 = sqlPlugin.CheckCustomerLogin(objPlugin1);
        //if (!drPlugin1.HasRows)
        //{
        //    txtEmail.Value = "Email Address";
        //    txtFirstName.Value = "Full Name";
        //    txtPassword.Value = "Password";
        //    txtConfirmPassword.Value = "Confirm Password";
        //    DivCustomerLoginMsg.Visible = true;
        //    CustomerLoginMsg.InnerHtml = "Customer already exist.Please sign In for Login.";
        //}
        while (drPlugin1.Read())
        {
            if (drPlugin1["Customer_Id"].ToString() != "0")
            {
                if (Convert.ToBoolean(drPlugin1["Is_Active"].ToString()) == false)
                {
                    string[] split = txtFirstName.Value.Split(new char[] { ' ' }, 2);
                    _plugin objPlugin = new _plugin();
                    objPlugin.Customer_ID = 0;
                    objPlugin.FirstName = split[0];
                    if (split.Length > 1)
                        objPlugin.LastName = split[1];
                    else
                        objPlugin.LastName = "";
                    objPlugin.EmailID = txtEmail.Value;
                    objPlugin.Password = txtPassword.Text;
                    if (alreadyRegistered == 1)
                        objPlugin.IsActive = true;
                    else
                        objPlugin.IsActive = false;
                    objPlugin.Address = "";
                    objPlugin.City = "";
                    objPlugin.State = "";
                    objPlugin.Country_ID = "";
                    objPlugin.Zip = 1;
                    objPlugin.PhoneNumber = "";
                    objPlugin.IsFacebook = false;
                    objPlugin.IsTwitter = false;
                    objPlugin.Status = "3";
                    objPlugin.Facebook_Id = "";
                    SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);
                    if (alreadyRegistered == 0)
                    {
                        _plugin objCredits = new _plugin();
                         objCredits.EmailID = txtEmail.Value;
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
                                    EmailContent = EmailContent.Replace("{Link}", "Please <a href='"+ ConfigurationManager.AppSettings["pageURL"].ToString()+"Site/Custometer/Activation?u=" + Server.UrlEncode(txtEmail.Value + "^" + 3) + "'>Click here</a> To Activate Your Account.<br/><br/>");
                                    EmailContent = GetEmailHeaderFooter(txtEmail.Value).Replace("{BODYCONTENT}", EmailContent);
                                    comman.SendMail(txtEmail.Value, "Activation Mail", EmailContent);
                                    DivCustomerLoginMsg.Visible = true;
                                    //divlogin.Visible = true;
                                    CustomerLoginMsg.InnerHtml = "Please click on the link on your email to activate your account.";
                                }
                                else
                                {
                                    _plugin objPluginupdate = new _plugin();
                                    objPluginupdate.Customer_ID = 0;
                                    objPluginupdate.FirstName = txtFirstName.Value;
                                    objPluginupdate.LastName = "";
                                    objPluginupdate.EmailID = txtEmail.Value;
                                    objPluginupdate.Password = txtPassword.Text;
                                    objPluginupdate.IsActive = true;
                                    objPluginupdate.Address = "";
                                    objPluginupdate.City = "";
                                    objPluginupdate.State = "";
                                    objPluginupdate.Country_ID = "";
                                    objPluginupdate.Zip = 1;
                                    objPluginupdate.PhoneNumber = "";
                                    objPluginupdate.IsFacebook = false;
                                    objPluginupdate.IsTwitter = false;
                                    objPluginupdate.Status = "3";
                                    objPluginupdate.Facebook_Id = "";
                                    SqlDataReader dr1 = sqlPlugin.UpdateCustomer_Master(objPluginupdate);
                                    Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                                    Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                                }
                            }
                    }
                    txtFirstName.Value = "Full Name";
                    txtEmail.Value = "Email Address";
                    txtPassword.Text = "Password";
                    txtConfirmPassword.Text = "Confirm Password";
                    if (alreadyRegistered == 1)
                    {
                        Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                        Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                    }
                }
                else
                {
                    DivCustomerLoginMsg.Visible = true;
                    //divlogin.Visible = true;
                    CustomerLoginMsg.InnerHtml = "An account already exists with this login.  Please sign in.";
                }
            }
            else
            {
                string[] split = txtFirstName.Value.Split(new char[] { ' ' }, 2);
                _plugin objPlugin = new _plugin();
                objPlugin.Customer_ID = 0;
                objPlugin.FirstName = split[0];
                if (split.Length > 1)
                    objPlugin.LastName = split[1];
                else
                    objPlugin.LastName = "";
                   objPlugin.EmailID = txtEmail.Value;
                objPlugin.Password = txtPassword.Text;
                objPlugin.Address = "";
                objPlugin.City = "";
                objPlugin.State = "";
                objPlugin.Country_ID = "";
                objPlugin.Zip = 1;
                objPlugin.PhoneNumber = "";
                objPlugin.IsFacebook = false;
                objPlugin.IsTwitter = false;
                objPlugin.IsActive = true;
                objPlugin.Facebook_Id = "";
                SqlDataReader dr  = sqlPlugin.InsertIntoCustomer_Master(objPlugin);
                while(dr.Read())
                {
                    Session["FirstName"] = txtFirstName.Value;
                    Session["CustomerID"] = dr["Customer_ID"].ToString();
                    Session["CustomerEmailId"] = dr["Email_Id"].ToString();
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard");
                }
            }
        }
        DBAccess.InstanceCreation().disconnect();
    }
}
