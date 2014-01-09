using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BAL;
using System.Collections;
using System.IO;
using BusinessObject;
using System.Net;
using System.Xml;
using System.Globalization;
namespace EricProject.Site
{
    public partial class CustomerCreditDetails : System.Web.UI.Page
    {
        string CustomerId;
        public int UnredeemedCredits = 0;
        public string CustomerEmail = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            hfPageUrl.Value = ConfigurationManager.AppSettings["pageURL"].ToString();
            if (!IsPostBack)
            {
                CustomerId = Session["CustomerID"].ToString();
                CustomerEmail = Session["CustomerEmailId"].ToString();
                _Customer_Credits obj = new _Customer_Credits();
                obj.Customer_Id = Convert.ToInt32(CustomerId);
                DAL.Plugin sqlobj = new DAL.Plugin();
                SqlDataReader dr = sqlobj.BindCustomerCredits(obj);
                if (dr.Read())
                    UnredeemedCredits = comman.getData(dr["UnredeemedCredit"], 0);
                else
                    UnredeemedCredits = 0;
                //txtPaypalUsername.Text = CustomerEmail;

                _plugin objCustomer = new _plugin();
                objCustomer.Customer_ID = Convert.ToInt32(CustomerId);
                SqlDataReader drCustomerDetails = sqlobj.BindCustomerById(objCustomer);
                //if (drCustomerDetails.Read())
                //{
                //    txtFirstName.Text = drCustomerDetails["First_Name"].ToString();
                //    txtLastName.Text = drCustomerDetails["Last_Name"].ToString();
                //}
                All();
                lnkAll.Attributes.Add("class", "sel");
                lnkMyReferrals.Attributes.Add("class", "");
                lnkMyRebates.Attributes.Add("class", "");
                if (Session["isValidPaypal"] != null)
                {
                    redeemdiv.Visible = true;
                    btnRedeem.Focus();
                    txtPaypalUsername.Text = Session["PaypalUserEmail"] as string;
                    lblCredits.Text = comman.FormatCredits(Session["RedeemCredits"] as string);
                    lblAmount.Text = "$" + Session["RedeemAmount"] as string + ".00";
                    hiddenCredits.Value = Session["RedeemCredits"] as string;
                    Session["isValidPaypal"] = null;
                }
                else
                {
                    redeemdiv.Visible = false;
                }
            }
        }
        public void All()
        {

            StringBuilder strall = new StringBuilder();
            _Customer_Credit_Details obj = new _Customer_Credit_Details();
            obj.Status = 0;
            obj.CustomerId = Convert.ToInt32(CustomerId);
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr = sqlobj.BindCustomerCreditDetails(obj);

            strall.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            //strall.Append("<tr class='toprow'><td width='12%'>Date</td><td width='30%'>Purchase</td><td width='14%'> Reward Type</td><td width='14%'>Reward Details</td><td width='14%'>Credits</td><td width='14%'>Total Credits</td><td width='10%'>Status</td></tr>");
            strall.Append("<tr class='toprow'><td width='14%'>Date</td><td width='37%'>Purchase</td><td width='14%'>  Type</td><td width='14%'> Details</td><td width='10%'>Credits</td><td width='17%'>Status</td></tr>");
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (dr["Credit_Received"].ToString() == "0" && dr["Total_redeemed_Credits"].ToString() == "0")
                        continue;
                    strall.Append("<tr>");
                    if (dr["Type"].ToString().Contains("Redeemed"))
                        strall.Append("<td class='first'><a href=\"javascript:void();\" onclick=\"RedirectCustomerReferenceIdRedeem(" + dr["Credit_Transaction_ID"].ToString() + "," + Session["CustomerID"].ToString() + ")\">" + dr["AddedOn"].ToString() + "</a></td>");
                    else if (dr["Type"].ToString().Contains("Merchant Referral"))
                    {
                        strall.Append("<td class='first' style='color:#085baf;'>"+dr["AddedOn"].ToString()+"</td>");
                    }
                    else
                        strall.Append("<td class='first'><a href=\"javascript:void();\" onclick=\"RedirectCustomerReferenceId(" + dr["Customer_Reference_Id"].ToString() + "," + Session["CustomerID"].ToString() + ")\">" + dr["AddedOn"].ToString() + "</a></td>");
                    if (dr["Type"].ToString().Contains("Redeemed"))
                    {
                        strall.Append("<td></td>");
                    }
                    else
                    {
                        if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                        {
                            strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></a></div>");
                        }
                        else
                        {
                            strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></a></div>");
                        }
                        if (dr["Type"].ToString().Contains("Merchant Referral"))
                        {
                            strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span></div><div class='clr'></div></td>");
                        }
                        else
                        {
                            if (dr["Website"].ToString().Contains("http"))
                                strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                            else
                                strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='http://" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");

                        }
                    }

                    strall.Append("<td>" + dr["Type"].ToString() + "</td>");
                    if (dr["Type"].ToString().Contains("Redeemed"))
                    {
                        strall.Append("<td>" + "$" + string.Format("{0:0.00}", Convert.ToDecimal(dr["Reward"])) + " </td>");
                    }
                    else if (dr["Type"].ToString().Contains("Merchant Referral"))
                    {
                        strall.Append("<td> </td>");
                    }
                    else
                        strall.Append("<td>" + dr["Reward"].ToString() + " </td>");
                    if (dr["Type"].ToString().Contains("Redeemed"))
                    {
                        strall.Append("<td>" + comman.FormatCredits(dr["Total_redeemed_Credits"]) + " Credits" + "</td>");
                    }
                    else
                    {
                        strall.Append("<td>" + comman.FormatCredits(dr["Credit_Received"]) + " Credits" + "</td>");
                    }
                    //strall.Append("<td>" + comman.FormatCredits(dr["Total_Credits"]) + " Credits" + "</td>");
                    if (dr["Customer_Credit_Status"].ToString().Contains("Unvested"))
                    {
                        strall.Append("<td class=\"orng\">To Be Paid<br/><span style=\"font-size: 11px;color:#000000;\">(" + dr["Vesting_Date"].ToString() + ")</span></td>");
                    }
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Decline"))
                        strall.Append("<td style=\"color:red;\">" + dr["Customer_Credit_Status"].ToString() + "</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Pending Payment"))
                        strall.Append("<td style=\"color:red;\">Not Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Successful"))
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed") && dr["Customer_Reference_Id"].ToString() != "0")
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed"))
                        strall.Append("<td style=\"color:#61a922;\">Success</td>");
                    strall.Append("</tr>");
                }
            }
            else
            {
                strall.Append("<tr>");
                strall.Append("<td colspan='6' style='text-align:center';>No Records Found</td>");
                strall.Append("</tr>");
            }
            strall.Append("</table>");
            alltable.InnerHtml = strall.ToString();
            DBAccess.InstanceCreation().disconnect();
        }
        protected void lnkAll_Click(object sender, EventArgs e)
        {
            _Customer_Credits obj1 = new _Customer_Credits();
            obj1.Customer_Id = Convert.ToInt32(Session["CustomerID"].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr1 = sqlobj.BindCustomerCredits(obj1);
            if (dr1.Read())
                UnredeemedCredits = comman.getData(dr1["UnredeemedCredit"], 0);
            else
                UnredeemedCredits = 0;
            CustomerId = Session["CustomerID"].ToString();
            lnkAll.Attributes.Add("class", "sel");
            lnkMyReferrals.Attributes.Add("class", "");
            lnkMyRebates.Attributes.Add("class", "");
            lnkRedeeemed.Attributes.Add("class", "");
            All();
        }
        protected void lnkMyReferrals_Click(object sender, EventArgs e)
        {
            _Customer_Credits obj1 = new _Customer_Credits();
            obj1.Customer_Id = Convert.ToInt32(Session["CustomerID"].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr1 = sqlobj.BindCustomerCredits(obj1);
            if (dr1.Read())
                UnredeemedCredits = comman.getData(dr1["UnredeemedCredit"], 0);
            else
                UnredeemedCredits = 0;
            CustomerId = Session["CustomerID"].ToString();
            lnkAll.Attributes.Add("class", "");
            lnkMyReferrals.Attributes.Add("class", "sel");
            lnkMyRebates.Attributes.Add("class", "");
            lnkRedeeemed.Attributes.Add("class", "");

            StringBuilder strall = new StringBuilder();
            _Customer_Credit_Details obj = new _Customer_Credit_Details();
            obj.Status = 0;
            obj.CustomerId = Convert.ToInt32(CustomerId);
            SqlDataReader dr = sqlobj.BindCustomerCreditDetails(obj);

            strall.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            strall.Append("<tr class='toprow'><td width='14%'>Date</td><td width='37%'>Purchase</td><td width='14%'> Type</td><td width='14%'>Details</td><td width='10%'>Credits</td><td width='17%'>Status</td></tr>");
            //strall.Append("<tr class='toprow'><td width='12%'>Date</td><td width='30%'>Purchase</td><td width='14%'> Reward Type</td><td width='14%'>Reward Details</td><td width='14%'>Credits</td><td width='14%'>Total Credits</td><td width='10%'>Status</td></tr>");
            while (dr.Read())
            {
                if (dr["Credit_Received"].ToString() == "0")
                    continue;
                if (dr["Type"].ToString() == "Referral")
                {
                    strall.Append("<tr>");
                    strall.Append("<td class='first'><a href=\"javascript:void();\" onclick=\"RedirectCustomerReferenceId(" + dr["Customer_Reference_Id"].ToString() + "," + Session["CustomerID"].ToString() + ")\">" + dr["AddedOn"].ToString() + "</a></td>");
                    if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                    {
                        strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></a></div>");
                    }
                    else
                    {
                        strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></a></div>");
                    }
                    //strall.Append("<td><div class='image'><a href='#'><img src=\"" + dr["Campaign_Image"].ToString() + "\" alt='' /></a></div>");
                    if (dr["Website"].ToString().Contains("http"))
                        strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                    else
                        strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='http://" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");

                    strall.Append("<td>" + dr["Type"].ToString() + "</td>");
                    strall.Append("<td>" + dr["Reward"].ToString() + " </td>");
                    strall.Append("<td>" + comman.FormatCredits(dr["Credit_Received"]) + " Credits" + "</td>");
                    //strall.Append("<td>" + comman.FormatCredits(dr["Total_Credits"]) + " Credits" + "</td>");
                    if (dr["Customer_Credit_Status"].ToString().Contains("Unvested"))
                    {
                        strall.Append("<td class=\"orng\">To Be Paid<br/><span style=\"font-size: 11px;color:#000000;\">(" + dr["Vesting_Date"].ToString() + ")</span></td>");
                    }
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Decline"))
                        strall.Append("<td style=\"color:red;\">" + dr["Customer_Credit_Status"].ToString() + "</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Pending Payment"))
                        strall.Append("<td style=\"color:red;\">Not Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Successful"))
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed"))
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    strall.Append("</tr>");
                }
            }
            strall.Append("</table>");
            alltable.InnerHtml = strall.ToString();
            DBAccess.InstanceCreation().disconnect();
        }
        protected void lnkMyRebates_Click(object sender, EventArgs e)
        {
            _Customer_Credits obj1 = new _Customer_Credits();
            obj1.Customer_Id = Convert.ToInt32(Session["CustomerID"].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr1 = sqlobj.BindCustomerCredits(obj1);
            if (dr1.Read())
                UnredeemedCredits = comman.getData(dr1["UnredeemedCredit"], 0);
            else
                UnredeemedCredits = 0;
            CustomerId = Session["CustomerID"].ToString();
            lnkAll.Attributes.Add("class", "");
            lnkMyReferrals.Attributes.Add("class", "");
            lnkMyRebates.Attributes.Add("class", "sel");
            lnkRedeeemed.Attributes.Add("class", "");

            StringBuilder strall = new StringBuilder();
            _Customer_Credit_Details obj = new _Customer_Credit_Details();
            obj.Status = 0;
            obj.CustomerId = Convert.ToInt32(CustomerId);

            SqlDataReader dr = sqlobj.BindCustomerCreditDetails(obj);

            strall.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            //strall.Append("<tr class='toprow'><td width='12%'>Date</td><td width='30%'>Purchase</td><td width='14%'> Reward Type</td><td width='14%'>Reward Details</td><td width='14%'>Credits</td><td width='14%'>Total Credits</td><td width='10%'>Status</td></tr>");
            strall.Append("<tr class='toprow'><td width='14%'>Date</td><td width='37%'>Purchase</td><td width='14%'> Type</td><td width='14%'>Details</td><td width='10%'>Credits</td><td width='17%'>Status</td></tr>");
            while (dr.Read())
            {
                if (dr["Credit_Received"].ToString() == "0")
                    continue;
                if (dr["Type"].ToString() == "Rebate")
                {
                    strall.Append("<tr>");
                    strall.Append("<td class='first'><a href=\"javascript:void();\" onclick=\"RedirectCustomerReferenceId(" + dr["Customer_Reference_Id"].ToString() + "," + Session["CustomerID"].ToString() + ")\">" + dr["AddedOn"].ToString() + "</a></td>");
                    if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                    {
                        strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></a></div>");
                    }
                    else
                    {
                        strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></a></div>");
                    }
                    //strall.Append("<td><div class='image'><a href='#'><img src=\"" + dr["Campaign_Image"].ToString() + "\" alt='' /></a></div>");
                    if (dr["Website"].ToString().Contains("http"))
                        strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                    else
                        strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='http://" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                    strall.Append("<td>" + dr["Type"].ToString() + "</td>");
                    //strall.Append("<td>Rebate <span class='size10'>Referred by " + dr["Reward Type"] + "</span></td>");
                    strall.Append("<td>" + dr["Reward"].ToString() + " </td>");
                    strall.Append("<td>" + comman.FormatCredits(dr["Credit_Received"]) + " Credits" + "</td>");
                    // strall.Append("<td>" + comman.FormatCredits(dr["Total_Credits"]) + " Credits" + "</td>");
                    if (dr["Customer_Credit_Status"].ToString().Contains("Unvested"))
                    {
                        strall.Append("<td class=\"orng\">To Be Paid<br/><span style=\"font-size: 11px;color:#000000;\">(" + dr["Vesting_Date"].ToString() + ")</span></td>");
                    }
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Decline"))
                        strall.Append("<td style=\"color:red;\">" + dr["Customer_Credit_Status"].ToString() + "</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Pending Payment"))
                        strall.Append("<td style=\"color:red;\">Not Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Successful"))
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    else if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed"))
                        strall.Append("<td style=\"color:#61a922;\">Paid</td>");
                    strall.Append("</tr>");
                }
            }
            strall.Append("</table>");
            alltable.InnerHtml = strall.ToString();
            DBAccess.InstanceCreation().disconnect();
        }
        protected void btnRedeem_Click(object sender, EventArgs e)
        {
            if (txtPaypalUsername.Text != Session["PaypalUserEmail"] as string)
            {
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/RedeemStatus/" + "failed^0");
                return;
            }
            CustomerId = Session["CustomerID"].ToString();
            _Customer_Credits obj = new _Customer_Credits();
            obj.Customer_Id = Convert.ToInt32(CustomerId);
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr = sqlobj.BindCustomerCredits(obj);
            if (dr.Read())
                UnredeemedCredits = comman.getData(dr["UnredeemedCredit"], 0);
            else
                UnredeemedCredits = 0;
            int Amount = 0;
            if (Convert.ToInt32(hiddenCredits.Value) <= UnredeemedCredits)
            {
                switch (Convert.ToInt32(hiddenCredits.Value))
                {
                    case 500:
                        Amount = 5;
                        break;
                    case 1000:
                        Amount = 10;
                        break;
                    case 5000:
                        Amount = 50;
                        break;
                    case 10000:
                        Amount = 100;
                        break;
                    case 50000:
                        Amount = 500;
                        break;
                }
                if (Amount > 0)
                {
                    string response = PaypalPay(Amount, txtPaypalUsername.Text, Convert.ToInt32(hiddenCredits.Value));
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/RedeemStatus/" + response);
                }
                overlay.Style["display"] = "none";
                progressdiv.Style["display"] = "none";
            }
        }
        protected string PaypalPay(int Amount, string Receiver, int credits)
        {
            // API endpoint for the Refund call in the Sandbox
            string sAPIEndpoint = "https://svcs.sandbox.paypal.com/AdaptivePayments/Pay";
            // Version that you are coding against
            string sVersion = "1.1.0";
            // Error Langugage
            string sErrorLangugage = "en_US";
            // Detail Level
            string sDetailLevel = "ReturnAll";
            // Request Data Binding
            string sRequestDataBinding = "XML";
            // Response Data Binding
            string sResponseDataBinding = "XML";
            // Application ID
            string sAppID = "APP-80W284485P519543T";
            // other clientDetails fields
            string sIpAddress = "127.0.0.1";
            string sPartnerName = "Social Referral";
            string sDeviceID = "127.0.0.1";
            // Currency Code
            string sCurrencyCode = "USD";
            // Action Type
            string sActionType = "PAY";
            // ReturnURL and CancelURL used for approval flow
            string sReturnURL = ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/RedeemStatus";
            string sCancelURL = ConfigurationManager.AppSettings["pageURL"] + "Site/Customer/RedeemStatus/cancel";
            // who pays the fees
            string sFeesPayer = "EACHRECEIVER";
            // memo field
            string sMemo = "Redeem Credits";
            // transaction amount

            // supply your own sandbox accounts for receiver and sender


            string sTrackingID = System.Guid.NewGuid().ToString();

            // construct the XML request string
            StringBuilder sRequest = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sRequest.Append("<PayRequest xmlns:ns2=\"http://svcs.paypal.com/types/ap\">");
            // requestEnvelope fields
            sRequest.Append("<requestEnvelope><errorLanguage>");
            sRequest.Append(sErrorLangugage);
            sRequest.Append("</errorLanguage><detailLevel>");
            sRequest.Append(sDetailLevel);
            sRequest.Append("</detailLevel></requestEnvelope>");
            // clientDetails fields
            sRequest.Append("<clientDetails><applicationId>");
            sRequest.Append(sAppID);
            sRequest.Append("</applicationId><deviceId>");
            sRequest.Append(sDeviceID);
            sRequest.Append("</deviceId><ipAddress>");
            sRequest.Append(sIpAddress);
            sRequest.Append("</ipAddress><partnerName>");
            sRequest.Append(sPartnerName);
            sRequest.Append("</partnerName></clientDetails>");
            // request specific data fields
            sRequest.Append("<actionType>");
            sRequest.Append(sActionType);
            sRequest.Append("</actionType><cancelUrl>");
            sRequest.Append(sCancelURL);
            sRequest.Append("</cancelUrl><returnUrl>");
            sRequest.Append(sReturnURL);
            sRequest.Append("</returnUrl><currencyCode>");
            sRequest.Append(sCurrencyCode);
            sRequest.Append("</currencyCode><feesPayer>");
            sRequest.Append(sFeesPayer);
            sRequest.Append("</feesPayer><memo>");
            sRequest.Append(sMemo);
            sRequest.Append("</memo><receiverList><receiver><amount>");
            sRequest.Append(Amount);
            sRequest.Append("</amount><email>");
            sRequest.Append(Receiver);
            sRequest.Append("</email></receiver></receiverList><senderEmail>");
            sRequest.Append("prateek_kulshrestha@seologistics.com");
            sRequest.Append("</senderEmail><trackingId>");
            sRequest.Append(sTrackingID);
            sRequest.Append("</trackingId></PayRequest>");


            // get ready to make the call
            HttpWebRequest oPayRequest = (HttpWebRequest)WebRequest.Create(sAPIEndpoint);
            oPayRequest.Method = "POST";
            byte[] array = Encoding.UTF8.GetBytes(sRequest.ToString());
            oPayRequest.ContentLength = array.Length;
            oPayRequest.ContentType = "text/xml;charset=utf-8";
            // set the HTTP Headers
            oPayRequest.Headers.Add("X-PAYPAL-SECURITY-USERID", "prateek_kulshrestha_api1.seologistics.com");
            oPayRequest.Headers.Add("X-PAYPAL-SECURITY-PASSWORD", "1387883122");
            oPayRequest.Headers.Add("X-PAYPAL-SECURITY-SIGNATURE", "AHdXXLAsMibXoMzmdURQsBjVkNMbAjYWP4fxDaOnYlXLfKf81gcfNWyY");
            oPayRequest.Headers.Add("X-PAYPAL-SERVICE-VERSION", sVersion);
            oPayRequest.Headers.Add("X-PAYPAL-APPLICATION-ID", sAppID);
            oPayRequest.Headers.Add("X-PAYPAL-REQUEST-DATA-FORMAT", sRequestDataBinding);
            oPayRequest.Headers.Add("X-PAYPAL-RESPONSE-DATA-FORMAT", sResponseDataBinding);
            // send the request
            Stream oStream = oPayRequest.GetRequestStream();
            oStream.Write(array, 0, array.Length);
            oStream.Close();
            // get the response
            ServicePointManager.Expect100Continue = false;
            HttpWebResponse oPayResponse = (HttpWebResponse)oPayRequest.GetResponse();
            StreamReader oStreamReader = new StreamReader(oPayResponse.GetResponseStream());
            string sResponse = oStreamReader.ReadToEnd();
            oStreamReader.Close();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(sResponse);
            try
            {
                string status = xmldoc.SelectSingleNode("//paymentInfoList/paymentInfo/transactionStatus").InnerText;
                string transactionid = xmldoc.SelectSingleNode("//paymentInfoList/paymentInfo/transactionId").InnerText;

                //Deduct credits from customer account
                _Customer_Redeem_Credits obj = new _Customer_Redeem_Credits();
                CustomerId = Session["CustomerID"].ToString();
                obj.Customer_Id = Convert.ToInt32(CustomerId);
                obj.redeemed_credits = Convert.ToDecimal(credits);
                obj.Paypal_Transaction_ID = transactionid;
                obj.Paypal_Corelation_ID = sTrackingID;
                obj.Paypal_Username = Receiver;
                obj.Paypal_First_Name = "";
                obj.Paypal_Last_Name = "";
                DAL.Plugin sqlobj = new DAL.Plugin();
                bool isSaved = sqlobj.UpdateCustomerAvailableCredits(obj);
                return status + "^" + transactionid;
            }
            catch
            {
                return "failed^0";
            }
        }
        protected void lnkRedeeemed_Click(object sender, EventArgs e)
        {
            CustomerId = Session["CustomerID"].ToString();
            _Customer_Credits obj1 = new _Customer_Credits();
            obj1.Customer_Id = Convert.ToInt32(Session["CustomerID"].ToString());
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr1 = sqlobj.BindCustomerCredits(obj1);
            if (dr1.Read())
                UnredeemedCredits = comman.getData(dr1["UnredeemedCredit"], 0);
            else
                UnredeemedCredits = 0;
            lnkAll.Attributes.Add("class", "");
            lnkMyReferrals.Attributes.Add("class", "");
            lnkMyRebates.Attributes.Add("class", "");
            lnkRedeeemed.Attributes.Add("class", "sel");
            StringBuilder strall = new StringBuilder();
            _Customer_Credit_Details obj = new _Customer_Credit_Details();
            obj.Status = 0;
            obj.CustomerId = Convert.ToInt32(CustomerId);
            SqlDataReader dr = sqlobj.BindCustomerCreditDetails(obj);
            strall.Append("<table cellpadding='0' cellspacing='0' border='0' width='100%'>");
            strall.Append("<tr class='toprow'><td width='14%'>Date</td><td width='37%'>Purchase</td><td width='14%'> Type</td><td width='14%'>Details</td><td width='10%'>Credits</td><td width='17%'>Status</td></tr>");
            while (dr.Read())
            {
                if (dr["Type"].ToString() != "Redeemed")
                    continue;
                if (dr["Customer_Credit_Status"].ToString() == "Redeemed")
                {
                    strall.Append("<tr>");
                    if (dr["Type"].ToString().Contains("Redeemed"))
                        strall.Append("<td class='first'><a href=\"javascript:void();\" onclick=\"RedirectCustomerReferenceIdRedeem(" + dr["Credit_Transaction_ID"].ToString() + "," + Session["CustomerID"].ToString() + ")\">" + dr["AddedOn"].ToString() + "</a></td>");
                    else
                        strall.Append("<td class='first'>" + dr["AddedOn"].ToString() + "</td>");
                    if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed") && dr["Customer_Reference_Id"].ToString() != "0")
                    {
                        if (dr["Campaign_Image"].ToString() != "" && File.Exists(Server.MapPath("~/images/MerchantImage/") + dr["Campaign_Image"]))
                        {
                            strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + dr["Campaign_Image"].ToString() + " alt='' /></a></div>");
                        }
                        else
                        {
                            strall.Append("<td><div class='image'><a href='#'><img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/NoImage.jpg alt='' /></a></div>");
                        }
                        if (dr["Website"].ToString().Contains("http"))
                            strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                        else
                            strall.Append("<div class='txt'><span>" + dr["Title"].ToString() + " </span> <a href='http://" + dr["Website"] + "' target='_blank'>" + dr["Website"] + "</a></div><div class='clr'></div></td>");
                    }
                    else
                    {
                        strall.Append("<td></td>");
                    }
                    strall.Append("<td>" + dr["Type"].ToString() + "</td>");
                    //strall.Append("<td>Rebate <span class='size10'>Referred by " + dr["Reward Type"] + "</span></td>");
                    strall.Append("<td>" + "$" + string.Format("{0:0.00}", Convert.ToDecimal(dr["Reward"])) + " </td>");
                    if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed") && dr["Customer_Reference_Id"].ToString() == "0")
                    {
                        strall.Append("<td>" + comman.FormatCredits(dr["Total_redeemed_Credits"]) + " Credits" + "</td>");
                    }
                    else
                    {
                        strall.Append("<td>" + comman.FormatCredits(dr["Credit_Received"]) + " Credits" + "</td>");
                    }
                    if (dr["Customer_Credit_Status"].ToString().Contains("Redeemed") && dr["Customer_Reference_Id"].ToString() != "0")
                        strall.Append("<td>Paid</td>");
                    else
                        strall.Append("<td style=\"color:#61a922;\">Success</td>");
                    strall.Append("</tr>");
                }
            }
            strall.Append("</table>");
            alltable.InnerHtml = strall.ToString();
            DBAccess.InstanceCreation().disconnect();
        }
    }
}