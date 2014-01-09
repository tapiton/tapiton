using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessObject;
using BAL;
using DAL;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web.Routing;
using Encryption64;
using System.Configuration;
using EricProject.LiveCreditCard;
using System.Web.UI;

namespace EricProject.Plugin
{
    /// <summary>
    /// Summary description for Plugin
    /// </summary>
    public class Plugin : IHttpHandler
    {
        string EmailId = "", OrderID = "", Platform = "", WebsiteName = "", FirstName = "", LastName = "", Total = "", Tax = "", Tax2 = "", Tax3 = "", Discount = "", Shipping = "", SubTotal = "", socailreferralID = "";
        SqlDataReader DRIntegration, CampaigndetailsDR;
        Decimal G_MaximumCreadits = 0.0M, G_Price = 0.0M, savingreferrerprice = 0, savingreferrerprice_C = 0, savingCustomerprice = 0, savingCustomerprice_C = 0, Credit_For_Customer, Credit_For_Customer_C, CreditAvailableDetails, TotalPendingCreditAvailable;
        string G_ProductName = "", G_SKUID = "", TotalAmount = "", ProductID = "", itemname = "", ReferrerReward = "", MessageMoneyBack = "", ProductName = "", CustomerReward;
        _CampaignsDetails CampaignDetails = new _CampaignsDetails();
        _CampaignsDetails CampaignDetails_C = new _CampaignsDetails();
        _plugin CustomerDetails = new _plugin();
        _plugin NewCustomerDetails = new _plugin();
        int Quantity = 0, TotalQuantitiy_C, TotalQuantity, CookieID = 0, TransactionID = 0, Credit_For_Referrer, G_Quantity, Merchant_ID, G_Quantity_C = 1, Credit_For_Referrer_C, TransactionFee;
        string SKU_ID_Referrer;
        int Creditsbelowzero = 0; int hourcoupon = 0, offerID, HourCookieID;
        string G_ProductName_C = "", G_SKUID_C = "";
        Decimal G_MaximumCreadits_C = 0.0M, G_Price_C = 0.0M;
        string PublicKey = "";
        public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/O/";
        public string strShortURL = "";
        int TotalAvailableCreditPurchase, thresholdAmount, WebsiteIDForTransaction;
        string PlanAmont;
        long PlanCredit;
        decimal totalcredits;
        System.Threading.Thread threadSendMails;
        public Plugin()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        public void ProcessRequest(HttpContext context)
        {
            //string  browser =HttpContext.Current.Request.Browser.Browser;
            //if (browser == "IE")
            //{
            //    HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO DSP COR CONo OUR LEG PUR UNI\"");
            //}

            //HttpContext.Current.Response.AddHeader("p3p", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            var routeValues = GetQueryStringValues(context);

            // Check Hostdetails and update the website and platform if null
            if (!CheckHostDetails())
            {
                // log code
                InsertIntoLog(0, 0, EmailId, true, false, false, false, false, false);
                return;
            }
            // Integrate the code
            if (EmailId.ToLower() == "integration@tapiton.com")
                CheckIntegrationCode(context);
            // Plugin Code Continue
            else
            {
                GetMerchantID();
                // Check Isintegrity and isactive
                if (!CheckMerchantIntegration())
                {
                    InsertIntoLog(Merchant_ID, 0, EmailId, false, true, false, false, false, false);
                    return;
                }
                AddNewCustomer(FirstName, LastName, EmailId);
                if (!CheckMerchantIsActive())
                {
                    InsertIntoLog(Merchant_ID, NewCustomerDetails.Customer_ID, EmailId, false, false, false, false, true, false);
                    return;
                }
                if (!CheckMerchantCredits())
                {
                    return;
                }
                checkCookie();
                CreditAvailable();

                if (HttpContext.Current.Request.Cookies["1HourTransaction"] != null)
                {
                    CheckSKUForCustomer(routeValues);
                    if (ValidateCampaign1hour(routeValues))
                    {
                        if (!CheckOrderDetails())
                        {
                            HourCookieID = Convert.ToInt32(HttpContext.Current.Request.Cookies["1HourTransaction"].Value);
                            if (Checkcookie_Transaction(CookieID))
                            {
                                hourcoupon = 1;
                                HttpContext.Current.Response.Cookies["1HourTransaction"].Expires = DateTime.Now;
                            }
                        }
                    }
                    //HttpContext.Current.Cache.Remove("1HourTransaction");
                }

                if (ValidateCampaign(routeValues))
                {
                    if (CookieID > 0)
                    {
                        CheckSKUForCustomer(routeValues);
                    }

                    if (!CheckOrderDetails())
                    {
                        Total = Total.Replace('$', ' ');
                        if (CookieID > 0)
                        {
                            EnterTotalDetails();
                        }
                        else
                        {
                            InsertIntoTransaction();
                        }
                        Save_Product_Details(routeValues);
                        if (Creditsbelowzero == 1)
                            couponShow1Hour();                      
                        else
                        {
                            if (CookieID > 0)
                            {
                                couponShow();
                                Campaign_stats();
                                referral();
                                sales();
                                referral_count();
                                updateReferrerURL();
                            }
                            else if (hourcoupon == 1)
                            {
                                customerEmailID(HourCookieID);
                                CookieID = HourCookieID;
                                Creditsbelowzero = 1;
                                EnterTotalDetails();
                                Save_Product_Details(routeValues);
                                couponShow1Hour();
                                updateReferrerURL();
                            }
                            else
                            {
                                insertintooffer();
                                SendmailMoneyback();
                                couponShow();
                                Campaign_stats();
                                referral();
                                sales();
                                referral_count();
                            }


                        }
                    }
                }
                else if (hourcoupon == 1)
                {
                        customerEmailID(HourCookieID);
                        CookieID = HourCookieID;
                        Creditsbelowzero = 1;
                        EnterTotalDetails();
                        Save_Product_Details(routeValues);
                        couponShow1Hour();
                        updateReferrerURL();
                }
                else
                {
                    InsertIntoLog(Merchant_ID, NewCustomerDetails.Customer_ID, EmailId, false, false, true, false, false, false);
                    AddtransactionDetails(NewCustomerDetails.Customer_ID, 0, WebsiteIDForTransaction, 0, OrderID, Quantity, "0", Total, SubTotal, Discount, Tax, Tax2, Tax3, Shipping, 0);
                    Save_Product_Details(routeValues);
                    //if (CookieID > 0)
                    //context.Response.Write("alert('This offer has expired or is no longer being honored by the merchant. Please contact the merchant for an extension of the offer.');");
                }
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        private void CheckIntegrationCode(HttpContext context)
        {
            HttpContext.Current.Response.ContentType = "text/javascript";
            if (IntegrationDetails() == true)
            {
                UpdateIntegration();
                int Campaign_count = Convert.ToInt32(DRIntegration[0]);
                int Credit_count = Convert.ToInt32(DRIntegration[1]);
                if (Campaign_count == 0 && Credit_count == 0)
                    context.Response.Write("var link = '"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/IntegrationTesting.aspx?Msg=Successful&Msg2=1';");
                else if (Campaign_count == 0)
                    context.Response.Write("var link ='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/IntegrationTesting.aspx?Msg=Successful&Msg2=2';");
                else if (Credit_count == 0)
                    context.Response.Write("var link ='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/IntegrationTesting.aspx?Msg=Successful&Msg2=3';");
                else
                    context.Response.Write("var link ='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/IntegrationTesting.aspx?Msg=Successful&Msg2=4';");
            }
            else
            {
                context.Response.Write("var link ='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/IntegrationTesting.aspx?Msg=Pending&Msg2=0';");
            }
            context.Response.Write("var iframe = document.createElement('iframe');");
            context.Response.Write("iframe.frameBorder=0;");
            context.Response.Write("iframe.width='650px';");
            context.Response.Write("iframe.height='400px';");
            context.Response.Write("iframe.id='randomid';");
            context.Response.Write("iframe.setAttribute('src', link);");

            if (Platform == "")
                Platform = "3d-cart";
            if (Platform.ToLower() == "3d-cart")
            {
                context.Response.Write("document.getElementById('div1').appendChild(iframe);");
            }
            else if (Platform.ToLower() == "volusion")
            {
                context.Response.Write("document.getElementById('div_articleid_49').appendChild(iframe);");
            }
            else if (Platform.ToLower() == "bigcommerce")
            {
                context.Response.Write("document.getElementById('LayoutColumn2').appendChild(iframe);");

            }
            else
            {
                context.Response.Write("document.getElementById('div1').appendChild(iframe);");
            }
        }

        private RouteValueDictionary GetQueryStringValues(HttpContext context)
        {
            var routeValues = context.Request.RequestContext.RouteData.Values;
            EmailId = comman.getData(routeValues["E"], "");
            OrderID = comman.getData(routeValues["OID"], "");
            Platform = comman.getData(routeValues["P"], "");
            WebsiteName = comman.getData(routeValues["W"], "");
            FirstName = comman.getData(routeValues["FN"], "");
            LastName = comman.getData(routeValues["LN"], "");
            Total = comman.getData(routeValues["TA"], "");
            Tax = comman.getData(routeValues["T1"], "");
            Tax2 = comman.getData(routeValues["T2"], "");
            Tax3 = comman.getData(routeValues["T3"], "");
            Discount = comman.getData(routeValues["D"], "");
            Shipping = comman.getData(routeValues["S"], "");
            SubTotal = comman.getData(routeValues["ST"], "");
            socailreferralID = comman.getData(routeValues["SocialReferralID"], "");
            return routeValues;
        }
        public bool CheckHostDetails()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int hostdetails = sqlobj.UpdateHostDetails(obj);
            if (hostdetails == 0)
                return false;
            else
                WebsiteIDForTransaction = hostdetails;
            return true;

        }
        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            ////FIRST TRY BUILT IN ASP.NT CHECK
            //if (context.Request.Browser.IsMobileDevice)
            //{
            //    return true;
            //}
            ////THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            //if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            //{
            //    return true;
            //}
            ////THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            //if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
            //    context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            //{
            //    return true;
            //}
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                {
                    "midp", "j2me", "avant", "docomo", 
                    "novarra", "palmos", "palmsource", 
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/", 
                    "blackberry", "mib/", "symbian", 
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio", 
                    "SIE-", "SEC-", "samsung", "HTC", 
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx", 
                    "NEC", "philips", "mmm", "xx", 
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java", 
                    "pt", "pg", "vox", "amoi", 
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo", 
                    "sgh", "gradi", "jb", "dddi", 
                    "moto", "iphone"
                };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }
            string sUA = HttpContext.Current.Request.UserAgent.Trim().ToLower();

            //if (HttpContext.Current.Request.Browser.IsMobileDevice || sUA.Contains("ipod") || sUA.Contains("iphone") || sUA.Contains("android") || sUA.Contains("opera mobi") || (sUA.Contains("windows phone os") && sUA.Contains("iemobile")) || sUA.Contains("fennec"))
            //    return true;

            return false;
        }
        public int GetMerchantID()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            Merchant_ID = sqlobj.GetMerchantID(obj);
            return Merchant_ID;
        }
        public bool IntegrationDetails()
        {
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.Website = WebsiteName;
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            DRIntegration = Campaignsqlobj.CheckIntegrationError(Campaignobj);
            if (DRIntegration.Read())
                return true;
            else
                return false;
        }
        public void UpdateIntegration()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int i = sqlobj.UpdateIntegration(obj);

        }
        public bool CheckMerchantIntegration()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int integrationdetails = sqlobj.CheckMerchantIntegration(obj);
            if (integrationdetails == 0)
                return false;
            else
                return true;
        }
        public bool CheckMerchantCredits()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int merchantcredits = sqlobj.CheckMerchantCreditscheck(obj);
            if (merchantcredits == 0)
                return false;
            else
                return true;
        }
        public bool CheckMerchantIsActive()
        {
            _HostDetails obj = new _HostDetails();
            obj.Platform = Platform;
            obj.Website = WebsiteName;
            obj.Social_Referral_Id = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int inactive = sqlobj.CheckMerchantIsActive(obj);
            if (inactive == 0)
                return false;
            else
                return true;
        }
        public void InsertIntoLog(int Merchant_id, int Customer_Id, string Customer_EmailID, bool Is_Wrong_Host_Details, bool Is_Not_Integrated, bool Is_No_Campaign, bool Is_Below_Min_Purchase, bool Is_Inactive_Merchant, bool Is_Same_Order)
        {
            _Log obj = new _Log();
            obj.Merchant_Id = Merchant_id;
            obj.Customer_Id = Customer_Id;
            obj.Customer_EmailID = Customer_EmailID;
            obj.Is_Wrong_Host_Details = Is_Wrong_Host_Details;
            obj.Is_Not_Integrated = Is_Not_Integrated;
            obj.Is_No_Campaign = Is_No_Campaign;
            obj.Is_Below_Min_Purchase = Is_Below_Min_Purchase;
            obj.Is_Inactive_Merchant = Is_Inactive_Merchant;
            obj.Is_Same_Order = Is_Same_Order;
            obj.Log_Time = DateTime.Now;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int integrationdetails = sqlobj.InsertIntoLog(obj);
        }
        private bool ValidateCampaign(RouteValueDictionary routeValues)
        {
            G_MaximumCreadits = 0.0M;
            G_SKUID = "0";
            if (routeValues["OD"] != null)
            {
                string[] OrderDetailsFull = routeValues["OD"].ToString().Split('/');
                for (int j = 0; j < OrderDetailsFull.Length; j++)
                {
                    string[] SingleProductDetails = OrderDetailsFull[j].Split('^');
                    ProductID = comman.getData(SingleProductDetails[0], "");
                    TotalAmount = comman.getData(SingleProductDetails[1], "");
                    Quantity = comman.getData(SingleProductDetails[2], 0);
                    itemname = comman.getData(SingleProductDetails[3], "");

                    object[] ProductCampaignDetails = CheckCampaignType(ProductID, TotalAmount, Quantity);
                    if (Convert.ToBoolean(ProductCampaignDetails[0]))
                    {
                        if (G_MaximumCreadits < Convert.ToDecimal(ProductCampaignDetails[1]))
                        {
                            G_MaximumCreadits = Convert.ToDecimal(ProductCampaignDetails[1]);
                            G_SKUID = ProductID;
                            G_ProductName = itemname;
                            G_Price = Convert.ToDecimal(TotalAmount.Replace('$', ' '));
                            G_Quantity = Quantity;
                        }
                    }
                }
                if (CampaignsDetails(G_SKUID) == true)
                    return true;
                else return false;
            }
            else return false;
        }
        private bool ValidateCampaign1hour(RouteValueDictionary routeValues)
        {
            G_MaximumCreadits = 0.0M;
            G_SKUID = "0";
            if (routeValues["OD"] != null)
            {
                string[] OrderDetailsFull = routeValues["OD"].ToString().Split('/');
                for (int j = 0; j < OrderDetailsFull.Length; j++)
                {
                    string[] SingleProductDetails = OrderDetailsFull[j].Split('^');
                    ProductID = comman.getData(SingleProductDetails[0], "");
                    TotalAmount = comman.getData(SingleProductDetails[1], "");
                    Quantity = comman.getData(SingleProductDetails[2], 0);
                    itemname = comman.getData(SingleProductDetails[3], "");

                    object[] ProductCampaignDetails = CheckCampaignType1hour(ProductID, TotalAmount, Quantity);
                    if (Convert.ToBoolean(ProductCampaignDetails[0]))
                    {
                        if (G_MaximumCreadits < Convert.ToDecimal(ProductCampaignDetails[1]))
                        {
                            G_MaximumCreadits = Convert.ToDecimal(ProductCampaignDetails[1]);
                            G_SKUID = ProductID;
                            G_ProductName = itemname;
                            G_Price = Convert.ToDecimal(TotalAmount.Replace('$', ' '));
                            G_Quantity = Quantity;
                        }
                    }
                }
                if (CampaignsDetails1hourdetails(G_SKUID) == true)
                    return true;
                else return false;
            }
            else return false;
        }
        private bool CheckSKUForCustomer1hour(RouteValueDictionary routeValues)
        {
            G_MaximumCreadits_C = 0.0M;
            G_SKUID_C = "0";
            SKUDetails(CookieID);
            if (routeValues["OD"] != null)
            {
                string[] OrderDetailsFull = routeValues["OD"].ToString().Split('/');
                for (int j = 0; j < OrderDetailsFull.Length; j++)
                {
                    string[] SingleProductDetails = OrderDetailsFull[j].Split('^');
                    ProductID = comman.getData(SingleProductDetails[0], "");
                    TotalAmount = comman.getData(SingleProductDetails[1], "");
                    Quantity = comman.getData(SingleProductDetails[2], 0);
                    itemname = comman.getData(SingleProductDetails[3], "");

                    object[] ProductCampaignDetails = CheckCampaignType1hour(ProductID, TotalAmount, Quantity);
                    if (Convert.ToBoolean(ProductCampaignDetails[0]))
                    {
                        if (SKU_ID_Referrer == ProductID)
                        {
                            G_MaximumCreadits_C = Convert.ToDecimal(ProductCampaignDetails[1]);
                            G_SKUID_C = ProductID;
                            G_ProductName_C = itemname;
                            G_Price_C = Convert.ToDecimal(TotalAmount.Replace('$', ' '));
                            G_Quantity_C = Quantity;
                        }
                    }
                }
                if (CampaignsDetails_C_1hour(G_SKUID_C) == true)
                    return true;
                else return false;
            }
            else return false;
        }
        public object[] CheckCampaignType1hour(string ProductID, string TotalAmount, int quantity)
        {
            object[] result = new object[2];
            bool isSKUBased = false;
            Decimal Referrer_reward = 0.0M;
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.SKU_ID = ProductID;
            Campaignobj.Website = WebsiteName;
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            Campaignobj.TotalAmount = TotalAmount;
            Campaignobj.Quantity = 1;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.ProductStore1hour(Campaignobj);
            if (CampaignDR.Read())
            {
                isSKUBased = true;
                Referrer_reward = Convert.ToDecimal(CampaignDR["Referrer_reward"]);
            }
            DBAccess.InstanceCreation().disconnect();
            result[0] = isSKUBased;
            result[1] = Referrer_reward;
            return result;
        }
        private bool CheckSKUForCustomer(RouteValueDictionary routeValues)
        {
            TotalQuantitiy_C = 0;
            _Transaction Campaignobj = new _Transaction();
            Campaignobj.offer_Id = CookieID;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            CampaigndetailsDR = Campaignsqlobj.GetcampRewardsDetails(Campaignobj);
            if (CampaigndetailsDR.Read())
            {
                CampaignDetails_C.Customer_reward = Convert.ToDecimal(CampaigndetailsDR["Customer_reward"].ToString());
                CampaignDetails_C.Referrer_reward = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward"].ToString());
                CampaignDetails_C.Customer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Customer_reward_type"].ToString());
                CampaignDetails_C.Referrer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward_type"].ToString());
                CampaignDetails_C.MinimumPurchaseAmount = Convert.ToDecimal(CampaigndetailsDR["Min_purchase_amt"].ToString());
                CampaignDetails_C.SKU_ID = CampaigndetailsDR["SKU_ID"].ToString();
                if (routeValues["OD"] != null)
                {
                    string[] OrderDetailsFull = routeValues["OD"].ToString().Split('/');
                    for (int j = 0; j < OrderDetailsFull.Length; j++)
                    {
                        string[] SingleProductDetails = OrderDetailsFull[j].Split('^');
                        ProductID = comman.getData(SingleProductDetails[0], "");
                        TotalAmount = comman.getData(SingleProductDetails[1], "");
                        Quantity = comman.getData(SingleProductDetails[2], 0);
                        itemname = comman.getData(SingleProductDetails[3], "");
                        if (CampaigndetailsDR["SKU_ID"].ToString() == ProductID)
                        {
                            G_Price_C = Convert.ToDecimal(TotalAmount.Replace('$', ' '));
                            G_Quantity_C = Quantity;

                        }
                        TotalQuantitiy_C += Quantity;
                    }
                }
                if (CampaigndetailsDR["SKU_ID"].ToString() == "0")
                {
                    if (G_SKUID_C == "0")
                    {
                        G_Price_C = Convert.ToDecimal(TotalAmount.Replace('$', ' '));
                        G_Quantity_C = 1;
                    }
                }
                return true;
            }
            else return false;
        }
        public object[] CheckCampaignType(string ProductID, string TotalAmount, int quantity)
        {
            object[] result = new object[2];
            bool isSKUBased = false;
            Decimal Referrer_reward = 0.0M;
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.SKU_ID = ProductID;
            Campaignobj.Website = WebsiteName;
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            Campaignobj.TotalAmount = TotalAmount;
            Campaignobj.Quantity = 1;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.ProductStore(Campaignobj);
            if (CampaignDR.Read())
            {
                isSKUBased = true;
                Referrer_reward = Convert.ToDecimal(CampaignDR["Referrer_reward"]);
            }
            DBAccess.InstanceCreation().disconnect();
            result[0] = isSKUBased;
            result[1] = Referrer_reward;
            return result;
        }
        public bool CampaignsDetails(string ProductID)
        {
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.SKU_ID = ProductID;
            Campaignobj.Website = WebsiteName;
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            CampaigndetailsDR = Campaignsqlobj.CheckCampaignsDetails(Campaignobj);
            if (CampaigndetailsDR.Read())
            {
                CampaignDetails.Customer_reward = Convert.ToDecimal(CampaigndetailsDR["Customer_reward"].ToString());
                CampaignDetails.Referrer_reward = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward"].ToString());
                CampaignDetails.DefaultFaceBook_Title = CampaigndetailsDR["DefaultFaceBook_Title"].ToString();
                CampaignDetails.DefaultFaceBook_ShareText = CampaigndetailsDR["DefaultFaceBook_ShareText"].ToString();
                CampaignDetails.DefaultEmail_Subject = CampaigndetailsDR["DefaultEmail_Subject"].ToString();
                CampaignDetails.DefaultTweet_Message = CampaigndetailsDR["DefaultTweet_Message"].ToString();
                CampaignDetails.DefaultEmail_Message = CampaigndetailsDR["DefaultEmail_Message"].ToString();
                CampaignDetails.WebsiteID = Convert.ToInt32(CampaigndetailsDR["Website_ID"].ToString());
                CampaignDetails.CampaignID = Convert.ToInt32(CampaigndetailsDR["Campaign_ID"].ToString());
                CampaignDetails.SKU_ID = CampaigndetailsDR["SKU_ID"].ToString();
                CampaignDetails.Type_Of_Campaign = Convert.ToInt32(CampaigndetailsDR["Campaign_Type"].ToString());
                CampaignDetails.Expiry_days = Convert.ToInt32(CampaigndetailsDR["Expiry_days"]);
                CampaignDetails.NameOfDay = CampaigndetailsDR["NameOfDay"].ToString();
                CampaignDetails.noofdays = Convert.ToInt32(CampaigndetailsDR["noofdays"].ToString());
                CampaignDetails.MerchantID = Convert.ToInt32(CampaigndetailsDR["Merchant_ID"].ToString());
                CampaignDetails.Customer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Customer_reward_type"].ToString());
                CampaignDetails.Referrer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward_type"].ToString());
                CampaignDetails.MinimumPurchaseAmount = Convert.ToDecimal(CampaigndetailsDR["Min_purchase_amt"].ToString());
                CampaignDetails.BackGroundColor = (CampaigndetailsDR["BackGroundColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["BackGroundColor"].ToString() : CampaigndetailsDR["BackGroundColor"].ToString());
                CampaignDetails.BorderColor = (CampaigndetailsDR["BorderColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["BorderColor"].ToString() : CampaigndetailsDR["BorderColor"].ToString());
                CampaignDetails.ForeColor = (CampaigndetailsDR["ForeColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["ForeColor"].ToString() : CampaigndetailsDR["ForeColor"].ToString());
                CampaignDetails.Campaign_Image = CampaigndetailsDR["Campaign_Image"].ToString();
                CampaignDetails.Platform = CampaigndetailsDR["PLATFORM"].ToString();
                CampaignDetails.Campaign_Name = CampaigndetailsDR["Campaign_Name"].ToString();
                CampaignDetails.Company_Name = CampaigndetailsDR["Company_Name"].ToString();
                CampaignDetails.Merchant_email_ID = CampaigndetailsDR["Email_Id"].ToString();
                CampaignDetails.Display_Type = Convert.ToInt32(CampaigndetailsDR["Display_Type"].ToString());
                CampaignDetails.First_Name = CampaigndetailsDR["First_Name"].ToString();
                CampaignDetails.Last_Name = CampaigndetailsDR["Last_name"].ToString();
                CampaignDetails.Created_On = Convert.ToDateTime(CampaigndetailsDR["Created_On"].ToString());
                return true;
            }
            else return false;
        }
        public bool CampaignsDetails1hourdetails(string ProductID)
        {
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.SKU_ID = ProductID;
            Campaignobj.Website = WebsiteName;
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            CampaigndetailsDR = Campaignsqlobj.CheckCampaignsDetails1hour(Campaignobj);
            if (CampaigndetailsDR.Read())
            {
                CampaignDetails.Customer_reward = Convert.ToDecimal(CampaigndetailsDR["Customer_reward"].ToString());
                CampaignDetails.Referrer_reward = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward"].ToString());
                CampaignDetails.DefaultFaceBook_Title = CampaigndetailsDR["DefaultFaceBook_Title"].ToString();
                CampaignDetails.DefaultFaceBook_ShareText = CampaigndetailsDR["DefaultFaceBook_ShareText"].ToString();
                CampaignDetails.DefaultEmail_Subject = CampaigndetailsDR["DefaultEmail_Subject"].ToString();
                CampaignDetails.DefaultTweet_Message = CampaigndetailsDR["DefaultTweet_Message"].ToString();
                CampaignDetails.DefaultEmail_Message = CampaigndetailsDR["DefaultEmail_Message"].ToString();
                CampaignDetails.WebsiteID = Convert.ToInt32(CampaigndetailsDR["Website_ID"].ToString());
                CampaignDetails.CampaignID = Convert.ToInt32(CampaigndetailsDR["Campaign_ID"].ToString());
                CampaignDetails.SKU_ID = CampaigndetailsDR["SKU_ID"].ToString();
                CampaignDetails.Type_Of_Campaign = Convert.ToInt32(CampaigndetailsDR["Campaign_Type"].ToString());
                CampaignDetails.Expiry_days = Convert.ToInt32(CampaigndetailsDR["Expiry_days"]);
                CampaignDetails.NameOfDay = CampaigndetailsDR["NameOfDay"].ToString();
                CampaignDetails.noofdays = Convert.ToInt32(CampaigndetailsDR["noofdays"].ToString());
                CampaignDetails.MerchantID = Convert.ToInt32(CampaigndetailsDR["Merchant_ID"].ToString());
                CampaignDetails.Customer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Customer_reward_type"].ToString());
                CampaignDetails.Referrer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward_type"].ToString());
                CampaignDetails.MinimumPurchaseAmount = Convert.ToDecimal(CampaigndetailsDR["Min_purchase_amt"].ToString());
                CampaignDetails.BackGroundColor = (CampaigndetailsDR["BackGroundColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["BackGroundColor"].ToString() : CampaigndetailsDR["BackGroundColor"].ToString());
                CampaignDetails.BorderColor = (CampaigndetailsDR["BorderColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["BorderColor"].ToString() : CampaigndetailsDR["BorderColor"].ToString());
                CampaignDetails.ForeColor = (CampaigndetailsDR["ForeColor"].ToString().IndexOf("#") < 0 ? "#" + CampaigndetailsDR["ForeColor"].ToString() : CampaigndetailsDR["ForeColor"].ToString());
                CampaignDetails.Campaign_Image = CampaigndetailsDR["Campaign_Image"].ToString();
                CampaignDetails.Platform = CampaigndetailsDR["PLATFORM"].ToString();
                CampaignDetails.Campaign_Name = CampaigndetailsDR["Campaign_Name"].ToString();
                CampaignDetails.Company_Name = CampaigndetailsDR["Company_Name"].ToString();
                CampaignDetails.Merchant_email_ID = CampaigndetailsDR["Email_Id"].ToString();
                CampaignDetails.Display_Type = Convert.ToInt32(CampaigndetailsDR["Display_Type"].ToString());
                CampaignDetails.First_Name = CampaigndetailsDR["First_Name"].ToString();
                CampaignDetails.Last_Name = CampaigndetailsDR["Last_name"].ToString();
                return true;
            }
            else return false;
        }

        public bool CampaignsDetails_C_1hour(string ProductID)
        {
            _CampaignsDetails Campaignobj = new _CampaignsDetails();
            Campaignobj.SKU_ID = ProductID;
            Campaignobj.Website = WebsiteName.Replace("http://", "");
            Campaignobj.Platform = Platform;
            Campaignobj.SocialReferralSiteID = socailreferralID;
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            CampaigndetailsDR = Campaignsqlobj.CheckCampaignsDetails1hour(Campaignobj);
            if (CampaigndetailsDR.Read())
            {
                CampaignDetails_C.Customer_reward = Convert.ToDecimal(CampaigndetailsDR["Customer_reward"].ToString());
                CampaignDetails_C.Referrer_reward = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward"].ToString());
                CampaignDetails_C.Customer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Customer_reward_type"].ToString());
                CampaignDetails_C.Referrer_reward_type = Convert.ToDecimal(CampaigndetailsDR["Referrer_reward_type"].ToString());
                CampaignDetails_C.MinimumPurchaseAmount = Convert.ToDecimal(CampaigndetailsDR["Min_purchase_amt"].ToString());
                CampaignDetails_C.SKU_ID = CampaigndetailsDR["SKU_ID"].ToString();

                return true;
            }
            else return false;
        }
        public bool CheckOrderDetails()
        {
            _TransactionDetails obj = new _TransactionDetails();
            obj.Order_ID = OrderID;
            obj.Website = WebsiteName;
            obj.SocialReferralSiteID = socailreferralID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.CheckOrderDetails(obj);
            if (DR.Read())
            {
                TransactionID = Convert.ToInt32(DR["Transaction_ID"].ToString());
                CookieID = Convert.ToInt32(DR["Referral_ID"].ToString());
                if (DR["offer_id"].ToString() != null || DR["offer_id"].ToString() != "")             
                    offerID = Convert.ToInt32(DR["offer_id"].ToString());              
                else               
                    offerID = 0;               
                if (offerID == 0)
                {
                    couponShow1Hour();
                }
                else
                {
                    couponShow();
                }
                InsertIntoLog(Merchant_ID, NewCustomerDetails.Customer_ID, EmailId, false, false, false, false, false, true);
                return true;
            }
            else
                return false;
        }
        public void InsertIntoTransaction()
        {
            AddtransactionDetails(NewCustomerDetails.Customer_ID, 0, CampaignDetails.WebsiteID, CampaignDetails.CampaignID, OrderID, Quantity, CampaignDetails.SKU_ID, Total, SubTotal, Discount, Tax, Tax2, Tax3, Shipping, 0);
        }

        public void checkCookie()
        {

            if (HttpContext.Current.Request.Cookies["OfferID"] != null)
                CookieID = Convert.ToInt32(HttpContext.Current.Request.Cookies["OfferID"].Value);

            if (CookieID > 0)
            {
                if (!Checkcookie_Transaction(CookieID))
                    CookieID = 0;
                else
                {
                    customerEmailID(CookieID);
                    HttpContext.Current.Response.Cookies["OfferID"].Expires = DateTime.Now;
                }

            }
           
        }
        public void MessageDisplay()
        {
            if (CampaignDetails.Referrer_reward_type == 1)
            {
                SubTotal = SubTotal.Replace('$', ' ');
                Total = Total.Replace('$', ' ');
                ReferrerReward = "$" + CampaignDetails.Referrer_reward;
                Credit_For_Referrer = G_Quantity * Convert.ToInt32(CampaignDetails.Referrer_reward * 100);
                if (CampaignDetails.MinimumPurchaseAmount == 0)
                {
                    if (CampaignDetails.SKU_ID == "0")
                    {
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>$" + CampaignDetails.Referrer_reward.ToString() + "</span> off your purchase";
                        ProductName = "anything";
                    }
                    else
                    {
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>$" + CampaignDetails.Referrer_reward.ToString() + "</span> off your purchase of " + G_ProductName;
                        ProductName = G_ProductName;
                    }
                }
                else
                {
                    if (CampaignDetails.SKU_ID == "0")
                    {
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'> $" + CampaignDetails.Referrer_reward.ToString() + "</span> off your $" + CampaignDetails.MinimumPurchaseAmount + " purchase";
                        ProductName = "anything";
                    }
                    else
                    {
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'> $" + CampaignDetails.Referrer_reward.ToString() + "</span> off your $" + CampaignDetails.MinimumPurchaseAmount + " purchase of " + G_ProductName;
                        ProductName = G_ProductName;
                    }
                }
            }
            else
            {
                ReferrerReward = CampaignDetails.Referrer_reward + "%";
                if (CampaignDetails.MinimumPurchaseAmount == 0)
                {
                    if (CampaignDetails.SKU_ID == "0")
                    {
                        savingreferrerprice = Convert.ToDecimal((CampaignDetails.Referrer_reward / 100) * Convert.ToDecimal(SubTotal.Replace('$', ' ')));
                        Credit_For_Referrer = Convert.ToInt32(savingreferrerprice * 100);
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>" + string.Format("{0:0}", CampaignDetails.Referrer_reward) + "%</span> off your purchase";
                        ProductName = "anything";
                    }
                    else
                    {
                        savingreferrerprice = G_Quantity * Convert.ToDecimal((CampaignDetails.Referrer_reward / 100) * G_Price);
                        Credit_For_Referrer = Convert.ToInt32(savingreferrerprice * 100);
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>" + string.Format("{0:0}", CampaignDetails.Referrer_reward) + "%</span> off your purchase of " + G_ProductName;
                        ProductName = G_ProductName;
                    }
                }
                else
                {
                    if (CampaignDetails.SKU_ID == "0")
                    {
                        savingreferrerprice = Convert.ToDecimal((CampaignDetails.Referrer_reward / 100) * Convert.ToDecimal(SubTotal.Replace('$', ' ')));
                        Credit_For_Referrer = Convert.ToInt32(savingreferrerprice * 100);
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>" + string.Format("{0:0}", CampaignDetails.Referrer_reward) + "%</span> off your $" + CampaignDetails.MinimumPurchaseAmount + " purchase";
                        ProductName = "anything";
                    }
                    else
                    {
                        savingreferrerprice = Convert.ToDecimal((CampaignDetails.Referrer_reward / 100) * Convert.ToDecimal(SubTotal.Replace('$', ' ')));
                        Credit_For_Referrer = Convert.ToInt32(savingreferrerprice * 100);
                        MessageMoneyBack = "<span style='color:" + CampaignDetails.BorderColor + "; font-size: 19px;'>" + string.Format("{0:0}", CampaignDetails.Referrer_reward) + "%</span> off your $" + CampaignDetails.MinimumPurchaseAmount + " purchase of " + G_ProductName;
                        ProductName = G_ProductName;
                    }
                }
            }
            if (CampaignDetails.Customer_reward_type == 1)
            {
                CustomerReward = "$" + CampaignDetails.Customer_reward;
                Credit_For_Customer = G_Quantity * Convert.ToInt32(CampaignDetails.Customer_reward * 100);
            }
            else
            {
                CustomerReward = CampaignDetails.Customer_reward + "%";
                SubTotal = SubTotal.Replace('$', ' ');
                if (CampaignDetails.SKU_ID == "0")
                {
                    savingCustomerprice = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal((CampaignDetails.Customer_reward / 100) * Convert.ToDecimal(SubTotal))));
                    Credit_For_Customer = savingCustomerprice * 100;
                }
                else
                {
                    savingCustomerprice = G_Quantity * Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal((CampaignDetails.Customer_reward / 100) * G_Price)));
                    Credit_For_Customer = savingCustomerprice * 100;
                }
            }
            if (CookieID > 0)
            {
                if (CampaignDetails_C.Customer_reward_type == 1)
                {
                    Credit_For_Customer_C = G_Quantity_C * Convert.ToInt32(CampaignDetails_C.Customer_reward * 100);
                }
                else
                {

                    SubTotal = SubTotal.Replace('$', ' ');
                    if (CampaignDetails_C.SKU_ID == "0")
                    {
                        savingCustomerprice_C = Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal((CampaignDetails_C.Customer_reward / 100) * Convert.ToDecimal(SubTotal))));
                        Credit_For_Customer_C = savingCustomerprice_C * 100;
                    }
                    else
                    {
                        savingCustomerprice_C = G_Quantity_C * Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal((CampaignDetails_C.Customer_reward / 100) * G_Price_C)));
                        Credit_For_Customer_C = savingCustomerprice_C * 100;
                    }
                }

                if (CampaignDetails_C.Referrer_reward_type == 1)
                {
                    Credit_For_Referrer_C = G_Quantity_C * Convert.ToInt32(CampaignDetails_C.Referrer_reward * 100);
                }
                else
                {

                    SubTotal = SubTotal.Replace('$', ' ');
                    if (CampaignDetails_C.SKU_ID == "0")
                    {
                        savingreferrerprice_C = Convert.ToDecimal((CampaignDetails_C.Referrer_reward / 100) * Convert.ToDecimal(SubTotal.Replace('$', ' ')));
                        Credit_For_Referrer_C = Convert.ToInt32(savingreferrerprice_C * 100);
                    }
                    else
                    {
                        savingreferrerprice_C = G_Quantity_C * Convert.ToDecimal((CampaignDetails_C.Referrer_reward / 100) * Convert.ToDecimal(G_Price_C));
                        Credit_For_Referrer_C = Convert.ToInt32(savingreferrerprice_C * 100);
                    }
                }
            }
        }
        public void couponShow1Hour()
        {
            HttpContext.Current.Response.ContentType = "text/javascript";
            SubTotal = SubTotal.Replace('$', ' ');
            //Display Message in Coupon in 3D cart
            MessageDisplay();
            SubTotal = string.Format("{0:0.00}", SubTotal);
            string Coupon_Html = HttpContext.Current.Server.MapPath("~/Plugin/HTML/RebateOnly.htm");
            StreamReader PluginCoupon = new StreamReader(Coupon_Html);
            string PluginCoupon_R = PluginCoupon.ReadToEnd();
            PluginCoupon.Close();
            if (Platform == "")
                Platform = "3d-cart";
            if (Platform.ToLower() == "3d-cart")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div1");
            }
            else if (Platform.ToLower() == "volusion")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div_articleid_49");
            }
            else if (Platform.ToLower() == "bigcommerce")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "LayoutColumn2");
            }
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div2");
            }

            if (CookieID > 0)
            {
                if (CampaignDetails_C.MinimumPurchaseAmount <= Convert.ToDecimal(Total.Replace('$', ' ')))
                {
                    SKUDetails(CookieID);

                    if (SKU_ID_Referrer == CampaignDetails_C.SKU_ID)
                    {
                        if (CampaignDetails_C.Customer_reward_type == 1)
                        {
                            if (Convert.ToDecimal(CampaignDetails_C.Customer_reward).ToString() == "0.00")
                                return;
                            else
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "<tr><td style=' text-align: center;'><p><span>You’ve received a rebate of $" + (CampaignDetails_C.Customer_reward * G_Quantity_C) + "*. Please check your e-mail for further details.</span></p></td></tr><tr><td style='text-align: center;border-bottom: solid 1px rgb(173, 169, 169);'><small>Disclaimer:You have received $" + (CampaignDetails_C.Customer_reward * G_Quantity_C) + " worth of credit</small></td></tr>");

                        }
                        else
                        {
                            if (Convert.ToDecimal(savingCustomerprice_C).ToString() == "0.00")
                                return;
                            else
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "<tr><td style=' text-align: center;'><p><span>You’ve received a rebate of $" + savingCustomerprice_C + "*. Please check your e-mail for further details.</span></p></td></tr><tr><td style='text-align: center;border-bottom: solid 1px rgb(173, 169, 169);'><small>Disclaimer:You have received $" + savingCustomerprice_C + " worth of credit</small></td></tr>");

                        }
                    }
                    else
                    {
                        PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                    }
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                }
            }
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
            }

            HttpContext.Current.Response.Write(PluginCoupon_R);

        }
        public void couponShow()
        {
            strShortURL = ShortURL(URL + offerID);
            HttpContext.Current.Response.ContentType = "text/javascript";
            SubTotal = SubTotal.Replace('$', ' ');
            //Display Message in Coupon in 3D cart
            MessageDisplay();
            SubTotal = string.Format("{0:0.00}", SubTotal);
            string Coupon_Html;
            if (CampaignDetails.Display_Type == 2)
            {
                Coupon_Html = HttpContext.Current.Server.MapPath("~/Plugin/HTML/PluginCoupon.htm");
            }
            else
            {
                Coupon_Html = HttpContext.Current.Server.MapPath("~/Plugin/HTML/InlinePlugin.html");
            }

            StreamReader PluginCoupon = new StreamReader(Coupon_Html);
            string PluginCoupon_R = PluginCoupon.ReadToEnd();
            PluginCoupon.Close();
            if (Platform == "")
                Platform = "3d-cart";
            if (Platform.ToLower() == "3d-cart")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div1");
            }
            else if (Platform.ToLower() == "volusion")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div_articleid_49");
            }
            else if (Platform.ToLower() == "bigcommerce")
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "LayoutColumn2");
            }
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{div}", "div1");
            }
            if (Convert.ToDecimal(CampaignDetails.Referrer_reward).ToString() == "0.00")
                PluginCoupon_R = PluginCoupon_R.Replace("{MessageMoneyBack}", "<h2><p>Invite your friends and they’ll get " + CustomerReward.ToString() + " off.</h2></p>");
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{MessageMoneyBack}", "<h2>Let us give you money back! <br /></h2><p> " + MessageMoneyBack.ToString() + " every time you share.</p> ");
            }
            PluginCoupon_R = PluginCoupon_R.Replace("{CopyToClip}", strShortURL);
            string sUA = HttpContext.Current.Request.UserAgent.Trim().ToLower();
            //bool isMobile = false;
            //if (HttpContext.Current.Request.Browser.IsMobileDevice || sUA.Contains("ipod") || sUA.Contains("iphone") || sUA.Contains("android") || sUA.Contains("opera mobi") || (sUA.Contains("windows phone os") && sUA.Contains("iemobile")) || sUA.Contains("fennec"))
            //    isMobile = true;

            if (isMobileBrowser())
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{imgtd}", "");
                PluginCoupon_R = PluginCoupon_R.Replace("{divforlinkclick}", "");
            }
            else
            {

                if (CampaignDetails.Campaign_Image == "")
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{imgtd}", "");
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{imgtd}", "<td width='24.5%' valign='top' class='logoImg'> <img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + CampaignDetails.Campaign_Image.ToString() + " alt='' /></td>");
                }
                //Short Facebook share url

                PluginCoupon_R = PluginCoupon_R.Replace("{divforlinkclick}", "<td width='30%'><span class='sharecupon'>Share your coupon link</span><input type='text' text='Message' value='" + strShortURL + "'/></td>");

            }
            if (CookieID > 0)
            {
                if (CampaignDetails_C.MinimumPurchaseAmount <= Convert.ToDecimal(Total.Replace('$', ' ')))
                {
                    SKUDetails(CookieID);

                    if (SKU_ID_Referrer == CampaignDetails.SKU_ID)
                    {
                        if (CampaignDetails_C.Customer_reward_type == 1)
                        {
                            if (Convert.ToDecimal(CampaignDetails_C.Customer_reward).ToString() == "0.00")
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                            else
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "<tr><td style=' text-align: center;'><p><span>You’ve received a rebate of $" + (CampaignDetails_C.Customer_reward * G_Quantity_C) + "*. Please check your e-mail for further details.</span></p></td></tr><tr><td style='text-align: center;border-bottom: solid 1px rgb(173, 169, 169);'><small>Disclaimer:You have received $" + (CampaignDetails_C.Customer_reward * G_Quantity_C) + " worth of credit</small></td></tr>");


                        }
                        else
                        {
                            if (Convert.ToDecimal(savingCustomerprice_C).ToString() == "0.00")
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                            else
                                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "<tr><td style=' text-align: center;'><p><span>You’ve received a rebate of $" + savingCustomerprice_C + "*. Please check your e-mail for further details.</span></p></td></tr><tr><td style='text-align: center;border-bottom: solid 1px rgb(173, 169, 169);'><small>Disclaimer:You have received $" + savingCustomerprice_C + " worth of credit</small></td></tr>");

                        }
                    }
                    else
                    {
                        PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                    }
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
                }
            }
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{DynamicTr}", "");
            }

            //PluginCoupon_R = PluginCoupon_R.Replace("{FunctionEmailClick}", "EmailClick");
            PluginCoupon_R = PluginCoupon_R.Replace("{pageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{FromEmailID}", EmailId.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{Customerfromemailid}", EmailId.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{OfferID}", offerID.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{Backcolor}", CampaignDetails.BorderColor.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{ForeColor}", CampaignDetails.ForeColor.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{color}", CampaignDetails.BackGroundColor.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{CustomorReward}", CustomerReward.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{ProductName}", ProductName.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{ReferrerReward}", ReferrerReward.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{SubTotal}", (string.Format("{0:0.00}", Convert.ToDecimal(SubTotal)).ToString()));
            PluginCoupon_R = PluginCoupon_R.Replace("{EmailID_Subject}", CampaignDetails.DefaultEmail_Subject.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{EmailID_Message}", CampaignDetails.DefaultEmail_Message.ToString());

            if (CampaignDetails.Expiry_days > 20000)
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{NameOfDay}", "");
                PluginCoupon_R = PluginCoupon_R.Replace("{Expiry_date}", "");
            }
            else
            {
                PluginCoupon_R = PluginCoupon_R.Replace("{NameOfDay}", "This offer is only valid until " + CampaignDetails.NameOfDay);
                PluginCoupon_R = PluginCoupon_R.Replace("{Expiry_date}", DateTime.Now.AddDays(CampaignDetails.Expiry_days).ToShortDateString());
            }
            PluginCoupon_R = PluginCoupon_R.Replace("{TransactionNO}", TransactionID.ToString());
            PluginCoupon_R = PluginCoupon_R.Replace("{company_name}", ConfigurationManager.AppSettings["company_name"].ToString());
            if (Convert.ToDecimal(CampaignDetails.Referrer_reward).ToString() == "0.00")
                PluginCoupon_R = PluginCoupon_R.Replace("{MessageForReward}", "Invite your friends and they’ll get " + CustomerReward.ToString() + " off.");
            else if (Convert.ToDecimal(CampaignDetails.Customer_reward).ToString() == "0.00")
                PluginCoupon_R = PluginCoupon_R.Replace("{MessageForReward}", "Invite your friends and get " + ReferrerReward.ToString() + " back when they make a purchase.");
            else
                PluginCoupon_R = PluginCoupon_R.Replace("{MessageForReward}", "Invite your friends and they’ll get  " + CustomerReward.ToString() + "  off.If they buy anything, you’ll Get " + ReferrerReward.ToString() + " back");
            PluginCoupon_R = PluginCoupon_R.Replace("{clickurl}", "Click on the link below to get " + CustomerReward.ToString() + "off your purchase:<br/><a href=" + strShortURL + ">" + strShortURL + "</a>");
            PluginCoupon_R = PluginCoupon_R.Replace("{clickurlshare}", "Click on the link below to get " + CustomerReward.ToString() + "off your purchase:<br/>" + strShortURL + "");

            if (CampaignDetails.Referrer_reward_type == 1)
            {
                //PluginCoupon_R = PluginCoupon_R.Replace("{clickurl}", "Click on the link below to get $" + (string.Format("{0:0.00}", CampaignDetails.Referrer_reward).ToString()) + "off your purchase:<br/><a style='font-size:12px;text-decoration:underline;padding-right:287px;color:blue;line-height:17px;' href=" + strShortURL + " target='_blank'>" + strShortURL + "</a>");
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R}", (string.Format("{0:0.00}", CampaignDetails.Referrer_reward).ToString()));
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R_3}", (string.Format("{0:0.00}", CampaignDetails.Referrer_reward * 3).ToString()));
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R_5}", (string.Format("{0:0.00}", CampaignDetails.Referrer_reward * 5).ToString()));
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward))).ToString()).TrimStart());
                }
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 3)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_3}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 3))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_3}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 3))).ToString()).TrimStart());
                }
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 5)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_5}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 5))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_5}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(CampaignDetails.Referrer_reward * 5))).ToString()).TrimStart());
                }
            }
            else
            {
                //PluginCoupon_R = PluginCoupon_R.Replace("{clickurl}", "Click on the link below to get " + (string.Format("{0:0.00}", savingreferrerprice).ToString()) + "% off your purchase: <br/> <a style='font-size:12px;text-decoration:underline;padding-right:287px;color:blue;line-height:17px;' href=" + strShortURL + " target='_blank'>" + strShortURL + "</a>");
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R}", (string.Format("{0:0.00}", savingreferrerprice).ToString()));
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R_3}", (string.Format("{0:0.00}", savingreferrerprice * 3).ToString()));
                PluginCoupon_R = PluginCoupon_R.Replace("{Type_of_Reward_R_5}", (string.Format("{0:0.00}", savingreferrerprice * 5).ToString()));
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString()).TrimStart());
                }
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_3}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_3}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString()).TrimStart());
                }
                if (((Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5)).ToString()).Contains('-'))
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_5}", "-$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString().Replace('-', ' ')).TrimStart());
                }
                else
                {
                    PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_5}", "$" + (string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString()).TrimStart());
                }
                //PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost}", string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString());
                //PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_3}", string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString());
                //PluginCoupon_R = PluginCoupon_R.Replace("{FinalCost_5}", string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString());

            }
            HttpContext.Current.Response.Write(PluginCoupon_R);
        }
        public bool Checkcookie_Transaction(int CookieIDForReferral)
        {
            _TransactionDetails obj = new _TransactionDetails();
            obj.Offer_ID = CookieIDForReferral;
            obj.Merchant_Id = Merchant_ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader CheckcookieTransactionDR = sqlobj.Checkcookie_Transaction(obj);
            if (CheckcookieTransactionDR.Read())
            {
                if (CheckcookieTransactionDR[0].ToString() == "0")
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        public void customerEmailID(int CookieIDForReferral)
        {
            _TransactionDetails obj = new _TransactionDetails();
            obj.Offer_ID = CookieIDForReferral;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader CustomerDetailsDR = sqlobj.CustomerDetails_Transaction(obj);
            if (CustomerDetailsDR.Read())
            {
                CustomerDetails.EmailID = CustomerDetailsDR["Email_ID"].ToString();
                CustomerDetails.FirstName = CustomerDetailsDR["First_Name"].ToString();
                CustomerDetails.Password = CustomerDetailsDR["Password"].ToString();
                CustomerDetails.Customer_ID = Convert.ToInt32(CustomerDetailsDR["Customer_ID"].ToString());
                CustomerDetails.referred_transaction_id = Convert.ToInt32(CustomerDetailsDR["Transaction_ID"].ToString());
                CustomerDetails.IsActive = Convert.ToBoolean(CustomerDetailsDR["Is_Active"]);
            }

        }
        public string GeneratePassword(int PasswordLength)
        {
            string Password = "";
            string AlhaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random rn = new Random();
            for (int i = 0; i < PasswordLength; i++)
            {
                int num = rn.Next(9);
                Password += AlhaNumeric[num].ToString();
            }
            return Password;
        }
        public void AddNewCustomer(string FirstName, string LastName, string EmailID)
        {
            _plugin obj = new _plugin();
            obj.Customer_ID = 0;
            obj.FirstName = FirstName;
            obj.LastName = LastName;
            obj.EmailID = EmailID;
            obj.Password = "";
            // Password = obj.Password;
            obj.Address = "";
            obj.City = "";
            obj.State = "";
            obj.Country_ID = "0";
            obj.Zip = Convert.ToInt32(0);
            obj.PhoneNumber = "";
            obj.IsFacebook = false;
            obj.IsTwitter = false;
            obj.IsActive = false;
            obj.Facebook_Id = "";
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader DR = sqlobj.InsertIntoCustomer_Master(obj);
            if (DR.Read())
            {
                NewCustomerDetails.Password = DR["Password"].ToString();
                NewCustomerDetails.Customer_ID = Convert.ToInt32(DR["Customer_ID"].ToString());
                NewCustomerDetails.IsActive = Convert.ToBoolean(DR["Is_Active"]);
            }

        }
        public void AddtransactionDetails(int CustomerID, int ReferralID, int WebsiteID_Add, int CampaignID, string OrderID, int Quantity, string SKUID, string TotalAmount, string Subtotal, string Discount, string Tax, string Tax2, string Tax3, string Shipping, int referred_transaction_id)
        {

            _TransactionDetails obj = new _TransactionDetails();
            obj.Transaction_ID = 0;
            obj.Customer_ID = CustomerID;
            obj.Referral_ID = ReferralID;
            obj.Website_ID = WebsiteID_Add;
            obj.Campaign_ID = CampaignID;
            obj.Order_ID = OrderID;
            obj.Quantity = Quantity;
            obj.SKU_ID = SKUID;
            obj.TotalAmount = TotalAmount;
            obj.SubTotal = Subtotal;
            obj.Discount = Discount;
            obj.Tax = Tax;
            obj.Tax2 = Tax2;
            obj.Tax3 = Tax3;
            obj.Shipping = Shipping;
            obj.referred_transaction_id = referred_transaction_id;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.InsertIntoTransaction_Details(obj);
            TransactionID = result;
        }
        private void Save_Product_Details(RouteValueDictionary routeValues)
        {
            if (routeValues["OD"] != null)
            {
                string[] OrderDetailsFull = routeValues["OD"].ToString().Split('/');
                for (int j = 0; j < OrderDetailsFull.Length; j++)
                {
                    string[] SingleProductDetails = OrderDetailsFull[j].Split('^');
                    ProductID = comman.getData(SingleProductDetails[0], "");
                    TotalAmount = comman.getData(SingleProductDetails[1], "");
                    Quantity = comman.getData(SingleProductDetails[2], 0);
                    itemname = comman.getData(SingleProductDetails[3], "");
                    AddProductName(CampaignDetails.MerchantID, CampaignDetails.WebsiteID, ProductID, itemname);
                    AddProductDetails(ProductID, itemname, Quantity, TotalAmount, TransactionID);
                }
            }
        }
        public int AddProductName(int merchantID, int WebsiteproductID, string SKUID, string ProductNAme)
        {
            _Product_name obj = new _Product_name();
            obj.Merchant_ID = merchantID;
            obj.Website_Id = WebsiteproductID;
            obj.SKU_Id = SKUID;
            obj.Product_Name = ProductNAme;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.InsetIntoProduct_name(obj);
            return result;
        }
        public int AddProductDetails(string ProductID, string ProductName, int Quantity, string Price, int TransactionID)
        {
            _Product_Details obj = new _Product_Details();
            obj.SKU_ID = ProductID;
            obj.Product_Name = ProductName;
            obj.Quantity = Quantity;
            obj.Price = Price;
            obj.Transaction_ID = TransactionID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.InsetIntoProduct_Details(obj);
            return result;
        }
        public void EnterTotalDetails()
        {
            // if (HttpContext.Current.Request.Cookies["OfferID"] != null)
            if (CookieID > 0)
            {
                Total = Total.Replace('$', ' ');
                MessageDisplay();
                SKUDetails(CookieID);
                if (SKU_ID_Referrer == CampaignDetails.SKU_ID)
                {
                    AddtransactionDetails(NewCustomerDetails.Customer_ID, CookieID, CampaignDetails.WebsiteID, CampaignDetails.CampaignID, OrderID, Quantity, CampaignDetails.SKU_ID, Total, SubTotal, Discount, Tax, Tax2, Tax3, Shipping, CustomerDetails.referred_transaction_id);
                    if (CampaignDetails_C.MinimumPurchaseAmount <= Convert.ToDecimal(Total))
                    {
                        CreditAvailable();
                        Threshold();
                        if (EmailId == CustomerDetails.EmailID)
                        {
                            Credit_For_Referrer_C = 0;
                            totalcredits = Credit_For_Customer;
                            TransactionFee = Convert.ToInt32((3 * totalcredits) / 100);
                            if (CreditAvailableDetails > (totalcredits + TransactionFee + thresholdAmount))
                            {
                                AvailableCreditsNotifications(CreditAvailableDetails - Credit_For_Customer_C - Credit_For_Referrer_C - TransactionFee);
                            }
                            else
                            {
                                _Merchant objautorep = new _Merchant();
                                objautorep.Merchant_ID = CampaignDetails.MerchantID;
                                DAL.Credit_card objauto = new Credit_card();
                                SqlDataReader DRAuto = objauto.GetMerchantAutorep(objautorep);

                                if (DRAuto.Read())
                                {
                                    if (DRAuto["Is_auto_replenish"].ToString() == "True")
                                    {
                                        _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
                                        DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
                                        objCreditPlanMaster.PaymentCredits = Convert.ToInt32(totalcredits + TransactionFee);
                                        objCreditPlanMaster.Merchant_ID = Convert.ToInt32(CampaignDetails.MerchantID.ToString());
                                        SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindTotalCreditPlanByAmount(objCreditPlanMaster);


                                        while (drCreditPlan.Read())
                                        {
                                            if (Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()) > 0)
                                                CreditCard(drCreditPlan["Payment_Amount"].ToString(), Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                                            else
                                            {
                                                long credits = Convert.ToInt32(Convert.ToInt32(totalcredits + TransactionFee) + Convert.ToInt32(drCreditPlan["Threshold"].ToString()) - Convert.ToInt32(CreditAvailableDetails));
                                                decimal divide = 100;
                                                decimal mathamount = Math.Ceiling(credits / divide);
                                                PlanAmont = mathamount.ToString();
                                                PlanCredit = Convert.ToInt32(mathamount * 100);
                                                CreditCard(PlanAmont, Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                                            }
                                        }
                                        CreditAvailable();
                                    }
                                    else
                                    {
                                        AvailableCreditsNotifications(CreditAvailableDetails - Credit_For_Customer_C - TransactionFee);
                                    }
                                }
                            }
                            if (CreditAvailableDetails > (totalcredits + TransactionFee))
                            {
                                // for customer
                                CreditAvailableDetails = CreditAvailableDetails - Credit_For_Customer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID = AddCreditTransaction(TransactionID, NewCustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Customer_C, "Rebate", "Successful");
                                AddCustomerTransaction(Credit_transactionID);
                                AddMerchantTransaction(Credit_transactionID);
                                UpdateCustomerCredits(NewCustomerDetails.Customer_ID, Credit_For_Customer_C);

                                //end for customer

                                // for referrer

                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R = AddCreditTransaction(TransactionID, CustomerDetails.Customer_ID, CampaignDetails.MerchantID, 0, "Reward", "Successful");
                                AddCustomerTransaction(Credit_transactionID_R);
                                AddMerchantTransaction(Credit_transactionID_R);
                                UpdateCustomerCredits(CustomerDetails.Customer_ID, 0);
                                //update expiry date
                                updatemerchantexpiry(CampaignDetails.MerchantID);
                                //end of update expiry date
                                //end for reffer
                                //Transactionfee
                                CreditAvailableDetails = CreditAvailableDetails - TransactionFee;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R_transaction_fee = AddCreditTransaction(TransactionID, 0, CampaignDetails.MerchantID, TransactionFee, "TransactionFee", "Successful");
                                AddMerchantTransaction(Credit_transactionID_R_transaction_fee);

                                //End transactionFee

                            }
                            else
                            {
                                UpdateCampaignsInactive();
                                // for customer
                                TotalPendingCreditAvailable = TotalPendingCreditAvailable + Credit_For_Customer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID = AddCreditTransaction(TransactionID, NewCustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Customer_C, "Rebate", "Pending Payment");
                                AddCustomerTransaction(Credit_transactionID);
                                AddMerchantTransaction(Credit_transactionID);
                                UpdateCustomerCredits(NewCustomerDetails.Customer_ID, Credit_For_Customer_C);
                                // end for customer
                                //for referrer
                                Creditsbelowzero = 1;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R = AddCreditTransaction(TransactionID, CustomerDetails.Customer_ID, CampaignDetails.MerchantID, 0, "Reward", "Pending Payment");
                                AddCustomerTransaction(Credit_transactionID_R);
                                AddMerchantTransaction(Credit_transactionID_R);
                                UpdateCustomerCredits(CustomerDetails.Customer_ID, 0);
                                // updatemerchantexpiry(CustomerDetails.MerchantID);

                                //end for referrer
                                //Transactionfee
                                TotalPendingCreditAvailable = TotalPendingCreditAvailable + TransactionFee;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R_transaction_fee = AddCreditTransaction(TransactionID, 0, CampaignDetails.MerchantID, TransactionFee, "TransactionFee", "Pending Payment");
                                AddMerchantTransaction(Credit_transactionID_R_transaction_fee);

                                //End transactionFee
                            }
                        }
                        else
                        {
                            totalcredits = Credit_For_Customer_C + Credit_For_Referrer_C;
                            TransactionFee = Convert.ToInt32((3 * totalcredits) / 100);
                            if (CreditAvailableDetails > (totalcredits + TransactionFee + thresholdAmount))
                            {
                                AvailableCreditsNotifications(CreditAvailableDetails - Credit_For_Customer_C - Credit_For_Referrer_C - TransactionFee);
                            }
                            else
                            {
                                _Merchant objautorep = new _Merchant();
                                objautorep.Merchant_ID = CampaignDetails.MerchantID;
                                DAL.Credit_card objauto = new Credit_card();
                                SqlDataReader DRAuto = objauto.GetMerchantAutorep(objautorep);
                                if (DRAuto.Read())
                                {

                                    if (DRAuto["Is_auto_replenish"].ToString() == "True")
                                    {
                                        _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
                                        DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
                                        objCreditPlanMaster.PaymentCredits = Convert.ToInt32(totalcredits + TransactionFee);
                                        objCreditPlanMaster.Merchant_ID = Merchant_ID;
                                        SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindTotalCreditPlanByAmount(objCreditPlanMaster);

                                        while (drCreditPlan.Read())
                                        {
                                            if (Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()) > 0)
                                                CreditCard(drCreditPlan["Payment_Amount"].ToString(), Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                                            else
                                            {
                                                long credits = Convert.ToInt32(Convert.ToInt32(totalcredits + TransactionFee) + Convert.ToInt32(drCreditPlan["Threshold"].ToString()) - Convert.ToInt32(CreditAvailableDetails));
                                                decimal divide = 100;
                                                decimal mathamount = Math.Ceiling(credits / divide);
                                                PlanAmont = mathamount.ToString();
                                                PlanCredit = Convert.ToInt32(mathamount * 100);
                                                CreditCard(PlanAmont, Convert.ToInt32(drCreditPlan["Credit_Plan_ID"].ToString()));
                                            }
                                        }
                                        CreditAvailable();
                                    }
                                    else
                                    {
                                        AvailableCreditsNotifications(CreditAvailableDetails - Credit_For_Customer_C - Credit_For_Referrer_C - TransactionFee);
                                    }
                                }
                            }
                            if (CreditAvailableDetails > (totalcredits + TransactionFee))
                            {
                                // for customer
                                CreditAvailableDetails = CreditAvailableDetails - Credit_For_Customer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID = AddCreditTransaction(TransactionID, NewCustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Customer_C, "Rebate", "Successful");
                                AddCustomerTransaction(Credit_transactionID);
                                AddMerchantTransaction(Credit_transactionID);
                                UpdateCustomerCredits(NewCustomerDetails.Customer_ID, Credit_For_Customer_C);
                                //end for customer

                                // for referrer
                                CreditAvailableDetails = CreditAvailableDetails - Credit_For_Referrer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R = AddCreditTransaction(TransactionID, CustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Referrer_C, "Reward", "Successful");
                                AddCustomerTransaction(Credit_transactionID_R);
                                AddMerchantTransaction(Credit_transactionID_R);
                                UpdateCustomerCredits(CustomerDetails.Customer_ID, Credit_For_Referrer_C);
                                //update expiry date
                                updatemerchantexpiry(CampaignDetails.MerchantID);
                                //end of update expiry date
                                //end for reffer
                                //Transactionfee
                                CreditAvailableDetails = CreditAvailableDetails - TransactionFee;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R_transaction_fee = AddCreditTransaction(TransactionID, 0, CampaignDetails.MerchantID, TransactionFee, "TransactionFee", "Successful");
                                AddMerchantTransaction(Credit_transactionID_R_transaction_fee);

                                //End transactionFee

                            }
                            else
                            {
                                // for customer
                                TotalPendingCreditAvailable = TotalPendingCreditAvailable + Credit_For_Customer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID = AddCreditTransaction(TransactionID, NewCustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Customer_C, "Rebate", "Pending Payment");
                                AddCustomerTransaction(Credit_transactionID);
                                AddMerchantTransaction(Credit_transactionID);
                                UpdateCustomerCredits(NewCustomerDetails.Customer_ID, Credit_For_Customer_C);
                                // end for customer
                                //for referrer
                                TotalPendingCreditAvailable = TotalPendingCreditAvailable + Credit_For_Referrer_C;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R = AddCreditTransaction(TransactionID, CustomerDetails.Customer_ID, CampaignDetails.MerchantID, Credit_For_Referrer_C, "Reward", "Pending Payment");
                                AddCustomerTransaction(Credit_transactionID_R);
                                AddMerchantTransaction(Credit_transactionID_R);
                                UpdateCustomerCredits(CustomerDetails.Customer_ID, Credit_For_Referrer_C);
                                // updatemerchantexpiry(CustomerDetails.MerchantID);
                                UpdateCampaignsInactive();
                                Creditsbelowzero = 1;
                                //end for referrer
                                //Transactionfee
                                TotalPendingCreditAvailable = TotalPendingCreditAvailable + TransactionFee;
                                UpdateMerchantCredit(CampaignDetails.MerchantID, CreditAvailableDetails, TotalPendingCreditAvailable);
                                int Credit_transactionID_R_transaction_fee = AddCreditTransaction(TransactionID, 0, CampaignDetails.MerchantID, TransactionFee, "TransactionFee", "Pending Payment");
                                AddMerchantTransaction(Credit_transactionID_R_transaction_fee);

                                //End transactionFee
                            }
                        }
                        if (Creditsbelowzero != 1)
                        {
                            insertintooffer();
                            SendMail();
                        }
                        else
                        {
                            SendMailNoffer();
                        }
                        CheckReferral(CampaignDetails.MerchantID);

                    }
                    else
                    {
                        insertintooffer();
                        InsertIntoLog(Merchant_ID, NewCustomerDetails.Customer_ID, EmailId, false, false, false, true, false, false);
                    }
                }
                else
                {
                    AddtransactionDetails(NewCustomerDetails.Customer_ID, 0, CampaignDetails.WebsiteID, CampaignDetails.CampaignID, OrderID, Quantity, CampaignDetails.SKU_ID, Total, SubTotal, Discount, Tax, Tax2, SKU_ID_Referrer, Shipping, 0);
                    insertintooffer();
                }
            }
        }
        public void SendMailNoffer()
        {
            SubTotal = SubTotal.Replace('$', ' ');
            string EmailContent = "";
            string URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/RererrelSuccess.html");
            string URL_C = HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/RebateForNooffer.html");
            StreamReader SR = new StreamReader(URL_C);
            EmailContent = SR.ReadToEnd();
            SR.Close();
            if (Credit_For_Customer_C > 0)
            {
                if (Convert.ToBoolean(NewCustomerDetails.IsActive) == false)
                {

                    EmailContent = EmailContent.Replace("{COMPANYNAME}", CampaignDetails.Company_Name);
                    EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Customer_C).ToString());
                    EncryptDecrypt ED = new EncryptDecrypt();
                    string statsEncrypted;
                    statsEncrypted = ED.Encrypt(FirstName + "^" + EmailId, PublicKey);
                    string URLToSend = ConfigurationManager.AppSettings["pageURL"].ToString()+"Site/SignUp/" + HttpContext.Current.Server.UrlEncode(FirstName + "^" + EmailId);

                    EmailContent = EmailContent.Replace("{Login}", "<a href=" + URLToSend + ">activate your account</a>");
                    EmailContent = EmailContent.Replace("{OFFER}", "");
                }
                else
                {
                    EmailContent = EmailContent.Replace("{COMPANYNAME}", CampaignDetails.Company_Name);
                    EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Customer_C).ToString());
                    EmailContent = EmailContent.Replace("{Login}", "<a href='"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Site/Customer/Login'>login to your account</a>");
                    EmailContent = EmailContent.Replace("{OFFER}", "");
                }
                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                EmailContent = GetEmailHeaderFooter(EmailId).Replace("{BODYCONTENT}", EmailContent);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(EmailId, "Rebate Received!", EmailContent);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
            else
            {
                string EmailContentCompany = "";
                StreamReader SRCompany = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/CustomerPurchaseMade.html"));
                EmailContentCompany = SRCompany.ReadToEnd();
                EmailContentCompany = EmailContentCompany.Replace("{COMPANY NAME}", CampaignDetails.Company_Name).Replace("{OFFER}", "<a href='"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/Share/O/" + offerID + "' >Offer</a>");
                SRCompany.Close();
                EmailContentCompany = GetEmailHeaderFooter((EmailId)).Replace("{BODYCONTENT}", EmailContentCompany);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(EmailId, "Money back from " + CampaignDetails.Company_Name, EmailContentCompany);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
            StreamReader SR_R = new StreamReader(URL);
            string EmailContent_R = SR_R.ReadToEnd();
            SR_R.Close();
            if (EmailId != CustomerDetails.EmailID)
            {
                if (Convert.ToBoolean(CustomerDetails.IsActive) == false)
                {
                    EmailContent_R = EmailContent_R.Replace("{FIRSTNAME}", FirstName);
                    EmailContent_R = EmailContent_R.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Referrer_C).ToString());
                    EncryptDecrypt ED = new EncryptDecrypt();
                    string statsEncryptedFirstName;
                    statsEncryptedFirstName = ED.Encrypt(CustomerDetails.FirstName + "^" + CustomerDetails.EmailID, PublicKey);
                    string URLToSendR = ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SignUp/" + HttpContext.Current.Server.UrlEncode(CustomerDetails.FirstName + "^" + CustomerDetails.EmailID);
                    EmailContent_R = EmailContent_R.Replace("{Login}", "<a href=" + URLToSendR + ">activate your account</a>");

                    EmailContent_R = EmailContent_R.Replace("{LoginDetails}", "");
                }
                else
                {
                    EmailContent_R = EmailContent_R.Replace("{FIRSTNAME}", FirstName);
                    EmailContent_R = EmailContent_R.Replace("{CREDIT}", comman.FormatCredits((Credit_For_Referrer_C)).ToString());
                    EmailContent_R = EmailContent_R.Replace("{Login}", "<a href='"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Site/Customer/Login'>login to your account</a>");
                    EmailContent_R = EmailContent_R.Replace("{LoginDetails}", "");
                }
                EmailContent_R = EmailContent_R.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                EmailContent_R = GetEmailHeaderFooter(CustomerDetails.EmailID).Replace("{BODYCONTENT}", EmailContent_R);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(CustomerDetails.EmailID, "Successful Referral Made!", EmailContent_R);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
        }
        public void SendmailMoneyback()
        {
            string EmailContentCompany = "";
            StreamReader SRCompany = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/CustomerPurchaseMade.html"));
            EmailContentCompany = SRCompany.ReadToEnd();
            EmailContentCompany = EmailContentCompany.Replace("{COMPANY NAME}", CampaignDetails.Company_Name).Replace("{OFFER}", "<a href='"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/Offer/" + offerID + "' >Offer</a>");
            SRCompany.Close();
            EmailContentCompany = GetEmailHeaderFooter((EmailId)).Replace("{BODYCONTENT}", EmailContentCompany);
            threadSendMails = new System.Threading.Thread(delegate()
            {
                comman.SendMail(EmailId, "Money back from " + CampaignDetails.Company_Name, EmailContentCompany);
            });
            threadSendMails.IsBackground = true;
            threadSendMails.Start();
        }
        public void SendMail()
        {
            SubTotal = SubTotal.Replace('$', ' ');
            string EmailContent = "";
            string URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/RererrelSuccess.html");
            string URL_C = HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/RebateSucessfull.html");
            StreamReader SR = new StreamReader(URL_C);
            EmailContent = SR.ReadToEnd();
            SR.Close();
            if (Credit_For_Customer_C > 0)
            {
                if (Convert.ToBoolean(NewCustomerDetails.IsActive) == false)
                {

                    EmailContent = EmailContent.Replace("{COMPANYNAME}", CampaignDetails.Company_Name);
                    EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Customer_C).ToString());
                    EncryptDecrypt ED = new EncryptDecrypt();
                    string statsEncrypted;
                    statsEncrypted = ED.Encrypt(FirstName + "^" + EmailId, PublicKey);
                    string URLToSend = ConfigurationManager.AppSettings["pageURL"].ToString()+"Site/SignUp/" + HttpContext.Current.Server.UrlEncode(FirstName + "^" + EmailId);

                    EmailContent = EmailContent.Replace("{Login}", "<a href=" + URLToSend + ">activate your account</a>");
                    EmailContent = EmailContent.Replace("{OFFER}", "<a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Offer/" + offerID + "' >Offer</a>");
                }
                else
                {
                    EmailContent = EmailContent.Replace("{COMPANYNAME}", CampaignDetails.Company_Name);
                    EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Customer_C).ToString());
                    EmailContent = EmailContent.Replace("{Login}", "<a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/Customer/Login'>login to your account</a>");
                    EmailContent = EmailContent.Replace("{OFFER}", "<a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Offer/" + offerID + "' >Offer</a>");
                }
                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                EmailContent = GetEmailHeaderFooter(EmailId).Replace("{BODYCONTENT}", EmailContent);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(EmailId, "Rebate Received!", EmailContent);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
            else
            {
                string EmailContentCompany = "";
                StreamReader SRCompany = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/CustomerPurchaseMade.html"));
                EmailContentCompany = SRCompany.ReadToEnd();
                EmailContentCompany = EmailContentCompany.Replace("{COMPANY NAME}", CampaignDetails.Company_Name).Replace("{OFFER}", "<a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/O/" + offerID + "' >Offer</a>");
                SRCompany.Close();
                EmailContentCompany = GetEmailHeaderFooter((EmailId)).Replace("{BODYCONTENT}", EmailContentCompany);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(EmailId, "Money back from " + CampaignDetails.Company_Name, EmailContentCompany);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
            StreamReader SR_R = new StreamReader(URL);
            string EmailContent_R = SR_R.ReadToEnd();
            SR_R.Close();
            if (EmailId != CustomerDetails.EmailID)
            {
                if (Convert.ToBoolean(CustomerDetails.IsActive) == false)
                {
                    EmailContent_R = EmailContent_R.Replace("{FIRSTNAME}", FirstName);
                    EmailContent_R = EmailContent_R.Replace("{CREDIT}", comman.FormatCredits(Credit_For_Referrer_C).ToString());
                    EncryptDecrypt ED = new EncryptDecrypt();
                    string statsEncryptedFirstName;
                    statsEncryptedFirstName = ED.Encrypt(CustomerDetails.FirstName + "^" + CustomerDetails.EmailID, PublicKey);
                    string URLToSendR = ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SignUp/" + HttpContext.Current.Server.UrlEncode(CustomerDetails.FirstName + "^" + CustomerDetails.EmailID);
                    EmailContent_R = EmailContent_R.Replace("{Login}", "<a href=" + URLToSendR + ">activate your account</a>");

                    EmailContent_R = EmailContent_R.Replace("{LoginDetails}", "");
                }
                else
                {
                    EmailContent_R = EmailContent_R.Replace("{FIRSTNAME}", FirstName);
                    EmailContent_R = EmailContent_R.Replace("{CREDIT}", comman.FormatCredits((Credit_For_Referrer_C)).ToString());
                    EmailContent_R = EmailContent_R.Replace("{Login}", "<a href='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "Site/Customer/Login'>login to your account</a>");
                    EmailContent_R = EmailContent_R.Replace("{LoginDetails}", "");
                }
                EmailContent_R = EmailContent_R.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                EmailContent_R = GetEmailHeaderFooter(CustomerDetails.EmailID).Replace("{BODYCONTENT}", EmailContent_R);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(CustomerDetails.EmailID, "Successful Referral Made!", EmailContent_R);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();
            }
        }
        public void Threshold()
        {
            _Merchant objmerchant = new _Merchant();
            objmerchant.Merchant_ID = CampaignDetails.MerchantID;
            DAL.Plugin objplugin = new DAL.Plugin();
            SqlDataReader dr = objplugin.ThresholdAmount(objmerchant);
            if (dr.Read())
            {
                thresholdAmount = Convert.ToInt32(dr["value"].ToString());
            }

        }
        public void CreditCard(string Amount, int PlanID)
        {
            _Credit_card obj = new _Credit_card();
            obj.Merchant_ID = CampaignDetails.MerchantID;
            Credit_card objCredit = new Credit_card();
            SqlDataReader DRCredits = objCredit.GetMerchantCreditDetails(obj);
            ServiceSoapClient ws = new ServiceSoapClient();
            if (DRCredits.Read())
            {
                try
                {
                    EricProject.LiveCreditCard.Transaction txn = new EricProject.LiveCreditCard.Transaction();
                    // set correct credential values
                    txn.ExactID = ConfigurationManager.AppSettings["exactID"].ToString();
                    txn.Password = ConfigurationManager.AppSettings["CreditPassword"].ToString();
                    txn.Transaction_Type = "00";
                    txn.Card_Number = "";
                    txn.CardHoldersName = DRCredits["Cardholder_Name"].ToString();
                    txn.DollarAmount = Amount.ToString();
                    txn.Expiry_Date = DRCredits["Expiry_Date"].ToString();
                    txn.User_Name = "";
                    txn.Secure_AuthResult = "";
                    txn.Ecommerce_Flag = "";
                    txn.XID = "";
                    txn.CardType = "";
                    txn.CAVV = "";
                    txn.CAVV_Algorithm = "";
                    txn.Reference_No = "";
                    txn.Customer_Ref = "";
                    txn.Reference_3 = "";
                    txn.Client_IP = "";					                    //This value is only used for fraud investigation.
                    txn.Client_Email = "saurabh_tyagi@seologistics.com";		//This value is only used for fraud investigation.
                    txn.Language = "en";			//English="en" French="fr"
                    txn.Track1 = "";
                    txn.Track2 = "";
                    txn.Authorization_Num = "";
                    txn.Transaction_Tag = "";
                    txn.VerificationStr1 = "";
                    txn.VerificationStr2 = "123";
                    txn.CVD_Presence_Ind = "";
                    txn.Secure_AuthRequired = "";                   
                    txn.CardType = DRCredits["Card_Type"].ToString();
                    txn.TransarmorToken = DRCredits["TransarmorToken"].ToString();

                    TransactionResult result = ws.SendAndCommit(txn);
                    //Response.Write(result.CTR);
                    //Response.Write("e4 Transarmor Token: " : " + re+ result.TransarmorToken);
                    //Response.Write("e4 messagesult.EXact_Message);
                    //Response.Write("bank resp code: " + result.Bank_Resp_Code);
                    //Response.Write("bank message: " + result.Bank_Message);
                    //Response.Write(result.CardType);
                    DAL.Transaction sqlTransaction = new DAL.Transaction();
                    if (result.Bank_Message == "Approved")
                    {

                        if (PlanID != 0)
                        {
                            _CreditPlanMaster objCreditPlanMaster = new _CreditPlanMaster();
                            DAL.CreditPlanMaster sqlCreditPlanMaster = new DAL.CreditPlanMaster();
                            objCreditPlanMaster.CreditPlanId = Convert.ToInt32(PlanID);
                            SqlDataReader drCreditPlan = sqlCreditPlanMaster.BindCreditPlan(objCreditPlanMaster);
                            while (drCreditPlan.Read())
                            {
                                PlanCredit = Convert.ToInt32(drCreditPlan["Received_Credits"].ToString());
                                PlanAmont = drCreditPlan["Payment_Amount"].ToString();
                            }
                        }

                        //Add credit to merchant first

                        _Merchant_Credits objMerchantCredits1 = new _Merchant_Credits();
                        DAL.Plugin sqlMerchantCredits1 = new DAL.Plugin();
                        objMerchantCredits1.MerchantID = Convert.ToInt32(CampaignDetails.MerchantID);
                        objMerchantCredits1.AvailableCredit = PlanCredit;
                        objMerchantCredits1.PendingCredit = 0;
                        objMerchantCredits1.MonthlyFeeApplicable = false;
                        try
                        {

                            sqlMerchantCredits1.InsertIntoMerchant_Credits(objMerchantCredits1);
                        }
                        catch { }

                        //Get total available credit from merchant credit after adding credits
                        _Merchant_Credits objMerchantCredits2 = new _Merchant_Credits();
                        DAL.Plugin sqlMerchantCredits2 = new DAL.Plugin();
                        objMerchantCredits2.MerchantID = Convert.ToInt32(CampaignDetails.MerchantID);
                        SqlDataReader drMerchantCredits2 = sqlMerchantCredits2.BindTotalAvailableMerchantCreditByMerchantId(objMerchantCredits2);
                        try
                        {
                            if (drMerchantCredits2.Read())
                            {
                                TotalAvailableCreditPurchase = Convert.ToInt32(drMerchantCredits2["TotalAvailableCredit"].ToString());
                            }
                        }
                        catch { }

                        //Pending Credit
                        _Transaction objTransaction1 = new _Transaction();
                        objTransaction1.MerchantId = Convert.ToInt32(CampaignDetails.MerchantID);
                        SqlDataReader dr = sqlTransaction.CheckTransactionHistoryPendingByMerchantId(objTransaction1);
                        int Credits = 0;
                        int Temp = 0;
                        while (dr.Read())
                        {
                            Temp = TotalAvailableCreditPurchase;
                            Credits = Convert.ToInt32(dr["CREDITS"].ToString());
                            if (Credits < Temp)
                            {
                                Temp = Temp - Credits;
                                //Update credit transaction table status
                                _Credit_Transaction objtrans = new _Credit_Transaction();
                                objtrans.Merchant_id = Convert.ToInt32(CampaignDetails.MerchantID);
                                objtrans.CustomerTransactionID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());

                                try
                                {
                                    sqlTransaction.UpdateCreditTransactionStatusByMerchantCustomerTransactionId(objtrans);
                                }
                                catch { }


                                //Update Merchant Credits for pending Credits
                                _Merchant_Credits objMerchantCredits = new _Merchant_Credits();
                                DAL.Plugin sqlMerchantCredits = new DAL.Plugin();
                                objMerchantCredits.MerchantID = Convert.ToInt32(CampaignDetails.MerchantID);
                                //string Credits_Remaining = dr["Credits_Remaining"].ToString().Replace('-',' ').Trim();
                                //decimal DCredits_Remaining = decimal.Parse(Credits_Remaining);
                                //int iCredits_Remaining = Convert.ToInt32(DCredits_Remaining);
                                //objMerchantCredits.PendingCredit = iCredits_Remaining;
                                //objMerchantCredits.PurchaseCredit = PlanCredit;
                                objMerchantCredits.PendingCredit = Convert.ToInt32(dr["Credits"].ToString());
                                objMerchantCredits.PurchaseCredit = PlanCredit;

                                try
                                {
                                    sqlMerchantCredits.UpdateMerchantCreditsByMerchantId(objMerchantCredits);
                                }
                                catch { }
                                //Update customer credits
                                _credit_details objcredit_details = new _credit_details();
                                DAL.Plugin sqlCreditDetails = new DAL.Plugin();
                                objcredit_details.Referral_ID = Convert.ToInt32(dr["Referrer_Id"].ToString());
                                objcredit_details.Customer_ID = Convert.ToInt32(dr["CUSTOMER_Id"].ToString());
                                objcredit_details.Referral_Credits = Convert.ToInt32(dr["REFERRER_CREDITS"].ToString());
                                objcredit_details.Customer_Credits = Convert.ToInt32(dr["CUSTOMER_CREDITS"].ToString());
                                objcredit_details.Merchant_ID = Convert.ToInt32(CampaignDetails.MerchantID);
                                objcredit_details.Customer_Transaction_ID = Convert.ToInt32(dr["Customer_Transaction_ID"].ToString());
                                try
                                {
                                    sqlCreditDetails.UpdateCreditDetailsByReffferCustomerId(objcredit_details);
                                }
                                catch { }
                            }
                        }
                        //Pending Credit



                        //Insert credit transaction table status
                        _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                        objCredit_Transaction.Transaction_id = 0;
                        objCredit_Transaction.Customer_id = 0;
                        objCredit_Transaction.Merchant_id = Convert.ToInt32(CampaignDetails.MerchantID);
                        objCredit_Transaction.Amount = PlanCredit;
                        objCredit_Transaction.Type = "Credit Purchase";
                        objCredit_Transaction.Status = "Successful";
                        objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                        objCredit_Transaction.IS_Purchase = true;
                        int result1 = 0;
                        try
                        {
                            result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                        }
                        catch { }

                        //Insert  merchant transaction table
                        _Transaction objTransaction = new _Transaction();
                        objTransaction.CreditTransactionId = result1;
                        objTransaction.CreditPlanId = Convert.ToInt32(PlanID);
                        objTransaction.Credit_Card_ID = Convert.ToInt32(DRCredits["Credit_Card_Id"].ToString());
                        int result2 = 0;
                        try
                        {
                            result2 = sqlTransaction.InsertInToMerchant_Transaction(objTransaction);
                        }
                        catch { }

                        DBAccess.InstanceCreation().disconnect();
                        string EmailContent = "";
                        StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/AUto-Replenish.html"));
                        EmailContent = SR.ReadToEnd();
                        EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(PlanCredit)).Replace("{AMOUNT}", string.Format("{0:0.00}", Convert.ToDecimal(PlanAmont))).Replace("{AVAILABLECREDITS}", comman.FormatCredits(TotalAvailableCreditPurchase));
                        EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                        EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                        SR.Close();
                        EmailContent = GetEmailHeaderFooter((CampaignDetails.Merchant_email_ID)).Replace("{BODYCONTENT}", EmailContent);
                        threadSendMails = new System.Threading.Thread(delegate()
                        {
                            comman.SendMail(CampaignDetails.Merchant_email_ID, ConfigurationManager.AppSettings["site_name"].ToString() + " Receipt", EmailContent);
                        });
                        threadSendMails.IsBackground = true;
                        threadSendMails.Start();

                    }
                    else
                    {
                        _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                        objCredit_Transaction.Transaction_id = 0;
                        objCredit_Transaction.Customer_id = 0;
                        objCredit_Transaction.Merchant_id = Convert.ToInt32(CampaignDetails.MerchantID);
                        objCredit_Transaction.Amount = PlanCredit;
                        objCredit_Transaction.Type = "Credit Purchase";
                        objCredit_Transaction.Status = "Failed";
                        objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                        objCredit_Transaction.IS_Purchase = true;
                        int result1 = 0;
                        try
                        {
                            result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                        }
                        catch { }

                        string EmailContent = "";
                        if (CreditAvailableDetails - TransactionFee - totalcredits >= 0)
                        {
                            StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/AUto-Replenish_Failed.html"));
                            EmailContent = SR.ReadToEnd();
                            EmailContent = EmailContent.Replace("{CREDIT}", comman.FormatCredits(CreditAvailableDetails - TransactionFee - totalcredits));
                            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                            EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                            SR.Close();
                            EmailContent = GetEmailHeaderFooter((CampaignDetails.Merchant_email_ID)).Replace("{BODYCONTENT}", EmailContent);
                            threadSendMails = new System.Threading.Thread(delegate()
                            {
                                comman.SendMail(CampaignDetails.Merchant_email_ID, "Auto Replenish Failed", EmailContent);
                                comman.SendMail("admin@tapiton.com", "Auto-replenish transaction failed", "Auto Replenish Failed");
                            });
                            threadSendMails.IsBackground = true;
                            threadSendMails.Start();
                        }
                        else
                        {
                            StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/Auto_replenish-failed_Negative.html"));
                            EmailContent = SR.ReadToEnd();
                            EmailContent = EmailContent.Replace("{CREDIT}", (CreditAvailableDetails - TransactionFee - totalcredits).ToString());
                            EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                            EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                            SR.Close();
                            EmailContent = GetEmailHeaderFooter((CampaignDetails.Merchant_email_ID)).Replace("{BODYCONTENT}", EmailContent);
                            threadSendMails = new System.Threading.Thread(delegate()
                            {
                                comman.SendMail(CampaignDetails.Merchant_email_ID, "Auto Replenish Failed", EmailContent);
                                comman.SendMail("tanu_garg@seologistics.com", "Error", "Auto Replenish Failed");
                                comman.SendMail("admin@tapiton.com", "Error", "Auto Replenish Failed");
                            });
                            threadSendMails.IsBackground = true;
                            threadSendMails.Start();
                        }
                        _Transaction objTransaction1 = new _Transaction();
                        objTransaction1.MerchantId = Convert.ToInt32(CampaignDetails.MerchantID);
                        SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                        SqlDataReader drdetails = sqlTransaction.GetDetails(objTransaction1);
                        if (dr.Read())
                        {
                            if (drdetails.Read())
                            {
                                StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/SupportMail.html"));
                                EmailContent = SRSupport.ReadToEnd();
                                EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", Amount.ToString());
                                EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToDecimal(Amount) * 100).ToString());
                                EmailContent = EmailContent.Replace("{name}", drdetails["First_Name"].ToString() + drdetails["Last_Name"].ToString());
                                EmailContent = EmailContent.Replace("{merchant name}", drdetails["Company_Name"].ToString());
                                EmailContent = EmailContent.Replace("{merchant_id}", (CampaignDetails.MerchantID).ToString());
                                EmailContent = EmailContent.Replace("{date joined}", Convert.ToDateTime(drdetails["Created_On"]).ToString("MMM dd,yyyy"));
                                EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                                SRSupport.Close();
                                EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                                threadSendMails = new System.Threading.Thread(delegate()
                                {
                                    comman.SendMail("tanu_garg@seologistics.com", "Auto-replenish transaction failed", EmailContent);
                                    comman.SendMail("admin@tapiton.com", "Auto-replenish transaction failed", EmailContent);
                                });
                                threadSendMails.IsBackground = true;
                                threadSendMails.Start();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _Credit_Transaction objCredit_Transaction = new _Credit_Transaction();
                    objCredit_Transaction.Transaction_id = 0;
                    objCredit_Transaction.Customer_id = 0;
                    objCredit_Transaction.Merchant_id = Convert.ToInt32(CampaignDetails.MerchantID);
                    objCredit_Transaction.Amount = PlanCredit;
                    objCredit_Transaction.Type = "Credit Purchase";
                    objCredit_Transaction.Status = "Failed";
                    objCredit_Transaction.Amount_redeemed = Convert.ToDecimal("0.00");
                    objCredit_Transaction.IS_Purchase = true;
                    DAL.Transaction sqlTransaction = new DAL.Transaction();
                    int result1 = 0;
                    try
                    {
                        result1 = sqlTransaction.InsertIntoCredit_Transaction(objCredit_Transaction);
                    }
                    catch { }
                    threadSendMails = new System.Threading.Thread(delegate()
                        {
                            comman.SendMail("admin@tapiton.com", "Error in first data API", ex.Message);
                            comman.SendMail("tanu_garg@seologistics.com", "Error in first data API", ex.Message);
                        });
                    threadSendMails.IsBackground = true;
                    string EmailContent = "";
                    threadSendMails.Start(); _Transaction objTransaction1 = new _Transaction();
                    objTransaction1.MerchantId = Convert.ToInt32(CampaignDetails.MerchantID);
                    SqlDataReader dr = sqlTransaction.GetTotalCredits(objTransaction1);
                    StreamReader SRSupport = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/SupportMail.html"));
                    EmailContent = SRSupport.ReadToEnd();
                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_USD}", Amount.ToString());
                    EmailContent = EmailContent.Replace("{PURCHASE_AMOUNT_CREDITS}", (Convert.ToInt32(Amount) * 100).ToString());
                    EmailContent = EmailContent.Replace("{name}", CampaignDetails.First_Name + CampaignDetails.Last_Name);
                    EmailContent = EmailContent.Replace("{merchant name}", CampaignDetails.Company_Name);
                    EmailContent = EmailContent.Replace("{merchant_id}", (CampaignDetails.MerchantID).ToString());
                    EmailContent = EmailContent.Replace("{date joined}", (CampaignDetails.Created_On).ToString());
                    EmailContent = EmailContent.Replace("{total rewards}", dr["TotalCredits"].ToString());
                    SRSupport.Close();
                    EmailContent = GetEmailHeaderFooter(("admin@tapiton.com")).Replace("{BODYCONTENT}", EmailContent);
                    threadSendMails = new System.Threading.Thread(delegate()
                    {
                        comman.SendMail("tanu_garg@seologistics.com", "Auto-replenish transaction failed", EmailContent);
                        comman.SendMail("admin@tapiton.com", "Auto-replenish transaction failed", EmailContent);
                    });
                    threadSendMails.IsBackground = true;
                    threadSendMails.Start();
                }
            }
        }
        public void CheckReferral(int MerchantID)
        {
            DAL.MerchantReferral sqlobj1 = new DAL.MerchantReferral();
            _MerchantReferral objbStatus = new _MerchantReferral();
            objbStatus.Referral_Merchant_ID = MerchantID;
            SqlDataReader DRReferral = sqlobj1.CheckreferralDetails(objbStatus);
            if (DRReferral.Read())
            {
                if (DRReferral["ReferralType"].ToString().Contains("Merchant"))
                {
                    BindCampaign(CampaignDetails.MerchantID);
                }
                else
                {
                    CustomerReferralUpdate(CampaignDetails.MerchantID);
                }
            }
        }
        public void CustomerReferralUpdate(int MerchantID)
        {
            DAL.MerchantReferral sqlobj1 = new DAL.MerchantReferral();
            _MerchantReferral objbStatus = new _MerchantReferral();
            objbStatus.Referral_Merchant_ID = MerchantID;
            objbStatus.Status = "Successfully Referred";
            SqlDataReader referred = sqlobj1.UpdateCustomerReferral(objbStatus);
            if (referred.Read())
            {
                int Credit_transactionID_R = AddCreditTransactionRefer(0, Convert.ToInt32(referred["Referrer_ID"].ToString()), CampaignDetails.MerchantID, 5000, "Merchant Referral", "Successful");
                AddCustomerTransaction(Credit_transactionID_R);
                _credit_details obj = new _credit_details();
                obj.Customer_ID = Convert.ToInt32(referred["Referrer_ID"].ToString());
                obj.pending_credits = 5000;
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader DRresult = sqlobj.InsetIntoCustomer_CreditsBYRefer(obj);
                if (DRresult.Read())
                {
                    string EmailContent = "";
                    StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Plugin/Customer_Referral.html"));
                    EmailContent = SR.ReadToEnd();
                    EmailContent = EmailContent.Replace("{Amount}", "5000");
                    EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                    EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                    SR.Close();
                    EmailContent = GetEmailHeaderFooter(DRresult["Email_Id"].ToString()).Replace("{BODYCONTENT}", EmailContent);
                    threadSendMails = new System.Threading.Thread(delegate()
                    {
                        comman.SendMail(DRresult["Email_Id"].ToString(), "Successful Merchant Referral Made!", EmailContent);
                    });
                    threadSendMails.IsBackground = true;
                    threadSendMails.Start();
                }
            }
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + HttpContext.Current.Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }
        protected void BindCampaign(int MerchantID)
        {
            DAL.MerchantReferral sqlobj1 = new DAL.MerchantReferral();
            _MerchantReferral objbStatus = new _MerchantReferral();
            objbStatus.Referral_Merchant_ID = MerchantID;
            objbStatus.Status = "Successfully Referred";
            int referred = sqlobj1.UpdateReferralCampin(objbStatus);
            if (referred == 1)
            {
                DAL.MerchantReferral sqlobjref = new DAL.MerchantReferral();
                _MerchantReferral objbStatusref = new _MerchantReferral();
                objbStatusref.Referral_Merchant_ID = MerchantID;
                SqlDataReader DR = sqlobjref.GetTotalReferralsDetails(objbStatusref);
                if (DR.Read())
                {
                    string EmailContent = "";
                    StreamReader SR = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/Merchant_Referral.html"));
                    EmailContent = SR.ReadToEnd();
                    EmailContent = EmailContent.Replace("{MerchantName}", CampaignDetails.Company_Name);
                    if (DR["free_period_expiry_date"].ToString() == "")
                    {
                        if (DR["Referrals"].ToString() == "1")
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "Your free trial period has been extended by 1 year.");
                            EmailContent = EmailContent.Replace("{1morereferral}", "If you make 1 more referral, your free trial will be extended for 2 more years.");
                        }
                        if (DR["Referrals"].ToString() == "2")
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "Your free trial period has been extended by 1 year.");
                            EmailContent = EmailContent.Replace("{1morereferral}", "If you make 1 more referral,you may continue using the site without monthly payments forever'.");
                        }
                        if (Convert.ToInt32(DR["Referrals"].ToString()) >= 3)
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "You may now continue using the site for without payments forever.");
                            EmailContent = EmailContent.Replace("{1morereferral}", "");
                        }
                    }
                    else
                    {
                        if (DR["Referrals"].ToString() == "1")
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "You may now continue using the site for free until " + Convert.ToDateTime(DR["free_period_expiry_date"]).ToString("MMM dd,yyyy"));
                            EmailContent = EmailContent.Replace("{1morereferral}", "If you make 1 more referral, you can continue using the site until " + Convert.ToDateTime(DR["free_period_expiry_date"].ToString()).AddYears(2).ToString("MMM dd,yyyy") + ".");
                        }
                        if (DR["Referrals"].ToString() == "2")
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "You may now continue using the site for free until " + Convert.ToDateTime(DR["free_period_expiry_date"]).ToString("MMM dd,yyyy"));
                            EmailContent = EmailContent.Replace("{1morereferral}", "If you make 1 more referral,you may continue using the site without monthly payments forever'.");
                        }
                        if (Convert.ToInt32(DR["Referrals"].ToString()) >= 3)
                        {
                            EmailContent = EmailContent.Replace("{FreePeriod}", "You may now continue using the site for without payments forever.");
                            EmailContent = EmailContent.Replace("{1morereferral}", "");
                        }
                    }
                    SR.Close();
                    EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
                    EmailContent = GetEmailHeaderFooter((DR["Email_Id"].ToString())).Replace("{BODYCONTENT}", EmailContent);
                    threadSendMails = new System.Threading.Thread(delegate()
                    {
                        comman.SendMail(DR["Email_Id"].ToString(), "Successful Referral Made!", EmailContent);
                    });
                    threadSendMails.IsBackground = true;
                    threadSendMails.Start();
                }
            }
        }
        protected void updatemerchantexpiry(int MerchantID)
        {
            _Merchant objbStatus = new _Merchant();
            objbStatus.Merchant_ID = MerchantID;
            int result = Getmerchantexpiry(MerchantID);
            if (result > 0)
                objbStatus.free_period_expiry_date = DateTime.Now.AddMonths(6);
            else
                objbStatus.free_period_expiry_date = DateTime.Now.AddMonths(3);
            DAL.Plugin sqlobj = new DAL.Plugin();
            sqlobj.update_merchant_expiry(objbStatus);
        }
        protected int Getmerchantexpiry(int MerchantID)
        {
            _Merchant objbStatus = new _Merchant();
            objbStatus.Merchant_ID = MerchantID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.Get_merchant_expiry(objbStatus);
            return result;
        }
        public int AddCreditTransaction(int Credit_transaction_ID, int Credit_customer_id, int Credit_Merchant_id, decimal Credit_Amount, string Credit_Type, string Credit_Status)
        {
            _Credit_Transaction obj = new _Credit_Transaction();
            obj.Transaction_id = Credit_transaction_ID;
            obj.Customer_id = Credit_customer_id;
            obj.Merchant_id = Credit_Merchant_id;
            obj.Amount = Credit_Amount;
            obj.Type = Credit_Type;
            obj.Status = Credit_Status;
            obj.Amount_redeemed = Convert.ToDecimal("0.00");
            obj.IS_Purchase = false;
            DAL.Transaction sqlobj = new DAL.Transaction();
            int result = sqlobj.InsertIntoCredit_Transaction(obj);
            return result;
        }
        public int AddCreditTransactionRefer(int Credit_transaction_ID, int Credit_customer_id, int Credit_Merchant_id, decimal Credit_Amount, string Credit_Type, string Credit_Status)
        {
            _Credit_Transaction obj = new _Credit_Transaction();
            obj.Transaction_id = Credit_transaction_ID;
            obj.Customer_id = Credit_customer_id;
            obj.Merchant_id = Credit_Merchant_id;
            obj.Amount = Credit_Amount;
            obj.Type = Credit_Type;
            obj.Status = Credit_Status;
            obj.Amount_redeemed = Convert.ToDecimal("0.00");
            obj.IS_Purchase = false;
            DAL.Transaction sqlobj = new DAL.Transaction();
            int result = sqlobj.InsertInToCredits_TransactionRefer(obj);
            return result;
        }
        public void CreditAvailable()
        {
            _credit_details obj = new _credit_details();
            obj.Merchant_ID = Merchant_ID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader drCheckDetails = sqlobj.merchantcreditAvailable(obj);
            if (drCheckDetails.Read())
            {
                CreditAvailableDetails = Convert.ToDecimal(drCheckDetails["TotalAvailableCredit"].ToString());
                TotalPendingCreditAvailable = Convert.ToDecimal(drCheckDetails["TotalPendingCredit"].ToString());
            }

        }
        public int AddCustomerTransaction(int Credit_Transaction_id)
        {
            _Customer_Transaction obj = new _Customer_Transaction();
            obj.Credit_Transaction_id = Credit_Transaction_id;
            obj.Unredeemed_Credits_Remaining = Convert.ToDecimal("0.00");
            obj.Total_redeemed_Credits = Convert.ToDecimal("0.00");
            DAL.Transaction sqlobj = new DAL.Transaction();
            int result = sqlobj.InsertIntoCustomer_Transaction(obj);
            return result;
        }
        public int AddMerchantTransaction(int Credit_Transaction_Id)
        {
            _Merchant_Transaction obj = new _Merchant_Transaction();
            obj.Credit_Transaction_Id = Credit_Transaction_Id;
            obj.Credit_Plan_Id = Convert.ToInt32("0");
            obj.Credit_Card_ID = Convert.ToInt32("0");
            DAL.Transaction sqlobj = new DAL.Transaction();
            int result = sqlobj.InsertIntoMerchant_Transaction(obj);
            return result;
        }
        public int UpdateCustomerCredits(int Customer_ID_details, decimal pending_credits)
        {
            _credit_details obj = new _credit_details();
            obj.Customer_ID = Customer_ID_details;
            obj.pending_credits = pending_credits;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.InsetIntoCustomer_Credits(obj);
            return result;
        }
        public int UpdateMerchantCredit(int Merchant_ID, decimal TotalAvailableCredit, decimal TotalPendingCredits)
        {
            _credit_details obj = new _credit_details();
            obj.Merchant_ID = Merchant_ID;
            obj.TotalAvailableCredit = TotalAvailableCredit;
            obj.TotalPendingCredit = TotalPendingCreditAvailable;
            DAL.Plugin sqlobj = new DAL.Plugin();
            int result = sqlobj.UpdateMerchantCredits(obj);
            return result;
        }
        public void SKUDetails(int TransactionID)
        {
            _TransactionDetails obj = new _TransactionDetails();
            obj.Transaction_ID = TransactionID;
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader SKUDetails = sqlobj.SKUDetails(obj);
            if (SKUDetails.Read())
            {
                SKU_ID_Referrer = SKUDetails["SKU_ID"].ToString();
            }


        }
        public void UpdateCampaignsInactive()
        {
            _Merchant obj = new _Merchant();
            obj.Merchant_ID = CampaignDetails.MerchantID;
            DAL.Plugin objsql = new DAL.Plugin();
            objsql.UpdateCampaignsInactive(obj);
        }
        private void insertintooffer()
        {
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = "";
            sqlOffer.Customer_Id = NewCustomerDetails.Customer_ID.ToString();
            sqlOffer.Campaign_Id = CampaignDetails.CampaignID.ToString();
            sqlOffer.Expiry_Time = DateTime.Now.AddDays(CampaignDetails.Expiry_days);
            sqlOffer.Clicks = "0";
            sqlOffer.Reach = "0";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 0;
            sqlOffer.TransactionId = TransactionID;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerID = offerobj.InsertInToOfferDetails(sqlOffer);
        }
        private void referral()
        {
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/Share/" + offerID;
            sqlOffer.Customer_Id = NewCustomerDetails.Customer_ID.ToString();
            sqlOffer.Campaign_Id = CampaignDetails.CampaignID.ToString();
            sqlOffer.Expiry_Time = Convert.ToDateTime(DateTime.Now.ToString());
            sqlOffer.Clicks = "0";
            sqlOffer.Reach = "0";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 4;
            sqlOffer.TransactionId = TransactionID;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerobj.InsertInToOfferDetails(sqlOffer);
        }
        private void sales()
        {
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/Share/" + offerID;
            sqlOffer.Customer_Id = NewCustomerDetails.Customer_ID.ToString();
            sqlOffer.Campaign_Id = CampaignDetails.CampaignID.ToString();
            sqlOffer.Expiry_Time = Convert.ToDateTime(DateTime.Now.ToString());
            sqlOffer.Clicks = "0";
            sqlOffer.Reach = "0";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 5;
            sqlOffer.TransactionId = TransactionID;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerobj.InsertInToOfferDetails(sqlOffer);
        }
        private void referral_count()
        {
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/Share/" + offerID;
            sqlOffer.Customer_Id = NewCustomerDetails.Customer_ID.ToString();
            sqlOffer.Campaign_Id = CampaignDetails.CampaignID.ToString();
            sqlOffer.Expiry_Time = Convert.ToDateTime(DateTime.Now.ToString());
            sqlOffer.Clicks = "0";
            sqlOffer.Reach = "0";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 6;
            sqlOffer.TransactionId = TransactionID;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerobj.InsertInToOfferDetails(sqlOffer);
        }
        private void Campaign_stats()
        {
            _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            objCampaigns_Stats.Campaign_Id = Convert.ToInt32(CampaignDetails.CampaignID.ToString());
            objCampaigns_Stats.FB_click = 0;
            objCampaigns_Stats.FBShare_Click = 0;
            objCampaigns_Stats.Link_Click = 0;
            objCampaigns_Stats.Proceed_Click = 0;
            objCampaigns_Stats.Tweet_Click = 0;
            objCampaigns_Stats.Email_Click = 0;
            objCampaigns_Stats.EmailSubmit_Click = 0;
            objCampaigns_Stats.StatusClick = 0;
            int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);


        }
        private void updateReferrerURL()
        {
            int referrerID = 0;
            if (HttpContext.Current.Request.Cookies["ReferrerURL"] != null)
                referrerID = Convert.ToInt32(HttpContext.Current.Request.Cookies["ReferrerURL"].Value);
            if (referrerID > 0)
            {
                _ReferrerURL objReferrerURL = new _ReferrerURL();
                DAL.Plugin ReferrerURLobj = new DAL.Plugin();
                objReferrerURL.UrlReferrer_ID = Convert.ToInt32(HttpContext.Current.Request.Cookies["ReferrerURL"].Value);
                objReferrerURL.Offer_ID = Convert.ToInt32(CookieID);
                objReferrerURL.Referrer_ID = TransactionID;
                objReferrerURL.URL = "";
                objReferrerURL.Status = "Purchased";
                int resultReferrerURL = ReferrerURLobj.InsertIntoReferrerURL(objReferrerURL);
            }
            HttpContext.Current.Response.Cookies["ReferrerURL"].Expires = DateTime.Now;
            //HttpContext.Current.Cache.Remove("ReferrerURL");
        }
        private void AvailableCreditsNotifications(decimal AvailableAmount)
        {
            threadSendMails = new System.Threading.Thread(delegate()
            {
                comman.SendMail("tanu_garg@seologistics.com", "subject", AvailableAmount.ToString() + " - " + CampaignDetails.Merchant_email_ID);
            });
            threadSendMails.IsBackground = true;
            threadSendMails.Start();

            string EmailContent = "";
            string URL = "";
            string Subject = "";
            if (AvailableAmount < 0)
            {
                EmailContent = "";
                URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/CreditBelow0.htm");
                StreamReader SR = new StreamReader(URL);
                EmailContent = SR.ReadToEnd();
                EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                SR.Close();
                Subject = "No Remaining Credits";
            }
            else if (AvailableAmount < 2000)
            {
                EmailContent = "";
                URL = HttpContext.Current.Server.MapPath("~/EmailTemplate/Merchant/CreditBelow2000.htm");
                StreamReader SR = new StreamReader(URL);
                EmailContent = SR.ReadToEnd();
                EmailContent = EmailContent.Replace("{PageURL}", ConfigurationManager.AppSettings["pageURL"].ToString());
                SR.Close();
                Subject = "Only 2,000 Credits Remaining";
            }
            if (AvailableAmount < 2000)
            {
                StreamReader SR = new StreamReader(URL);
                EmailContent = SR.ReadToEnd();
                SR.Close();

                EmailContent = EmailContent.Replace("{REBATE}", Credit_For_Customer_C.ToString());
                EmailContent = EmailContent.Replace("{REWARD}", Credit_For_Referrer_C.ToString());
                EmailContent = EmailContent.Replace("{BALANCE}", AvailableAmount.ToString());
                EmailContent = EmailContent.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());

                EmailContent = GetEmailHeaderFooter(CampaignDetails.Merchant_email_ID).Replace("{BODYCONTENT}", EmailContent);
                threadSendMails = new System.Threading.Thread(delegate()
                {
                    comman.SendMail(CampaignDetails.Merchant_email_ID, Subject, EmailContent);
                });
                threadSendMails.IsBackground = true;
                threadSendMails.Start();

            }
        }
        //Short url
        public string ShortURL(string LongUrl)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"longUrl\":\"" + LongUrl + "\"}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Response.Write(responseText);
                int iStart = responseText.IndexOf("id") + 6;
                int iEnd = responseText.IndexOf("longUrl") - 5;
                responseText = responseText.Substring(iStart, iEnd - iStart);
                return responseText;
            }
        }
    }
}