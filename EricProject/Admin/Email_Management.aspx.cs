using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Admin_Email_Management : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Literal litDashboard = new Literal();
            litDashboard = (Literal)Page.Master.FindControl("litDashboard");
            litDashboard.Text = "<li id='lilitDashboard' style=\"display:none\"  class=\"dash\"><a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Admin/Default.aspx\"><span>Dashboard</span></a></li>";

            Literal litCustomerManagement = new Literal();
            litCustomerManagement = (Literal)Page.Master.FindControl("litCustomerManagement");
            litCustomerManagement.Text = "<li id='lilitCustomerManagement' style=\"display:none\" class=\"forms\"><a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Admin/CustomerManagement.aspx\"><span>Customer Management</span></a></li>";

            Literal litManageMerchantAccounts = new Literal();
            litManageMerchantAccounts = (Literal)Page.Master.FindControl("litManageMerchantAccounts");
            litManageMerchantAccounts.Text = "<li id='liManagemerchantaccounts' style=\"display:none\" class=\"ui\"><a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Admin/ManageMerchantAccount.aspx\"  ><span>Manage Merchant Account</span></a></li>";

            Literal litFAQManagement = new Literal();
            litFAQManagement = (Literal)Page.Master.FindControl("litFAQManagement");
            litFAQManagement.Text = "<li id='lilitFAQManagement' style=\"display:none\" class=\"typo\"><a href=\"" + ConfigurationManager.AppSettings["pageURL"] + "Admin/FAQ_Management.aspx\"><span>FAQ Management</span></a></li>";
        }
    }
}
