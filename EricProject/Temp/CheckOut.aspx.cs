using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using BusinessObject;
using System.Configuration;
using System.Data;
namespace EricProject.Temp
{
    public partial class CheckOut : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
             dt = Session["mySession"] as DataTable;
          
            ProductDetails.Text = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                TextBox txt = new TextBox();
                txt.ID = "textBox"+i;
                
                ProductDetails.Text += "<table>	<tr>";
                ProductDetails.Text += "	<td >" +"Product Name" + dt.Rows[i]["ProductName"].ToString() + "</span></td></tr>";
                ProductDetails.Text += "	<tr> <td>" + "Product ID" + dt.Rows[i]["ProductID"].ToString() + "</td> </tr>";
                ProductDetails.Text += "	 <tr> <td><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/" + dt.Rows[i]["img"].ToString() + " ></img></td> </tr>";
                ProductDetails.Text += "	<tr><td><strong>" + "Your Price " + dt.Rows[i]["Price"].ToString() + "</strong></td></tr></table>";
                ProductDetails.Text += "	 <span >Quantity</span>"; 
                ProductDetails.Text += "	<hr/>";
                //  spanProductName.Text = dt.Rows[0]["ProductName"].ToString();
                // spanProductID.Text = dt.Rows[0]["ProductID"].ToString();
                //imgProductImage.Src = ConfigurationManager.AppSettings["pageURL"] + "images/" + dt.Rows[0]["img"].ToString();
                //spanProductPrice.Text = dt.Rows[0]["Price"].ToString();
                
            }

        }

        protected void btncheckout_Click(object sender, EventArgs e)
        {
           // MyColumn = new DataColumn("Quantity");
           // dt.Columns.Add(MyColumn);
            //DataColumn dc = new DataColumn();
           // dc = "2";
            //dt.Columns.Add(dc);
           // dt.AcceptChanges();
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Temp/CustomerPurchase.aspx");
        }
    }
}