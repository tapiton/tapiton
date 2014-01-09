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


public partial class Site_MerchantAccount : System.Web.UI.Page
{
    _Merchant objMarchant = new _Merchant();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    DAL.Admin sqlAdmin = new DAL.Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MerchantID"] == null)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "site/home");
        }
        this.PopulateCountry();
        this.PopulateEcomPlatform();
        if (!IsPostBack)
        {
            if (Session["MerchantID"] != null)
            {
                objMarchant.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());


                SqlDataReader drPlugin = sqlPlugin.BindMerchantById(objMarchant);
                if (drPlugin.Read())
                {
                    txtFirstName.Value = drPlugin["FirstName"].ToString();
                    txtLastName.Value = drPlugin["LastName"].ToString();
                    txtEmail.Value = drPlugin["EmailID"].ToString();
                    txtCompanyName.Value = drPlugin["CompanyName"].ToString();
                    txtAddress.Value = drPlugin["StreetAddress"].ToString();
                    txtCity.Value = drPlugin["City"].ToString();
                    txtState.Value = drPlugin["State"].ToString();
                    ddlCountry.SelectedValue = drPlugin["Country_ID"].ToString();
                    txtZip.Value = drPlugin["zip"].ToString();
                    txtPhone.Value = drPlugin["PhoneNumber"].ToString();
                    txtFax.Value = drPlugin["Fax"].ToString();
                    ddlEcomPlatform.SelectedValue = drPlugin["Ecom_Platform_Id"].ToString();
                    txtWebsiteURL.Value = drPlugin["WebsiteUrl"].ToString();
                }
            }
        }
    }


    public void PopulateCountry()
    {
        _Country objCountry = new _Country();
        objCountry.ID = 0;
        SqlDataReader drAdmin = sqlAdmin.BindCountry(objCountry);
        while (drAdmin.Read())
        {
            ddlCountry.Items.Add(new ListItem(drAdmin["Country_Name"].ToString(), drAdmin["Country_Id"].ToString()));
            
        }
        ddlCountry.Items.Insert(0, new ListItem("Choose Country", "0"));
        
    }

    public void PopulateEcomPlatform()
    {
        _ECommercePlatForm objECommercePlatForm = new _ECommercePlatForm();
        objECommercePlatForm.ID = 0;
        SqlDataReader drECommercePlatForm = sqlAdmin.BindECommerce(objECommercePlatForm);
        while (drECommercePlatForm.Read())
        {
            ddlEcomPlatform.Items.Add(new ListItem(drECommercePlatForm["Ecommerce_Platform_Name"].ToString(), drECommercePlatForm["Ecom_Platform_Id"].ToString()));

        }
        ddlEcomPlatform.Items.Insert(0, new ListItem("Choose Platform", "0"));

    }
    protected void btnSaveDetails_Click(object sender, EventArgs e)
    {
        objMarchant.MerchantID = Convert.ToInt32(Session["MerchantID"].ToString());
        objMarchant.FirstName = txtFirstName.Value;
        objMarchant.LastName = txtLastName.Value;
        objMarchant.EmailID = txtEmail.Value;
        objMarchant.CompanyName = txtCompanyName.Value;
        objMarchant.StreetAddress = txtAddress.Value;
        objMarchant.City = txtCity.Value;
        objMarchant.State = txtState.Value;
        objMarchant.CountryID = ddlCountry.SelectedValue;
        objMarchant.Zip = txtZip.Value;
        objMarchant.PhoneNumber = txtPhone.Value;
        objMarchant.Fax = txtFax.Value;
        objMarchant.Ecom_Platform_Id = ddlEcomPlatform.SelectedValue;
        objMarchant.WebsiteUrl = txtWebsiteURL.Value;
        int result = sqlPlugin.UpdateMerchantMasterById(objMarchant);

        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Dashboard");
    }
}
