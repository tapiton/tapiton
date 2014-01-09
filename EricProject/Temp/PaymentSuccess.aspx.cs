using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObject;
using System.Data;
namespace EricProject.Temp
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        string ProductID, ProductName, Price,EmailID,FirstName,LastName;
        public DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
             dt = Session["mySession"] as DataTable;
             foreach (DataRow dr in dt.Rows)
             {

             }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProductID = dt.Rows[0]["ProductID"].ToString();
                ProductName = dt.Rows[0]["ProductName"].ToString();
                Price = dt.Rows[0]["Price"].ToString();
            }
            EmailID = comman.getData(Request.QueryString["EmailID"], "");
            FirstName = comman.getData(Request.QueryString["FirstName"], "");
            LastName = comman.getData(Request.QueryString["LastName"].ToString(), "");
           // Response.Redirect("http://socialreferral.onlineshoppingpool.com/Plugin/D3H7B1H7/" + EmailID + "/AB-1107/volusion/" + FirstName + "/" + LastName + "/v1295262.rqg9sectj467.demo14.volusion.com/$" + Price + "/$"+Price+"/$0.00/$0.00/$0.00/$0.00/$0.00/1004^26.00^1^Coffee%20Mug");

        }
    }
}