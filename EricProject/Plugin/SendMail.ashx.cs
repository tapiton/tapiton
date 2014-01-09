using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BAL;
using BusinessObject;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
namespace EricProject.Plugin
{
    /// <summary>
    /// Summary description for SendMail
    /// </summary>
    public class SendMail : IHttpHandler
    {
        string OfferID, Customer_ID, FromEmailId;
        int Campaign_ID;

        string mode, firstName;
        string Message, clickurl;
        public void ProcessRequest(HttpContext context)
        {
            mode = comman.getData(context.Request.QueryString["Mode"], "");
            string ToEmailAddress = comman.getData(context.Request.QueryString["ToEmailAddress"], "");
            string Subject = comman.getData(context.Request.QueryString["Subject"], "");
            Message = comman.getData(context.Request.QueryString["Message"].ToString(), "");
            OfferID = comman.getData(context.Request.QueryString["OfferID"].ToString(), "");
            FromEmailId = comman.getData(context.Request.QueryString["FromEmailId"].ToString(), "");
            clickurl = comman.getData(context.Request.QueryString["clickurl"].ToString(), "");
            if (mode == "1")
            {
                LinkClick(6);
                Email_offer();
                string[] EMailAddress = ToEmailAddress.Split(',');
                //string mail = "<span style='font-family:Arial ;font-size:12px;'>Your friend " + firstName + " wants to share a deal with you. <a href='"+ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/E/" + OfferID + "'>Click here</a> to explore it.<br/><br/>" + Message + "<br/><br/>"+clickurl+"</span>";
                string mail = "<span style='font-family:Arial ;font-size:12px;'>Your friend " + firstName + " wants to share a deal with you. <br/><br/>" + Message + "<br/><br/>" + clickurl + "</span>";
                string sendmessage = mail.Replace("\n", "<br/>");
                foreach (string word in EMailAddress)
                {
                    Reach();
                    comman.SendMail(word, Subject, mail);
                    //comman.SendMailfrom(word, FromEmailId, Subject, mail, "", "");
                }
                AddCampaignEmailText();
            }
            else if (mode == "2")
            {
                LinkClick(7);
            }
            else if (mode == "4")
            {
                // LinkClick(9);
            }
            // context.Response.ContentType = "text/plain";
            // context.Response.Write("Hello World");

        }

        //Add Update Email Reach
        //public void UpdateEmailReach()
        //{
        //    _TransactionDetails TransOBJ = new _TransactionDetails();
        //    TransOBJ.Transaction_ID = Convert.ToInt32(TransactionID);
        //    DAL.Plugin Campaignsqlobj = new DAL.Plugin();
        //    SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

        //    if (CampaignDR.Read())
        //    {
        //        DAL.Plugin sqlPlugin = new DAL.Plugin();
        //        _Customer_Social_Share_Tokens objCSST = new _Customer_Social_Share_Tokens();
        //        objCSST.TokenID = 0;
        //        objCSST.EmailID = CampaignDR["Email_ID"].ToString();
        //        objCSST.Name = CampaignDR["First_Name"].ToString();
        //        objCSST.Gender = "";
        //        objCSST.FacebookId = "";
        //        objCSST.TwitterId = "";
        //        objCSST.FacebookAccessToken = "";
        //        objCSST.TwitterAccessToken = "";
        //        objCSST.TwitterAccessTokenSecret = "";
        //        objCSST.IsFBTW = true;
        //        objCSST.TotalFriends = "";
        //        objCSST.Follower = "";
        //        objCSST.EmailReach = 1;
        //        objCSST.CustomerId = Convert.ToInt32(CampaignDR["Customer_ID"].ToString());
        //        sqlPlugin.InsertInToCustomer_Social_Share_TokensEmailReach(objCSST);
        //    }
        //}
        public void LinkClick(int StatusClick)
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

            if (CampaignDR.Read())
            {
                Campaign_ID = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Customer_ID = CampaignDR["Customer_ID"].ToString();
                firstName = CampaignDR["First_Name"].ToString();
            }

            _Campaigns_Stats objCampaign_Stats = new _Campaigns_Stats();
            DAL.Plugin Campaignssqlobj = new DAL.Plugin();
            objCampaign_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID.ToString());
            objCampaign_Stats.FB_click = 0;
            objCampaign_Stats.FBShare_Click = 0;
            objCampaign_Stats.Link_Click = 1;
            objCampaign_Stats.Proceed_Click = 0;
            objCampaign_Stats.Tweet_Click = 0;
            objCampaign_Stats.Email_Click = 0;
            objCampaign_Stats.EmailSubmit_Click = 0;
            objCampaign_Stats.StatusClick = StatusClick;
            int resultFacebookShareClick = Campaignssqlobj.InsertInToCampaignsStats(objCampaign_Stats);


        }
        public void Reach()
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

            if (CampaignDR.Read())
            {
                Campaign_ID = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Customer_ID = CampaignDR["Customer_ID"].ToString();

            }
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + OfferID;
            sqlOffer.Customer_Id = Customer_ID.ToString();
            sqlOffer.Campaign_Id = Campaign_ID.ToString();
            sqlOffer.Expiry_Time = DateTime.Now;
            sqlOffer.Clicks = "1";
            sqlOffer.Reach = "1";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 3;
            sqlOffer.TransactionId = 0;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerobj.InsertInToOfferDetails(sqlOffer);
        }
        public void Email_offer()
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

            if (CampaignDR.Read())
            {
                Campaign_ID = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Customer_ID = CampaignDR["Customer_ID"].ToString();

            }
            _Offer sqlOffer = new _Offer();
            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + OfferID;
            sqlOffer.Customer_Id = Customer_ID.ToString();
            sqlOffer.Campaign_Id = Campaign_ID.ToString();
            sqlOffer.Expiry_Time = DateTime.Now;
            sqlOffer.Clicks = "1";
            sqlOffer.Reach = "1";
            sqlOffer.Referrals = 0;
            sqlOffer.Sales = 0;
            sqlOffer.Referrer_Credits = 0;
            sqlOffer.Status = 10;
            sqlOffer.TransactionId = 0;
            DAL.Plugin offerobj = new DAL.Plugin();
            offerobj.InsertInToOfferDetails(sqlOffer);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //Add Campaign Email share text 
        public void AddCampaignEmailText()
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

            if (CampaignDR.Read())
            {
                _Offer_Posts objCustomerSocialSharePost = new _Offer_Posts();
                objCustomerSocialSharePost.Campaign_Id = CampaignDR["Campaign_ID"].ToString();
                objCustomerSocialSharePost.Customer_Id = CampaignDR["Customer_Id"].ToString();
                objCustomerSocialSharePost.Post_Location = 3;
                objCustomerSocialSharePost.Text = Message;
                objCustomerSocialSharePost.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + OfferID;
                int resultShare = Campaignsqlobj.InsertIntoCampaignSharetext(objCustomerSocialSharePost);

                _Customer_Shares objCustomer_Shares = new _Customer_Shares();
                objCustomer_Shares.Post_Id = resultShare;
                objCustomer_Shares.Recipient_Email = CampaignDR["Email_ID"].ToString();
                Campaignsqlobj.InsertIntoCustomerShares(objCustomer_Shares);
            }
        }
        //Add Campaign Email share text 


    }
}