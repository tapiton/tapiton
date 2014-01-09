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
    public partial class SiteMerchant : System.Web.UI.MasterPage
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
            //hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            //string RedirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
            //if (Request.Cookies["MerchantEmail"] != null && Request.Cookies["MerchantEmail"].Value != "" && Request.Cookies["MerchantId"].Value != "")
            //{
            //    Session["MerchantEmailId"] = Request.Cookies["MerchantEmail"].Value;
            //    Session["MerchantID"] = Request.Cookies["MerchantId"].Value;
            //}

            //if (Session["MerchantID"] == null)
            //    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Home?RedirectUrl=" + RedirectUrl);

        }
        protected void Page_Init(object sender, EventArgs e)
        {

            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            string RedirectUrl = Server.UrlEncode(Request.Url.AbsoluteUri);
            if (Request.Cookies["MerchantEmail"] != null && Request.Cookies["MerchantEmail"].Value != "" && Request.Cookies["MerchantId"].Value != "")
            {
                Session["MerchantEmailId"] = Request.Cookies["MerchantEmail"].Value;
                Session["MerchantID"] = Request.Cookies["MerchantId"].Value;
            }

            if (Session["MerchantID"] == null)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Home?RedirectUrl=" + RedirectUrl);

            if (Session["MerchantEmailId"] != null)
            {
                string currentURL = HttpContext.Current.Request.Url.AbsoluteUri.ToLower();
                string selfURL = ConfigurationManager.AppSettings["pageURL"].ToLower();
                if ((currentURL == selfURL + "home") || (currentURL == selfURL.Replace(".com/", ".com")) || (currentURL == selfURL + "site/index.aspx") || (currentURL == selfURL + "site/Merchant/login") || (currentURL == selfURL + "site/Merchant/login?"))
                {
                    Session["BottomLinkVisibleForMerchant"] = "Yes";
                }
                else
                {
                    Session["BottomLinkVisibleForMerchant"] = null;
                }
            }
        }
    }
}