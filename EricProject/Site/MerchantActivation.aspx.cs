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
    public partial class MerchantActivation : System.Web.UI.Page
    {
        string EmailId = string.Empty;
        string Password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["u"] != "")
            {
                EmailId = Request.QueryString["u"].ToString();
                // Password = Request.QueryString["pwd"].ToString();

                _Merchant objMarchant = new _Merchant();
                objMarchant.MerchantID = 1;
                objMarchant.FirstName = "";
                objMarchant.LastName = "";
                objMarchant.EmailID = EmailId;
                objMarchant.Password = Password;
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


                DAL.Plugin sqlPlugin = new DAL.Plugin();
                SqlDataReader dr = sqlPlugin.MerchantLogin(objMarchant);
                //string email = EmailId;
                //string checkmail = dr["IsActive"].ToString();
                if (dr.Read())
                {
                    if (dr["Is_Active"].ToString() == "True")
                    {
                        //Response.Write("Email Id is Already Activated");
                        lblMsg.Text = "Email Id is Already Activated";
                    }
                    else
                    {
                        objMarchant.IsActive = true;
                        int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);
                        string EmailContent = "";
                        string URL = Server.MapPath("~/EmailTemplate/Merchant/MerchantConfirmation.htm");
                        StreamReader SR = new StreamReader(URL);
                        EmailContent = SR.ReadToEnd();
                        SR.Close();
                        string ReferralId = dr["Social_Referral_Id"].ToString();
                        EmailContent = EmailContent.Replace("{SocialReferralId}", ReferralId);
                        EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                        

                        comman.SendMail(EmailId, "Confirmation Mail", EmailContent);
                        lblMsg.Text = "Your Account is Successfully Activated.";
                        //Response.Redirect("Index.aspx");
                    }
                }
                else
                {
                    //Response.Write("Email Id does not exist");
                    lblMsg.Text = "Email Id does not Exist";
                }
                DBAccess.InstanceCreation().disconnect();
            }
        }

        //protected void lnkClickHere_Click(object sender, EventArgs e)
        //{

        //    //_Merchant objMarchant = new _Merchant();
        //    //objMarchant.MerchantID = 1;
        //    //objMarchant.FirstName = "";
        //    //objMarchant.LastName = "";
        //    //objMarchant.EmailID = EmailId;
        //    //objMarchant.Password = Password;
        //    //objMarchant.ReferralEmailID = "";
        //    //objMarchant.CompanyName = "";
        //    //objMarchant.StreetAddress = "";
        //    //objMarchant.City = "";
        //    //objMarchant.State = "";
        //    //objMarchant.CountryID = "";
        //    //objMarchant.Zip = 0;
        //    //objMarchant.PhoneNumber = "";
        //    //objMarchant.Fax = 0;
        //    //objMarchant.AccountStatus = 0;
            

        //    //DAL.Plugin sqlPlugin = new DAL.Plugin();
        //    //SqlDataReader dr = sqlPlugin.MerchantLogin(objMarchant);

        //    //if (dr.Read())
        //    //{
        //    //    if (dr["IsActive"].ToString() == "True")
        //    //    {
        //    //        Response.Write("Email Id Already Activated");
        //    //    }
        //    //    else
        //    //    {
        //    //        objMarchant.IsActive = true;
        //    //        int result = sqlPlugin.InsertIntoMerchant_Master(objMarchant);
        //    //        string EmailContent = "";
        //    //        string URL = Server.MapPath("~/EmailTemplate/Merchant/MerchantConfirmation.htm");
        //    //        StreamReader SR = new StreamReader(URL);
        //    //        EmailContent = SR.ReadToEnd();
        //    //        SR.Close();

        //    //        comman.SendMail(EmailId, "Confirmation Mail", EmailContent);
        //    //        Response.Redirect("Index.aspx");
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    Response.Write("Email Id does not exist");
        //    //}
        //}

    }
}