using System;
using System.Collections.Generic;
using System.Linq;
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


namespace EricProject.Site
{
    public partial class RoughPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Cookies["MerchantEmail"] != null)
                    txtEmail.Value = Request.Cookies["MerchantEmail"].Value;
                if (Request.Cookies["MerchantPassword"] != null)
                    //txtPassword.Value = Request.Cookies["CustomerPassword"].Value;
                    txtPassword.Attributes.Add("value", Request.Cookies["MerchantPassword"].Value);
            }

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            _Merchant objMarchant = new _Merchant();
            objMarchant.EmailID = txtEmail.Value;
            objMarchant.Password = txtPassword.Value;

            DAL.Plugin sqlPlugin = new DAL.Plugin();
            SqlDataReader drPlugin = sqlPlugin.CheckMerchantLogin(objMarchant);
            if (drPlugin.Read())
            {
                if (drPlugin["Merchant_ID"].ToString() != "0")
                {
                    if (drPlugin["IsActive"].ToString() == "True")
                    {
                        DivMerchantLoginMsg.Visible = false;
                        if (hiddenRememberMerchant.Value == "1")
                        {
                            Response.Cookies["MerchantEmail"].Value = txtEmail.Value;
                            Response.Cookies["MerchantPassword"].Value = txtPassword.Value;
                            Response.Cookies["MerchantEmail"].Expires = DateTime.Now.AddDays(7);
                            Response.Cookies["MerchantPassword"].Expires = DateTime.Now.AddDays(7);
                        }
                        else
                        {
                            Response.Cookies["MerchantEmail"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["MerchantPassword"].Expires = DateTime.Now.AddDays(-1);
                        }
                        Session["MerchantID"] = drPlugin["Merchant_ID"].ToString();
                        Session["MerchantEmailId"] = drPlugin["EmailID"].ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "Script", "window.parent.parent.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/MerchantDashboard.aspx'", true);
                    }
                    else
                    {
                        DivMerchantLoginMsg.Visible = true;
                        MerchantLoginMsg.InnerHtml = "Your Account Is Not Activated.<br/> Please activate your account.";
                        // Response.End();
                    }
                }
                else
                {
                    DivMerchantLoginMsg.Visible = true;
                    MerchantLoginMsg.InnerHtml = "Invalid Email or Password.";
                }
            }
            else
            {
                DivMerchantLoginMsg.Visible = true;
                MerchantLoginMsg.InnerHtml = "Invalid Email or Password.";
            }

        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
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
            objMarchant.Zip = 0;
            objMarchant.PhoneNumber = "";
            objMarchant.Fax = 0;
            objMarchant.AccountStatus = 0;
            objMarchant.WebsiteUrl = txtWebsiteUrlSignUp.Text;


            DAL.Plugin sqlPlugin = new DAL.Plugin();
            //SqlDataReader dr = sqlPlugin.CheckMerchantLogin(objMarchant);
            SqlDataReader dr = sqlPlugin.MerchantLogin(objMarchant);

            //while (dr.Read())
            //{
            if (dr.Read())
            {
                //if (dr["Merchant_ID"].ToString() != "0")
                //{
                DivMerchantLoginMsg.Visible = true;
                MerchantLoginMsg.InnerHtml = "EmailId already Exist. Please Sign In.";
                //}
            }
            else
            {
                int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);
                string EmailContent = "";
                string URL = Server.MapPath("~/EmailTemplate/Merchant/MerchantMail.htm");
                StreamReader SR = new StreamReader(URL);
                EmailContent = SR.ReadToEnd();
                SR.Close();

                //EmailContent = EmailContent.Replace("{Link}", "http://localhost:2180/EricProject/Site/MerchantActivation.aspx?u=" + txtEmailSignUp.Text);
                EmailContent = EmailContent.Replace("{Link}", "http://socialreferral.onlineshoppingpool.com/Site/MerchantActivation.aspx?u=" + txtEmailSignUp.Text);
                //EmailContent = EmailContent.Replace("{EMAIL}", txtEmailSignUp.Text);
                //EmailContent = EmailContent.Replace("{PASSWORD}", txtPasswordSignUp.Text);
                comman.SendMail(txtEmailSignUp.Text, "Activation Mail", EmailContent);

                DivMerchantLoginMsg.Visible = true;
                MerchantLoginMsg.InnerHtml = "Please Check Your Email Id<br/>To Activate Your Account.";
            }
            //}
        }
    }
}