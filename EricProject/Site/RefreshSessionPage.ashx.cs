using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for RefreshSessionPage
    /// </summary>
    public class RefreshSessionPage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string MerchantEmailId = HttpContext.Current.Session["MerchantEmailId"].ToString()+"";
            string CustomerEmailId = HttpContext.Current.Session["CustomerEmailId"].ToString()+"";
            context.Response.Write("1");
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