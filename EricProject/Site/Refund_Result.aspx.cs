using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class Refund_Result : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] split=Page.RouteData.Values["pid"].ToString().Split('^');
            string result=split[0].ToString();
                string Amount=split[1].ToString();
                string CArdnumber = split[2].ToString();
            if (result == "True")
            {
                Message.InnerText = "$" + Amount + " has been refunded to your credit card ending in '" + CArdnumber.Substring(12) + "'. ";
            }
            else
            {
                Message.InnerText = "We attempted to refund the money to the credit card that was charged for your last purchase of Credits. But it has failed. A member of our support team has been contacted and will be in contact with you shortly.";
            }
        }
    }
}