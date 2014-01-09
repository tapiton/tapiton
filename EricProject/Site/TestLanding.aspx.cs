using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

namespace EricProject.Site
{
    public partial class TestLanding : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["socialdiv"] != null)
            {
                ltl_script.Text = Page.RouteData.Values["socialdiv"].ToString().Replace("[", "/");
            }
        }
    }
}