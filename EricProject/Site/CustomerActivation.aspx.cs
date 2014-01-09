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

namespace EricProject.Site
{
    public partial class CustomerActivation : System.Web.UI.Page
    {
        string EmailId = string.Empty;
        string Password = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["u"] != "")
            {
                EmailId = Request.QueryString["u"].ToString().Split('^')[0];
                // Password = Request.QueryString["pwd"].ToString();
                string Active = Request.QueryString["u"].ToString().Split('^')[1];
                _plugin  objCustomer = new _plugin();
                objCustomer.EmailID = EmailId;
                DAL.Plugin sqlPlugin = new DAL.Plugin();
                SqlDataReader dr = sqlPlugin.CustomerLogin(objCustomer);
                //string email = EmailId;
                //string checkmail = dr["IsActive"].ToString();
                if (dr.Read())
                {
                    Session["CustomerID"] = dr["Customer_ID"].ToString();
                    Session["CustomerEmailId"] = dr["Email_Id"].ToString();
                    objCustomer.Status = Active;
                    int result = sqlPlugin.updateCustomerStatus(objCustomer);  
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Your account has been activated');window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Dashboard" + "';", true);
                }            
                DBAccess.InstanceCreation().disconnect();
            }
        }
    }
}