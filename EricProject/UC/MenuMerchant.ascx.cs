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
    public partial class SiteMerchantHeader : System.Web.UI.UserControl
    {
        string id;
        
        protected void Page_Load(object sender, EventArgs e)
        {

         
            Menu_Image();
            if (Session["MerchantID"] != null)
                id = Session["MerchantID"].ToString();
            MerchantCredits();
        }
        protected void Menu_Image()
        {
            string str = Request.Url.PathAndQuery.ToLower();
            if (str == "/site/merchant/dashboard")
            {
                li_Home.Attributes.Add("class", "sel");
            }
            else if (str == "/site/how-it-works")
            {
                li_HowItWorks.Attributes.Add("class", "sel");
            }
            else if (str == "/site/learnmore")
            {
                li_Compare_Us.Attributes.Add("class", "sel");
            }
            else if (str == "/site/features")
            {
                li_Features.Attributes.Add("class", "sel");
            }
            else if (str == "/site/faq")
            {
                li_SiteFAQ.Attributes.Add("class", "sel");
            }
            else if (str == "/site/pricing")
            {
                li_Prices.Attributes.Add("class", "sel");
            }
            else if (str == "/site/merchant/documentation")
            {
                li_Documentation.Attributes.Add("class", "sel");
            }
            else
            {
                li_Home.Attributes.Add("class", "sel");
            }

        }

        public void MerchantCredits()
        {
            _Merchant_Customer_Credits obj = new _Merchant_Customer_Credits();
            obj.Status = 0;
            obj.Id = Convert.ToInt32(id);

            DAL.Plugin sql = new DAL.Plugin();
            SqlDataReader dr = sql.BindMerchantCustomerCredits(obj);
            if (dr.Read())
            {
                if(dr["TotalAvailableCredit"].ToString().Contains('-'))
                lblCreditsMerchant.Text = '-'+comman.FormatCredits(dr["TotalAvailableCredit"].ToString().Replace('-',' '));
                else
                lblCreditsMerchant.Text = comman.FormatCredits(dr["TotalAvailableCredit"].ToString().Replace('-', ' '));
                Session["TotalAvailablecreditsforrefund"] = dr["TotalAvailableCredit"].ToString().Replace('-', ' ');
            }
            else
            {
                lblCreditsMerchant.Text = "0";
            }
        }

        protected void lnklogo_Click(object sender, EventArgs e)
        {
            Session["BottomLinkVisibleForMerchant"] = "Yes";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Home");
        }
    }
}