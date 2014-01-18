using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Net;
using System.IO;
namespace EricProject.Plugin
{
    public partial class Offer : System.Web.UI.Page
    {
        public string Color;
        public string Backcolor, EmailID_Subject, EmailID_Message, MessageForReward, divforlinkclick, CopyToClip;
        public string Forecolor;
        public string SubTotal;
        public string Type_of_Reward_R;
        public string Type_of_Reward_R_3;
        public string Type_of_Reward_R_5;
        public string MessageMoneyBack;
        public string imgtd;
        public string FinalCost;
        public string FinalCost_3;
        public string FinalCost_5;
        public string NameOfDay;
        public string Expiry_date;
        public string company_name, Customerfromemailid, CustomerReward;
        public int OfferID;
        public string strShortURL = "", strShortURL_E="";
        public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/O/";
        public string URL_E = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/E/";
        public string pageURL = ConfigurationManager.AppSettings["pageURL"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["OfferID"] != null)
            {
                PAgeURLHidden.Value = ConfigurationManager.AppSettings["pageURL"].ToString(); ;
                string ProductName = "";
                string MessageMoney = "";
                OfferID = Convert.ToInt32(Page.RouteData.Values["OfferID"].ToString());
                _TransactionDetails TransOBJ = new _TransactionDetails();
                TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
                DAL.Plugin Campaignsqlobj = new DAL.Plugin();
                SqlDataReader CampaignDR = Campaignsqlobj.GetOffer(TransOBJ);
                if (CampaignDR.Read())
                {
                    FromEmailID.Value = CampaignDR["Email_ID"].ToString();
                    strShortURL = ShortURL(URL + OfferID);
                    strShortURL_E = ShortURL(URL_E + OfferID);
                    CopyToClip = strShortURL;
                    company_name = ConfigurationManager.AppSettings["company_name"].ToString();
                    Backcolor = CampaignDR["BorderColor"].ToString();
                    Forecolor = CampaignDR["ForeColor"].ToString();
                    Color = CampaignDR["BackGroundColor"].ToString();
                    string CustomorReward = "", ReferrerRewards="";
                    if (Convert.ToDecimal(CampaignDR["Customer_reward_type"].ToString()) == 1)
                    {
                        CustomorReward = "$" + Convert.ToDecimal(CampaignDR["Customer_reward"].ToString());
                    }
                    else
                    {
                        CustomorReward = Convert.ToDecimal(CampaignDR["Customer_reward"].ToString()) + "%";
                    }
                    if (Convert.ToDecimal(CampaignDR["Referrer_reward_type"].ToString()) == 1)
                    {
                        ReferrerRewards = "$" + Convert.ToDecimal(CampaignDR["Referrer_reward"].ToString());
                    }
                    else
                    {
                        ReferrerRewards = Convert.ToDecimal(CampaignDR["Referrer_reward"].ToString()) + "%";
                    }
                    decimal ReferrerReward = Convert.ToDecimal(CampaignDR["Referrer_reward"].ToString());
                    SubTotal = string.Format("{0:0.00}", Convert.ToDecimal(CampaignDR["SubTotal"].ToString().Replace("$", ""))).ToString();
                    EmailID_Subject = CampaignDR["DefaultEmail_Subject"].ToString();
                    EmailID_Message = CampaignDR["DefaultEmail_Message"].ToString();
                    NameOfDay = CampaignDR["NameOfDay"].ToString();
                    Expiry_date = DateTime.Now.AddDays(Convert.ToDouble(CampaignDR["Expiry_days"])).ToShortDateString();
                    if (Convert.ToDecimal(ReferrerReward).ToString() == "0.00")
                        MessageForReward = "Invite your friends and they’ll get " + CustomorReward.ToString() + " off.";
                    else if (Convert.ToDecimal(CampaignDR["Customer_reward"].ToString()).ToString() == "0.00")
                        MessageForReward = "Invite your friends and get " + ReferrerRewards.ToString() + " back when they make a purchase.";
                    else
                        MessageForReward = "Invite your friends and they’ll get  " + CustomorReward.ToString() + "  off.If they buy anything, you’ll Get " + ReferrerRewards.ToString() + " back";

                    if (Convert.ToInt32(CampaignDR["Min_purchase_amt"]) == 0)
                    {
                        if (CampaignDR["SKU_ID"].ToString() == "0")
                        {
                            MessageMoney = "<span style='color:" + Backcolor + "; font-size: 19px;'>" + ReferrerRewards.ToString() + "</span> off your purchase";
                            ProductName = "anything";
                        }
                        else
                        {

                            MessageMoney = "<span style='color:" + Backcolor + "; font-size: 19px;'>" + ReferrerRewards.ToString() + "</span> off your purchase of " + CampaignDR["Product_name"].ToString();
                            ProductName = CampaignDR["Product_name"].ToString();
                        }
                    }
                    else
                    {
                        if (CampaignDR["SKU_ID"].ToString() == "0")
                        {
                            MessageMoney = "<span style='color:" + Backcolor + "; font-size: 19px;'> " + ReferrerRewards + "</span> off your $" + CampaignDR["Min_purchase_amt"] + " purchase";
                            ProductName = "anything";
                        }
                        else
                        {
                            MessageMoney = "<span style='color:" + Backcolor + "; font-size: 19px;'> " + ReferrerRewards + "</span> off your $" + CampaignDR["Min_purchase_amt"] + " purchase of " + CampaignDR["Product_name"].ToString();
                            ProductName = CampaignDR["Product_name"].ToString();
                        }
                    }

                    if (CampaignDR["Campaign_Image"].ToString() == "")
                    {
                        imgtd = "";
                    }
                    else
                    {
                        imgtd = "<td width='24.5%' valign='top' class='logoImg'> <img src=" + ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + CampaignDR["Campaign_Image"].ToString() + " alt='' /></td>";
                    }
                    //Short Facebook share url

                    divforlinkclick = "<td width='30%'><span class='sharecupon'>Share your coupon link</span><input type='text' text='Message' value='" + strShortURL + "'/></td>";

                    decimal savingreferrerprice = Convert.ToDecimal((ReferrerReward / 100) * Convert.ToDecimal(SubTotal.Replace('$', ' ')));
                    if (Convert.ToDecimal(ReferrerReward).ToString() == "0.00")
                        MessageMoneyBack = "<h2><p>Invite your friends and they’ll get " + CustomorReward.ToString() + " off.</h2></p>";
                    else
                    {
                        MessageMoneyBack = "<h2>Let us give you money back! <br /></h2><p> " + MessageMoney.ToString() + " every time you share.</p> ";
                    }
                    if (Convert.ToInt32(CampaignDR["Customer_reward_type"]) == 1)
                    {
                        CustomerReward = "$" + CampaignDR["Customer_reward"].ToString();
                    }
                    else
                    {
                        CustomerReward = CampaignDR["Customer_reward"] + "%";
                    }
                    //  //GetOfferIDFromUrl                 
                    clickurl.Value = "Click on the link below to get " + CustomerReward.ToString() + "off your purchase:<br/><a href=" + strShortURL_E + ">" + strShortURL_E + "</a>";
                
                    if (Convert.ToInt32(CampaignDR["Referrer_reward_type"].ToString()) == 1)
                    {
                        Type_of_Reward_R = string.Format("{0:0.00}", ReferrerReward.ToString());
                        Type_of_Reward_R_3 = string.Format("{0:0.00}", ReferrerReward * 3).ToString();
                        Type_of_Reward_R_5 = string.Format("{0:0.00}", ReferrerReward * 5).ToString();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward))).ToString()).Contains('-'))
                            FinalCost = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward))).ToString().Replace('-', ' ').TrimStart();

                        else
                            FinalCost = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward))).ToString().Replace('-', ' ').TrimStart();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 3))).ToString()).Contains('-'))
                            FinalCost_3 = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 3))).ToString().Replace('-', ' ').TrimStart();
                        else
                            FinalCost_3 = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 3))).ToString().Replace('-', ' ').TrimStart();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 5))).ToString()).Contains('-'))
                            FinalCost_5 = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 5))).ToString().Replace('-', ' ').TrimStart();
                        else
                            FinalCost_5 = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(ReferrerReward * 5))).ToString().Replace('-', ' ').TrimStart();
                    }
                    else
                    {
                        Type_of_Reward_R = string.Format("{0:0.00}", savingreferrerprice).ToString();
                        Type_of_Reward_R_3 = string.Format("{0:0.00}", savingreferrerprice * 3).ToString();
                        Type_of_Reward_R_5 = string.Format("{0:0.00}", savingreferrerprice * 5).ToString();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString()).Contains('-'))
                            FinalCost = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString().Replace('-', ' ').TrimStart();
                        else
                            FinalCost = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice))).ToString().Replace('-', ' ').TrimStart();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString()).Contains('-'))
                            FinalCost_3 = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString().Replace('-', ' ').TrimStart();
                        else
                            FinalCost_3 = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 3))).ToString().Replace('-', ' ').TrimStart();
                        if ((string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString()).Contains('-'))
                            FinalCost_5 = "-$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString().Replace('-', ' ').TrimStart();
                        else
                            FinalCost_5 = "$" + string.Format("{0:0.00}", (Convert.ToDecimal(SubTotal) - Convert.ToDecimal(savingreferrerprice * 5))).ToString().Replace('-', ' ').TrimStart();

                    }
                }
            }
        }
        public string ShortURL(string LongUrl)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.googleapis.com/urlshortener/v1/url");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"longUrl\":\"" + LongUrl + "\"}";
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Response.Write(responseText);
                int iStart = responseText.IndexOf("id") + 6;
                int iEnd = responseText.IndexOf("longUrl") - 5;
                responseText = responseText.Substring(iStart, iEnd - iStart);
                return responseText;
            }
        }
    }
}