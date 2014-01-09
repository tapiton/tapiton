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
using System.IO;
using System.Web.Services;
using Encryption64;

namespace EricProject.Site
{
    public partial class Contactus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSendDetails_Click(object sender, EventArgs e)
        {
            string StrUrl = ConfigurationManager.AppSettings["pageURL"] + "Site/Home/" + ID;
            string StrMsg = "";
            StrMsg += "Name: " + txtName.Value + "<br />";
            StrMsg += "Email: " + txtEmail.Value + "<br />";
            StrMsg += "Message: " + txtMessage.Value + "<br />";
            comman.SendMail("info@tapiton.com", "Support Email - " + txtName.Value, StrMsg);
            SpanSuccess.Visible = true;
            txtEmail.Value = "";
            txtName.Value = "";
            txtMessage.Value = "";
        }
    }

}