using BAL;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EricProject.Site
{
    public partial class Reddem_Details : System.Web.UI.Page
    {
        DAL.Transaction sqlTransaction = new DAL.Transaction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Customer_Reference_Id_Redeem"] != null)
            {
                _Credit_Transaction objcredit = new _Credit_Transaction();
                objcredit.Transaction_id = Convert.ToInt32(Session["Customer_Reference_Id_Redeem"]);
                SqlDataReader Dr = sqlTransaction.GetRedeemDetails(objcredit);
                if (Dr.Read())
                {
                    litTransactionId.Text =   Dr["Paypal_Transaction_ID"].ToString();
                    litAmount.Text = Dr["Payment_Amount"] + " USD";
                    litDate.Text = Convert.ToDateTime(Dr["Added_On"]).ToString("d MMM yyyy");
                    litcard.Text = Dr["Total_redeemed_Credits"].ToString();
                    litComments.Text = "Paypal";
                    if (Dr["Paypal_First_Name"].ToString() != "")
                    {
                        notediv.Visible = true;
                        litNote.Text = " Payment sent to paypal user " + Dr["Paypal_First_Name"].ToString() + "";
                    }
                }
            }
        }
    }
}