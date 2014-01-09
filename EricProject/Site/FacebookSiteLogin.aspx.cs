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
using System.Data.SqlClient;
using System.IO;
using Encryption64;

namespace EricProject.Site
{
    public partial class FacebookSiteLogin : System.Web.UI.Page
    { string PublicKey = "";
   
        public FacebookSiteLogin()
        {
            PublicKey = ConfigurationManager.AppSettings["PublicKey"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["token"]!= null)
            {
                string token = Request.QueryString["token"];

                string HitURL = string.Format("https://graph.facebook.com/me?access_token={0}", token);
                oAuthFacebook objFbCall = new oAuthFacebook();
                string JSONInfo = objFbCall.WebRequest(oAuthFacebook.Method.GET, HitURL, "");

                JObject Job = JObject.Parse(JSONInfo);
                JToken Jdata = Job.Root;

                if (Jdata.HasValues)
                {
                    string UID = (string)Jdata.SelectToken("id");
                    string firstname = (string)Jdata.SelectToken("first_name");
                    string lastname = (string)Jdata.SelectToken("last_name");
                    string email = (string)Jdata.SelectToken("email");

                    string UserName = firstname + " " + lastname;
                    Session["UserName"] = UserName + "";
                    Session["firstname"] = firstname + "";
                    Session["lastname"] = lastname + "";
                    Session["CustomerEmail"] = email + "";
                    Session["CustomerEmailId"] = email;


                    _plugin objPlugin1 = new _plugin();
                    objPlugin1.EmailID = email;
                    objPlugin1.Password = "";

                    DAL.Plugin sqlPlugin = new DAL.Plugin();
                    SqlDataReader drPlugin1 = sqlPlugin.CheckCustomerLogin(objPlugin1);
                    while (drPlugin1.Read())
                    {
                        if (drPlugin1["Customer_Id"].ToString() != "0")
                        {
                            if (Convert.ToBoolean(drPlugin1["Is_Facebook"].ToString()) == false)
                            {
                                _plugin objCredits = new _plugin();
                                objCredits.EmailID = email;
                                SqlDataReader drcredits = sqlPlugin.Is_credits(objCredits);
                                if (drcredits.Read())
                                {
                                    if (Convert.ToInt32(drcredits["Credits"].ToString()) > 0)
                                    {
                                        string EmailContent = "";
                                        string URL = Server.MapPath("~/EmailTemplate/CustomerThanks/CustomerActivation.htm");
                                        StreamReader SR = new StreamReader(URL);
                                        EmailContent = SR.ReadToEnd();
                                        SR.Close();
                                        EmailContent = EmailContent.Replace("{Link}", "Please <a href='"+ConfigurationManager.AppSettings["pageURL"]+"Site/Custometer/Activation?u=" + Server.UrlEncode(email + "^" + 1) + "'>Click here</a> To Activate Your Account.<br/><br/>");
                                        EmailContent = GetEmailHeaderFooter(email).Replace("{BODYCONTENT}", EmailContent);
                                        comman.SendMail(email, "Activation Mail", EmailContent);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "alert('Please check your email to activate');window.location.href='" + ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Login" + "';", true);
                                    }
                                    else
                                    {
                                        _plugin objPlugin = new _plugin();
                                        objPlugin.Customer_ID = 0;
                                        objPlugin.FirstName = firstname;
                                        objPlugin.LastName = lastname;
                                        objPlugin.EmailID = email;
                                        objPlugin.Password = "";
                                        objPlugin.IsActive = false;
                                        objPlugin.Address = "";
                                        objPlugin.City = "";
                                        objPlugin.State = "";
                                        objPlugin.Country_ID = "";
                                        objPlugin.Zip = 1;
                                        objPlugin.PhoneNumber = "";
                                        objPlugin.IsFacebook = true;
                                        objPlugin.IsTwitter = false;
                                        objPlugin.Status = "1";
                                        objPlugin.Facebook_Id = UID+"";
                                        SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);
                                        Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                                        Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/FacebookSuccess.aspx");

                                    }
                                }
                            }
                            else
                            {
                                _plugin objPlugin = new _plugin();
                                objPlugin.Customer_ID = 0;
                                objPlugin.FirstName = firstname;
                                objPlugin.LastName = lastname;
                                objPlugin.EmailID = email;
                                objPlugin.Password = "";
                                objPlugin.IsActive = false;
                                objPlugin.Address = "";
                                objPlugin.City = "";
                                objPlugin.State = "";
                                objPlugin.Country_ID = "";
                                objPlugin.Zip = 1;
                                objPlugin.PhoneNumber = "";
                                objPlugin.IsFacebook = true;
                                objPlugin.IsTwitter = false;
                                objPlugin.Status = "1";
                                objPlugin.Facebook_Id = UID+"";
                                SqlDataReader dr = sqlPlugin.UpdateCustomer_Master(objPlugin);
                                Session["CustomerID"] = drPlugin1["Customer_ID"].ToString();
                                Session["CustomerEmailId"] = drPlugin1["Email_Id"].ToString();
                                Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/FacebookSuccess.aspx");


                            }
                        }
                        else
                        {
                            objPlugin1.Facebook_Id = UID;
                            SqlDataReader drCheck_Customer_Exist_By_FacebookID = sqlPlugin.Sp_Check_Customer_Exist_By_FacebookID(objPlugin1);
                            while (drCheck_Customer_Exist_By_FacebookID.Read())
                            {
                                if (drCheck_Customer_Exist_By_FacebookID["Exists"].ToString() == "0")
                                {
                                    _plugin objPlugin = new _plugin();
                                    objPlugin.Customer_ID = 0;
                                    objPlugin.FirstName = firstname;
                                    objPlugin.LastName = lastname;
                                    objPlugin.EmailID = email;
                                    objPlugin.Password = "";
                                    objPlugin.Address = "";
                                    objPlugin.City = "";
                                    objPlugin.State = "";
                                    objPlugin.Country_ID = "";
                                    objPlugin.Zip = 1;
                                    objPlugin.PhoneNumber = "";
                                    objPlugin.IsFacebook = true;
                                    objPlugin.IsTwitter = false;
                                    objPlugin.IsActive = false;
                                    objPlugin.Facebook_Id = UID + "";
                                    SqlDataReader dr = sqlPlugin.InsertIntoCustomer_Master(objPlugin);
                                    while (dr.Read())
                                    {
                                        Session["CustomerID"] = dr["Customer_ID"].ToString();
                                        Session["CustomerEmailId"] = dr["Email_Id"].ToString();
                                        Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/FacebookSuccess.aspx");
                                    }
                                }
                                if (drCheck_Customer_Exist_By_FacebookID["Exists"].ToString() == "1")
                                {
                                    Session["CustomerID"] = drCheck_Customer_Exist_By_FacebookID["Customer_ID"].ToString();
                                    Session["CustomerEmailId"] = drCheck_Customer_Exist_By_FacebookID["Email_Id"].ToString();
                                    Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/FacebookSuccess.aspx");
                                }
                            }
                            

                        }
                    }
                    DBAccess.InstanceCreation().disconnect();
                    //Insert into Customer Table              
                }

            }
            else
            {
               // Response.Write(Request.QueryString["error"].ToString());
                //Response.Redirect(ConfigurationManager.AppSettings["pageURL"] + "" + "Site/Customer/Login");
                ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>self.close();</script>");

            }
        }
        public string GetEmailHeaderFooter(string Email)
        {
            //Header Footer Email Code
            StreamReader HeaderFooterSR = new StreamReader(Server.MapPath("~/EmailTemplate/Standard/Header_Footer.htm"));
            string HeaderFooter = HeaderFooterSR.ReadToEnd();
            HeaderFooterSR.Close();
            string BasePath = ConfigurationManager.AppSettings["pageURL"].ToString();
            HeaderFooter = HeaderFooter.Replace("{site_name}", ConfigurationManager.AppSettings["site_name"].ToString());
            HeaderFooter = HeaderFooter.Replace("{logoURL}", "<img src='" + ConfigurationManager.AppSettings["pageURL"].ToString() + "images/newimages/logo.png' alt='" + ConfigurationManager.AppSettings["site_name"].ToString() + "' title='" + ConfigurationManager.AppSettings["site_name"].ToString() + "'/>");
            HeaderFooter = HeaderFooter.Replace("{UNSUBSCRIBEURL}", BasePath + "Site/EmailUnsubscription.aspx?e=" + Server.UrlEncode(new EncryptDecrypt().Encrypt(Email, PublicKey)));
            return HeaderFooter;
            //Header Footer Email Code
        }
    }

}