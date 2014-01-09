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


public partial class Plugin_FacebookShare : System.Web.UI.Page
{

    DAL.Plugin Campaignsqlobj = new DAL.Plugin();
    public string DefaultFaceBook_Title = "";
    public string Campaign_Image = "";
    public string OfferID = "";
    public string Campaign_Name = "";
    public string Campaign_Title = "";
    public string Customer_Email = "";
    public string Customer_ID = "";
    public int Campaign_ID = 0;
    string facebookClientId = System.Configuration.ConfigurationManager.AppSettings["FacebookClientId"];
    string facebookSecret = System.Configuration.ConfigurationManager.AppSettings["FacebookSecret"];
    public string URL = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Shared/F/";
    public string strShortURL = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["OfferID"] != null || Session["OfferID"].ToString() != null)
        {

            _TransactionDetails TransOBJ = new _TransactionDetails();
            if (Page.RouteData.Values["OfferID"] != null)
            {
                OfferID = Page.RouteData.Values["OfferID"].ToString();
            }
            else
            {
                OfferID = Session["OfferID"].ToString();
            }
            TransOBJ.Offer_ID = Convert.ToInt32(OfferID);
            SqlDataReader CampaignDR = Campaignsqlobj.CheckFacebookDetails(TransOBJ);

            //Short Facebook share url
            strShortURL = ShortURL(URL + OfferID);

            //Short Facebook share url
            strShortURL = ShortURL(URL + OfferID);

            Session["OfferID"] = OfferID;
            if (CampaignDR.Read())
            {
                DefaultFaceBook_Title = CampaignDR["DefaultFaceBook_Title"].ToString();
                if (DefaultFaceBook_Title.Contains("{Product Name}."))
                {
                    if (CampaignDR["SKU_ID"].ToString() == "0")
                    {
                        DefaultFaceBook_Title = DefaultFaceBook_Title.Replace("{Product Name}.", CampaignDR["Company_Name"] + "");
                    }
                    else
                    {
                        DefaultFaceBook_Title = DefaultFaceBook_Title.Replace("{Product Name}.", CampaignDR["Product_Name"] + "");
                    }
                }
                Campaign_ID = Convert.ToInt32(CampaignDR["Campaign_ID"].ToString());
                Campaign_Image = CampaignDR["Campaign_Image"].ToString();
                if (Campaign_Image == "")
                    imgDiv.Visible = false;
                Campaign_Name = CampaignDR["Campaign_Name"].ToString();
                Campaign_Title = CampaignDR["Item_Name"].ToString();
                Customer_Email = CampaignDR["Email_ID"].ToString();
                Customer_ID = CampaignDR["Customer_ID"].ToString();
               
                if (!IsPostBack)
                {
                    txtComment.Value = CampaignDR["DefaultFaceBook_ShareText"].ToString();

                   
                }
                //Facebook  share Increment
            }
            DBAccess.InstanceCreation().disconnect();
        }
    }
    protected void offerdeatils(int status,string totalfriends)
    {
        _Offer sqlOffer = new _Offer();
        sqlOffer.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + OfferID;
        sqlOffer.Customer_Id = Customer_ID.ToString();
        sqlOffer.Campaign_Id = Campaign_ID.ToString();
        sqlOffer.Expiry_Time = DateTime.Now;
        sqlOffer.Clicks = "0";
        sqlOffer.Reach = totalfriends;
        sqlOffer.Referrals = 0;
        sqlOffer.Sales = 0;
        sqlOffer.Referrer_Credits = 0;
        sqlOffer.Status = status;
        sqlOffer.TransactionId = 0;
        DAL.Plugin offerobj = new DAL.Plugin();
        offerobj.InsertInToOfferDetails(sqlOffer);

    }
    protected void btnShareLink_Click(object sender, EventArgs e)
    {
        DAL.Plugin Campaignsqlobj = new DAL.Plugin();

        _Customer_Social_Share_Tokens objCustomer_Social_Share_Tokens = new _Customer_Social_Share_Tokens();
        objCustomer_Social_Share_Tokens.CustomerId = Convert.ToInt32(Customer_ID);
        Session["CampaignID"] = Campaign_ID;
        Session["PluginCustomerID"] = Customer_ID;
        if (Campaign_Image != "")
        {
            Campaign_Image = ConfigurationManager.AppSettings["pageURL"] + "images/MerchantImage/" + Campaign_Image;
        }

        Session["OfferID"] = OfferID;
        Session["Campaign_Image"] = Campaign_Image;
        Session["Campaign_Name"] = Campaign_Name;
        Session["Campaign_Title"] = Campaign_Title;
        Session["Comment"] = txtComment.Value;
        Session["DefaultFaceBook_Title"] = DefaultFaceBook_Title;
        Session["Customer_Email"] = Customer_Email;
        Session["Customer_ID"] = Customer_ID;

        DAL.Plugin sqlPlugin = new DAL.Plugin();
        SqlDataReader drPlugin = sqlPlugin.CustomerEmailExistForFacebookShare(objCustomer_Social_Share_Tokens);
        if (drPlugin.Read())
        {
            BusinessObject.Facebook fb = new FacebookService().GetById(99);


            fb.FacebookAccessToken = drPlugin["Facebook_Access_Token"].ToString();
            if (fb.FacebookAccessToken == string.Empty)
                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "Plugin/FacebookAuthorise.aspx");

            //Facebook post share Increment
            _Campaigns_Stats objCampaigns_Stats = new _Campaigns_Stats();

            objCampaigns_Stats.Campaign_Id = Convert.ToInt32(Campaign_ID);
            objCampaigns_Stats.FB_click = 0;
            objCampaigns_Stats.FBShare_Click = 1;
            objCampaigns_Stats.Link_Click = 0;
            objCampaigns_Stats.Proceed_Click = 0;
            objCampaigns_Stats.Tweet_Click = 0;
            objCampaigns_Stats.Email_Click = 0;
            objCampaigns_Stats.EmailSubmit_Click = 0;
            objCampaigns_Stats.StatusClick = 2;
            int resultFacebookShareClick = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats);
            //Facebook post share Increment

            _Campaigns_Stats objCampaigns_Stats1 = new _Campaigns_Stats();
            objCampaigns_Stats1.Campaign_Id = Convert.ToInt32(Campaign_ID);
            objCampaigns_Stats1.FB_click = 1;
            objCampaigns_Stats1.FBShare_Click = 0;
            objCampaigns_Stats1.Link_Click = 0;
            objCampaigns_Stats1.Proceed_Click = 0;
            objCampaigns_Stats1.Tweet_Click = 0;
            objCampaigns_Stats1.Email_Click = 0;
            objCampaigns_Stats1.EmailSubmit_Click = 0;
            objCampaigns_Stats1.StatusClick = 1;
            int resultFacebookShareClick1 = Campaignsqlobj.InsertInToCampaignsStats(objCampaigns_Stats1);
            //Short Facebook share url
            strShortURL = ShortURL(URL + OfferID);

            if (Campaign_Image != "")
            {
                postStatus result1 = new FacebookService().Post_Feed("me", drPlugin["Facebook_Access_Token"].ToString(), strShortURL, Campaign_Image, strShortURL, Campaign_Title, txtComment.Value, DefaultFaceBook_Title);
                if (result1 == null)
                {
                    Session["CheckTokenExpire"] = "1";
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/FacebookAuthorise.aspx");
                }
            }
            else
            {
                postStatus result1 = new FacebookService().Post_Feed("me", drPlugin["Facebook_Access_Token"].ToString(), strShortURL, strShortURL, Campaign_Title, txtComment.Value, DefaultFaceBook_Title);
                if (result1 == null)
                {
                    Session["CheckTokenExpire"] = "1";
                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/FacebookAuthorise.aspx");
                }
            }

            FacebookFriendCounts FriendsCount = new FacebookService().Friend_GetDetails(drPlugin["Facebook_Access_Token"].ToString());
            string CurrentTotalFriends = FriendsCount.data.Count.ToString();
            //insert into offer table
            offerdeatils(3, CurrentTotalFriends);
            offerdeatils(8, CurrentTotalFriends);
            // end of insert

            //Add Campaign share Facebook text 
            _Offer_Posts objCustomerSocialSharePost = new _Offer_Posts();
            objCustomerSocialSharePost.Campaign_Id = Convert.ToString(Campaign_ID);
            objCustomerSocialSharePost.Customer_Id = Customer_ID;
            objCustomerSocialSharePost.Post_Location = 1;
            objCustomerSocialSharePost.Text = txtComment.Value;
            objCustomerSocialSharePost.Referral_Url = ConfigurationManager.AppSettings["pageURL"].ToString() + "Plugin/Share/" + Session["OfferID"];
            int resultShare = Campaignsqlobj.InsertIntoCampaignSharetext(objCustomerSocialSharePost);
            //Add Campaign share Facebook text 

            //Insert  Email to Customer_Shares
            _Customer_Shares objCustomer_Shares = new _Customer_Shares();
            objCustomer_Shares.Post_Id = resultShare;
            objCustomer_Shares.Recipient_Email = Customer_Email;
            Campaignsqlobj.InsertIntoCustomerShares(objCustomer_Shares);

            //Insert  Email to Customer_Shares           
            //ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);self.close();</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>alert('Shared Successful');self.close();</script>");

            DBAccess.InstanceCreation().disconnect();
        }
        else
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/FacebookAuthorise.aspx");
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

