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


public partial class Plugin_FacebookAuthorise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BusinessObject.Facebook fb = new FacebookService().GetById(99); // we haven't saved anything in any database, so we are getting a fake'd object;
        string facebookClientId = System.Configuration.ConfigurationManager.AppSettings["FacebookClientId"];
        string host = Request.ServerVariables["HTTP_HOST"];

        // remember to fix the Url for your own development environment
        //litAuthoriseUrl.Text = string.Concat(@"<a href=""https://graph.facebook.com/oauth/authorize?client_id=" + facebookClientId + "&",
        //                                            "redirect_uri=http://" + host + @"/Plugin/FacebookCompleteAuthorisation.aspx&scope=user_photos,user_videos,publish_stream,offline_access,user_photo_video_tags"">Authorise user</a>");

        Response.Redirect("https://graph.facebook.com/oauth/authorize?client_id=" + facebookClientId + "&redirect_uri="+ConfigurationManager.AppSettings["pageURL"]+"Plugin/FacebookCompleteAuthorisation.aspx&scope=user_photos,user_videos,publish_stream,offline_access,user_photo_video_tags");

        //Response.Redirect("https://graph.facebook.com/oauth/authorize?client_id=362001227199877&redirect_uri=http://localhost:2180/EricProject/Plugin/FacebookCompleteAuthorisation.aspx&scope=user_photos,user_videos,publish_stream,offline_access,user_photo_video_tags");
    }
}
