using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using System.Web.UI.HtmlControls;
using BusinessObject;
using System.Configuration;
using System.IO;
using System.Web.Services;
namespace EricProject.Site
{
    public partial class Testpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void aa(object sender, EventArgs e)
        {
            Response.Write("Tanu");
        }
       
    }

}