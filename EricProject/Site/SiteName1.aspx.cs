using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.IO;
namespace EricProject.Site
{
    public partial class SiteName : System.Web.UI.Page
    {
        _Merchant objMarchant = new _Merchant();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
            if (drPlugin.Read())
            {

                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                objMarchant.FirstName = drPlugin["First_Name"].ToString();
                objMarchant.LastName = drPlugin["Last_Name"].ToString();
                objMarchant.EmailID = drPlugin["Email_Id"].ToString();
                objMarchant.Password = "";
                objMarchant.CompanyName = txtsitename.Text;
                objMarchant.StreetAddress = drPlugin["Street_Address"].ToString();
                objMarchant.City = drPlugin["City"].ToString();
                objMarchant.State =drPlugin["State"].ToString();
                objMarchant.CountryID = drPlugin["Country_Id"].ToString();
                objMarchant.Zip =  drPlugin["zip"].ToString();
                objMarchant.PhoneNumber = drPlugin["Phone_Number"].ToString();
                objMarchant.Fax =drPlugin["Fax"].ToString();
                objMarchant.pending_Credit_duration = Convert.ToInt32(drPlugin["pending_Credit_duration"].ToString());
                //objMarchant.Ecom_Platform_Id = ddlEcomPlatform.SelectedValue;
                //objMarchant.WebsiteUrl = txtWebsiteURL.Value;
                sqlPlugin.UpdateMerchantMasterById(objMarchant);

                _Merchant_website_detail objmerchantwebsite = new _Merchant_website_detail();
                objmerchantwebsite.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                objmerchantwebsite.ECom_platformID = Convert.ToInt32(drPlugin["Ecom_PlatformId"].ToString());
                objmerchantwebsite.Website = txtwebsiteurl.Text.Trim();
                int merchantwebsite = sqlPlugin.UpdateMerchantWebsiteDetailsById(objmerchantwebsite);
            }
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Dashboard");
        }
    }
}