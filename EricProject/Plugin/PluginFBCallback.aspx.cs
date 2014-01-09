using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Facebook.Rest;
using Facebook.Session;
using Facebook.Schema;
using Facebook.Web.FbmlControls;
using Facebook.Utility;
using Facebook.BindingHelper;
using BAL;
using DAL;
using BusinessObject;


public partial class Plugin_PluginFBCallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["code"] != null)
        {

            string code = Request.QueryString["code"].ToString();

            oAuthFacebookPlugin fbAC = new oAuthFacebookPlugin(); //Standard FB class file available on net in c#
            string respnse = "";
            try
            {
                fbAC.AccessTokenGet(code);
                respnse = fbAC.Token;
            }
            catch (Exception ex)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/PluginFBSiteLogin.aspx?error=" + ex.Message);
            }

            if (Session["RedirectURL"] != null && Session["RedirectURL"].ToString() != "")
            {
                Response.Redirect(Session["RedirectURL"].ToString() + "?token=" + respnse + "&source=FB");
            }
            else
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/PluginFBSiteLogin.aspx?token=" + respnse);
            }

        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/PluginFBSiteLogin.aspx?error=code not found" +
                              Request.QueryString["error_reason"].ToString());
        }
    }
}
