using BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
namespace EricProject.Site
{
    public partial class RedeemCreditsStatus : System.Web.UI.Page
    {
        public string status="";
        public string transactionid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            var routeValues = Request.RequestContext.RouteData.Values;
            string queryvalues = comman.getData(routeValues["S"], "");
            status = queryvalues.Split('^')[0].ToString();
            transactionid = queryvalues.Split('^')[1].ToString();

        }
    }
}