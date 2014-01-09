using BAL;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Script.Serialization;

namespace EricProject.Site
{
    public partial class CreditRedeem : System.Web.UI.Page
    {
        string CustomerId;
        public int UnredeemedCredits = 0;
        public string CustomerEmail = "";
        public string hostname = "https://api.sandbox.paypal.com";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["code"] != null && Request.QueryString["code"].Length == 176)
                {
                    string access_token = "";
                    string code = Request.QueryString["code"].ToString();
                    string sAPIEndpoint = hostname + "/v1/identity/openidconnect/tokenservice?client_id=AcBjtBB6OFzYFX-eLEpQFAD0ZUKHdcDBL9KZfCw6w3GKFP4mtqDwfMT3Ay6w&client_secret=EELLiBD2j2LqhiFoZyAaJcB3zt4FMXWrF1AWpfbn7mqIzcmmdktL7xJ6RTom&grant_type=authorization_code&code=" + code + "redirect_uri="+ConfigurationManager.AppSettings["pageURL"]+"Site/CreditRedeem.aspx";
                    //Response.Redirect(sAPIEndpoint);

                    WebRequest request = WebRequest.Create(sAPIEndpoint);
                    request.Method = "GET";

                    using (WebResponse response = request.GetResponse())
                    {
                        StreamReader oStreamReader = new StreamReader(response.GetResponseStream());
                        string jsonresult = oStreamReader.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Dictionary<string, object> result = js.Deserialize<dynamic>(jsonresult);
                        access_token = result["access_token"].ToString();
                        oStreamReader.Close();
                    }

                    request = WebRequest.Create(hostname + "/v1/identity/openidconnect/userinfo?schema=openid&access_token=" + access_token);
                    using (WebResponse response = request.GetResponse())
                    {
                        StreamReader oStreamReader = new StreamReader(response.GetResponseStream());
                        string jsonresult = oStreamReader.ReadToEnd();
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        Dictionary<string, object> result = js.Deserialize<dynamic>(jsonresult);
                        string Email = result["email"] as string;
                        string Name = result["name"] as string;
                        Session["isValidPaypal"] = true;
                        Session["PaypalUserEmail"] = Email;
                        Session["PaypalUserFullName"] = Name;
                        oStreamReader.Close();
                    }
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "opener.redirect('" + ConfigurationManager.AppSettings["pageURL"] + "Site/CustomerCreditDetails.aspx');window.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "", "opener.redirect('" + ConfigurationManager.AppSettings["pageURL"] + "Site/CustomerCreditDetails.aspx');window.close();", true);
                }
            }
        }
    }
}