using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace EricProject.Master
{
    public partial class SiteSignUpMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hiddenPageURL.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
        }
    }
}