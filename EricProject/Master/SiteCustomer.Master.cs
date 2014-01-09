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
using System.Xml;
using oAuthExample;
using System.IO;
using EricProject.Master;

namespace EricProject.Master
{
    public partial class SiteCustomer : System.Web.UI.MasterPage
    {
        public string name = "";
        public string username = "";
        public string profileImage = "";
        public string followersCount = "";
        public string noOfTweets = "";
        public string recentTweet = "";
        //public string PageURl;
        string consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
        string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        _plugin objPlugin = new _plugin();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Page_Init(object sender, EventArgs e)
        {
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            string RedirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
            if (Request.Cookies["CustomerEmailId"] != null && Request.Cookies["CustomerEmailId"].Value != "" && Request.Cookies["CustomerID"].Value != "")
            {
                Session["CustomerEmailId"] = Request.Cookies["CustomerEmailId"].Value;
                Session["CustomerID"] = Request.Cookies["CustomerID"].Value;
            }

            if (Session["CustomerID"] == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/Login?RedirectUrl=" + RedirectUrl);
            }

            if (Session["CustomerEmailId"] != null)
            {
                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
                if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "site/customer/login") || (currentURL == selfURL + "site/customer/login?"))
                {
                    Session["BottomLinkVisibleForCustomer"] = "Yes";
                }
                else
                {
                    Session["BottomLinkVisibleForCustomer"] = null;
                }
            }
        }
    }
}