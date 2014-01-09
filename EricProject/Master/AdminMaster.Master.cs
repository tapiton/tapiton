using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL;

public partial class Master_AdminMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["isValid"] == null)
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Admin/Login.aspx");
        CheckUserPage();
        
    }
    protected void CheckUserPage()
    {
        string StrPageanme = "/AddNewSubAdmin.aspx";
        string StrStaticData = "/ManageStaticData.aspx";
        string StrFaq = "/FAQ_Management.aspx";
        string strprofile = "/ManageProfile.aspx";
        string PATH = Request.Url.AbsolutePath.Substring(Request.Url.AbsolutePath.LastIndexOf("/"));
        if (Convert.ToInt32(Session["UserID"]) == 3)
        {
            if (PATH == StrPageanme || PATH == StrStaticData || PATH == StrFaq || PATH == strprofile)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Admin/default.aspx");
            }
        }
        else if (Convert.ToInt32(Session["UserID"]) == 2)
        {
            if (PATH == StrPageanme)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Admin/default.aspx");
            }
        }
    }
}
