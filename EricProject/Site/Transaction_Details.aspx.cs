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
    public partial class Transaction_Details : System.Web.UI.Page
    {
        DAL.Transaction sqlTransaction = new DAL.Transaction();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Session["Transaction_Reference_Id"]+"abc");
            // Response.End();
            _Transaction objTransaction = new _Transaction();
            objTransaction.TransactionId = Convert.ToInt32(Session["Transaction_Reference_Id"]);
            SqlDataReader dr = sqlTransaction.Transaction_Details_Total(objTransaction);
            SqlDataReader drReferral = sqlTransaction.Referral_details_Merchant(objTransaction);
           
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
            ReferralDetails.Text = "";
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
                        litTransactionHistory.Text += "	<td><strong>" + dollar + (Convert.ToDecimal(dr["Price"]) * Convert.ToDecimal(dr["Quantity"])).ToString() + "</strong></td>";
                    }
                    // /////  litTransactionHistory.Text += "	<td class='last'><strong>" + comman.FormatCredits ( drReferral["CUSTOMER_CREDITS"]) +Credits+ "</storng></td>";
                    litTransactionHistory.Text += "	</tr>";
                    if (dr["Subtotal"].ToString().Contains('$'))
                    {
                        OrderSubtotal.Text = "<td class='last'><strong>"+dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Subtotal"].ToString().Replace("$","")))) + "</strong></td>";
                    }
                    else
                    {
                        OrderSubtotal.Text = "<td class='last'><strong>" + dollar + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(dr["Subtotal"].ToString()))) + "</strong></td>";
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
               
                EncryptDecrypt ED = new EncryptDecrypt();
                string Encrypted = ED.Encrypt(drReferral["Campaign_ID"].ToString(), "S@!U7AH$1$");
                ReferralDetails.Text += "	<tr>";
                ReferralDetails.Text += "	<td class='first'><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'>" + drReferral["Campaign_Name"].ToString() + "</a></td>";
                ReferralDetails.Text += "   <td>Referrer Reward</td>";
                ReferralDetails.Text += "	<td><a href='#'>" + drReferral["Referral"].ToString() + "</a></td>";
                if (EligibleAmount == "")
                    EligibleAmount = "0";
                if (drReferral["Referral"].ToString() == drReferral["Customer"].ToString())
                    ReferralDetails.Text += "	<td>$0.00 </td>";
                else
                    ReferralDetails.Text += "	<td>$" +  Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(EligibleAmount))) + "</td>";
                
                if (drReferral["Referrer_reward_type"].ToString() == "1")
                    ReferralDetails.Text += "	<td>$" + drReferral["Referrer_reward"].ToString() + "</td>";
                else
                    ReferralDetails.Text += "	<td>" + drReferral["Referrer_reward"].ToString() + "%</td>";
                
                ReferralDetails.Text += "	<td class='last'>" + comman.FormatCredits(drReferral["REFERRER_CREDITS"]) + Credits + "</td>";
                ReferralDetails.Text += "	</tr>";
             
                customerdetails.Text += "	<tr>";
                customerdetails.Text += "	<td class='first'><a href='" + ConfigurationManager.AppSettings["pageURL"] + "Site/Merchant/Campaign/Overview?CampaignId=" + Server.UrlEncode(Encrypted) + "'>" + drReferral["Campaign_Name"].ToString() + "</a></td>";
               
                
                customerdetails.Text += "   <td>Customer Rebate</td>";
                customerdetails.Text += "	<td><a href='#'>" + drReferral["Customer"].ToString() + "</a></td>";
              
                if (EligibleAmount.Contains('$'))
                {
                    customerdetails.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(EligibleAmount.Replace("$","")))) + "</td>";
                }
                else
                {
                    customerdetails.Text += "	<td>$" + Convert.ToDecimal(string.Format("{0:0.00}", Convert.ToDecimal(EligibleAmount))) + "</td>";
                }
              
                if (drReferral["Customer_reward_type"].ToString() == "1")
                    customerdetails.Text += "	<td>$" + drReferral["Customer_reward"].ToString() + "</td>";
                else
                    customerdetails.Text += "	<td>" + drReferral["Customer_reward"].ToString() + "%</td>";
                customerdetails.Text += "	<td class='last'>" + comman.FormatCredits(drReferral["CUSTOMER_CREDITS"]) + Credits + "</td>";
                customerdetails.Text += "	</tr>";
              
                TotalCredits.Text = comman.FormatCredits(Convert.ToInt32(drReferral["CUSTOMER_CREDITS"]) + Convert.ToInt32(drReferral["REFERRER_CREDITS"]) + Convert.ToInt32(drReferral["transaction_fee"])) + Credits;                
                if (drReferral["Customer_Credit_Status"].ToString().Contains("Successful"))
                {
                    Status.Text = "	<span class=\"grnrew\">Successful</span>";
                    HiddenTransactionStatus.Value = "";
                }
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Pending Vesting"))
                {
                    Status.Text = "<span class=\"orng\">Pending Vesting</span>";
                    HiddenTransactionStatus.Value = "Pending Vesting";
                }
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Pending Payment"))
                {
                    Status.Text = "<span style=\"Color:Red;\">Pending Payment</span>";
                    HiddenTransactionStatus.Value = "Pending Payment";
                }
                else if (drReferral["Customer_Credit_Status"].ToString().Contains("Declined"))
                {
                    Status.Text = "<span style=\"Color:Red;\">Declined</span>";
                    HiddenTransactionStatus.Value = "";
                }                
                TransactionFee.Text = comman.FormatCredits(drReferral["transaction_fee"].ToString()) + " Credits";
                transaction_Date.Text = drReferral["Transaction_date"].ToString();
                //if(drReferral["Customer_Credit_Status"].ToString()=="Approved")
                //Status.Text = "SuccessFull";
                //else
                //Status.Text = "Pending";             
            }
        }

        public void BindTransactionDeclinedReason()
        {
            _Declined_Credits objDeclineCredits = new _Declined_Credits();
            objDeclineCredits.Transaction_Id = Convert.ToInt32(Session["Transaction_Reference_Id"]);
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
