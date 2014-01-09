using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;

namespace EricProject.Site
{
    public partial class MerchantSignUpFree : System.Web.UI.Page
    {
        int platform = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            Msg.Visible = false;
            MsgInform.Visible = false;
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
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
                MsgInform.InnerHtml = "";
                MsgInform.Visible = false;
                Msg.Visible = true;
                Msg.InnerHtml = "";
                Msg.InnerHtml = "Email Id Already Exist. Please Sign In.";
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
                int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);

                _Merchant_website_detail objwebsitedetail = new _Merchant_website_detail();
                //objwebsitedetail.Website_ID = 0;
                objwebsitedetail.Merchant_ID = result;
                objwebsitedetail.Website = txtWebsiteUrlSignUp.Text;
                objwebsitedetail.ECom_platformID = platform;
                int MerchantWebsite = sqlPlugin.InsertIntoMerchantWebsiteDetails(objwebsitedetail);

                string EmailContent = "";
                string URL = Server.MapPath("~/EmailTemplate/Merchant/MerchantMail.htm");
                StreamReader SR = new StreamReader(URL);
                EmailContent = SR.ReadToEnd();
                SR.Close();

                //EmailContent = EmailContent.Replace("{Link}", "http://localhost:2180/EricProject/Site/Merchant/Activation?u=" + txtEmailSignUp.Text);
                EmailContent = EmailContent.Replace("{Link}", ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/MerchantActivation.aspx?u=" + txtEmailSignUp.Text);
                EmailContent = EmailContent.Replace("{SocialReferralId}", ReferralId);
                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                //EmailContent = EmailContent.Replace("{PASSWORD}", txtPasswordSignUp.Text);
                comman.SendMail(txtEmailSignUp.Text, "Activation Mail", EmailContent);

                txtEmailSignUp.Text = "";
                txtPasswordSignUp.Text = "";
                txtRePasswordSignUp.Text = "";
                txtWebsiteUrlSignUp.Text = "";
                Msg.InnerHtml = "";
                Msg.Visible = false;
                MsgInform.Visible = true;
                MsgInform.InnerHtml = "";
                MsgInform.InnerHtml = "Please Check Your Email Id<br/>To Activate Your Account.";
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
    }
}