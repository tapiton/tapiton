using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
namespace EricProject.Site
{
    /// <summary>
    /// Summary description for ValidateLoggedInUser1
    /// </summary>
    public class ValidateLoggedInUser1 : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //Customer
            context.Response.ContentType = "text/plain";
            string customeremail = context.Request.QueryString["customeremail"].ToString();

            if (HttpContext.Current.Session["CustomerEmailId"] != null &&
                HttpContext.Current.Session["CustomerEmailId"].ToString() == customeremail)
            {
                HttpContext.Current.Session["CustomerEmailIdNew"] = customeremail;
                context.Response.Write("1");
            }

            if (HttpContext.Current.Session["CustomerEmailId"] != null &&
                 HttpContext.Current.Session["CustomerEmailId"].ToString() != customeremail)
            {
                HttpContext.Current.Session["CustomerEmailIdNew"] = customeremail;
                context.Response.Write("2");
            }
            else
            {
                HttpContext.Current.Session["CustomerEmailIdNew"] = customeremail;
                context.Response.Write("3");
            }
            HttpContext.Current.Session["BottomLinkVisibleForCustomer"] = "Yes";
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