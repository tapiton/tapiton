using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class SMTP_ErrorHandling : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SubscriptionFailure"] != null)
            {
                if (Session["SubscriptionFailure"].ToString() == "SubscriptionFailure")
                    lblmessage.Text = "Your subscription payment has been made but we were unable to send you an email receipt. We apologize for the inconvenience.";
            }
            else
                lblmessage.Text = "We were unable to process your request.We apologize for the inconvenience. Please pardon this interruption as we work to enhance the site to improve your experience.";
        }
    }
}