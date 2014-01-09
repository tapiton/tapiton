using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for PopupToDeactiveAccount
    /// </summary>
    public class PopupToDeactiveAccount : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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