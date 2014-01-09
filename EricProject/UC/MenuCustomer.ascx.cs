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

namespace EricProject.UC
{
    public partial class MenuCustomer : System.Web.UI.UserControl
    {
        string id;
        string RefferalURL = "";
        protected void Page_Load(object sender, EventArgs e)
        {
          
            string str = Request.Url.PathAndQuery.ToLower();
            if (str == "/site/customer/dashboard")
            {
                li_Home.Attributes.Add("class", "sel");
            }
            else if (str == "/site/faq")
            {
                li_SiteFAQ.Attributes.Add("class", "sel");
            }
            else
            {
                li_Home.Attributes.Add("class", "sel");
            }

            if (Session["CustomerID"] != null)
            {
                id = Session["CustomerID"].ToString();
            }

            CustomerCredits();
        }

        public void CustomerCredits()
        {
            _Merchant_Customer_Credits obj = new _Merchant_Customer_Credits();
            obj.Status = 1;
            obj.Id = Convert.ToInt32(id);

            DAL.Plugin sql = new DAL.Plugin();
            SqlDataReader dr = sql.BindMerchantCustomerCredits(obj);
            if (dr.Read())
            {
                lblCreditsCustomer.Text = comman.FormatCredits(dr["Credit_Received"]);
            }
            else
            {
                lblCreditsCustomer.Text = "0";
            }
        }

        protected void ibtnLogo_Click(object sender, ImageClickEventArgs e)
        {
            Session["BottomLinkVisibleForCustomer"] = "Yes";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Home");
        }
    }
}