using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BAL;
using DAL;
using BusinessObject;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Xml;
using oAuthExample;


public partial class Site_TwitterSuccess : System.Web.UI.Page
{
    public string name = "";
    public string username = "";
    public string profileImage = "";
    public string followersCount = "";
    public string noOfTweets = "";
    public string recentTweet = "";
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
