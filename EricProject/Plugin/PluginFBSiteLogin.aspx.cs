using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using BAL;
using DAL;
using BusinessObject;


public partial class Plugin_PluginFBSiteLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["response"] + "" == "")
        {
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/FacebookAuthorise.aspx");
        }
        BusinessObject.Facebook fb = new FacebookService().GetById(99);

        FacebookFriendCounts FriendsCount = new FacebookService().Friend_GetDetails(fb.FacebookAccessToken);
        FacebookProfile objFacebookProfile = new FacebookService().User_GetDetails(fb.FacebookAccessToken);

        string token = Request.QueryString["token"];
        string HitURL = string.Format("https://graph.facebook.com/me?access_token={0}", token);
        oAuthFacebookPlugin objFbCall = new oAuthFacebookPlugin();
        string JSONInfo = objFbCall.WebRequest(oAuthFacebookPlugin.Method.GET, HitURL, "");

        JObject Job = JObject.Parse(JSONInfo);
        JToken Jdata = Job.Root;
        string Gender = objFacebookProfile.Gender;
        if (Jdata.HasValues)
        {
            string UID = (string)Jdata.SelectToken("id");
            string firstname = (string)Jdata.SelectToken("first_name");
            string lastname = (string)Jdata.SelectToken("last_name");
            //string email = (string)Jdata.SelectToken("email");
            string UserName = firstname + " " + lastname;



        
     
            //Insert into Customer_Social_Share_Tokens
            DAL.Plugin sqlPlugin = new DAL.Plugin();
            _Customer_Social_Share_Tokens objCSST = new _Customer_Social_Share_Tokens();
            objCSST.TokenID = 0;
            objCSST.FacebookId =  UID ;
            objCSST.FacebookAccessToken = fb.FacebookAccessToken;
            objCSST.TwitterAccessToken = "";
            objCSST.TotalFriends = FriendsCount.data.Count.ToString();
            objCSST.CustomerId = Convert.ToInt32(Session["PluginCustomerID"].ToString());
            objCSST.Gender = Gender;
            if(Session["CheckTokenExpire"]+""!="1")
            {
                int result = sqlPlugin.InsertIntoCustomerSocialShareTokens(objCSST);
            }
            else
            {
                objCSST.CustomerId = Convert.ToInt32(Session["PluginCustomerID"].ToString());
                objCSST.FacebookAccessToken = fb.FacebookAccessToken;
                int result = sqlPlugin.UpdateCustomerSocialShareTokens(objCSST);
            }
            //Insert into Customer_Social_Share_Tokens
            Session["response"] = "";
            Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Plugin/FacebookShare.aspx");
        }
    }
}
