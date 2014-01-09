using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using System.Configuration;
using System.Data;
namespace EricProject.Temp
{
    public partial class CustomerPurchase : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void btncheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Temp/PaymentSuccess.aspx?EmailID=" + Emailid.Value + "&FirstName=" + firstname.Value + "&LastName=" + lastname.Value + "");
        }
    }
}