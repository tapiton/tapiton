using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using System.Data.SqlClient;
using BusinessObject;
using System.Configuration;
using Encryption64;

namespace EricProject.Site
{
    public partial class CustomerTransaction_History : System.Web.UI.Page
    {
        DAL.Transaction sqlTransaction = new DAL.Transaction();
        protected void Page_Load(object sender, EventArgs e)
        {       //Response.Write(Session["Transaction_Reference_Id"]+"abc");
            // Response.End();
            _Transaction objTransaction = new _Transaction();
            DAL.Transaction sqlTransaction = new DAL.Transaction();
            objTransaction.TransactionId = Convert.ToInt32(Session["Customer_Reference_Id"]);
            SqlDataReader dr = sqlTransaction.Transaction_Details_Total(objTransaction);

            objTransaction.TransactionId = Convert.ToInt32(Session["Customer_Reference_Id"]);
            objTransaction.CustomerId = Convert.ToInt32(Session["CustomerId"]);
            SqlDataReader drReferral = sqlTransaction.Bind_CustomerTransactionDetails(objTransaction);

            //Bind transaction declined reason
            BindTransactionDeclinedReason();
            //Response.Write(Convert.ToInt32(Session["Transaction_Reference_Id"]));
            //Response.End();
            int i = 0;
            if (!dr.HasRows)
            {
                DivNoData.Visible = false;
            }
            litTransactionHistory.Text = "";
            OrderSubtotal.Text = "";
            Tax.Text = "";
            string dollar = "$";
            string EligibleAmount = "";
            //string quantity = "Pc";
            string Credits = " Credits";
            if (drReferral.Read())
            {
                while (dr.Read())
                {

                    DivNoData.Visible = true;
                    i++;
                    if (drReferral["SKU_ID"].ToString() == "0")
                        EligibleAmount = dr["Subtotal"].ToString();
                    else
                    {
                        if (drReferral["SKU_ID"].ToString() == dr["SKU_ID"].ToString())
                        {
                            EligibleAmount = (Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(dr["Quantity"])).ToString();
                        }
                    }
                    litTransactionHistory.Text += "	<tr>";
                    litTransactionHistory.Text += "	<td class='first'>" + dr["Product_Name"].ToString() + "</span></td>";
                    litTransactionHistory.Text += "	<td>" + dr["Quantity"].ToString() + "</td>";
                    litTransactionHistory.Text += "	<td>" + dr["SKU_ID"].ToString() + "</td>";
                    if (dr["Price"].ToString().Contains('$'))
                    {
                        litTransactionHistory.Text += "	<td><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Price"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Price"].ToString()))) + "</strong></td>";
                    }
                    if (dr["Price"].ToString().Contains('$'))
                    {
                        litTransactionHistory.Text += "	<td><strong>" + dollar + (Convert.ToDecimal(dr["Price"].ToString().Replace("$", "")) * Convert.ToDecimal(dr["Quantity"])).ToString() + "</strong></td>";
                    }
                    else
                    {
                        litTransactionHistory.Text += "	<td><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", (Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(dr["Quantity"])).ToString())) + "</strong></td>";
                    }
                    //   litTransactionHistory.Text += "	<td class='last'><strong>" + comman.FormatCredits ( drReferral["CUSTOMER_CREDITS"]) +Credits+ "</storng></td>";
                    litTransactionHistory.Text += "	</tr>";
                    if (dr["Subtotal"].ToString().Contains('$'))
                    {
                        OrderSubtotal.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Subtotal"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        OrderSubtotal.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Subtotal"].ToString()))) + "</strong></td>";
                    }
                    if (dr["Shipping"].ToString().Contains('$'))
                    {
                        ShippingCharege.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Shipping"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        OrderSubtotal.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Subtotal"].ToString().Trim()))) + "</strong></td>";
                    }
                    if (dr["Tax"].ToString().Contains('$'))
                    {
                        Tax.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Tax"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        Tax.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Tax"].ToString()))) + "</strong></td>";
                    }
                    if (dr["Shipping"].ToString().Contains('$'))
                    {
                        ShippingCharege.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Shipping"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        ShippingCharege.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Shipping"].ToString()))) + "</strong></td>";
                    }
                    if (dr["TotalAmount"].ToString().Contains('$'))
                    {
                        Total.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["TotalAmount"].ToString().Replace("$", "")))) + "</strong></td>";
                    }
                    else
                    {
                        Total.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["TotalAmount"].ToString().Trim()))) + "</strong></td>";
                    }
                }


                //ReferralDetails.Text += "	<tr>";
                //ReferralDetails.Text += "	<td class='first'>" + drReferral["Campaign_Name"].ToString() + "</span></td>";
                //ReferralDetails.Text += "	<td>" + drReferral["Customer"].ToString() + "</td>";
                //ReferralDetails.Text += "	<td>" + Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]) + Credits + "</td>";
                //ReferralDetails.Text += "	<td><strong>" + drReferral["Referral"].ToString() + "</strong></td>";
                //ReferralDetails.Text += "	<td class='last'><strong>" + Convert.ToInt32(drReferral["REFERRER_CREDITS"]) + Credits + "</storng></td>";
                //ReferralDetails.Text += "	</tr>";
                EncryptDecrypt ED = new EncryptDecrypt();
                string Encrypted = ED.Encrypt(drReferral["Campaign_ID"].ToString(), "S@!U7AH$1$");

                customerdetails.Text += "	<tr>";
                //customerdetails.Text += "	<td class='first'><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'>" + drReferral["Campaign_Name"].ToString() + "</a></td>";
                customerdetails.Text += "   <td>" + drReferral["Type"].ToString() + "</td>";
                customerdetails.Text += "	<td>" + drReferral["Merchant_Company_Name"].ToString() + "</td>";
                customerdetails.Text += "	<td>" + drReferral["Item_Name"].ToString() + "</td>";
                if (EligibleAmount.Contains("$"))
                {
                    customerdetails.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(string.Format("{0:0.00}", EligibleAmount.ToString().Replace("$", ""))))) + "</td>";
                }
                else
                {
                    customerdetails.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(EligibleAmount))) + "</td>";
                }
                customerdetails.Text += "	<td>" + drReferral["Reward"].ToString() + "</td>";
                customerdetails.Text += "	<td class='last'>" + comman.FormatCredits(drReferral["CUSTOMER_CREDITS"]) + Credits + "</td>";
                customerdetails.Text += "	</tr>";

                if (drReferral["Customer_Credit_Status"].ToString().Contains("Successful"))
                    Status.Text = "	<span class=\"grnrew\">Paid</span>";
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Pending Payment"))
                    Status.Text = "<span style=\"color:red;\">Not Paid</span>";
                //else if (drReferral["Customer_Credit_Status"].ToString().Contains("Unvested"))
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Unvested"))
                {
                    Status.Text = "<span class=\"orng\">To Be Paid <span style=\"font-size: 11px;color:#000000;\">(" + drReferral["Vesting_Date"].ToString() + ")</span></span>";
                }
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Decline"))
                    Status.Text = "<span style=\"Color:Red;\">Declined</span>";
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Redeemed"))
                    Status.Text = "<span class=\"grnrew\">Paid</span>";

                transaction_Date.Text = drReferral["Transaction_date"].ToString();
            }

        }
        public void BindTransactionDeclinedReason()
        {
            _Declined_Credits objDeclineCredits = new _Declined_Credits();
            objDeclineCredits.Transaction_Id = Convert.ToInt32(Session["Customer_Reference_Id"]);
            SqlDataReader drDeclineCredit = sqlTransaction.BindTransactionDelinedReasonByTransactionId(objDeclineCredits);
            if (drDeclineCredit.Read())
            {
                litDeclinedReason.Text = drDeclineCredit["Reason"].ToString();
                DivDeclinedReason.Visible = true;
            }
            else
            {
                DivDeclinedReason.Visible = false;
            }
            DBAccess.InstanceCreation().disconnect();
        }
    }
}