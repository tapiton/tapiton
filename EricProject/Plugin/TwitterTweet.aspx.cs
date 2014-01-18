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
using System.Data.SqlClient;
using System.Net;
using System.IO;
using Twitterizer;


public partial class Plugin_TwitterTweet : System.Web.UI.Page
{
    DAL.Plugin Campaignsqlobj = new DAL.Plugin();
    public string DefaultTweet_Message = "";
    public string OfferID = "";
    public string Customer_ID = "";
    public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Shares/T/";
    public int Campaign_ID = 0;
    public string Followers = "0";
    Twitter twitterobj = new Twitter();
    string consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
    string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
    _plugin objPlugin = new _plugin();
    DAL.Plugin sqlPlugin = new DAL.Plugin();
    _Customer_Social_Share_Tokens objCSST = new _Customer_Social_Share_Tokens();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["OfferID"] != null)
        {
            _TransactionDetails TransOBJ = new _TransactionDetails();
            OfferID = Page.RouteData.Values["OfferID"].ToString();
            Session["OfferID"] = OfferID;
           
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);
       
            if (CampaignDR.Read())
            {
                DefaultTweet_Message = CampaignDR["DefaultTweet_Message"].ToString();
                Campaign_ID = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Session["Email_Id"] = CampaignDR["Email_ID"].ToString();
                Customer_ID = CampaignDR["Customer_ID"].ToString();
                Session["Customer_Email"] = CampaignDR["Email_ID"].ToString();
                Session["Customer_Id"] = CampaignDR["Customer_ID"].ToString();
                if (!IsPostBack)
                {
                    txtComment.Value = CampaignDR["DefaultTweet_Message"].ToString();
                    hfComment.Value = CampaignDR["DefaultTweet_Message"].ToString();
                    int LengthMsg = 115 - hfComment.Value.Length;
                    txtcount.Text = LengthMsg.ToString();
                    if (Page.RouteData.Values["OfferID"] != null)
                    {
                     
                    }
                }
                //Facebook  share Increment
            }
            DBAccess.InstanceCreation().disconnect();
        }
        if (!IsPostBack)
        {
            if (Request["oauth_token"] != null)
            {

                Twitterizer.OAuthTokenResponse accessTokenResponse = twitterobj.GetAccessToken(consumerKey, consumerSecret,
                                                                          Request.QueryString["oauth_token"],
                                                                          Request.QueryString["oauth_verifier"]);


                //Tweet
                Twitterizer.OAuthTokens tokens = new Twitterizer.OAuthTokens()
                {
                    AccessToken = accessTokenResponse.Token,
                    AccessTokenSecret = accessTokenResponse.TokenSecret,
                    ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                    ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString()
                };
                Twitterizer.TwitterResponse<Twitterizer.TwitterUser> twitterResponse = Twitterizer.TwitterAccount.VerifyCredentials(tokens);

                //Count Followers
                string TwitterResult = twitterResponse.Content;
                int iStart = TwitterResult.IndexOf("followers_count") + 16;
                TwitterResult = TwitterResult.Substring(iStart, 4);
                Followers = TwitterResult.Replace(':', ' ').Replace('}', ' ').Replace(',', ' ').Replace('"', ' ');
                //Count Followers              
                //Insert Into Customer social share tokens twitter
                try
                {
                    objCSST.TokenID = 0;
                    objCSST.FacebookId = "";
                    objCSST.TwitterId = accessTokenResponse.UserId.ToString();
                    objCSST.FacebookAccessToken = "";
                    objCSST.TwitterAccessToken = accessTokenResponse.Token;
                    objCSST.TwitterAccessTokenSecret = accessTokenResponse.TokenSecret;
                    objCSST.TotalFriends = "";
                    objCSST.Follower = Followers + "";
                    objCSST.CustomerId = Convert.ToInt32(Session["Customer_Id"].ToString());
                    objCSST.Gender = "";
                    objCSST.TwitterUserName = accessTokenResponse.ScreenName;
                    sqlPlugin.InsertInToCustomer_Social_Share_Tokens_Twitter(objCSST);

                    //Insert Into Customer social share tokens twitter

                    if (twitterResponse.Result == Twitterizer.RequestResult.Success)
                    {
                        string strShortURL = ShortURL(URL + Session["OfferID"].ToString());
                        Twitterizer.TwitterResponse<Twitterizer.TwitterStatus> twitterRes = Twitterizer.TwitterStatus.Update(tokens, Session["Tweet"].ToString() + " " + strShortURL, null);
                        litMsg.Visible = true;

                        if (twitterRes.Result == Twitterizer.RequestResult.Success)
                        {

                           

                            //Add Campaign twitter share text 
                            _Offer_Posts objCustomerSocialSharePost = new _Offer_Posts();
                            objCustomerSocialSharePost.Campaign_Id = Convert.ToString(Campaign_ID);
                            objCustomerSocialSharePost.Customer_Id = Customer_ID;
                            objCustomerSocialSharePost.Post_Location = 2;
                            objCustomerSocialSharePost.Text = Session["Tweet"].ToString();
                            objCustomerSocialSharePost.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"];
                            int resultShare = Campaignsqlobj.InsertIntoCampaignSharetext(objCustomerSocialSharePost);
                            //Add Campaign twitter share text 
   
                            //Insert  Email to Customer_Shares
                            _Customer_Shares objCustomer_Shares = new _Customer_Shares();
                            objCustomer_Shares.Post_Id = resultShare;
                            objCustomer_Shares.Recipient_Email = Session["Customer_Email"].ToString();
                            Campaignsqlobj.InsertIntoCustomerShares(objCustomer_Shares);
                            //Insert  Email to Customer_Shares                           

                            //insert into offer table twitter
                            _Offer sqlOfferfb = new _Offer();
                            sqlOfferfb.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                            sqlOfferfb.Customer_Id = Customer_ID.ToString();
                            sqlOfferfb.Campaign_Id = Campaign_ID.ToString();
                            sqlOfferfb.Expiry_Time = DateTime.Now;
                            sqlOfferfb.Clicks = "0";
                            sqlOfferfb.Reach = Followers + "";
                            sqlOfferfb.Referrals = 0;
                            sqlOfferfb.Sales = 0;
                            sqlOfferfb.Referrer_Credits = 0;
                            sqlOfferfb.Status = 9;
                            sqlOfferfb.TransactionId = 0;
                            DAL.Plugin offerobjfb = new DAL.Plugin();
                            offerobjfb.InsertInToOfferDetails(sqlOfferfb);
                            // end of insert
                            _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

                            objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID);
                            objCampaigns_Stats.FB_click = 0;
                            objCampaigns_Stats.FBShare_Click = 0;
                            objCampaigns_Stats.Link_Click = 0;
                            objCampaigns_Stats.Proceed_Click = 0;
                            objCampaigns_Stats.Tweet_Click = 1;
                            objCampaigns_Stats.Email_Click = 0;
                            objCampaigns_Stats.EmailSubmit_Click = 0;
                            objCampaigns_Stats.StatusClick = 8;
                            int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
                            //insert into offer table twitter
                            _Offer sqlOffer = new _Offer();
                            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                            sqlOffer.Customer_Id = Customer_ID.ToString();
                            sqlOffer.Campaign_Id = Campaign_ID.ToString();
                            sqlOffer.Expiry_Time = DateTime.Now;
                            sqlOffer.Clicks = "0";
                            sqlOffer.Reach = Followers + "";
                            sqlOffer.Referrals = 0;
                            sqlOffer.Sales = 0;
                            sqlOffer.Referrer_Credits = 0;
                            sqlOffer.Status = 3;
                            sqlOffer.TransactionId = 0;
                            DAL.Plugin offerobj = new DAL.Plugin();
                            offerobj.InsertInToOfferDetails(sqlOffer);
                            // end of insert

                            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>alert('Tweet Successful');self.close();</script>");
                        }
                        else
                        {
                            litMsg.Text = "<span style=\"color:red;\">Please Update tweet message.Duplicate tweet can not be posted.</span>";
                        }
                    }
                    else
                    {
                        litMsg.Text = "<span style=\"color:red;\">Not Authorized.</span>";
                    }
                    //Tweet
                }
                catch (Exception ex)
                {
                    litMsg.Text = "<span style=\"color:red;\">Some error occurred.Please try again.</span>";
                }

                
            }
        }
    }

    protected void ibtn_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (hfComment.Value == "")
            {
                Session["Tweet"] = txtComment.Value;
            }
            else
            {
                Session["Tweet"] = hfComment.Value;
            }
            DAL.Plugin Campaignsqlobj = new DAL.Plugin();
            _Customer_Social_Share_Tokens objCustomer_Social_Share_Tokens = new _Customer_Social_Share_Tokens();
            objCustomer_Social_Share_Tokens.CustomerId =Convert.ToInt32(Customer_ID);
            SqlDataReader drPlugin = sqlPlugin.CustomerEmailExistForTwitterTokens(objCustomer_Social_Share_Tokens);
            if (drPlugin.Read())
            {
                if (drPlugin["Twitter_Access_Token"].ToString()!=null && drPlugin["Twitter_Access_Token"].ToString()!="")
                {
                    Session["Customer_Id"] = drPlugin["Customer_Id"] + "";


                    //Tweet
                    Twitterizer.OAuthTokens tokens = new Twitterizer.OAuthTokens()
                    {
                        AccessToken = drPlugin["Twitter_Access_Token"].ToString(),
                        AccessTokenSecret = drPlugin["Twitter_Access_Token_Secret"].ToString(),
                        ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                        ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString()
                    };
                    Twitterizer.TwitterResponse<Twitterizer.TwitterUser> twitterResponse = Twitterizer.TwitterAccount.VerifyCredentials(tokens);

                    //Count Followers
                    string TwitterResult = twitterResponse.Content;
                    int iStart = TwitterResult.IndexOf("followers_count") + 16;
                    TwitterResult = TwitterResult.Substring(iStart, 4);
                    Followers = TwitterResult.Replace(':', ' ').Replace('}', ' ').Replace(',', ' ').Replace('"', ' ');
                    //Count Followers

                    //Insert Into Customer social share tokens twitter
                    if (twitterResponse.Result == Twitterizer.RequestResult.Success)
                    {
                        string strShortURL = ShortURL(URL + Session["OfferID"].ToString());
                        Twitterizer.TwitterResponse<Twitterizer.TwitterStatus> twitterRes = Twitterizer.TwitterStatus.Update(tokens, Session["Tweet"].ToString() + " " + strShortURL, null);
                        litMsg.Visible = true;
                        if (twitterRes.Result == Twitterizer.RequestResult.Success)
                        {  


                            //Add Campaign twitter share text 
                            _Offer_Posts objCustomerSocialSharePost = new _Offer_Posts();
                            objCustomerSocialSharePost.Campaign_Id = Convert.ToString(Campaign_ID);
                            objCustomerSocialSharePost.Customer_Id = Customer_ID;
                            objCustomerSocialSharePost.Post_Location = 2;
                            objCustomerSocialSharePost.Text = Session["Tweet"].ToString();
                            objCustomerSocialSharePost.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"];
                            int resultShare = Campaignsqlobj.InsertIntoCampaignSharetext(objCustomerSocialSharePost);
                            //Add Campaign twitter share text 

                            //Insert  Email to Customer_Shares
                            _Customer_Shares objCustomer_Shares = new _Customer_Shares();
                            objCustomer_Shares.Post_Id = resultShare;
                            objCustomer_Shares.Recipient_Email = Session["Customer_Email"].ToString();
                            Campaignsqlobj.InsertIntoCustomerShares(objCustomer_Shares);
                            //Insert  Email to Customer_Shares

                            //insert into offer table twitter
                            _Offer sqlOfferfb = new _Offer();
                            sqlOfferfb.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                            sqlOfferfb.Customer_Id = Customer_ID.ToString();
                            sqlOfferfb.Campaign_Id = Campaign_ID.ToString();
                            sqlOfferfb.Expiry_Time = DateTime.Now;
                            sqlOfferfb.Clicks = "0";
                            sqlOfferfb.Reach = Followers + "";
                            sqlOfferfb.Referrals = 0;
                            sqlOfferfb.Sales = 0;
                            sqlOfferfb.Referrer_Credits = 0;
                            sqlOfferfb.Status = 9;
                            sqlOfferfb.TransactionId = 0;
                            DAL.Plugin offerobjfb = new DAL.Plugin();
                            offerobjfb.InsertInToOfferDetails(sqlOfferfb);
                            // end of insert
                            _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

                            objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID);
                            objCampaigns_Stats.FB_click = 0;
                            objCampaigns_Stats.FBShare_Click = 0;
                            objCampaigns_Stats.Link_Click = 0;
                            objCampaigns_Stats.Proceed_Click = 0;
                            objCampaigns_Stats.Tweet_Click = 1;
                            objCampaigns_Stats.Email_Click = 0;
                            objCampaigns_Stats.EmailSubmit_Click = 0;
                            objCampaigns_Stats.StatusClick = 8;
                            int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
                            //insert into offer table twitter
                            _Offer sqlOffer = new _Offer();
                            sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                            sqlOffer.Customer_Id = Customer_ID.ToString();
                            sqlOffer.Campaign_Id = Campaign_ID.ToString();
                            sqlOffer.Expiry_Time = DateTime.Now;
                            sqlOffer.Clicks = "0";
                            sqlOffer.Reach = Followers + "";
                            sqlOffer.Referrals = 0;
                            sqlOffer.Sales = 0;
                            sqlOffer.Referrer_Credits = 0;
                            sqlOffer.Status = 3;
                            sqlOffer.TransactionId = 0;
                            DAL.Plugin offerobj = new DAL.Plugin();
                            offerobj.InsertInToOfferDetails(sqlOffer);
                            // end of insert
                            litMsg.Text = "<span style=\"color:green;\">Tweet Scccessfully.</span>";
                           ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>alert('Tweet Successful');self.close();</script>");


                        }
                        else
                        {
                            litMsg.Text = "<span style=\"color:red;\">Please Update tweet message.Duplicate tweet can not be posted.</span>";
                        }
                    }
                    else
                    {
                        litMsg.Text = "<span style=\"color:red;\">Not Authorized.</span>";
                    }
                    //Tweet


                }
                else
                {
                    if (Request["oauth_token"] == null)
                    {
                        ////insert into offer table twitter
                        //_Offer sqlOfferfb = new _Offer();
                        //sqlOfferfb.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                        //sqlOfferfb.Customer_Id = Customer_ID.ToString();
                        //sqlOfferfb.Campaign_Id = Campaign_ID.ToString();
                        //sqlOfferfb.Expiry_Time = DateTime.Now;
                        //sqlOfferfb.Clicks = "0";
                        //sqlOfferfb.Reach = Followers + "";
                        //sqlOfferfb.Referrals = 0;
                        //sqlOfferfb.Sales = 0;
                        //sqlOfferfb.Referrer_Credits = 0;
                        //sqlOfferfb.Status = 9;
                        //sqlOfferfb.TransactionId = 0;
                        //DAL.Plugin offerobjfb = new DAL.Plugin();
                        //offerobjfb.InsertInToOfferDetails(sqlOfferfb);
                        //// end of insert
                        //Response.Write("4");
                        //_Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

                        //objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID);
                        //objCampaigns_Stats.FB_click = 0;
                        //objCampaigns_Stats.FBShare_Click = 0;
                        //objCampaigns_Stats.Link_Click = 0;
                        //objCampaigns_Stats.Proceed_Click = 0;
                        //objCampaigns_Stats.Tweet_Click = 1;
                        //objCampaigns_Stats.Email_Click = 0;
                        //objCampaigns_Stats.EmailSubmit_Click = 0;
                        //objCampaigns_Stats.StatusClick = 8;
                        //int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
                        //Response.Write("5");
                        ////insert into offer table twitter
                        //_Offer sqlOffer = new _Offer();
                        //sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                        //sqlOffer.Customer_Id = Customer_ID.ToString();
                        //sqlOffer.Campaign_Id = Campaign_ID.ToString();
                        //sqlOffer.Expiry_Time = DateTime.Now;
                        //sqlOffer.Clicks = "0";
                        //sqlOffer.Reach = Followers + "";
                        //sqlOffer.Referrals = 0;
                        //sqlOffer.Sales = 0;
                        //sqlOffer.Referrer_Credits = 0;
                        //sqlOffer.Status = 3;
                        //sqlOffer.TransactionId = 0;
                        //DAL.Plugin offerobj = new DAL.Plugin();
                        //offerobj.InsertInToOfferDetails(sqlOffer);
                        //Response.Write("6");
                        //// end of insert

                        //string callbackUrl = ConfigurationManager.AppSettings["pageURL"] + "Plugin/TwitterTweet.aspx";
                        string callbackUrl = ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/TwShare/"+Session["OfferID"];
                        string requestToken = twitterobj.GetRequestToken(consumerKey, consumerSecret, callbackUrl);

                        Uri authenticationUri = twitterobj.BuildAuthorizationUri(requestToken);
                        Response.Redirect(authenticationUri.AbsoluteUri);
                    }
                }
            }
            else
            {
                if (Request["oauth_token"] == null)
                {
                    ////insert into offer table twitter
                    //_Offer sqlOfferfb = new _Offer();
                    //sqlOfferfb.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                    //sqlOfferfb.Customer_Id = Customer_ID.ToString();
                    //sqlOfferfb.Campaign_Id = Campaign_ID.ToString();
                    //sqlOfferfb.Expiry_Time = DateTime.Now;
                    //sqlOfferfb.Clicks = "0";
                    //sqlOfferfb.Reach = Followers + "";
                    //sqlOfferfb.Referrals = 0;
                    //sqlOfferfb.Sales = 0;
                    //sqlOfferfb.Referrer_Credits = 0;
                    //sqlOfferfb.Status = 9;
                    //sqlOfferfb.TransactionId = 0;
                    //DAL.Plugin offerobjfb = new DAL.Plugin();
                    //offerobjfb.InsertInToOfferDetails(sqlOfferfb);
                    //// end of insert
                    //Response.Write("7");
                    //_Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

                    //objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID);
                    //objCampaigns_Stats.FB_click = 0;
                    //objCampaigns_Stats.FBShare_Click = 0;
                    //objCampaigns_Stats.Link_Click = 0;
                    //objCampaigns_Stats.Proceed_Click = 0;
                    //objCampaigns_Stats.Tweet_Click = 1;
                    //objCampaigns_Stats.Email_Click = 0;
                    //objCampaigns_Stats.EmailSubmit_Click = 0;
                    //objCampaigns_Stats.StatusClick = 8;
                    //int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
                    //Response.Write("8");
                    ////insert into offer table twitter
                    //_Offer sqlOffer = new _Offer();
                    //sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"].ToString();
                    //sqlOffer.Customer_Id = Customer_ID.ToString();
                    //sqlOffer.Campaign_Id = Campaign_ID.ToString();
                    //sqlOffer.Expiry_Time = DateTime.Now;
                    //sqlOffer.Clicks = "0";
                    //sqlOffer.Reach = Followers + "";
                    //sqlOffer.Referrals = 0;
                    //sqlOffer.Sales = 0;
                    //sqlOffer.Referrer_Credits = 0;
                    //sqlOffer.Status = 3;
                    //sqlOffer.TransactionId = 0;
                    //DAL.Plugin offerobj = new DAL.Plugin();
                    //offerobj.InsertInToOfferDetails(sqlOffer);
                    //// end of insert
                    //Response.Write("9");

                    //string callbackUrl = ConfigurationManager.AppSettings["pageURL"] + "Plugin/TwitterTweet.aspx";
                    string callbackUrl =  ConfigurationManager.AppSettings["pageURL"].ToString()+"Plugin/TwShare/" + Session["OfferID"];
                    string requestToken = twitterobj.GetRequestToken(consumerKey, consumerSecret, callbackUrl);

                    Uri authenticationUri = twitterobj.BuildAuthorizationUri(requestToken);
                    Response.Redirect(authenticationUri.AbsoluteUri);
                }
            }
            DBAccess.InstanceCreation().disconnect();
        }
        catch (Exception ex)
        {
            litMsg.Text = "<span style=\"color:red;\">Some error occurred.Please try again.</span>";
        }
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
