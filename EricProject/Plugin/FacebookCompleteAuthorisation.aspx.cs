using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DAL;
using BusinessObject;
using System.Configuration;
using System.Net;
using System.IO;


public partial class Plugin_FacebookCompleteAuthorisation : System.Web.UI.Page
{
    public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Shared/F/";

    protected void Page_Load(object sender, EventArgs e)
    {
        string facebookClientId = System.Configuration.ConfigurationManager.AppSettings["FacebookClientId"];
        string facebookSecret = System.Configuration.ConfigurationManager.AppSettings["FacebookSecret"];
        string host = Request.ServerVariables["HTTP_HOST"];
        string code = Request.QueryString["code"];
   

        //string response = Tools.CallUrl("https://graph.facebook.com/oauth/access_token?client_id=362001227199877&redirect_uri=http://localhost:2180/EricProject/Plugin/FacebookCompleteAuthorisation.aspx&client_secret=10691f7e98deeab41ed22f0bb5cd37a6&code=" + code);

        string response = Tools.CallUrl("https://graph.facebook.com/oauth/access_token?client_id=" + facebookClientId + "&redirect_uri="+ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/FacebookCompleteAuthorisation.aspx&client_secret=" + facebookSecret + "&code=" + code);

        BusinessObject.Facebook fb = new FacebookService().GetById(99);
        fb.FacebookAccessToken = response.Replace("access_token=", ""); // save the access token you are given
        fb.FacebookId = new FacebookService().User_GetDetails(fb.FacebookAccessToken).id; // call facebook for the profile id
        new FacebookService().Save(fb); // save it (puts stuff into session for the time being)

        response = Tools.CallUrl(string.Concat("https://graph.facebook.com/oauth/access_token?grant_type=fb_exchange_token", "&client_id=" + facebookClientId, "&client_secret=" + facebookSecret, "&fb_exchange_token=" + fb.FacebookAccessToken));


        FacebookFriendCounts FriendsCount = new FacebookService().Friend_GetDetails(fb.FacebookAccessToken);

        FacebookProfile objFacebookProfile = new FacebookService().User_GetDetails(fb.FacebookAccessToken);

        string FName = objFacebookProfile.first_name;
        string LName = objFacebookProfile.last_name;
        string UID = objFacebookProfile.id;
        string Name = objFacebookProfile.name;
        string Gender = objFacebookProfile.Gender;
        //Insert into Customer_Social_Share_Tokens
        DAL.Plugin sqlPlugin = new DAL.Plugin();
        _Customer_Social_Share_Tokens objCSST = new _Customer_Social_Share_Tokens();
        objCSST.TokenID = 0;
        objCSST.FacebookId = UID;
        objCSST.TwitterId =  "";
        objCSST.FacebookAccessToken = fb.FacebookAccessToken;
        objCSST.TwitterAccessToken =  "";
        objCSST.TwitterAccessTokenSecret = "";
        objCSST.TotalFriends = FriendsCount.data.Count.ToString();
        objCSST.Follower = "";
        objCSST.CustomerId =Convert.ToInt32(Session["Customer_ID"].ToString());
        objCSST.Gender = Gender;
        Session["TotalFriends"] = FriendsCount.data.Count.ToString();
        if (Session["CheckTokenExpire"] + "" != "1")
        {
            sqlPlugin.InsertInToCustomer_Social_Share_Tokens_Facebook(objCSST);
        }
        else
        {
            objCSST.CustomerId = Convert.ToInt32(Session["Customer_ID"].ToString());
            objCSST.FacebookAccessToken = fb.FacebookAccessToken;
            objCSST.TotalFriends = FriendsCount.data.Count.ToString();
            sqlPlugin.Update_Customer_Social_Share_Tokens_Facebook(objCSST);
        }
        //Insert into Customer_Social_Share_Tokens

        //Add Campaign share Facebook text 
        DAL.Plugin Campaignsqlobj = new DAL.Plugin();
        _Offer_Posts objCustomerSocialSharePost = new _Offer_Posts();
        objCustomerSocialSharePost.Campaign_Id = Convert.ToString(Session["CampaignID"].ToString());
        objCustomerSocialSharePost.Customer_Id = Convert.ToString(Session["Customer_ID"].ToString());
        objCustomerSocialSharePost.Post_Location = 1;
        objCustomerSocialSharePost.Text = Session["Comment"].ToString();
        objCustomerSocialSharePost.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"];
        int resultShare = Campaignsqlobj.InsertIntoCampaignSharetext(objCustomerSocialSharePost);
        //Add Campaign share Facebook text 

        //Insert  Email to Customer_Shares
        _Customer_Shares objCustomer_Shares = new _Customer_Shares();
        objCustomer_Shares.Post_Id = resultShare;
        objCustomer_Shares.Recipient_Email = Session["Customer_Email"].ToString();
        Campaignsqlobj.InsertIntoCustomerShares(objCustomer_Shares);
        //Insert  Email to Customer_Shares

        DAL.Plugin Campaignsqlobjins = new DAL.Plugin();
        //Facebook post share Increment
        _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

        objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Session["CampaignID"].ToString());
        objCampaigns_Stats.FB_click = 0;
        objCampaigns_Stats.FBShare_Click = 1;
        objCampaigns_Stats.Link_Click = 0;
        objCampaigns_Stats.Proceed_Click = 0;
        objCampaigns_Stats.Tweet_Click = 0;
        objCampaigns_Stats.Email_Click = 0;
        objCampaigns_Stats.EmailSubmit_Click = 0;
        objCampaigns_Stats.StatusClick = 2;
        int resultFacebookShareClick = Campaignsqlobjins.InsertInToCampaignsStats(objCampaigns_Stats);
        //Facebook post share Increment
        _Campaigns_Stats objCampaigns_Stats1 = new _Campaigns_Stats();
        objCampaigns_Stats1.Campaign_Id = Convert.ToInt32(Session["CampaignID"].ToString());
        objCampaigns_Stats1.FB_click = 1;
        objCampaigns_Stats1.FBShare_Click = 0;
        objCampaigns_Stats1.Link_Click = 0;
        objCampaigns_Stats1.Proceed_Click = 0;
        objCampaigns_Stats1.Tweet_Click = 0;
        objCampaigns_Stats1.Email_Click = 0;
        objCampaigns_Stats1.EmailSubmit_Click = 0;
        objCampaigns_Stats1.StatusClick = 1;
        int resultFacebookShareClick1 = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats1);
        //insert into offer table
        _Offer sqlOffer = new _Offer();
        sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
        sqlOffer.Customer_Id = Session["Customer_ID"].ToString();
        sqlOffer.Campaign_Id = Session["CampaignID"].ToString();
        sqlOffer.Expiry_Time = DateTime.Now;
        sqlOffer.Clicks = "0";
        sqlOffer.Reach = Session["TotalFriends"].ToString();
        sqlOffer.Referrals = 0;
        sqlOffer.Sales = 0;
        sqlOffer.Referrer_Credits = 0;
        sqlOffer.Status = 3;
        sqlOffer.TransactionId = 0;
        DAL.Plugin offerobj = new DAL.Plugin();
        offerobj.InsertInToOfferDetails(sqlOffer);
        // end of insert

        sqlOffer.Status = 8;
        offerobj.InsertInToOfferDetails(sqlOffer);
        // end of insert

        //Short Facebook share url
        string strShortURL = ShortURL(URL + Session["OfferID"].ToString());

        if (Session["Campaign_Image"] + "" != "")
        {
            postStatus result1 = new FacebookService().Post_Feed("me", fb.FacebookAccessToken, strShortURL, Session["Campaign_Image"].ToString(), strShortURL, Session["Campaign_Title"].ToString(), Session["Comment"].ToString(), Session["DefaultFaceBook_Title"].ToString());
        }
        else
        {
            postStatus result1 = new FacebookService().Post_Feed("me", fb.FacebookAccessToken, strShortURL, strShortURL, Session["Campaign_Title"].ToString(), Session["Comment"].ToString(), Session["DefaultFaceBook_Title"].ToString());
        }
        //ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>alert('Shared Successful');self.close();</script>");
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
