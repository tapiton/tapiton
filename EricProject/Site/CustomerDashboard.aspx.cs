using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.IO;
using System.Net;

namespace EricProject.Site
{
    public partial class CustomerDashboard : System.Web.UI.Page
    {
        string CustomerId;
        public string TrasId;
        public bool flag1 = false;
        public bool flag2 = false;
        public bool flag3 = false;
        string CampaignId = string.Empty;
        string CampaignId1 = string.Empty;
        string CampaignId2 = string.Empty;
        public string OfferID = "";
        public string strShortURL = ""; string CustomerReward = "";
        public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/O/";
        public string pageURL = ConfigurationManager.AppSettings["pageURL"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerId = Session["CustomerID"].ToString();
            _Customer_Credits obj = new _Customer_Credits();
            obj.Customer_Id = Convert.ToInt32(CustomerId);
            DAL.Plugin sqlobj = new DAL.Plugin();
            SqlDataReader dr = sqlobj.BindCustomerCredits(obj);
            if (dr.Read())
            {
                lblUnredeemedCredit.Text = comman.FormatCredits(dr["UnredeemedCredit"].ToString());
                lblPendingCredits.Text = comman.FormatCredits(dr["PendingCredits"].ToString());
                lblTotalCredits.Text = comman.FormatCredits(dr["TotalCredits"].ToString());
            }
            else
            {
                lblUnredeemedCredit.Text = "0";
                lblPendingCredits.Text = "0";
                lblTotalCredits.Text = "0";
            }

            _Offer objoffer = new _Offer();
            objoffer.Customer_Id = CustomerId;
            SqlDataReader droffer = sqlobj.BindOfferMerchantCampaign(objoffer);
            if (!droffer.HasRows)
            {
                SpanRewardDisclaimer.Visible = false;
            }
            while (droffer.Read())
            {
                SpanRewardDisclaimer.Visible = true;
                CampaignId = droffer["Campaign_ID"].ToString();
                //if (droffer["Campaign_Image"].ToString() == "")
                //    continue;
                hiddenCampaignId.Value = droffer["Campaign_Image"].ToString();
                imgCampaignImage.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + droffer["Campaign_Image"].ToString();
                //lblCampaignName.Text = droffer["Campaign_Name"].ToString();
                if (droffer["Referrer_reward_type"].ToString() == "1")
                {
                    lblCampaignName.Text = "Get $" + droffer["Referrer_reward"].ToString() + "* for each " + droffer["item_Name"].ToString() + " referral";
                }
                else
                {
                    if (droffer["SKU_ID"].ToString() != "0")
                    {
                        lblCampaignName.Text = "Get " + droffer["Referrer_reward"].ToString() + "%*  of the purchase of each referred " + droffer["item_Name"].ToString();
                    }
                    else
                    {
                        lblCampaignName.Text = "Get " + droffer["Referrer_reward"].ToString() + "%* of each referred purchase at " + droffer["item_Name"].ToString();
                    }
                }
                if (lblCampaignName.Text != "") { imgCampaignImage.Alt = lblCampaignName.Text; } else { imgCampaignImage.Alt = "No Image"; }
                lblView.Text = "View " + droffer["Views"].ToString() + " | Share (" + droffer["Shares"].ToString() + ") | Clicks (" + droffer["Clicks"].ToString() + ")";
                string day = droffer["Day"].ToString();
                //lblDay.Text = "<strong>Time Remaining : </strong>" + NumberFormat(day);
                //lblExpiresOn.Text = droffer["Date"].ToString();
                if (droffer["Expiry_Time"].ToString() != "")
                {
                    hiddenIsExpired.Value = "No";
                    lblDay.Text = "<strong>Time Remaining : </strong>" + droffer["Expiry_Time"].ToString();
                }
                else
                {
                    hiddenIsExpired.Value = "Yes";
                    lblDay.Text = "<span style=\"font-weight:bold;color:red;\">Offer expired<span style=\"color:white;\">Offer expired</span></span>";
                }
              
                if (Convert.ToInt32(droffer["Customer_reward_type"]) == 1)
                {
                    CustomerReward = "$" +droffer["Customer_reward"].ToString();                 
                }
                else
                {
                    CustomerReward = droffer["Customer_reward"] + "%";
                }
                //  //GetOfferIDFromUrl
                FromEmailID.Value = droffer["Email_ID"].ToString();
                objoffer.Referral_Url = droffer["Referral_Url"].ToString();
                string OfferID = sqlobj.GetOfferIDFromUrl(objoffer);
                strShortURL = ShortURL(URL + OfferID);
                clickurl.Value = "Click on the link below to get " + CustomerReward.ToString() + "off your purchase:<br/><a href=" + strShortURL + ">" + strShortURL + "</a>";
                txtReferralUrl.Value = ShortURL(ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/o/" + OfferID);
                txtReferralUrlGrey.Value = ShortURL(ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/o/" + OfferID);
                hiddenReferralUrl.Value = ShortURL(ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/o/" + OfferID);
                hiddenReferralUrlFbShare.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/FBShare/" + OfferID;
                hiddenTwitterURL.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/TwShare/" + OfferID;
                TransNo.Value = droffer["Referral_Url"].ToString().Replace(ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/", "");
                EmailSubject.Value = droffer["DefaultEmail_Subject"].ToString();
                EmailMessage.Value = droffer["DefaultEmail_Message"].ToString();
                hiddenUrlMyOffer.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/o/" + OfferID;
                //litTwitter.Text = "<a href='https://twitter.com/share' class='twitter-share-button' data-url=" + droffer["Referral_Url"].ToString() + " data-via='your_screen_name' target='_blank' data-lang='en'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/newimages/twitter_icon.png' alt='' border='0' /></a>";
                //litTwitter.Text = "<a href='"+ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/TwShare/481'><img src='" + ConfigurationManager.AppSettings["pageURL"] + "images/newimages/twitter_icon.png' border='0' /></a>";
                lnkViewDeal.Visible = true;
                divbotlinks.Visible = true;
                break;
            }

            _Offer_Campaign objofferCampaign = new _Offer_Campaign();
            objofferCampaign.CampaignId = CampaignId;
            SqlDataReader drofferCampaign = sqlobj.BindMoreOffer(objofferCampaign);

            while (drofferCampaign.Read())
            {
                CampaignId1 = drofferCampaign["Campaign_Id"].ToString();
                if (drofferCampaign["Campaign_Image"].ToString() == "")
                    continue;
                imgoffer1.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drofferCampaign["Campaign_Image"].ToString();
                //lblCampaign1.Text = drofferCampaign["Campaign_Name"].ToString();
                if (drofferCampaign["Customer_reward_type"].ToString() == "$")
                    lblCampaign1.Text = "$" + drofferCampaign["Customer_reward"].ToString() + " off " + drofferCampaign["item_Name"].ToString();
                else
                    lblCampaign1.Text = drofferCampaign["Customer_reward"].ToString() + "% off " + drofferCampaign["item_Name"].ToString();
                imgoffer1.Alt = lblCampaign1.Text;
                lblWebsiteUrl1.Text = drofferCampaign["Website"].ToString();
                hiddenUrl1.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/o/" + drofferCampaign["Offer_ID"].ToString();
                break;
            }

            objofferCampaign.CampaignId = CampaignId;
            objofferCampaign.CampaignId1 = CampaignId1;
            SqlDataReader drofferCampaign2 = sqlobj.BindMoreOffer2(objofferCampaign);

            while (drofferCampaign2.Read())
            {
                CampaignId2 = drofferCampaign2["Campaign_Id"].ToString();
                if (drofferCampaign2["Campaign_Image"].ToString() == "")
                    continue;
                imgoffer2.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drofferCampaign2["Campaign_Image"].ToString();
                //lblCampaign1.Text = drofferCampaign["Campaign_Name"].ToString();
                if (drofferCampaign2["Customer_reward_type"].ToString() == "$")
                    lblCampaign2.Text = "$" + drofferCampaign2["Customer_reward"].ToString() + " off " + drofferCampaign2["item_Name"].ToString();
                else
                    lblCampaign2.Text = drofferCampaign2["Customer_reward"].ToString() + "% off " + drofferCampaign2["item_Name"].ToString();
                imgoffer2.Alt = lblCampaign2.Text;
                lblWebsiteUrl2.Text = drofferCampaign2["Website"].ToString();
                // hiddenUrl2.Value = (lblWebsiteUrl2.Text.IndexOf("http") < 0 ? "http://" + lblWebsiteUrl2.Text : lblWebsiteUrl2.Text);
                hiddenUrl2.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/O/" + drofferCampaign2["Offer_ID"].ToString();
                break;
            }

            objofferCampaign.CampaignId = CampaignId;
            objofferCampaign.CampaignId1 = CampaignId1;
            objofferCampaign.CampaignId2 = CampaignId2;
            SqlDataReader drofferCampaign3 = sqlobj.BindMoreOffer3(objofferCampaign);

            while (drofferCampaign3.Read())
            {
                if (drofferCampaign3["Campaign_Image"].ToString() == "")
                    continue;
                imgoffer3.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drofferCampaign3["Campaign_Image"].ToString();
                //lblCampaign1.Text = drofferCampaign["Campaign_Name"].ToString();
                if (drofferCampaign3["Customer_reward_type"].ToString() == "$")
                    lblCampaign3.Text = "$" + drofferCampaign3["Customer_reward"].ToString() + " off " + drofferCampaign3["item_Name"].ToString();
                else
                    lblCampaign3.Text = drofferCampaign3["Customer_reward"].ToString() + "% off " + drofferCampaign3["item_Name"].ToString();
                imgoffer3.Alt = lblCampaign3.Text;
                lblWebsiteUrl3.Text = drofferCampaign2["Website"].ToString();
                // hiddenUrl3.Value = (lblWebsiteUrl3.Text.IndexOf("http") < 0 ? "http://" + lblWebsiteUrl3.Text : lblWebsiteUrl3.Text);
                hiddenUrl3.Value = ConfigurationManager.AppSettings["pageURL"] + "Plugin/Share/O/" + drofferCampaign3["Offer_ID"].ToString();
                break;
            }



            //if (flag2 == false)
            //{
            //    imgoffer2.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drofferCampaign["Campaign_Image"].ToString();
            //    if (drofferCampaign["Customer_reward_type"].ToString() == "$")
            //        lblCampaign2.Text = "$" + drofferCampaign["Customer_reward"].ToString() + " off " + drofferCampaign["item_Name"].ToString();
            //    else
            //        lblCampaign2.Text = drofferCampaign["Customer_reward"].ToString() + "% off " + drofferCampaign["item_Name"].ToString();
            //    imgoffer2.Alt = lblCampaign2.Text;
            //    lblWebsiteUrl2.Text = drofferCampaign["Website"].ToString();
            //    hiddenUrl2.Value = (lblWebsiteUrl2.Text.IndexOf("http") < 0 ? "http://" + lblWebsiteUrl2.Text : lblWebsiteUrl2.Text);
            //    flag2 = true;
            //    continue;
            //}

            //if (flag3 == false)
            //{
            //    imgoffer3.Src = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + drofferCampaign["Campaign_Image"].ToString();
            //    if (drofferCampaign["Customer_reward_type"].ToString() == "$")
            //        lblCampaign3.Text = "$" + drofferCampaign["Customer_reward"].ToString() + " off " + drofferCampaign["item_Name"].ToString();
            //    else
            //        lblCampaign3.Text = drofferCampaign["Customer_reward"].ToString() + "% off " + drofferCampaign["item_Name"].ToString();
            //    imgoffer3.Alt = lblCampaign3.Text;
            //    lblWebsiteUrl3.Text = drofferCampaign["Website"].ToString();
            //    hiddenUrl3.Value = (lblWebsiteUrl3.Text.IndexOf("http") < 0 ? "http://" + lblWebsiteUrl3.Text : lblWebsiteUrl3.Text);
            //    flag3 = true;
            //    break;
            //}
            DBAccess.InstanceCreation().disconnect();
        }

        public string NumberFormat(string Number)
        {
            string str;
            if (Number == "01" || Number == "21" || Number == "31")
            {
                str = Number + "st ";
            }
            else if (Number == "02" || Number == "22")
            {
                str = Number + "nd ";
            }
            else if (Number == "03" || Number == "23")
            {
                str = Number + "rd ";
            }
            else
            {
                str = Number + "th ";
            }
            return str;
        }

        //Short url
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