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


public partial class Site_MerchantProfile : System.Web.UI.Page
{
    _Merchant objMarchant = new _Merchant();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    DAL.Admin sqlAdmin = new DAL.Admin();
  
    protected void Page_Load(object sender, EventArgs e)
    {
   
        SpanSuccess.Visible = false;

        if (Session["MerchantId"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "home");

        if (!IsPostBack)
        {
            if (Session["MerchantID"] != null)
            {
                this.PopulateCountry();
                this.PopulateEcomPlatform();
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
                if (drPlugin.Read())
                {
                    txtFirstName.Value = drPlugin["First_Name"].ToString();
                    txtLastName.Value = drPlugin["Last_Name"].ToString();
                    lblEmail.Text = drPlugin["Email_Id"].ToString();
                    txtCompanyName.Value = drPlugin["Company_Name"].ToString();
                    txtAddress.Value = drPlugin["Street_Address"].ToString();
                    txtCity.Value = drPlugin["City"].ToString();
                    txtState.Value = drPlugin["State"].ToString();
                    ddlCountry.SelectedValue = drPlugin["Country_Id"].ToString();
                    txtZip.Value = drPlugin["zip"].ToString();
                    txtPhone.Value = drPlugin["Phone_Number"].ToString();
                    txtFax.Value = drPlugin["Fax"].ToString();
                    ddlPendingDate.SelectedValue = drPlugin["pending_Credit_duration"].ToString();
                    ddlEcomPlatform.SelectedValue = drPlugin["Ecom_PlatformId"].ToString();
                    txtWebsiteURL.Value = drPlugin["Website"].ToString();
                    lblsocialreferralid.Text = drPlugin["Social_Referral_Id"].ToString();
                    isintegrated.Value = drPlugin["IsIntegrated"].ToString();
                    MerchantWebsiteURL.Value = drPlugin["Website"].ToString();
                    MerchantPlatForm.Value = drPlugin["Ecom_PlatformId"].ToString();
                }
                DBAccess.InstanceCreation().disconnect();
            }
          
        }
       
    }


    public void PopulateCountry()
    {
        //ddlCountry.Items.Clear();
        _Country objCountry = new _Country();
        objCountry.ID = 0;
        SqlDataReader drAdmin = sqlAdmin.BindCountry(objCountry);
        while (drAdmin.Read())
        {
            ddlCountry.Items.Add(new ListItem(drAdmin["Country_Name"].ToString(), drAdmin["Country_Id"].ToString()));
            
        }
        ddlCountry.Items.Insert(0, new ListItem("Choose Country", "0"));
        DBAccess.InstanceCreation().disconnect();
    }

    public void PopulateEcomPlatform()
    {
        //ddlEcomPlatform.Items.Clear();
        _ECommercePlatForm objECommercePlatForm = new _ECommercePlatForm();
        objECommercePlatForm.ID = 0;
        SqlDataReader drECommercePlatForm = sqlAdmin.BindECommerce(objECommercePlatForm);
        while (drECommercePlatForm.Read())
        {
            ddlEcomPlatform.Items.Add(new ListItem(drECommercePlatForm["Ecommerce_Platform_Name"].ToString(), drECommercePlatForm["Ecom_Platform_Id"].ToString()));

        }
        ddlEcomPlatform.Items.Insert(0, new ListItem("Choose Platform", "0"));
        DBAccess.InstanceCreation().disconnect();
    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        if (MerchantWebsiteURL.Value != txtWebsiteURL.Value || MerchantPlatForm.Value != ddlEcomPlatform.SelectedValue)
        {
                    _Merchant objmerchantintegration = new _Merchant();
                    objmerchantintegration.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                    DAL.Plugin objmerchant = new Plugin();
                    int i = objmerchant.UpdateStatus_Merchant_Integration_Status(objmerchantintegration);
        }
      
            DAL.Plugin sql = new DAL.Plugin();
            _Merchant_website_details objwebsite = new _Merchant_website_details();
            objwebsite.Website = txtWebsiteURL.ToString();
            objwebsite.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
            SqlDataReader dr = sql.Dublicate_MerchantWebsite(objwebsite);
            while (dr.Read())
            {
                if (dr["result"].ToString() == "1")
                {
                    objMarchant.MerchantID = Convert.ToInt32(Session["MerchantId"].ToString());
                    objMarchant.FirstName = txtFirstName.Value;
                    objMarchant.LastName = txtLastName.Value;
                    objMarchant.EmailID = lblEmail.Text;
                    objMarchant.Password = "";
                    objMarchant.CompanyName = txtCompanyName.Value;
                    objMarchant.StreetAddress = txtAddress.Value;
                    objMarchant.City = txtCity.Value;
                    objMarchant.State = txtState.Value;
                    objMarchant.CountryID = ddlCountry.SelectedValue;
                    objMarchant.Zip = txtZip.Value;
                    objMarchant.PhoneNumber = txtPhone.Value;
                    objMarchant.Fax = txtFax.Value;
                    objMarchant.pending_Credit_duration = Convert.ToInt32(ddlPendingDate.Text);
                    //objMarchant.Ecom_Platform_Id = ddlEcomPlatform.SelectedValue;
                    //objMarchant.WebsiteUrl = txtWebsiteURL.Value;
                    sqlPlugin.UpdateMerchantMasterById(objMarchant);

                    _Merchant_website_detail objmerchantwebsite = new _Merchant_website_detail();
                    objmerchantwebsite.Merchant_ID = Convert.ToInt32(Session["MerchantId"].ToString());
                    objmerchantwebsite.ECom_platformID = Convert.ToInt32(ddlEcomPlatform.SelectedValue);
                    objmerchantwebsite.Website = txtWebsiteURL.Value.Trim();
                    int merchantwebsite = sqlPlugin.UpdateMerchantWebsiteDetailsById(objmerchantwebsite);
                    SpanSuccess.Visible = true;

                }
                else
                {
                    Response.Write("<script>alert('Website already exists')</script>");
                }
            }
        
        //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Dashboard");
    }
}
