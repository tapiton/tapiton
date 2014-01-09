using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


public partial class Site_PagingTotalPost : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["PageName"].ToString() == "Post")
        {
            Session["PagingTotalPost"] = Request.QueryString["PageID"].ToString();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/Posts");
        }
        if (Request.QueryString["PageName"].ToString() == "ManageCredit")
        {
            Session["PagingCredits"] = Request.QueryString["PageID"].ToString();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Merchant/ManageCredits");
        }

    }
}
