using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;


public partial class Site_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string check = Request.QueryString["str"].ToString();
        Response.Cookies["CustomerEmail"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["CustomerPassword"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["CustomerID"].Expires = DateTime.Now.AddDays(-1);

        Response.Cookies["MerchantEmail"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["MerchantPassword"].Expires = DateTime.Now.AddDays(-1);
        Response.Cookies["MerchantId"].Expires = DateTime.Now.AddDays(-1);
        if (check == "2")
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/Login");
        }
        else
        {
            Session.RemoveAll();
            Session.Abandon();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Home");
        }
    }
}
