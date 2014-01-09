using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;


public partial class Site_PrivacyPolicy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        _Manage_Text obj = new _Manage_Text();
        obj.Page_Id = 4;
        DAL.Plugin sqlobj = new DAL.Plugin();
        SqlDataReader dr = sqlobj.ManageText(obj);
        if (dr.Read())
        {
            lblPageName.Text = dr["Page_Name"].ToString();
            lblTitle.Text = dr["Title"].ToString();
        }
        DBAccess.InstanceCreation().disconnect();
    }
}
