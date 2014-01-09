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


public partial class Site_MerchantLogin : System.Web.UI.Page
{
    int platform = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
        Msg.Visible = false;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        MerchantLoginMsg.InnerHtml = "";
        DivMerchantLoginMsg.InnerHtml = "";
        DivMerchantLoginMsg.Visible = false;
        if (txtEmail.Value == "")
        {
           
            //DivMerchantLoginMsg.Visible = true;
            MerchantLoginMsg.Visible = true;
            MerchantLoginMsg.InnerHtml = "";
            MerchantLoginMsg.InnerHtml = "Please enter a valid email address.";
         
        }
        else if (txtPassword.Text == "")
        {

            //DivMerchantLoginMsg.Visible = true;
            MerchantLoginMsg.Visible = true;
            MerchantLoginMsg.InnerHtml = "";
            MerchantLoginMsg.InnerHtml = "Please enter a correct Password.";

        }
        else
        {
            _Merchant objMarchant = new _Merchant();
            objMarchant.EmailID = txtEmail.Value;
            objMarchant.Password = txtPassword.Text;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader drPlugin = sqlPlugin.CheckMerchantLogin(objMarchant);
            if (drPlugin.Read())
            {
                if (drPlugin["Merchant_Id"].ToString() != "0")
                {
                    if (drPlugin["Is_Active"].ToString() == "True")
                    {
                        DivMerchantLoginMsg.Visible = false;
                        if (CbRememberMe.Checked)
                        {
                            Session["FirstName"] = drPlugin["First_Name"].ToString();
                            Response.Cookies["MerchantEmail"].Value = txtEmail.Value;
                            Response.Cookies["MerchantPassword"].Value = txtPassword.Text;
                            Response.Cookies["MerchantId"].Value = drPlugin["Merchant_Id"].ToString();
                            Response.Cookies["MerchantEmail"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["MerchantPassword"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["MerchantId"].Expires = DateTime.Now.AddDays(7);

                        }
                        else
                        {
                            Session["FirstName"] = drPlugin["First_Name"].ToString();
                            Response.Cookies["MerchantEmail"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["MerchantPassword"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["MerchantId"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Session["MerchantID"] = drPlugin["Merchant_Id"].ToString();
                        Session["MerchantEmailId"] = drPlugin["Email_Id"].ToString();

                        objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                        SqlDataReader drPlugincheck = sqlPlugin.BindMerchantById(objMarchant);
                        //if (drPlugincheck.Read())
                        //{
                        //    if (Request.QueryString.Count > 0)
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + Request.QueryString[0] + "';", true);
                        //    else if (drPlugincheck["Company_Name"].ToString() == null || drPlugincheck["Company_Name"].ToString() == "")
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName';", true);
                        //    else
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard';", true);

                        //}
                        //else
                        //{
                        //    if (Request.QueryString.Count > 0)
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + Request.QueryString[0] + "';", true);
                        //    else
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard';", true);
                        //}
                        if (drPlugincheck.Read())
                        {
                            if (Request.QueryString.Count > 0)
                                Response.Redirect(Request.QueryString[0]);
                            else if (drPlugincheck["Company_Name"].ToString() == null || drPlugincheck["Company_Name"].ToString() == "")
                                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName");
                            else
                                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard");

                        }
                        else
                        {
                            if (Request.QueryString.Count > 0)
                                Response.Redirect(Request.QueryString[0]);
                            else
                                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Dashboard");
                        }
                    }
                    else
                    {
                        txtEmail.Value = "";
                        //DivMerchantLoginMsg.Visible = true;
                        MerchantLoginMsg.Visible = true;
                        MerchantLoginMsg.InnerHtml = "";
                        MerchantLoginMsg.InnerHtml = "Your Account Is Not Activated. Please Activate Your Account.";
                        // Response.End();
                    }
                }
                else
                {
                    txtEmail.Value = "";
                    //DivMerchantLoginMsg.Visible = true;
                    MerchantLoginMsg.Visible = true;
                    MerchantLoginMsg.InnerHtml = "";
                    MerchantLoginMsg.InnerHtml = "Invalid Email or Password.";
                }
            }
            else
            {
                txtEmail.Value = "";
                //DivMerchantLoginMsg.Visible = true;
                MerchantLoginMsg.Visible = true;
                MerchantLoginMsg.InnerHtml = "";
                MerchantLoginMsg.InnerHtml = "Invalid Email or Password.";

            }
        }
        DBAccess.InstanceCreation().disconnect();
    }
    protected void btnSignUpMerchantReg_Click(object sender, EventArgs e)
    {
        MerchantLoginMsg.InnerHtml = "";
        DivMerchantLoginMsg.InnerHtml = "";
        DivMerchantLoginMsg.Visible = false;

        string Weburl = txtWebsiteUrlSignUp.Text;
        _Merchant objMarchant = new _Merchant();
        //objMarchant.MerchantID = 0;
        objMarchant.FirstName = "";
        objMarchant.LastName = "";
        objMarchant.EmailID = txtEmailSignUp.Text;
        objMarchant.Password = txtPasswordSignUp.Text;
        objMarchant.ReferralEmailID = "";
        objMarchant.CompanyName = "";
        objMarchant.StreetAddress = "";
        objMarchant.City = "";
        objMarchant.State = "";
        objMarchant.CountryID = "";
        objMarchant.Zip = "";
        objMarchant.PhoneNumber = "";
        objMarchant.Fax = "";
        objMarchant.AccountStatus = 1;
        //objMarchant.WebsiteUrl = Weburl;

        SqlDataReader dr;
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        //SqlDataReader dr = sqlPlugin.CheckMerchantLogin(objMarchant);

        dr = sqlPlugin.MerchantLogin(objMarchant);

        if (dr.Read())
        {
            txtEmailSignUp.Text = "";
            txtPasswordSignUp.Text = "";
            txtRePasswordSignUp.Text = "";
            txtWebsiteUrlSignUp.Text = "";
            //DivMerchantLoginMsg.Visible = true;
            MerchantLoginMsg.Visible = true;
            MerchantLoginMsg.InnerHtml = "";
            MerchantLoginMsg.InnerHtml = "Email Id Already Exist. Please Sign In.";
        }
        else
        {
            DAL.Admin sqlcompany = new DAL.Admin();
            _Company objCompany = new _Company();
            objCompany.CompanyWebsite = Weburl;
            objCompany.CompanyEmail = txtEmailSignUp.Text;
            dr = sqlcompany.CompanySearch(objCompany);

            if (dr.Read())
            {
                string CompanyEmail = dr["Company_Email"].ToString();
                DAL.Admin sqlcompanycontact = new DAL.Admin();
                _CompanyContact objCompanyContact = new _CompanyContact();
                objCompanyContact.ContactEmail = CompanyEmail;
                SqlDataReader dr1 = sqlcompanycontact.CompanySearch(objCompanyContact);
                if (dr1.Read())
                {
                    objMarchant.FirstName = dr1["Contact_Name"].ToString();
                }

                objMarchant.CompanyName = dr["Company_Name"].ToString();
                //objMarchant.Ecom_Platform_Id = dr["Ecom_Platform_Id"].ToString();
                platform = Convert.ToInt32(dr["Ecom_Platform_Id"].ToString());
                objMarchant.StreetAddress = dr["Address"].ToString() + dr["Address1"].ToString();
                objMarchant.City = dr["City"].ToString();
                objMarchant.State = dr["State"].ToString();
                objMarchant.Zip = dr["Zip"].ToString();
                objMarchant.PhoneNumber = dr["Company_Phone"].ToString();
                objMarchant.Fax = dr["Company_Fax"].ToString();
            }

            objMarchant.Social_Referral_Id = GenerateId(8);
            string ReferralId = objMarchant.Social_Referral_Id;
            SqlDataReader dr2;
            dr2 = sqlPlugin.CheckMerchantSocialReferralId(objMarchant);
            while (dr2.Read())
            {
                objMarchant.Social_Referral_Id = GenerateId(8);
                ReferralId = objMarchant.Social_Referral_Id;
                dr2 = sqlPlugin.CheckMerchantSocialReferralId(objMarchant);
            }
            //objMarchant.PendingDate = 0;
            objMarchant.IsActive = true;
            int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);

            _Merchant_website_detail objwebsitedetail = new _Merchant_website_detail();
            //objwebsitedetail.Website_ID = 0;
            objwebsitedetail.Merchant_ID = result;
            objwebsitedetail.Website = txtWebsiteUrlSignUp.Text;
            objwebsitedetail.ECom_platformID = platform;
            
            int MerchantWebsite = sqlPlugin.InsertIntoMerchantWebsiteDetails(objwebsitedetail);

            string EmailContent = "";
            StreamReader SR = new StreamReader(Server.MapPath("~/EmailTemplate/Merchant/MerchantMail.htm"));
            EmailContent = SR.ReadToEnd();
            SR.Close();
            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            EmailContent = GetEmailHeaderFooter().Replace("{BODYCONTENT}", EmailContent);
            comman.SendMail(txtEmailSignUp.Text, "Welcome To " + ConfigurationManager.AppSettings["site_name"].ToString(), EmailContent);

            Session["FirstName"] = "";
            Session["MerchantID"] = result;
            Session["MerchantEmailId"] = txtEmailSignUp.Text;

            txtEmailSignUp.Text = "";
            txtPasswordSignUp.Text = "";
            txtRePasswordSignUp.Text = "";
            txtWebsiteUrlSignUp.Text = "";
            DivMerchantLoginMsg.Visible = true;
            MerchantLoginMsg.InnerHtml = "";
            MerchantLoginMsg.Visible = false;
            UpdateMerchantReferralDetails(result);
            //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName");
            //ClientScript.RegisterStartupScript(this.GetType(),"Script", "<script language=javascript>  window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName';</script>");
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName");
        }
        //}
        DBAccess.InstanceCreation().disconnect();
    }

    public void ResetSignUp()
    {
        txtEmailSignUp.Text = "";
        txtPasswordSignUp.Text = "";
        txtRePasswordSignUp.Text = "";
        txtWebsiteUrlSignUp.Text = "";
    }

    public string GenerateId(int IdLength)
    {
        string SocialReferralId = "";
        //string AlhaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string AlhaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Number = "0123456789";
        Random rn = new Random();
        //for (int i = 0; i < IdLength; i++)
        //{
        //    int num = rn.Next(9);
        //    SocialReferralId += AlhaNumeric[num].ToString();
        //}

        for (int i = 0; i < 4; i++)
        {
            int num = rn.Next(9);
            SocialReferralId += AlhaNumeric[num].ToString();
            for (int j = 0; j < 1; j++)
            {
                SocialReferralId += Number[num].ToString();
            }
        }
        return SocialReferralId;
    }
    public void UpdateMerchantReferralDetails(int Referral_Mechant_ID)
    {
        if (Request.Cookies["RID"] != null)
        {
            int RID = Convert.ToInt32(Request.Cookies["RID"].Value);
            _MerchantReferral ObjMerchantReferral = new _MerchantReferral();
            ObjMerchantReferral.Referral_Merchant_ID = Convert.ToInt32(Referral_Mechant_ID);
            ObjMerchantReferral.Merchant_Referral_ID = RID;
            DAL.MerchantReferral sqlMerchantReferral = new DAL.MerchantReferral();
            sqlMerchantReferral.UpdateMerchantReferralDetails(ObjMerchantReferral);
            ObjMerchantReferral.Status = "Registered, not active";
            sqlMerchantReferral.UpdateReferral(ObjMerchantReferral);
            Response.Cookies.Clear();
        }
    }
    public string GetEmailHeaderFooter()
    {
        //Header Footer Email Code
        StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
        string HeaderFooter = HeaderFooterSR.ReadToEnd();
        HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
        HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
        HeaderFooterSR.Close();
        return HeaderFooter;
        //Header Footer Email Code
    }

    protected void btnMerchantForgetPassword_Click(object sender, EventArgs e)
    {

    }
}