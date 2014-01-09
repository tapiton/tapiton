using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
namespace EricProject.Site
{
    public partial class Credit_Card_Listing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BAL._credit_details objcredit = new BAL._credit_details();
            objcredit.Merchant_ID = Convert.ToInt32(Session["MerchantID"].ToString());
            DAL.Plugin objplugin = new DAL.Plugin();
            SqlDataReader DR = objplugin.getcreditcarddetails(objcredit);
            if (DR.Read())
            {
                txtcardholdername.Text = DR["Cardholder_Name"].ToString();
                TxtTokenNumber.Text = "XXXXXXXXXXXX" + DR["TransarmorToken"].ToString().Substring(12);
                txtcardtype.Text = DR["Card_Type"].ToString();
                int date = Convert.ToInt32(DR["Expiry_Date"].ToString().Substring(2));
                txtexpirydate.Text = DR["Expiry_Date"].ToString().Substring(0, 2) + "/20" + date;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Session["IsSubscriptionEnd"] != null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Subscription");
            }
            else if (Session["RenewSubscription"] != null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Renew_subscription");
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AccountDetails");
            }
            
        }

        protected void aEditCardDetails_Click(object sender, EventArgs e)
        {
            if (Session["IsSubscriptionEnd"] != null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/AutoSubscription");
            }
           
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/ModifyDetails");
            }
        }
   
    }
}