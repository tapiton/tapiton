using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net.Http.Headers;
namespace EricProject.Plugin
{
    public partial class FaceBookLinkClick : System.Web.UI.Page
    {
        //  decimal Type_of_Reward_C;
        //string  Expiry_date;
        string browser;
        string WebsteURL, Product_Name = "", platform = "", OfferID = "", CampaignImage = "", TransactionID = "", ProductURl = "";
        public DateTime ExpiryDate = new DateTime();
        int Type_Of_Campaign, noofdays, Campaign_id = 0, Customer_id = 0;
        DateTime ExpDate;
        int resultReferrerURL, MerchantID;
        string URLWebsite = "", SKUID = ""; string urlstring;
        decimal Type_Of_Reward_C = 0, Type_of_Reward_R = 0, Type_C = 0, Type_R = 0, MinimumPurchaseAmount = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            browser = Request.Browser.Browser;
            if (browser == "IE")
            {
                HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO DSP COR CONo OUR LEG PUR UNI\"");
            }
            if (Page.RouteData.Values["OfferID"] != null)
            {
                Btnclose.Style.Add("display", "none");
                OfferID = Page.RouteData.Values["OfferID"].ToString();
                OfferIDtext.Value = OfferID;
                CampaignsDetails(Convert.ToInt32(OfferID));
                if (CampaignImage == "")
                {
                    imgsrc.Src = ConfigurationManager.AppSettings["pageURL"].ToString() + "images/download.jpg";
                    imgsrc.Style.Add("Border", "0px");
                }
                else
                {
                    imgsrc.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + CampaignImage;
                }
                URL.Value = (WebsteURL.ToString().IndexOf("http") < 0 ? "http://" + WebsteURL.ToString() : WebsteURL.ToString());

                //if (platform.Trim() == "3D-cart")

                if (URLWebsite == "socialreferral.onlineshoppingpool.com")
                {
                    HtmlControl contentPanel1 = (HtmlControl)this.FindControl("iframe1");
                    if (contentPanel1 != null)
                        contentPanel1.Attributes["src"] = ConfigurationManager.AppSettings["pageURL"].ToString() + "Temp/Products.aspx";
                }
                else
                {
                    if (ProductURl == "")
                    {
                        if (URLWebsite.Contains("http"))
                        {
                            HtmlControl contentPanel1 = (HtmlControl)this.FindControl("iframe1");
                            if (contentPanel1 != null)
                                contentPanel1.Attributes["src"] = URLWebsite;
                        }
                        else
                        {
                            HtmlControl contentPanel1 = (HtmlControl)this.FindControl("iframe1");
                            if (contentPanel1 != null)
                                contentPanel1.Attributes["src"] = "http://" + URLWebsite + "/";

                        }
                    }
                    else
                    {
                        if (ProductURl.Contains("http"))
                        {
                            HtmlControl contentPanel1 = (HtmlControl)this.FindControl("iframe1");
                            if (contentPanel1 != null)
                                contentPanel1.Attributes["src"] = ProductURl;
                        }
                        else
                        {
                            HtmlControl contentPanel1 = (HtmlControl)this.FindControl("iframe1");
                            if (contentPanel1 != null)
                                contentPanel1.Attributes["src"] = "http://" + ProductURl + "/";

                        }
                    }
                }

                //Facebook  link click Increment

                //DAL.Plugin Campaignsqlobj = new DAL.Plugin();
                //_TransactionDetails TransOBJ = new _TransactionDetails();
                //TransOBJ.Transaction_ID = Convert.ToInt32(OfferID);
                //SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);               
                if (!IsPostBack)
                {

                    string source = Page.RouteData.Values["Source"].ToString();
                    string url = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();
                    string CurrentURl = Request.Url.ToString();
                    //comman.SendMail("tanu_garg@seologistics.com", url, source);                     
                    if (url.IndexOf("goo.gl") == 7)
                    {
                    }
                    else
                    {

                        if (url == "" && source == "T" && !CurrentURl.Contains("tapiton.com"))
                        {
                        }
                        else
                        {
                            if (url == "" && source == "F" && !CurrentURl.Contains("tapiton.com"))
                            {
                            }
                            else
                            {
                                _ReferrerURL objReferrerURL = new _ReferrerURL();
                                DAL.Plugin ReferrerURLobj = new DAL.Plugin();
                                objReferrerURL.UrlReferrer_ID = 0;
                                objReferrerURL.Offer_ID = Convert.ToInt32(OfferID);
                                objReferrerURL.Referrer_ID = 0;
                                objReferrerURL.URL = source;
                                objReferrerURL.Status = "Not Purchased";
                                resultReferrerURL = ReferrerURLobj.InsertIntoReferrerURL(objReferrerURL);
                                ViewState["resultReferrerURL"] = resultReferrerURL;

                                _Campaigns_Stats objCampaign_Stats = new _Campaigns_Stats();
                                DAL.Plugin Campaignssqlobj = new DAL.Plugin();
                                objCampaign_Stats.Campaign_Id = Convert.ToInt32(Campaign_id.ToString());
                                objCampaign_Stats.FB_click = 0;
                                objCampaign_Stats.FBShare_Click = 0;
                                objCampaign_Stats.Link_Click = 1;
                                objCampaign_Stats.Proceed_Click = 0;
                                objCampaign_Stats.Tweet_Click = 0;
                                objCampaign_Stats.Email_Click = 0;
                                objCampaign_Stats.EmailSubmit_Click = 0;
                                objCampaign_Stats.StatusClick = 3;
                                int resultFacebookShareClick = Campaignssqlobj.InsertInToCampaignsStats(objCampaign_Stats);


                                if (source == "O")
                                {
                                    _Offer sqlOffer = new _Offer();
                                    sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + OfferID;
                                    sqlOffer.Customer_Id = Customer_id.ToString();
                                    sqlOffer.Campaign_Id = Campaign_id.ToString();
                                    sqlOffer.Expiry_Time = ExpDate;
                                    sqlOffer.Clicks = "1";
                                    sqlOffer.Reach = "0";
                                    sqlOffer.Referrals = 0;
                                    sqlOffer.Sales = 0;
                                    sqlOffer.Referrer_Credits = Type_of_Reward_R;
                                    sqlOffer.Status = 2;
                                    sqlOffer.TransactionId = Convert.ToInt32(TransactionID);
                                    DAL.Plugin offerobj = new DAL.Plugin();
                                    offerobj.InsertInToOfferDetails(sqlOffer);
                                    LinkClick();
                                }
                            }
                        }
                    }
                }
                // Response.Write(resultReferrerURL);           
                // int cookievalues;


                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alertScript", "<script language='javascript'>var a=CookieSetText();alert(a);document.getElementById('<%= testcookie.ClientID%>').value=a;</script>");
                //Response.Write(Request.Form["hidden"].ToString());
                //if (Request.Form["hidden"] == "false")
                //{
                //    Response.Write("cookie1");
                //    //if (Session["cookieEnabled"].ToString() == "No")
                //    //{
                //        Response.Write("cookie2");
                //        btnprocced.Enabled = false;
                //        btnprocced.Visible = false;
                //        lbltest.Visible = true;
                //        Lblexpired.Text = "Please enable cookies to redeem this offer";
                //        Btnclose.Visible = true;
                //        //HttpContext.Current.Response.Cookies["CookieEnable"].Expires = DateTime.Now;
                //    //}
                //}

                if (ExpDate < DateTime.Now)
                {
                    btnprocced.Enabled = false;
                    btnprocced.Visible = false;
                    lbltest.Visible = true;
                    Lblexpired.Text = "This offer has expired or is no longer being honored.";
                    Btnclose.Style.Add("display", "block");
                }
                if (!MerchantCredits())
                {
                    btnprocced.Enabled = false;
                    btnprocced.Visible = false;
                    lbltest.Visible = true;
                    Lblexpired.Text = "This offer has expired or is no longer being honored.";
                    DateTime current = DateTime.Now.AddDays(-1);
                    HiddenDate.Value = current.Day.ToString();
                    HiddenMonth.Value = current.Month.ToString();
                    HiddenHour.Value = current.Hour.ToString();
                    HiddenMin.Value = current.Minute.ToString();
                    HiddenSec.Value = current.Second.ToString();
                    Btnclose.Style.Add("display", "block");
                }
                if (!MerchantNocampaigns())
                {
                    btnprocced.Enabled = false;
                    btnprocced.Visible = false;
                    lbltest.Visible = true;
                    Lblexpired.Text = "This offer has expired or is no longer being honored.";
                    DateTime current = DateTime.Now.AddDays(-1);
                    HiddenDate.Value = current.Day.ToString();
                    HiddenMonth.Value = current.Month.ToString();
                    HiddenHour.Value = current.Hour.ToString();
                    HiddenMin.Value = current.Minute.ToString();
                    HiddenSec.Value = current.Second.ToString();
                    // Btnclose.Visible = true;
                    Btnclose.Style.Add("display", "block");
                }
                MessageDisplay();

                //  Facebook  link click Increment
            }
        }
        public void LinkClick()
        {
            _Campaigns_Stats objCampaign_Stats = new _Campaigns_Stats();
            DAL.Plugin Campaignssqlobj = new DAL.Plugin();
            objCampaign_Stats.Campaign_Id = Convert.ToInt32(Campaign_id.ToString());
            objCampaign_Stats.FB_click = 0;
            objCampaign_Stats.FBShare_Click = 0;
            objCampaign_Stats.Link_Click = 1;
            objCampaign_Stats.Proceed_Click = 0;
            objCampaign_Stats.Tweet_Click = 0;
            objCampaign_Stats.Email_Click = 0;
            objCampaign_Stats.EmailSubmit_Click = 0;
            objCampaign_Stats.StatusClick = 9;
            int resultFacebookShareClick = Campaignssqlobj.InsertInToCampaignsStats(objCampaign_Stats);
        }
        public void MessageDisplay()
        {
            if (Type_R == 1)
            {
                if (MinimumPurchaseAmount == 0)
                {
                    if (SKUID == "0")
                    {
                        Money.Text = "$" + Type_Of_Reward_C.ToString() + " off your purchase";
                    }
                    else
                    {
                        Money.Text = "$" + Type_Of_Reward_C.ToString() + " off your " + Product_Name + " Purchase";
                    }
                }
                else
                    if (SKUID == "0")
                    {
                        Money.Text = " $" + Type_Of_Reward_C.ToString() + " off your $" + MinimumPurchaseAmount + " purchase";
                    }
                    else
                    {
                        Money.Text = " $" + Type_Of_Reward_C.ToString() + " off your $" + MinimumPurchaseAmount + " purchase of " + Product_Name;
                    }
            }
            else
            {
                if (MinimumPurchaseAmount == 0)
                {
                    if (SKUID == "0")
                    {

                        Money.Text = "" + Type_Of_Reward_C.ToString() + "% off your purchase";

                    }
                    else
                    {
                        Money.Text = "" + Type_Of_Reward_C.ToString() + "% off your " + Product_Name + " Purchase";

                    }
                }
                else
                    if (SKUID == "0")
                    {
                        Money.Text = "" + Type_Of_Reward_C.ToString() + "% off your $" + MinimumPurchaseAmount + " purchase";

                    }
                    else
                    {
                        Money.Text = "" + Type_Of_Reward_C.ToString() + "% off your $" + MinimumPurchaseAmount + " purchase of " + Product_Name;

                    }

            }
        }
        public bool MerchantCredits()
        {
            _Merchant_Customer_Credits obj = new _Merchant_Customer_Credits();
            obj.Status = 0;
            obj.Id = Convert.ToInt32(MerchantID);
            DAL.Plugin sql = new DAL.Plugin();
            SqlDataReader dr = sql.BindMerchantCustomerCredits(obj);
            if (dr.Read())
            {
                if (dr["TotalAvailableCredit"].ToString().Contains('-'))
                {
                    return false;
                }
                else
                    return true;
            }
            else
                return false;
        }
        public bool MerchantNocampaigns()
        {
            _Merchant obj = new _Merchant();
            obj.MerchantID = Convert.ToInt32(MerchantID);
            obj.CampaignId = Convert.ToInt32(Campaign_id);
            DAL.Plugin sql = new DAL.Plugin();
            SqlDataReader dr = sql.TotalActiveCampaignsByID(obj);
            if (dr.Read())
            {
                if (Convert.ToInt32(dr["TotalActiveCampaigns"].ToString()) > 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public void CampaignsDetails(int OfferID)
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);
            if (CampaignDR.Read())
            {
                //Money.Text = Convert.ToDecimal(CampaignDR["Customer_reward"]).ToString();
                platform = CampaignDR["Platform"].ToString();
                OfferValid.Value = CampaignDR["noofdays"].ToString();
                Type_Of_Reward_C = Convert.ToDecimal(CampaignDR["Customer_reward"].ToString());
                Campaign_id = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Customer_id = Convert.ToInt32(CampaignDR["Customer_Id"].ToString());
                CampaignImage = CampaignDR["Campaign_Image"].ToString();
                Type_of_Reward_R = Convert.ToDecimal(CampaignDR["Referrer_reward"].ToString());
                Customerreferrer.Text = Type_of_Reward_R.ToString();
                Type_Of_Campaign = Convert.ToInt32(CampaignDR["Campaign_Type"].ToString());
                noofdays = Convert.ToInt32(CampaignDR["noofdays"].ToString());
                Hiddennoofdays.Value = CampaignDR["noofdays"].ToString();
                SKUID = CampaignDR["SKU_ID"].ToString();
                Type_C = Convert.ToDecimal(CampaignDR["Customer_reward_type"].ToString());
                Type_R = Convert.ToDecimal(CampaignDR["Referrer_reward_type"].ToString());
                Product_Name = CampaignDR["Product_Name"].ToString();
                URLWebsite = CampaignDR["Website"].ToString().Trim();
                WebsiteURL.Value = URLWebsite;
                WebsteURL = CampaignDR["Platform"].ToString();
                ProductURl = CampaignDR["ProductURl"].ToString();
                MinimumPurchaseAmount = Convert.ToDecimal(CampaignDR["Min_purchase_amt"].ToString());
                HiddenGetDate.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).ToShortDateString();
                ExpDate = Convert.ToDateTime(CampaignDR["Expiry_date"]);
                HiddenDate.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Day.ToString();
                HiddenMonth.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Month.ToString();
                HiddenHour.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Hour.ToString();
                HiddenMin.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Minute.ToString();
                HiddenSec.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Second.ToString();
                MerchantID = Convert.ToInt32(CampaignDR["Merchant_ID"].ToString());
                TransactionID = CampaignDR["Transaction_ID"].ToString();
                HiddenYear.Value = Convert.ToDateTime(CampaignDR["Expiry_date"].ToString()).Year.ToString();
            }
            else
            {
                // Response.End();
            }
            DBAccess.InstanceCreation().disconnect();

        }

        protected void btnprocced_Click(object sender, ImageClickEventArgs e)
        {

            CampaignsDetails(Convert.ToInt32(OfferID));
            if (!MerchantCredits())
            {
                // Btnclose.Visible = true;
                Btnclose.Style.Add("display", "block");
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('This offer has expired or is no longer being honored by the merchant. Please contact the merchant for an extension of the offer.');", true);
            }
            else
            {
                if (ExpDate < DateTime.Now)
                {
                    //Btnclose.Visible = true;
                    Btnclose.Style.Add("display", "block");
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Sorry!! This coupon has been expired.');", true);
                }
                else
                {
                    // Response.Write(ViewState["resultReferrerURL"].ToString());
                    DAL.Plugin Campaignsqlobj = new DAL.Plugin();
                    _TransactionDetails TransOBJ = new _TransactionDetails();
                    TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
                    SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);
                    _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();
                    if (CampaignDR.Read())
                    {
                        objCampaigns_Stats.Campaign_Id = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                        objCampaigns_Stats.StatusClick = 4;
                        int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
                    }
                    //DBAccess.InstanceCreation().disconnect();     

                    //if (browser == "IE")
                    //{
                    //    Response.Cookies["OfferID"].Value = Page.RouteData.Values["OfferID"].ToString();                       
                    //    Response.Cookies["1HourTransaction"].Value = Page.RouteData.Values["OfferID"].ToString();
                    //    Response.Cookies["1HourTransaction"].Expires = DateTime.Now.AddHours(1);
                    //    Response.Cookies["ReferrerURL"].Value = ViewState["resultReferrerURL"].ToString();
                    //    Response.Cookies["ReferrerURL"].Expires = Convert.ToDateTime(ExpDate.ToString());
                    //}
                    //else
                    //{
                    Response.Cookies["OfferID"].Value = Page.RouteData.Values["OfferID"].ToString();
                    Response.Cookies["OfferID"].Expires = Convert.ToDateTime(ExpDate.ToString());
                    Response.Cookies["1HourTransaction"].Value = Page.RouteData.Values["OfferID"].ToString();
                    Response.Cookies["1HourTransaction"].Expires = DateTime.Now.AddHours(1);
                    Response.Cookies["ReferrerURL"].Value = ViewState["resultReferrerURL"].ToString();
                    Response.Cookies["ReferrerURL"].Expires = Convert.ToDateTime(ExpDate.ToString());
                    // }

                    //Response.Cookies["OfferID"].Domain = "flexsin.org/lab/socialeref/index.php";
                    //  Response.Cookies["OfferID"].Path= "flexsin.org/lab/socialeref/index.php";
                    //HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
                    //var cookie = new CookieHeaderValue("OfferID", Page.RouteData.Values["OfferID"].ToString());
                    //cookie.Domain = "http://www.flexsin.org/lab/socialeref/index.php";
                    //cookie.Expires = ExpDate;

                    //Cache["OfferID"] = Page.RouteData.Values["OfferID"].ToString();
                    // Cache.Insert("OfferID", Page.RouteData.Values["OfferID"].ToString(), null, Convert.ToDateTime(ExpDate.ToString()), System.Web.Caching.Cache.NoSlidingExpiration);
                    // Cache.Insert("1HourTransaction", Page.RouteData.Values["OfferID"].ToString(), null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                    //  Cache.Insert("ReferrerURL", ViewState["resultReferrerURL"].ToString(), null, Convert.ToDateTime(ExpDate.ToString()), System.Web.Caching.Cache.NoSlidingExpiration);

                    if (URLWebsite == "socialreferral.onlineshoppingpool.com")
                    {
                        urlstring = ConfigurationManager.AppSettings["pageURL"].ToString() + "Temp/Products.aspx";
                    }
                    else
                    {
                        if (ProductURl == "")
                        {
                            if (URLWebsite.Contains("http"))
                                urlstring = URLWebsite;
                            else
                                urlstring = "http://" + URLWebsite + "/";
                        }
                        else
                        {
                            if (ProductURl.Contains("http"))
                                urlstring = ProductURl;
                            else
                                urlstring = "http://" + ProductURl + "/";
                        }
                    }
                    // if (platform.Trim() == "3D-cart")
                    Response.Redirect(urlstring);
                    // else
                    //  Response.Redirect("http://v1278348.fmwtaqsuzve7.demo13.volusion.com/");
                }
            }
        }

        protected void Btnclose_Click(object sender, EventArgs e)
        {

            if (URLWebsite == "socialreferral.onlineshoppingpool.com")
            {
                urlstring = ConfigurationManager.AppSettings["pageURL"].ToString() + "Temp/Products.aspx";
            }
            else
            {
                if (URLWebsite.Contains("http"))
                    urlstring = URLWebsite;
                else
                    urlstring = "http://" + URLWebsite + "/";
            }
            // if (platform.Trim() == "3D-cart")
            Response.Redirect(urlstring);

        }
    }
}