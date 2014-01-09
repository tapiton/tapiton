using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class CustomerLoginRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            if (!IsPostBack)
            {
                litMsg.Text = "";
                if (Session["Isblank"] != null && Session["Isblank"].ToString() == "Fill")
                {
                    litMsg.Text += " You are already logged in with the email account : <span style=\"color:#7ebb01;font-weight:bold;\">" + Session["CustomerEmailId"].ToString() + "</span>. You will be redirected to your dashboard for this account in a moment.";
                    litMsg.Text += "<br /><br />";
                    litMsg.Text += "If you are not automatically redirected, please <a href=\"javascript:ReditectToDashboard();\" style=\"text-decoration: none; color: red;\">click here</a>";
                    Session["CustomerEmailIdNew"] = Session["CustomerEmailId"];
                    //Session["CustomerEmailIdNew"] = null;
                }
            }
        }
    }
}