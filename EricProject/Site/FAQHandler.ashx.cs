using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BAL;

namespace EricProject.Site
{
    /// <summary>
    /// Summary description for FAQHandler
    /// </summary>
    public class FAQHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["Search"] != "")
            {
                string search = context.Request.QueryString["Search"];
               
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