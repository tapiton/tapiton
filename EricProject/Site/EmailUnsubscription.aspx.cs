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
using System.IO;
using Encryption64;
namespace EricProject.Site
{
    public partial class EmailUnsubscription : System.Web.UI.Page
    {
        string EmailId = string.Empty;
        string Password = string.Empty;
        string PublicKey = "";
        public EmailUnsubscription()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["e"] != null)
                {
                    string Email = new EncryptDecrypt().Decrypt(Request.QueryString["e"].ToString(), PublicKey);
                    headingMessage.Text = "Email Subscriptions for " + Email;
                    txtEmail.Text = Email;
                }
            }
        }

        protected void lnkbtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DAL.Admin obj = new DAL.Admin();
                obj.InsertIntoBlackList(txtEmail.Text);
                lblResult.Text = "You have successfully unsubscribed and will no longer receive messages from " + ConfigurationManager.AppSettings["site_name"].ToString() + ".";
            }
            catch
            {
                lblResult.Text = "Oops! Something went wrong. Please try again later after sometime.";
            }
        }
    }
}