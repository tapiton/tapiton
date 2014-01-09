using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using System.Configuration;
namespace EricProject.Plugin
{
    public partial class IntegrationTesting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string Message = comman.getData(Request.QueryString["Msg"], "");
            int Error = comman.getData(Request.QueryString["Msg2"], 0);
            msg.Text ="Integration "+ Message;
            if (Error == 0)
                stepsDiv.InnerHtml = "<ul><li>Please check your " + ConfigurationManager.AppSettings["site_name"].ToString() + " account details for website and ecommerce platform.</li></ul>";
            else if (Error == 1)
                stepsDiv.InnerHtml = "<ul><li>Login to Referral Web Site.</li><li><a href='javascript:OpenCampaign()' target='_blank'>Create campaign.</a></li><li><a href='javascript:OpenCredit()' target='_blank'>Purchase Credits.</a></li><li>Start getting referrals!</li></ul>";
            else if (Error == 2)
                stepsDiv.InnerHtml = "<ul><li>Login to Referral Web Site.</li><li><a href='javascript:OpenCampaign()' target='_blank'>Create campaign.</a></li><li>Start getting referrals!</li></ul>";
            else if (Error == 3)
                stepsDiv.InnerHtml = "<ul><li>Login to Referral Web Site.</li><li><a href='javascript:OpenCredit()' target='_blank'>Purchase Credits.</a></li><li>Start getting referrals!</li></ul>";
            else
                stepsDiv.InnerHtml = "<ul><li>Your campaign starts now.</li><li>Please <a href='javascript:openLogin()'>click here</a> to login and manage and optimize your campaign.</li></ul>";
        }
    }
}