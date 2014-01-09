using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for ValidateLoggedInMerchant
    /// </summary>
    public class ValidateLoggedInMerchant : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string customeremail = context.Request.QueryString["Merchantemail"].ToString();

            if (HttpContext.Current.Session["MerchantEmailId"] != null &&
                HttpContext.Current.Session["MerchantEmailId"].ToString() == customeremail)
            {
                HttpContext.Current.Session["MerchantEmailIdNew"] = customeremail;
                context.Response.Write("1");
            }

            if (HttpContext.Current.Session["MerchantEmailId"] != null &&
                 HttpContext.Current.Session["MerchantEmailId"].ToString() != customeremail)
            {
                HttpContext.Current.Session["MerchantEmailIdNew"] = customeremail;
                context.Response.Write("2");
            }
            else
            {
                HttpContext.Current.Session["MerchantEmailIdNew"] = customeremail;
                context.Response.Write("3");
            }
            HttpContext.Current.Session["BottomLinkVisibleForMerchant"] = "Yes";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}