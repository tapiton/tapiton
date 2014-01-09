using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using BusinessObject;
using System.Configuration;
namespace EricProject
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RegisterRoute(System.Web.Routing.RouteTable.Routes);
        }
        void RegisterRoute(System.Web.Routing.RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");
            routes.MapPageRoute("Index", "Home", "~/Site/Index.aspx");
            routes.MapPageRoute("IndexWithParameter", "Site/Home/{RID}", "~/Site/Index.aspx");
            routes.MapPageRoute("HowItWorks", "Site/How-It-Works", "~/Site/HowItWorks.aspx");
            routes.MapPageRoute("Features", "Site/Features", "~/Site/Features.aspx");
            routes.MapPageRoute("SiteFAQ", "Site/FAQ", "~/Site/Site_FAQ.aspx");
            routes.MapPageRoute("Prices", "Site/Pricing", "~/Site/Prices.aspx");
            routes.MapPageRoute("MerchantDocumentation", "Site/Merchant/Documentation", "~/Site/Documentation.aspx");
            routes.MapPageRoute("SignUp", "Site/SignUp/{*FirstName}", "~/Site/SignUp.aspx");
            routes.MapPageRoute("LearnMore", "Site/LearnMore", "~/Site/LearnMore.aspx");
            routes.MapPageRoute("CustomerLogin", "Site/Customer/Login", "~/Site/CustomerLogin.aspx");
            routes.MapPageRoute("CustomerProfile", "Site/Customer/Profile", "~/Site/CustomerProfile.aspx");
            routes.MapPageRoute("PrivacyPolicy", "Site/Privacy-Policy", "~/Site/PrivacyPolicy.aspx");
            routes.MapPageRoute("TermsAndConditions", "Site/Terms-And-Conditions", "~/Site/TermsAndConditions.aspx");
            routes.MapPageRoute("Logout", "Site/Logout", "~/Site/Logout.aspx");
            routes.MapPageRoute("CustomerCreditDetails", "Site/Customer/CreditDetails", "~/Site/CustomerCreditDetails.aspx");
            routes.MapPageRoute("MerchantProfile", "Site/Merchant/Profile", "~/Site/MerchantProfile.aspx");
            routes.MapPageRoute("MerchantActivation", "Site/Merchant/Activation", "~/Site/MerchantActivation.aspx");
            routes.MapPageRoute("CustomerActivation", "Site/Custometer/Activation", "~/Site/CustomerActivation.aspx");
            routes.MapPageRoute("CustomerRedeem", "Site/Custometer/RedeemDetails", "~/Site/Reddem_Details.aspx");
            routes.MapPageRoute("MerchantDashboard", "Site/Merchant/Dashboard", "~/Site/MerchantDashboard.aspx");
            routes.MapPageRoute("MerchantChangePassword", "Site/Merchant/ChangePassword", "~/Site/ChangePasswordMerchant.aspx");
            routes.MapPageRoute("MerchantCampaignNew", "Site/Merchant/Campaign/New", "~/Site/MerchantCampaigns.aspx");
            routes.MapPageRoute("MerchantCampaignOverview", "Site/Merchant/Campaign/Overview", "~/Site/CampaignOverview.aspx");
            routes.MapPageRoute("MerchantCampaignMessage", "Site/Merchant/Campaign/Message", "~/Site/Campaign_Message.aspx");
            routes.MapPageRoute("MerchantCampaignFacebook", "Site/Merchant/Campaign/Facebook", "~/Site/MerchantFacebook.aspx");
            routes.MapPageRoute("MerchantCampaignTwitter", "Site/Merchant/Campaign/Twitter", "~/Site/Merchant_Twitter.aspx");
            routes.MapPageRoute("MerchantCampaignEmail", "Site/Merchant/Campaign/Email", "~/Site/MerchantEmail.aspx");
            routes.MapPageRoute("MerchantCampaignColor", "Site/Merchant/Campaign/Color", "~/Site/MerchantCustomizeColor.aspx");
            routes.MapPageRoute("MerchantLogin", "Site/Merchant/Login", "~/Site/MerchantLogin.aspx");
            //routes.MapPageRoute("MerchantCampaignManagement", "Site/Merchant/Campaign", "~/Site/CampaignManagement.aspx");
            routes.MapPageRoute("MerchantCampaignManagement", "Site/Merchant/CampaignManagement", "~/Site/CampaignManagement.aspx");
            routes.MapPageRoute("MerchantAnalytics", "Site/Merchant/Analytics/{*CampaignID}", "~/Site/Analytics.aspx");
            routes.MapPageRoute("MerchantAddCredits", "Site/Merchant/Credits", "~/Site/AddCredit.aspx");
            routes.MapPageRoute("MerchantRefundResult", "Site/Merchant/RefundResult/{pid}", "~/Site/Refund_Result.aspx");
            routes.MapPageRoute("MerchantRefund", "Site/Merchant/Refund", "~/Site/Refund.aspx");
            routes.MapPageRoute("MerchantPaymentSuccess", "Site/Merchant/PaymentSuccess/{pid}", "~/Site/PaymentSuccess.aspx");
            routes.MapPageRoute("MerchantManageCredits", "Site/Merchant/ManageCredits", "~/Site/ManageCredits.aspx");
            routes.MapPageRoute("MerchantAccountDetails", "Site/Merchant/AccountDetails", "~/Site/Account_Details.aspx");
            routes.MapPageRoute("MerchantAccountDetailsAuto", "Site/Merchant/AutoReplensish", "~/Site/Auto-Replenish.aspx");
            routes.MapPageRoute("MerchantAccountDetailsSubscription", "Site/Merchant/Renew_subscription", "~/Site/Renew_subscription.aspx");
            routes.MapPageRoute("MerchantPosts", "Site/Merchant/Posts", "~/Site/MerchantTotalPosts.aspx");
            routes.MapPageRoute("MerchantTransactionDetails", "Site/Merchant/TransactionDetails", "~/Site/Transaction_Details.aspx");
            routes.MapPageRoute("CustomerDasboard", "Site/Customer/Dashboard", "~/Site/CustomerDashboard.aspx");
            routes.MapPageRoute("CustomerLoginWelcome", "Site/Customer/Login/Welcome", "~/Site/CustomerLoginWelcome.aspx");
            routes.MapPageRoute("PluginShareURL", "Plugin/Share/{Source}/{OfferID}", "~/Plugin/FaceBookLinkClick.aspx");
            routes.MapPageRoute("PluginShareURLtest", "Plugin/Shares/{Source}/{OfferID}", "~/Plugin/FaceBookLinkClicks.aspx");
            routes.MapPageRoute("PluginShareURLtestfacebok", "Plugin/Shared/{Source}/{OfferID}", "~/Plugin/FaceBookLinkClickd.aspx");
            routes.MapPageRoute("FacebookPluginShareURL", "Plugin/FBShare/{OfferID}", "~/Plugin/FacebookShare.aspx");
            routes.MapPageRoute("TwitterPluginShareURL", "Plugin/TwShare/{OfferID}", "~/Plugin/TwitterTweet.aspx");
            routes.Add(new Route("Plugin/{SocialReferralID}/Resources/CSS", new HttpHandlerRouteHandler("~/Resources/style.ashx")));
            routes.Add(new Route("Plugin/{SocialReferralID}/Mail", new HttpHandlerRouteHandler("~/Plugin/Mail.aspx")));
            routes.Add(new Route("Plugin/{SocialReferralID}/{E}/{OID}/{P}/{FN}/{LN}/{W}/{ST}/{TA}/{D}/{T1}/{T2}/{T3}/{S}/{*OD}", new HttpHandlerRouteHandler("~/Plugin/Plugin.ashx")));
            routes.MapPageRoute("MerchantReferral", "Site/Merchant/MerchantReferral", "~/Site/MerchantReferral.aspx");
            routes.MapPageRoute("CustomerReferral", "Site/Customer/CustomerReferral", "~/Site/CustomerReferral.aspx");
            routes.MapPageRoute("CheckSocialReferralIdExist", "Temp/CheckSocailReferralId/{SRID}", "~/Temp/CheckSocialReferralIdExist.aspx");
            routes.MapPageRoute("RedeemStatus", "Site/Customer/RedeemStatus/{S}", "~/Site/RedeemCreditsStatus.aspx");
            routes.MapPageRoute("DeclineTransaction", "Site/Merchant/DeclineTransaction", "~/Site/DeclineTransaction.aspx");
            routes.MapPageRoute("CustomerTransactionHistory", "Site/Customer/CustomerTransactionHistory", "~/Site/CustomerTransaction_History.aspx");
            routes.MapPageRoute("CustomerLoginCustomerLoginRedirectRedirect", "Site/Customer/LoginRedirect", "~/Site/CustomerLoginRedirect.aspx");
            routes.MapPageRoute("MerchantCardDetails", "Site/Merchant/CardDetails", "~/Site/Credit_Card_Listing.aspx");
            routes.MapPageRoute("MerchantModifyDetails", "Site/Merchant/ModifyDetails", "~/Site/Modify_Auto_Replenish.aspx");
            routes.MapPageRoute("MerchantCreditcard", "Site/Merchant/AutoCardDetails", "~/Site/Auto_Credit_card.aspx");
            routes.MapPageRoute("MerchantSubscription", "Site/Merchant/Subscription", "~/Site/Subscription.aspx");
            routes.MapPageRoute("MerchantAutoSubscription", "Site/Merchant/AutoSubscription", "~/Site/AutoSubscription.aspx");
            routes.MapPageRoute("Offer", "Plugin/Offer/{OfferID}", "~/Plugin/Offer.aspx");
            routes.MapPageRoute("Sitedetails", "Site/Merchant/SiteName", "~/Site/SiteDetails.aspx");

            routes.MapPageRoute("MerchantLoginRedirect", "Site/Merchnat/LoginRedirect", "~/Site/MerchantLoginRedirct.aspx");
 
            routes.MapPageRoute("PluginShareURL121test", "Plugintest/TestCoupoun/{socialdiv}", "~/Site/TestLanding.aspx");
            routes.MapPageRoute("CookieEnable", "site/CookieEnable", "~/Site/CookieEnable.aspx");

            routes.MapPageRoute("Contactus", "Contactus", "~/Site/Contactus.aspx");

        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string OldURL = Request.Url.ToString().ToLower();
           if (OldURL.Contains("site/index"))
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"]+"home");
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}