using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for ValidateLoggedInUser
    /// </summary>
    public class ValidateLoggedInUser : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //Customer
            string customeremail = context.Request.QueryString["customeremail"].ToString();
            if (HttpContext.Current.Session["CustomerEmailId"] != null && HttpContext.Current.Session["CustomerEmailId"].ToString() == customeremail)
            {
                HttpContext.Current.Session["CustomerEmailIdNew"] = customeremail;
                context.Response.Write("1");
            }
            else if (HttpContext.Current.Session["CustomerEmailId"] != null && HttpContext.Current.Session["CustomerEmailId"].ToString() != customeremail)
            {
                HttpContext.Current.Session["CustomerEmailIdNew"] = customeremail;
                context.Response.Write("2");
            }
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