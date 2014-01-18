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



public partial class Site_Index : System.Web.UI.Page
{
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    _CampaignsDetails objCampaignsDetails = new _CampaignsDetails();
    int platform = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
        if (!IsPostBack)
        {
            MsgDiv.Visible = false;
            MsgInform.Visible = false;
            //Bind LatestTop3PostByCustomer
            if (Session["MerchantID"] == null)
            {
                objCampaignsDetails.MerchantID = Convert.ToInt32("0");
            }
            else
            {
                objCampaignsDetails.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
            }
            SqlDataReader drPluginPost = sqlPlugin.BindLatestTop3PostByCustomer(objCampaignsDetails);
            LatestTop3PostByCustomer.Text = "";
          
                while (drPluginPost.Read())
                {
                    if (drPluginPost["Post_Location"].ToString() == "1")
                    {
                        LatestTop3PostByCustomer.Text += "<li>";
                        LatestTop3PostByCustomer.Text += "<div class=\"img\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://www.facebook.com/" + drPluginPost["FacebookId"].ToString() + "\" target=\"_blank\"><img src=\"https://graph.facebook.com/" + drPluginPost["FacebookId"].ToString() + "/picture\" alt=\"" + " " + drPluginPost["Name"].ToString() + "\" title=\"" + drPluginPost["Name"].ToString() + "\"/></a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"text\">";
                        if (drPluginPost["DefaultFaceBook_ShareText"].ToString().Length > 170)
                            LatestTop3PostByCustomer.Text += "<p><a href=\"https://www.facebook.com/" + drPluginPost["FacebookId"].ToString() + "\" target=\"_blank\">" + drPluginPost["Name"].ToString() + "</a>  " + drPluginPost["DefaultFaceBook_ShareText"].ToString().Substring(0, 170) + "...</p>";
                        else
                            LatestTop3PostByCustomer.Text += "<p><a href=\"https://www.facebook.com/" + drPluginPost["FacebookId"].ToString() + "\" target=\"_blank\">" + drPluginPost["Name"].ToString() + "</a>  " + drPluginPost["DefaultFaceBook_ShareText"].ToString() + "</p>";
                        LatestTop3PostByCustomer.Text += "<span>" + drPluginPost["TimeAgo"].ToString() + "</span>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "</li>";
                    }
                    if (drPluginPost["Post_Location"].ToString() == "2")
                    {
                        LatestTop3PostByCustomer.Text += "<li>";
                        LatestTop3PostByCustomer.Text += "<div class=\"img\">";
                        LatestTop3PostByCustomer.Text += "<a href=\"https://twitter.com/" + drPluginPost["TwitterUserName"].ToString() + "\" target=\"_blank\"><img src=\"https://abs.twimg.com/sticky/default_profile_images/default_profile_6_normal.png\" alt=\"" + " " + drPluginPost["Name"].ToString() + "\" title=\"" + drPluginPost["Name"].ToString() + "\"/></a>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "<div class=\"text\">";
                        LatestTop3PostByCustomer.Text += "<p><a href=\"https://twitter.com/" + drPluginPost["TwitterUserName"].ToString() + "\" target=\"_blank\">" + drPluginPost["Name"].ToString() + "</a>  " + drPluginPost["DefaultFaceBook_ShareText"].ToString() + "</p>";
                        LatestTop3PostByCustomer.Text += "<span>" + drPluginPost["TimeAgo"].ToString() + "</span>";
                        LatestTop3PostByCustomer.Text += "</div>";
                        LatestTop3PostByCustomer.Text += "</li>";
                    }
                }
           
            if (!drPluginPost.HasRows)
            {
                SqlDataReader drPluginPostTemp = sqlPlugin.BindLatestTop3Posttemp();
                while (drPluginPostTemp.Read())
                {
                    LatestTop3PostByCustomer.Text += "<li>";
                    LatestTop3PostByCustomer.Text += "<div class=\"img\">";
                    LatestTop3PostByCustomer.Text += "<a href=\"#\" ><img src="+ConfigurationManager.AppSettings["pageURL"].ToString() + "images/MerchantImage/"+drPluginPostTemp["Image"]+"></a>";
                    LatestTop3PostByCustomer.Text += "</div>";
                    LatestTop3PostByCustomer.Text += "<div class=\"text\">";
                    LatestTop3PostByCustomer.Text += "<p>"+ drPluginPostTemp["Text"].ToString() + "</p>";
                   // LatestTop3PostByCustomer.Text += "<span>" + drPluginPost["TimeAgo"].ToString() + "</span>";
                    LatestTop3PostByCustomer.Text += "</div>";
                    LatestTop3PostByCustomer.Text += "</li>";
                }
                if (!drPluginPostTemp.HasRows)
                {
                    LatestTop3PostByCustomer.Text += "<span style=\"color:#ffffff\">No Post Available</span>";
                }
            }
            //Bind LatestTop3PostByCustomer
            DBAccess.InstanceCreation().disconnect();

        }
        try
        {
            var routeValues = Request.RequestContext.RouteData.Values;
            if (routeValues["RID"] != null)
            {
                Response.Cookies["RID"].Value = routeValues["RID"].ToString();
                Response.Cookies["RID"].Expires = DateTime.Now.AddDays(7);
            }
        }
        catch { }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        string Weburl = txtWebsiteUrl.Value;
        _Merchant objMarchant = new _Merchant();
        //objMarchant.MerchantID = 0;
        objMarchant.FirstName = "";
        objMarchant.LastName = "";
        objMarchant.EmailID = txtEmail.Value;
        objMarchant.Password = txtPassword.Text;
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
            txtEmail.Value = "Email";
            txtWebsiteUrl.Value = "Website Url";
            //txtPassword.Text = "Password";
            //txtConfirmPassword.Text = "Confirm Password";
            MsgInform.InnerHtml = "";
            MsgInform.Visible = false;
            MsgDiv.Visible = true;
            MsgDiv.InnerHtml = "";
            MsgDiv.InnerHtml = "Email Id Already Exists.";
        }
        else
        {
            DAL.Admin sqlcompany = new DAL.Admin();
            _Company objCompany = new _Company();
            objCompany.CompanyWebsite = Weburl;
            objCompany.CompanyEmail = txtEmail.Value;
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
            objMarchant.IsActive = true;
            int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);

            _Merchant_website_detail objwebsitedetail = new _Merchant_website_detail();
            //objwebsitedetail.Website_ID = 0;
            objwebsitedetail.Merchant_ID = result;
            if (txtWebsiteUrl.Value == "Website Url")
            {
                objwebsitedetail.Website = "";
            }
            else
            {
                objwebsitedetail.Website = txtWebsiteUrl.Value;
            }
            objwebsitedetail.ECom_platformID = platform;
            int MerchantWebsite = sqlPlugin.InsertIntoMerchantWebsiteDetails(objwebsitedetail);

            string EmailContent = "";
            StreamReader SR = new StreamReader(Server.MapPath("~/EmailTemplate/Merchant/MerchantMail.htm"));
            EmailContent = SR.ReadToEnd();
            SR.Close();
            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            EmailContent = GetEmailHeaderFooter().Replace("{BODYCONTENT}", EmailContent);
            comman.SendMail(txtEmail.Value, "Welcome To " + ConfigurationManager.AppSettings["site_name"].ToString(), EmailContent);
            Session["FirstName"] = "";
            Session["MerchantID"] = result;
            Session["MerchantEmailId"] = txtEmail.Value;


            txtEmail.Value = "Email";
            txtWebsiteUrl.Value = "Website Url";
            //txtPassword.Text = "Password";
            //txtConfirmPassword.Text = "Confirm Password";
            MsgDiv.InnerHtml = "";
            MsgDiv.Visible = false;
            MsgInform.Visible = true;
            MsgInform.InnerHtml = "";
            UpdateMerchantReferralDetails(result);

            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/SiteName");
        }
        //}
        DBAccess.InstanceCreation().disconnect();
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
        DAL.MerchantReferral sqlMerchantReferral = new DAL.MerchantReferral();
        int RID = 0;
        if (Request.Cookies["RID"] != null)
            RID = Convert.ToInt32(Request.Cookies["RID"].Value);
        else
            RID = sqlMerchantReferral.Get_Last_Referrer(Session["MerchantEmailId"].ToString());

        _MerchantReferral ObjMerchantReferral = new _MerchantReferral();
        ObjMerchantReferral.Referral_Merchant_ID = Convert.ToInt32(Referral_Mechant_ID);
        ObjMerchantReferral.Merchant_Referral_ID = RID;
        sqlMerchantReferral.UpdateMerchantReferralDetails(ObjMerchantReferral);
        ObjMerchantReferral.Status = "Registered, not active";
        sqlMerchantReferral.UpdateReferral(ObjMerchantReferral);
        Response.Cookies.Clear();
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
}