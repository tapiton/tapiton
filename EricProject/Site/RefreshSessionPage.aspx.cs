using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class RefreshSessionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string MerchantEmailId = "";
            string CustomerEmailId = "";
            if (HttpContext.Current.Session["MerchantEmailId"] != null)
                MerchantEmailId = HttpContext.Current.Session["MerchantEmailId"].ToString();
            if (HttpContext.Current.Session["CustomerEmailId"] != null)
                CustomerEmailId = HttpContext.Current.Session["CustomerEmailId"].ToString();
        }
    }
}